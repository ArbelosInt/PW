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
    public LayerMask layerMask;
    public float hitPointHeight;

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

    private void FixedUpdate()
    {
        CheckForOverpass();
    }

    private void CheckForOverpass()
    {
        Ray ray = new Ray(transform.position, -Vector3.up);

        RaycastHit[] hits;

        hits = Physics.RaycastAll(ray, 3.0f, layerMask);

        collisionOverpassInProgress = false;

        if (hits != null)
        {

            foreach (RaycastHit hit in hits)
            {
                if (hit.transform.tag == "Overpass")
                {
                    collisionOverpassInProgress = true;
                    collisionObject = hit.transform.gameObject;
                    hitPointHeight = hit.point.y;
                    break;
                }
            }
        }
    }

    void OnCollisionEnter(Collision collisionInfo)
	{
		// OVERPASS

		//if (collisionInfo.gameObject.tag == "Overpass") {
		//	collisionOverpassInProgress = true;
		//	collisionObject = collisionInfo.gameObject;
		//	Debug.Log("=====================================");
		//	Debug.Log("COLLISION:  " + gameObject.name + " - " + collisionInfo.collider.name);
		//	return;
		//}
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







