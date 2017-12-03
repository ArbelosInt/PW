using UnityEngine;
using System.Collections;

/// GuiManager
/// Main handling of all user interface elements

public class GuiManager : MonoBehaviour 
{
	// DEBUGGING OPTIONS
	private bool skipStraightToLevel = false;
	private int  skipStraightToLevelFrameCount = 0;
	private bool displayFrameRate = false;

	//===================================
	//===================================
	//		MODULE VARIABLES
	//===================================
	//===================================

	// STATE MACHINE FOR GUI
	public string guiState;
	private float guiStateStartTime;
	private float guiStateDuration;
	private float guiFadePercentComplete;
	private float guiOpacity;
	
	// INFO PANEL MANAGEMENT
	private bool infoPanelVisible = false;
	private float infoPanelTransStart;
	private float infoPanelTransTime;

	// MISC VARIABLES
	public int selectedPuma = -1;			// TEMP: !!! should not be public; need to consolidate in either LevelManager or GUIManager, with function call to get it	
	public GUISkin customGUISkin;
	
	// KEYBOARD TRACKING
	private bool spacePressed = false;
	private bool tabPressed = false;
	private bool leftShiftPressed = false;
	private bool rightShiftPressed = false;
	private bool leftArrowPressed = false;
	private bool rightArrowPressed = false;
	
	// KEYBOARD DEBOUNCING
	private bool debounceSpace = false;
	private bool debounceTab = false;
	private bool debounceLeftShift = false;
	private bool debounceRightShift = false;
	private bool debounceLeftArrow = false;
	private bool debounceRightArrow = false;

	// PUMA CHARACTERISTICS
	private float[] speedArray = new float[] {0.85f, 0.75f, 0.55f, 0.45f, 0.25f, 0.15f};
	private float[] stealthArray = new float[] {0.15f, 0.25f, 0.45f, 0.55f, 0.75f, 0.85f};
	private float[] enduranceArray = new float[] {0.85f, 0.75f, 0.55f, 0.45f, 0.25f, 0.15f};
	private float[] powerArray = new float[] {0.15f, 0.25f, 0.45f, 0.55f, 0.75f, 0.85f};

	// EXTERNAL IMAGE FILES 
	public Texture2D logoTexture; 
	public Texture2D backgroundTexture; 
	public Texture2D pumaIconTexture; 
	public Texture2D pumaIconShadowTexture; 
	public Texture2D pumaIconShadowYellowTexture; 
	public Texture2D pumaIconHighlightTexture; 
	public Texture2D buckHeadTexture; 
	public Texture2D doeHeadTexture; 
	public Texture2D fawnHeadTexture; 
	public Texture2D buckStandTexture; 
	public Texture2D doeStandTexture; 
	public Texture2D fawnStandTexture; 
	public Texture2D hunterTexture; 
	public Texture2D vehicleTexture; 
	public Texture2D highwayTexture; 
	public Texture2D overpassTexture; 
	public Texture2D forestTexture; 
	public Texture2D buttonTexture; 
	public Texture2D buttonHoverTexture; 
	public Texture2D buttonDownTexture; 
//	public Texture2D swapButtonTexture; 
//	public Texture2D swapButtonHoverTexture; 
//	public Texture2D radioButtonTexture; 
//	public Texture2D radioSelectTexture; 
	public Texture2D sliderBarTexture; 
	public Texture2D sliderThumbTexture; 
	public Texture2D blackArrowTexture; 
	public Texture2D blackArrowShortTexture; 
	public Texture2D greenArrowTexture; 
	public Texture2D greenCheckTexture; 
	public Texture2D greenOutlineRectTexture; 
	public Texture2D greenOutlineRectVertTexture; 
	public Texture2D redXTexture; 
	public Texture2D pumaCrossbonesTexture; 
	public Texture2D pumaCrossbonesRedTexture; 
	public Texture2D pumaCrossbonesDarkRedTexture; 
	public Texture2D pumaStealthTexture; 
	public Texture2D pumaRunTexture; 
	public Texture2D pumaStealthDarkTexture; 
	public Texture2D pumaRunDarkTexture; 
	public Texture2D pumaAttackTexture; 
	public Texture2D pumaJumpTexture; 
	public Texture2D pumaPreyTexture; 
	public Texture2D greenHeartTexture; 
	public Texture2D headshot1Texture; 
	public Texture2D headshot2Texture; 
	public Texture2D headshot3Texture; 
	public Texture2D headshot4Texture; 
	public Texture2D headshot5Texture; 
	public Texture2D headshot6Texture; 
	public Texture2D closeup1Texture; 
	public Texture2D closeup2Texture; 
	public Texture2D closeup3Texture; 
	public Texture2D closeup4Texture; 
	public Texture2D closeup5Texture; 
	public Texture2D closeup6Texture; 
	public Texture2D closeup1OutlineTexture; 
	public Texture2D closeup2OutlineTexture; 
	public Texture2D closeup3OutlineTexture; 
	public Texture2D closeup4OutlineTexture; 
	public Texture2D closeup5OutlineTexture; 
	public Texture2D closeup6OutlineTexture; 
	public Texture2D closeupSensesTexture; 
	public Texture2D closeupBackgroundTexture; 
//	public Texture2D arrowTrayTexture; 
//	public Texture2D arrowTrayTopTexture; 
//	public Texture2D arrowTrayFlippedTexture; 
//	public Texture2D arrowTrayTopFlippedTexture; 
//	public Texture2D arrowLeftTexture; 
//	public Texture2D arrowRightTexture; 
//	public Texture2D arrowUpTexture; 
//	public Texture2D arrowDownTexture; 
//	public Texture2D arrowLeftOnTexture; 
//	public Texture2D arrowRightOnTexture; 
//	public Texture2D arrowUpOnTexture; 
//	public Texture2D arrowDownOnTexture; 
//	public Texture2D arrowTurnLeftTexture; 
//	public Texture2D arrowTurnRightTexture;
	public Texture2D pawDiagLeftTexture;
	public Texture2D pawDiagRightTexture;
	public Texture2D pawStraightTexture;
	public Texture2D controlRightIconsTexture;
	public Texture2D controlRightIconsLowerTexture;
	public Texture2D controlLeftJumpTexture;
	public Texture2D controlLeftExitTexture;
	public Texture2D controlLeftDiagLeftTexture;
	public Texture2D controlLeftDiagRightTexture;
	public Texture2D infoArrowDownTexture;
	public Texture2D infoArrowUpTexture;
	public Texture2D indicatorBuck; 
	public Texture2D indicatorDoe; 
	public Texture2D indicatorFawn; 
	public Texture2D indicatorBkgnd; 
	public Texture2D iconFacebookTexture; 
	public Texture2D iconTwitterTexture; 
	public Texture2D iconGoogleTexture; 
	public Texture2D iconPinterestTexture; 
	public Texture2D iconYouTubeTexture; 
	public Texture2D iconLinkedInTexture; 
	public Texture2D logoFelidaeTexture; 
	public Texture2D logoBappTexture; 
	public Texture2D levelImage1Texture; 
	public Texture2D levelImage2Texture; 
	public Texture2D levelImage3Texture; 
	public Texture2D levelImage4Texture; 
	public Texture2D levelImage5Texture; 
	public Texture2D levelImage6Texture; 
	public Texture2D levelImage6bTexture; 
	public Texture2D statsScreenTexture; 
	public Texture2D predationArrowsTexture; 
	public Texture2D predationCirclesTexture; 
	public Texture2D huntLongTexture; 
	public Texture2D huntShortTexture; 
	public Texture2D crossingScreenshotTexture; 
	public Texture2D crossingBadAngleTexture; 
	public Texture2D crossingGoodAngleTexture; 
	public Texture2D physicalCharacteristicsTexture;
	public Texture2D ecologyTexture;
	public Texture2D pumasAndPredationTexture;
	public Texture2D pumaKittenTexture;
	public Texture2D survivalThreatsTexture;
	public Sprite donateButtonSprite;
	

	// EXTERNAL MODULES
	private GuiComponents guiComponents;		// TEMP: may not need this when this file has been fully pruned
	private LevelManager levelManager;
	private ScoringSystem scoringSystem;
	private InputControls inputControls;
	private OverlayPanel overlayPanel;	
	private InfoPanel infoPanel;	
	private FeedingDisplay feedingDisplay;
	private PumaDoneDisplay pumaDoneDisplay;
	private GameplayDisplay gameplayDisplay;	

	//===================================
	//===================================
	//		INITIALIZATION
	//===================================
	//===================================

	void Start() 
	{	
		// connect to external modules
		guiComponents = GetComponent<GuiComponents>();
		levelManager = GetComponent<LevelManager>();
		scoringSystem = GetComponent<ScoringSystem>();
		inputControls = GetComponent<InputControls>();
		overlayPanel = GetComponent<OverlayPanel>();
		infoPanel = GetComponent<InfoPanel>();
		feedingDisplay = GetComponent<FeedingDisplay>();
		pumaDoneDisplay = GetComponent<PumaDoneDisplay>();
		gameplayDisplay = GetComponent<GameplayDisplay>();
		
		// additional initialization
		SetGuiState("guiStateEnterApp1");
		infoPanelVisible = true;
		infoPanelTransStart = Time.time - 100000;
		customGUISkin.button.normal.textColor = new Color(0.90f, 0.65f, 0f, 1f);
		customGUISkin.button.hover.textColor = new Color(0.99f, 0.75f, 0.21f, 1f);
	}
	
	
	//======================================
	//
	//	Update() - SET PARAMS PRIOR TO DRAW
	//
	//	This function is where any params 
	//	get changed prior to the draw code
	//	being called; all the param changes 
	//	must happen here because the draw
	//	code gets called twice (layout+render),
	//	and therefore must not change params
	//
	//======================================
	
	void Update() 
	{
		// clean handling for modifier keys
		DebounceKeyboardInput();

		// debug option (enabled at top of file)
		if (skipStraightToLevel == true) {
			selectedPuma = 0;
			levelManager.SetSelectedPuma(selectedPuma);			
			infoPanelVisible = false;
			if (++skipStraightToLevelFrameCount == 10) {
				SetGuiState("guiStateGameplay");
				levelManager.SetGameState("gameStateLeavingGui");
				skipStraightToLevel = false;
			}
		}
		
		// detect caught condition
		if (levelManager.IsCaughtState() == true && guiState != "guiStateFeeding1" && guiState != "guiStateFeeding2") {
			SetGuiState("guiStateFeeding1");
		}
		
		//======================================
		// GUI STATE MACHINE
		//
		// Processing for GUI state machine
		//======================================
		
		switch (guiState) {
		
		//---------------------
		// StartApp States
		//
		// game launch
		//---------------------

		case "guiStateEnterApp1":
			// start with solid black front rect
			guiStateDuration = 0.5f;
			if (Time.time > guiStateStartTime + guiStateDuration)
				SetGuiState("guiStateEnterApp2");
			break;

		case "guiStateEnterApp2":
			// fade-out of front rect
			guiStateDuration = 2.5f;
			FadeOutOpacityLogarithmic();		
			if (Time.time > guiStateStartTime + guiStateDuration) {
				SetGuiState("guiStateEnterApp3");
				FadeInOpacityLogarithmic();	
			}
			break;

		case "guiStateEnterApp3":
			// fade-in of ok button
			guiStateDuration = 0.4f;
			FadeInOpacityLogarithmic();	
			if (Time.time > guiStateStartTime + guiStateDuration)
				SetGuiState("guiStateStartApp1");
			break;

		case "guiStateStartApp1":
			// ongoing display of new level panel
			CheckForKeyboardEscapeFromNewLevel();
			break;

		case "guiStateStartApp2":
			// first check for end game case
			if (infoPanel.GetLevelNumber() == 5) {
				SetGuiState("guiStateFinalScreen5");
				return;
			}
			if (infoPanel.GetLevelNumber() == 6) {
				SetGuiState("guiStateFinalScreen9");
				return;
			}
			// fade-out of new level panel
			guiStateDuration = 1f;
			if (Time.time > guiStateStartTime + guiStateDuration) {
				infoPanel.SetNewLevelFlag(false);
				SetGuiState("guiStateEnteringOverlay");
			}
			break;

		//-----------------------
		// Overlay States
		//
		// entering, viewing and
		// leaving the main GUI
		//-----------------------

		case "guiStateEnteringOverlay":
			// fade-in of overlay panel
			guiStateDuration = 2.0f;
			FadeInOpacityLogarithmic();		
			CheckForKeyboardEscapeFromOverlay();
			CheckForKeyboardSelectionOfPuma();
			if (Time.time > guiStateStartTime + guiStateDuration)
				SetGuiState("guiStateOverlay");
			break;

		case "guiStateOverlay":
			// ongoing overlay state
			CheckForKeyboardEscapeFromOverlay();
			CheckForKeyboardSelectionOfPuma();
			break;

		case "guiStateLeavingOverlay":
			// fade-out of overlay panel
			guiStateDuration = 1f;
			FadeOutOpacityLogarithmic();
			if (Time.time > guiStateStartTime + guiStateDuration)
				SetGuiState("guiStateEnteringGameplay1");
			break;
			
		case "guiStateLeavingInfoPanel":
			// fade-out of info panel
			guiStateDuration = 1f;
			FadeOutOpacityLogarithmic();
			if (Time.time > guiStateStartTime + guiStateDuration)
				SetGuiState("guiStateEnteringGameplay1");
			break;
			
		//-----------------------
		// Gameplay States
		//
		// entering, viewing and
		// leaving the 3D world
		//-----------------------

		case "guiStateEnteringGameplay1":
			// no GUI during initial camera zoom
			guiStateDuration = 2.7f;
			if (Time.time > guiStateStartTime + guiStateDuration)
				SetGuiState("guiStateEnteringGameplay2");
			break;
			
		case "guiStateEnteringGameplay2":
			// fade-in of position indicator background
			guiStateDuration = 1.1f;
			FadeInOpacityLogarithmic();
			CheckForKeyboardEscapeFromGameplay();
			if (Time.time > guiStateStartTime + guiStateDuration)
				SetGuiState("guiStateEnteringGameplay2a");
			break;
			
		case "guiStateEnteringGameplay2a":
			// fade-in of position indicators
			guiStateDuration = 1.4f * 0.7f;
			FadeInOpacityLogarithmic();
			CheckForKeyboardEscapeFromGameplay();
			if (Time.time > guiStateStartTime + guiStateDuration)
				SetGuiState("guiStateEnteringGameplay3");
			break;
			
		case "guiStateEnteringGameplay3":
			// zoom the indicators to screen edges
			guiStateDuration = 1.75f * 0.8f;
			FadeInOpacityLogarithmic();
			CheckForKeyboardEscapeFromGameplay();
			if (Time.time > guiStateStartTime + guiStateDuration)
				SetGuiState("guiStateEnteringGameplay4");
			break;
			
		case "guiStateEnteringGameplay4":
			// fade-in of movement controls
			guiStateDuration = 1.5f * 0.8f;
			FadeInOpacityLogarithmic();
			if (Time.time > guiStateStartTime + guiStateDuration)
				SetGuiState("guiStateEnteringGameplay5");
			break;
			
		case "guiStateEnteringGameplay5":
			// fade-in of status displays
			guiStateDuration = 1.2f * 0.8f;
			FadeInOpacityLinear();
			CheckForKeyboardEscapeFromGameplay();
			if (Time.time > guiStateStartTime + guiStateDuration)
				SetGuiState("guiStateGameplay");
			break;
			
		case "guiStateGameplay":
			// ongoing game-play state
			CheckForKeyboardEscapeFromGameplay();
			break;
			
		case "guiStateLeavingGameplay":
			// fade-out of game-play controls
			guiStateDuration = 0.7f;
			FadeOutOpacityLinear();
			CheckForKeyboardSelectionOfPuma();
			if (Time.time > guiStateStartTime + guiStateDuration) {
				SetGuiState("guiStateEnteringOverlay");
				if (overlayPanel.GetCurrentScreen() == 3) {
					// return to select screen rather than quit screen
					overlayPanel.SetCurrentScreen(0);
				}
				if (scoringSystem.GetPumaHealth(selectedPuma) <= 0f) {
					// puma has died
					selectedPuma = -1;
					overlayPanel.SetCurrentScreen(0);
				}
			}
			break;
		
		case "guiStateLeavingFeeding":
			// fade-out of feeding controls
			guiStateDuration = 0.7f;
			FadeOutOpacityLinear();
			CheckForKeyboardSelectionOfPuma();
			if (Time.time > guiStateStartTime + guiStateDuration) {
				overlayPanel.SetCurrentScreen(scoringSystem.GetPumaHealth(selectedPuma) == 1f ? 0 : 2);
				SetGuiState("guiStateEnteringOverlay");
			}
			break;
		
		//------------------------------
		// Feeding States
		//
		// entering, viewing and
		// leaving the feeding display
		//------------------------------

		case "guiStateFeeding1":
			// fade-out of game-play controls
			guiStateDuration = 1f;
			FadeOutOpacityLinear();
			if (Time.time > guiStateStartTime + guiStateDuration) {
				levelManager.goStraightToFeeding = false;
				SetGuiState("guiStateFeeding2");
			}
			break;

		case "guiStateFeeding2":
			// pause during attack on deer
			guiStateDuration = (levelManager.caughtDeer != null) ? 2f : (levelManager.gameState == "gameStateTree1a" || levelManager.gameState == "gameStateTree2a") ?  1.8f : 0.7f;
			if (Time.time - guiStateStartTime > guiStateDuration) {
				feedingDisplay.PrepareGUIItems();
				SetGuiState("guiStateFeeding3");
			}
			break;

		case "guiStateFeeding3":
			// fade-in of feeding display main panel
			guiStateDuration = 1.5f;
			FadeInOpacityLinear();
			scoringSystem.SetScoreReduction(1f);
			CheckForKeyboardEscapeFromFeeding();
			if (Time.time > guiStateStartTime + guiStateDuration)
				SetGuiState("guiStateFeeding3a");
			break;

		case "guiStateFeeding3a":
			// scroll the score numbers
			guiStateDuration = 2f;
			FadeInOpacityLinear();
			scoringSystem.SetScoreReduction(1f - guiOpacity);
			CheckForKeyboardEscapeFromFeeding();
			if (Time.time > guiStateStartTime + guiStateDuration)
				SetGuiState("guiStateFeeding4");
			break;

		case "guiStateFeeding4":
			// fade-in of level indicator
			guiStateDuration = 0.75f;
			FadeInOpacityLinear();
			scoringSystem.SetScoreReduction(0f);
			CheckForKeyboardEscapeFromFeeding();
			if (Time.time > guiStateStartTime + guiStateDuration)
				SetGuiState("guiStateFeeding4a");
			break;

		case "guiStateFeeding4a":
			// brief pause
			guiStateDuration = 0.3f;
			CheckForKeyboardEscapeFromFeeding();
			if (Time.time > guiStateStartTime + guiStateDuration)
				SetGuiState("guiStateFeeding5");
			break;

		case "guiStateFeeding5":
			// fade-in of feeding display 'ok' button
			guiStateDuration = 0.7f;
			FadeInOpacityLinear();
			CheckForKeyboardEscapeFromFeeding();
			if (Time.time > guiStateStartTime + guiStateDuration)
				SetGuiState("guiStateFeeding6");
			break;

		case "guiStateFeeding6":
			// ongoing view of feeding display
			CheckForKeyboardEscapeFromFeeding();
			break;

		case "guiStateFeeding7":
			// fade-out of feeding display
			guiStateDuration = 2.6f;
			FadeOutOpacityLinear();
			if (Time.time > guiStateStartTime + guiStateDuration)
				SetGuiState("guiStateFeeding7a");
			break;

		case "guiStateFeeding7a":
			// pause for puma to step over deer
			guiStateDuration = 1.4f;
			if (Time.time > guiStateStartTime + guiStateDuration)
				SetGuiState("guiStateFeeding8");
			break;

		case "guiStateFeeding8":
			//  fade-in of position indicator background
			guiStateDuration = 1.7f * 0.7f;
			FadeInOpacityLogarithmic();
			CheckForKeyboardEscapeFromGameplay();
			if (Time.time > guiStateStartTime + guiStateDuration)
				SetGuiState("guiStateFeeding8a");
			break;
			
		case "guiStateFeeding8a":
			// fade-in of position indicators
			guiStateDuration = 1f * 0.7f;
			FadeInOpacityLogarithmic();
			CheckForKeyboardEscapeFromGameplay();
			if (Time.time > guiStateStartTime + guiStateDuration)
				SetGuiState("guiStateFeeding9");
			break;
			
		case "guiStateFeeding9":
			// zoom the indicators to screen edges
			guiStateDuration = 1.5f * 0.8f;
			FadeInOpacityLogarithmic();
			CheckForKeyboardEscapeFromGameplay();
			if (Time.time > guiStateStartTime + guiStateDuration)
				SetGuiState("guiStateFeeding10");
			break;
			
		case "guiStateFeeding10":
			// fade-in of movement controls
			guiStateDuration = 1.1f * 0.8f;
			FadeInOpacityLogarithmic();
			CheckForKeyboardEscapeFromGameplay();
			if (Time.time > guiStateStartTime + guiStateDuration)
				SetGuiState("guiStateFeeding11");
			break;

		case "guiStateFeeding11":
			// fade-in of status indicators
			guiStateDuration = 0.9f * 0.8f;
			FadeInOpacityLinear();
			CheckForKeyboardEscapeFromGameplay();
			if (Time.time > guiStateStartTime + guiStateDuration)
				SetGuiState("guiStateGameplay");
			break;

		//------------------------------
		// Next Level
		//
		// progress to next level
		//------------------------------

		case "guiStateNextLevel1":
			// fade-out of feeding display
			guiStateDuration = 0.7f;
			FadeOutOpacityLinear();
			if (Time.time > guiStateStartTime + guiStateDuration) {
				SetGuiState("guiStateNextLevel2");
				selectedPuma = -1;
				overlayPanel.SetCurrentScreen(0);
				scoringSystem.SetHuntSuccessCount(0);
				scoringSystem.LevelHasChanged();
			}
			break;
			
		case "guiStateNextLevel2":
			// brief pause
			guiStateDuration = 0.3f;
			if (Time.time > guiStateStartTime + guiStateDuration)
				SetGuiState("guiStateNextLevel3");
			break;			
			
		case "guiStateNextLevel3":
			// fade in of back rect
			guiStateDuration = 1.4f;
			FadeInOpacityLogarithmic();
			if (Time.time > guiStateStartTime + guiStateDuration)
				SetGuiState("guiStateNextLevel4");
			break;			

		case "guiStateNextLevel4":
			// brief pause
			guiStateDuration = 0.3f;
			if (Time.time > guiStateStartTime + guiStateDuration) {
				infoPanel.SetNewLevelFlag(true);
				infoPanel.SetNewLevelNumber(scoringSystem.GetPopulationHealth() > 0f ? (levelManager.GetCurrentLevel() + 1) : 5);
				OpenInfoPanel(-1, true);
				SetGuiState("guiStateNextLevel5");
			}
			break;			

		case "guiStateNextLevel5":
			// fade in of info screen and switch to next level
			guiStateDuration = 2.4f;
			FadeInOpacityLogarithmic();
			if (Time.time > guiStateStartTime + guiStateDuration) {
				int nextLevel = levelManager.GetCurrentLevel() + 1;
				if (nextLevel > 4 || scoringSystem.GetPopulationHealth() == 0f) {
					levelManager.InitLevel(0);
					SetGuiState("guiStateFinalScreen1");
				}
				else {
					levelManager.InitLevel(nextLevel);
					SetGuiState("guiStateNextLevel6");
				}
			}
			break;			

		case "guiStateNextLevel6":
			// brief pause
			guiStateDuration = 0.2f;
			if (Time.time > guiStateStartTime + guiStateDuration)
				SetGuiState("guiStateNextLevel7");
			break;			

		case "guiStateNextLevel7":
			// fade out of back rect
			guiStateDuration = 2.4f;
			FadeOutOpacityLogarithmic();
			CheckForKeyboardEscapeFromNewLevel();
			if (Time.time > guiStateStartTime + guiStateDuration)
				SetGuiState("guiStateNextLevel8");
			break;			

		case "guiStateNextLevel8":
			// fade in of 'Go' button
			guiStateDuration = 1f;
			FadeInOpacityLinear();
			CheckForKeyboardEscapeFromNewLevel();
			if (Time.time > guiStateStartTime + guiStateDuration)
				SetGuiState("guiStateStartApp1");
			break;	

		//------------------------------
		// Final Screen
		//
		// after completing last level
		//------------------------------

		case "guiStateFinalScreen1":
			// brief pause
			guiStateDuration = 0.2f;
			if (Time.time > guiStateStartTime + guiStateDuration)
				SetGuiState("guiStateFinalScreen2");
			break;			

		case "guiStateFinalScreen2":
			// fade out of back rect to half opacity
			guiStateDuration = 2f;
			FadeOutOpacityLogarithmic();
			CheckForKeyboardEscapeFromNewLevel();
			if (Time.time > guiStateStartTime + guiStateDuration)
				SetGuiState("guiStateFinalScreen3");
			break;			

		case "guiStateFinalScreen3":
			// fade in of 'Go' button
			guiStateDuration = 1f;
			FadeInOpacityLinear();
			CheckForKeyboardEscapeFromNewLevel();
			if (Time.time > guiStateStartTime + guiStateDuration) {
				SetGuiState("guiStateFinalScreen4");
			}
			break;	

		case "guiStateFinalScreen4":
			// ongoing display of first final screen
			CheckForKeyboardEscapeFromNewLevel();
			break;

		case "guiStateFinalScreen5":
			// fade-out of first final screen
			guiStateDuration = 1.4f;
			infoPanel.SetBackgroundIsLocked(true);
			if (Time.time > guiStateStartTime + guiStateDuration) {
				// after last level; add one more screen
				infoPanel.SetNewLevelFlag(false);
				infoPanel.SetNewLevelNumber(6);
				infoPanel.SetCurrentScreen(4);
				OpenInfoPanel(4, true);
				SetGuiState("guiStateFinalScreen6");
			}
			break;

		case "guiStateFinalScreen6":
			// fade in of second final screen
			guiStateDuration = 4f;
			FadeInOpacityLogarithmic();
			CheckForKeyboardEscapeFromNewLevel();
			if (Time.time > guiStateStartTime + guiStateDuration)
				SetGuiState("guiStateFinalScreen7");
			break;			

		case "guiStateFinalScreen7":
			// fade in of 'Go' button
			guiStateDuration = 1f;
			FadeInOpacityLinear();
			CheckForKeyboardEscapeFromNewLevel();
			if (Time.time > guiStateStartTime + guiStateDuration) {
				SetGuiState("guiStateFinalScreen8");
			}
			break;			

		case "guiStateFinalScreen8":
			// ongoing display of second final screen
			CheckForKeyboardEscapeFromNewLevel();
			break;

		case "guiStateFinalScreen9":
			// fade-out of second final screen
			guiStateDuration = 1.4f;
			FadeOutOpacityLogarithmic();
			infoPanel.SetBackgroundIsLocked(false);
			if (Time.time > guiStateStartTime + guiStateDuration) {
				scoringSystem.InitScoringSystem();
				infoPanel.SetNewLevelNumber(0);	
				SetGuiState("guiStateEnteringOverlay");
			}
			break;

		//------------------------------
		// Puma Done
		//
		// starvation or
		// collision with car
		// or reaching full health
		//------------------------------

		case "guiStatePumaDone1":
			// fade-out of done screen
			guiStateDuration = 2f;
			FadeOutOpacityLinear();
			if (Time.time > guiStateStartTime + guiStateDuration)
				SetGuiState("guiStatePumaDone2");
			break;

		case "guiStatePumaDone2":
			// pause for a bit of space
			guiStateDuration = 2f;
			if (Time.time - guiStateStartTime > guiStateDuration) {
				pumaDoneDisplay.PrepareGUIItems(levelManager.CheckStarvation() == true ? "threatTypeStarvation" : "threatTypeVehicle");		
				SetGuiState("guiStatePumaDone3");
			}
			break;

		case "guiStatePumaDone3":
			// fade-in of done display main panel
			guiStateDuration = 2f;
			FadeInOpacityLinear();
			CheckForKeyboardEscapeFromPumaDone();
			if (Time.time > guiStateStartTime + guiStateDuration)
				SetGuiState("guiStatePumaDone4");
			break;

		case "guiStatePumaDone4":
			// brief pause
			guiStateDuration = 0.2f;
			CheckForKeyboardEscapeFromPumaDone();
			if (Time.time > guiStateStartTime + guiStateDuration)
				SetGuiState("guiStatePumaDone5");
			break;

		case "guiStatePumaDone5":
			// fade-in of done display 'ok' button
			guiStateDuration = 1f;
			FadeInOpacityLinear();
			CheckForKeyboardEscapeFromPumaDone();
			if (Time.time > guiStateStartTime + guiStateDuration)
				SetGuiState("guiStatePumaDone6");
			break;

		case "guiStatePumaDone6":
			// ongoing view of done display
			CheckForKeyboardEscapeFromPumaDone();
			break;

		case "guiStateLeavingPumaDone":
			// fade-out of done display
			guiStateDuration = 0.7f;
			FadeOutOpacityLinear();
			CheckForKeyboardSelectionOfPuma();
			if (Time.time > guiStateStartTime + guiStateDuration) {
				SetGuiState(scoringSystem.GetPopulationHealth() > 0f ? "guiStateEnteringOverlay" : "guiStateNextLevel2");
				selectedPuma = -1;
				overlayPanel.SetCurrentScreen(0);
			}
			break;
		
		case "guiStateLeavingPumaDoneAlt":
			// leaving gameplay from info panel; no fade-out of done display
			guiStateDuration = 0.7f;
			FadeOutOpacityLinear();
			CheckForKeyboardSelectionOfPuma();
			if (Time.time > guiStateStartTime + guiStateDuration) {
				SetGuiState("guiStateEnteringOverlay");
				selectedPuma = -1;
				overlayPanel.SetCurrentScreen(0);
			}
			break;
		
		//------------------
		// Error Check
		//------------------
		
		default:
			Debug.Log("ERROR - GuiManager.Update() got bad state: " + guiState);
			break;
		}
		
		UpdateGUIItems();  // replaces OnGUI() in the old GUI system
	}


	//=====================================
	//=====================================
	//	  UTILITIES USED BY UPDATE()
	//=====================================
	//=====================================


	private void CheckForKeyboardEscapeFromGameplay()
	{
		if (spacePressed || rightShiftPressed) {
			// use keyboard to leave gameplay
			//SetGuiState("guiStateLeavingGameplay");
			//levelManager.SetGameState("gameStateLeavingGameplay");
		}	
	}
	
	private void CheckForKeyboardEscapeFromOverlay()
	{
		if (selectedPuma >= 0 && (spacePressed || rightShiftPressed)) {
			// use keyboard to leave overlay
			SetGuiState("guiStateLeavingOverlay");
			levelManager.SetGameState("gameStateLeavingGui");
		}	
	}
	
	private void CheckForKeyboardEscapeFromNewLevel()
	{
		if (spacePressed || rightShiftPressed) {
			// use keyboard to close info panel
			CloseInfoPanel(true);
			SetGuiState("guiStateStartApp2");
		}	
	}
	
	private void CheckForKeyboardEscapeFromFeeding()
	{
		if (spacePressed || rightShiftPressed) {
			if (scoringSystem.GetHuntSuccessCount() >= 3) {
				// use keyboard to go to next level
				SetGuiState("guiStateNextLevel1");
				levelManager.SetGameState("gameStateLeavingGameplay");
			}
			else {
				// use keyboard to resume gameplay
				SetGuiState("guiStateFeeding7");
				levelManager.SetGameState("gameStateFeeding5");
			}
		}
	}

	private void CheckForKeyboardEscapeFromPumaDone()
	{
		if (spacePressed || rightShiftPressed) {
			// use keyboard to go to overlay
			SetGuiState("guiStateLeavingPumaDone");
			if (levelManager.CheckCarCollision() == true)
				levelManager.EndCarCollision();
			else if (levelManager.CheckStarvation() == true)
				levelManager.EndStarvation();		
			levelManager.SetGameState("gameStateLeavingGameplay");
		}
	}

	private void CheckForKeyboardSelectionOfPuma()
	{
		if (overlayPanel.GetCurrentScreen() == 0) {
			// we are in 'select' screen
			if (leftArrowPressed)
				DecrementPuma();
			if (rightArrowPressed)
				IncrementPuma();
		}
	}

	////////////////////////////////
	////////////////////////////////
	////////////////////////////////

	private void FadeInOpacityLogarithmic()
	{
		// logarithmic curve 
		if (Time.time - guiStateStartTime < (guiStateDuration * 0.5f)) {
			guiFadePercentComplete = (Time.time - guiStateStartTime) / (guiStateDuration * 0.5f);
			guiFadePercentComplete = guiFadePercentComplete * guiFadePercentComplete;
			guiOpacity = guiFadePercentComplete * 0.5f;
		}
		else if (Time.time - guiStateStartTime < guiStateDuration) {
			guiFadePercentComplete = ((Time.time - guiStateStartTime) - (guiStateDuration * 0.5f)) / (guiStateDuration * 0.5f);				
			guiFadePercentComplete = guiFadePercentComplete + (guiFadePercentComplete - (guiFadePercentComplete * guiFadePercentComplete));
			guiOpacity = 0.5f + guiFadePercentComplete * 0.5f;
		}
		else {
			guiOpacity = 1.0f;
			UpdateGUIItems();
		}
	}

	private void FadeInOpacityLinear()
	{
		// linear curve
		if (Time.time - guiStateStartTime < guiStateDuration) {
			guiFadePercentComplete = (Time.time - guiStateStartTime) / guiStateDuration;
			guiOpacity = guiFadePercentComplete;
		}
		else {
			guiOpacity = 1.0f;
			UpdateGUIItems();
		}
	}

	private void FadeOutOpacityLogarithmic()
	{
		// logarithmic curve
		if (Time.time - guiStateStartTime < (guiStateDuration * 0.5f)) {
			guiFadePercentComplete = (Time.time - guiStateStartTime) / (guiStateDuration * 0.5f);
			guiFadePercentComplete = guiFadePercentComplete * guiFadePercentComplete;
			guiOpacity =  1f - (guiFadePercentComplete * 0.5f);
		}
		else if (Time.time - guiStateStartTime < guiStateDuration) {
			guiFadePercentComplete = ((Time.time - guiStateStartTime) - (guiStateDuration * 0.5f)) / (guiStateDuration * 0.5f);				
			guiFadePercentComplete = guiFadePercentComplete + (guiFadePercentComplete - (guiFadePercentComplete * guiFadePercentComplete));
			guiOpacity =  1f - (0.5f + guiFadePercentComplete * 0.5f);
		}
		else {
			guiOpacity = 0.0f;
			UpdateGUIItems();
		}
	}

	private void FadeOutOpacityLinear()
	{
		// linear curve
		if (Time.time - guiStateStartTime < guiStateDuration) {
			guiFadePercentComplete = (Time.time - guiStateStartTime) / guiStateDuration;
			guiOpacity =  1f - guiFadePercentComplete;
		}
		else {
			guiOpacity = 0.0f;
			UpdateGUIItems();
		}
	}


	//===============================================
	//
	//	UpdateGUIItems() - DRAW THE USER INTERFACE
	//
	//	This function is the top level
	//	draw routine for rendering the UI;
	//	it replaces OnGUI from the old interface
	//
	//===============================================
	
	void UpdateGUIItems()
	{	
		// initially clear the gameplay input rects
		// if the rects are active, these values will be filled in during gameplayDisplay.Draw call below
		inputControls.SetRectLeftButton(new Rect(0f, 0f, 0f, 0f)); 
		inputControls.SetRectRightButton(new Rect(0f, 0f, 0f, 0f)); 

		if (infoPanelVisible == false || Time.time - infoPanelTransStart < infoPanelTransTime) {
		
			float infoPanelOpacityComplement = 1f;
			float infoPanelElapsedTime = Time.time - infoPanelTransStart;
		
			if (infoPanelVisible == true && infoPanelElapsedTime < infoPanelTransTime) {
				// fading in
				infoPanelOpacityComplement = 1f - infoPanelElapsedTime / infoPanelTransTime;			
			}
			else if (infoPanelVisible == false && infoPanelElapsedTime < infoPanelTransTime) {
				// fading out		
				infoPanelOpacityComplement = infoPanelElapsedTime / infoPanelTransTime;
			}		
			
			// DRAW SOMETHING - info panel is either not showing, or is fading in or out

			switch (guiState) {
		
			//-----------------------
			// Overlay States
			//
			// entering, viewing and
			// leaving the main GUI
			//-----------------------

			case "guiStateEnteringOverlay":
			case "guiStateOverlay":
			case "guiStateLeavingOverlay":
				overlayPanel.UpdateGUIItems(guiOpacity * infoPanelOpacityComplement);
				break;
							
			//-----------------------
			// Gameplay States
			//
			// entering, viewing and
			// leaving the 3D world
			//-----------------------

			case "guiStateEnteringGameplay1":
				// no GUI during initial camera zoom
				break;
				
			case "guiStateEnteringGameplay2":
				// fade-in of position indicator backgrounds
				gameplayDisplay.UpdateGUIItems(0f, guiOpacity, 0f, 0f, 0f);
				break;
				
			case "guiStateEnteringGameplay2a":
				// fade-in of position indicators
				gameplayDisplay.UpdateGUIItems(0f, 1f, guiOpacity, 0f, 0f);
				break;
				
			case "guiStateEnteringGameplay3":
				// zoom the indicators to screen edges
				gameplayDisplay.UpdateGUIItems(0f, 1f, 1f, guiOpacity, 0f);
				break;
				
			case "guiStateEnteringGameplay4":
				// fade-in of movement controls
				gameplayDisplay.UpdateGUIItems(guiOpacity, 1f, 1f, 1f, 0f);
				break;
				
			case "guiStateEnteringGameplay5":
				// fade-in of status displays
				gameplayDisplay.UpdateGUIItems(1f, 1f, 1f, 1f, guiOpacity);
				break;
				
			case "guiStateGameplay":
				// ongoing game-play state
				gameplayDisplay.UpdateGUIItems(1f, 1f, 1f, 1f, 1f);
				break;
				
			case "guiStateLeavingGameplay":
				// fade-out of game-play controls
				gameplayDisplay.UpdateGUIItems(guiOpacity, guiOpacity, guiOpacity, 1f, guiOpacity);
				break;
			
			case "guiStateLeavingFeeding":
				// fade-out of feeding display
				feedingDisplay.UpdateGUIItems(guiOpacity, 1f, guiOpacity, guiOpacity);
				break;
			
			//------------------------------
			// Feeding States
			//
			// entering, viewing and
			// leaving the feeding display
			//------------------------------

			case "guiStateFeeding1":
				// fade-out of game-play controls
				gameplayDisplay.UpdateGUIItems(guiOpacity, guiOpacity, guiOpacity, 1f, guiOpacity);
				break;

			case "guiStateFeeding2":
				// no GUI display during attack on deer
				break;

			case "guiStateFeeding3":
				// fade-in of feeding display main panel
				feedingDisplay.UpdateGUIItems(guiOpacity, 0f, 0f, 0f);
				break;

			case "guiStateFeeding3a":
				// brief pause for rolling score
				feedingDisplay.UpdateGUIItems(1f, guiOpacity, 0f, 0f);
				break;

			case "guiStateFeeding4":
				// fade-in of level display
				feedingDisplay.UpdateGUIItems(1f, 1f, guiOpacity, 0f);
				break;

			case "guiStateFeeding4a":
				// brief pause
				feedingDisplay.UpdateGUIItems(1f, 1f, 1f, 0f);
				break;

			case "guiStateFeeding5":
				// fade-in of feeding display 'ok' button
				feedingDisplay.UpdateGUIItems(1f, 1f, 1f, guiOpacity);
				break;

			case "guiStateFeeding6":
				// ongoing view of feeding display
				feedingDisplay.UpdateGUIItems(1f, 1f, 1f, 1f);
				break;

			case "guiStateFeeding7":
				// fade-out of feeding display
				feedingDisplay.UpdateGUIItems(guiOpacity, 1f, guiOpacity, guiOpacity);
				break;

			case "guiStateFeeding8":
				// fade-in of position indicator backgrounds
				gameplayDisplay.UpdateGUIItems(0f, guiOpacity, 0f, 0f, 0f);
				break;
				
			case "guiStateFeeding8a":
				// fade-in of position indicators
				gameplayDisplay.UpdateGUIItems(0f, 1f, guiOpacity, 0f, 0f);
				break;
				
			case "guiStateFeeding9":
				// zoom the indicators to screen edges
				gameplayDisplay.UpdateGUIItems(0f, 1f, 1f, guiOpacity, 0f);
				break;
				
			case "guiStateFeeding10":
				// fade-in of movement controls
				gameplayDisplay.UpdateGUIItems(guiOpacity, 1f, 1f, 1f, 0f);
				break;
				
			case "guiStateFeeding11":
				// fade-in of status indicators
				gameplayDisplay.UpdateGUIItems(1f, 1f, 1f, 1f, guiOpacity);
				break;
				
			//------------------------------
			// Next Level
			//
			// progress to next level
			//------------------------------

			case "guiStateNextLevel1":
				// fade-out of feeding display
				feedingDisplay.UpdateGUIItems(guiOpacity, 1f, guiOpacity, guiOpacity);
				break;

			//------------------------------
			// Puma Done
			//
			// starvation or
			// collision with car
			// or reaching full health
			//------------------------------

			case "guiStatePumaDone1":
				// fade-out of game-play controls
				gameplayDisplay.UpdateGUIItems(guiOpacity, guiOpacity, guiOpacity, 1f, guiOpacity);
				break;

			case "guiStatePumaDone2":
				// pause for a bit of space
				break;

			case "guiStatePumaDone3":
				// fade-in of puma done display main panel
				if (levelManager.CheckCarCollision() == true)
					pumaDoneDisplay.UpdateGUIItems(guiOpacity, "threatTypeVehicle", 0f);
				else if (levelManager.CheckStarvation() == true)
					pumaDoneDisplay.UpdateGUIItems(guiOpacity, "threatTypeStarvation", 0f);	
				break;

			case "guiStatePumaDone4":
				// brief pause
				if (levelManager.CheckCarCollision() == true)
					pumaDoneDisplay.UpdateGUIItems(1f, "threatTypeVehicle", 0f);
				else if (levelManager.CheckStarvation() == true)
					pumaDoneDisplay.UpdateGUIItems(1f, "threatTypeStarvation", 0f);
				break;

			case "guiStatePumaDone5":
				// fade-in of puma done display 'ok' button
				if (levelManager.CheckCarCollision() == true)
					pumaDoneDisplay.UpdateGUIItems(1f * infoPanelOpacityComplement, "threatTypeVehicle", guiOpacity * infoPanelOpacityComplement);
				else if (levelManager.CheckStarvation() == true)
					pumaDoneDisplay.UpdateGUIItems(1f * infoPanelOpacityComplement, "threatTypeStarvation", guiOpacity * infoPanelOpacityComplement);
				break;

			case "guiStatePumaDone6":
				// ongoing view of puma done display
				if (levelManager.CheckCarCollision() == true)
					pumaDoneDisplay.UpdateGUIItems(1f * infoPanelOpacityComplement, "threatTypeVehicle", 1f * infoPanelOpacityComplement);
				else if (levelManager.CheckStarvation() == true)
					pumaDoneDisplay.UpdateGUIItems(1f * infoPanelOpacityComplement, "threatTypeStarvation", 1f * infoPanelOpacityComplement);
				break;

			case "guiStateLeavingPumaDone":
				// fade-out of puma done display
				if (levelManager.CheckCarCollision() == true)
					pumaDoneDisplay.UpdateGUIItems(guiOpacity, "threatTypeVehicle", guiOpacity);
				else if (levelManager.CheckStarvation() == true)
					pumaDoneDisplay.UpdateGUIItems(guiOpacity, "threatTypeStarvation", guiOpacity);
				break;
			}
		}

		//------------------------------
		// Draw Info Panel
		//------------------------------
				
		float elapsedTime = Time.time - infoPanelTransStart;
		float percentVisible = 0f;
		float backRectOpacity = 0f;
		float goButtonOpacity = 1f;

		if (guiState == "guiStateNextLevel3")
			backRectOpacity = guiOpacity;
		if (guiState == "guiStateNextLevel4" || guiState == "guiStateNextLevel5" || guiState == "guiStateNextLevel6" || guiState == "guiStateFinalScreen1")
			backRectOpacity = 1f;
		else if (guiState == "guiStateNextLevel7")
			backRectOpacity = guiOpacity;
		else if (guiState == "guiStateFinalScreen2")
			backRectOpacity = 0.5f + (guiOpacity * 0.5f);
		else if (guiState == "guiStateFinalScreen3" || guiState == "guiStateFinalScreen4" || guiState == "guiStateFinalScreen5" || guiState == "guiStateFinalScreen6" || guiState == "guiStateFinalScreen7" || guiState == "guiStateFinalScreen8")
			backRectOpacity = 0.5f;
		else if (guiState == "guiStateFinalScreen9")
			backRectOpacity = guiOpacity * 0.5f;
			
			
		if (guiState == "guiStateNextLevel3" || guiState == "guiStateNextLevel4" || guiState == "guiStateNextLevel5" || guiState == "guiStateNextLevel6" || guiState == "guiStateNextLevel7" || guiState == "guiStateFinalScreen1" || guiState == "guiStateFinalScreen2" || guiState == "guiStateFinalScreen6")  
			goButtonOpacity = 0f;
		else if (guiState == "guiStateNextLevel8" || guiState == "guiStateFinalScreen3" || guiState == "guiStateFinalScreen7")
			goButtonOpacity = guiOpacity;	

			
		if (guiState == "guiStateNextLevel3" || guiState == "guiStateNextLevel4") {
			infoPanel.UpdateGUIItems(0f, backRectOpacity, goButtonOpacity);
		}
		else if (guiState == "guiStateNextLevel5") {
			infoPanel.UpdateGUIItems(guiOpacity, backRectOpacity, goButtonOpacity);
		}
		else if (infoPanelVisible == true && elapsedTime < infoPanelTransTime) {
			// fading in
			percentVisible = elapsedTime / infoPanelTransTime;			
			infoPanel.UpdateGUIItems(percentVisible, backRectOpacity, goButtonOpacity);
		}
		else if (infoPanelVisible == true) {
			// fully visible
			if (guiState == "guiStateEnterApp1")
				infoPanel.UpdateGUIItems(1f, backRectOpacity, 1f, 1f);
			else if (guiState == "guiStateEnterApp2")
				infoPanel.UpdateGUIItems(1f, backRectOpacity, 1f, (guiOpacity > 0.0001f) ? guiOpacity : 0.0001f);
			else if (guiState == "guiStateEnterApp3")
				infoPanel.UpdateGUIItems(1f, backRectOpacity, guiOpacity, 0);
			else
				infoPanel.UpdateGUIItems(1f, backRectOpacity, goButtonOpacity);
		}
		else if (elapsedTime < infoPanelTransTime) {
			// fading out		
			percentVisible = 1f - elapsedTime / infoPanelTransTime;
			infoPanel.UpdateGUIItems(percentVisible, backRectOpacity, goButtonOpacity);
		}
		else if (guiState == "guiStateFinalScreen5") {
			// after last level: background of panel is retained during screen swap
			infoPanel.UpdateGUIItems(0f, backRectOpacity, 0f);
		}
		else if (guiState == "guiStateFinalScreen9") {
			// ... and during final fadeout
			infoPanel.UpdateGUIItems(0f, backRectOpacity, 0f);
		}
		else if (elapsedTime >= infoPanelTransTime) {
			// shut it off
			infoPanel.UpdateGUIItems(0f, 0f, 0f);			
		}
		
		
		///////////////////////////////////////
		// NOTE: STILL NEEDS DEBUG PANELS!!!
		///////////////////////////////////////

		
		
	}


	//======================================
	//
	//	OnGUI() - DRAW THE USER INTERFACE
	//
	//	This function is the top level
	//	draw routine for rendering the UI;
	//	it's called twice: once to do the
	//	layout, and once to draw the UI
	//
	//======================================
	
	void OnGUI()
	{	
		// initially clear the gameplay input rects
		// if the rects are active, these values will be filled in during gameplayDisplay.Draw call below
		//inputControls.SetRectLeftButton(new Rect(0f, 0f, 0f, 0f)); 
		//inputControls.SetRectRightButton(new Rect(0f, 0f, 0f, 0f)); 
		//
		// -- this was MOVED TO UpdateGUIItems() to avoid double-clear
		//

		if (infoPanelVisible == false || Time.time - infoPanelTransStart < infoPanelTransTime) {
		
			float infoPanelOpacityComplement = 1f;
			float infoPanelElapsedTime = Time.time - infoPanelTransStart;
		
			if (infoPanelVisible == true && infoPanelElapsedTime < infoPanelTransTime) {
				// fading in
				infoPanelOpacityComplement = 1f - infoPanelElapsedTime / infoPanelTransTime;			
			}
			else if (infoPanelVisible == false && infoPanelElapsedTime < infoPanelTransTime) {
				// fading out		
				infoPanelOpacityComplement = infoPanelElapsedTime / infoPanelTransTime;
			}		
			
			// DRAW SOMETHING - info panel is either not showing, or is fading in or out

			switch (guiState) {
		
			//-----------------------
			// Overlay States
			//
			// entering, viewing and
			// leaving the main GUI
			//-----------------------

			case "guiStateEnteringOverlay":
			case "guiStateOverlay":
			case "guiStateLeavingOverlay":
				overlayPanel.Draw(guiOpacity * infoPanelOpacityComplement);
				break;
							
			//-----------------------
			// Gameplay States
			//
			// entering, viewing and
			// leaving the 3D world
			//-----------------------

			case "guiStateEnteringGameplay1":
				// no GUI during initial camera zoom
				break;
				
			case "guiStateEnteringGameplay2":
				// fade-in of position indicator backgrounds
				gameplayDisplay.Draw(0f, guiOpacity, 0f, 0f, 0f);
				break;
				
			case "guiStateEnteringGameplay2a":
				// fade-in of position indicators
				gameplayDisplay.Draw(0f, 1f, guiOpacity, 0f, 0f);
				break;
				
			case "guiStateEnteringGameplay3":
				// zoom the indicators to screen edges
				gameplayDisplay.Draw(0f, 1f, 1f, guiOpacity, 0f);
				break;
				
			case "guiStateEnteringGameplay4":
				// fade-in of movement controls
				gameplayDisplay.Draw(guiOpacity, 1f, 1f, 1f, 0f);
				break;
				
			case "guiStateEnteringGameplay5":
				// fade-in of status displays
				gameplayDisplay.Draw(1f, 1f, 1f, 1f, guiOpacity);
				break;
				
			case "guiStateGameplay":
				// ongoing game-play state
				gameplayDisplay.Draw(1f, 1f, 1f, 1f, 1f);
				break;
				
			case "guiStateLeavingGameplay":
				// fade-out of game-play controls
				gameplayDisplay.Draw(guiOpacity, guiOpacity, guiOpacity, 1f, guiOpacity);
				break;
			
			case "guiStateLeavingFeeding":
				// fade-out of feeding display
				feedingDisplay.Draw(guiOpacity, 1f, guiOpacity, guiOpacity);
				break;
			
			//------------------------------
			// Feeding States
			//
			// entering, viewing and
			// leaving the feeding display
			//------------------------------

			case "guiStateFeeding1":
				// fade-out of game-play controls
				gameplayDisplay.Draw(guiOpacity, guiOpacity, guiOpacity, 1f, guiOpacity);
				break;

			case "guiStateFeeding2":
				// no GUI display during attack on deer
				break;

			case "guiStateFeeding3":
				// fade-in of feeding display main panel
				feedingDisplay.Draw(guiOpacity, 0f, 0f, 0f);
				break;

			case "guiStateFeeding3a":
				// brief pause for rolling score
				feedingDisplay.Draw(1f, guiOpacity, 0f, 0f);
				break;

			case "guiStateFeeding4":
				// fade-in of level display
				feedingDisplay.Draw(1f, 1f, guiOpacity, 0f);
				break;

			case "guiStateFeeding4a":
				// brief pause
				feedingDisplay.Draw(1f, 1f, 1f, 0f);
				break;

			case "guiStateFeeding5":
				// fade-in of feeding display 'ok' button
				feedingDisplay.Draw(1f, 1f, 1f, guiOpacity);
				break;

			case "guiStateFeeding6":
				// ongoing view of feeding display
				feedingDisplay.Draw(1f, 1f, 1f, 1f);
				break;

			case "guiStateFeeding7":
				// fade-out of feeding display
				feedingDisplay.Draw(guiOpacity, 1f, guiOpacity, guiOpacity);
				break;

			case "guiStateFeeding8":
				// fade-in of position indicator backgrounds
				gameplayDisplay.Draw(0f, guiOpacity, 0f, 0f, 0f);
				break;
				
			case "guiStateFeeding8a":
				// fade-in of position indicators
				gameplayDisplay.Draw(0f, 1f, guiOpacity, 0f, 0f);
				break;
				
			case "guiStateFeeding9":
				// zoom the indicators to screen edges
				gameplayDisplay.Draw(0f, 1f, 1f, guiOpacity, 0f);
				break;
				
			case "guiStateFeeding10":
				// fade-in of movement controls
				gameplayDisplay.Draw(guiOpacity, 1f, 1f, 1f, 0f);
				break;
				
			case "guiStateFeeding11":
				// fade-in of status indicators
				gameplayDisplay.Draw(1f, 1f, 1f, 1f, guiOpacity);
				break;
				
			//------------------------------
			// Next Level
			//
			// progress to next level
			//------------------------------

			case "guiStateNextLevel1":
				// fade-out of feeding display
				feedingDisplay.Draw(guiOpacity, 1f, guiOpacity, guiOpacity);
				break;

			//------------------------------
			// Puma Done
			//
			// starvation or
			// collision with car
			// or reaching full health
			//------------------------------

			case "guiStatePumaDone1":
				// fade-out of game-play controls
				gameplayDisplay.Draw(guiOpacity, guiOpacity, guiOpacity, 1f, guiOpacity);
				break;

			case "guiStatePumaDone2":
				// pause for a bit of space
				break;

			case "guiStatePumaDone3":
				// fade-in of puma done display main panel
				if (levelManager.CheckCarCollision() == true)
					pumaDoneDisplay.Draw(guiOpacity, "threatTypeVehicle", 0f);
				else if (levelManager.CheckStarvation() == true)
					pumaDoneDisplay.Draw(guiOpacity, "threatTypeStarvation", 0f);	
				break;

			case "guiStatePumaDone4":
				// brief pause
				if (levelManager.CheckCarCollision() == true)
					pumaDoneDisplay.Draw(1f, "threatTypeVehicle", 0f);
				else if (levelManager.CheckStarvation() == true)
					pumaDoneDisplay.Draw(1f, "threatTypeStarvation", 0f);
				break;

			case "guiStatePumaDone5":
				// fade-in of puma done display 'ok' button
				if (levelManager.CheckCarCollision() == true)
					pumaDoneDisplay.Draw(1f * infoPanelOpacityComplement, "threatTypeVehicle", guiOpacity * infoPanelOpacityComplement);
				else if (levelManager.CheckStarvation() == true)
					pumaDoneDisplay.Draw(1f * infoPanelOpacityComplement, "threatTypeStarvation", guiOpacity * infoPanelOpacityComplement);
				break;

			case "guiStatePumaDone6":
				// ongoing view of puma done display
				if (levelManager.CheckCarCollision() == true)
					pumaDoneDisplay.Draw(1f * infoPanelOpacityComplement, "threatTypeVehicle", 1f * infoPanelOpacityComplement);
				else if (levelManager.CheckStarvation() == true)
					pumaDoneDisplay.Draw(1f * infoPanelOpacityComplement, "threatTypeStarvation", 1f * infoPanelOpacityComplement);
				break;

			case "guiStateLeavingPumaDone":
				// fade-out of puma done display
				if (levelManager.CheckCarCollision() == true)
					pumaDoneDisplay.Draw(guiOpacity, "threatTypeVehicle", guiOpacity);
				else if (levelManager.CheckStarvation() == true)
					pumaDoneDisplay.Draw(guiOpacity, "threatTypeStarvation", guiOpacity);
				break;
			}
		}

		//------------------------------
		// Draw Info Panel
		//------------------------------
				
		float elapsedTime = Time.time - infoPanelTransStart;
		float percentVisible = 0f;
		float backRectOpacity = 0f;
		float goButtonOpacity = 1f;

		if (guiState == "guiStateNextLevel3")
			backRectOpacity = guiOpacity;
		if (guiState == "guiStateNextLevel4" || guiState == "guiStateNextLevel5" || guiState == "guiStateNextLevel6" || guiState == "guiStateFinalScreen1")
			backRectOpacity = 1f;
		else if (guiState == "guiStateNextLevel7")
			backRectOpacity = guiOpacity;
		else if (guiState == "guiStateFinalScreen2")
			backRectOpacity = 0.5f + (guiOpacity * 0.5f);
		else if (guiState == "guiStateFinalScreen3" || guiState == "guiStateFinalScreen4" || guiState == "guiStateFinalScreen5" || guiState == "guiStateFinalScreen6" || guiState == "guiStateFinalScreen7" || guiState == "guiStateFinalScreen8")
			backRectOpacity = 0.5f;
		else if (guiState == "guiStateFinalScreen9")
			backRectOpacity = guiOpacity * 0.5f;
			
			
		if (guiState == "guiStateNextLevel3" || guiState == "guiStateNextLevel4" || guiState == "guiStateNextLevel5" || guiState == "guiStateNextLevel6" || guiState == "guiStateNextLevel7" || guiState == "guiStateFinalScreen1" || guiState == "guiStateFinalScreen2" || guiState == "guiStateFinalScreen6")  
			goButtonOpacity = 0f;
		else if (guiState == "guiStateNextLevel8" || guiState == "guiStateFinalScreen3" || guiState == "guiStateFinalScreen7")
			goButtonOpacity = guiOpacity;	

			
		if (guiState == "guiStateNextLevel3" || guiState == "guiStateNextLevel4") {
			infoPanel.Draw(0f, backRectOpacity, goButtonOpacity);
		}
		else if (guiState == "guiStateNextLevel5") {
			infoPanel.Draw(guiOpacity, backRectOpacity, goButtonOpacity);
		}
		else if (infoPanelVisible == true && elapsedTime < infoPanelTransTime) {
			// fading in
			percentVisible = elapsedTime / infoPanelTransTime;			
			infoPanel.Draw(percentVisible, backRectOpacity, goButtonOpacity);
		}
		else if (infoPanelVisible == true) {
			// fully visible
			if (guiState == "guiStateEnterApp1")
				infoPanel.Draw(1f, backRectOpacity, goButtonOpacity, 1f);
			else if (guiState == "guiStateEnterApp2")
				infoPanel.Draw(1f, backRectOpacity, goButtonOpacity, guiOpacity);
			else
				infoPanel.Draw(1f, backRectOpacity, goButtonOpacity);
		}
		else if (elapsedTime < infoPanelTransTime) {
			// fading out		
			percentVisible = 1f - elapsedTime / infoPanelTransTime;
			infoPanel.Draw(percentVisible, backRectOpacity, goButtonOpacity);
		}
		else if (guiState == "guiStateFinalScreen5") {
			// after last level: background of panel is retained during screen swap
			infoPanel.Draw(0f, backRectOpacity, 0f);
		}
		else if (guiState == "guiStateFinalScreen9") {
			// ... and during final fadeout
			infoPanel.Draw(0f, backRectOpacity, 0f);
		}
		else if (elapsedTime >= infoPanelTransTime) {
			// shut it off
			infoPanel.Draw(0f, 0f, 0f);			
		}
		

		//------------------------------
		// Frame Rate Display
		//------------------------------

		if (displayFrameRate == true && levelManager != null) {
			GUI.color = Color.white;

			GUIStyle style = new GUIStyle();
			style.alignment = TextAnchor.MiddleCenter;

			float fontRef = Screen.height * 0.1f;
			style.fontSize = (int)(fontRef * 0.22f);
			style.normal.textColor =  new Color(1f, 1f, 1f, 1f);
			style.fontStyle = FontStyle.Bold;
			
			float backBoxWidth = Screen.height * 0.28f;
			float backBoxHeight = Screen.height * 0.036f;

			int msec = levelManager.frameAverageDuration;
			GUI.Box(new Rect(Screen.width * 0.24f - backBoxWidth/2, 0, backBoxWidth, backBoxHeight), "");		
			GUI.Button(new Rect(Screen.width * 0.24f - backBoxWidth/2, 0, backBoxWidth, backBoxHeight), "Avg Frame time: " + msec.ToString(), style);		

			GUI.Box(new Rect(Screen.width * 0.50f - backBoxWidth/2, 0, backBoxWidth, backBoxHeight), "");		
			GUI.Button(new Rect(Screen.width * 0.50f - backBoxWidth/2, 0, backBoxWidth, backBoxHeight), "Screen Res: " + Screen.width.ToString() + "x" + Screen.height.ToString(), style);				
			
			int averageFrameRate = (levelManager.frameAverageDuration == 0) ? 0 : (int)(1000 / levelManager.frameAverageDuration);
			GUI.Box(new Rect(Screen.width * 0.76f - backBoxWidth/2, 0, backBoxWidth, backBoxHeight), "");		
			GUI.Button(new Rect(Screen.width * 0.76f - backBoxWidth/2, 0, backBoxWidth, backBoxHeight), "Avg Frame rate: " + averageFrameRate.ToString(), style);		

			GUI.Box(new Rect(Screen.width * 0.24f - backBoxWidth/2, Screen.height - backBoxHeight*2, backBoxWidth, backBoxHeight), "");		
			GUI.Button(new Rect(Screen.width * 0.24f - backBoxWidth/2, Screen.height - backBoxHeight*2, backBoxWidth, backBoxHeight), levelManager.displayVar1, style);		
			GUI.Box(new Rect(Screen.width * 0.50f - backBoxWidth/2, Screen.height - backBoxHeight*2, backBoxWidth, backBoxHeight), "");		
			GUI.Button(new Rect(Screen.width * 0.50f - backBoxWidth/2, Screen.height - backBoxHeight*2, backBoxWidth, backBoxHeight), levelManager.displayVar3, style);				
			GUI.Box(new Rect(Screen.width * 0.76f - backBoxWidth/2, Screen.height - backBoxHeight*2, backBoxWidth, backBoxHeight), "");		
			GUI.Button(new Rect(Screen.width * 0.76f - backBoxWidth/2, Screen.height - backBoxHeight*2, backBoxWidth, backBoxHeight), levelManager.displayVar5, style);		

			Vector3 mousePos = Input.mousePosition;
			GUI.Box(new Rect(Screen.width * 0.24f - backBoxWidth/2, Screen.height - backBoxHeight, backBoxWidth, backBoxHeight), "");		
			GUI.Button(new Rect(Screen.width * 0.24f - backBoxWidth/2, Screen.height - backBoxHeight, backBoxWidth, backBoxHeight), levelManager.displayVar2, style);		
			GUI.Box(new Rect(Screen.width * 0.50f - backBoxWidth/2, Screen.height -backBoxHeight, backBoxWidth, backBoxHeight), "");		
			GUI.Button(new Rect(Screen.width * 0.50f - backBoxWidth/2, Screen.height - backBoxHeight, backBoxWidth, backBoxHeight), levelManager.displayVar4, style);				
			GUI.Box(new Rect(Screen.width * 0.76f - backBoxWidth/2, Screen.height - backBoxHeight, backBoxWidth, backBoxHeight), "");		
			GUI.Button(new Rect(Screen.width * 0.76f - backBoxWidth/2, Screen.height - backBoxHeight, backBoxWidth, backBoxHeight), levelManager.displayVar6, style);		
		}		
	}	
 
	//=====================================
	//=====================================
	//	UTILITY AND CONVENIENCE FUNCTIONS
	//=====================================
	//=====================================

	public void SetGuiState(string newState) 
	{	
		guiState = newState;
		guiStateStartTime = Time.time;
		Update();
		//Debug.Log("NEW GUI STATE: " + newState);
	}

	public void OpenInfoPanel(int newScreenNum, bool slowFlag = false)
	{
		if (newScreenNum >= 0)
			infoPanel.SetCurrentScreen(newScreenNum);
		infoPanelTransTime = slowFlag ? 2.4f : 0.4f;
		infoPanelTransStart = Time.time;
		infoPanelVisible = true;
	}
 
 	public void CloseInfoPanel(bool slowFlag = false)
	{
		infoPanelTransTime = slowFlag ? 0.6f : 0.3f;
		infoPanelTransStart = Time.time;
		infoPanelVisible = false;
	}
	
	//////////////////
	//////////////////

	bool SelectedPumaIsFullHealth()
	{	
		if (scoringSystem.GetPumaHealth(selectedPuma) >= 1f)
			return true;
			
		return false;
	}
	
		
	bool PumaIsSelectable(int pumaNum)
	{	
		if (scoringSystem.GetPumaHealth(pumaNum) <= 0f)
			return false;
			
		return true;
	}
	
	
	void IncrementPuma()
	{
		for (int i = selectedPuma+1; i < 6; i++) {
			if (PumaIsSelectable(i)) {
				selectedPuma = i;
				levelManager.SetSelectedPuma(selectedPuma);
				return;
			}
		}
	}
	
	void DecrementPuma()
	{
		for (int i = selectedPuma-1; i >= 0; i--) {
			if (PumaIsSelectable(i)) {
				selectedPuma = i;
				levelManager.SetSelectedPuma(selectedPuma);
				return;
			}
		}
		if (selectedPuma == -1) {
			selectedPuma = 5;
			levelManager.SetSelectedPuma(selectedPuma);
		}
	}
	
	//////////////////
	//////////////////

	public float GetPumaSpeed(int pumaNum)
	{
		return (speedArray[pumaNum]);
	}

	public float GetPumaStealth(int pumaNum)
	{
		return (stealthArray[pumaNum]);
	}

	public float GetPumaEndurance(int pumaNum)
	{
		return (enduranceArray[pumaNum]);
	}

	public float GetPumaPower(int pumaNum)
	{
		return (powerArray[pumaNum]);
	}

	//////////////////
	//////////////////

	float GetSelectedPumaSpeed()
	{
		return (selectedPuma == -1) ?  0f : speedArray[selectedPuma];
	}

	float GetSelectedPumaStealth()
	{
		return (selectedPuma == -1) ?  0f : stealthArray[selectedPuma];
	}

	float GetSelectedPumaEndurance()
	{
		return (selectedPuma == -1) ?  0f : enduranceArray[selectedPuma];
	}

	float GetSelectedPumaPower()
	{
		return (selectedPuma == -1) ?  0f : powerArray[selectedPuma];
	}

	//////////////////
	//////////////////

	void DebounceKeyboardInput()
	{
		// filter out key repeats
				
		spacePressed = false;
		tabPressed = false;
		leftShiftPressed = false;
		rightShiftPressed = false;
		leftArrowPressed = false;
		rightArrowPressed = false;
		
		bool leftArrowState = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.J);
		bool rightArrowState = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.L);


		if (debounceSpace == true) {
			if (Input.GetKey(KeyCode.Space) == false)
				debounceSpace = false;
		}
		else if (Input.GetKey(KeyCode.Space) == true) {
			spacePressed = true;
			debounceSpace = true;
		}
	
		if (debounceTab == true) {
			if (Input.GetKey(KeyCode.Tab) == false)
				debounceTab = false;
		}
		else if (Input.GetKey(KeyCode.Tab) == true) {
			tabPressed = true;
			debounceTab = true;
		}
	
		if (debounceLeftShift == true) {
			if (Input.GetKey(KeyCode.LeftShift) == false)
				debounceLeftShift = false;
		}
		else if (Input.GetKey(KeyCode.LeftShift) == true) {
			leftShiftPressed = true;
			debounceLeftShift = true;
		}
	
		if (debounceRightShift == true) {
			if (Input.GetKey(KeyCode.RightShift) == false)
				debounceRightShift = false;
		}
		else if (Input.GetKey(KeyCode.RightShift) == true) {
			rightShiftPressed = true;
			debounceRightShift = true;
		}
		
		if (debounceLeftArrow == true) {
			if (leftArrowState == false)
				debounceLeftArrow = false;
		}
		else if (leftArrowState == true) {
			leftArrowPressed = true;
			debounceLeftArrow = true;
		}
	
		if (debounceRightArrow == true) {
			if (rightArrowState == false)
				debounceRightArrow = false;
		}
		else if (rightArrowState == true) {
			rightArrowPressed = true;
			debounceRightArrow = true;
		}
	}
		
}
