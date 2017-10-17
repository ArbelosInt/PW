// Programmer : Agnel Blaise
// 
// The script is used to check for roads on both sides of the deer or tree ahead of deer 
// 

using UnityEngine;
using System.Collections;

public class DeerCollisionChecker : MonoBehaviour {

    // 0 if no road. Comparing the left and right values helps to identify the side to avoid
    public LayerMask layermask;
    public Transform targetLeft;
    public Transform targetRight;

    public float roadDistanceLeft = 0.0f;
    public float roadDistanceRight = 0.0f;
    public float roadDir = 0;
    public bool treeAhead = false;

    public int treeDir = 0;

    public float bridgeDistanceLeft = 0.0f;
    public float bridgeDistanceRight = 0.0f;

    public bool roadDetected = false;
    public bool treeDetected = false;
    public bool bridgeDetected = false;

    private Ray rayCheckLeft;
    private Ray rayCheckRight;
    private Ray rayCheckForward;

    private RaycastHit hitL;
    private RaycastHit hitR;
    private RaycastHit hitF;

    private bool collisionOverpassInProgress = false;
    private GameObject collisionObject;

    // Use this for initialization
    void Start()
    {

    }

    private void FixedUpdate()
    {
        CheckForOverpass();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, targetLeft.position - transform.position,Color.red);
        Debug.DrawRay(transform.position, targetRight.position - transform.position, Color.red);
        // If road detected, use raycasters to find the distance to shortest road to be avoided.
        if (roadDetected)
        {
            //Debug.Log("DeerCollisionChecker: Road detected in update func");

            // Check left side for road and get the distance to road 
            rayCheckLeft = new Ray(transform.position, targetLeft.position - transform.position);
            if (Physics.Raycast(rayCheckLeft, out hitL, layermask))  // Use layer mask to check only for roads and trees
            {
                //Debug.Log("DeerCollisionChecker: rayCheckLeft hit");            

                if (hitL.collider.gameObject.tag == "Road")
                {
                    roadDistanceLeft = hitL.distance;
                    //Debug.Log("DeerCollisionChecker: roadDistanceLeft = " + hitL.distance);            
                }

                if(hitL.collider.gameObject.tag == "Bridge")
                {
                    bridgeDistanceLeft = hitL.distance;
                    //Debug.Log("DeerCollisionChecker: bridgeDistanceLeft = " + hitL.distance);            
                }
            }
            else
            {
                roadDistanceLeft = 0.0f; // Reset
                bridgeDistanceLeft = 0.0f;
                //Debug.Log("DeerCollisionChecker: rayCheckLeft no hit");            
            }

            // Check the right side for road and get the distance to road
            rayCheckRight = new Ray(transform.position, targetRight.position - transform.position);
            if (Physics.Raycast(rayCheckRight, out hitR, layermask))
            {
                //Debug.Log("DeerCollisionChecker: rayCheckRight hit");            

                if (hitR.collider.gameObject.tag == "Road")
                {
                    roadDistanceRight = hitR.distance;
                    //Debug.Log("DeerCollisionChecker: roadDistanceRight = " + hitR.distance);            
                }

                if(hitR.collider.gameObject.tag == "Bridge")
                {
                    bridgeDistanceRight = hitR.distance;
                    //Debug.Log("DeerCollisionChecker: bridgeDistanceRight = " + hitR.distance);            
                }
            }
            else
            {
                roadDistanceRight = 0.0f; // Reset
                bridgeDistanceRight = 0.0f;
                //Debug.Log("DeerCollisionChecker: rayCheckRight no hit");            
            }
        }

        // If tree detected, use raycaster to check if tree if right in front
        if (treeDetected)
        {
            rayCheckForward = new Ray(transform.position, transform.forward * 3.0f);
            if (Physics.Raycast(rayCheckForward, out hitF, 50.0f, layermask))
            {
                treeAhead = true;
            }
            else
            {
                treeAhead = false;
            }
        }
    }

    public void CheckForOverpass()
    {
        Ray ray = new Ray(transform.parent.position - (transform.parent.forward) + transform.parent.up, -Vector3.up);

        Ray rayBehind = new Ray(transform.parent.position + (transform.parent.forward * 1.5f) + transform.parent.up, -Vector3.up);

        RaycastHit[] hits;
        RaycastHit[] backHits;

        hits = Physics.RaycastAll(ray, 2.0f, layermask);
        backHits = Physics.RaycastAll(rayBehind, 2.0f, layermask);

        collisionOverpassInProgress = false;

        if (hits != null)
        {

            foreach (RaycastHit hit in hits)
            {
                if (hit.transform.tag == "Overpass")
                {
                    collisionOverpassInProgress = true;
                    collisionObject = hit.transform.gameObject;
                    break;
                }
            }
        }

        if (backHits != null)
        {
            foreach (RaycastHit hit in backHits)
            {
                if (hit.transform.tag == "Overpass")
                {
                    collisionOverpassInProgress = true;
                    collisionObject = hit.transform.gameObject;
                    break;
                }
            }
        }
    }

    public float GetCollisionOverpassSurfaceHeight()
    {
        return collisionObject.transform.position.y + 0.48f;
    }

    public bool CheckCollisionOverpassInProgress()
    {
        return (collisionOverpassInProgress == true);
    }

    // If Road or Tree detected, turn the variables to true for further check
    void OnCollisionEnter(Collision other)
	{   
        if (other.gameObject.tag == "Road")
        {
            roadDetected = true;
            roadDir = LeftRightTest(other.contacts[0].point);
            Debug.Log("Road detected by Deer");
        }
        else if(other.gameObject.tag == "Bridge")
        {
            bridgeDetected = true;
            Debug.Log("Bridge detected by Deer");
        }
        else if (other.gameObject.tag == "Tree")
        {
            treeDetected = true;

            if (LeftRightTest(other.contacts[0].point) > 0)
            {
                treeDir = 1;
            }
            else
            {
                treeDir = -1;
            }
            Debug.Log(other.gameObject.tag + " detected by " + this.transform.parent.name);
        }
    }

    // On Trigger Exit, reset the variables
    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Road")
        {
            roadDetected = false;
            roadDir = 0;
        }
        else if(other.gameObject.tag == "Bridge")
        {
            bridgeDetected = false;
            bridgeDistanceLeft = 0.0f;
            bridgeDistanceRight = 0.0f;
        }
        else if (other.gameObject.tag == "Tree")
        {
            treeDetected = false;
            treeAhead = false;
        }
    }

    public float LeftRightTest(Vector3 target)
    {
        Transform parentTransform = transform.parent;

        Vector3 direction = target - parentTransform.position;

        Vector3 perpendicular = Vector3.Cross(parentTransform.forward, direction);

        float dirNum = Vector3.Dot(perpendicular, parentTransform.up);

        return dirNum;
    }

    public void QuickRoadCheck()
    {
        RaycastHit hit;
        Ray rayCheck = new Ray(transform.position, -transform.up);
        if (Physics.Raycast(rayCheck, out hit, layermask))  // Use layer mask to check only for roads and trees
        {
            if (hit.collider.gameObject.tag == "Road" || hit.collider.gameObject.tag == "Overpass")
            {
                roadDetected = true;
                Debug.Log("Quick Check : Deer spawn on road or overpass. ");
            }
            else
            {
                roadDetected = false;
            }
        }
    }
}
