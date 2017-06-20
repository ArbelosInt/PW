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
    public bool treeAhead = false;

    public bool roadDetected = false;
    public bool treeDetected = false;

    private Ray rayCheckLeft;
    private Ray rayCheckRight;
    private Ray rayCheckForward;

    private RaycastHit hitL;
    private RaycastHit hitR;
    private RaycastHit hitF;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, targetLeft.position - transform.position,Color.red);
        Debug.DrawRay(transform.position, targetRight.position - transform.position, Color.red);
        // If road detected, use raycasters to find the distance to shortest road to be avoided.
        if (roadDetected)
        {            
            // Check left side for road and get the distance to road 
            rayCheckLeft = new Ray(transform.position, targetLeft.position - transform.position);
            if (Physics.Raycast(rayCheckLeft, out hitL, layermask))  // Use layer mask to check only for roads and trees
            {
                if (hitL.collider.gameObject.tag == "Road")
                {
                    roadDistanceLeft = hitL.distance;
                }
            }
            else
            {
                roadDistanceLeft = 0.0f; // Reset
            }

            // Check the right side for road and get the distance to road
            rayCheckRight = new Ray(transform.position, targetRight.position - transform.position);
            if (Physics.Raycast(rayCheckRight, out hitR, layermask))
            {
                if (hitR.collider.gameObject.tag == "Road")
                {
                    roadDistanceRight = hitR.distance;
                }
            }
            else
            {
                roadDistanceRight = 0.0f; // Reset
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

    // If Road or Tree detected, turn the variables to true for further check
    void OnCollisionEnter(Collision other)
	{   
        if (other.gameObject.tag == "Road")
        {
            roadDetected = true;
            Debug.Log("Road detected by Deer");
        }
        else
        {
            treeDetected = true;
            Debug.Log("Tree detected by Deer");
        }
    }

    // On Trigger Exit, reset the variables
    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Road")
        {
            roadDetected = false;
            roadDistanceLeft = 0.0f;
            roadDistanceRight = 0.0f;
        }
        else
        {
            treeDetected = false;
            treeAhead = false;
        }
    }

    public void QuickRoadCheck()
    {
        RaycastHit hit;
        Ray rayCheck = new Ray(transform.position, -transform.up);
        if (Physics.Raycast(rayCheck, out hit, layermask))  // Use layer mask to check only for roads and trees
        {
            if (hit.collider.gameObject.tag == "Road")
            {
                roadDetected = true;
                Debug.Log("Quick Check : Deer spawn on road. ");
            }
            else
            {
                roadDetected = false;
            }
        }
    }
}
