﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorBetween : MonoBehaviour
{
    [SerializeField] Transform Target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(transform.position, Target.position, Color.cyan);
    }
}
