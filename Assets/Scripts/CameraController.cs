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
	private string sideCameraStateL;
	private float  sideCameraStateOpenTimeL;
	private float  sideCameraTransStartTimeL;
	private float  sideCameraTransFadeTimeL;
	private string sideCameraStateR;
	private float  sideCameraStateOpenTimeR;
	private float  sideCameraTransStartTimeR;
	private float  sideCameraTransFadeTimeR;
	private bool   sideCameraWasReversedL = false;
	private bool   sideCameraWasReversedR = false;
	private float  sideCameraLastPumaRoadAngle;

	// audio ambience control
	private AudioSource forestAmbience;
	private AudioSource skyAmbience;
	private bool audioCrossfadeInProgress;
	private bool forestAmbienceIsPlaying;
	private bool audioFadeupFlag;
	private float audioFadeupStartTime;

	// external module
	private LevelManager levelManager;
	private TrafficManager trafficManager;
	private InputControls inputControls;
	private CameraCollider cameraCollider;

    public PumaController puma;
    private PumaTrafficDetector pumaTrafficDetector;

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
        puma = AssetManager.puma.GetComponent<PumaController>();
        pumaTrafficDetector = GameObject.Find("PumaTrafficDetector").GetComponent< PumaTrafficDetector>();

		currentCameraY = 0f;
		currentCameraRotX = 0f;
		currentCameraDistance = 0f;
		currentCameraRotOffsetY = 0f;
		
		sideCameraStateL = "sideCameraStateClosed";
		sideCameraStateR = "sideCameraStateClosed";

        forestAmbience = GameObject.Find("Ambient_Source_Forest").GetComponent<AudioSource>();
        skyAmbience = GameObject.Find("Ambient_Source_Sky").GetComponent<AudioSource>();
        forestAmbience.volume = 0;
        skyAmbience.volume = 0;
		audioCrossfadeInProgress = false;
		forestAmbienceIsPlaying = false;
		audioFadeupFlag = true;
		audioFadeupStartTime = Time.time;
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

		// start audio crossfade if needed
		if ((targetPositionLabel == "cameraPosGui" && forestAmbienceIsPlaying) ||
			(targetPositionLabel != "cameraPosGui" && !forestAmbienceIsPlaying)) {

			Debug.Log("===ENABLE FADE");

			audioCrossfadeInProgress = true;
		}
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

        float terrainY;

        if (puma.CheckCollisionOverpassInProgress())
        {
            terrainY = levelManager.GetTerrainHeight(cameraX, cameraZ, puma.hitPointHeight);
        }
        else if (cameraCollider.CheckCollisionOverpassInProgress())
        {
            terrainY = levelManager.GetTerrainHeight(cameraX, cameraZ, cameraCollider.hitPointHeight);
        }
        else
        {
            terrainY = levelManager.GetTerrainHeight(cameraX, cameraZ, 0f);
        }

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
		
		//-----------------------------------------------
		// crossfade ambient sound mix
		//-----------------------------------------------

		float skyAmbMinVol = 0.1f;
		float skyAmbMaxVol = 0.6f;
		float forestAmbMaxVol = 0.8f;
		float audioFadeupWaitTime = 0.2f;
		float audioFadeupTime = 2f;

		if (audioFadeupFlag && (Time.time >= audioFadeupStartTime + audioFadeupWaitTime)) {
			float effectiveAudioFadeupStartTime = audioFadeupStartTime + audioFadeupWaitTime;
			if (Time.time >= effectiveAudioFadeupStartTime + audioFadeupTime) {
			    skyAmbience.volume = skyAmbMaxVol;
				audioFadeupFlag = false;
			}
			else {
				float audioFadeupPercent = (Time.time - effectiveAudioFadeupStartTime) / audioFadeupTime;
			    skyAmbience.volume = skyAmbMaxVol * ((audioFadeupPercent*audioFadeupPercent)*0.8f + audioFadeupPercent*0.2f);
			}
		}

		else if (audioCrossfadeInProgress) {
			if (Time.time >= transStartTime + transFadeTime) {
				if (forestAmbienceIsPlaying) {
			        skyAmbience.volume = skyAmbMaxVol;
			        forestAmbience.volume = 0;
			        forestAmbienceIsPlaying = false;
				}
				else {
			        skyAmbience.volume = skyAmbMinVol;
			        forestAmbience.volume = forestAmbMaxVol;
			        forestAmbienceIsPlaying = true;
				}
				audioCrossfadeInProgress = false;
			}
			else {
				float audioFadePercent = (Time.time - transStartTime) / transFadeTime;

				if (forestAmbienceIsPlaying) {
			        skyAmbience.volume = (audioFadePercent*audioFadePercent)*0.25f + audioFadePercent*0.75f;
			        skyAmbience.volume = skyAmbMinVol + (skyAmbMaxVol-skyAmbMinVol) * skyAmbience.volume;
			        forestAmbience.volume = forestAmbMaxVol * (1 - (audioFadePercent*audioFadePercent));
				}
				else {
			        skyAmbience.volume = 1 - audioFadePercent;
			        skyAmbience.volume = skyAmbMinVol + (skyAmbMaxVol-skyAmbMinVol) * skyAmbience.volume;
			        float forestVolLog = 1 - ((1-audioFadePercent)*(1-audioFadePercent));
			        forestAmbience.volume = forestAmbMaxVol * ((forestVolLog + audioFadePercent) / 2);
				}
			}
		}

		//-----------------------------------------------
		// turn on side view cameras near roads
		//-----------------------------------------------

        if (levelManager.GetCurrentLevel() == 0 || levelManager.GetCurrentLevel() == 4) {
			cameraL.enabled = false;
			cameraR.enabled = false;
			return;
        }

		Vector3 nearestRoadCenterPos = trafficManager.FindClosestRoadCenterPos(new Vector3(pumaX, pumaY, pumaZ));
		float nearestRoadCenterPosDistance = Vector3.Distance(nearestRoadCenterPos, new Vector3(pumaX, pumaY, pumaZ));
		float pumaRoadAngle = levelManager.GetAngleFromOffset(pumaX, pumaZ, nearestRoadCenterPos.x, nearestRoadCenterPos.z);
		float sideViewDistance = (levelManager.gameState == "gameStateStalking") ? 15f : 30f;
        float transTime = (levelManager.gameState == "gameStateChasing") ? 0.2f : 0.4f;
		float differentialAngle = cameraRotY - pumaRoadAngle;
		bool sideViewVisibleL = true;
		bool sideViewVisibleR = true;
		bool sideViewIsReversedL = false;
		bool sideViewIsReversedR = false;

		// slew the pumaRoadAngle (except when it's a really big change)
		float pumaRoadAngleStepSize = 0.7f;
		if (pumaRoadAngle > sideCameraLastPumaRoadAngle) {
			float diff = pumaRoadAngle - sideCameraLastPumaRoadAngle;
			if (diff < 30f && diff > pumaRoadAngleStepSize) {
				pumaRoadAngle = sideCameraLastPumaRoadAngle + pumaRoadAngleStepSize;
			}
		}
		else if (pumaRoadAngle < sideCameraLastPumaRoadAngle) {
			float diff = sideCameraLastPumaRoadAngle - pumaRoadAngle;
			if (diff < 30f && diff > pumaRoadAngleStepSize) {
				pumaRoadAngle = sideCameraLastPumaRoadAngle - pumaRoadAngleStepSize;
			}
		}
		sideCameraLastPumaRoadAngle = pumaRoadAngle;

        float cameraYRotL = pumaRoadAngle - 90f;
        float cameraYRotR = pumaRoadAngle + 90f;

		// ensure angle is positive
		if (differentialAngle < 0) {
			differentialAngle += 360;
		}
		if (differentialAngle > 360) {
			differentialAngle -= 360;
		}

		// condition-based enabling
        if (levelManager.gameState != "gameStateStalking" && levelManager.gameState != "gameStateChasing") {
			sideViewVisibleL = false;
			sideViewVisibleR = false;
        }
		if (nearestRoadCenterPosDistance > trafficManager.GetMostRecentRoadWidth() + sideViewDistance) {
			sideViewVisibleL = false;
			sideViewVisibleR = false;
		}

		// angle-based calculations
		if (nearestRoadCenterPosDistance > trafficManager.GetMostRecentRoadWidth()) {
			// left side camera
			if (differentialAngle > 135f && differentialAngle < 315f) {
				sideViewVisibleL = false;
			}
			// right side camera
			if (differentialAngle > 45f && differentialAngle < 225f) {
				sideViewVisibleR = false;
			}
		}
		else {
			// left side camera
			if (differentialAngle > 100f && differentialAngle < 135f) {
				sideViewVisibleL = false;
			}
			else if (differentialAngle > 135f && differentialAngle < 280f) {
				cameraYRotL = pumaRoadAngle + 90f;
				sideViewIsReversedL = true;
			}
			else if (differentialAngle > 280f && differentialAngle < 315f) {
				sideViewVisibleL = false;
			}
			// right side camera
			if (differentialAngle > 45f && differentialAngle < 80f) {
				sideViewVisibleR = false;
			}
			else if (differentialAngle > 80f && differentialAngle < 225f) {
				cameraYRotR = pumaRoadAngle - 90f;
				sideViewIsReversedR = true;
			}
			else if (differentialAngle > 225f && differentialAngle < 260f) {
				sideViewVisibleR = false;
			}
		}

		// process left side camera

		if (sideCameraStateL == "sideCameraStateOpen") {
			if (sideViewVisibleL == false) {
				sideCameraStateL = "sideCameraStateClosing";
				sideCameraTransStartTimeL = Time.time;
				sideCameraTransFadeTimeL = transTime;
				if (sideCameraWasReversedL) {
					cameraYRotL = pumaRoadAngle + 90f;
				}
			}
			else {
				sideCameraWasReversedL = sideViewIsReversedL;	
			}
			cameraL.enabled = true;
			cameraL.transform.position = new Vector3(pumaX, adjustedCameraY, pumaZ);
			cameraL.transform.rotation = Quaternion.Euler(0f, cameraYRotL, cameraRotZ);
		}
		else if (sideCameraStateL == "sideCameraStateOpening") {
			cameraL.enabled = true;
			float percentHidden;
			if (sideViewVisibleL == false) {
				sideCameraStateL = "sideCameraStateClosing";
				float percentDone = (Time.time - sideCameraTransStartTimeL) / sideCameraTransFadeTimeL;
				sideCameraTransStartTimeL = Time.time - (transTime * (1f - percentDone));
				sideCameraTransFadeTimeL = transTime;
				percentHidden = (Time.time - sideCameraTransStartTimeL) / sideCameraTransFadeTimeL;
				if (sideCameraWasReversedL) {
					cameraYRotL = pumaRoadAngle + 90f;
				}
			}
			else if (Time.time > sideCameraTransStartTimeL + sideCameraTransFadeTimeL) {
				percentHidden = 0f;
				sideCameraStateL = "sideCameraStateOpen";
				sideCameraStateOpenTimeL = Time.time;
			}
			else {
				percentHidden = 1f - (Time.time - sideCameraTransStartTimeL) / sideCameraTransFadeTimeL;
			}
			float vertOffset = 0.034f * Screen.width / Screen.height;
			float cameraRectX = 0.034f;
			float cameraRectY = -vertOffset + 0.75f + 0.25f*percentHidden;
			float cameraRectW = 0.30f - 0.30f*percentHidden;
			float cameraRectH = 0.25f - 0.25f*percentHidden;		
			cameraL.rect = new Rect(cameraRectX, cameraRectY, cameraRectW, cameraRectH);
			cameraL.transform.position = new Vector3(pumaX, adjustedCameraY, pumaZ);
			cameraL.transform.rotation = Quaternion.Euler(0f, cameraYRotL, cameraRotZ);
		}
		else if (sideCameraStateL == "sideCameraStateClosing") {
			cameraL.enabled = true;
			float percentHidden;
			if (sideViewVisibleL == true) {
				sideCameraStateL = "sideCameraStateOpening";
				float percentDone = (Time.time - sideCameraTransStartTimeL) / sideCameraTransFadeTimeL;
				sideCameraTransStartTimeL = Time.time - (transTime * (1f - percentDone));
				sideCameraTransFadeTimeL = transTime;
				percentHidden = 1f - (Time.time - sideCameraTransStartTimeL) / sideCameraTransFadeTimeL;
				sideCameraWasReversedL = sideViewIsReversedL;
			}
			else if (Time.time > sideCameraTransStartTimeL + sideCameraTransFadeTimeL) {
				percentHidden = 1f;
				sideCameraStateL = "sideCameraStateClosed";
			}
			else {
				percentHidden = (Time.time - sideCameraTransStartTimeL) / sideCameraTransFadeTimeL;
				if (sideCameraWasReversedL) {
					cameraYRotL = pumaRoadAngle + 90f;
				}
			}
			float vertOffset = 0.034f * Screen.width / Screen.height;
			float cameraRectX = 0.034f;
			float cameraRectY = -vertOffset + 0.75f + 0.25f*percentHidden;
			float cameraRectW = 0.30f - 0.30f*percentHidden;
			float cameraRectH = 0.25f - 0.25f*percentHidden;		
			cameraL.rect = new Rect(cameraRectX, cameraRectY, cameraRectW, cameraRectH);
			cameraL.transform.position = new Vector3(pumaX, adjustedCameraY, pumaZ);
			cameraL.transform.rotation = Quaternion.Euler(0f, cameraYRotL, cameraRotZ);
		}
		else if (sideCameraStateL == "sideCameraStateClosed") {
			// side cameras off
			if (sideViewVisibleL == true) {
				sideCameraStateL = "sideCameraStateOpening";
				sideCameraTransStartTimeL = Time.time;
				sideCameraTransFadeTimeL = transTime;
				sideCameraWasReversedL = sideViewIsReversedL;
			}
			cameraL.enabled = false;
		}

		// process right side camera

		if (sideCameraStateR == "sideCameraStateOpen") {
			if (sideViewVisibleR == false) {
				sideCameraStateR = "sideCameraStateClosing";
				sideCameraTransStartTimeR = Time.time;
				sideCameraTransFadeTimeR = transTime;
				if (sideCameraWasReversedR) {
					cameraYRotR = pumaRoadAngle - 90f;
				}
			}
			else {
				sideCameraWasReversedR = sideViewIsReversedR;	
			}
			cameraR.enabled = true;
			cameraR.transform.position = new Vector3(pumaX, adjustedCameraY, pumaZ);
			cameraR.transform.rotation = Quaternion.Euler(0f, cameraYRotR, cameraRotZ);
		}
		else if (sideCameraStateR == "sideCameraStateOpening") {
			cameraR.enabled = true;
			float percentHidden;
			if (sideViewVisibleR == false) {
				sideCameraStateR = "sideCameraStateClosing";
				float percentDone = (Time.time - sideCameraTransStartTimeR) / sideCameraTransFadeTimeR;
				sideCameraTransStartTimeR = Time.time - (transTime * (1f - percentDone));
				sideCameraTransFadeTimeR = transTime;
				percentHidden = (Time.time - sideCameraTransStartTimeR) / sideCameraTransFadeTimeR;
				if (sideCameraWasReversedR) {
					cameraYRotR = pumaRoadAngle - 90f;
				}
			}
			else if (Time.time > sideCameraTransStartTimeR + sideCameraTransFadeTimeR) {
				percentHidden = 0f;
				sideCameraStateR = "sideCameraStateOpen";
				sideCameraStateOpenTimeR = Time.time;
			}
			else {
				percentHidden = 1f - (Time.time - sideCameraTransStartTimeR) / sideCameraTransFadeTimeR;
			}
			float vertOffset = 0.034f * Screen.width / Screen.height;
			float cameraRectX = -0.034f + 0.70f + 0.30f*percentHidden;
			float cameraRectY = -vertOffset + 0.75f + 0.25f*percentHidden;
			float cameraRectW = 0.30f - 0.30f*percentHidden;
			float cameraRectH = 0.25f - 0.25f*percentHidden;		
			cameraR.rect = new Rect(cameraRectX, cameraRectY, cameraRectW, cameraRectH);
			cameraR.transform.position = new Vector3(pumaX, adjustedCameraY, pumaZ);
			cameraR.transform.rotation = Quaternion.Euler(0f, cameraYRotR, cameraRotZ);
			sideCameraWasReversedR = sideViewIsReversedR;
		}
		else if (sideCameraStateR == "sideCameraStateClosing") {
			cameraR.enabled = true;
			float percentHidden;
			if (sideViewVisibleR == true) {
				sideCameraStateR = "sideCameraStateOpening";
				float percentDone = (Time.time - sideCameraTransStartTimeR) / sideCameraTransFadeTimeR;
				sideCameraTransStartTimeR = Time.time - (transTime * (1f - percentDone));
				sideCameraTransFadeTimeR = transTime;
				percentHidden = 1f - (Time.time - sideCameraTransStartTimeR) / sideCameraTransFadeTimeR;
			}
			else if (Time.time > sideCameraTransStartTimeR + sideCameraTransFadeTimeR) {
				percentHidden = 1f;
				sideCameraStateR = "sideCameraStateClosed";
			}
			else {
				percentHidden = (Time.time - sideCameraTransStartTimeR) / sideCameraTransFadeTimeR;
				if (sideCameraWasReversedR) {
					cameraYRotR = pumaRoadAngle - 90f;
				}
			}
			float vertOffset = 0.034f * Screen.width / Screen.height;
			float cameraRectX = -0.034f + 0.70f + 0.30f*percentHidden;
			float cameraRectY = -vertOffset + 0.75f + 0.25f*percentHidden;
			float cameraRectW = 0.30f - 0.30f*percentHidden;
			float cameraRectH = 0.25f - 0.25f*percentHidden;		
			cameraR.rect = new Rect(cameraRectX, cameraRectY, cameraRectW, cameraRectH);
			cameraR.transform.position = new Vector3(pumaX, adjustedCameraY, pumaZ);
			cameraR.transform.rotation = Quaternion.Euler(0f, cameraYRotR, cameraRotZ);
		}
		else if (sideCameraStateR == "sideCameraStateClosed") {
			// side cameras off
			if (sideViewVisibleR == true) {
				sideCameraStateR = "sideCameraStateOpening";
				sideCameraTransStartTimeR = Time.time;
				sideCameraTransFadeTimeR = transTime;
				sideCameraWasReversedR = sideViewIsReversedR;
			}
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


