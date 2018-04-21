using UnityEngine;
using System.Collections;
using System.Linq;

public enum Shadow2DType
{
	Sticky, // Anchored to parent object
	DirectionalRaycast, // Uses raycasts to draw shadow on first collider
	DirectionalRaycastShrink // Same as DirectionalRaycast, except auto scales based on height
}

public enum Shadow2DEffect
{
	None, 		// Shadow remains the solid Shadow Tint color
	Fade 		// Fades shadow to transparent
}

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Easy2DShadow : MonoBehaviour {

	public Color shadowTint = new Color(0,0,0,180); 	// Init shadow to be transparent black
	public Vector3 shadowOffset = new Vector3 (0,0,0.1f); // Init shadow behind the object
	public Shadow2DType shadowType = Shadow2DType.Sticky; // Behavior of the Shadow, sticky remains fixed to parent
	public Shadow2DEffect shadowEffect = Shadow2DEffect.None; // Apply minor effects to the shadow object
	public float shrinkRate = 2;							// How quickly should shadow scale down during Shrink Mode
	public Vector3 rayDirection = new Vector3(0,-1,0); // Project straight "down" (negative Y axis)
	public bool flipped = false;						// Flips shadow vertically
	public bool animated = false;						// Constantly refreshes UV coordinates and mesh vertices
	public float shear = 1;								// Horizontal Shear amount
	public float scale = 1;								// Use Shear and Scale to achieve desired effect

	private Color32[] shadowMeshColor;
	private Vector3 shearOffset = Vector3.zero;
	private Vector3 rayCastHitPosition = Vector3.zero; // world coordinates for raycastposition
	private Vector3 flippedVerts;
	private Vector3 shrinkAmount;	// Used in DirectionalRaycastShrink to auto shrink shadow
	private float shrinkRatio; 		// Shrink based on the height of the mesh

	// Keep reference to shadow mesh, for possible changes
	private GameObject shadowObject;

	// Lazy initialization, only create shadow object when it's needed
	private GameObject ShadowObject
	{
		get 
		{
			if (shadowObject == null)
			{
				shadowObject = new GameObject("2d Shadow");
				shadowObject.transform.parent = this.transform;
				shadowObject.AddComponent<MeshRenderer>();
				shadowObject.AddComponent<MeshFilter>();
			}
			return shadowObject;
		}
	}

	private Mesh shadowMesh;
	private Mesh originalMesh;

	// Skewing Matrix
	private Matrix4x4 transMatrix = Matrix4x4.identity;
	private Vector3[] shadowVerts;

	void OnEnable ()
	{
		RefreshTransformationMatrix();

		// Creates a duplicated mesh
		if (!CreateShadow()) return;

		// Shear the shadow object
		ApplyShearMatrix();

		// Color will be applied
		ApplyShadowEffect();
	}

	void OnDisable ()
	{
		if (shadowObject == null) return;
		shadowObject.SetActive(false);
	}

	void Update ()
	{		
		// Lets you see changes you make in editor
		#if UNITY_EDITOR

			RefreshFlippedVerts();
			RefreshTransformationMatrix();
			RefreshShadowPosition();

		#endif 

		switch (shadowType)
		{
			// Sticky shadow type requires no extra processing
			case Shadow2DType.DirectionalRaycast:
				UpdateRaycast();
				RefreshShadowPosition();
				break;

			case Shadow2DType.DirectionalRaycastShrink:
				UpdateRaycast();
				ApplyShrink();
				RefreshShadowPosition();
				break;
		}

		if (!animated) return;
		// Main object is animated, UVs are changing, keep uvs the same
		RefreshUV();
		ApplyShearMatrix();		
	}

	public void UpdateRaycast ()
	{
		RaycastHit hit;
		if (Physics.Raycast(this.transform.position, rayDirection, out hit))
		{
			rayCastHitPosition = hit.point;	

			if (shadowType == Shadow2DType.DirectionalRaycastShrink) CreateShrinkAmount(hit.distance);
		}
		else 
		{
			// Ray didn't hit any colliders, move off screen
			// Alternatively, can set to local position to make shadow stick to object
			rayCastHitPosition = new Vector3(-900,-900,-900);
		}
	}

	public void RefreshFlippedVerts ()
	{
		flippedVerts = flipped ? new Vector3(1,-1,1) : new Vector3(1,1,1);
	}

	public void RefreshTransformationMatrix ()
	{
		transMatrix.SetRow(0, new Vector4(1,shear,0,0));
		transMatrix.SetRow(1, new Vector4(0,scale,0,0));
	}

	// Clone mesh
	private bool CreateShadow ()
	{
		// Mesh to create shadow from
		originalMesh = this.GetComponent<MeshFilter>().sharedMesh;

		if (originalMesh == null)
		{
			originalMesh = this.GetComponent<MeshFilter>().mesh;

		 	this.GetComponent<Easy2DShadow>().enabled = false;
		 	return false;
		}

		shadowMesh = ShadowObject.GetComponent<MeshFilter>().mesh;

		if (shadowMesh == null) return false;

		ShadowObject.SetActive(true);

		RefreshFlippedVerts();
		RefreshShadowPosition();
		RefreshUV();

		shadowVerts = new Vector3[originalMesh.vertices.Length];
		shadowMesh.RecalculateNormals();

		ShadowObject.GetComponent<MeshFilter>().mesh = shadowMesh;
		ShadowObject.GetComponent<Renderer>().material = this.GetComponent<Renderer>().sharedMaterial;		

		RefreshShrinkRatio();

		return true;
	}

	public void RefreshUV ()
	{
		shadowMesh.vertices = originalMesh.vertices;
		shadowMesh.triangles = originalMesh.triangles;
		shadowMesh.uv = originalMesh.uv;
	}

	public void RefreshShadowPosition ()
	{
		Vector3 newPosition = shadowOffset;
		newPosition += shearOffset;
		switch (shadowType)
		{
			case Shadow2DType.Sticky:
				// This shadow type keeps shadow position relative to parent
				ShadowObject.transform.localPosition = newPosition;
				break;

			case Shadow2DType.DirectionalRaycast:
				// This shadow type projects the shadow in world coordinates
				ShadowObject.transform.position = rayCastHitPosition + newPosition;				
				break;

			case Shadow2DType.DirectionalRaycastShrink:
				ShadowObject.transform.position = rayCastHitPosition + newPosition;
				break;
		}
	}

	public void ApplyShearMatrix ()
	{
		if (shadowMesh.vertices.Length < 1 || shadowVerts.Length < 1) return;

		// Shearing alters the position of the shadow, use this to fix alignment
		Vector3 shearDiff = shadowMesh.vertices[0];

		// Flip verts and apply transformation matrix for shearing effect
		for (int i = 0; i < shadowVerts.Length; i++)
		{
			shadowVerts[i] = transMatrix.MultiplyPoint3x4(Vector3.Scale(shadowMesh.vertices[i], flippedVerts));
		}

		// The shearing difference in positions 
		shearOffset = shearDiff - shadowVerts[0];
		RefreshShadowPosition();

		shadowMesh.vertices = shadowVerts;
	}

	public void CreateShrinkAmount (float hitDistance)
	{
		// Linear scale down
		float newScale = (-1/shrinkRatio) * hitDistance + 1;
		float uniformScale = Mathf.Clamp(newScale,0.0f, 1.0f);
		shrinkAmount = new Vector3(uniformScale,uniformScale,uniformScale);
	}

	public void ApplyShrink ()
	{
		ShadowObject.transform.localScale = shrinkAmount;
	}

	public void ApplyShadowEffect ()
	{
		switch (shadowEffect)
		{
			case Shadow2DEffect.None:
				SetVertexColors();
				break;

			case Shadow2DEffect.Fade:
				FadeVertexColors();
				break;

			default:
				SetVertexColors();
				break;
		}
	}

	private void SetVertexColors ()
	{
		shadowMeshColor = new Color32[shadowMesh.vertices.Length];

		for (int i =0; i < shadowMeshColor.Length; i++)
			shadowMeshColor[i] = shadowTint;

		shadowMesh.colors32 = shadowMeshColor;
	}

	// If mesh changes size, must refresh new ratio
	public void RefreshShrinkRatio ()
	{
		Vector3[] meshVerts = shadowMesh.vertices;
		shrinkRatio = (meshVerts.Max(s => s.y) - meshVerts.Min( s => s.y)) * shrinkRate;
		shrinkRatio = shrinkRatio == 0 ? 1 : shrinkRatio;
	}

	// If flipping sprite dynamically and object isn't animated, manually call RefreshUV to update mesh
	private void FadeVertexColors ()
	{
		Vector3[] meshVerts = shadowMesh.vertices;
		Color32[] shadowMeshColor = new Color32[shadowMesh.vertices.Length];
		//Color shadowFade = shadowTint;
		//shadowFade.a = 0;
		Color shadowFade = new Color(0,0,0,0);
		float maxY = meshVerts.Max(s => s.y);
		float minY = meshVerts.Min(s => s.y);
		float fadeLength = maxY - minY != 0 ? maxY - minY : 1; // Don't risk dividing by zero

		for (int i =0; i < meshVerts.Length; i++)
		{
			float fadeAmount = ((meshVerts[i].y - minY)/ fadeLength);

			shadowMeshColor[i] = flipped ? Color32.Lerp(shadowFade, shadowTint,fadeAmount) : Color32.Lerp(shadowTint, shadowFade,fadeAmount);
		}

		shadowMesh.colors32 = shadowMeshColor;
	}
}
