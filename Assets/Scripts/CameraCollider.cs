using UnityEngine;
using System.Collections;

/// CameraCollider
/// Tracks collisions for the main camera

public class CameraCollider : MonoBehaviour
{
	//===================================
	//===================================
	//		MODULE VARIABLES
	//===================================
	//===================================
	
	// COLLISION DETECTION
		
	private GameObject collisionObject;
	private bool collisionOverpassInProgress = false;
	
	// EXTERNAL MODULES
	
	//===================================
	//===================================
	//		INITIALIZATION
	//===================================
	//===================================

    void Start()
    {
		// connect to external modules
	}
	
 	//===================================
	//===================================
	//		UPDATES
	//===================================
	//===================================

    
	//===================================
	//===================================
	//		COLLISION LOGIC
	//===================================
	//===================================

	void OnCollisionEnter(Collision collisionInfo)
	{
		// OVERPASS

		if (collisionInfo.gameObject.tag == "Overpass") {
			collisionOverpassInProgress = true;
			collisionObject = collisionInfo.gameObject;
			Debug.Log("=====================================");
			Debug.Log("COLLISION:  " + gameObject.name + " - " + collisionInfo.collider.name);
			return;
		}
	}

	void OnCollisionStay(Collision collisionInfo)

	{

	}

	void OnCollisionExit(Collision collisionInfo)

	{
		if (collisionInfo.gameObject.tag == "Overpass") {
			collisionOverpassInProgress = false;
			Debug.Log("=====================================");
			Debug.Log("Collision End:  " + gameObject.name + " - " + collisionInfo.collider.name);
			return;
		}
	}
	
	//===========================================
	//===========================================
	//	PUBLIC FUNCTIONS
	//===========================================
	//===========================================

	public float GetCollisionOverpassSurfaceHeight()
	{
		if (collisionOverpassInProgress == false) {
			Debug.Log("ERROR:  CameraCollider.GetCollisionOverpassSurfaceHeight got called during no collision");
			return 0f;
		}
	
		return collisionObject.transform.position.y + 0.48f;
	}
	
	public bool CheckCollisionOverpassInProgress()
	{
		return (collisionOverpassInProgress == true);
	}
	
}







