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
	private bool carHitNoiseStarted = false;

    void Start()
    {
        previousPos = transform.position;
	}

    void Update()
    {
        velocity = (transform.position - previousPos).magnitude / Time.deltaTime;
        HeadingDirection = (transform.position - previousPos).normalized;
        previousPos = transform.position;   

		if(carHitNoiseStarted && !AudioSFX.IsPlaying()) 
		{
			// The car hit noise must be finished. Restart the Engine Noise
			StartEngineNoise();
		}
    }

	void OnCollisionEnter(Collision other) {
		// If we hit a puma
		if(other.gameObject.tag == "Puma") {
			// Stop all sounds
			AudioSFX.StopSound();

			// Play the hit sound
			AudioSFX.PlaySound("CarHitNoise", 0.9f);

			// Mark the car hit noise started flag to true
			carHitNoiseStarted = true;
		}
	}

    public Vector3 GetHeadingDirection()
    {
        return HeadingDirection;
    }

	public void StartEngineNoise() 
	{
		AudioSFX.PlayLoopingSound("EngineNoise", 0.7f);
	}
}


