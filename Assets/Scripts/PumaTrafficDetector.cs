// Programmer : Agnel Blaise
//
// The script controls a sphere collider that detects oncoming traffic and inturn calls the GUI Manager to show Traffic indicators.
//
using UnityEngine;
using System.Collections;

public class PumaTrafficDetector : MonoBehaviour {

    public GuiPoolManager GuiPooler;
    [Range(0.2f, 40.0f)]    public float maxRadius = 10.0f;
    private float minRadius = 0.2f;
    private float radiusIncrement = 2.0f;
    private SphereCollider collider;
    public bool isActivated = false;

    private GameObject puma;


    // Use this for initialization
    void Start()
    {
        puma = AssetManager.puma.gameObject;
        collider = this.GetComponent<SphereCollider>();
        collider.radius = minRadius;
    }



    // Update is called once per frame
    void Update()
    {
        transform.position = puma.transform.position;

        if (isActivated)
        {
            if (collider.radius <= maxRadius)
            {
                collider.radius += radiusIncrement;
            }
        }
    }

    void SetCurrentPuma()
    {
        puma = AssetManager.puma.gameObject;
    }

    // Call to activate the traffic detector
    public void Activate(bool status)
    {
        if (isActivated != status) // If new status is different
        {
            isActivated = status;
            SetCurrentPuma(); 

            if (!status) // If deactivated, reset collider and callback to remove GUI elements
            {
                collider.radius = minRadius;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (isActivated)
        {
            if (other.gameObject.tag == "Vehicle") // If collision with a vehicle
            {
                float heightDifference = Mathf.Abs(other.transform.position.y - puma.transform.position.y);
                if (heightDifference < 3.5) // If the vehicle in on the same plane as the puma
                {
                    // Call GUI Manager and transfer data to it
                    GuiPooler.AddVehicle(other.gameObject);
                    Debug.Log("Vehicle Detected : Asking GUI Pooler to add alert");
                }
            }
        }
    }

}
