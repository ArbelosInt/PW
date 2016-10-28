using UnityEngine;
using System.Collections;

/// CameraController
/// Manages camera position relative to puma

public class CameraController : MonoBehaviour
{
	//===================================
	//===================================
	//		MODULE VARIABLES
	//===================================
	//===================================
	
	public Camera cameraMain;
	public Camera cameraL;
	public Camera cameraR;
	
	// current camera position (controllable params)
	private float currentCameraY;
	private float currentCameraRotX;
	private float currentCameraDistance;
	private float currentCameraRotOffsetY;
	
	// previous position
	private float previousCameraY;
	private float previousCameraRotX;
	private float previousCameraDistance;
	private float previousCameraRotOffsetY;
	
	// target position
	private float targetCameraY;
	private float targetCameraRotX;
	private float targetCameraDistance;
	private float targetCameraRotOffsetY;
	
	// transition processing
	private float transStartTime;
	private float transFadeTime;
	private string transMainCurve;
	private string transRotXCurve;
	
	// side camera processing
	private string sideCameraState;
	private float sideCameraStateOpenTime;
	private float sideCameraTransStartTime;
	private float sideCameraTransFadeTime;
	
	// external module
	private LevelManager levelManager;
	private TrafficManager trafficManager;
	private InputControls inputControls;
	private CameraCollider cameraCollider;

    public PumaController puma;

	//===================================
	//===================================
	//		INITIALIZATION
	//===================================
	//===================================

    void Start()
    {
		// connect to external modules
		levelManager = GetComponent<LevelManager>();
		trafficManager = GetComponent<TrafficManager>();
		inputControls = GetComponent<InputControls>();
		cameraCollider = GameObject.Find("CameraMain").GetComponent<CameraCollider>();
        puma = GameObject.Find("Puma").GetComponent<PumaController>();

		currentCameraY = 0f;
		currentCameraRotX = 0f;
		currentCameraDistance = 0f;
		currentCameraRotOffsetY = 0f;
		
		sideCameraState = "sideCameraStateClosed";
	}
	
	//===================================
	//===================================
	//	  PUBLIC FUNCTIONS
	//===================================
	//===================================
	
	//-----------------------
	// SelectRelativePosition
	//
	// sets target for trans
	// to relative position 
	//-----------------------

	public void SelectTargetPosition(string targetPositionLabel, float targetRotOffsetY, float fadeTime, string mainCurve, string rotXCurve)
	{
		// remember previous position
		previousCameraY = currentCameraY;
		previousCameraRotX = currentCameraRotX;
		previousCameraDistance = currentCameraDistance;
		previousCameraRotOffsetY = currentCameraRotOffsetY;
		
		float terrainScaleFactor = 0.5f;
		float fieldOfViewDistanceFactor = 0.6f;		// default field of view is 60
		float fieldOfViewHeightFactor = 0.9f;		// default field of view is 60
				
		
		// select target position
		switch (targetPositionLabel) {
		
		case "cameraPosHigh":
			targetCameraY = 5.7f * terrainScaleFactor * fieldOfViewHeightFactor;
			targetCameraRotX = 12.8f;
			targetCameraDistance = 8.6f * terrainScaleFactor * fieldOfViewDistanceFactor;
			break;

		case "cameraPosMedium":
			targetCameraY = 4f * terrainScaleFactor * fieldOfViewHeightFactor;
			targetCameraRotX = 4f;
			targetCameraDistance = 7.5f * terrainScaleFactor * fieldOfViewDistanceFactor;
			break;

		case "cameraPosLow":
			targetCameraY = 3f * terrainScaleFactor * fieldOfViewHeightFactor;
			targetCameraRotX = -2f;
			targetCameraDistance = 7f * terrainScaleFactor * fieldOfViewDistanceFactor;
			break;

		case "cameraPosCloseup":
			targetCameraY = 2.75f * terrainScaleFactor * fieldOfViewHeightFactor;
			targetCameraRotX = 2.75f;
			targetCameraDistance = 6.5f * terrainScaleFactor * fieldOfViewDistanceFactor;
			break;

		case "cameraPosEating":
			targetCameraY = 9f * terrainScaleFactor * fieldOfViewHeightFactor;
			targetCameraRotX = 30f;
			targetCameraDistance = 9f * terrainScaleFactor * fieldOfViewDistanceFactor;
			break;

		case "cameraPosGui":
			targetCameraY = 90f * terrainScaleFactor * fieldOfViewHeightFactor;
			targetCameraRotX = 33f;
			targetCameraDistance = 48f * terrainScaleFactor * fieldOfViewDistanceFactor;
			break;
		}

		if (targetRotOffsetY != 1000000f)
			targetCameraRotOffsetY = targetRotOffsetY;

		// constrain previousCameraRotOffsetY to within 180 degrees of targetCameraRotOffsetY
		// so that camera always swings around the shortest path
		if (previousCameraRotOffsetY > targetCameraRotOffsetY + 180f) 
			previousCameraRotOffsetY -= 360f;
		if (previousCameraRotOffsetY < targetCameraRotOffsetY - 180f)
			previousCameraRotOffsetY += 360f;

		transStartTime = Time.time;
		transFadeTime = fadeTime;
		transMainCurve = mainCurve;
		transRotXCurve = rotXCurve;
	}

	//-----------------------
	// UpdateCameraPosition
	//
	// sets the actual camera
	// position in 3D world
	// once per frame
	//-----------------------
	
	public void UpdateCameraPosition(float pumaX, float pumaY, float pumaZ, float mainHeading)
	{
		float fadePercentComplete;
		float cameraRotXPercentDone;
		float backwardsTime = (transStartTime + transFadeTime) - (Time.time - transStartTime);	
		
		ProcessKeyboardInput();  // for manual camera adjustments - DEV ONLY

		// if trans has expired use target values
		
		if (Time.time >= transStartTime + transFadeTime) {
			currentCameraY = targetCameraY;
			currentCameraRotX = targetCameraRotX;
			currentCameraDistance = targetCameraDistance;
			currentCameraRotOffsetY = targetCameraRotOffsetY;
		}
		
		// else calculate current position based on transition

		else {
	
			switch (transMainCurve) {
			
			case "mainCurveLinear":
				fadePercentComplete = (Time.time - transStartTime) / transFadeTime;
				currentCameraY = previousCameraY + fadePercentComplete * (targetCameraY - previousCameraY);
				currentCameraDistance = previousCameraDistance + fadePercentComplete * (targetCameraDistance - previousCameraDistance);
				currentCameraRotOffsetY = previousCameraRotOffsetY + fadePercentComplete * (targetCameraRotOffsetY - previousCameraRotOffsetY);
				break;
			
			case "mainCurveSForward":
				// combines two logarithmic curves to create an S-curve
				if (Time.time < transStartTime + (transFadeTime * 0.5f)) {
					// 1st half
					fadePercentComplete = (Time.time - transStartTime) / (transFadeTime * 0.5f);
					fadePercentComplete = fadePercentComplete * fadePercentComplete;  // apply bulge
					currentCameraY = previousCameraY + fadePercentComplete * ((targetCameraY - previousCameraY) * 0.5f);
					currentCameraDistance = previousCameraDistance + fadePercentComplete * ((targetCameraDistance - previousCameraDistance) * 0.5f);
					currentCameraRotOffsetY = previousCameraRotOffsetY + fadePercentComplete * ((targetCameraRotOffsetY - previousCameraRotOffsetY) * 0.5f);
				}
				else {
					// 2nd half
					fadePercentComplete = ((Time.time - transStartTime) - (transFadeTime * 0.5f)) / (transFadeTime * 0.5f);				
					fadePercentComplete = fadePercentComplete + (fadePercentComplete - (fadePercentComplete * fadePercentComplete));  // apply bulge in opposite direction
					currentCameraY = previousCameraY + ((targetCameraY - previousCameraY) * 0.5f) + fadePercentComplete * ((targetCameraY - previousCameraY) * 0.5f);
					currentCameraDistance = previousCameraDistance + ((targetCameraDistance - previousCameraDistance) * 0.5f) + fadePercentComplete * ((targetCameraDistance - previousCameraDistance) * 0.5f);
					currentCameraRotOffsetY = previousCameraRotOffsetY + ((targetCameraRotOffsetY - previousCameraRotOffsetY) * 0.5f) + fadePercentComplete * ((targetCameraRotOffsetY - previousCameraRotOffsetY) * 0.5f);
				}
				break;
			
			case "mainCurveSBackward":
				// same as mainCurveSCurveForward except it runs backwards in time (reversing 'target' and 'previous') to get a different feel
				if (backwardsTime < transStartTime + (transFadeTime * 0.5f)) {
					// 1st half
					fadePercentComplete = (backwardsTime - transStartTime) / (transFadeTime * 0.5f);
					fadePercentComplete = fadePercentComplete * fadePercentComplete;  // apply bulge
					currentCameraY = targetCameraY + fadePercentComplete * ((previousCameraY - targetCameraY) * 0.5f);
					currentCameraDistance = targetCameraDistance + fadePercentComplete * ((previousCameraDistance - targetCameraDistance) * 0.5f);
					currentCameraRotOffsetY = targetCameraRotOffsetY + fadePercentComplete * ((previousCameraRotOffsetY - targetCameraRotOffsetY) * 0.5f);
				}
				else {
					// 2nd half
					fadePercentComplete = ((backwardsTime - transStartTime) - (transFadeTime * 0.5f)) / (transFadeTime * 0.5f);				
					fadePercentComplete = fadePercentComplete + (fadePercentComplete - (fadePercentComplete * fadePercentComplete)); // apply bulge in opposite direction
					currentCameraY = targetCameraY + ((previousCameraY - targetCameraY) * 0.5f) + fadePercentComplete * ((previousCameraY - targetCameraY) * 0.5f);
					currentCameraDistance = targetCameraDistance + ((previousCameraDistance - targetCameraDistance) * 0.5f) + fadePercentComplete * ((previousCameraDistance - targetCameraDistance) * 0.5f);
					currentCameraRotOffsetY = targetCameraRotOffsetY + ((previousCameraRotOffsetY - targetCameraRotOffsetY) * 0.5f) + fadePercentComplete * ((previousCameraRotOffsetY - targetCameraRotOffsetY) * 0.5f);
				}		
				break;
			
			default:
				Debug.Log("ERROR - CameraController.UpdateActualPosition() got bad main curve: " + transMainCurve);
				break;
			}

			switch (transRotXCurve) {
			
			case "curveRotXLinear":
				cameraRotXPercentDone = (Time.time - transStartTime) / transFadeTime;
				currentCameraRotX = previousCameraRotX + cameraRotXPercentDone * (targetCameraRotX - previousCameraRotX);
				break;
			
			case "curveRotXLogarithmic":
				cameraRotXPercentDone = (Time.time - transStartTime) / transFadeTime;
				cameraRotXPercentDone = cameraRotXPercentDone * cameraRotXPercentDone; // apply bulge
				currentCameraRotX = previousCameraRotX + cameraRotXPercentDone * (targetCameraRotX - previousCameraRotX);
				break;
			
			case "curveRotXLinearSecondHalf":
				if (Time.time < transStartTime + (transFadeTime * 0.5f)) {
					// 1st half
					currentCameraRotX = previousCameraRotX; // no change
				}
				else {
					// 2nd half
					cameraRotXPercentDone = ((Time.time - transStartTime) - (transFadeTime * 0.5f)) / (transFadeTime * 0.5f);
					currentCameraRotX = previousCameraRotX + cameraRotXPercentDone * (targetCameraRotX - previousCameraRotX);
				}
				break;
			
			case "curveRotXLogarithmicSecondHalf":
				if (Time.time < transStartTime + (transFadeTime * 0.5f)) {
					// 1st half
					currentCameraRotX = previousCameraRotX; // no change
				}
				else {
					// 2nd half
					cameraRotXPercentDone = ((Time.time - transStartTime) - (transFadeTime * 0.5f)) / (transFadeTime * 0.5f);
					cameraRotXPercentDone = cameraRotXPercentDone * cameraRotXPercentDone; // apply bulge
					currentCameraRotX = previousCameraRotX + cameraRotXPercentDone * (targetCameraRotX - previousCameraRotX);
				}
				break;
			
			case "curveRotXLinearBackwardsSecondHalf":
				// same as curveRotXLinearSecondHalf except it runs backwards in time (reversing 'target' and 'previous') to get a different feel
				if (backwardsTime < transStartTime + (transFadeTime * 0.5f)) {
					// 1st half
					currentCameraRotX = targetCameraRotX; // no change
				}
				else {
					// 2nd half
					cameraRotXPercentDone = ((backwardsTime - transStartTime) - (transFadeTime * 0.5f)) / (transFadeTime * 0.5f);
					currentCameraRotX = targetCameraRotX + cameraRotXPercentDone * (previousCameraRotX - targetCameraRotX);
				}		
				break;

			case "curveRotXLogarithmicBackwardsSecondHalf":
				// same as curveRotXLogarithmicSecondHalf except it runs backwards in time (reversing 'target' and 'previous') to get a different feel
				if (backwardsTime < transStartTime + (transFadeTime * 0.5f)) {
					// 1st half
					currentCameraRotX = targetCameraRotX; // no change
				}
				else {
					// 2nd half
					cameraRotXPercentDone = ((backwardsTime - transStartTime) - (transFadeTime * 0.5f)) / (transFadeTime * 0.5f);
					cameraRotXPercentDone = cameraRotXPercentDone * cameraRotXPercentDone; // apply bulge
					currentCameraRotX = targetCameraRotX + cameraRotXPercentDone * (previousCameraRotX - targetCameraRotX);
				}		
				break;

			default:
				Debug.Log("ERROR - CameraController.UpdateActualPosition() got bad rotX curve: " + transRotXCurve);
				break;
			}
		}
			
		//-----------------------------------------------
		// set actual position
		//-----------------------------------------------

		float cameraRotX = currentCameraRotX;
		float cameraRotY = mainHeading + currentCameraRotOffsetY;
		float cameraRotZ = 0f;
		
		float cameraX = pumaX - (Mathf.Sin(cameraRotY*Mathf.PI/180) * currentCameraDistance);
		float cameraY = currentCameraY;
		float cameraZ = pumaZ - (Mathf.Cos(cameraRotY*Mathf.PI/180) * currentCameraDistance);	
	
		//-----------------------------------------------
		// calculate camera adjustments based on terrain
		//-----------------------------------------------

		// initially camera goes to 'cameraY' units above terrain
		// that screws up the distance to the puma in extreme slope terrain
		// the camera is then moved to the 'correct' distance along the vector from puma to camera
		// that screws up the viewing angle, putting the puma too high or low in field of view
		// lastly we calculate an angle offset for new position, and factor in some fudge to account for viewing angle problem

		float terrainY = levelManager.GetTerrainHeight(cameraX, cameraZ, (puma.CheckCollisionOverpassInProgress() == true) ? puma.GetCollisionOverpassSurfaceHeight() : 0f);

		float adjustedCameraX = cameraX;
		float adjustedCameraY = cameraY + terrainY;
		float adjustedCameraZ = cameraZ;

		float idealVisualDistance = Vector3.Distance(new Vector3(0, 0, 0), new Vector3(currentCameraDistance, cameraY, 0));
		float currentVisualAngle = levelManager.GetAngleFromOffset(0, pumaY, currentCameraDistance, adjustedCameraY);
		float adjustedCameraDistance = Mathf.Sin(currentVisualAngle*Mathf.PI/180) * idealVisualDistance;

		adjustedCameraY = pumaY + Mathf.Cos(currentVisualAngle*Mathf.PI/180) * idealVisualDistance;
		adjustedCameraX = pumaX - (Mathf.Sin(cameraRotY*Mathf.PI/180) * adjustedCameraDistance);
		adjustedCameraZ = pumaZ - (Mathf.Cos(cameraRotY*Mathf.PI/180) * adjustedCameraDistance);	

		float cameraRotXAdjustment = -1f * (levelManager.GetAngleFromOffset(0, pumaY, currentCameraDistance, terrainY) - 90f);
		cameraRotXAdjustment *= (cameraRotXAdjustment > 0) ? 0.65f : 0.8f;
		float adjustedCameraRotX = cameraRotX + cameraRotXAdjustment;

		//-----------------------------------------------
		// write out values to camera object
		//-----------------------------------------------

		cameraMain.transform.position = new Vector3(adjustedCameraX, adjustedCameraY, adjustedCameraZ);
		cameraMain.transform.rotation = Quaternion.Euler(adjustedCameraRotX, cameraRotY, cameraRotZ);
		


		// turn on side view cameras near roads

		Vector3 nearestRoadPos = trafficManager.FindClosestNode(new Vector3(pumaX, 0, pumaZ));
		float nearestRoadDistance = Vector3.Distance(nearestRoadPos, new Vector3(pumaX, 0, pumaZ));
		float pumaRoadAngle = levelManager.GetAngleFromOffset(pumaX, pumaZ, nearestRoadPos.x, nearestRoadPos.z);

		bool sideViewVisible = true;
//		if (levelManager.GetCurrentLevel() == 0 || levelManager.GetCurrentLevel() == 3 || levelManager.GetCurrentLevel() == 4)
		if (levelManager.GetCurrentLevel() == 0 || levelManager.GetCurrentLevel() == 4)
			sideViewVisible = false;
		else if (levelManager.gameState != "gameStateStalking" && levelManager.gameState != "gameStateChasing")
			sideViewVisible = false;
		else if (nearestRoadDistance > 30f)
			sideViewVisible = false;
		else if (pumaRoadAngle > cameraRotY && pumaRoadAngle - cameraRotY > 90f && pumaRoadAngle - cameraRotY < 270f)
			sideViewVisible = false;
		else if (cameraRotY > pumaRoadAngle && cameraRotY - pumaRoadAngle > 90f && cameraRotY - pumaRoadAngle < 270f)
			sideViewVisible = false;
		//else if (levelManager.GetCurrentLevel() == 2 && trafficManager.FindClosestRoad(new Vector3(pumaX, 0, pumaZ)) == 1)
			//sideViewVisible = false;
				
				
		float transTime = (levelManager.gameState == "gameStateChasing") ? 0.25f : 0.5f;

		if (sideCameraState == "sideCameraStateOpen") {
			if ((Time.time > sideCameraStateOpenTime + 0.5f) && sideViewVisible == false) {
				sideCameraState = "sideCameraStateClosing";
				sideCameraTransStartTime = Time.time;
				sideCameraTransFadeTime = transTime;
			}
			cameraL.enabled = true;
			cameraR.enabled = true;
			cameraL.transform.position = new Vector3(pumaX, adjustedCameraY, pumaZ);
			cameraL.transform.rotation = Quaternion.Euler(adjustedCameraRotX, cameraRotY - 70f, cameraRotZ);
			cameraR.transform.position = new Vector3(pumaX, adjustedCameraY, pumaZ);
			cameraR.transform.rotation = Quaternion.Euler(adjustedCameraRotX, cameraRotY + 70f, cameraRotZ);
		}
		else if (sideCameraState == "sideCameraStateOpening") {
			cameraL.enabled = true;
			cameraR.enabled = true;
			float percentHidden;
			if (sideViewVisible == false) {
				sideCameraState = "sideCameraStateClosing";
				float percentDone = (Time.time - sideCameraTransStartTime) / sideCameraTransFadeTime;
				sideCameraTransStartTime = Time.time - (transTime * (1f - percentDone));
				sideCameraTransFadeTime = transTime;
				percentHidden = (Time.time - sideCameraTransStartTime) / sideCameraTransFadeTime;
			}
			else if (Time.time > sideCameraTransStartTime + sideCameraTransFadeTime) {
				percentHidden = 0f;
				sideCameraState = "sideCameraStateOpen";
				sideCameraStateOpenTime = Time.time;
			}
			else {
				percentHidden = 1f - (Time.time - sideCameraTransStartTime) / sideCameraTransFadeTime;
			}
			float cameraRectX = 0f;
			float cameraRectY = 0.65f + 0.35f*percentHidden;
			float cameraRectW = 0.35f - 0.35f*percentHidden;
			float cameraRectH = 0.35f - 0.35f*percentHidden;		
			cameraL.rect = new Rect(cameraRectX, cameraRectY, cameraRectW, cameraRectH);
			cameraRectX = 0.65f + 0.35f*percentHidden;
			cameraR.rect = new Rect(cameraRectX, cameraRectY, cameraRectW, cameraRectH);
			cameraL.transform.position = new Vector3(pumaX, adjustedCameraY, pumaZ);
			cameraL.transform.rotation = Quaternion.Euler(adjustedCameraRotX, cameraRotY - 70f, cameraRotZ);
			cameraR.transform.position = new Vector3(pumaX, adjustedCameraY, pumaZ);
			cameraR.transform.rotation = Quaternion.Euler(adjustedCameraRotX, cameraRotY + 70f, cameraRotZ);
		}
		else if (sideCameraState == "sideCameraStateClosing") {
			cameraL.enabled = true;
			cameraR.enabled = true;
			float percentHidden;
			if (sideViewVisible == true) {
				sideCameraState = "sideCameraStateOpening";
				float percentDone = (Time.time - sideCameraTransStartTime) / sideCameraTransFadeTime;
				sideCameraTransStartTime = Time.time - (transTime * (1f - percentDone));
				sideCameraTransFadeTime = transTime;
				percentHidden = 1f - (Time.time - sideCameraTransStartTime) / sideCameraTransFadeTime;
			}
			else if (Time.time > sideCameraTransStartTime + sideCameraTransFadeTime) {
				percentHidden = 1f;
				sideCameraState = "sideCameraStateClosed";
			}
			else {
				percentHidden = (Time.time - sideCameraTransStartTime) / sideCameraTransFadeTime;
			}
			float cameraRectX = 0f;
			float cameraRectY = 0.65f + 0.35f*percentHidden;
			float cameraRectW = 0.35f - 0.35f*percentHidden;
			float cameraRectH = 0.35f - 0.35f*percentHidden;		
			cameraL.rect = new Rect(cameraRectX, cameraRectY, cameraRectW, cameraRectH);
			cameraRectX = 0.65f + 0.35f*percentHidden;
			cameraR.rect = new Rect(cameraRectX, cameraRectY, cameraRectW, cameraRectH);
			cameraL.transform.position = new Vector3(pumaX, adjustedCameraY, pumaZ);
			cameraL.transform.rotation = Quaternion.Euler(adjustedCameraRotX, cameraRotY - 70f, cameraRotZ);
			cameraR.transform.position = new Vector3(pumaX, adjustedCameraY, pumaZ);
			cameraR.transform.rotation = Quaternion.Euler(adjustedCameraRotX, cameraRotY + 70f, cameraRotZ);
		}
		else if (sideCameraState == "sideCameraStateClosed") {
			// side cameras off
			if (sideViewVisible == true) {
				sideCameraState = "sideCameraStateOpening";
				sideCameraTransStartTime = Time.time;
				sideCameraTransFadeTime = transTime;
			}
			cameraL.enabled = false;
			cameraR.enabled = false;
		}
	}
	
	//-----------------------
	// GetCurrentRotOffsetY
	//
	// returns current val
	//-----------------------

	public float GetCurrentRotOffsetY()
	{
		return currentCameraRotOffsetY;
	}

	//-----------------------
	// SetCurrentRotOffsetY
	//
	// sets current val
	//-----------------------

	public void SetCurrentRotOffsetY(float newVal)
	{
		currentCameraRotOffsetY = newVal;
	}

	//-----------------------
	// ProcessKeyboardInput
	//
	// DEV ONLY
	// manual camera control
	//-----------------------

	void ProcessKeyboardInput()
	{
		float inputVert = 0f;
		float inputHorz = 0f;
		
		if (Input.GetKey(KeyCode.UpArrow))
			inputVert = 1f;
		else if (Input.GetKey(KeyCode.DownArrow))
			inputVert = -1f;
	
		if (Input.GetKey(KeyCode.LeftArrow))
			inputHorz = -1f;
		else if (Input.GetKey(KeyCode.RightArrow))
			inputHorz = 1f;
	
		if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.LeftControl)) {
			// dev only: camera distance and angle
			targetCameraDistance -= inputVert * Time.deltaTime * 4 * levelManager.speedOverdrive;
			targetCameraRotOffsetY += inputHorz * Time.deltaTime * 60 * levelManager.speedOverdrive;
			inputControls.ResetControls();
		}
		
		else if (Input.GetKey(KeyCode.LeftShift)) {
			// dev only: camera height
			targetCameraY += inputVert * Time.deltaTime  * 3 * levelManager.speedOverdrive;
			inputControls.ResetControls();
		}
		
		else if (Input.GetKey(KeyCode.LeftControl)) {
			// dev only: camera pitch
			targetCameraRotX += inputVert * Time.deltaTime  * 25 * levelManager.speedOverdrive;
			inputControls.ResetControls();
		}
	}
}


