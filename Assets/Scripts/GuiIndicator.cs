using UnityEngine;
using System.Collections;

public class GuiIndicator : MonoBehaviour {

    private VehicleController vehicle;
    private GuiPoolManager GuiPooler;
    private GameObject puma;
    private Vector3 vehiclePumaVector;
    private Camera mainCam;
    private float borderSize = 0.0f;
    private Vector3 viewPort = Vector3.zero;

    // Use this for initialization
    void Start ()
    {
        puma = AssetManager.puma.gameObject;
        mainCam = GameObject.Find("CameraMain").GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update ()
    {

        vehiclePumaVector = (puma.transform.position - vehicle.transform.position).normalized; // Direction vector from Vehicle to puma
        float angle = Vector3.Angle(vehiclePumaVector, vehicle.GetHeadingDirection()); // get angle betwen vehicle heading direction and direction toward puma.

        if (Mathf.Abs(angle) <= 90) // If vehicle is heading towards Puma 
        {
            // Get viewport of the objective
            Vector3 newViewPort;
            newViewPort = mainCam.WorldToViewportPoint(vehicle.transform.position);

            // Clamp it to the screen
            newViewPort = new Vector3(Mathf.Clamp(newViewPort.x, 0.0f + borderSize, 1.0f - borderSize), Mathf.Clamp(newViewPort.y, 0.0f + borderSize, 1.0f - borderSize), Mathf.Clamp(newViewPort.z, 0.0f, newViewPort.z));
            if (newViewPort.z == 0.0f) // If behind, flip the viewport to bring it to front
            {
                newViewPort.x = (1.0f - newViewPort.x);
                Mathf.Clamp(newViewPort.x, 0.0f + borderSize, 1.0f - borderSize);
                newViewPort.y = 0.0f + borderSize;
            }

            viewPort = newViewPort;
            transform.position = mainCam.ViewportToScreenPoint(viewPort);
        }
        else
        {
            GuiPooler.AddToInactivePool(this);
            this.gameObject.SetActive(false);
        }
	}

    public void SetTarget(GameObject target, GuiPoolManager pooler)
    {
        vehicle = target.GetComponent<VehicleController>();
        GuiPooler = pooler;
        this.gameObject.SetActive(true);

        Debug.Log("GUI Alert Added");
    }
}
