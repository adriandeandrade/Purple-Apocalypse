Easy 2D Shadows v1.0

Thank you for purchasing Easy 2D Shadows!

Easy 2D Shadows allows you to create shadows for 2D sprites
that use a simple plane mesh. While it is easy to parent
premade shadow planes to objects, that task can take up a
lot of time. Tweaking planes, and arranging textures just right
can be avoided with Easy 2D Shadows.

HOW-TO:
===================================
Step 1:

	To make Easy 2D Shadows to be as easy to use as possible,
	this script makes some assumptions.
	1) You are using a simple mesh for 2D Sprites (such as 2d Toolkit meshes).
	2) You are working in the orthographic "back" view (Positive Y Axis is up, Positive X Axis is right)

-----------------------------------------------------------------------------------------
Step 2:

	Attach Easy2dShadow component to a gameobject that contains a mesh, renderer, and material.
	This script works by copying the gameobject's sprite, cloning it, tinting it black, and applying a transform. 

-----------------------------------------------------------------------------------------
Step 3:

	Once you have a valid gameobject, you can tweak the settings in Easy 2D Shadows to get your desired effect.

	- Shadow Tint: Use black with an optional alpha for best results.

	- Shadow Offset: If your pivot is off, then you must use an offset to line up the shadow.

	- Shadow Type: 
		A) Sticky: Sticks directly to your object. The most simple shadow and should be used for ground objects.
		B) DirectionalRaycast: Raycast will be created, but shadow will only show on colliders.
		C) DirectionalRaycastShrink: Same as previous, but automatically scales shadow based on raycast distance. 

	-Shadow Effect:
		A) None: Shadow is solid.
		B) Fade: Shadow gradually fades from the base Shadow Tint to transparent.

	-Shrink Rate: Only used for DirectionalRaycastShrink Shadow Type, and this value determines how fast the 
					shadow scales based on height.

	-Ray Direction: If using a Raycast Shadow Type, then use this vector to decide where to project shadow.
					use x = 0, y = -1, z = 0 for a "down" projection (negative Y Axis).

	-Flipped: Generally a shadow would be behind a 2d object. This option allows the shadow to be in front of the
				object instead.

	-Animated: Use this option for animated 2D sprites. This will automatically refresh the shadow every frame.

	-Shear: Use this value to "skew"/shift the shadow for a better effect.

	-Scale: Shear, in conjunction with Scale, is generally all you need for good results.

-----------------------------------------------------------------------------------------
Step 4:

	Shadows will be generated at runtime. For advanced techniques, you may eventually want to control Easy 2D Shadow
	values by script. Applying a blue alpha tint for simulating water reflection, updating the shear/scale values,
	for example would be easy to implement.

Additional Notes:
	
	Not every 2D project is setup the same, but based on how 2d Toolkit operates, and other 2d solutions on the asset
	store, then you should be able to use Easy 2D Shadows with almost no tweaking. If you are using your own custom
	2d solution, then you may have to tweak Easy 2D Shadows for your particular setup.

Support:
Email: NicolasRuizNR@yahoo.com
