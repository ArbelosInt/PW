using UnityEngine;
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
    
	public AudioModule AudioSFX;

    private Vector3 previousPos;
    private Vector3 HeadingDirection;
	//===================================
	//===================================
	//		INITIALIZATION
	//===================================
	//===================================
	private bool engineNoiseStarted = false;

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

	void OnCollisionEnter(Collision other) {
		// If we hit a puma
		if(other.gameObject.tag == "Puma") {
			// Play the hit sound
			AudioSFX.PlaySound("CarHitNoise");
		}
	}

    public Vector3 GetHeadingDirection()
    {
        return HeadingDirection;
    }
}


