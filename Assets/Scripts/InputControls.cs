using UnityEngine;
using System.Collections;

/// MovementControls
/// Handles user input and the movement of the puma

public class InputControls : MonoBehaviour
{
	//===================================
	//===================================
	//		MODULE VARIABLES
	//===================================
	//===================================
	
	private bool inputRampUpFlag = false;
	private bool inputRampDownFlag = false;
	private float inputRampStartTime;
	private float inputRampTotalTime;
	private float inputRampVertStartLevel;
	private float inputRampHorzStartLevel;
	
	private bool inputVertExternalSetFlag = false;
	private float inputVertExternal;
	
	private bool navInProgress = false;
	private bool navBasedOnZero;
	private bool chasingBeganWithSideStalk = false;
	private bool movementEnabled = false;
	
	private bool previousKeyStateLeftButton = false;

	// possible states for each direction of movement
	enum NavState {Off, Inc, Full, Dec};
	
	// four main directions
	private NavState navStateLeft;
	private NavState navStateRight;
	private NavState navStateForward;
	private NavState navStateBack;
	private float navValLeft;
	private float navValRight;
	private float navValForward;
	private float navValBack;

	// four diagonal directions (FOR SMOOTHING; NOT YET IMPLEMENTED)
	private NavState navStateForwardLeft;
	private NavState navStateForwardRight;
	private NavState navStateBackLeft;
	private NavState navStateBackRight;
	private float navValForwardLeft;
	private float navValForwardRight;
	private float navValBackLeft;
	private float navValBackRight;

	// used in resolving interactions between forward and back
	private bool forwardKey;
	private bool backKey;

	// main variables for current movement status
	private float inputVert;
	private float inputHorz;

	// used for passing back data from subroutine
	private NavState newNavState;
	private float newNavVal;
	
	// onscreen locations for control boxes
	private Rect rectLeftButton;
	private Rect rectMiddleButton;
	private Rect rectRightButton;
	private Rect rectForward;
	private Rect rectBack;
	private Rect rectDiagLeft;
	private Rect rectDiagRight;
	private Rect rectTurnLeft;
	private Rect rectTurnRight;
	
	// external modules
	private LevelManager levelManager;
	private GuiManager guiManager;

	//===================================
	//===================================
	//		INITIALIZATION
	//===================================
	//===================================

    void Start()
    {
		// connect to external modules
		levelManager = GetComponent<LevelManager>();
		guiManager = GetComponent<GuiManager>();
		
		rectLeftButton = new Rect (0f, 0f, 0f, 0f);
		rectMiddleButton = new Rect (0f, 0f, 0f, 0f);
		rectRightButton = new Rect (0f, 0f, 0f, 0f);
		rectForward = new Rect (0f, 0f, 0f, 0f);
		rectBack = new Rect (0f, 0f, 0f, 0f);
		rectDiagLeft = new Rect (0f, 0f, 0f, 0f);
		rectDiagRight = new Rect (0f, 0f, 0f, 0f);
		rectTurnLeft = new Rect (0f, 0f, 0f, 0f);
		rectTurnRight = new Rect (0f, 0f, 0f, 0f);		

		ResetControls();	
	}

	public void ResetControls()
	{
		navStateForward = NavState.Off;
		navStateBack = NavState.Off;
		navStateLeft = NavState.Off;
		navStateRight = NavState.Off;
		navValForward = 0;
		navValBack = 0;
		navValLeft = 0;
		navValRight = 0;

		navStateForwardLeft = NavState.Off;
		navStateForwardRight = NavState.Off;
		navStateBackLeft = NavState.Off;
		navStateBackRight = NavState.Off;
		navValForwardLeft = 0;
		navValForwardRight = 0;
		navValBackLeft = 0;
		navValBackRight = 0;
		
		forwardKey = false;
		backKey = false;
		
		inputVert = 0f;
		inputHorz = 0f;
	}


	public void EnableMovement() {
		movementEnabled = true;
	}


	//===================================
	//===================================
	//		CONTROL PROCESSING
	//===================================
	//===================================

	//--------------------------------------------
	// ProcessControls()
	// 
	// This is the main entry point for input
	// processing, called at the beginning of
	// the main Update() function in LevelManager
	//--------------------------------------------

	public void ProcessControls(string gameState)
	{
		// initialize key states to 'off'
		bool keyStateLeftButton = false;
		bool keyStateMiddleButton = false;
		bool keyStateRightButton = false;
		bool previousKeyStateRightButton = false;
		bool keyStateForward = false;
		bool keyStateBack = false;
		bool keyStateDiagLeft = false;
		bool keyStateDiagRight = false;
		bool keyStateTurnLeft = false;
		bool keyStateTurnRight = false;

		float oldInputVert = inputVert;
		float oldInputHorz = inputHorz;

		bool DISPLAY_MOUSE_POS = false;

		if (DISPLAY_MOUSE_POS == true) {
			levelManager.displayVar1 = "";
			levelManager.displayVar2 = "";
			levelManager.displayVar3 = "";
			levelManager.displayVar4 = "";
			levelManager.displayVar5 = "";
			levelManager.displayVar6 = "";
		}
			
		if (inputVertExternalSetFlag == true) {
			// external setting of inputVert -- allows level mgr to make puma walk after feeding
			inputVert = inputVertExternal;
			inputHorz = 0f;
			if (inputVertExternal == 0f) {
				inputVertExternalSetFlag = false;
			}
		}

        else if (Input.touchCount > 0 || Input.GetMouseButton(0)) {

			int count = Input.touchCount > 0 ? Input.touchCount : 1;

			for (int i = 0; i < count; i++) {
				float mouseX;
				float mouseY;

				if (Input.touchCount > 0) {
					// using touch
					Touch touch = Input.GetTouch(i);
					if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled) {
						mouseX = touch.position.x;
						mouseY = Screen.height - touch.position.y;
						if (DISPLAY_MOUSE_POS == true) {
							if (i == 0)	{
								levelManager.displayVar1 = "touch1 xPos: " + mouseX;
								levelManager.displayVar2 = "touch1 yPos: " + mouseY;
							}
							else if (i == 1) {
								levelManager.displayVar3 = "touch2 xPos: " + mouseX;
								levelManager.displayVar4 = "touch2 yPos: " + mouseY;
							}
						}
					}
					else {
						continue;
					}
				}
				else {
					// using mouse
					mouseX = Input.mousePosition.x;
					mouseY = Screen.height - Input.mousePosition.y;	
					if (DISPLAY_MOUSE_POS == true) {
						levelManager.displayVar1 = "mouse xPos: " + mouseX;
						levelManager.displayVar2 = "mouse yPos: " + mouseY;
					}
				}

				previousKeyStateRightButton = keyStateRightButton;

				// check for pressed mouse within any of the onscreen rects		
				if (mouseX >= rectLeftButton.xMin && mouseX <= rectLeftButton.xMax && mouseY >= rectLeftButton.yMin && mouseY <= rectLeftButton.yMax) {
					keyStateLeftButton = true;
				}
				if (mouseX >= rectMiddleButton.xMin && mouseX <= rectMiddleButton.xMax && mouseY >= rectMiddleButton.yMin && mouseY <= rectMiddleButton.yMax) {
					keyStateMiddleButton = true;
				}
				if (mouseX >= rectRightButton.xMin && mouseX <= rectRightButton.xMax && mouseY >= rectRightButton.yMin && mouseY <= rectRightButton.yMax) {
					keyStateRightButton = movementEnabled ? true : false;
				}
				if (mouseX >= rectForward.xMin && mouseX <= rectForward.xMax && mouseY >= rectForward.yMin && mouseY <= rectForward.yMax) {
					keyStateForward = true;
				}
				if (mouseX >= rectBack.x && mouseX <= rectBack.x+rectBack.width && mouseY >= rectBack.y && mouseY <= rectBack.y+rectBack.height) {
					keyStateBack = true;
				}
				if (mouseX >= rectDiagLeft.x && mouseX <= rectDiagLeft.x+rectDiagLeft.width && mouseY >= rectDiagLeft.y && mouseY <= rectDiagLeft.y+rectDiagLeft.height) {
					keyStateDiagLeft = true;
				}
				if (mouseX >= rectDiagRight.x && mouseX <= rectDiagRight.x+rectDiagRight.width && mouseY >= rectDiagRight.y && mouseY <= rectDiagRight.y+rectDiagRight.height) {
					keyStateDiagRight = true;
				}
				if (mouseX >= rectTurnLeft.x && mouseX <= rectTurnLeft.x+rectTurnLeft.width && mouseY >= rectTurnLeft.y && mouseY <= rectTurnLeft.y+rectTurnLeft.height) {
					keyStateTurnLeft = true;
				}
				if (mouseX >= rectTurnRight.x && mouseX <= rectTurnRight.x+rectTurnRight.width && mouseY >= rectTurnRight.y && mouseY <= rectTurnRight.y+rectTurnRight.height) {
					keyStateTurnRight = true;
				}

				//if ((keyStateRightButton == true && previousKeyStateRightButton == false) || (navInProgress == true && (Input.touchCount == 0 || (keyStateLeftButton == false && keyStateMiddleButton == false)))) {
				if ((keyStateRightButton == true && previousKeyStateRightButton == false) || (navInProgress == true && mouseX > Screen.width / 2)) {

					// MOTION CONTROLS 
				
					float rightButtonUpperAreaHeight = (rectRightButton.yMax - rectRightButton.yMin) * 0.80f;

					if (navInProgress == false) {
						navInProgress = true;
						levelManager.pumaAnimator.SetBool("Movement Engaged", true);
						navBasedOnZero = (mouseY > rectRightButton.yMin + rightButtonUpperAreaHeight) ? true : false;
					}
					else if (navBasedOnZero == false && (mouseY > rectRightButton.yMin + rightButtonUpperAreaHeight)) {
						navBasedOnZero = true;
					}
				
					if (mouseY > rectRightButton.yMax) {
						inputVert = 0f;
					}
					else {
						// use bottom 20% as slow setting
						if (mouseY > rectRightButton.yMin + rightButtonUpperAreaHeight) {
							inputVert = 0.001f;
						}
						else {
							inputVert = 1f - ((mouseY - rectRightButton.yMin) / rightButtonUpperAreaHeight);
						}
					}
					
					if (inputVert > 0.001f)
						navBasedOnZero = false;
						
					inputHorz = (((mouseX - rectRightButton.xMin) / (rectRightButton.xMax - rectRightButton.xMin)) * 2f) - 1f;
						
					if (inputVert > 1.0f)
						inputVert = 1.0f;
					if (inputHorz < -1.0f)
						inputHorz = -1.0f;
					if (inputHorz > 1.0f)
						inputHorz = 1.0f;

					if (levelManager.gameState == "gameStateStalking" || levelManager.gameState == "gameStateFeeding7")
						inputVert = 1f - (1f - inputVert) * (1f - inputVert);
					else if (levelManager.gameState == "gameStateChasing" || levelManager.gameState == "gameStateFeeding1a")
						inputVert = inputVert * inputVert;
						//inputVert = inputVert;
						//inputVert = 1f - (1f - inputVert) * (1f - inputVert);
					
					if (navBasedOnZero == false) {
						if (levelManager.gameState == "gameStateStalking")
							inputVert = 0.20f + inputVert * 0.80f;
						else if (levelManager.gameState == "gameStateChasing")
							inputVert = 0.67f + inputVert * 0.33f;
					}
					
					bool horzFlippedFlag = false;
					if (inputHorz < 0f) {
						horzFlippedFlag = true;
						inputHorz *= -1f;
					}
					float percentLogarithmic = (levelManager.gameState == "gameStateStalking") ? 0.20f : 1.00f;
					float inputHorzLogarithmic = 1f - (1f - inputHorz) * (1f - inputHorz);
					inputHorz = (inputHorz * (1f - percentLogarithmic)) + (inputHorzLogarithmic * percentLogarithmic);
					//inputHorz = 1f - (1f - inputHorz);
					//inputHorz = inputHorz * inputHorz;
					if (horzFlippedFlag == true)
						inputHorz *= -1f;	
						
						
					// lastly, filter all this out during tree collision, so when it resumes it ramps up

					if (levelManager.gameState == "gameStateTree1" || levelManager.gameState == "gameStateTree2") {
						navInProgress = false;
						levelManager.pumaAnimator.SetBool("Movement Engaged", false);
						inputVert = 0f;
						inputHorz = 0f;
					}
				}
			}
		}

		else {
			// no user input
			navInProgress = false;
			levelManager.pumaAnimator.SetBool("Movement Engaged", false);
			inputVert = 0f;
			inputHorz = 0f;
		}
		
		
		
		// fade in or out motion when starting or stopping

		if (inputRampUpFlag == false && ((oldInputVert == 0f && inputVert != 0f) || (oldInputHorz == 0f && inputHorz != 0f))) {
			// starting
			inputRampUpFlag = true;
			inputRampDownFlag = false;
			inputRampStartTime = Time.time;
			inputRampTotalTime = 0.2f;
			inputRampVertStartLevel = oldInputVert;
			inputRampHorzStartLevel = oldInputHorz;
			inputVert = oldInputVert;
			inputHorz = oldInputHorz;
		}
		
		else if (inputRampDownFlag == false && ((oldInputVert != 0f && inputVert == 0f) || (oldInputHorz != 0f && inputHorz == 0f))) {
			// stopping
			inputRampUpFlag = false;
			inputRampDownFlag = true;
			inputRampStartTime = Time.time;
			inputRampTotalTime = levelManager.gameState == "gameStateChasing" ? 0.45f : 0.25f;
			inputRampVertStartLevel = oldInputVert;
			inputRampHorzStartLevel = oldInputHorz;
			inputVert = oldInputVert;
			inputHorz = oldInputHorz;
		}
		
		else if (inputRampUpFlag == true) {
			if (Time.time > inputRampStartTime + inputRampTotalTime) {
				inputRampUpFlag = false;
			}
			else {
				float progressPercent = (Time.time - inputRampStartTime) / inputRampTotalTime;	
				inputVert = progressPercent * (inputVert - inputRampVertStartLevel);
				inputHorz = progressPercent * (inputHorz - inputRampHorzStartLevel);
			}
		}
		
		else if (inputRampDownFlag == true) {
			if (Time.time > inputRampStartTime + inputRampTotalTime) {
				inputRampDownFlag = false;
			}
			else {
				float progressPercent = (Time.time - inputRampStartTime) / inputRampTotalTime;	
				inputVert = inputRampVertStartLevel + progressPercent * (inputVert - inputRampVertStartLevel);
				inputHorz = inputRampHorzStartLevel + progressPercent * (inputHorz - inputRampHorzStartLevel);
			}
		}
				
				
		// check for relevant keys pressed on the physical keyboard
		if (Input.GetKey(KeyCode.Space) && rectLeftButton.xMin != rectLeftButton.xMax && guiManager.guiState != "guiStateFeeding1" && guiManager.guiState != "guiStatePumaDone1")
			keyStateLeftButton = true;
		if (Input.GetKey(KeyCode.RightShift) && rectMiddleButton.xMin != rectMiddleButton.xMax && guiManager.guiState != "guiStateFeeding1" && guiManager.guiState != "guiStatePumaDone1")
			keyStateMiddleButton = true;
		if (Input.GetKey(KeyCode.I) || Input.GetKey(KeyCode.UpArrow))
			keyStateForward = true;
		if (Input.GetKey(KeyCode.K) || Input.GetKey(KeyCode.DownArrow))
			keyStateBack = true;
		if (Input.GetKey(KeyCode.U))
			keyStateDiagLeft = true;
		if (Input.GetKey(KeyCode.O))
			keyStateDiagRight = true;
		if (Input.GetKey(KeyCode.J) || Input.GetKey(KeyCode.LeftArrow))
			keyStateTurnLeft = true;
		if (Input.GetKey(KeyCode.L) || Input.GetKey(KeyCode.RightArrow))
			keyStateTurnRight = true;
		if (Input.GetKey(KeyCode.A))
			levelManager.SwapLevel(0);
		if (Input.GetKey(KeyCode.S))
			levelManager.SwapLevel(1);
		if (Input.GetKey(KeyCode.D))
			levelManager.SwapLevel(2);
		if (Input.GetKey(KeyCode.F))
			levelManager.SwapLevel(3);
		if (Input.GetKey(KeyCode.G))
			levelManager.SwapLevel(4);

		if (Input.GetKey(KeyCode.Tab))
			levelManager.speedOverdrive = 3f;
		else
			levelManager.speedOverdrive = 1f;
			
		// set the heading to either straight ahead or diagonal
/*	
		if (keyStateForward == true)
			levelManager.pumaHeadingOffset = 0f;

		if (keyStateDiagLeft == true) {
			levelManager.pumaHeadingOffset = -60f;
			keyStateForward = true;
		}
		else if (keyStateDiagRight == true) {
			levelManager.pumaHeadingOffset = 60f;
			keyStateForward = true;
		}
*/		
		// handle left button during gameplay
		if (keyStateLeftButton == true) {
			if (levelManager.gameState == "gameStateChasing" && levelManager.GetPumaSideStalk() == true) {
				levelManager.SetPumaSideStalk(false);
				chasingBeganWithSideStalk = true;
			}
			else if (chasingBeganWithSideStalk == true) {
				// do nothing until button has been released
			}
			else if (levelManager.gameState == "gameStateStalking") {
				//if (navInProgress == true) {
					// side-stalk		
					if (levelManager.GetPumaSideStalk() == false)					
						levelManager.SetPumaSideStalk(true);		
				//}
			}
			else if (levelManager.gameState == "gameStateChasing") {
				// jump
				levelManager.PumaJump();
			}
		}
		else {
			levelManager.SetPumaSideStalk(false);
			chasingBeganWithSideStalk = false;
		}
		
		// handle middle button during gameplay
		if (keyStateMiddleButton == true) {
			if (levelManager.gameState == "gameStateStalking") {
				// exit gameplay
				guiManager.SetGuiState("guiStateLeavingGameplay");
				levelManager.SetGameState("gameStateLeavingGameplay");
			}
			else if (levelManager.gameState == "gameStateChasing") {
				// exit chase
				guiManager.SetGuiState("guiStateFeeding1");
				levelManager.SetGameState("gameStateFeeding1a");
			}
		}
		

		// deal with interactions between forward and back keys
	
		if (inputVert == 0) {
			if (forwardKey == false)
				forwardKey = keyStateForward;
			if (forwardKey == false)
				backKey = keyStateBack;	
		}
		else if (inputVert > 0) {
			if (forwardKey == false)
				forwardKey = keyStateForward;
			else if (keyStateBack == true)
				forwardKey = false;
		}
		else {
			if (backKey == false)
				backKey = keyStateBack;
			else if (keyStateForward == true)
				backKey = false;
		}
		
		// now do main input processing
	
		UpdateNavChannel(navStateForward, navValForward, forwardKey, Time.deltaTime * 3, Time.deltaTime * 3);
		navStateForward = newNavState;
		navValForward = newNavVal;

		UpdateNavChannel(navStateBack, -navValBack * 2, backKey, Time.deltaTime * 3, Time.deltaTime * 3);
		navStateBack = newNavState;
		navValBack = -newNavVal / 2;

		UpdateNavChannel(navStateLeft, -navValLeft, keyStateTurnLeft, Time.deltaTime * ((gameState == "gameStateStalking") ? 3f : 4.4f), Time.deltaTime * 3);
		navStateLeft = newNavState;
		navValLeft = -newNavVal;

		UpdateNavChannel(navStateRight, navValRight, keyStateTurnRight, Time.deltaTime * ((gameState == "gameStateStalking") ? 3f : 4.4f), Time.deltaTime * 3);
		navStateRight = newNavState;
		navValRight = newNavVal;
		
		//inputVert = navValForward;				 // disable backward motion
		//inputVert = navValForward + navValBack;		 // enable backward motion
		//inputHorz = navValRight + navValLeft;
			
		previousKeyStateLeftButton = keyStateLeftButton;
	}

	private void UpdateNavChannel(NavState previousNavState, float previousNavVal, bool keyPressed, float incStep, float decStep)
	{
		newNavState = previousNavState;
		newNavVal = previousNavVal;
	
		switch (previousNavState) {

		case NavState.Off:
			if (keyPressed) {
				newNavState = NavState.Inc;
				newNavVal = previousNavVal + incStep;
				if (newNavVal >= 1f) {
					newNavState = NavState.Full;
					newNavVal = 1f;
				}
			}
			else {
				newNavVal = 0f;
			}
			break;

		case NavState.Inc:
			if (keyPressed) {
				newNavVal = previousNavVal + incStep;
				if (newNavVal >= 1f) {
					newNavState = NavState.Full;
					newNavVal = 1f;
				}
			}
			else {
				newNavState = NavState.Dec;
				newNavVal = previousNavVal - decStep;
				if (newNavVal <= 0f) {
					newNavState = NavState.Off;
					newNavVal = 0f;
				}
			}
			break;
			
		case NavState.Full:
			if (keyPressed) {
				newNavVal = 1f;
			}
			else {
				newNavState = NavState.Dec;
				newNavVal = previousNavVal - decStep;
				if (newNavVal <= 0f) {
					newNavState = NavState.Off;
					newNavVal = 0f;
				}
			}
			break;
			
		case NavState.Dec:
			if (keyPressed) {
				newNavState = NavState.Inc;
				newNavVal = previousNavVal + incStep;
				if (newNavVal >= 1f) {
					newNavState = NavState.Full;
					newNavVal = 1f;
				}
			}
			else {
				newNavVal = previousNavVal - decStep;
				if (newNavVal <= 0f) {
					newNavState = NavState.Off;
					newNavVal = 0f;
				}
			}
			break;
		}
	}

	//===================================
	//===================================
	//		READ CURRENT STATE
	//===================================
	//===================================

	public float GetInputVert()
	{
		return inputVert;
	}
	
	public float GetInputHorz()
	{
		return inputHorz;
	}
	
	
	public void SetInputVert(float val)
	{	
		inputVertExternal = val;
		inputVertExternalSetFlag = true;
	}
	
	public bool CheckNavInProgress()
	{	
		return navInProgress;
	}
	
	
	
	//===================================
	//===================================
	//		SET ONSCREEN BOXES
	//===================================
	//===================================

	public void SetRectLeftButton(Rect rect)
	{
		rectLeftButton = rect;	
	}
	
	public void SetRectMiddleButton(Rect rect)
	{
		rectMiddleButton = rect;	
	}
	
	public void SetRectRightButton(Rect rect)
	{
		rectRightButton = rect;	
	}
	
	public void SetRectForward(Rect rect)
	{
		rectForward = rect;	
	}
	
	public void SetRectBack(Rect rect)
	{
		rectBack = rect;	
	}
	
	public void SetRectDiagLeft(Rect rect)
	{
		rectDiagLeft = rect;	
	}
	
	public void SetRectDiagRight(Rect rect)
	{
		rectDiagRight = rect;	
	}
	
	public void SetRectTurnLeft(Rect rect)
	{
		rectTurnLeft = rect;	
	}
	
	public void SetRectTurnRight(Rect rect)
	{
		rectTurnRight = rect;	
	}
}

















