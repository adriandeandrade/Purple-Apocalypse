﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicLayerSorting : MonoBehaviour
{

    public float yOffset;

    public float by;

    float layer;

    SpriteRenderer rend;

    Vector3 centerBottom;

    // Use this for initialization
    void Start()
    {

        rend = GetComponent<SpriteRenderer>();
    }


    void Update()
    {

        centerBottom = transform.TransformPoint(rend.sprite.bounds.min);

        layer = centerBottom.y + yOffset;

        rend.sortingOrder = -(int)(layer * 100);

    }
}
