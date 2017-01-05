﻿using UnityEngine;
using System.Collections;

/// VehicleController
/// Manages vehicle state and activities

public class VehicleController : MonoBehaviour
{
	//===================================
	//===================================
	//		MODULE VARIABLES
	//===================================
	//===================================

	public float heading;
	public float pitch;
    public float velocity;
    


    private Vector3 previousPos;
    private Vector3 HeadingDirection;
	//===================================
	//===================================
	//		INITIALIZATION
	//===================================
	//===================================

    void Start()
    {
        previousPos = transform.position;
	}

    void Update()
    {
        velocity = (transform.position - previousPos).magnitude / Time.deltaTime;
        HeadingDirection = (transform.position - previousPos).normalized;
        previousPos = transform.position;   
    }


    public Vector3 GetHeadingDirection()
    {
        return HeadingDirection;
    }
}


