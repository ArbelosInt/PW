using UnityEngine;
using System.Collections;

/// DeerController
/// Manages state and activities for deer population

public class DeerController : MonoBehaviour
{
	//===================================
	//===================================
	//		MODULE VARIABLES
	//===================================
	//===================================

	public class DeerClass {
		public GameObject gameObj;
		public string type;
		public string state;  // eating, walking, alert, running, caught
		public float heading = 0f;
		public float targetHeading = 0f;
		public float nextTurnTime = 0f;
		public float forwardRate = 0f;
		public float turnRate = 0f;
		public float baseY;
	}
	
	public DeerClass buck;
	public DeerClass doe;
	public DeerClass fawn;

	private bool newChaseFlag = false;

	private float buckDefaultForwardRate = 30f * 0.66f;
	private float buckDefaultTurnRate = 22.5f * 0.66f;
	private float doeDefaultForwardRate = 29f * 0.66f;
	private float doeDefaultTurnRate = 22.5f * 0.66f;
	private float fawnDefaultForwardRate = 28f * 0.66f;
	private float fawnDefaultTurnRate = 22.5f * 0.66f;

	// THE CAUGHT DEER
	
	private DeerClass caughtDeer;
	private float deerCaughtHeading;
	private float deerCaughtFinalHeading;
	private bool deerCaughtHeadingLeft = false;
	private float deerCaughtOffsetX;
	private float deerCaughtFinalOffsetX;
	private float deerCaughtOffsetZ;
	private float deerCaughtFinalOffsetZ;
	private float deerCaughtmainHeading;
	private float deerCaughtNextFrameTime;
	public int deerCaughtEfficiency = 0;
	
	//===================================
	//===================================
	//		INITIALIZATION
	//===================================
	//===================================

    void Start()
    {

	}
/*
	public void InitializeDeerPopulation()
	{
		buck.heading = buck.targetHeading = Random.Range(0f,360f);
		buck.gameObj.transform.rotation = Quaternion.Euler(0, buck.heading, 0);
		doe.heading = doe.targetHeading = Random.Range(0f,360f);		
		doe.gameObj.transform.rotation = Quaternion.Euler(0, doe.heading, 0);
		fawn.heading = fawn.targetHeading = Random.Range(0f,360f);	
		fawn.gameObj.transform.rotation = Quaternion.Euler(0, fawn.heading, 0);

		float randomDirection = Random.Range(0f,360f);	
		float deerDistance = Random.Range(80f,120f);  //Random.Range(100f,250f); //Random.Range(200f,350f);
		float positionVariance = 10f;
		float newX = pumaX + (Mathf.Sin(randomDirection*Mathf.PI/180) * deerDistance);
		float newZ = pumaZ + (Mathf.Cos(randomDirection*Mathf.PI/180) * deerDistance);

		float deerX;
		float deerZ;
		float deerY;
		
		deerX = newX + Random.Range(-positionVariance, positionVariance);
		deerZ = newZ + Random.Range(-positionVariance, positionVariance);
		deerY = buck.baseY + GetTerrainHeight(deerX, deerZ);
		buck.gameObj.transform.position = new Vector3(deerX, deerY, deerZ);

		deerX = newX + Random.Range(-positionVariance, positionVariance);
		deerZ = newZ + Random.Range(-positionVariance, positionVariance);
		deerY = doe.baseY + GetTerrainHeight(deerX, deerZ);
		doe.gameObj.transform.position = new Vector3(deerX, deerY, deerZ);

		deerX = newX + Random.Range(-positionVariance, positionVariance);
		deerZ = newZ + Random.Range(-positionVariance, positionVariance);
		deerY = fawn.baseY + GetTerrainHeight(deerX, deerZ);
		fawn.gameObj.transform.position = new Vector3(deerX, deerY, deerZ);

		//System.Console.WriteLine("PLACE DEER POSITIONS");	
		//System.Console.WriteLine("positive variance: " + positionVariance.ToString());	
		//System.Console.WriteLine("buck X: " + buck.gameObj.transform.position.x.ToString());	
	}

	//===================================
	//===================================
	//		MAIN DEER PROCESSING
	//===================================
	//===================================
	
	public void ProcessDeerPopulation()
	{
		//-----------------------
		//  PROCESS EACH DEER
		//
		//  based on current
		//  deer state
		//-----------------------

		case "gameStateStalking":
			buck.forwardRate = 0f;
			buck.turnRate = 0f;
			doe.forwardRate = 0f;
			doe.turnRate = 0f;
			fawn.forwardRate = 0f;
			fawn.turnRate = 0f;



		case "gameStateChasing":
			buck.forwardRate = buckDefaultForwardRate * Random.Range(0.9f, 1.1f);		// ??? should these really be changed every frame ???
			buck.turnRate = buckDefaultTurnRate * Random.Range(0.9f, 1.1f);
			doe.forwardRate = doeDefaultForwardRate * Random.Range(0.9f, 1.1f);
			doe.turnRate = doeDefaultTurnRate * Random.Range(0.9f, 1.1f);
			fawn.forwardRate = fawnDefaultForwardRate * Random.Range(0.9f, 1.1f);
			fawn.turnRate = fawnDefaultTurnRate * Random.Range(0.9f, 1.1f);
			//buck.forwardRate = 0f;
			//doe.forwardRate = 0f;
			//fawn.forwardRate = 0f;
			//buck.turnRate = 0f;
			//doe.turnRate = 0f;
			//fawn.turnRate = 0f;



		UpdateDeerHeading(buck);
		UpdateDeerHeading(doe);
		UpdateDeerHeading(fawn);
		UpdateDeerPosition(buck);
		UpdateDeerPosition(doe);
		UpdateDeerPosition(fawn);
			
		float pumaDeerDistance1 = Vector3.Distance(pumaObj.transform.position, buck.gameObj.transform.position);
		float pumaDeerDistance2 = Vector3.Distance(pumaObj.transform.position, doe.gameObj.transform.position);
		float pumaDeerDistance3 = Vector3.Distance(pumaObj.transform.position, fawn.gameObj.transform.position);		

		case "gameStateStalking":
			float lookingDistance = chaseTriggerDistance * 2f;
			float chasingDistance = chaseTriggerDistance;

			if (pumaDeerDistance1 < lookingDistance || pumaDeerDistance2 < lookingDistance || pumaDeerDistance3 < lookingDistance) {
				buckAnimator.SetBool("Looking", true);
				doeAnimator.SetBool("Looking", true);
				fawnAnimator.SetBool("Looking", true);
			}
			else {
				buckAnimator.SetBool("Looking", false);
				doeAnimator.SetBool("Looking", false);
				fawnAnimator.SetBool("Looking", false);
			}

			if (pumaDeerDistance1 < chasingDistance || pumaDeerDistance2 < chasingDistance || pumaDeerDistance3 < chasingDistance) {
				SetGameState("gameStateChasing");
				pumaAnimator.SetBool("Chasing", true);
				buckAnimator.SetBool("Running", true);
				doeAnimator.SetBool("Running", true);
				fawnAnimator.SetBool("Running", true);
				newChaseFlag = true;
				pumaHeadingOffset = 0;  // instantly disable diagonal movement (TEMP - really should swing camera around)
			}
			break;
	
	
		case "gameStateChasing":
			if (pumaDeerDistance1 < 2.5f || pumaDeerDistance2 < 2.5f || pumaDeerDistance3 < 2.5f) {
			
				// DEER IS CAUGHT !!!
			
				if (pumaDeerDistance1 < 2.5f) {
					buck.forwardRate = 0;
					buck.turnRate = 0f;
					buckAnimator.SetBool("Die", true);
					caughtDeer = buck;
					scoringSystem.DeerCaught(selectedPuma, "Buck");
				}
				else if (pumaDeerDistance2 < 2.5f) {
					doe.forwardRate = 0f;
					doe.turnRate = 0f;
					doeAnimator.SetBool("Die", true);
					caughtDeer = doe;
					scoringSystem.DeerCaught(selectedPuma, "Doe");
				}
				else {
					fawn.forwardRate = 0f;
					fawn.turnRate = 0f;
					fawnAnimator.SetBool("Die", true);
					caughtDeer = fawn;
					scoringSystem.DeerCaught(selectedPuma, "Fawn");
				}

				// prepare caughtDeer obj for slide
				deerCaughtHeading = caughtDeer.heading;
				if (mainHeading >= deerCaughtHeading) {
					deerCaughtHeadingLeft = (mainHeading - deerCaughtHeading <= 180) ? false : true;
				}
				else {
					deerCaughtHeadingLeft = (deerCaughtHeading - mainHeading <= 180) ? true : false;
				}
				if (deerCaughtHeadingLeft == true) {
					deerCaughtFinalHeading = mainHeading + 90;
				}
				else {
					//deerCaughtFinalHeading = mainHeading - 90;
					deerCaughtFinalHeading = mainHeading + 90;
				}
				if (deerCaughtFinalHeading < 0)
					deerCaughtFinalHeading += 360;
				if (deerCaughtFinalHeading >= 360)
					deerCaughtFinalHeading -= 360;
				deerCaughtmainHeading = mainHeading;
				deerCaughtOffsetX = caughtDeer.gameObj.transform.position.x - pumaX;
				deerCaughtOffsetZ = caughtDeer.gameObj.transform.position.z - pumaZ;
				deerCaughtFinalOffsetX = (Mathf.Sin(mainHeading*Mathf.PI/180) * deerCaughtFinalOffsetFactor0);
				deerCaughtFinalOffsetZ = (Mathf.Cos(mainHeading*Mathf.PI/180) * deerCaughtFinalOffsetFactor0);
				deerCaughtFinalOffsetX += (Mathf.Sin((mainHeading-90f)*Mathf.PI/180) * deerCaughtFinalOffsetFactor90);
				deerCaughtFinalOffsetZ += (Mathf.Cos((mainHeading-90f)*Mathf.PI/180) * deerCaughtFinalOffsetFactor90);
				deerCaughtNextFrameTime = 0;
				
				if (Time.time - stateStartTime < 5f)
					deerCaughtEfficiency = 3;
				else if (Time.time - stateStartTime < 10f)
					deerCaughtEfficiency = 2;
				else if (Time.time - stateStartTime < 16f)
					deerCaughtEfficiency = 1;
				else
					deerCaughtEfficiency = 0;
					
				pumaAnimator.SetBool("DeerKill", true);
				SetGameState("gameStateFeeding1");
			}
			break;		
		
		
		case "gameStateFeeding1":
			// deer and puma slide to a stop as camera swings around to front
			fadeTime = 1.3f;
			
			if (Time.time < stateStartTime + fadeTime) {
				// puma and deer slide to a stop
				float percentDone = 1f - ((Time.time - stateStartTime) / fadeTime);
				// during slide move deer to correct position 
				percentDone = ((Time.time - stateStartTime) / fadeTime);
				if ((deerCaughtFinalHeading > deerCaughtHeading) && (deerCaughtFinalHeading - deerCaughtHeading > 180))
					deerCaughtHeading += 360;
				else if ((deerCaughtHeading > deerCaughtFinalHeading) && (deerCaughtHeading - deerCaughtFinalHeading > 180))
					deerCaughtFinalHeading += 360;
				if (deerCaughtFinalHeading > deerCaughtHeading)
					caughtDeer.heading = deerCaughtHeading + ((deerCaughtFinalHeading - deerCaughtHeading) * percentDone);
				else
					caughtDeer.heading = deerCaughtHeading - ((deerCaughtHeading - deerCaughtFinalHeading) * percentDone);
				if (caughtDeer.heading < 0)
					caughtDeer.heading += 360;
				if (caughtDeer.heading >= 360)
					caughtDeer.heading -= 360;
				float deerX = pumaX + (deerCaughtOffsetX * (1f - percentDone)) + (deerCaughtFinalOffsetX * percentDone);
				float deerY = caughtDeer.gameObj.transform.position.y;
				float deerZ = pumaZ + (deerCaughtOffsetZ * (1f - percentDone)) + (deerCaughtFinalOffsetZ * percentDone);
				caughtDeer.gameObj.transform.rotation = Quaternion.Euler(0, caughtDeer.heading, 0);
				//System.Console.WriteLine("update heading: " + caughtDeer.heading.ToString());	
				caughtDeer.gameObj.transform.position = new Vector3(deerX, deerY, deerZ);
			}
			else {
				float deerX = pumaX + deerCaughtFinalOffsetX;
				float deerY = caughtDeer.gameObj.transform.position.y;
				float deerZ = pumaZ + deerCaughtFinalOffsetZ;
				caughtDeer.gameObj.transform.rotation = Quaternion.Euler(0, deerCaughtFinalHeading, 0);
				caughtDeer.gameObj.transform.position = new Vector3(deerX, deerY, deerZ);
			}
		
		
	}
	
	
	//===================================
	//===================================
	//		SUBROUTINES
	//===================================
	//===================================
	
	void UpdateDeerHeading(DeerClass deer)
	{
		if (deer.turnRate == 0 && deer.forwardRate == 0)
			return;
		
		if (Time.time > deer.nextTurnTime && deer.turnRate != 0f) {
			
			float randVal = Random.Range(0, 3);

			if (randVal < 1.0f)
				deer.targetHeading -= deer.turnRate * Random.Range(0.5f, 1f);
			else if (randVal < 2.0f)
				deer.targetHeading += 0;
			else
				deer.targetHeading += deer.turnRate * Random.Range(0.5f, 1f);				
					
			if (deer.targetHeading < 0)
				deer.targetHeading += 360;
			if (deer.targetHeading >= 360)
				deer.targetHeading -= 360;
			
			// limit to running away from puma

			float pumaDeerAngle = GetAngleFromOffset(pumaObj.transform.position.x, pumaObj.transform.position.z, deer.gameObj.transform.position.x, deer.gameObj.transform.position.z);

			if (pumaDeerAngle < 0)
				pumaDeerAngle += 360;
			if (pumaDeerAngle >= 360)
				pumaDeerAngle -= 360;
			
			if (pumaDeerAngle > deer.targetHeading) {
				if ((pumaDeerAngle - deer.targetHeading > 70f) && (pumaDeerAngle - deer.targetHeading <= 180f)) {
					deer.targetHeading = pumaDeerAngle - 70f;
				}
				else if ((pumaDeerAngle - deer.targetHeading >= 180f) && (pumaDeerAngle - deer.targetHeading <= 290f)) {
					deer.targetHeading = pumaDeerAngle - 290f;
					if (deer.targetHeading < 0)
						deer.targetHeading += 360;
				}
			}	
			else if (deer.targetHeading > pumaDeerAngle) {
				if ((deer.targetHeading - pumaDeerAngle > 70f) && (deer.targetHeading - pumaDeerAngle <= 180f)) {
					deer.targetHeading = pumaDeerAngle + 70f;
				}
				else if ((deer.targetHeading - pumaDeerAngle >= 180f) && (deer.targetHeading - pumaDeerAngle <= 290f)) {
					deer.targetHeading = pumaDeerAngle + 290f;
					if (pumaDeerAngle >= 360)
						pumaDeerAngle -= 360;
				}
			}
			
			deer.nextTurnTime = Time.time + Random.Range(0.2f, 0.4f);			

		}
		
		// slew the change in heading
		
		float slewRate = 100f * Time.deltaTime;
		
		if (newChaseFlag == true) {
			slewRate *= 3;
			if (Time.time - stateStartTime > 0.3f)	
				newChaseFlag = false;
		}
			
		if (deer.heading > deer.targetHeading) {
			if ((deer.heading - deer.targetHeading) < 180)
				deer.heading -= (deer.heading - deer.targetHeading > slewRate) ? slewRate : deer.heading - deer.targetHeading;
			else
				deer.heading += slewRate;
		}
		else if (deer.heading < deer.targetHeading) {
			if ((deer.targetHeading - deer.heading) < 180)
				deer.heading += (deer.targetHeading - deer.heading > slewRate) ? slewRate : deer.targetHeading - deer.heading;
			else
				deer.heading -= slewRate;
		}

		if (deer.heading < 0)
			deer.heading += 360;
		if (deer.heading >= 360)
			deer.heading -= 360;
					
		deer.gameObj.transform.rotation = Quaternion.Euler(0, deer.heading, 0);
		
		//System.Console.WriteLine("DEER HEADING: " + deer.heading.ToString());	
	}

	void UpdateDeerPosition(DeerClass deer)
	{
		//if (deer.type == "Buck")
			//offsetY = deer.gameObj.GetComponent<BuckRunScript>().GetOffsetY();
		//else if (deer.type == "Doe")	
			//offsetY = deer.gameObj.GetComponent<DoeRunScript>().GetOffsetY();
		//else if (deer.type == "Fawn")	
			//offsetY = deer.gameObj.GetComponent<FawnRunScript>().GetOffsetY();

		float forwardRate = deer.forwardRate;
		
		if (newChaseFlag) 
			forwardRate = deer.forwardRate * ((Time.time - stateStartTime) / 0.3f);
		
		float deerX = deer.gameObj.transform.position.x + (Mathf.Sin(deer.heading*Mathf.PI/180) * Time.deltaTime  * forwardRate);
		float deerZ = deer.gameObj.transform.position.z + (Mathf.Cos(deer.heading*Mathf.PI/180) * Time.deltaTime  * forwardRate);
		float deerY = deer.baseY + GetTerrainHeight(deerX, deerZ);

		deer.gameObj.transform.position = new Vector3(deerX, deerY, deerZ);
	}
*/
	
}