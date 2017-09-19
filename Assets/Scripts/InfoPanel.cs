using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// InfoPanel
/// Draw the Info panel that provides info on topics

public class InfoPanel : MonoBehaviour
{
	//===================================
	//===================================
	//		MODULE VARIABLES
	//===================================
	//===================================

	private bool USE_NEW_GUI = true;	
	private bool initComplete = false;
	private float lastSeenScreenWidth;
	private float lastSeenScreenHeight;
	private bool newLevelFlag = true;
	private int currentLevel = 0;
	private bool backgroundIsLocked = false;
	
	private Rect overlayRect;
	private int currentScreen;
	private int soundEnable;
	private float soundVolume;
	private float pawRightFlag;
	
	// button styling
	private GUIStyle buttonStyle;	
	private GUIStyle buttonDownStyle;	
	private GUIStyle buttonDisabledStyle;	
	private GUIStyle bigButtonStyle;	
	private GUIStyle bigButtonDisabledStyle;
	private GUIStyle swapButtonStyle;

	// slider styling
	private GUISkin customSkin;
	private GUIStyle sliderBarStyle;
	private GUIStyle sliderThumbStyle;	

	// textures based on bitmap files
	private Texture2D logoTexture;
	private Texture2D backgroundTexture;
	private Texture2D pumaIconTexture;
	private Texture2D pumaIconShadowTexture;
	private Texture2D greenCheckTexture;	
//	private Texture2D radioButtonTexture;
	private Texture2D radioSelectTexture;
	private Texture2D arrowTrayTexture;
	private Texture2D arrowTrayTopTexture;
	private Texture2D infoArrowDownTexture;
	private Texture2D infoArrowUpTexture;
	private Texture2D swapButtonTexture;
	private Texture2D swapButtonHoverTexture;
	private Texture2D sliderBarTexture;
	private Texture2D sliderThumbTexture;
	private Texture2D buckHeadTexture;
	private Texture2D doeHeadTexture;
	private Texture2D fawnHeadTexture;
	private Texture2D buckStandTexture;
	private Texture2D doeStandTexture;
	private Texture2D fawnStandTexture;
	private Texture2D headshot1Texture;
	private Texture2D headshot2Texture;
	private Texture2D headshot3Texture;
	private Texture2D headshot4Texture;
	private Texture2D headshot5Texture;
	private Texture2D headshot6Texture;
	private Texture2D closeup1Texture;
	private Texture2D closeup2Texture;
	private Texture2D closeup3Texture;
	private Texture2D closeup4Texture;
	private Texture2D closeup5Texture;
	private Texture2D closeup6Texture;
	private Texture2D closeupBackgroundTexture;
	private Texture2D pumaCrossbonesDarkRedTexture;
	private Texture2D blackArrowTexture;
	private Texture2D blackArrowShortTexture;
	private Texture2D greenArrowTexture;
	private Texture2D greenHeartTexture;
	private Texture2D redXTexture;
	private Texture2D pumaStealthTexture;
	private Texture2D pumaRunTexture;
	private Texture2D pumaAttackTexture;
	private Texture2D pumaJumpTexture;
	private Texture2D pumaPreyTexture;
	private Texture2D predationArrowsTexture;
	private Texture2D predationCirclesTexture;
	private Texture2D huntLongTexture;
	private Texture2D huntShortTexture;
	private Texture2D crossingScreenshotTexture; 
	private Texture2D crossingBadAngleTexture; 
	private Texture2D crossingGoodAngleTexture;
	private Texture2D physicalCharacteristicsTexture;
	private Texture2D ecologyTexture;
	private Texture2D pumasAndPredationTexture;
	private Texture2D pumaKittenTexture;
	private Texture2D survivalThreatsTexture;
	private Sprite donateButtonSprite;

	private Texture2D iconFacebookTexture; 
	private Texture2D iconTwitterTexture; 
	private Texture2D iconGoogleTexture; 
	private Texture2D iconPinterestTexture; 
	private Texture2D iconYouTubeTexture; 
	private Texture2D iconLinkedInTexture; 
	private Texture2D logoFelidaeTexture; 
	private Texture2D logoBappTexture; 
	private Texture2D levelImage1Texture; 
	private Texture2D levelImage2Texture; 
	private Texture2D levelImage3Texture; 
	private Texture2D levelImage4Texture; 
	private Texture2D levelImage5Texture; 
	private Texture2D levelImage6Texture; 
	private Texture2D levelImage6bTexture; 
	
	// external modules
	private GuiManager guiManager;
	private GuiComponents guiComponents;
	private GuiUtils guiUtils;
	private LevelManager levelManager;
	//private InputControls inputControls;
	private ScoringSystem scoringSystem;
	

	//===================================
	//===================================
	//		INITIALIZATION
	//===================================
	//===================================

	void Start() 
	{	
		// connect to external modules
		guiManager = GetComponent<GuiManager>();
		guiComponents = GetComponent<GuiComponents>();
		guiUtils = GetComponent<GuiUtils>();
		levelManager = GetComponent<LevelManager>();
		//inputControls = GetComponent<InputControls>();
		scoringSystem = GetComponent<ScoringSystem>();
		
		// texture references from GuiManager
		logoTexture = guiManager.logoTexture;
		backgroundTexture = guiManager.backgroundTexture;
		pumaIconTexture = guiManager.pumaIconTexture;
		pumaIconShadowTexture = guiManager.pumaIconShadowTexture;
		greenCheckTexture = guiManager.greenCheckTexture;
//		radioButtonTexture = guiManager.radioButtonTexture;
//		radioSelectTexture = guiManager.radioSelectTexture;
//		arrowTrayTexture = guiManager.arrowTrayTexture;
//		arrowTrayTopTexture = guiManager.arrowTrayTopTexture;
		infoArrowDownTexture = guiManager.infoArrowDownTexture;
		infoArrowUpTexture = guiManager.infoArrowUpTexture;
//		swapButtonTexture = guiManager.swapButtonTexture;
//		swapButtonHoverTexture = guiManager.swapButtonHoverTexture;
		sliderBarTexture = guiManager.sliderBarTexture;
		sliderThumbTexture = guiManager.sliderThumbTexture;
		buckHeadTexture = guiManager.buckHeadTexture;
		doeHeadTexture = guiManager.doeHeadTexture;
		fawnHeadTexture = guiManager.fawnHeadTexture;
		buckStandTexture = guiManager.buckStandTexture;
		doeStandTexture = guiManager.doeStandTexture;
		fawnStandTexture = guiManager.fawnStandTexture;
		headshot1Texture = guiManager.headshot1Texture;
		headshot2Texture = guiManager.headshot2Texture;
		headshot3Texture = guiManager.headshot3Texture;
		headshot4Texture = guiManager.headshot4Texture;
		headshot5Texture = guiManager.headshot5Texture;
		headshot6Texture = guiManager.headshot6Texture;
		closeup1Texture = guiManager.closeup1Texture;
		closeup2Texture = guiManager.closeup2Texture;
		closeup3Texture = guiManager.closeup3Texture;
		closeup4Texture = guiManager.closeup4Texture;
		closeup5Texture = guiManager.closeup5Texture;
		closeup6Texture = guiManager.closeup6Texture;
		closeupBackgroundTexture = guiManager.closeupBackgroundTexture;
		pumaCrossbonesDarkRedTexture = guiManager.pumaCrossbonesDarkRedTexture;		
		blackArrowTexture = guiManager.blackArrowTexture;		
		blackArrowShortTexture = guiManager.blackArrowShortTexture;		
		greenArrowTexture = guiManager.greenArrowTexture;		
		greenHeartTexture = guiManager.greenHeartTexture;		
		redXTexture = guiManager.redXTexture;		
		pumaStealthTexture = guiManager.pumaStealthTexture;		
		pumaRunTexture = guiManager.pumaRunTexture;		
		pumaAttackTexture = guiManager.pumaAttackTexture;		
		pumaJumpTexture = guiManager.pumaJumpTexture;		
		pumaPreyTexture = guiManager.pumaPreyTexture;		
		predationArrowsTexture = guiManager.predationArrowsTexture;		
		predationCirclesTexture = guiManager.predationCirclesTexture;		
		huntLongTexture = guiManager.huntLongTexture;		
		huntShortTexture = guiManager.huntShortTexture;		
		crossingScreenshotTexture = guiManager.crossingScreenshotTexture;		
		crossingBadAngleTexture = guiManager.crossingBadAngleTexture;		
		crossingGoodAngleTexture = guiManager.crossingGoodAngleTexture;	
		physicalCharacteristicsTexture = guiManager.physicalCharacteristicsTexture;
		ecologyTexture = guiManager.ecologyTexture;
		pumasAndPredationTexture = guiManager.pumasAndPredationTexture;
		pumaKittenTexture = guiManager.pumaKittenTexture;
		survivalThreatsTexture = guiManager.survivalThreatsTexture;
		donateButtonSprite = guiManager.donateButtonSprite;

		iconFacebookTexture = guiManager.iconFacebookTexture;
		iconTwitterTexture = guiManager.iconTwitterTexture;
		iconGoogleTexture = guiManager.iconGoogleTexture;
		iconPinterestTexture = guiManager.iconPinterestTexture;
		iconYouTubeTexture = guiManager.iconYouTubeTexture;
		iconLinkedInTexture = guiManager.iconLinkedInTexture;
		logoFelidaeTexture = guiManager.logoFelidaeTexture;
		logoBappTexture = guiManager.logoBappTexture;
		
		levelImage1Texture = guiManager.levelImage1Texture;
		levelImage2Texture = guiManager.levelImage2Texture;
		levelImage3Texture = guiManager.levelImage3Texture;
		levelImage4Texture = guiManager.levelImage4Texture;
		levelImage5Texture = guiManager.levelImage5Texture;
		levelImage6Texture = guiManager.levelImage6Texture;
		levelImage6bTexture = guiManager.levelImage6bTexture;
		
		// custom button styling
		buttonStyle = new GUIStyle();
		buttonStyle.normal.textColor = new Color(0.99f, 0.7f, 0.2f, 1f);
		buttonStyle.hover.textColor = new Color(0.99f, 0.8f, 0.4f, 1f);
		buttonStyle.alignment = TextAnchor.MiddleCenter;
		buttonDownStyle = new GUIStyle();
		buttonDownStyle.normal.textColor = new Color(0.99f, 0.88f, 0.6f, 1f);
		buttonDownStyle.hover.textColor = new Color(0.99f, 0.88f, 0.6f, 1f);
		buttonDownStyle.alignment = TextAnchor.MiddleCenter;
		buttonDisabledStyle = new GUIStyle();
		buttonDisabledStyle.normal.textColor = new Color(0.3f, 0.3f, 0.3f, 1f);
		buttonDisabledStyle.hover.textColor = new Color(0.3f, 0.3f, 0.3f, 1f);
		buttonDisabledStyle.alignment = TextAnchor.MiddleCenter;
		bigButtonStyle = new GUIStyle();
		bigButtonStyle.normal.textColor = new Color(0.99f, 0.7f, 0.2f, 1f);
		bigButtonStyle.hover.textColor = new Color(0.99f, 0.8f, 0.4f, 1f);
		bigButtonStyle.alignment = TextAnchor.MiddleCenter;
		bigButtonDisabledStyle = new GUIStyle();
		bigButtonDisabledStyle.normal.textColor = new Color(0.4f, 0.4f, 0.4f, 1f);
		bigButtonDisabledStyle.hover.textColor = new Color(0.4f, 0.4f, 0.4f, 1f);
		bigButtonDisabledStyle.alignment = TextAnchor.MiddleCenter;
		swapButtonStyle = new GUIStyle();
		swapButtonStyle.normal.textColor = Color.white;
		swapButtonStyle.normal.background = swapButtonTexture;
		swapButtonStyle.hover.textColor = Color.white;
		swapButtonStyle.hover.background = swapButtonHoverTexture;
		swapButtonStyle.alignment = TextAnchor.MiddleCenter;

		// custom slider styling
		sliderBarStyle = new GUIStyle();
		sliderThumbStyle = new GUIStyle();
		sliderThumbStyle.normal.background = sliderThumbTexture;
		sliderThumbStyle.padding = new RectOffset(10,10,10,10);
		customSkin = (GUISkin)ScriptableObject.CreateInstance("GUISkin");
		customSkin.horizontalSlider = sliderBarStyle;
		customSkin.horizontalSliderThumb = sliderThumbStyle;	

		// additional initialization
		currentScreen = 0;
		soundEnable = 1;
		soundVolume = 0.5f;
		pawRightFlag = 1;
		
		// create and position GUI elements
		CreateGUIItems();
		PositionGUIItems();
		lastSeenScreenWidth = Screen.width;
		lastSeenScreenHeight = Screen.height;
	}
	

	//===========================
	//===========================
	//	  GUI ELEMENTS
	//===========================
	//===========================
	
	public GameObject infoPanelBackRect;
	public GameObject infoPanelMainPanel;
	public GameObject infoPanelOkButton;

	// prefab gui components
	public GameObject uiPanel;
	public GameObject uiSubPanel;
	public GameObject uiRect;
	public GameObject uiText;
	public GameObject uiRawImage;
	public GameObject uiButton;
	public GameObject uiButtonSeeThru;
	public GameObject uiImageButton;
		
	// programatically created items 
	private GameObject donatePanel;
	private GameObject backRect;
	private GameObject panelBackground;
	private GameObject titleBackground;
	private GameObject titleImage;
	private GameObject buttonTrayBackground;
	private GameObject selectedButtonBackground;
	private GameObject biologyButton;
	private GameObject predationButton;
	private GameObject ecologyButton;
	private GameObject survivalButton;
	private GameObject donateButton;
	private GameObject backButton;
	private GameObject playButton;
	private GameObject goButton;
	private GameObject nextLevelButtonBackground;	
	private GameObject nextLevelButton;
	private GameObject playAgainButtonBackground;
	private GameObject playAgainButton;
	private GameObject leftLabelBackground;
	private GameObject leftLabelText;
	private GameObject rightLabelBackground;
	private GameObject rightLabelText;
	private GameObject infoBackgroundOuterLeft;
	private GameObject infoBackgroundOuterRight;
	private GameObject infoBackgroundOuterFull;
	private GameObject infoBackgroundInnerLeft;
	private GameObject infoBackgroundInnerRight;
	private GameObject infoBackgroundInnerFull;
	
	
	void CreateGUIItems()
	{
		// first set all enables to 'off'
		infoPanelBackRect.SetActive(false);
		infoPanelMainPanel.SetActive(false);
		infoPanelOkButton.SetActive(false);

		// back rect
		backRect = (GameObject)Instantiate(uiRect);
		backRect.GetComponent<RectTransform>().SetParent(infoPanelBackRect.GetComponent<RectTransform>(), false);
		guiUtils.SetItemOffsets(backRect, -40f, -40f, Screen.width + 80f, Screen.height + 80f);
		backRect.GetComponent<Image>().color = new Color(0.1f, 0.1f, 0.1f, 1f);

		// panel background
		panelBackground = (GameObject)Instantiate(uiPanel);
		panelBackground.GetComponent<RectTransform>().SetParent(infoPanelMainPanel.GetComponent<RectTransform>(), false);

		// graphical logo
		titleBackground = (GameObject)Instantiate(uiPanel);
		titleBackground.GetComponent<RectTransform>().SetParent(infoPanelMainPanel.GetComponent<RectTransform>(), false);
		titleBackground.GetComponent<Image>().color = new Color(0f, 0f, 0f, 120f/255f);

		titleImage = (GameObject)Instantiate(uiRawImage);
		titleImage.GetComponent<RectTransform>().SetParent(infoPanelMainPanel.GetComponent<RectTransform>(), false);
		titleImage.GetComponent<RawImage>().texture = logoTexture;


		// tray of buttons
		buttonTrayBackground = (GameObject)Instantiate(uiPanel);
		buttonTrayBackground.GetComponent<RectTransform>().SetParent(infoPanelMainPanel.GetComponent<RectTransform>(), false);

		selectedButtonBackground = (GameObject)Instantiate(uiRect);
		selectedButtonBackground.GetComponent<RectTransform>().SetParent(infoPanelMainPanel.GetComponent<RectTransform>(), false);
		selectedButtonBackground.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0.5f);

		biologyButton = (GameObject)Instantiate(uiButtonSeeThru);
		biologyButton.GetComponent<RectTransform>().SetParent(infoPanelMainPanel.GetComponent<RectTransform>(), false);
		biologyButton.GetComponent<RectTransform>().FindChild("Text").GetComponent<Text>().text = "Biology";
		biologyButton.GetComponent<Button>().onClick.AddListener( delegate {currentScreen = 0;} );

		predationButton = (GameObject)Instantiate(uiButtonSeeThru);
		predationButton.GetComponent<RectTransform>().SetParent(infoPanelMainPanel.GetComponent<RectTransform>(), false);
		predationButton.GetComponent<RectTransform>().FindChild("Text").GetComponent<Text>().text = "Predation";
		predationButton.GetComponent<Button>().onClick.AddListener( delegate {currentScreen = 1;} );

		ecologyButton = (GameObject)Instantiate(uiButtonSeeThru);
		ecologyButton.GetComponent<RectTransform>().SetParent(infoPanelMainPanel.GetComponent<RectTransform>(), false);
		ecologyButton.GetComponent<RectTransform>().FindChild("Text").GetComponent<Text>().text = "Ecology";
		ecologyButton.GetComponent<Button>().onClick.AddListener( delegate {currentScreen = 2;} );

		survivalButton = (GameObject)Instantiate(uiButtonSeeThru);
		survivalButton.GetComponent<RectTransform>().SetParent(infoPanelMainPanel.GetComponent<RectTransform>(), false);
		survivalButton.GetComponent<RectTransform>().FindChild("Text").GetComponent<Text>().text = "Survival";
		survivalButton.GetComponent<Button>().onClick.AddListener( delegate {currentScreen = 3;} );

		donateButton = (GameObject)Instantiate(uiButtonSeeThru);
		donateButton.GetComponent<RectTransform>().SetParent(infoPanelMainPanel.GetComponent<RectTransform>(), false);
		donateButton.GetComponent<RectTransform>().FindChild("Text").GetComponent<Text>().text = "Take Action";
		donateButton.GetComponent<Button>().onClick.AddListener( delegate {currentScreen = 4;} );

		backButton = (GameObject)Instantiate(uiButtonSeeThru);
		backButton.GetComponent<RectTransform>().SetParent(infoPanelMainPanel.GetComponent<RectTransform>(), false);
		backButton.GetComponent<RectTransform>().FindChild("Text").GetComponent<Text>().text = "<";
		backButton.GetComponent<RectTransform>().FindChild("Text").GetComponent<Text>().color = new Color(0.99f, 0.88f, 0.6f, 1f);
		backButton.GetComponent<Button>().onClick.AddListener( delegate {guiManager.CloseInfoPanel(true);} );

		playButton = (GameObject)Instantiate(uiButton);
		playButton.GetComponent<RectTransform>().SetParent(infoPanelMainPanel.GetComponent<RectTransform>(), false);
		playButton.GetComponent<RectTransform>().FindChild("Text").GetComponent<Text>().text = "PLAY";
		playButton.GetComponent<Button>().onClick.AddListener( delegate {
			guiManager.CloseInfoPanel(true);
			guiManager.SetGuiState("guiStateLeavingInfoPanel");
			levelManager.SetGameState("gameStateLeavingGui");
		} );
	
		goButton = (GameObject)Instantiate(uiButton);
		goButton.GetComponent<RectTransform>().SetParent(infoPanelMainPanel.GetComponent<RectTransform>(), false);
		goButton.GetComponent<RectTransform>().FindChild("Text").GetComponent<Text>().text = "GO";
		goButton.GetComponent<Button>().onClick.AddListener( delegate {
			guiManager.CloseInfoPanel(true);
			guiManager.SetGuiState("guiStateLeavingPumaDoneAlt");
			if (levelManager.CheckCarCollision() == true)
				levelManager.EndCarCollision();
			if (levelManager.CheckStarvation() == true)
				levelManager.EndStarvation();
			levelManager.SetGameState("gameStateLeavingGameplay");
		} );

		// upper left and upper right labels
		leftLabelBackground = (GameObject)Instantiate(uiPanel);
		leftLabelBackground.GetComponent<RectTransform>().SetParent(infoPanelMainPanel.GetComponent<RectTransform>(), false);
		leftLabelBackground.GetComponent<Image>().color = new Color(0f, 0f, 0f, 110f/255f);
				
		leftLabelText = (GameObject)Instantiate(uiText);
		leftLabelText.GetComponent<RectTransform>().SetParent(infoPanelMainPanel.GetComponent<RectTransform>(), false);
		leftLabelText.GetComponent<Text>().text = "In Game";
		leftLabelText.GetComponent<Text>().color =  new Color(0.816f * 1.1f, 0.537f * 1.1f, 0.18f * 1.1f, 1f);
		leftLabelText.GetComponent<Text>().fontStyle = FontStyle.BoldAndItalic;
				
		rightLabelBackground = (GameObject)Instantiate(uiPanel);
		rightLabelBackground.GetComponent<RectTransform>().SetParent(infoPanelMainPanel.GetComponent<RectTransform>(), false);
		rightLabelBackground.GetComponent<Image>().color = new Color(0f, 0f, 0f, 110f/255f);
				
		rightLabelText = (GameObject)Instantiate(uiText);
		rightLabelText.GetComponent<RectTransform>().SetParent(infoPanelMainPanel.GetComponent<RectTransform>(), false);
		rightLabelText.GetComponent<Text>().text = "Real World";
		rightLabelText.GetComponent<Text>().color = new Color(0.816f * 1.1f, 0.537f * 1.1f, 0.18f * 1.1f, 1f);
		rightLabelText.GetComponent<Text>().fontStyle = FontStyle.BoldAndItalic;
		
		// invisible donate panel to hold items
		donatePanel = (GameObject)Instantiate(uiSubPanel);
		donatePanel.GetComponent<RectTransform>().SetParent(infoPanelMainPanel.GetComponent<RectTransform>(), false);

		// info background panels 
		infoBackgroundOuterLeft = (GameObject)Instantiate(uiPanel);
		infoBackgroundOuterLeft.GetComponent<RectTransform>().SetParent(infoPanelMainPanel.GetComponent<RectTransform>(), false);

		infoBackgroundOuterRight = (GameObject)Instantiate(uiPanel);
		infoBackgroundOuterRight.GetComponent<RectTransform>().SetParent(infoPanelMainPanel.GetComponent<RectTransform>(), false);

		infoBackgroundOuterFull = (GameObject)Instantiate(uiPanel);
		infoBackgroundOuterFull.GetComponent<RectTransform>().SetParent(donatePanel.GetComponent<RectTransform>(), false);

		infoBackgroundInnerLeft = (GameObject)Instantiate(uiPanel);
		infoBackgroundInnerLeft.GetComponent<RectTransform>().SetParent(infoPanelMainPanel.GetComponent<RectTransform>(), false);
		infoBackgroundInnerLeft.GetComponent<Image>().color = new Color(0f, 0f, 0f, 51f/255f);

		infoBackgroundInnerRight = (GameObject)Instantiate(uiPanel);
		infoBackgroundInnerRight.GetComponent<RectTransform>().SetParent(infoPanelMainPanel.GetComponent<RectTransform>(), false);
		infoBackgroundInnerRight.GetComponent<Image>().color = new Color(0f, 0f, 0f, 51f/255f);

		infoBackgroundInnerFull = (GameObject)Instantiate(uiPanel);
		infoBackgroundInnerFull.GetComponent<RectTransform>().SetParent(donatePanel.GetComponent<RectTransform>(), false);
		infoBackgroundInnerFull.GetComponent<Image>().color = new Color(0f, 0f, 0f, 51f/255f);
		
		// content of panels
		CreateLevelItems();
		CreateBiologyItems();
		CreatePredationItems();
		CreateEcologyItems();
		CreateSurvivalItems();
		CreateDonateItems();
		
		// standalone 'next' buttons
		nextLevelButtonBackground = (GameObject)Instantiate(uiPanel);
		nextLevelButtonBackground.GetComponent<RectTransform>().SetParent(infoPanelOkButton.GetComponent<RectTransform>(), false);
				
		nextLevelButton = (GameObject)Instantiate(uiButton);
		nextLevelButton.GetComponent<RectTransform>().SetParent(infoPanelOkButton.GetComponent<RectTransform>(), false);
		nextLevelButton.GetComponent<RectTransform>().FindChild("Text").GetComponent<Text>().text = "GO";
		nextLevelButton.GetComponent<Button>().onClick.AddListener( delegate { guiManager.CloseInfoPanel(true); guiManager.SetGuiState("guiStateStartApp2"); } );

		playAgainButtonBackground = (GameObject)Instantiate(uiPanel);
		playAgainButtonBackground.GetComponent<RectTransform>().SetParent(infoPanelOkButton.GetComponent<RectTransform>(), false);
				
		playAgainButton = (GameObject)Instantiate(uiButton);
		playAgainButton.GetComponent<RectTransform>().SetParent(infoPanelOkButton.GetComponent<RectTransform>(), false);
		playAgainButton.GetComponent<RectTransform>().FindChild("Text").GetComponent<Text>().text = "Play Again";
		playAgainButton.GetComponent<Button>().onClick.AddListener( delegate { guiManager.CloseInfoPanel(true); guiManager.SetGuiState("guiStateStartApp2"); } );

		initComplete = true;
	}


	void PositionGUIItems()
	{
		if (initComplete == false)
			return;
	
		// use expanded overlay rect for info panel
		CalculateOverlayRect();
		float originalOverlayWidth = overlayRect.width; // used for logo width
		overlayRect.x -= overlayRect.width * 0.06f;
		overlayRect.width += overlayRect.width * 0.12f;
		
		// panel background
		guiUtils.SetItemOffsets(panelBackground, overlayRect.x + overlayRect.width*0.02f, overlayRect.y, overlayRect.width - overlayRect.width*0.04f, overlayRect.height);

		// graphical logo
		float logoY = overlayRect.y - overlayRect.height * 0.013f;
		float logoWidth = originalOverlayWidth * 0.28f;
		float logoHeight = logoTexture.height * (logoWidth / logoTexture.width);
		float logoX = overlayRect.x + overlayRect.width/2 - logoWidth/2;
		guiUtils.SetItemOffsets(titleBackground, logoX, logoY + logoHeight * 0.2f, logoWidth, logoHeight * 0.6f);
		guiUtils.SetItemOffsets(titleImage, logoX, logoY, logoWidth, logoHeight);
		
		// tray of buttons
		float buttonWidth = overlayRect.width * 0.11f;
		float buttonGap = overlayRect.width * 0.012f;
		float buttonMargin = overlayRect.x + overlayRect.width * 0.135f;
		float buttonY = overlayRect.y + overlayRect.height * 0.924f;
		float buttonheight = overlayRect.height * 0.075f;
		float predationExtraMargin = overlayRect.width * 0.003f;
		float backgroundRectWidthAdjust = (currentScreen == 4) ? buttonWidth * 0.32f : ((currentScreen == 3) ? buttonWidth * 0.03f : ((currentScreen == 1) ? buttonWidth * 0.12f : 0f));
		float shiftRight = (currentScreen == 4) ? buttonGap * 0.8f : 0f;
		if (currentScreen == 1)
			shiftRight += predationExtraMargin*0.5f;
		if (currentScreen == 2 || currentScreen == 4)
			shiftRight += predationExtraMargin*1.5f;
		if (currentScreen == 3)
			shiftRight += predationExtraMargin*3;
		if (currentScreen == 4)
			shiftRight += predationExtraMargin*1f;
		guiUtils.SetItemOffsets(buttonTrayBackground, overlayRect.x + overlayRect.width * 0.02f, overlayRect.y + overlayRect.height * 0.92f, overlayRect.width * 0.96f, overlayRect.height * 0.08f);
		guiUtils.SetItemOffsets(selectedButtonBackground, buttonMargin + buttonWidth*currentScreen + buttonGap*currentScreen + buttonWidth*0.05f - backgroundRectWidthAdjust*0.5f + shiftRight, buttonY + buttonheight * 0.10f, buttonWidth - buttonWidth*0.1f + backgroundRectWidthAdjust, buttonheight - buttonheight * 0.24f);
		guiUtils.SetItemOffsets(biologyButton, buttonMargin, buttonY, buttonWidth, buttonheight);
		biologyButton.GetComponent<RectTransform>().FindChild("Text").GetComponent<Text>().fontSize = (int)(overlayRect.width * 0.0225);
		guiUtils.SetItemOffsets(predationButton, buttonMargin + buttonWidth + buttonGap, buttonY, buttonWidth, buttonheight);
		predationButton.GetComponent<RectTransform>().FindChild("Text").GetComponent<Text>().fontSize = (int)(overlayRect.width * 0.0225);
		guiUtils.SetItemOffsets(ecologyButton, buttonMargin + buttonWidth*2f + buttonGap*2f + predationExtraMargin, buttonY, buttonWidth, buttonheight);
		ecologyButton.GetComponent<RectTransform>().FindChild("Text").GetComponent<Text>().fontSize = (int)(overlayRect.width * 0.0225);
		guiUtils.SetItemOffsets(survivalButton, buttonMargin + buttonWidth*3f + buttonGap*3f + predationExtraMargin*2, buttonY, buttonWidth, buttonheight);
		survivalButton.GetComponent<RectTransform>().FindChild("Text").GetComponent<Text>().fontSize = (int)(overlayRect.width * 0.0225);
		guiUtils.SetItemOffsets(donateButton, buttonMargin + buttonWidth*4f + buttonGap*4.8f + predationExtraMargin*2 - buttonWidth*0.1f, buttonY, buttonWidth + buttonWidth*0.2f, buttonheight);
		donateButton.GetComponent<RectTransform>().FindChild("Text").GetComponent<Text>().fontSize = (int)(overlayRect.width * 0.0225);
		overlayRect.width *= 0.9f;
		guiUtils.SetItemOffsets(backButton, overlayRect.x + overlayRect.width * 0.055f, overlayRect.y + overlayRect.height * 0.927f, overlayRect.width * 0.08f, overlayRect.height * 0.07f);
		backButton.GetComponent<RectTransform>().FindChild("Text").GetComponent<Text>().fontSize = (int)(overlayRect.width * 0.032);
		guiUtils.SetItemOffsets(playButton, overlayRect.x + overlayRect.width * 0.917f, overlayRect.y + overlayRect.height * 0.93f, overlayRect.width * 0.15f, overlayRect.height * 0.06f);
		playButton.GetComponent<RectTransform>().FindChild("Text").GetComponent<Text>().fontSize = (int)(overlayRect.width * 0.025);
		guiUtils.SetItemOffsets(goButton, overlayRect.x + overlayRect.width * 0.937f, overlayRect.y + overlayRect.height * 0.932f, overlayRect.width * 0.13f, overlayRect.height * 0.06f);
		goButton.GetComponent<RectTransform>().FindChild("Text").GetComponent<Text>().fontSize = (int)(overlayRect.width * 0.0225f);
		overlayRect.width /= 0.9f;

		// upper left and upper right labels
		guiUtils.SetItemOffsets(leftLabelBackground, (overlayRect.x + overlayRect.width * 0.06f) - overlayRect.width * 0.02f, (overlayRect.height * 0.022f) + overlayRect.y + overlayRect.height * 0.095f, overlayRect.width * 0.14f, overlayRect.height * 0.064f);
		guiUtils.SetItemOffsets(leftLabelText, (overlayRect.x + overlayRect.width * 0.06f) - overlayRect.width * 0.02f, (overlayRect.height * 0.022f) + overlayRect.y + overlayRect.height * 0.077f, overlayRect.width * 0.14f, overlayRect.height * 0.1f);
		leftLabelText.GetComponent<Text>().fontSize = (int)(overlayRect.width * 0.021f);		
		guiUtils.SetItemOffsets(rightLabelBackground, overlayRect.x + overlayRect.width - overlayRect.width * 0.2f, (overlayRect.height * 0.022f) + overlayRect.y + overlayRect.height * 0.095f, overlayRect.width * 0.16f, overlayRect.height * 0.064f);
		guiUtils.SetItemOffsets(rightLabelText, overlayRect.x + overlayRect.width - overlayRect.width * 0.2f, (overlayRect.height * 0.022f) + overlayRect.y + overlayRect.height * 0.077f, overlayRect.width * 0.16f, overlayRect.height * 0.1f);
		rightLabelText.GetComponent<Text>().fontSize = (int)(overlayRect.width * 0.021f);
		
		// info background panels 
		float panelX = overlayRect.x + overlayRect.width * 0.06f;
		float panelY = overlayRect.y + overlayRect.height * 0.205f;
		float panelWidth = overlayRect.width * 0.88f;
		float panelHeight = overlayRect.height * 0.6575f;
		float gapSize = overlayRect.width * 0.02f;
		guiUtils.SetItemOffsets(infoBackgroundOuterLeft, panelX - overlayRect.width * 0.02f, panelY - overlayRect.height * 0.025f, panelWidth/2 + overlayRect.width * 0.02f - gapSize/2, panelHeight + overlayRect.height * 0.05f);
		guiUtils.SetItemOffsets(infoBackgroundOuterRight, panelX + panelWidth/2 + gapSize/2, panelY - overlayRect.height * 0.025f, panelWidth/2 + overlayRect.width * 0.02f - gapSize/2, panelHeight + overlayRect.height * 0.05f);
		guiUtils.SetItemOffsets(infoBackgroundOuterFull, panelX - overlayRect.width * 0.02f, panelY - overlayRect.height * 0.025f, panelWidth + overlayRect.width * 0.04f, panelHeight + overlayRect.height * 0.05f);
		guiUtils.SetItemOffsets(infoBackgroundInnerLeft, panelX, panelY, panelWidth/2 - overlayRect.width * 0.02f - gapSize/2, panelHeight);
		guiUtils.SetItemOffsets(infoBackgroundInnerRight, panelX + panelWidth/2 + overlayRect.width * 0.02f + gapSize/2, panelY, panelWidth/2 - overlayRect.width * 0.02f - gapSize/2, panelHeight);
		guiUtils.SetItemOffsets(infoBackgroundInnerFull, panelX, panelY, panelWidth, panelHeight);
			
		// content of panels
		PositionLevelItems();
		PositionBiologyItems();
		PositionPredationItems();
		PositionEcologyItems();
		PositionSurvivalItems();
		PositionDonateItems();

		// standalone 'next' buttons
		guiUtils.SetItemOffsets(nextLevelButtonBackground, overlayRect.x + overlayRect.width * 0.415f, overlayRect.y + overlayRect.height * 0.892f, overlayRect.width * 0.17f, overlayRect.height * 0.098f);
		guiUtils.SetItemOffsets(nextLevelButton, overlayRect.x + overlayRect.width/2 - overlayRect.width * 0.12f * 0.5f, overlayRect.y + overlayRect.height * 0.904f, overlayRect.width * 0.12f, overlayRect.height * 0.07f);
		nextLevelButton.GetComponent<RectTransform>().FindChild("Text").GetComponent<Text>().fontSize = (int)(overlayRect.width * 0.028);
		guiUtils.SetItemOffsets(playAgainButtonBackground, overlayRect.x + overlayRect.width * 0.415f + overlayRect.width * 0.35f, overlayRect.y + overlayRect.height * 0.895f + overlayRect.height * 0.01f, overlayRect.width * 0.17f, overlayRect.height * 0.098f - overlayRect.height * 0.01f * 2);
		guiUtils.SetItemOffsets(playAgainButton, overlayRect.x + overlayRect.width/2 - overlayRect.width * 0.12f * 0.5f + overlayRect.width * 0.35f, overlayRect.y + overlayRect.height * 0.907f + overlayRect.height * 0.01f, overlayRect.width * 0.12f, overlayRect.height * 0.07f - overlayRect.height * 0.01f*2);
		playAgainButton.GetComponent<RectTransform>().FindChild("Text").GetComponent<Text>().fontSize = (int)(overlayRect.width * 0.019);
	}
	
	
	public void UpdateGUIItems(float incomingInfoPanelOpacity, float backRectOpacity, float okButtonOpacity, float frontRectOpacity = 0f) 
	{ 
		if (initComplete == false)
			return;
	
		float infoPanelOpacity = (backgroundIsLocked == true) ? 1f : incomingInfoPanelOpacity;	

		if (USE_NEW_GUI == true) {

			// check for screen size change
			if (lastSeenScreenWidth != Screen.width || lastSeenScreenHeight != Screen.height) {
				lastSeenScreenWidth = Screen.width;
				lastSeenScreenHeight = Screen.height;
				PositionGUIItems();
			}

			// set top level enables and opacities
			infoPanelBackRect.SetActive(backRectOpacity > 0f ? true : false);
			infoPanelBackRect.GetComponent<CanvasGroup>().alpha = backRectOpacity;
			infoPanelMainPanel.SetActive(infoPanelOpacity > 0f ? true : false);
			infoPanelMainPanel.GetComponent<CanvasGroup>().alpha = infoPanelOpacity;
			infoPanelOkButton.SetActive(okButtonOpacity > 0f ? true : false);
			infoPanelOkButton.GetComponent<CanvasGroup>().alpha = incomingInfoPanelOpacity * okButtonOpacity;
			
			// set context-dependent enables and visual params
			buttonTrayBackground.SetActive((newLevelFlag == false && currentLevel != 6) ? true : false);
			selectedButtonBackground.SetActive((newLevelFlag == false && currentLevel != 6) ? true : false);
			float buttonWidth = overlayRect.width * 0.11f;
			float buttonGap = overlayRect.width * 0.012f;
			float buttonMargin = overlayRect.x + overlayRect.width * 0.135f;
			float buttonY = overlayRect.y + overlayRect.height * 0.924f;
			float buttonheight = overlayRect.height * 0.075f;
			float predationExtraMargin = overlayRect.width * 0.003f;
			float backgroundRectWidthAdjust = (currentScreen == 4) ? buttonWidth * 0.32f : ((currentScreen == 3) ? buttonWidth * 0.03f : ((currentScreen == 1) ? buttonWidth * 0.12f : 0f));
			float shiftRight = (currentScreen == 4) ? buttonGap * 0.8f : 0f;
			if (currentScreen == 1)
				shiftRight += predationExtraMargin*0.5f;
			if (currentScreen == 2 || currentScreen == 4)
				shiftRight += predationExtraMargin*1.5f;
			if (currentScreen == 3)
				shiftRight += predationExtraMargin*3;
			if (currentScreen == 4)
				shiftRight += predationExtraMargin*1f;
			guiUtils.SetItemOffsets(selectedButtonBackground, buttonMargin + buttonWidth*currentScreen + buttonGap*currentScreen + buttonWidth*0.05f - backgroundRectWidthAdjust*0.5f + shiftRight, buttonY + buttonheight * 0.10f, buttonWidth - buttonWidth*0.1f + backgroundRectWidthAdjust, buttonheight - buttonheight * 0.24f);
			Color buttonTextColor = new Color(0.99f, 0.88f, 0.6f, 1f);
			Color buttonDownTextColor = new Color(0.99f, 0.7f, 0.2f, 1f);
			biologyButton.SetActive((newLevelFlag == false && currentLevel != 6) ? true : false);
			biologyButton.GetComponent<RectTransform>().FindChild("Text").GetComponent<Text>().color = (currentScreen == 0) ? buttonDownTextColor : buttonTextColor;
			predationButton.SetActive((newLevelFlag == false && currentLevel != 6) ? true : false);
			predationButton.GetComponent<RectTransform>().FindChild("Text").GetComponent<Text>().color = (currentScreen == 1) ? buttonDownTextColor : buttonTextColor;
			ecologyButton.SetActive((newLevelFlag == false && currentLevel != 6) ? true : false);
			ecologyButton.GetComponent<RectTransform>().FindChild("Text").GetComponent<Text>().color = (currentScreen == 2) ? buttonDownTextColor : buttonTextColor;
			survivalButton.SetActive((newLevelFlag == false && currentLevel != 6) ? true : false);
			survivalButton.GetComponent<RectTransform>().FindChild("Text").GetComponent<Text>().color = (currentScreen == 3) ? buttonDownTextColor : buttonTextColor;
			donateButton.SetActive((newLevelFlag == false && currentLevel != 6) ? true : false);
			donateButton.GetComponent<RectTransform>().FindChild("Text").GetComponent<Text>().color = (currentScreen == 4) ? buttonDownTextColor : buttonTextColor;
			backButton.SetActive((newLevelFlag == false && currentLevel != 6 && levelManager.CheckCarCollision() == false && levelManager.CheckStarvation() == false) ? true : false);
			playButton.SetActive((newLevelFlag == false && currentLevel != 6 && levelManager.CheckCarCollision() == false && levelManager.CheckStarvation() == false) ? true : false);
			playButton.GetComponent<Button>().interactable = (guiManager.selectedPuma != -1) ? true : false;
			goButton.SetActive((newLevelFlag == false && currentLevel != 6 && (levelManager.CheckCarCollision() == true || levelManager.CheckStarvation() == true)) ? true : false);
			nextLevelButtonBackground.SetActive((newLevelFlag == true && currentLevel != 6) ? true : false);
			nextLevelButton.SetActive((newLevelFlag == true && currentLevel != 6) ? true : false);
			playAgainButtonBackground.SetActive((newLevelFlag == false && currentLevel == 6) ? true : false);
			playAgainButton.SetActive((newLevelFlag == false && currentLevel == 6) ? true : false);
			leftLabelBackground.SetActive((newLevelFlag == false && currentLevel != 6 && currentScreen != 4) ? true : false);
			leftLabelText.SetActive((newLevelFlag == false && currentLevel != 6 && currentScreen != 4) ? true : false);
			rightLabelBackground.SetActive((newLevelFlag == false && currentLevel != 6) ? true : false);
			rightLabelText.SetActive((newLevelFlag == false && currentLevel != 6) ? true : false);
			infoBackgroundOuterLeft.SetActive((newLevelFlag == false && currentLevel != 6 && currentScreen != 4) ? true : false);
			infoBackgroundOuterRight.SetActive((newLevelFlag == false && currentLevel != 6 && currentScreen != 4) ? true : false);
			infoBackgroundOuterFull.SetActive((newLevelFlag == false && currentScreen == 4) ? true : false);	
			infoBackgroundInnerLeft.SetActive((newLevelFlag == false && currentLevel != 6 && currentScreen != 4) ? true : false);
			infoBackgroundInnerRight.SetActive((newLevelFlag == false && currentScreen != 4) ? true : false);
			infoBackgroundInnerFull.SetActive((newLevelFlag == false && currentScreen == 4) ? true : false);
			
			UpdateLevelItems(incomingInfoPanelOpacity);
			UpdateBiologyItems();
			UpdatePredationItems();
			UpdateEcologyItems();
			UpdateSurvivalItems();
			UpdateDonateItems(incomingInfoPanelOpacity);

		}
		else {
			// set all enables to 'off'
			infoPanelBackRect.SetActive(false);
			infoPanelMainPanel.SetActive(false);
			infoPanelOkButton.SetActive(false);
		}
	}

	
	//===================================
	//===================================
	//	  DRAW THE INFO PANEL
	//===================================
	//===================================
	
	public void Draw(float incomingInfoPanelOpacity, float backRectOpacity, float okButtonOpacity, float frontRectOpacity = 0f) 
	{ 
		if (USE_NEW_GUI == true) {
		
			if (frontRectOpacity > 0f) {
				// front rect USES OLD GUI !!!!  (for clean first frame of game...only used there)
				GUI.color = new Color(1f, 1f, 1f, 1f * frontRectOpacity);
				guiUtils.DrawRect(new Rect(0f, 0f, Screen.width, Screen.height), new Color(0f, 0f, 0f, 1f));	
			}
		
			return; 
		}
		
		
		//////////////////////////////////
		//////////////////////////////////
		
		// LEGACY DRAW CODE

		//////////////////////////////////
		//////////////////////////////////

		
		float infoPanelOpacity = (backgroundIsLocked == true) ? 1f : incomingInfoPanelOpacity;	

		GUIStyle style = new GUIStyle();
		style.alignment = TextAnchor.MiddleCenter;
		
		if (USE_NEW_GUI == false) {
			// back rect happens before everything else
			GUI.color = new Color(1f, 1f, 1f, 1f * backRectOpacity);
			guiUtils.DrawRect(new Rect(0f, 0f, Screen.width, Screen.height), new Color(0.1f, 0.1f, 0.1f, 1f));	
			GUI.color = new Color(1f, 1f, 1f, 1f * infoPanelOpacity);
		}
	
		// use expanded overlay rect during this function
		CalculateOverlayRect();
		float originalOverlayWidth = overlayRect.width;
		overlayRect.x -= overlayRect.width * 0.06f;
		overlayRect.width += overlayRect.width * 0.12f;
	
	
		if (USE_NEW_GUI == false) {
			// background panel
			GUI.color = new Color(0f, 0f, 0f, 1f * infoPanelOpacity);
			GUI.Box(new Rect(overlayRect.x + overlayRect.width*0.02f, overlayRect.y, overlayRect.width - overlayRect.width*0.04f, overlayRect.height), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * infoPanelOpacity);
		}


		if (USE_NEW_GUI == false) {
			// graphical logo
			float logoY = overlayRect.y - overlayRect.height * 0.013f;
			float logoWidth = originalOverlayWidth * 0.28f;
			float logoHeight = logoTexture.height * (logoWidth / logoTexture.width);
			float logoX = overlayRect.x + overlayRect.width/2 - logoWidth/2;
			GUI.color = new Color(0f, 0f, 0f, 1f * infoPanelOpacity);
			GUI.Box(new Rect(logoX, logoY + logoHeight * 0.2f, logoWidth, logoHeight * 0.6f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * infoPanelOpacity);			
			GUI.DrawTexture(new Rect(logoX, logoY, logoWidth, logoHeight), logoTexture);
		}

		//=====================================
		// ADD BUTTONS
		//=====================================
		
		if (newLevelFlag == false && currentLevel != 6) {
			// user initiated info display
		
			if (USE_NEW_GUI == false) {
				// background rectangle
				GUI.color = new Color(0f, 0f, 0f, 1f * infoPanelOpacity);
				GUI.Box(new Rect(overlayRect.x + overlayRect.width * 0.02f, overlayRect.y + overlayRect.height * 0.926f, overlayRect.width * 0.96f, overlayRect.height * 0.074f), "");
				GUI.color = new Color(1f, 1f, 1f, 1f * infoPanelOpacity);
			}
			

			float buttonWidth = overlayRect.width * 0.11f;
			float buttonGap = overlayRect.width * 0.012f;
			float buttonMargin = overlayRect.x + overlayRect.width * 0.135f;
			float buttonY = overlayRect.y + overlayRect.height * 0.924f;
			float buttonheight = overlayRect.height * 0.075f;
			float predationExtraMargin = overlayRect.width * 0.003f;
			float backgroundRectWidthAdjust = (currentScreen == 4) ? buttonWidth * 0.32f : ((currentScreen == 3) ? buttonWidth * 0.03f : ((currentScreen == 1) ? buttonWidth * 0.12f : 0f));
			float shiftRight = (currentScreen == 4) ? buttonGap * 0.8f : 0f;
			if (currentScreen == 1)
				shiftRight += predationExtraMargin*0.5f;
			if (currentScreen == 2 || currentScreen == 4)
				shiftRight += predationExtraMargin*1.5f;
			if (currentScreen == 3)
				shiftRight += predationExtraMargin*3;
			if (currentScreen == 4)
				shiftRight += predationExtraMargin*1f;

				
			if (USE_NEW_GUI == false) {
				// button background for selected button
				guiUtils.DrawRect(new Rect(buttonMargin + buttonWidth*currentScreen + buttonGap*currentScreen + buttonWidth*0.05f - backgroundRectWidthAdjust*0.5f + shiftRight, buttonY + buttonheight * 0.15f, buttonWidth - buttonWidth*0.1f + backgroundRectWidthAdjust, buttonheight - buttonheight * 0.29f), new Color(0f, 0f, 0f, 0.5f));	
			}


			// SELECTION BUTTONS
					
			buttonStyle.fontSize = buttonDownStyle.fontSize = (int)(overlayRect.width * 0.0225);
			buttonDisabledStyle.fontSize = buttonDownStyle.fontSize = (int)(overlayRect.width * 0.0225);
			buttonDownStyle.normal.textColor = new Color(0.99f, 0.7f, 0.2f, 1f);
			buttonStyle.normal.textColor = new Color(0.99f, 0.88f, 0.6f, 1f);
			

			if (USE_NEW_GUI == false) {
				// 'Biology'
				if (GUI.Button(new Rect(buttonMargin, buttonY, buttonWidth, buttonheight), "Biology", (currentScreen == 0) ? buttonDownStyle : buttonStyle))
					currentScreen = 0;
				// 'Ecology'
				if (GUI.Button(new Rect(buttonMargin + buttonWidth + buttonGap, buttonY, buttonWidth, buttonheight), "Predation", (currentScreen == 1) ? buttonDownStyle : buttonStyle))
					currentScreen = 1;
				// 'Predation'
				if (GUI.Button(new Rect(buttonMargin + buttonWidth*2f + buttonGap*2f + predationExtraMargin, buttonY, buttonWidth, buttonheight), "Ecology", (currentScreen == 2) ? buttonDownStyle : buttonStyle))
					currentScreen = 2;
				// 'Survival'
				if (GUI.Button(new Rect(buttonMargin + buttonWidth*3f + buttonGap*3f + predationExtraMargin*2, buttonY, buttonWidth, buttonheight), "Survival", (currentScreen == 3) ? buttonDownStyle : buttonStyle))
					currentScreen = 3;
				// 'Take Action'
				if (GUI.Button(new Rect(buttonMargin + buttonWidth*4f + buttonGap*4.8f + predationExtraMargin*2, buttonY, buttonWidth, buttonheight), "Take Action", (currentScreen == 4) ? buttonDownStyle : buttonStyle))
					currentScreen = 4;
			}

				
			// PLAY/OK BUTTONS
				
			overlayRect.width *= 0.9f;
			
			buttonStyle.normal.textColor = new Color(0.99f, 0.88f, 0.6f, 1f);
			buttonDownStyle.normal.textColor = new Color(0.99f, 0.88f, 0.6f, 1f);
			buttonStyle.fontSize = buttonDownStyle.fontSize = (int)(overlayRect.width * 0.032);
			buttonDisabledStyle.fontSize = buttonDownStyle.fontSize = (int)(overlayRect.width * 0.023);
			buttonWidth = overlayRect.width * 0.12f;
	
			GUI.color = new Color(1f, 1f, 1f, 1f * infoPanelOpacity);	
		
			if (levelManager.CheckCarCollision() == false && levelManager.CheckStarvation() == false) {
		
				// normal case

				// 'close'
				bigButtonStyle.fontSize = (int)(overlayRect.width * 0.032f);;
				bigButtonDisabledStyle.fontSize = (int)(overlayRect.width * 0.03f);;
				GUI.skin = guiManager.customGUISkin;
				guiManager.customGUISkin.button.fontSize = (int)(overlayRect.width * 0.030f);
				guiManager.customGUISkin.button.fontStyle = FontStyle.Normal;
				GUI.backgroundColor = new Color(1f, 1f, 1f, 1f);
				float startButtonX = overlayRect.x + overlayRect.width * 0.055f;
				float startButtonY = overlayRect.y + overlayRect.height * 0.937f;
				float startButtonWidth = overlayRect.width * 0.08f;
				float startButtonHeight = overlayRect.height * 0.05f;

				if (USE_NEW_GUI == false) {
					if (GUI.Button(new Rect(startButtonX, startButtonY, startButtonWidth, startButtonHeight), "", buttonStyle)) {
						guiManager.CloseInfoPanel(true);
					}
					if (GUI.Button(new Rect(startButtonX, startButtonY, startButtonWidth, startButtonHeight), "<", buttonStyle)) {
						guiManager.CloseInfoPanel(true);
					}
				}


				// 'play'
				// background rectangle
				startButtonX = overlayRect.x + overlayRect.width * 0.917f;
				startButtonY = overlayRect.y + overlayRect.height * 0.932f;
				startButtonWidth = overlayRect.width * 0.15f;
				startButtonHeight = overlayRect.height * 0.06f;
				GUI.color = (guiManager.selectedPuma != -1) ? new Color(1f, 1f, 1f, 1f * infoPanelOpacity) : new Color(1f, 1f, 1f, 0.5f * infoPanelOpacity);
				bigButtonStyle.fontSize = (int)(overlayRect.width * 0.032f);;
				bigButtonDisabledStyle.fontSize = (int)(overlayRect.width * 0.03f);;
				GUI.skin = guiManager.customGUISkin;
				guiManager.customGUISkin.button.fontSize = (int)(overlayRect.width * 0.025f);
				guiManager.customGUISkin.button.fontStyle = FontStyle.Bold;
				GUI.backgroundColor = new Color(1f, 1f, 1f, 1f);

				if (USE_NEW_GUI == false) {
					if (guiManager.selectedPuma != -1) {
						if (GUI.Button(new Rect(startButtonX, startButtonY, startButtonWidth, startButtonHeight), "")) {
							guiManager.CloseInfoPanel(true);
							guiManager.SetGuiState("guiStateLeavingOverlay");
							levelManager.SetGameState("gameStateLeavingGui");
						}
						if (GUI.Button(new Rect(startButtonX, startButtonY, startButtonWidth, startButtonHeight), "PLAY")) {
							guiManager.CloseInfoPanel(true);
							guiManager.SetGuiState("guiStateLeavingOverlay");
							levelManager.SetGameState("gameStateLeavingGui");
						}
					}
					else {
						GUI.Button(new Rect(startButtonX, startButtonY, startButtonWidth, startButtonHeight), "PLAY", bigButtonDisabledStyle);
					}
				}

			}
			else {
				// coming from puma done screen
			
				// 'play'
				// background rectangle
				float startButtonX = overlayRect.x + overlayRect.width * 0.917f;
				float startButtonY = overlayRect.y + overlayRect.height * 0.932f;
				float startButtonWidth = overlayRect.width * 0.15f;
				float startButtonHeight = overlayRect.height * 0.06f;
				GUI.color = (guiManager.selectedPuma != -1) ? new Color(1f, 1f, 1f, 1f * infoPanelOpacity) : new Color(1f, 1f, 1f, 0.5f * infoPanelOpacity);
				bigButtonStyle.fontSize = (int)(overlayRect.width * 0.032f);;
				bigButtonDisabledStyle.fontSize = (int)(overlayRect.width * 0.03f);;
				GUI.skin = guiManager.customGUISkin;
				guiManager.customGUISkin.button.fontSize = (int)(overlayRect.width * 0.0225f);
				guiManager.customGUISkin.button.fontStyle = FontStyle.Bold;
				GUI.backgroundColor = new Color(1f, 1f, 1f, 1f);
				if (guiManager.selectedPuma != -1) {
				
					if (USE_NEW_GUI == false) {
						if (GUI.Button(new Rect(startButtonX, startButtonY, startButtonWidth, startButtonHeight), "")) {
							guiManager.CloseInfoPanel(true);
							guiManager.SetGuiState("guiStateLeavingPumaDoneAlt");
							if (levelManager.CheckCarCollision() == true)
								levelManager.EndCarCollision();
							else if (levelManager.CheckStarvation() == true)
								levelManager.EndStarvation();		
							levelManager.SetGameState("gameStateLeavingGameplay");
						}
						if (GUI.Button(new Rect(startButtonX, startButtonY, startButtonWidth, startButtonHeight), "GO")) {
							guiManager.CloseInfoPanel(true);
							guiManager.SetGuiState("guiStateLeavingPumaDoneAlt");
							if (levelManager.CheckCarCollision() == true)
								levelManager.EndCarCollision();
							else if (levelManager.CheckStarvation() == true)
								levelManager.EndStarvation();		
							levelManager.SetGameState("gameStateLeavingGameplay");
						}
					}
					
				}
			}


			overlayRect.width /= 0.9f;		
		}

		else {
		
			if (currentLevel != 6) {
				// transition screen for next level			
				infoPanelOpacity = incomingInfoPanelOpacity;
		
				if (USE_NEW_GUI == false) {
					// background rectangle
					GUI.color = new Color(0f, 0f, 0f, 1f * infoPanelOpacity * okButtonOpacity);
					GUI.Box(new Rect(overlayRect.x + overlayRect.width * 0.415f, overlayRect.y + overlayRect.height * 0.892f, overlayRect.width * 0.17f, overlayRect.height * 0.098f), "");
					GUI.color = new Color(1f, 1f, 1f, 1f * infoPanelOpacity * okButtonOpacity);
				}

				GUI.color = new Color(1f, 1f, 1f, 1f * infoPanelOpacity * okButtonOpacity);		
				// 'play'
				bigButtonStyle.fontSize = (int)(overlayRect.width * 0.032);;
				bigButtonDisabledStyle.fontSize = (int)(overlayRect.width * 0.03);;
				GUI.skin = guiManager.customGUISkin;
				guiManager.customGUISkin.button.fontSize = (int)(overlayRect.width * 0.028);
				guiManager.customGUISkin.button.fontStyle = FontStyle.Normal;
				GUI.backgroundColor = new Color(1f, 1f, 1f, 1f);
				float startButtonWidth = overlayRect.width * 0.12f;
				float startButtonX = overlayRect.x + overlayRect.width/2 - startButtonWidth/2;
				float startButtonY = overlayRect.y + overlayRect.height * 0.906f;
				float startButtonHeight = overlayRect.height * 0.07f;

				if (USE_NEW_GUI == false) {
					if (GUI.Button(new Rect(startButtonX, startButtonY, startButtonWidth, startButtonHeight), "")) {
						guiManager.CloseInfoPanel(true);
						guiManager.SetGuiState("guiStateStartApp2");
					}
					if (GUI.Button(new Rect(startButtonX, startButtonY, startButtonWidth, startButtonHeight), "GO")) {
						guiManager.CloseInfoPanel(true);
						guiManager.SetGuiState("guiStateStartApp2");
					}
				}
			}
			else {
				// restarting new game at first level		
				infoPanelOpacity = incomingInfoPanelOpacity;
				float rightShift = overlayRect.width * 0.35f;
				float downShift = overlayRect.height * 0.01f;
		
				if (USE_NEW_GUI == false) {
					// background rectangle
					GUI.color = new Color(0f, 0f, 0f, 1f * infoPanelOpacity * okButtonOpacity);
					GUI.Box(new Rect(overlayRect.x + overlayRect.width * 0.415f + rightShift, overlayRect.y + overlayRect.height * 0.895f + downShift, overlayRect.width * 0.17f, overlayRect.height * 0.098f - downShift*2), "");
					GUI.color = new Color(1f, 1f, 1f, 1f * infoPanelOpacity * okButtonOpacity);
				}

				GUI.color = new Color(1f, 1f, 1f, 1f * infoPanelOpacity * okButtonOpacity);		
				// 'play'
				bigButtonStyle.fontSize = (int)(overlayRect.width * 0.032);;
				bigButtonDisabledStyle.fontSize = (int)(overlayRect.width * 0.03);;
				GUI.skin = guiManager.customGUISkin;
				guiManager.customGUISkin.button.fontSize = (int)(overlayRect.width * 0.019);
				guiManager.customGUISkin.button.fontStyle = FontStyle.Normal;
				GUI.backgroundColor = new Color(1f, 1f, 1f, 1f);
				float startButtonWidth = overlayRect.width * 0.12f;
				float startButtonX = overlayRect.x + overlayRect.width/2 - startButtonWidth/2 + rightShift;
				float startButtonY = overlayRect.y + overlayRect.height * 0.909f + downShift;
				float startButtonHeight = overlayRect.height * 0.07f - downShift*2;

				if (USE_NEW_GUI == false) {
					if (GUI.Button(new Rect(startButtonX, startButtonY, startButtonWidth, startButtonHeight), "")) {
						guiManager.CloseInfoPanel(true);
						guiManager.SetGuiState("guiStateStartApp2");
					}
					if (GUI.Button(new Rect(startButtonX, startButtonY, startButtonWidth, startButtonHeight), "Play Again")) {
						guiManager.CloseInfoPanel(true);
						guiManager.SetGuiState("guiStateStartApp2");
					}
				}
			}
		}
		
		infoPanelOpacity = incomingInfoPanelOpacity;

		GUI.color = new Color(1f, 1f, 1f, 1f * infoPanelOpacity);
	
		if (newLevelFlag == true) {	
			DrawLevelScreen(infoPanelOpacity);
		}
		else {
			// draw selected screen
			switch (currentScreen) {
			case 0:
				DrawBiologyScreen(infoPanelOpacity);
				break;		
			case 1:
				DrawPredationScreen(infoPanelOpacity);
				break;
			case 2:
				DrawEcologyScreen(infoPanelOpacity);
				break;
			case 3:
				DrawSurvivalScreen(infoPanelOpacity);
				break;
			case 4:
				DrawDonateScreen(infoPanelOpacity);
				break;
			}
		}


		if (USE_NEW_GUI == false) {
			// front rect happens last
			GUI.color = new Color(1f, 1f, 1f, 1f * frontRectOpacity);
			guiUtils.DrawRect(new Rect(0f, 0f, Screen.width, Screen.height), new Color(0f, 0f, 0f, 1f));	
			GUI.color = new Color(1f, 1f, 1f, 1f * infoPanelOpacity);
		}
	}




	//======================================================================
	//======================================================================
	//======================================================================
	//								NEW LEVEL
	//======================================================================
	//======================================================================
	//======================================================================
	
	GameObject levelPanel;
	GameObject levelBackgroundOuter;
	GameObject levelBackgroundInner;
	GameObject levelMainImage;
	
	// right side text
	GameObject levelTextTitle1;
	GameObject levelTextTitle2;
	GameObject levelTextUpper1;
	GameObject levelTextUpper2;
	GameObject levelTextUpper3;
	GameObject levelTextLower1;
	GameObject levelTextLower2;
	GameObject levelTextLower3;
	
	// extinction
	GameObject levelLoseHead1;
	GameObject levelLoseHead2;
	GameObject levelLoseHead3;
	GameObject levelLoseHead4;
	GameObject levelLoseHead5;
	GameObject levelLoseHead6;
	GameObject levelLoseSkull1;
	GameObject levelLoseSkull2;
	GameObject levelLoseSkull3;
	GameObject levelLoseSkull4;
	GameObject levelLoseSkull5;
	GameObject levelLoseSkull6;

	// completion
	GameObject levelWinPercentBgnd;
	GameObject levelWinPercentText;
	GameObject levelWinHealthText;
	GameObject levelWinLeftText;
	GameObject levelWinRightText;
	
	

	void CreateLevelItems()
	{
		// invisible panel to hold items
		levelPanel = (GameObject)Instantiate(uiSubPanel);
		levelPanel.GetComponent<RectTransform>().SetParent(infoPanelMainPanel.GetComponent<RectTransform>(), false);
	
		// main items
		levelBackgroundOuter = 	guiUtils.CreatePanel(levelPanel, new Color(0f, 0f, 0f, 0.4f));
		levelBackgroundInner = 	guiUtils.CreatePanel(levelPanel, new Color(0f, 0f, 0f, 0.2f));
		levelMainImage = 		guiUtils.CreateImage(levelPanel, levelImage1Texture, new Color(1f, 1f, 1f, 1f));
		
		// right side text
		levelTextTitle1 = 		guiUtils.CreateText(levelPanel, "Level 1:", new Color(0.99f * 0.78f, 0.64f * 0.78f, 0.13f * 0.78f, 1f), FontStyle.Italic, TextAnchor.MiddleLeft);
		levelTextTitle2 = 		guiUtils.CreateText(levelPanel, "WILD NATURE", new Color(0.2f, 0.7f, 0.14f), FontStyle.Bold, TextAnchor.MiddleLeft);
		levelTextUpper1 = 		guiUtils.CreateText(levelPanel, "Landscape", new Color(0.846f, 0.537f, 0.12f, 1f), FontStyle.BoldAndItalic, TextAnchor.MiddleLeft);
		levelTextUpper2 = 		guiUtils.CreateText(levelPanel, "Natural pristine wilderness", new Color(0.99f * 0.78f, 0.88f * 0.78f, 0.66f * 0.78f, 1f), FontStyle.Normal, TextAnchor.MiddleLeft);
		levelTextUpper3 = 		guiUtils.CreateText(levelPanel, "No human activity", new Color(0.99f * 0.78f, 0.88f * 0.78f, 0.66f * 0.78f, 1f), FontStyle.Normal, TextAnchor.MiddleLeft);
		levelTextLower1 = 		guiUtils.CreateText(levelPanel, "Survival", new Color(0.846f, 0.537f, 0.12f, 1f), FontStyle.BoldAndItalic, TextAnchor.MiddleLeft);
		levelTextLower2 = 		guiUtils.CreateText(levelPanel, "Hunt efficiently for good health", new Color(0.99f * 0.78f, 0.88f * 0.78f, 0.66f * 0.78f, 1f), FontStyle.Normal, TextAnchor.MiddleLeft);
		levelTextLower3 = 		guiUtils.CreateText(levelPanel, "Hunt poorly and you die", new Color(0.99f * 0.78f, 0.88f * 0.78f, 0.66f * 0.78f, 1f), FontStyle.Normal, TextAnchor.MiddleLeft);
		
		// extinction
		float redXOpacity = 0.7f;
		float redXLuminocity = 0.93f;
		float imageLuminosity = 0.2f;
		levelLoseHead1 = 		guiUtils.CreateImage(levelPanel, closeup1Texture, new Color(imageLuminosity, imageLuminosity, imageLuminosity, 1f));
		levelLoseHead2 = 		guiUtils.CreateImage(levelPanel, closeup2Texture, new Color(imageLuminosity, imageLuminosity, imageLuminosity, 1f));
		levelLoseHead3 = 		guiUtils.CreateImage(levelPanel, closeup3Texture, new Color(imageLuminosity, imageLuminosity, imageLuminosity, 0.9f));
		levelLoseHead4 = 		guiUtils.CreateImage(levelPanel, closeup4Texture, new Color(imageLuminosity, imageLuminosity, imageLuminosity, 0.9f));
		levelLoseHead5 = 		guiUtils.CreateImage(levelPanel, closeup5Texture, new Color(imageLuminosity, imageLuminosity, imageLuminosity, 1f));
		levelLoseHead6 = 		guiUtils.CreateImage(levelPanel, closeup6Texture, new Color(imageLuminosity, imageLuminosity, imageLuminosity, 1f));
		levelLoseSkull1 = 		guiUtils.CreateImage(levelPanel, pumaCrossbonesDarkRedTexture, new Color(redXLuminocity, redXLuminocity, redXLuminocity, redXOpacity));
		levelLoseSkull2 = 		guiUtils.CreateImage(levelPanel, pumaCrossbonesDarkRedTexture, new Color(redXLuminocity, redXLuminocity, redXLuminocity, redXOpacity));
		levelLoseSkull3 = 		guiUtils.CreateImage(levelPanel, pumaCrossbonesDarkRedTexture, new Color(redXLuminocity, redXLuminocity, redXLuminocity, redXOpacity));
		levelLoseSkull4 = 		guiUtils.CreateImage(levelPanel, pumaCrossbonesDarkRedTexture, new Color(redXLuminocity, redXLuminocity, redXLuminocity, redXOpacity));
		levelLoseSkull5 = 		guiUtils.CreateImage(levelPanel, pumaCrossbonesDarkRedTexture, new Color(redXLuminocity, redXLuminocity, redXLuminocity, redXOpacity));
		levelLoseSkull6 = 		guiUtils.CreateImage(levelPanel, pumaCrossbonesDarkRedTexture, new Color(redXLuminocity, redXLuminocity, redXLuminocity, redXOpacity));

		// completion
		levelWinPercentBgnd = 	guiUtils.CreatePanel(levelPanel, new Color(0f, 0f, 0f, 0.4f));
		levelWinPercentText = 	guiUtils.CreateText(levelPanel, "50%", new Color(0.85f * 0.90f, 0.80f * 0.90f, 0f, 0.85f), FontStyle.Bold, TextAnchor.MiddleLeft);
		levelWinHealthText = 	guiUtils.CreateText(levelPanel, "Health", new Color(0.85f * 0.90f, 0.80f * 0.90f, 0f, 0.85f), FontStyle.Normal, TextAnchor.MiddleLeft);
		levelWinLeftText = 		guiUtils.CreateText(levelPanel, "Population:", new Color(0.85f * 0.90f, 0.80f * 0.90f, 0f, 0.85f), FontStyle.Normal, TextAnchor.MiddleLeft);
		levelWinRightText = 	guiUtils.CreateText(levelPanel, "SUSTAINING", new Color(0.85f * 0.90f, 0.80f * 0.90f, 0f, 0.85f), FontStyle.Normal, TextAnchor.MiddleLeft);
		
		SetupLevelDisplay();
	}


	
	void PositionLevelItems()
	{
		float panelX = overlayRect.x + overlayRect.width * 0.06f;
		float panelY = overlayRect.y + overlayRect.height * 0.205f;
		float panelWidth = overlayRect.width * 0.88f;
		float panelHeight = overlayRect.height * 0.63f;
		float xOffset = panelWidth * -0.017f;
		float yOffset = panelHeight * 0.042f;
		float yOffset2 = panelHeight * -0.005f;
		float leftShift = panelWidth * 0.195f;

		// main items
		float textureX = panelX + panelWidth * 0.03f;
		float textureY = panelY + panelHeight * 0.1f;
		float textureHeight = panelHeight * 0.8f;
		float textureWidth = levelImage1Texture.width * (textureHeight / levelImage1Texture.height);
		guiUtils.SetItemOffsets(levelBackgroundOuter, panelX - overlayRect.width * 0.02f, panelY - overlayRect.height * 0.025f, panelWidth + overlayRect.width * 0.04f, panelHeight + overlayRect.height * 0.05f + panelHeight * 0.005f);
		guiUtils.SetItemOffsets(levelBackgroundInner, panelX, panelY, panelWidth, panelHeight + panelHeight * 0.005f);
		guiUtils.SetItemOffsets(levelMainImage, textureX, textureY, textureWidth, textureHeight);
		
		// right side text
		guiUtils.SetTextOffsets(levelTextTitle1, xOffset + panelX + panelWidth * 0.58f, yOffset + panelY + panelHeight * 0.065f, panelWidth * 0.4f, panelHeight * 0.1f, (int)(overlayRect.width * 0.020f));
		guiUtils.SetTextOffsets(levelTextTitle2, xOffset + panelX + panelWidth * 0.58f, yOffset + panelY + panelHeight * 0.145f, panelWidth * 0.4f, panelHeight * 0.1f, (int)(overlayRect.width * 0.025f));
		guiUtils.SetTextOffsets(levelTextUpper1, xOffset + panelX + panelWidth * 0.58f, yOffset + panelY + panelHeight * 0.29f, panelWidth * 0.4f, panelHeight * 0.1f, (int)(overlayRect.width * 0.024f));
		guiUtils.SetTextOffsets(levelTextUpper2, xOffset + panelX + panelWidth * 0.6f, yOffset + panelY + panelHeight * 0.37f, panelWidth * 0.4f, panelHeight * 0.1f, (int)(overlayRect.width * 0.020f));
		guiUtils.SetTextOffsets(levelTextUpper3, xOffset + panelX + panelWidth * 0.6f, yOffset + panelY + panelHeight * 0.45f, panelWidth * 0.4f, panelHeight * 0.1f, (int)(overlayRect.width * 0.020f));
		guiUtils.SetTextOffsets(levelTextLower1, xOffset + panelX + panelWidth * 0.58f, yOffset + panelY + panelHeight * 0.59f, panelWidth * 0.4f, panelHeight * 0.1f, (int)(overlayRect.width * 0.024f));
		guiUtils.SetTextOffsets(levelTextLower2, xOffset + panelX + panelWidth * 0.6f, yOffset + panelY + panelHeight * 0.67f, panelWidth * 0.4f, panelHeight * 0.1f, (int)(overlayRect.width * 0.020f));
		guiUtils.SetTextOffsets(levelTextLower3, xOffset + panelX + panelWidth * 0.6f, yOffset + panelY + panelHeight * 0.75f, panelWidth * 0.4f, panelHeight * 0.1f, (int)(overlayRect.width * 0.020f));
		
		// extinction
		float headLeftShift = panelWidth * -0.02f;
		textureX = panelX + panelWidth * 0.05f;
		textureY = panelY + panelHeight * 0.2f;
		textureHeight = panelHeight * 0.27f;
		textureWidth = closeup1Texture.width * (textureHeight / closeup1Texture.height);
		guiUtils.SetItemOffsets(levelLoseHead1, headLeftShift + textureX, textureY, textureWidth, textureHeight);
		guiUtils.SetItemOffsets(levelLoseSkull1, textureX + textureWidth * 0.1f, textureY + textureHeight * 0.25f, textureWidth * 0.8f, textureHeight * 0.8f);
		textureX = panelX + panelWidth * 0.21f;
		textureWidth = closeup2Texture.width * (textureHeight / closeup2Texture.height);
		guiUtils.SetItemOffsets(levelLoseHead2, headLeftShift + textureX, textureY, textureWidth, textureHeight);
		guiUtils.SetItemOffsets(levelLoseSkull2, textureX + textureWidth * 0.1f, textureY + textureHeight * 0.25f, textureWidth * 0.8f, textureHeight * 0.8f);
		textureX = panelX + panelWidth * 0.37f;
		textureWidth = closeup3Texture.width * (textureHeight / closeup3Texture.height);
		guiUtils.SetItemOffsets(levelLoseHead3, headLeftShift + textureX, textureY, textureWidth, textureHeight);
		guiUtils.SetItemOffsets(levelLoseSkull3, textureX + textureWidth * 0.1f, textureY + textureHeight * 0.25f, textureWidth * 0.8f, textureHeight * 0.8f);
		textureX = panelX + panelWidth * 0.05f;
		textureY = panelY + panelHeight * 0.52f;
		textureWidth = closeup4Texture.width * (textureHeight / closeup4Texture.height);
		guiUtils.SetItemOffsets(levelLoseHead4, headLeftShift + textureX, textureY, textureWidth, textureHeight);
		guiUtils.SetItemOffsets(levelLoseSkull4, textureX + textureWidth * 0.1f, textureY + textureHeight * 0.25f, textureWidth * 0.8f, textureHeight * 0.8f);
		textureX = panelX + panelWidth * 0.21f;
		textureWidth = closeup5Texture.width * (textureHeight / closeup5Texture.height);
		guiUtils.SetItemOffsets(levelLoseHead5, headLeftShift + textureX, textureY, textureWidth, textureHeight);
		guiUtils.SetItemOffsets(levelLoseSkull5, textureX + textureWidth * 0.1f, textureY + textureHeight * 0.25f, textureWidth * 0.8f, textureHeight * 0.8f);
		textureX = panelX + panelWidth * 0.37f;
		textureWidth = closeup6Texture.width * (textureHeight / closeup6Texture.height);
		guiUtils.SetItemOffsets(levelLoseHead6, headLeftShift + textureX, textureY, textureWidth, textureHeight);
		guiUtils.SetItemOffsets(levelLoseSkull6, textureX + textureWidth * 0.1f, textureY + textureHeight * 0.25f, textureWidth * 0.8f, textureHeight * 0.8f);

		// completion
		guiUtils.SetItemOffsets(levelWinPercentBgnd, xOffset + panelX + panelWidth * 0.92f - leftShift, yOffset + yOffset2 + panelY + panelHeight * 0.33f, panelWidth * 0.069f, panelHeight * 0.13f);
		guiUtils.SetTextOffsets(levelWinPercentText, xOffset + panelX + panelWidth * 0.93f - leftShift, yOffset + yOffset2 + panelY + panelHeight * 0.325f, panelWidth * 0.4f, panelHeight * 0.1f, (int)(overlayRect.width * 0.020f));
		guiUtils.SetTextOffsets(levelWinHealthText, xOffset + panelX + panelWidth * 0.93f - leftShift, yOffset + yOffset2 + panelY + panelHeight * 0.376f, panelWidth * 0.4f, panelHeight * 0.1f, (int)(overlayRect.width * 0.014f));
		guiUtils.SetTextOffsets(levelWinLeftText, xOffset + panelX + panelWidth * 0.6f, yOffset + panelY + panelHeight * 0.37f, panelWidth * 0.4f, panelHeight * 0.1f, (int)(overlayRect.width * 0.020f));
		guiUtils.SetTextOffsets(levelWinRightText, xOffset + panelX + panelWidth * 0.81f, yOffset + panelY + panelHeight * 0.37f, panelWidth * 0.4f, panelHeight * 0.1f, (int)(overlayRect.width * 0.020f));
	}


	void SetupLevelDisplay()  // called during start-up, and whenever level is changed
	{
		bool populationDoneFlag = scoringSystem.GetPopulationHealth() == 0f ? true : false;

		// level image
		Texture2D imageTexture = levelImage1Texture;
		float imageOpacity = 1f;
		
		switch (currentLevel) {
		case 0:
			imageTexture = levelImage1Texture;
			imageOpacity = 1f;
			break;
		case 1:
			imageTexture = levelImage2Texture;
			imageOpacity = 1f;
			break;
		case 2:
			imageTexture = levelImage3Texture;
			imageOpacity = 0.9f;
			break;
		case 3:
			imageTexture = levelImage4Texture;
			imageOpacity = 1f;
			break;
		case 4:
			imageTexture = levelImage5Texture;
			imageOpacity = 0.9f;
			break;
		case 5:
			imageTexture = populationDoneFlag == true ? levelImage6bTexture : levelImage6Texture;
			imageOpacity = populationDoneFlag == true ? 0.55f : 0.9f;
			break;
		}		
		
		levelMainImage.GetComponent<RawImage>().texture = imageTexture;
		levelMainImage.GetComponent<RawImage>().color = new Color(1f, 1f, 1f, imageOpacity);

		
		// level text	
		string titleText1 = "empty text";
		string titleText2 = "empty text";
		string landscapeText1 = "empty text";
		string landscapeText2 = "empty text";
		string survivalText1 = "empty text";
		string survivalText2 = "empty text";
		Color labelColor = new Color(0f, 0f, 0f);
		Color subTextColor = new Color(0f, 0f, 0f);
		
		switch (currentLevel) {
		case 0:
			titleText1 = "Level 1:";
			titleText2 = "WILD NATURE";
			labelColor = new Color(0.2f, 0.7f, 0.14f);
			landscapeText1 = "Natural pristine wilderness";
			landscapeText2 = "No human activity";
			survivalText1 = "Hunt efficiently for good health";
			survivalText2 = "Hunt poorly and you die";
			break;
		case 1:
			titleText1 = "Level 2:";
			titleText2 = "HUMAN ARRIVAL";
			labelColor = new Color(0.85f, 0.66f, 0.0f);
			landscapeText1 = "First presence of humans";
			landscapeText2 = "Roads with light traffic";
			survivalText1 = "Cross roads carefully to chase prey";
			survivalText2 = "If a vehicle hits you - instant death";
			break;
		case 2:
			titleText1 = "Level 3:";
			titleText2 = "DEVELOPMENT";
			labelColor = new Color(1f, 0.5f, 0.0f);
			landscapeText1 = "Humans have altered the landscape";
			landscapeText2 = "Bigger roads with faster traffic";
			survivalText1 = "Find bridges and cross underneath";
			survivalText2 = "Be extra careful crossing roads";
			break;
		case 3:
			titleText1 = "Level 4:";
			titleText2 = "FRAGMENTATION";
			labelColor = new Color(1f, 0.2f, 0.0f);
			landscapeText1 = "Human activity and roads everywhere";
			landscapeText2 = "Roads impassible with heavy traffic";
			survivalText1 = "Cross under AND OVER bridges";
			survivalText2 = "Stay off the roads - or instant death";
			break;
		case 4:
			titleText1 = "Level 5:";
			titleText2 = "CONNECTIVITY!";
			labelColor = new Color(0.14f, 0.7f, 0.14f);
			landscapeText1 = "Humans have become good neighbors";
			landscapeText2 = "Free movement is possible again";
			survivalText1 = "Travel easily over and under roads";
			survivalText2 = "You still need to stay clear of cars";
			break;
		case 5:
			if (populationDoneFlag == true) {
				// local extinction
				titleText1 = "You didn't make it...";
				titleText2 = "The Population Is Extinct!";
				labelColor = new Color(0.90f * 0.78f, 0.15f * 0.78f, 0f);
				landscapeText1 = "Local pumas have not survived";
				landscapeText2 = "The ecosystem's health will decline";
			}
			else {
				// successful completion
				titleText1 = "You've Made It...";
				titleText2 = "Local Pumas Have Survived!";
				labelColor = new Color(0.88f, 0.65f, 0.20f);				
				if (scoringSystem.GetPopulationHealth() < 0.2f) {
					subTextColor = new Color(0.86f, 0.07f, 0f, 1f);
					landscapeText1 = "CRITICAL";
					landscapeText2 = "They will improve in good habitat";
				}
				else if (scoringSystem.GetPopulationHealth() < 0.4f) {
					subTextColor = new Color(0.99f, 0.40f, 0f, 1f);
					landscapeText1 = "ENDANGERED";
					landscapeText2 = "They will improve in good habitat";
				}
				else if (scoringSystem.GetPopulationHealth() < 0.6f) {
					subTextColor = new Color(0.85f * 0.90f, 0.80f * 0.90f, 0f, 0.85f);
					landscapeText1 = "SUSTAINING";
					landscapeText2 = "They will get stronger in good habitat";
				}
				else if (scoringSystem.GetPopulationHealth() < 0.8f) {
					subTextColor = new Color(0.5f * 1.04f, 0.7f * 1.04f, 0f, 1f);
					landscapeText1 = "ESTABLISHED";
					landscapeText2 = "They will stay strong in good habitat";
				}
				else {
					subTextColor = new Color(0f, 0.75f, 0f, 1f);
					landscapeText1 = "THRIVING";
					landscapeText2 = "They will stay strong in good habitat";
				}
			}
			survivalText1 = "Puma Populations face increasing risks";
			survivalText2 = "You can make a difference...";
			break;
		}
		
		
		// apply params to objects
		levelTextTitle1.GetComponent<Text>().text = titleText1;
		levelTextTitle2.GetComponent<Text>().text = titleText2;
		levelTextTitle2.GetComponent<Text>().color = labelColor;
		levelTextUpper2.GetComponent<Text>().text = landscapeText1;
		levelTextUpper3.GetComponent<Text>().text = landscapeText2;
		levelTextLower2.GetComponent<Text>().text = survivalText1;
		levelTextLower3.GetComponent<Text>().text = survivalText2;
		

		// swap in or out special text on final screen
		
		if (currentLevel != 5 || populationDoneFlag == true) {
			levelTextUpper1.GetComponent<Text>().text = (populationDoneFlag == true) ? "Result" : "Landscape";
			levelTextUpper2.SetActive(true);
			levelTextUpper3.GetComponent<Text>().color =  new Color(0.99f * 0.78f, 0.88f * 0.78f, 0.66f * 0.78f, 1f);
			levelTextLower1.GetComponent<Text>().text =  (populationDoneFlag == true) ? "Real World" : "Survival";
			levelTextLower2.GetComponent<Text>().color = (populationDoneFlag == true) ? (new Color(1f * 0.7f, 0.14f * 0.7f, 0.0f * 0.7f, 1f)) : (new Color(0.99f * 0.78f, 0.88f * 0.78f, 0.66f * 0.78f, 1f));

			levelWinPercentBgnd.SetActive(false);
			levelWinPercentText.SetActive(false);
			levelWinHealthText.SetActive(false);
			levelWinLeftText.SetActive(false);
			levelWinRightText.SetActive(false);
		}
		else {
			levelTextUpper1.GetComponent<Text>().text = "Result";
			levelTextUpper2.SetActive(false);
			levelTextUpper3.GetComponent<Text>().color =  new Color(0.99f * 0.7f, 0.88f * 0.7f, 0.55f  * 0.7f, 1f);
			levelTextLower1.GetComponent<Text>().text = "Real World";
			levelTextLower2.GetComponent<Text>().color =  new Color(1f * 0.7f, 0.14f * 0.7f, 0.0f * 0.7f, 1f);
			
			levelWinPercentBgnd.SetActive(true);
			levelWinPercentText.SetActive(true);
			levelWinHealthText.SetActive(true);
			levelWinLeftText.SetActive(true);
			levelWinRightText.SetActive(true);

			levelWinPercentText.GetComponent<Text>().color = subTextColor;
			levelWinHealthText.GetComponent<Text>().color = subTextColor;
			levelWinLeftText.GetComponent<Text>().color = subTextColor;
			levelWinRightText.GetComponent<Text>().color = subTextColor;
			
			int healthInt = (int)(scoringSystem.GetPopulationHealth() * 100);
			levelWinPercentText.GetComponent<Text>().text = healthInt + "%";
		}
		
		// turn on or off the "population extinct" graphics
		
		levelLoseHead1.SetActive(populationDoneFlag);
		levelLoseHead2.SetActive(populationDoneFlag);
		levelLoseHead3.SetActive(populationDoneFlag);
		levelLoseHead4.SetActive(populationDoneFlag);
		levelLoseHead5.SetActive(populationDoneFlag);
		levelLoseHead6.SetActive(populationDoneFlag);
		populationDoneFlag = false;
		levelLoseSkull1.SetActive(populationDoneFlag);
		levelLoseSkull2.SetActive(populationDoneFlag);
		levelLoseSkull3.SetActive(populationDoneFlag);
		levelLoseSkull4.SetActive(populationDoneFlag);
		levelLoseSkull5.SetActive(populationDoneFlag);
		levelLoseSkull6.SetActive(populationDoneFlag);
	}


	void UpdateLevelItems(float alpha)
	{
		levelPanel.SetActive(newLevelFlag);
		levelPanel.GetComponent<CanvasGroup>().alpha = (currentLevel == 5) ? alpha : 1f;
	}	


	void DrawLevelScreen(float panelOpacity)
	{ 
		float panelX = overlayRect.x + overlayRect.width * 0.06f;
		float panelY = overlayRect.y + overlayRect.height * 0.205f;
		float panelWidth = overlayRect.width * 0.88f;
		float panelHeight = overlayRect.height * 0.63f;
		float fontScale = 0.8f;
		bool populationDoneFlag = scoringSystem.GetPopulationHealth() == 0f ? true : false;

		
		if (USE_NEW_GUI == true)
			return;
		
		
		GUIStyle style = new GUIStyle();
		style.alignment = TextAnchor.MiddleCenter;

		// background rectangle
		GUI.color = new Color(0f, 0f, 0f, 1f * panelOpacity);
		GUI.Box(new Rect(panelX - overlayRect.width * 0.02f, panelY - overlayRect.height * 0.025f, panelWidth + overlayRect.width * 0.04f, panelHeight + overlayRect.height * 0.05f + panelHeight * 0.005f), "");
		GUI.color = new Color(0f, 0f, 0f, 0.5f * panelOpacity);
		GUI.Box(new Rect(panelX, panelY, panelWidth, panelHeight + panelHeight * 0.005f), "");
		GUI.color = new Color(1f, 1f, 1f, 1f * panelOpacity);


		// level image
		Texture2D imageTexture = levelImage1Texture;
		float imageOpacity = 1f;
		
		switch (currentLevel) {
		case 0:
			imageTexture = levelImage1Texture;
			imageOpacity = 1f;
			break;
		case 1:
			imageTexture = levelImage2Texture;
			imageOpacity = 1f;
			break;
		case 2:
			imageTexture = levelImage3Texture;
			imageOpacity = 0.9f;
			break;
		case 3:
			imageTexture = levelImage4Texture;
			imageOpacity = 1f;
			break;
		case 4:
			imageTexture = levelImage5Texture;
			imageOpacity = 0.9f;
			break;
		case 5:
			imageTexture = populationDoneFlag == true ? levelImage6bTexture : levelImage6Texture;
			imageOpacity = populationDoneFlag == true ? 0.55f : 0.9f;
			break;
		}		

		float textureX = panelX + panelWidth * 0.03f;
		float textureY = panelY + panelHeight * 0.1f;
		float textureHeight = panelHeight * 0.8f;
		float textureWidth = imageTexture.width * (textureHeight / imageTexture.height);
		GUI.color = new Color(1f, 1f, 1f, imageOpacity * panelOpacity);
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), imageTexture);
		GUI.color = new Color(1f, 1f, 1f, 1f * panelOpacity);
		
		if (populationDoneFlag) {
		
			float redXOpacity = 0.7f;
			float imageLuminosity = 0.2f;
		
			imageTexture = closeup1Texture;
			imageOpacity = 1f;
			textureX = panelX + panelWidth * 0.05f;
			textureY = panelY + panelHeight * 0.2f;
			textureHeight = panelHeight * 0.27f;
			textureWidth = imageTexture.width * (textureHeight / imageTexture.height);
			GUI.color = new Color(imageLuminosity, imageLuminosity, imageLuminosity, imageOpacity * panelOpacity);
			GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), imageTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * panelOpacity);
		
			imageTexture = pumaCrossbonesDarkRedTexture;
			GUI.color = new Color(0.93f, 0.93f, 0.93f, redXOpacity * panelOpacity);
			GUI.DrawTexture(new Rect(textureX + textureWidth * 0.1f, textureY + textureHeight * 0.25f, textureWidth * 0.8f, textureHeight * 0.8f), imageTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * panelOpacity);
		
		
			imageTexture = closeup2Texture;
			imageOpacity = 1f;
			textureX = panelX + panelWidth * 0.21f;
			textureWidth = imageTexture.width * (textureHeight / imageTexture.height);
			GUI.color = new Color(imageLuminosity, imageLuminosity, imageLuminosity, imageOpacity * panelOpacity);
			GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), imageTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * panelOpacity);
		
			imageTexture = pumaCrossbonesDarkRedTexture;
			GUI.color = new Color(0.93f, 0.93f, 0.93f, redXOpacity * panelOpacity);
			GUI.DrawTexture(new Rect(textureX + textureWidth * 0.1f, textureY + textureHeight * 0.25f, textureWidth * 0.8f, textureHeight * 0.8f), imageTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * panelOpacity);
		
		
			imageTexture = closeup3Texture;
			imageOpacity = 0.9f;
			textureX = panelX + panelWidth * 0.37f;
			textureWidth = imageTexture.width * (textureHeight / imageTexture.height);
			GUI.color = new Color(imageLuminosity, imageLuminosity, imageLuminosity, imageOpacity * panelOpacity);
			GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), imageTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * panelOpacity);
		
			imageTexture = pumaCrossbonesDarkRedTexture;
			GUI.color = new Color(0.93f, 0.93f, 0.93f, redXOpacity * panelOpacity);
			GUI.DrawTexture(new Rect(textureX + textureWidth * 0.1f, textureY + textureHeight * 0.25f, textureWidth * 0.8f, textureHeight * 0.8f), imageTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * panelOpacity);
		
		
		
			imageTexture = closeup4Texture;
			imageOpacity = 0.9f;
			textureX = panelX + panelWidth * 0.05f;
			textureY = panelY + panelHeight * 0.52f;
			textureWidth = imageTexture.width * (textureHeight / imageTexture.height);
			GUI.color = new Color(imageLuminosity, imageLuminosity, imageLuminosity, imageOpacity * panelOpacity);
			GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), imageTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * panelOpacity);
		
			imageTexture = pumaCrossbonesDarkRedTexture;
			GUI.color = new Color(0.93f, 0.93f, 0.93f, redXOpacity * panelOpacity);
			GUI.DrawTexture(new Rect(textureX + textureWidth * 0.1f, textureY + textureHeight * 0.25f, textureWidth * 0.8f, textureHeight * 0.8f), imageTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * panelOpacity);

		
			imageTexture = closeup5Texture;
			imageOpacity = 1f;
			textureX = panelX + panelWidth * 0.21f;
			textureWidth = imageTexture.width * (textureHeight / imageTexture.height);
			GUI.color = new Color(imageLuminosity, imageLuminosity, imageLuminosity, imageOpacity * panelOpacity);
			GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), imageTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * panelOpacity);
		
			imageTexture = pumaCrossbonesDarkRedTexture;
			GUI.color = new Color(0.93f, 0.93f, 0.93f, redXOpacity * panelOpacity);
			GUI.DrawTexture(new Rect(textureX + textureWidth * 0.1f, textureY + textureHeight * 0.25f, textureWidth * 0.8f, textureHeight * 0.8f), imageTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * panelOpacity);

		
			imageTexture = closeup6Texture;
			imageOpacity = 1f;
			textureX = panelX + panelWidth * 0.37f;
			textureWidth = imageTexture.width * (textureHeight / imageTexture.height);
			GUI.color = new Color(imageLuminosity, imageLuminosity, imageLuminosity, imageOpacity * panelOpacity);
			GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), imageTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * panelOpacity);
		
			imageTexture = pumaCrossbonesDarkRedTexture;
			GUI.color = new Color(0.93f, 0.93f, 0.93f, redXOpacity * panelOpacity);
			GUI.DrawTexture(new Rect(textureX + textureWidth * 0.1f, textureY + textureHeight * 0.25f, textureWidth * 0.8f, textureHeight * 0.8f), imageTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * panelOpacity);
		
		}

		
		// level text	
		string titleText1 = "empty text";
		string titleText2 = "empty text";
		string landscapeText1 = "empty text";
		string landscapeText2 = "empty text";
		string survivalText1 = "empty text";
		string survivalText2 = "empty text";
		Color labelColor = new Color(0f, 0f, 0f);
		Color subTextColor = new Color(0f, 0f, 0f);
		
		switch (currentLevel) {
		case 0:
			titleText1 = "Level 1:";
			titleText2 = "WILD NATURE";
			labelColor = new Color(0.2f, 0.7f, 0.14f);
			landscapeText1 = "Natural pristine wilderness";
			landscapeText2 = "No human activity";
			survivalText1 = "Hunt efficiently for good health";
			survivalText2 = "Hunt poorly and you die";
			break;
		case 1:
			titleText1 = "Level 2:";
			titleText2 = "HUMAN ARRIVAL";
			labelColor = new Color(0.85f, 0.66f, 0.0f);
			landscapeText1 = "First presence of humans";
			landscapeText2 = "Roads with light traffic";
			survivalText1 = "Cross roads carefully to chase prey";
			survivalText2 = "If a vehicle hits you - instant death";
			break;
		case 2:
			titleText1 = "Level 3:";
			titleText2 = "DEVELOPMENT";
			labelColor = new Color(1f, 0.5f, 0.0f);
			landscapeText1 = "Humans have altered the landscape";
			landscapeText2 = "Bigger roads with faster traffic";
			survivalText1 = "Find bridges and cross underneath";
			survivalText2 = "Be extra careful crossing roads";
			break;
		case 3:
			titleText1 = "Level 4:";
			titleText2 = "FRAGMENTATION";
			labelColor = new Color(1f, 0.2f, 0.0f);
			landscapeText1 = "Human activity and roads everywhere";
			landscapeText2 = "Roads impassible with heavy traffic";
			survivalText1 = "Cross under AND OVER bridges";
			survivalText2 = "Stay off the roads - or instant death";
			break;
		case 4:
			titleText1 = "Level 5:";
			titleText2 = "CONNECTIVITY!";
			labelColor = new Color(0.14f, 0.7f, 0.14f);
			landscapeText1 = "Humans have become good neighbors";
			landscapeText2 = "Free movement is possible again";
			survivalText1 = "Travel easily over and under roads";
			survivalText2 = "You still need to stay clear of cars";
			break;
		case 5:
			if (populationDoneFlag == true) {
				// local extinction
				titleText1 = "You didn't make it...";
				titleText2 = "The Population Is Extinct!";
				labelColor = new Color(0.90f * 0.78f, 0.15f * 0.78f, 0f);
				landscapeText1 = "Local pumas have not survived";
				landscapeText2 = "The ecosystem's health will decline";
			}
			else {
				// successful completion
				titleText1 = "You've Made It...";
				titleText2 = "Local Pumas Have Survived!";
				labelColor = new Color(0.88f, 0.65f, 0.20f);
				if (scoringSystem.GetPopulationHealth() < 0.2f) {
					subTextColor = new Color(0.86f, 0.07f, 0f, 1f);
					landscapeText1 = "CRITICAL";
					landscapeText2 = "They will improve in good habitat";
				}
				else if (scoringSystem.GetPopulationHealth() < 0.4f) {
					subTextColor = new Color(0.99f, 0.40f, 0f, 1f);
					landscapeText1 = "ENDANGERED";
					landscapeText2 = "They will improve in good habitat";
				}
				else if (scoringSystem.GetPopulationHealth() < 0.6f) {
					subTextColor = new Color(0.85f * 0.90f, 0.80f * 0.90f, 0f, 0.85f);
					landscapeText1 = "SUSTAINING";
					landscapeText2 = "They will get stronger in good habitat";
				}
				else if (scoringSystem.GetPopulationHealth() < 0.8f) {
					subTextColor = new Color(0.5f * 1.04f, 0.7f * 1.04f, 0f, 1f);
					landscapeText1 = "ESTABLISHED";
					landscapeText2 = "They will stay strong in good habitat";
				}
				else {
					subTextColor = new Color(0f, 0.75f, 0f, 1f);
					landscapeText1 = "THRIVING";
					landscapeText2 = "They will stay strong in good habitat";
				}
			}
			survivalText1 = "Puma Populations face increasing risks";
			survivalText2 = "You can make a difference...";
			break;
		}

		style.alignment = TextAnchor.MiddleLeft;
		float colorScale = 0.78f;
		float xOffset = panelWidth * -0.017f;
		float yOffset = panelHeight * 0.042f;

		//

		style.fontSize = (int)(overlayRect.width * 0.020f);
		style.fontStyle = FontStyle.Italic;
		style.normal.textColor = new Color(0.99f * colorScale, 0.64f * colorScale, 0.13f * colorScale, 1f);
		GUI.Button(new Rect(xOffset + panelX + panelWidth * 0.58f, yOffset + panelY + panelHeight * 0.065f, panelWidth * 0.4f, panelHeight * 0.1f), titleText1, style);
		style.normal.textColor = labelColor;
		style.fontStyle = FontStyle.Bold;
		style.fontSize = (int)(overlayRect.width * 0.025f);
		GUI.Button(new Rect(xOffset + panelX + panelWidth * 0.58f, yOffset + panelY + panelHeight * 0.145f, panelWidth * 0.4f, panelHeight * 0.1f), titleText2, style);

		//

		style.fontSize = (int)(overlayRect.width * 0.024f);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.normal.textColor = new Color(0.846f, 0.537f, 0.12f, 1f);
		GUI.Button(new Rect(xOffset + panelX + panelWidth * 0.58f, yOffset + panelY + panelHeight * 0.29f, panelWidth * 0.4f, panelHeight * 0.1f), (currentLevel == 5 ? "Result" : "Landscape"), style);
		
		style.fontSize = (int)(overlayRect.width * 0.020f);
		style.fontStyle = FontStyle.Normal;
		style.normal.textColor = new Color(0.99f * colorScale, 0.88f * colorScale, 0.66f * colorScale, 1f);
		if (currentLevel == 5 && populationDoneFlag == false) {
			// final screen after successful game completion
			style.normal.textColor = subTextColor;
			GUI.Button(new Rect(xOffset + panelX + panelWidth * 0.6f, yOffset + panelY + panelHeight * 0.37f, panelWidth * 0.4f, panelHeight * 0.1f), "Population:", style);
			style.normal.textColor = new Color(0.99f * 0.7f, 0.88f * 0.7f, 0.55f  * 0.7f, 1f);
			GUI.Button(new Rect(xOffset + panelX + panelWidth * 0.6f, yOffset + panelY + panelHeight * 0.45f, panelWidth * 0.4f, panelHeight * 0.1f), landscapeText2, style);
			style.normal.textColor = subTextColor;
			GUI.Button(new Rect(xOffset + panelX + panelWidth * 0.81f, yOffset + panelY + panelHeight * 0.37f, panelWidth * 0.4f, panelHeight * 0.1f), landscapeText1, style);
			int healthInt = (int)(scoringSystem.GetPopulationHealth() * 100);
			float yOffset2 = panelHeight * -0.005f;
			float leftShift = panelWidth * 0.195f;
			GUI.color = new Color(0f, 0f, 0f, 1f * panelOpacity);
			GUI.Box(new Rect(xOffset + panelX + panelWidth * 0.92f - leftShift, yOffset + yOffset2 + panelY + panelHeight * 0.33f, panelWidth * 0.069f, panelHeight * 0.13f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * panelOpacity);
			style.fontStyle = FontStyle.Bold;
			style.fontSize = (int)(overlayRect.width * 0.020f);
			GUI.Button(new Rect(xOffset + panelX + panelWidth * 0.93f - leftShift, yOffset + yOffset2 + panelY + panelHeight * 0.325f, panelWidth * 0.4f, panelHeight * 0.1f), healthInt + "%", style);
			//style.fontStyle = FontStyle.Normal;
			style.fontSize = (int)(overlayRect.width * 0.014f);
			GUI.Button(new Rect(xOffset + panelX + panelWidth * 0.93f - leftShift, yOffset + yOffset2 + panelY + panelHeight * 0.376f, panelWidth * 0.4f, panelHeight * 0.1f), "Health", style);
		}
		else {
			// normal handling
			GUI.Button(new Rect(xOffset + panelX + panelWidth * 0.6f, yOffset + panelY + panelHeight * 0.37f, panelWidth * 0.4f, panelHeight * 0.1f), landscapeText1, style);
			GUI.Button(new Rect(xOffset + panelX + panelWidth * 0.6f, yOffset + panelY + panelHeight * 0.45f, panelWidth * 0.4f, panelHeight * 0.1f), landscapeText2, style);
		}
			
		//

		style.fontSize = (int)(overlayRect.width * 0.024f);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.normal.textColor = (new Color(0.846f, 0.537f, 0.12f, 1f));
		GUI.Button(new Rect(xOffset + panelX + panelWidth * 0.58f, yOffset + panelY + panelHeight * 0.59f, panelWidth * 0.4f, panelHeight * 0.1f), (currentLevel == 5 ? "Real World" : "Survival"), style);

		style.fontSize = (int)(overlayRect.width * 0.020f);
		style.fontStyle = FontStyle.Normal;
		style.normal.textColor = (currentLevel != 5) ? (new Color(0.99f * colorScale, 0.88f * colorScale, 0.66f * colorScale, 1f)) : (new Color(1f * colorScale, 0.14f * colorScale, 0.0f * colorScale, 1f));
		GUI.Button(new Rect(xOffset + panelX + panelWidth * 0.6f, yOffset + panelY + panelHeight * 0.67f, panelWidth * 0.4f, panelHeight * 0.1f), survivalText1, style);
		style.normal.textColor = new Color(0.99f * colorScale, 0.88f * colorScale, 0.66f * colorScale, 1f);
		GUI.Button(new Rect(xOffset + panelX + panelWidth * 0.6f, yOffset + panelY + panelHeight * 0.75f, panelWidth * 0.4f, panelHeight * 0.1f), survivalText2, style);

		//

		style.alignment = TextAnchor.MiddleCenter;

		
	}	


	
	//======================================================================
	//======================================================================
	//======================================================================
	//								BIOLOGY
	//======================================================================
	//======================================================================
	//======================================================================
	
	GameObject biologyPanel;
	GameObject biologyHeaderTextL;
	GameObject biologyHeaderTextR;
	
	// LEFT PANEL
	
	// title
	GameObject biologyLeftTitleBackground;
	GameObject biologyLeftTitleText;
	
	// pumas
	GameObject biologyLeftPumasBackground;
	GameObject biologyLeftPumaImagel;
	GameObject biologyLeftPumaImage2;
	GameObject biologyLeftPumaImage3;
	GameObject biologyLeftPumaImage4;
	GameObject biologyLeftPumaImage5;
	GameObject biologyLeftPumaImage6;
	GameObject biologyLeftNameText1;
	GameObject biologyLeftNameText2;
	GameObject biologyLeftNameText3;
	GameObject biologyLeftNameText4;
	GameObject biologyLeftNameText5;
	GameObject biologyLeftNameText6;
	GameObject biologyLeftAgeTextLeft;
	GameObject biologyLeftAgeTextMiddle;
	GameObject biologyLeftAgeTextRight;
	
	// speed
	GameObject biologyLeftSpeedBgndMain;
	GameObject biologyLeftSpeedBgndTitle;
	GameObject biologyLeftSpeedImage;	
	GameObject biologyLeftSpeedTextTitle1;
	GameObject biologyLeftSpeedTextTitle2;
	GameObject biologyLeftSpeedTextLine1;
	GameObject biologyLeftSpeedTextLine2;
	GameObject biologyLeftSpeedTextLine3;
	GameObject biologyLeftSpeedTextLine4;
		
	// stealth
	GameObject biologyLeftStealthBgndMain;
	GameObject biologyLeftStealthBgndTitle;
	GameObject biologyLeftStealthImage;
	GameObject biologyLeftStealthTextTitle1;
	GameObject biologyLeftStealthTextTitle2;
	GameObject biologyLeftStealthTextLine1;
	GameObject biologyLeftStealthTextLine2;
	GameObject biologyLeftStealthTextLine3;
	GameObject biologyLeftStealthTextLine4;
	
	// arrow
	GameObject biologyLeftArrowImage;	

	// RIGHT PANEL

	// Labels
	GameObject runTitleLabel;
	GameObject runTitleBackground;
	GameObject runSpeedLabel;

	GameObject jumpTitleLabel;
	GameObject jumpTitleBackground;

	GameObject jumpVerticalTitleLabel;
	GameObject jumpVerticalHeightLabel;

	GameObject jumpHorizontalTitleLabel;
	GameObject jumpHorizontalHeightLabel;

	// Physical Characteritics Image
	GameObject physicalCharacteristicsImage;

	
	void CreateBiologyItems()
	{
		// invisible panel to hold items
		biologyPanel = (GameObject)Instantiate(uiSubPanel);
		biologyPanel.GetComponent<RectTransform>().SetParent(infoPanelMainPanel.GetComponent<RectTransform>(), false);
	
		// header text
		biologyHeaderTextL = 			guiUtils.CreateText(biologyPanel, "Speed and Stealth", new Color(0.99f * 0.82f, 0.88f * 0.82f, 0.66f * 0.82f, 1f), FontStyle.Normal);
		biologyHeaderTextR = 			guiUtils.CreateText(biologyPanel, "Physical Characteristics", new Color(0.99f * 0.82f, 0.88f * 0.82f, 0.66f * 0.82f, 1f), FontStyle.Normal);
		
		// LEFT PANEL
		
		// title
		biologyLeftTitleBackground = 	guiUtils.CreatePanel(biologyPanel, new Color(0f, 0f, 0f, 0.4f));
		biologyLeftTitleText = 			guiUtils.CreateText(biologyPanel, "When hunting deer...", new Color(0.84f * 1.1f, 0.61f * 1.1f, 0f, 0.9f), FontStyle.BoldAndItalic, TextAnchor.MiddleLeft);
		
		// pumas
		biologyLeftPumasBackground = 	guiUtils.CreatePanel(biologyPanel, new Color(0f, 0f, 0f, 0.4f));
		biologyLeftPumaImagel = 		guiUtils.CreateImage(biologyPanel, closeup1Texture, new Color(1f, 1f, 1f, 0.90f));
		biologyLeftPumaImage2 = 		guiUtils.CreateImage(biologyPanel, closeup2Texture, new Color(1f, 1f, 1f, 0.90f));
		biologyLeftPumaImage3 = 		guiUtils.CreateImage(biologyPanel, closeup3Texture, new Color(1f, 1f, 1f, 0.80f));
		biologyLeftPumaImage4 = 		guiUtils.CreateImage(biologyPanel, closeup4Texture, new Color(1f, 1f, 1f, 0.80f));
		biologyLeftPumaImage5 = 		guiUtils.CreateImage(biologyPanel, closeup5Texture, new Color(1f, 1f, 1f, 0.85f));
		biologyLeftPumaImage6 = 		guiUtils.CreateImage(biologyPanel, closeup6Texture, new Color(1f, 1f, 1f, 0.90f));
		biologyLeftNameText1 = 			guiUtils.CreateText(biologyPanel, "Eric", new Color(0.88f, 0.64f, 0f, 0.85f), FontStyle.BoldAndItalic);
		biologyLeftNameText2 = 			guiUtils.CreateText(biologyPanel, "Palo", new Color(0.88f, 0.64f, 0f, 0.85f), FontStyle.BoldAndItalic);
		biologyLeftNameText3 = 			guiUtils.CreateText(biologyPanel, "Mitch", new Color(0.88f, 0.64f, 0f, 0.85f), FontStyle.BoldAndItalic);
		biologyLeftNameText4 = 			guiUtils.CreateText(biologyPanel, "Trish", new Color(0.88f, 0.64f, 0f, 0.85f), FontStyle.BoldAndItalic);
		biologyLeftNameText5 = 			guiUtils.CreateText(biologyPanel, "Liam", new Color(0.88f, 0.64f, 0f, 0.85f), FontStyle.BoldAndItalic);
		biologyLeftNameText6 = 			guiUtils.CreateText(biologyPanel, "Barb", new Color(0.88f, 0.64f, 0f, 0.85f), FontStyle.BoldAndItalic);
		biologyLeftAgeTextLeft = 		guiUtils.CreateText(biologyPanel, "2 Years Old", new Color(0.99f * 0.74f, 0.88f * 0.74f, 0.66f * 0.74f, 1f), FontStyle.BoldAndItalic);
		biologyLeftAgeTextMiddle = 		guiUtils.CreateText(biologyPanel, "5 Years Old", new Color(0.99f * 0.74f, 0.88f * 0.74f, 0.66f * 0.74f, 1f), FontStyle.BoldAndItalic);
		biologyLeftAgeTextRight = 		guiUtils.CreateText(biologyPanel, "8 Years Old", new Color(0.99f * 0.74f, 0.88f * 0.74f, 0.66f * 0.74f, 1f), FontStyle.BoldAndItalic);

		// speed
		biologyLeftSpeedBgndMain = 		guiUtils.CreatePanel(biologyPanel, new Color(0f, 0f, 0f, 0.4f));
		biologyLeftSpeedBgndTitle =		guiUtils.CreatePanel(biologyPanel, new Color(0f, 0f, 0f, 0.4f));
		biologyLeftSpeedImage = 		guiUtils.CreateImage(biologyPanel, pumaRunTexture, new Color(1f, 1f, 1f, 1f));
		biologyLeftSpeedTextTitle1 = 	guiUtils.CreateText(biologyPanel, "Excels at", new Color(0.99f * 0.74f, 0.88f * 0.74f, 0.66f * 0.74f, 1f), FontStyle.BoldAndItalic);
		biologyLeftSpeedTextTitle2 = 	guiUtils.CreateText(biologyPanel, "Speed", new Color(0.816f, 0.537f, 0.18f, 1f), FontStyle.BoldAndItalic);
		biologyLeftSpeedTextLine1 = 	guiUtils.CreateText(biologyPanel, "Young cats can", new Color(0.99f * 0.74f, 0.88f * 0.74f, 0.66f * 0.74f, 1f), FontStyle.BoldAndItalic, TextAnchor.MiddleLeft);
		biologyLeftSpeedTextLine2 = 	guiUtils.CreateText(biologyPanel, "run very fast", new Color(0.99f * 0.74f, 0.88f * 0.74f, 0.66f * 0.74f, 1f), FontStyle.BoldAndItalic, TextAnchor.MiddleLeft);
		biologyLeftSpeedTextLine3 = 	guiUtils.CreateText(biologyPanel, "...but they can't", new Color(0.99f * 0.74f, 0.88f * 0.74f, 0.66f * 0.74f, 1f), FontStyle.BoldAndItalic, TextAnchor.MiddleLeft);
		biologyLeftSpeedTextLine4 = 	guiUtils.CreateText(biologyPanel, "sneak up close", new Color(0.99f * 0.74f, 0.88f * 0.74f, 0.66f * 0.74f, 1f), FontStyle.BoldAndItalic, TextAnchor.MiddleLeft);

		// stealth
		biologyLeftStealthBgndMain = 	guiUtils.CreatePanel(biologyPanel, new Color(0f, 0f, 0f, 0.4f));
		biologyLeftStealthBgndTitle = 	guiUtils.CreatePanel(biologyPanel, new Color(0f, 0f, 0f, 0.4f));
		biologyLeftStealthImage = 		guiUtils.CreateImage(biologyPanel, pumaStealthTexture, new Color(1f, 1f, 1f, 1f));
		biologyLeftStealthTextTitle1 = 	guiUtils.CreateText(biologyPanel, "Excels at", new Color(0.99f * 0.74f, 0.88f * 0.74f, 0.66f * 0.74f, 1f), FontStyle.BoldAndItalic);
		biologyLeftStealthTextTitle2 = 	guiUtils.CreateText(biologyPanel, "Stealth", new Color(0.816f, 0.537f, 0.18f, 1f), FontStyle.BoldAndItalic);
		biologyLeftStealthTextLine1 = 	guiUtils.CreateText(biologyPanel, "Older cats can", new Color(0.99f * 0.74f, 0.88f * 0.74f, 0.66f * 0.74f, 1f), FontStyle.BoldAndItalic, TextAnchor.MiddleLeft);
		biologyLeftStealthTextLine2 = 	guiUtils.CreateText(biologyPanel, "sneak up close", new Color(0.99f * 0.74f, 0.88f * 0.74f, 0.66f * 0.74f, 1f), FontStyle.BoldAndItalic, TextAnchor.MiddleLeft);
		biologyLeftStealthTextLine3 = 	guiUtils.CreateText(biologyPanel, "...but they can't", new Color(0.99f * 0.74f, 0.88f * 0.74f, 0.66f * 0.74f, 1f), FontStyle.BoldAndItalic, TextAnchor.MiddleLeft);
		biologyLeftStealthTextLine4 = 	guiUtils.CreateText(biologyPanel, "run very fast", new Color(0.99f * 0.74f, 0.88f * 0.74f, 0.66f * 0.74f, 1f), FontStyle.BoldAndItalic, TextAnchor.MiddleLeft);

		// arrow
		biologyLeftArrowImage = 		guiUtils.CreateImage(biologyPanel, blackArrowTexture, new Color(0.5f, 0.5f, 0.5f, 1f));

		// RIGHT PANEL

		// Labels
		runTitleLabel = 				guiUtils.CreateText(biologyPanel, "Run", new Color(0.816f, 0.537f, 0.18f, 1f), FontStyle.BoldAndItalic, TextAnchor.MiddleLeft);
		runTitleBackground =			guiUtils.CreatePanel(biologyPanel, new Color(0f, 0f, 0f, 0.4f));
		runSpeedLabel = 				guiUtils.CreateText(biologyPanel, "45 mph", new Color(0.99f * 0.74f, 0.88f * 0.74f, 0.66f * 0.74f, 1f), FontStyle.BoldAndItalic, TextAnchor.MiddleLeft);

		jumpTitleLabel = 				guiUtils.CreateText(biologyPanel, "Jump", new Color(0.816f, 0.537f, 0.18f, 1f), FontStyle.BoldAndItalic, TextAnchor.MiddleLeft);
		jumpTitleBackground = 			guiUtils.CreatePanel(biologyPanel, new Color(0f, 0f, 0f, 0.4f));

		jumpVerticalTitleLabel = 		guiUtils.CreateText(biologyPanel, "Vertical", new Color(0.816f, 0.537f, 0.18f, 1f), FontStyle.BoldAndItalic, TextAnchor.MiddleLeft);
		jumpVerticalHeightLabel = 		guiUtils.CreateText(biologyPanel, "20 ft", new Color(0.99f * 0.74f, 0.88f * 0.74f, 0.66f * 0.74f, 1f), FontStyle.BoldAndItalic, TextAnchor.MiddleLeft);

		jumpHorizontalTitleLabel  = 	guiUtils.CreateText(biologyPanel, "Horizontal", new Color(0.816f, 0.537f, 0.18f, 1f), FontStyle.BoldAndItalic, TextAnchor.MiddleLeft);
		jumpHorizontalHeightLabel = 	guiUtils.CreateText(biologyPanel, "50 ft", new Color(0.99f * 0.74f, 0.88f * 0.74f, 0.66f * 0.74f, 1f), FontStyle.BoldAndItalic, TextAnchor.MiddleLeft);

		// Physical Characteristics texture
		physicalCharacteristicsImage =	guiUtils.CreateImage(biologyPanel, physicalCharacteristicsTexture, new Color(1f, 1f, 1f, 1f));
	}

	
	void PositionBiologyItems()
	{
		float panelX = overlayRect.x + overlayRect.width * 0.06f;
		float panelY = overlayRect.y + overlayRect.height * 0.205f;
		float panelWidth = overlayRect.width * 0.88f;
		float panelHeight = overlayRect.height * 0.6575f;
		float yOffset = overlayRect.height * 0.115f;
		float gapSize = overlayRect.width * 0.02f;
		float textOffsetY = panelHeight * 0.035f;
		float textGap = panelWidth * 0.032f;
		float textHeight = panelHeight * 0.05f;
		float rightShift = panelWidth * 0.001f;
		float topRowOffsetY = panelHeight * 0.09f;
		float textureX;
		float textureY;
		float textureHeight;
		float textureWidth;

		// header text
		guiUtils.SetTextOffsets(biologyHeaderTextL, panelX, yOffset + overlayRect.y + overlayRect.height * 0.077f, panelWidth/2 - overlayRect.width * 0.02f - gapSize/2, overlayRect.height * 0.1f, (int)(overlayRect.width * 0.022f));
		guiUtils.SetTextOffsets(biologyHeaderTextR, panelX + panelWidth/2 + overlayRect.width * 0.02f + gapSize/2, yOffset + overlayRect.y + overlayRect.height * 0.077f, panelWidth/2 - overlayRect.width * 0.02f - gapSize/2, overlayRect.height * 0.1f, (int)(overlayRect.width * 0.022f));

		// LEFT PANEL

		// title
		textureX = panelX + panelWidth * 0.34f;
		textureY = panelY + panelHeight * 0.11f;
		textureWidth = panelWidth * 0.1f;
		textureHeight = panelHeight * 0.1f;
		guiUtils.SetItemOffsets(biologyLeftTitleBackground, textGap + panelX + gapSize*0.75f + panelWidth*0.08f, textureY + panelHeight*0.01f, (panelWidth/2 + overlayRect.width*0.02f - gapSize*4.0f) * 0.5f - gapSize*0.5f, textureHeight - panelHeight*0.02f);
		guiUtils.SetTextOffsets(biologyLeftTitleText, textGap + panelX + gapSize*0.75f + panelWidth*0.10f, textureY, (panelWidth/2 + overlayRect.width*0.02f - gapSize*4.0f) * 0.5f - gapSize*0.5f, textureHeight, (int)(overlayRect.width * 0.015f));

		// pumas
		textureY = panelY + panelHeight*0.2f + topRowOffsetY;
		textureHeight = panelHeight * 0.095f;
		textureX = panelX + panelWidth * 0.02f + rightShift;
		textureWidth = closeup1Texture.width * (textureHeight / closeup1Texture.height);
		guiUtils.SetItemOffsets(biologyLeftPumasBackground, panelX + gapSize*0.75f, panelY  + panelHeight * 0.23f, panelWidth/2 + overlayRect.width*0.02f - gapSize*4.0f, panelHeight * 0.22f);
		guiUtils.SetItemOffsets(biologyLeftPumaImagel, textureX, textureY, textureWidth, textureHeight);
		guiUtils.SetTextOffsets(biologyLeftNameText1, textureX, textureY - textureHeight * 0.5f, textureWidth, textHeight, (int)(overlayRect.width * 0.016f));
		textureX = panelX + panelWidth * 0.087f + rightShift;
		textureWidth = closeup2Texture.width * (textureHeight / closeup2Texture.height);
		guiUtils.SetItemOffsets(biologyLeftPumaImage2, textureX, textureY, textureWidth, textureHeight);
		guiUtils.SetTextOffsets(biologyLeftNameText2, textureX, textureY - textureHeight * 0.5f, textureWidth, textHeight, (int)(overlayRect.width * 0.016f));
		textureX = panelX + panelWidth * 0.1675f + rightShift;
		textureWidth = closeup3Texture.width * (textureHeight / closeup3Texture.height);
		guiUtils.SetItemOffsets(biologyLeftPumaImage3, textureX, textureY, textureWidth, textureHeight);
		guiUtils.SetTextOffsets(biologyLeftNameText3, textureX, textureY - textureHeight * 0.5f, textureWidth, textHeight, (int)(overlayRect.width * 0.016f));
		textureX = panelX + panelWidth * 0.2375f + rightShift;
		textureWidth = closeup4Texture.width * (textureHeight / closeup4Texture.height);
		guiUtils.SetItemOffsets(biologyLeftPumaImage4, textureX, textureY, textureWidth, textureHeight);
		guiUtils.SetTextOffsets(biologyLeftNameText4, textureX, textureY - textureHeight * 0.5f, textureWidth, textHeight, (int)(overlayRect.width * 0.016f));
		textureX = panelX + panelWidth * 0.315f + rightShift;
		textureWidth = closeup5Texture.width * (textureHeight / closeup5Texture.height);
		guiUtils.SetItemOffsets(biologyLeftPumaImage5, textureX, textureY, textureWidth, textureHeight);
		guiUtils.SetTextOffsets(biologyLeftNameText5, textureX, textureY - textureHeight * 0.5f, textureWidth, textHeight, (int)(overlayRect.width * 0.016f));
		textureX = panelX + panelWidth * 0.385f + rightShift;
		textureWidth = closeup6Texture.width * (textureHeight / closeup6Texture.height);
		guiUtils.SetItemOffsets(biologyLeftPumaImage6, textureX, textureY, textureWidth, textureHeight);
		guiUtils.SetTextOffsets(biologyLeftNameText6, textureX, textureY - textureHeight * 0.5f, textureWidth, textHeight, (int)(overlayRect.width * 0.016f));
		textureWidth = panelWidth * 0.105f;
		textureY = panelY + panelHeight*0.35f + topRowOffsetY;
		textureX = panelX + panelWidth * 0.025f + rightShift;
		guiUtils.SetTextOffsets(biologyLeftAgeTextLeft, textureX, textureY - textureHeight * 0.5f, textureWidth, textHeight, (int)(overlayRect.width * 0.014f));
		textureX = panelX + panelWidth * 0.1725f + rightShift;
		guiUtils.SetTextOffsets(biologyLeftAgeTextMiddle, textureX, textureY - textureHeight * 0.5f, textureWidth, textHeight, (int)(overlayRect.width * 0.014f));
		textureX = panelX + panelWidth * 0.325f + rightShift;
		guiUtils.SetTextOffsets(biologyLeftAgeTextRight, textureX, textureY - textureHeight * 0.5f, textureWidth, textHeight, (int)(overlayRect.width * 0.014f));

		// speed
		textureX = panelX + panelWidth * 0.02f;
		textureY = panelY + panelHeight * 0.565f;
		textureWidth = panelWidth * 0.1f;
		textureHeight = panelHeight * 0.1f;
		guiUtils.SetItemOffsets(biologyLeftSpeedBgndMain, panelX + gapSize*0.75f, panelY  + panelHeight * 0.57f, (panelWidth/2 + overlayRect.width*0.02f - gapSize*4.0f) * 0.5f - gapSize*0.5f, panelHeight * 0.43f - gapSize*0.75f);
		guiUtils.SetItemOffsets(biologyLeftSpeedBgndTitle, textureX + panelWidth*0.01f, textureY + panelHeight*0.02f, textureWidth - panelWidth*0.015f, textureHeight + panelHeight*0.02f);
		guiUtils.SetItemOffsets(biologyLeftSpeedImage, panelX + panelWidth * 0.118f, panelY + panelHeight * 0.602f, pumaRunTexture.width * ((panelHeight * 0.095f) / pumaRunTexture.height), panelHeight * 0.095f);
		guiUtils.SetTextOffsets(biologyLeftSpeedTextTitle1, textureX, textureY, textureWidth, textureHeight, (int)(overlayRect.width * 0.014f));
		guiUtils.SetTextOffsets(biologyLeftSpeedTextTitle2, panelX + panelWidth * 0.02f, panelY + panelHeight * 0.610f, panelWidth * 0.1f, panelHeight * 0.1f, (int)(overlayRect.width * 0.021f));
		textureX = panelX + panelWidth * 0.34f;
		textureY = panelY + panelHeight * 0.665f + textOffsetY;
		textureWidth = panelWidth * 0.1f;
		textureHeight = panelHeight * 0.1f;
		guiUtils.SetTextOffsets(biologyLeftSpeedTextLine1, textGap + panelX + gapSize*0.75f, textureY, (panelWidth/2 + overlayRect.width*0.02f - gapSize*4.0f) * 0.5f - gapSize*0.5f, textureHeight, (int)(overlayRect.width * 0.016f));
		textureY = panelY + panelHeight * 0.715f + textOffsetY;
		guiUtils.SetTextOffsets(biologyLeftSpeedTextLine2, textGap + panelX + gapSize*0.75f, textureY, (panelWidth/2 + overlayRect.width*0.02f - gapSize*4.0f) * 0.5f - gapSize*0.5f, textureHeight, (int)(overlayRect.width * 0.015f));
		textureY = panelY + panelHeight * 0.78f + textOffsetY;
		guiUtils.SetTextOffsets(biologyLeftSpeedTextLine3, textGap + panelX + gapSize*0.75f, textureY, (panelWidth/2 + overlayRect.width*0.02f - gapSize*4.0f) * 0.5f - gapSize*0.5f, textureHeight, (int)(overlayRect.width * 0.016f));
		textureY = panelY + panelHeight * 0.828f + textOffsetY;
		guiUtils.SetTextOffsets(biologyLeftSpeedTextLine4, textGap + panelX + gapSize*0.75f, textureY, (panelWidth/2 + overlayRect.width*0.02f - gapSize*4.0f) * 0.5f - gapSize*0.5f, textureHeight, (int)(overlayRect.width * 0.015f));

		// stealth
		textureX = panelX + panelWidth * 0.34f;
		textureY = panelY + panelHeight * 0.565f;
		textureWidth = panelWidth * 0.1f;
		textureHeight = panelHeight * 0.1f;
		guiUtils.SetItemOffsets(biologyLeftStealthBgndMain, panelX + gapSize*0.75f + ((panelWidth/2 + overlayRect.width*0.02f - gapSize*4.0f) * 0.5f - gapSize*0.5f) + gapSize, panelY  + panelHeight * 0.57f, (panelWidth/2 + overlayRect.width*0.02f - gapSize*4.0f) * 0.5f - gapSize*0.5f, panelHeight * 0.43f - gapSize*0.75f);
		guiUtils.SetItemOffsets(biologyLeftStealthBgndTitle, textureX + panelWidth*0.007f, textureY + panelHeight*0.02f, textureWidth - panelWidth*0.012f, textureHeight + panelHeight*0.02f);
		guiUtils.SetItemOffsets(biologyLeftStealthImage, panelX + panelWidth * 0.25f, panelY + panelHeight * 0.60f, pumaStealthTexture.width * ((panelHeight * 0.095f) / pumaStealthTexture.height), panelHeight * 0.095f);
		guiUtils.SetTextOffsets(biologyLeftStealthTextTitle1, textureX, textureY, textureWidth, textureHeight, (int)(overlayRect.width * 0.014f));
		guiUtils.SetTextOffsets(biologyLeftStealthTextTitle2, panelX + panelWidth * 0.34f, panelY + panelHeight * 0.610f, panelWidth * 0.1f, panelHeight * 0.1f, (int)(overlayRect.width * 0.021f));
		textureX = panelX + panelWidth * 0.34f;
		textureY = panelY + panelHeight * 0.665f + textOffsetY;
		textureWidth = panelWidth * 0.1f;
		textureHeight = panelHeight * 0.1f;
		guiUtils.SetTextOffsets(biologyLeftStealthTextLine1, textGap + panelX + gapSize*0.75f + ((panelWidth/2 + overlayRect.width*0.02f - gapSize*4.0f) * 0.5f - gapSize*0.5f) + gapSize, textureY, (panelWidth/2 + overlayRect.width*0.02f - gapSize*4.0f) * 0.5f - gapSize*0.5f, textureHeight, (int)(overlayRect.width * 0.016f));
		textureY = panelY + panelHeight * 0.715f + textOffsetY;
		guiUtils.SetTextOffsets(biologyLeftStealthTextLine2, textGap + panelX + gapSize*0.75f + ((panelWidth/2 + overlayRect.width*0.02f - gapSize*4.0f) * 0.5f - gapSize*0.5f) + gapSize, textureY, (panelWidth/2 + overlayRect.width*0.02f - gapSize*4.0f) * 0.5f - gapSize*0.5f, textureHeight, (int)(overlayRect.width * 0.015f));
		textureY = panelY + panelHeight * 0.78f + textOffsetY;
		guiUtils.SetTextOffsets(biologyLeftStealthTextLine3, textGap + panelX + gapSize*0.75f + ((panelWidth/2 + overlayRect.width*0.02f - gapSize*4.0f) * 0.5f - gapSize*0.5f) + gapSize, textureY, (panelWidth/2 + overlayRect.width*0.02f - gapSize*4.0f) * 0.5f - gapSize*0.5f, textureHeight, (int)(overlayRect.width * 0.016f));
		textureY = panelY + panelHeight * 0.828f + textOffsetY;
		guiUtils.SetTextOffsets(biologyLeftStealthTextLine4, textGap + panelX + gapSize*0.75f + ((panelWidth/2 + overlayRect.width*0.02f - gapSize*4.0f) * 0.5f - gapSize*0.5f) + gapSize, textureY, (panelWidth/2 + overlayRect.width*0.02f - gapSize*4.0f) * 0.5f - gapSize*0.5f, textureHeight, (int)(overlayRect.width * 0.015f));

		// arrow
		guiUtils.SetItemOffsets(biologyLeftArrowImage, panelX + panelWidth * 0.135f, panelY + panelHeight * 0.481f, panelWidth * 0.19f, panelHeight * 0.06f);

		// RIGHT PANEL

		// Labels
		textureX = panelX + panelWidth * 0.34f;
		textureY = panelY + panelHeight * 0.11f;
		textureWidth = panelWidth * 0.1f;
		textureHeight = panelHeight * 0.1f;

		// Run
		guiUtils.SetItemOffsets(runTitleBackground, textGap + panelX + gapSize*0.75f + panelWidth*0.8f, textureY + panelHeight*0.11f, (panelWidth/2 + overlayRect.width*0.02f - gapSize*4.0f) * 0.25f - gapSize*0.5f, textureHeight - panelHeight*0.02f);
		guiUtils.SetTextOffsets(runTitleLabel, textGap + panelX + gapSize*0.75f + panelWidth*0.82f, textureY + panelHeight*0.1f, (panelWidth/2 + overlayRect.width*0.02f - gapSize*4.0f) * 0.25f - gapSize*0.5f, textureHeight, (int)(overlayRect.width * 0.025f));
		guiUtils.SetTextOffsets(runSpeedLabel, textGap + panelX + gapSize*0.75f + panelWidth*0.82f, textureY + panelHeight*0.155f, (panelWidth/2 + overlayRect.width*0.02f - gapSize*4.0f) * 0.25f - gapSize*0.5f, textureHeight, (int)(overlayRect.width * 0.015f));

		// Jump
		guiUtils.SetItemOffsets(jumpTitleBackground, textGap + panelX + gapSize*0.75f + panelWidth*0.65f, textureY + panelHeight*0.71f, (panelWidth/2 +overlayRect.width*0.02f - gapSize*4.0f) * 0.3f - gapSize*0.5f, textureHeight - panelHeight*0.02f);
		guiUtils.SetTextOffsets(jumpTitleLabel, textGap + panelX + gapSize*0.75f + panelWidth*0.67f, textureY + panelHeight*0.7f, (panelWidth/2 + overlayRect.width*0.02f - gapSize*4.0f) * 0.25f - gapSize*0.5f, textureHeight, (int)(overlayRect.width * 0.025f));

		// Horizontal
		guiUtils.SetTextOffsets(jumpHorizontalTitleLabel, textGap + panelX + gapSize*0.75f + panelWidth*0.525f, textureY + panelHeight*0.76f, (panelWidth/2 + overlayRect.width*0.02f - gapSize*4.0f) * 0.25f - gapSize*0.5f, textureHeight, (int)(overlayRect.width * 0.0175f));
		guiUtils.SetTextOffsets(jumpHorizontalHeightLabel, textGap + panelX + gapSize*0.75f + panelWidth*0.56f, textureY + panelHeight*0.725f, (panelWidth/2 + overlayRect.width*0.02f - gapSize*4.0f) * 0.25f - gapSize*0.5f, textureHeight, (int)(overlayRect.width * 0.015f));

		// Vertical
		guiUtils.SetTextOffsets(jumpVerticalTitleLabel, textGap + panelX + gapSize*0.75f + panelWidth*0.775f, textureY + panelHeight*0.4f, (panelWidth/2 + overlayRect.width*0.02f - gapSize*4.0f) * 0.25f - gapSize*0.5f, textureHeight, (int)(overlayRect.width * 0.0175f));
		guiUtils.SetTextOffsets(jumpVerticalHeightLabel, textGap + panelX + gapSize*0.75f + panelWidth*0.79f, textureY + panelHeight*0.435f, (panelWidth/2 + overlayRect.width*0.02f - gapSize*4.0f) * 0.25f - gapSize*0.5f, textureHeight, (int)(overlayRect.width * 0.015f));


		// Image
		guiUtils.SetItemOffsets(physicalCharacteristicsImage, panelX + panelWidth*0.575f, textureY + panelHeight*0.01f, panelWidth*0.35f, panelWidth*0.35f);
	}

	
	void UpdateBiologyItems()
	{
		biologyPanel.SetActive((newLevelFlag == false && currentLevel != 6 && currentScreen == 0) ? true : false);
	}	

	
	void DrawBiologyScreen(float panelOpacity) 
	{ 
		//////////////////
		//////////////////
		// OLD GUI CODE
		//////////////////
		//////////////////

		float panelX = overlayRect.x + overlayRect.width * 0.06f;
		float panelY = overlayRect.y + overlayRect.height * 0.205f;
		float panelWidth = overlayRect.width * 0.88f;
		float panelHeight = overlayRect.height * 0.6575f;
		float fontScale = 0.8f;

		GUIStyle style = new GUIStyle();
		style.alignment = TextAnchor.MiddleCenter;

		float yOffset = overlayRect.height * 0.022f;

		if (USE_NEW_GUI == false) {
			// two labels at top of panel	
			GUI.color = new Color(0f, 0f, 0f, 1f * panelOpacity);
			GUI.Box(new Rect(panelX - overlayRect.width * 0.02f, yOffset + overlayRect.y + overlayRect.height * 0.095f, overlayRect.width * 0.14f, overlayRect.height * 0.064f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * panelOpacity);
			style.fontSize = (int)(overlayRect.width * 0.021f);
			style.fontStyle = FontStyle.BoldAndItalic;
			style.normal.textColor = new Color(0.816f, 0.537f, 0.18f, 1f);
			GUI.Button(new Rect(panelX - overlayRect.width * 0.02f, yOffset + overlayRect.y + overlayRect.height * 0.077f, overlayRect.width * 0.14f, overlayRect.height * 0.1f), "In Game", style);

			GUI.color = new Color(0f, 0f, 0f, 1f * panelOpacity);
			GUI.Box(new Rect(overlayRect.x + overlayRect.width - overlayRect.width * 0.2f, yOffset + overlayRect.y + overlayRect.height * 0.095f, overlayRect.width * 0.16f, overlayRect.height * 0.064f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * panelOpacity);
			style.fontSize = (int)(overlayRect.width * 0.021f);
			style.fontStyle = FontStyle.BoldAndItalic;
			style.normal.textColor = new Color(0.816f, 0.537f, 0.18f, 1f);
			GUI.Button(new Rect(overlayRect.x + overlayRect.width - overlayRect.width * 0.2f, yOffset + overlayRect.y + overlayRect.height * 0.077f, overlayRect.width * 0.16f, overlayRect.height * 0.1f), "Real World", style);
		}

		// background rectangles
		GUI.color = new Color(1f, 1f, 1f, 1f * panelOpacity);
		float gapSize = overlayRect.width * 0.02f;
		
		//left side
		if (USE_NEW_GUI == false) {
			GUI.color = new Color(0f, 0f, 0f, 1f * panelOpacity);
			GUI.Box(new Rect(panelX - overlayRect.width * 0.02f, panelY - overlayRect.height * 0.025f, panelWidth/2 + overlayRect.width * 0.02f - gapSize/2, panelHeight + overlayRect.height * 0.05f), "");
			GUI.color = new Color(0f, 0f, 0f, 0.5f * panelOpacity);
			GUI.Box(new Rect(panelX, panelY, panelWidth/2 - overlayRect.width * 0.02f - gapSize/2, panelHeight), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * panelOpacity);
		
			GUI.Box(new Rect(panelX + gapSize*0.75f, panelY  + panelHeight * 0.23f, panelWidth/2 + overlayRect.width*0.02f - gapSize*4.0f, panelHeight * 0.22f), "");
			GUI.Box(new Rect(panelX + gapSize*0.75f, panelY  + panelHeight * 0.57f, (panelWidth/2 + overlayRect.width*0.02f - gapSize*4.0f) * 0.5f - gapSize*0.5f, panelHeight * 0.43f - gapSize*0.75f), "");
			GUI.Box(new Rect(panelX + gapSize*0.75f + ((panelWidth/2 + overlayRect.width*0.02f - gapSize*4.0f) * 0.5f - gapSize*0.5f) + gapSize, panelY  + panelHeight * 0.57f, (panelWidth/2 + overlayRect.width*0.02f - gapSize*4.0f) * 0.5f - gapSize*0.5f, panelHeight * 0.43f - gapSize*0.75f), "");
		}

		
		// right side
		if (USE_NEW_GUI == false) {
			GUI.color = new Color(0f, 0f, 0f, 1f * panelOpacity);
			GUI.Box(new Rect(panelX + panelWidth/2 + gapSize/2, panelY - overlayRect.height * 0.025f, panelWidth/2 + overlayRect.width * 0.02f - gapSize/2, panelHeight + overlayRect.height * 0.05f), "");
			GUI.color = new Color(0f, 0f, 0f, 0.5f * panelOpacity);
			GUI.Box(new Rect(panelX + panelWidth/2 + overlayRect.width * 0.02f + gapSize/2, panelY, panelWidth/2 - overlayRect.width * 0.02f - gapSize/2, panelHeight), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * panelOpacity);
		}
		
		
		// HEADER TEXT 
		
		string headerTextL = "Speed and Stealth";
		string headerTextR = "Physical Characteristics";
		
		yOffset = overlayRect.height * 0.115f;
		float colorScale = 0.74f;
			
		if (USE_NEW_GUI == false) {
			GUI.color = new Color(1f, 1f, 1f, 1f * panelOpacity);
			style.fontSize = (int)(overlayRect.width * 0.022f);
			style.fontStyle = FontStyle.Normal;
			style.normal.textColor = new Color(0.99f * colorScale, 0.88f * colorScale, 0.66f * colorScale, 1f);
			GUI.Button(new Rect(panelX, yOffset + overlayRect.y + overlayRect.height * 0.077f, panelWidth/2 - overlayRect.width * 0.02f - gapSize/2, overlayRect.height * 0.1f), headerTextL, style);

			GUI.color = new Color(1f, 1f, 1f, 1f * panelOpacity);
			style.fontSize = (int)(overlayRect.width * 0.022f);
			style.fontStyle = FontStyle.Normal;
			style.normal.textColor = new Color(0.99f * colorScale, 0.88f * colorScale, 0.66f * colorScale, 1f);
			GUI.Button(new Rect(panelX + panelWidth/2 + overlayRect.width * 0.02f + gapSize/2, yOffset + overlayRect.y + overlayRect.height * 0.077f, panelWidth/2 - overlayRect.width * 0.02f - gapSize/2, overlayRect.height * 0.1f), headerTextR, style);
		}

		float topRowOffsetY = panelHeight * 0.09f;
		
		

		// BLACK ARROW

		float textureX;
		float textureY;
		float textureHeight;
		float textureWidth;

		if (USE_NEW_GUI == false) {
			GUI.color = new Color(0.5f, 0.5f, 0.5f, 1f * panelOpacity);
			textureX = panelX + panelWidth * 0.135f;
			textureY = panelY + panelHeight * 0.481f;
			textureHeight = panelHeight * 0.06f;
			textureWidth = panelWidth * 0.19f;
			GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), blackArrowTexture);
		}


		
		// SPEED AND STEALTH
		
		GUI.color = new Color(1f, 1f, 1f, panelOpacity);

		if (USE_NEW_GUI == false) {
			// icons
			textureX = panelX + panelWidth * 0.118f;
			textureY = panelY + panelHeight * 0.602f;
			textureHeight = panelHeight * 0.095f;
			textureWidth = pumaRunTexture.width * (textureHeight / pumaRunTexture.height);
			GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), pumaRunTexture);
			textureX = panelX + panelWidth * 0.25f;
			textureY = panelY + panelHeight * 0.60f;
			textureWidth = pumaStealthTexture.width * (textureHeight / pumaStealthTexture.height);
			GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), pumaStealthTexture);
		}

		
		// labels
		
		colorScale = 0.74f;

		if (USE_NEW_GUI == false) {
			style.normal.textColor = new Color(0.99f * colorScale, 0.88f * colorScale, 0.66f * colorScale, 1f);
			style.fontSize = (int)(overlayRect.width * 0.014f);
			style.fontStyle = FontStyle.BoldAndItalic;
			style.alignment = TextAnchor.MiddleCenter;
			textureX = panelX + panelWidth * 0.02f;
			textureY = panelY + panelHeight * 0.565f;
			textureWidth = panelWidth * 0.1f;
			textureHeight = panelHeight * 0.1f;
			GUI.Box(new Rect(textureX + panelWidth*0.01f, textureY + panelHeight*0.02f, textureWidth - panelWidth*0.015f, textureHeight + panelHeight*0.02f), "");
			GUI.Button(new Rect(textureX, textureY, textureWidth, textureHeight), "Excels at", style);	
			textureX = panelX + panelWidth * 0.34f;
			GUI.Box(new Rect(textureX + panelWidth*0.007f, textureY + panelHeight*0.02f, textureWidth - panelWidth*0.012f, textureHeight + panelHeight*0.02f), "");
			GUI.Button(new Rect(textureX, textureY, textureWidth, textureHeight), "Excels at", style);

			style.fontSize = (int)(overlayRect.width * 0.021f);
			style.fontStyle = FontStyle.BoldAndItalic;
			style.normal.textColor = new Color(0.816f, 0.537f, 0.18f, 1f);
			textureX = panelX + panelWidth * 0.02f;
			textureY = panelY + panelHeight * 0.610f;
			textureWidth = panelWidth * 0.1f;
			textureHeight = panelHeight * 0.1f;
			GUI.Button(new Rect(textureX, textureY, textureWidth, textureHeight), "Speed", style);	
			textureX = panelX + panelWidth * 0.34f;
			GUI.Button(new Rect(textureX, textureY, textureWidth, textureHeight), "Stealth", style);
		}
		textureHeight = panelHeight * 0.1f;

		
		// explanatory text
		
		float textOffsetY = panelHeight * 0.035f;
		float textGap = panelWidth * 0.032f;
		style.alignment = TextAnchor.MiddleLeft;
		style.fontSize = (int)(overlayRect.width * 0.015f);
		style.fontStyle = FontStyle.BoldAndItalic;
		
		if (USE_NEW_GUI == false) {
			style.normal.textColor = new Color(0.84f, 0.61f, 0f, 0.9f);
			textureY = panelY + panelHeight * 0.11f;
			GUI.Box(new Rect(textGap + panelX + gapSize*0.75f + panelWidth*0.08f, textureY + panelHeight*0.01f, (panelWidth/2 + overlayRect.width*0.02f - gapSize*4.0f) * 0.5f - gapSize*0.5f, textureHeight - panelHeight*0.02f), "");
			GUI.Button(new Rect(textGap + panelX + gapSize*0.75f + panelWidth*0.10f, textureY, (panelWidth/2 + overlayRect.width*0.02f - gapSize*4.0f) * 0.5f - gapSize*0.5f, textureHeight), "When hunting deer...", style);
			style.fontSize = (int)(overlayRect.width * 0.016f);
			style.fontStyle = FontStyle.Bold;
			style.normal.textColor = new Color(0.99f * colorScale, 0.88f * colorScale, 0.66f * colorScale, 1f);
			textureY = panelY + panelHeight * 0.665f + textOffsetY;
			GUI.Button(new Rect(textGap + panelX + gapSize*0.75f, textureY, (panelWidth/2 + overlayRect.width*0.02f - gapSize*4.0f) * 0.5f - gapSize*0.5f, textureHeight), "Young cats can", style);
			textureY = panelY + panelHeight * 0.715f + textOffsetY;
			GUI.Button(new Rect(textGap + panelX + gapSize*0.75f, textureY, (panelWidth/2 + overlayRect.width*0.02f - gapSize*4.0f) * 0.5f - gapSize*0.5f, textureHeight), "run very fast", style);
			style.fontSize = (int)(overlayRect.width * 0.015f);
			style.fontStyle = FontStyle.Normal;
			textureY = panelY + panelHeight * 0.78f + textOffsetY;
			GUI.Button(new Rect(textGap + panelX + gapSize*0.75f, textureY, (panelWidth/2 + overlayRect.width*0.02f - gapSize*4.0f) * 0.5f - gapSize*0.5f, textureHeight), "...but they can't", style);
			textureY = panelY + panelHeight * 0.828f + textOffsetY;
			GUI.Button(new Rect(textGap + panelX + gapSize*0.75f, textureY, (panelWidth/2 + overlayRect.width*0.02f - gapSize*4.0f) * 0.5f - gapSize*0.5f, textureHeight), "sneak up close", style);
			
			style.fontSize = (int)(overlayRect.width * 0.016f);
			style.fontStyle = FontStyle.Bold;
			textureY = panelY + panelHeight * 0.665f + textOffsetY;
			GUI.Button(new Rect(textGap + panelX + gapSize*0.75f + ((panelWidth/2 + overlayRect.width*0.02f - gapSize*4.0f) * 0.5f - gapSize*0.5f) + gapSize, textureY, (panelWidth/2 + overlayRect.width*0.02f - gapSize*4.0f) * 0.5f - gapSize*0.5f, textureHeight), "Older cats can", style);
			textureY = panelY + panelHeight * 0.715f + textOffsetY;
			GUI.Button(new Rect(textGap + panelX + gapSize*0.75f + ((panelWidth/2 + overlayRect.width*0.02f - gapSize*4.0f) * 0.5f - gapSize*0.5f) + gapSize, textureY, (panelWidth/2 + overlayRect.width*0.02f - gapSize*4.0f) * 0.5f - gapSize*0.5f, textureHeight), "sneak up close", style);
			style.fontSize = (int)(overlayRect.width * 0.015f);
			style.fontStyle = FontStyle.Normal;
			textureY = panelY + panelHeight * 0.78f + textOffsetY;
			GUI.Button(new Rect(textGap + panelX + gapSize*0.75f + ((panelWidth/2 + overlayRect.width*0.02f - gapSize*4.0f) * 0.5f - gapSize*0.5f) + gapSize, textureY, (panelWidth/2 + overlayRect.width*0.02f - gapSize*4.0f) * 0.5f - gapSize*0.5f, textureHeight), "...but they can't", style);
			textureY = panelY + panelHeight * 0.828f + textOffsetY;
			GUI.Button(new Rect(textGap + panelX + gapSize*0.75f + ((panelWidth/2 + overlayRect.width*0.02f - gapSize*4.0f) * 0.5f - gapSize*0.5f) + gapSize, textureY, (panelWidth/2 + overlayRect.width*0.02f - gapSize*4.0f) * 0.5f - gapSize*0.5f, textureHeight), "run very fast", style);
			style.alignment = TextAnchor.MiddleCenter;
		}



		// PUMA HEADS and NAMES
		
		Texture2D headshotTexture;
		float rightShift = panelWidth * 0.001f;
				
		GUI.color = new Color(1f, 1f, 1f, panelOpacity);
		textureY = panelY + panelHeight*0.2f + topRowOffsetY;
		textureHeight = panelHeight * 0.095f;
		float textHeight = panelHeight * 0.05f;

		style.normal.textColor = new Color(0.88f, 0.64f, 0f, 0.85f);
		style.fontSize = (int)(overlayRect.width * 0.016f);
		style.fontStyle = FontStyle.Italic;
		style.alignment = TextAnchor.MiddleCenter;

		if (USE_NEW_GUI == false) {
			// textures 1-6
			headshotTexture = closeup1Texture;
			textureX = panelX + panelWidth * 0.02f + rightShift;
			textureWidth = headshotTexture.width * (textureHeight / headshotTexture.height);
			GUI.color = new Color(1f, 1f, 1f, 0.90f * panelOpacity);
			GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), headshotTexture);
			GUI.Button(new Rect(textureX, textureY - textureHeight * 0.5f, textureWidth, textHeight), "Eric", style);

			headshotTexture = closeup2Texture;
			textureX = panelX + panelWidth * 0.087f + rightShift;
			textureWidth = headshotTexture.width * (textureHeight / headshotTexture.height);
			GUI.color = new Color(1f, 1f, 1f, 0.90f * panelOpacity);
			GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), headshotTexture);
			GUI.Button(new Rect(textureX, textureY - textureHeight * 0.5f, textureWidth, textHeight), "Palo", style);
			
			headshotTexture = closeup3Texture;
			textureX = panelX + panelWidth * 0.1675f + rightShift;
			textureWidth = headshotTexture.width * (textureHeight / headshotTexture.height);
			GUI.color = new Color(1f, 1f, 1f, 0.80f * panelOpacity);
			GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), headshotTexture);
			GUI.Button(new Rect(textureX, textureY - textureHeight * 0.5f, textureWidth, textHeight), "Mitch", style);
			
			headshotTexture = closeup4Texture;
			textureX = panelX + panelWidth * 0.2375f + rightShift;
			textureWidth = headshotTexture.width * (textureHeight / headshotTexture.height);
			GUI.color = new Color(1f, 1f, 1f, 0.80f * panelOpacity);
			GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), headshotTexture);
			GUI.Button(new Rect(textureX, textureY - textureHeight * 0.5f, textureWidth, textHeight), "Trish", style);
			
			headshotTexture = closeup5Texture;
			textureX = panelX + panelWidth * 0.315f + rightShift;
			textureWidth = headshotTexture.width * (textureHeight / headshotTexture.height);
			GUI.color = new Color(1f, 1f, 1f, 0.85f * panelOpacity);
			GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), headshotTexture);
			GUI.Button(new Rect(textureX, textureY - textureHeight * 0.5f, textureWidth, textHeight), "Liam", style);
			
			headshotTexture = closeup6Texture;
			textureX = panelX + panelWidth * 0.385f + rightShift;
			textureWidth = headshotTexture.width * (textureHeight / headshotTexture.height);
			GUI.color = new Color(1f, 1f, 1f, 0.90f * panelOpacity);
			GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), headshotTexture);
			GUI.Button(new Rect(textureX, textureY - textureHeight * 0.5f, textureWidth, textHeight), "Barb", style);
		}

		
		// PUMA AGES

		textureWidth = panelWidth * 0.105f;
		textureY = panelY + panelHeight*0.35f + topRowOffsetY;
		
		colorScale = 0.74f;
		style.normal.textColor = new Color(0.99f * colorScale, 0.88f * colorScale, 0.66f * colorScale, 1f);
		style.fontSize = (int)(overlayRect.width * 0.014f);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.MiddleCenter;


		if (USE_NEW_GUI == false) {
			textureX = panelX + panelWidth * 0.025f + rightShift;
			GUI.Button(new Rect(textureX, textureY - textureHeight * 0.5f, textureWidth, textHeight), "2 Years Old", style);
			
			textureX = panelX + panelWidth * 0.1725f + rightShift;
			GUI.Button(new Rect(textureX, textureY - textureHeight * 0.5f, textureWidth, textHeight), "5 Years Old", style);

			textureX = panelX + panelWidth * 0.325f + rightShift;
			GUI.Button(new Rect(textureX, textureY - textureHeight * 0.5f, textureWidth, textHeight), "8 Years Old", style);
		}
	}	
	

	//======================================================================
	//======================================================================
	//======================================================================
	//								PREDATION
	//======================================================================
	//======================================================================
	//======================================================================
	
	GameObject predationPanel;
	GameObject predationHeaderTextL;
	GameObject predationHeaderTextR;

	// LEFT PANEL
	
	// three panels
	GameObject predationLeftUpperPanelArrow;
	GameObject predationLeftUpperPanelBgnd;
	GameObject predationLeftUpperPanelText1;
	GameObject predationLeftUpperPanelText2;
	GameObject predationLeftUpperPanelText3;
	GameObject predationLeftUpperPanelText4;

	GameObject predationLeftMiddlePanelArrow;
	GameObject predationLeftMiddlePanelBgnd;
	GameObject predationLeftMiddlePanelText1;
	GameObject predationLeftMiddlePanelText2;
	GameObject predationLeftMiddlePanelText3;
	GameObject predationLeftMiddlePanelText4;

	GameObject predationLeftBottomPanelArrow;
	GameObject predationLeftBottomPanelBgnd;
	GameObject predationLeftBottomPanelText1;
	GameObject predationLeftBottomPanelText2;
	GameObject predationLeftBottomPanelText3;
	GameObject predationLeftBottomPanelText4;

	// diagram
	GameObject predationLeftDiagramBgnd;
	GameObject predationLeftDiagramBuck;
	GameObject predationLeftDiagramDoe;
	GameObject predationLeftDiagramFawn;
	GameObject predationLeftDiagramArrows;

	// attack
	GameObject predationLeftAttackBgnd;
	GameObject predationLeftAttackImage;

	// RIGHT PANEL

	GameObject healthyLabelPanel;
	GameObject healthyLabelText;

	GameObject unhealthyLabelPanel;
	GameObject unhealthyLabelText;

	GameObject pumasAndPredationImage;


	void CreatePredationItems()
	{
		float panelX = overlayRect.x + overlayRect.width * 0.06f;
		float panelY = overlayRect.y + overlayRect.height * 0.205f;
		float panelWidth = overlayRect.width * 0.88f;
		float panelHeight = overlayRect.height * 0.6575f;
		float gapSize = overlayRect.width * 0.02f;
		float upperY = panelY  + panelHeight * 0.13f;
		float middleY = panelY  + panelHeight * 0.44f;
		float lowerY = panelY  + panelHeight * 0.75f;			

		// invisible panel to hold items
		predationPanel = (GameObject)Instantiate(uiSubPanel);
		predationPanel.GetComponent<RectTransform>().SetParent(infoPanelMainPanel.GetComponent<RectTransform>(), false);
	
		// header text
		predationHeaderTextL = 			guiUtils.CreateText(predationPanel, "Catching Deer", new Color(0.99f * 0.82f, 0.88f * 0.82f, 0.66f * 0.82f, 1f), FontStyle.Normal);
		predationHeaderTextR = 			guiUtils.CreateText(predationPanel, "Pumas and Prey", new Color(0.99f * 0.82f, 0.88f * 0.82f, 0.66f * 0.82f, 1f), FontStyle.Normal);

		// LEFT PANEL
		
		// three panels
		predationLeftUpperPanelArrow = 	guiUtils.CreateImage(predationPanel, blackArrowShortTexture, new Color(0.5f, 0.5f, 0.5f, 1f));
		predationLeftUpperPanelBgnd = 	guiUtils.CreatePanel(predationPanel, new Color(0f, 0f, 0f, 0.4f));
		predationLeftUpperPanelText1 = 	guiUtils.CreateText(predationPanel, "Sneak", new Color(0.816f * 1.1f, 0.537f * 1.1f, 0.18f * 1.1f, 1f), FontStyle.BoldAndItalic, TextAnchor.MiddleLeft);
		predationLeftUpperPanelText2 = 	guiUtils.CreateText(predationPanel, "around herd...", new Color(0.88f, 0.64f, 0f, 0.9f), FontStyle.BoldAndItalic, TextAnchor.MiddleLeft);
		predationLeftUpperPanelText3 = 	guiUtils.CreateText(predationPanel, "Hold LEFT BUTTON", new Color(0.99f * 0.65f, 0.88f * 0.65f, 0.66f * 0.65f, 1f), FontStyle.Bold, TextAnchor.MiddleLeft);
		predationLeftUpperPanelText4 = 	guiUtils.CreateText(predationPanel, "for diagonal walk", new Color(0.99f * 0.65f, 0.88f * 0.65f, 0.66f * 0.65f, 1f), FontStyle.Bold, TextAnchor.MiddleLeft);

		predationLeftMiddlePanelArrow = guiUtils.CreateImage(predationPanel, blackArrowShortTexture, new Color(0.5f, 0.5f, 0.5f, 1f));
		predationLeftMiddlePanelBgnd = 	guiUtils.CreatePanel(predationPanel, new Color(0f, 0f, 0f, 0.4f));
		predationLeftMiddlePanelText1 = guiUtils.CreateText(predationPanel, "Attack", new Color(0.816f * 1.1f, 0.537f * 1.1f, 0.18f * 1.1f, 1f), FontStyle.BoldAndItalic, TextAnchor.MiddleLeft);
		predationLeftMiddlePanelText2 = guiUtils.CreateText(predationPanel, "from behind...", new Color(0.88f, 0.64f, 0f, 0.9f), FontStyle.BoldAndItalic, TextAnchor.MiddleLeft);
		predationLeftMiddlePanelText3 = guiUtils.CreateText(predationPanel, "Go straight in", new Color(0.99f * 0.65f, 0.88f * 0.65f, 0.66f * 0.65f, 1f), FontStyle.Bold, TextAnchor.MiddleLeft);
		predationLeftMiddlePanelText4 = guiUtils.CreateText(predationPanel, "toward the rear", new Color(0.99f * 0.65f, 0.88f * 0.65f, 0.66f * 0.65f, 1f), FontStyle.Bold, TextAnchor.MiddleLeft);

		predationLeftBottomPanelArrow = guiUtils.CreateImage(predationPanel, blackArrowShortTexture, new Color(0.5f, 0.5f, 0.5f, 1f));
		predationLeftBottomPanelBgnd = 	guiUtils.CreatePanel(predationPanel, new Color(0f, 0f, 0f, 0.4f));
		predationLeftBottomPanelText1 = guiUtils.CreateText(predationPanel, "Jump", new Color(0.816f * 1.1f, 0.537f * 1.1f, 0.18f * 1.1f, 1f), FontStyle.BoldAndItalic, TextAnchor.MiddleLeft);
		predationLeftBottomPanelText2 = guiUtils.CreateText(predationPanel, "to take prey!", new Color(0.88f, 0.64f, 0f, 0.9f), FontStyle.BoldAndItalic, TextAnchor.MiddleLeft);
		predationLeftBottomPanelText3 = guiUtils.CreateText(predationPanel, "Tap LEFT BUTTON", new Color(0.99f * 0.65f, 0.88f * 0.65f, 0.66f * 0.65f, 1f), FontStyle.Bold, TextAnchor.MiddleLeft);
		predationLeftBottomPanelText4 = guiUtils.CreateText(predationPanel, "to take down prey", new Color(0.99f * 0.65f, 0.88f * 0.65f, 0.66f * 0.65f, 1f), FontStyle.Bold, TextAnchor.MiddleLeft);

		// diagram
		predationLeftDiagramBgnd = 		guiUtils.CreateImage(predationPanel, predationCirclesTexture, new Color(1f, 1f, 1f, 0.75f));
		predationLeftDiagramBuck = 		guiUtils.CreateImage(predationPanel, buckStandTexture, new Color(1f, 1f, 1f, 0.75f));
		predationLeftDiagramDoe = 		guiUtils.CreateImage(predationPanel, doeStandTexture, new Color(1f, 1f, 1f, 0.75f));
		predationLeftDiagramFawn = 		guiUtils.CreateImage(predationPanel, fawnStandTexture, new Color(1f, 1f, 1f, 0.75f));
		predationLeftDiagramArrows = 	guiUtils.CreateImage(predationPanel, predationArrowsTexture, new Color(1f, 1f, 1f, 0.75f));

		// attack
		predationLeftAttackBgnd = 		guiUtils.CreatePanel(predationPanel, new Color(0f, 0f, 0f, 0.7f));
		predationLeftAttackImage = 		guiUtils.CreateImage(predationPanel, pumaAttackTexture, new Color(1f, 1f, 1f, 0.7f));

		// RIGHT PANEL

		// Labels
		healthyLabelPanel =				guiUtils.CreatePanel(predationPanel, new Color(0f, 0f, 0f, 0.4f));
		healthyLabelText = 				guiUtils.CreateText(predationPanel, "Healthy", new Color(0f, 0.74f, 0, 0.80f), FontStyle.BoldAndItalic);

		unhealthyLabelPanel =			guiUtils.CreatePanel(predationPanel, new Color(0f, 0f, 0f, 0.4f));
		unhealthyLabelText = 			guiUtils.CreateText(predationPanel, "Starving", new Color(0.88f, 0, 0, 0.85f), FontStyle.BoldAndItalic);

		pumasAndPredationImage = 		guiUtils.CreateImage(predationPanel, pumasAndPredationTexture, new Color(1f, 1f, 1f, 1f));
	}

	
	void PositionPredationItems()
	{
		float panelX = overlayRect.x + overlayRect.width * 0.06f;
		float panelY = overlayRect.y + overlayRect.height * 0.205f;
		float panelWidth = overlayRect.width * 0.88f;
		float panelHeight = overlayRect.height * 0.6575f;
		float yOffset = overlayRect.height * 0.115f;
		float gapSize = overlayRect.width * 0.02f;
		float upperY = panelY  + panelHeight * 0.13f;
		float middleY = panelY  + panelHeight * 0.44f;
		float lowerY = panelY  + panelHeight * 0.75f;			
		float textGap = panelWidth * 0.012f;

		guiUtils.SetItemOffsets(predationHeaderTextL, panelX, yOffset + overlayRect.y + overlayRect.height * 0.077f, panelWidth/2 - overlayRect.width * 0.02f - gapSize/2, overlayRect.height * 0.1f);
		predationHeaderTextL.GetComponent<Text>().fontSize = (int)(overlayRect.width * 0.022f);

		guiUtils.SetItemOffsets(predationHeaderTextR, panelX + panelWidth/2 + overlayRect.width * 0.02f + gapSize/2, yOffset + overlayRect.y + overlayRect.height * 0.077f, panelWidth/2 - overlayRect.width * 0.02f - gapSize/2, overlayRect.height * 0.1f);
		predationHeaderTextR.GetComponent<Text>().fontSize = (int)(overlayRect.width * 0.022f);
	
		// LEFT PANEL
		
		// three panels
		float textureX = panelX + panelWidth * 0.02f;
		float textureWidth = panelWidth * 0.1f;
		float textureHeight = panelHeight * 0.1f;
		guiUtils.SetItemOffsets(predationLeftUpperPanelArrow, panelX + panelWidth * 0.160f, upperY + panelHeight * 0.135f, panelWidth * 0.05f, panelHeight * 0.06f);
		guiUtils.SetItemOffsets(predationLeftUpperPanelBgnd, panelX + gapSize*0.75f, upperY, panelWidth * 0.145f, panelHeight * 0.25f - gapSize*0.75f);
		float textureY = panelY + panelHeight * 0.110f;
		guiUtils.SetTextOffsets(predationLeftUpperPanelText1, textGap + panelX + gapSize*0.75f, upperY + panelHeight * 0.0f, panelWidth * 0.3f, panelHeight * 0.08f, (int)(overlayRect.width * 0.021f));
		guiUtils.SetTextOffsets(predationLeftUpperPanelText2, textGap + panelX + gapSize*0.75f, upperY + panelHeight * 0.045f, panelWidth * 0.3f, panelHeight * 0.08f, (int)(overlayRect.width * 0.016f));
		guiUtils.SetTextOffsets(predationLeftUpperPanelText3, textGap + panelX + gapSize*0.75f, upperY + panelHeight * 0.09f, panelWidth * 0.3f, panelHeight * 0.1f, (int)(overlayRect.width * 0.012f));
		guiUtils.SetTextOffsets(predationLeftUpperPanelText4, textGap + panelX + gapSize*0.75f, upperY + panelHeight * 0.125f, panelWidth * 0.3f, panelHeight * 0.1f, (int)(overlayRect.width * 0.012f));

		guiUtils.SetItemOffsets(predationLeftMiddlePanelArrow, panelX + panelWidth * 0.160f, middleY + panelHeight * 0.135f, panelWidth * 0.05f, panelHeight * 0.06f);
		guiUtils.SetItemOffsets(predationLeftMiddlePanelBgnd, panelX + gapSize*0.75f, middleY, panelWidth * 0.145f, panelHeight * 0.25f - gapSize*0.75f);
		textureY = panelY + panelHeight * 0.510f;
		guiUtils.SetTextOffsets(predationLeftMiddlePanelText1, textGap + panelX + gapSize*0.75f, middleY + panelHeight * 0.0f, panelWidth * 0.3f, panelHeight * 0.08f, (int)(overlayRect.width * 0.021f));
		guiUtils.SetTextOffsets(predationLeftMiddlePanelText2, textGap + panelX + gapSize*0.75f, middleY + panelHeight * 0.045f, panelWidth * 0.3f, panelHeight * 0.08f, (int)(overlayRect.width * 0.016f));
		guiUtils.SetTextOffsets(predationLeftMiddlePanelText3, textGap + panelX + gapSize*0.75f, middleY + panelHeight * 0.09f, panelWidth * 0.3f, panelHeight * 0.1f, (int)(overlayRect.width * 0.012f));
		guiUtils.SetTextOffsets(predationLeftMiddlePanelText4, textGap + panelX + gapSize*0.75f, middleY + panelHeight * 0.125f, panelWidth * 0.3f, panelHeight * 0.1f, (int)(overlayRect.width * 0.012f));

		guiUtils.SetItemOffsets(predationLeftBottomPanelArrow, panelX + panelWidth * 0.160f, lowerY + panelHeight * 0.135f, panelWidth * 0.05f, panelHeight * 0.06f);
		guiUtils.SetItemOffsets(predationLeftBottomPanelBgnd, panelX + gapSize*0.75f, lowerY, panelWidth * 0.145f, panelHeight * 0.25f - gapSize*0.75f);
		textureY = panelY + panelHeight * 0.510f;
		guiUtils.SetTextOffsets(predationLeftBottomPanelText1, textGap + panelX + gapSize*0.75f, lowerY + panelHeight * 0.0f, panelWidth * 0.3f, panelHeight * 0.08f, (int)(overlayRect.width * 0.021f));
		guiUtils.SetTextOffsets(predationLeftBottomPanelText2, textGap + panelX + gapSize*0.75f, lowerY + panelHeight * 0.045f, panelWidth * 0.3f, panelHeight * 0.08f, (int)(overlayRect.width * 0.016f));
		guiUtils.SetTextOffsets(predationLeftBottomPanelText3, textGap + panelX + gapSize*0.75f, lowerY + panelHeight * 0.09f, panelWidth * 0.3f, panelHeight * 0.1f, (int)(overlayRect.width * 0.012f));
		guiUtils.SetTextOffsets(predationLeftBottomPanelText4, textGap + panelX + gapSize*0.75f, lowerY + panelHeight * 0.125f, panelWidth * 0.3f, panelHeight * 0.1f, (int)(overlayRect.width * 0.012f));

		// diagram
		guiUtils.SetItemOffsets(predationLeftDiagramBgnd, panelX + panelWidth * 0.245f, panelY + panelHeight * 0.22f, panelWidth * 0.225f, panelHeight * 0.40f);
		guiUtils.SetItemOffsets(predationLeftDiagramBuck, panelX + panelWidth * 0.32f, panelY + panelHeight * 0.25f, panelWidth * 0.046f, buckStandTexture.height * ((panelWidth * 0.046f) / buckStandTexture.width));
		guiUtils.SetItemOffsets(predationLeftDiagramDoe, panelX + panelWidth * 0.29f, panelY + panelHeight * 0.4f, panelWidth * 0.03f, doeStandTexture.height * ((panelWidth * 0.03f) / doeStandTexture.width));
		guiUtils.SetItemOffsets(predationLeftDiagramFawn, panelX + panelWidth * 0.39f, panelY + panelHeight * 0.41f, panelWidth * 0.03f, fawnStandTexture.height * ((panelWidth * 0.03f) / fawnStandTexture.width));
		guiUtils.SetItemOffsets(predationLeftDiagramArrows, panelX + panelWidth * 0.115f, panelY + panelHeight * 0.04f, panelWidth * 0.33f, predationArrowsTexture.height * ((panelWidth * 0.33f) / predationArrowsTexture.width));

		// attack
		guiUtils.SetItemOffsets(predationLeftAttackBgnd, panelX + panelWidth * 0.21f, lowerY, panelWidth * 0.24f, panelHeight * 0.25f - gapSize*0.75f);
		guiUtils.SetItemOffsets(predationLeftAttackImage, panelX + panelWidth * 0.23f, panelY + panelHeight * 0.77f, panelWidth * 0.19f, pumaAttackTexture.height * ((panelWidth * 0.19f) / pumaAttackTexture.width));

		// RIGHT PANEL

		// Labels
		textureX = panelX + panelWidth * 0.34f;
		textureY = panelY + panelHeight * 0.11f;
		textureWidth = panelWidth * 0.1f;
		textureHeight = panelHeight * 0.1f;

		// Healthy labels
		guiUtils.SetItemOffsets(healthyLabelPanel, textGap + panelX + gapSize*0.645f + panelWidth*0.525f, textureY + panelHeight*-0.00075f, (panelWidth/2 + overlayRect.width*0.02f - gapSize*4.0f) * 0.275f - gapSize*0.5f, textureHeight*0.8f);
		guiUtils.SetTextOffsets(healthyLabelText, textGap + panelX + gapSize*0.75f + panelWidth*0.525f, textureY + panelHeight*-0.01f, (panelWidth/2 + overlayRect.width*0.02f - gapSize*4.0f) * 0.25f - gapSize*0.5f, textureHeight, (int)(overlayRect.width * 0.0225f));

		// Overpopulation label
		guiUtils.SetItemOffsets(unhealthyLabelPanel, textGap + panelX + gapSize*0.645f + panelWidth*0.5175f, textureY + panelHeight*0.795f, (panelWidth/2 + overlayRect.width*0.02f - gapSize*4.0f) * 0.3f - gapSize*0.5f, textureHeight*0.8f);
		guiUtils.SetTextOffsets(unhealthyLabelText, textGap + panelX + gapSize*0.75f + panelWidth*0.525f, textureY + panelHeight*0.785f, (panelWidth/2 + overlayRect.width*0.02f - gapSize*4.0f) * 0.25f - gapSize*0.5f, textureHeight, (int)(overlayRect.width * 0.0225f));

		// Image
		guiUtils.SetItemOffsets(pumasAndPredationImage, panelX + panelWidth*0.585f, textureY + panelHeight*-0.0075f, panelWidth*0.4f, panelWidth*0.4f);

	}

	
	void UpdatePredationItems()
	{
		predationPanel.SetActive((newLevelFlag == false && currentLevel != 6 && currentScreen == 1) ? true : false);
	}	

	
	void DrawPredationScreen(float panelOpacity) 
	{ 
		//////////////////
		//////////////////
		// OLD GUI CODE
		//////////////////
		//////////////////

		float panelX = overlayRect.x + overlayRect.width * 0.06f;
		float panelY = overlayRect.y + overlayRect.height * 0.205f;
		float panelWidth = overlayRect.width * 0.88f;
		float panelHeight = overlayRect.height * 0.6575f;
		float fontScale = 0.8f;

		GUIStyle style = new GUIStyle();
		style.alignment = TextAnchor.MiddleCenter;

		float yOffset = overlayRect.height * 0.022f;

		if (USE_NEW_GUI == false) {
			// two labels at top of panel	
			GUI.color = new Color(0f, 0f, 0f, 1f * panelOpacity);
			GUI.Box(new Rect(panelX - overlayRect.width * 0.02f, yOffset + overlayRect.y + overlayRect.height * 0.095f, overlayRect.width * 0.14f, overlayRect.height * 0.064f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * panelOpacity);
			style.fontSize = (int)(overlayRect.width * 0.021f);
			style.fontStyle = FontStyle.BoldAndItalic;
			style.normal.textColor = new Color(0.816f, 0.537f, 0.18f, 1f);
			GUI.Button(new Rect(panelX - overlayRect.width * 0.02f, yOffset + overlayRect.y + overlayRect.height * 0.077f, overlayRect.width * 0.14f, overlayRect.height * 0.1f), "In Game", style);

			GUI.color = new Color(0f, 0f, 0f, 1f * panelOpacity);
			GUI.Box(new Rect(overlayRect.x + overlayRect.width - overlayRect.width * 0.2f, yOffset + overlayRect.y + overlayRect.height * 0.095f, overlayRect.width * 0.16f, overlayRect.height * 0.064f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * panelOpacity);
			style.fontSize = (int)(overlayRect.width * 0.021f);
			style.fontStyle = FontStyle.BoldAndItalic;
			style.normal.textColor = new Color(0.816f, 0.537f, 0.18f, 1f);
			GUI.Button(new Rect(overlayRect.x + overlayRect.width - overlayRect.width * 0.2f, yOffset + overlayRect.y + overlayRect.height * 0.077f, overlayRect.width * 0.16f, overlayRect.height * 0.1f), "Real World", style);
		}

		// background rectangles
		GUI.color = new Color(1f, 1f, 1f, 1f * panelOpacity);
		float gapSize = overlayRect.width * 0.02f;
		
		//left side
		if (USE_NEW_GUI == false) {
			GUI.color = new Color(0f, 0f, 0f, 1f * panelOpacity);
			GUI.Box(new Rect(panelX - overlayRect.width * 0.02f, panelY - overlayRect.height * 0.025f, panelWidth/2 + overlayRect.width * 0.02f - gapSize/2, panelHeight + overlayRect.height * 0.05f), "");
			GUI.color = new Color(0f, 0f, 0f, 0.5f * panelOpacity);
			GUI.Box(new Rect(panelX, panelY, panelWidth/2 - overlayRect.width * 0.02f - gapSize/2, panelHeight), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * panelOpacity);
		}

		float upperY = panelY  + panelHeight * 0.13f;
		float middleY = panelY  + panelHeight * 0.44f;
		float lowerY = panelY  + panelHeight * 0.75f;			
		if (USE_NEW_GUI == false) {
			GUI.Box(new Rect(panelX + gapSize*0.75f, upperY, panelWidth * 0.145f, panelHeight * 0.25f - gapSize*0.75f), "");
			GUI.Box(new Rect(panelX + gapSize*0.75f, middleY, panelWidth * 0.145f, panelHeight * 0.25f - gapSize*0.75f), "");
			GUI.Box(new Rect(panelX + gapSize*0.75f, lowerY, panelWidth * 0.145f, panelHeight * 0.25f - gapSize*0.75f), "");
			GUI.Box(new Rect(panelX + panelWidth * 0.21f, lowerY, panelWidth * 0.24f, panelHeight * 0.25f - gapSize*0.75f), "");
			GUI.Box(new Rect(panelX + panelWidth * 0.21f, lowerY, panelWidth * 0.24f, panelHeight * 0.25f - gapSize*0.75f), "");
		}

		// right side
		if (USE_NEW_GUI == false) {
			GUI.color = new Color(0f, 0f, 0f, 1f * panelOpacity);
			GUI.Box(new Rect(panelX + panelWidth/2 + gapSize/2, panelY - overlayRect.height * 0.025f, panelWidth/2 + overlayRect.width * 0.02f - gapSize/2, panelHeight + overlayRect.height * 0.05f), "");
			GUI.color = new Color(0f, 0f, 0f, 0.5f * panelOpacity);
			GUI.Box(new Rect(panelX + panelWidth/2 + overlayRect.width * 0.02f + gapSize/2, panelY, panelWidth/2 - overlayRect.width * 0.02f - gapSize/2, panelHeight), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * panelOpacity);
		}
		
		
		
		// HEADER TEXT 
		
		float textureX;
		float textureY;
		float textureHeight;
		float textureWidth;

		string headerTextL = "Catching Deer";
		string headerTextR = "Pumas and Prey";
		
		yOffset = overlayRect.height * 0.115f;
		float colorScale = 0.74f;
		
		if (USE_NEW_GUI == false) {
			GUI.color = new Color(1f, 1f, 1f, 1f * panelOpacity);
			style.fontSize = (int)(overlayRect.width * 0.022f);
			style.fontStyle = FontStyle.Normal;
			style.normal.textColor = new Color(0.99f * colorScale, 0.88f * colorScale, 0.66f * colorScale, 1f);
			GUI.Button(new Rect(panelX, yOffset + overlayRect.y + overlayRect.height * 0.077f, panelWidth/2 - overlayRect.width * 0.02f - gapSize/2, overlayRect.height * 0.1f), headerTextL, style);

			GUI.color = new Color(1f, 1f, 1f, 1f * panelOpacity);
			style.fontSize = (int)(overlayRect.width * 0.022f);
			style.fontStyle = FontStyle.Normal;
			style.normal.textColor = new Color(0.99f * colorScale, 0.88f * colorScale, 0.66f * colorScale, 1f);
			GUI.Button(new Rect(panelX + panelWidth/2 + overlayRect.width * 0.02f + gapSize/2, yOffset + overlayRect.y + overlayRect.height * 0.077f, panelWidth/2 - overlayRect.width * 0.02f - gapSize/2, overlayRect.height * 0.1f), headerTextR, style);
		}

		float topRowOffsetY = panelHeight * -0.01f;
		
		

		// labels
		
		GUI.color = new Color(1f, 1f, 1f, panelOpacity);
		float textGap = panelWidth * 0.012f;

		colorScale = 0.74f;
		style.normal.textColor = new Color(0.99f * colorScale, 0.88f * colorScale, 0.66f * colorScale, 1f);
		style.fontSize = (int)(overlayRect.width * 0.014f);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.MiddleLeft;

		if (USE_NEW_GUI == false) {
			style.fontSize = (int)(overlayRect.width * 0.021f);
			style.fontStyle = FontStyle.BoldAndItalic;
			style.normal.textColor = new Color(0.816f * 1.1f, 0.537f * 1.1f, 0.18f * 1.1f, 1f);
			GUI.Button(new Rect(textGap + panelX + gapSize*0.75f, upperY + panelHeight * 0.0f, panelWidth * 0.3f, panelHeight * 0.08f), "Sneak", style);	
			GUI.Button(new Rect(textGap + panelX + gapSize*0.75f, middleY + panelHeight * 0.0f, panelWidth * 0.3f, panelHeight * 0.08f), "Attack", style);
			GUI.Button(new Rect(textGap + panelX + gapSize*0.75f, lowerY + panelHeight * 0.0f, panelWidth * 0.3f, panelHeight * 0.08f), "Jump", style);

			// explanatory text
			
			float textOffsetY = panelHeight * 0.035f;
			style.fontSize = (int)(overlayRect.width * 0.016f);
			style.fontStyle = FontStyle.BoldAndItalic;
			style.normal.textColor = new Color(0.88f, 0.64f, 0f, 0.9f);
			GUI.Button(new Rect(textGap + panelX + gapSize*0.75f, upperY + panelHeight * 0.045f, panelWidth * 0.3f, panelHeight * 0.08f), "around herd...", style);
			GUI.Button(new Rect(textGap + panelX + gapSize*0.75f, middleY + panelHeight * 0.045f, panelWidth * 0.3f, panelHeight * 0.08f), "from behind...", style);
			GUI.Button(new Rect(textGap + panelX + gapSize*0.75f, lowerY + panelHeight * 0.045f, panelWidth * 0.3f, panelHeight * 0.08f), "to take prey!", style);
			style.fontSize = (int)(overlayRect.width * 0.012f);
			style.fontStyle = FontStyle.Bold;
			colorScale = 0.65f;
			style.normal.textColor = new Color(0.99f * colorScale, 0.88f * colorScale, 0.66f * colorScale, 1f);
			GUI.Button(new Rect(textGap + panelX + gapSize*0.75f, upperY + panelHeight * 0.09f, panelWidth * 0.3f, panelHeight * 0.1f), "Hold LEFT BUTTON", style);
			GUI.Button(new Rect(textGap + panelX + gapSize*0.75f, upperY + panelHeight * 0.125f, panelWidth * 0.3f, panelHeight * 0.1f), "for diagonal walk", style);
			GUI.Button(new Rect(textGap + panelX + gapSize*0.75f, middleY + panelHeight * 0.09f, panelWidth * 0.3f, panelHeight * 0.1f), "Go straight in", style);
			GUI.Button(new Rect(textGap + panelX + gapSize*0.75f, middleY + panelHeight * 0.125f, panelWidth * 0.3f, panelHeight * 0.1f), "toward the rear", style);
			GUI.Button(new Rect(textGap + panelX + gapSize*0.75f, lowerY + panelHeight * 0.09f, panelWidth * 0.3f, panelHeight * 0.1f), "Tap LEFT BUTTON", style);
			GUI.Button(new Rect(textGap + panelX + gapSize*0.75f, lowerY + panelHeight * 0.125f, panelWidth * 0.3f, panelHeight * 0.1f), "to take down prey", style);
		}	
		
		
		// BLACK ARROWS

		GUI.color = new Color(0.5f, 0.5f, 0.5f, 1f * panelOpacity);
		textureX = panelX + panelWidth * 0.160f;
		textureHeight = panelHeight * 0.06f;
		textureWidth = panelWidth * 0.05f;
		if (USE_NEW_GUI == false) {
			GUI.DrawTexture(new Rect(textureX, upperY + panelHeight * 0.135f, textureWidth, textureHeight), blackArrowShortTexture);
			GUI.DrawTexture(new Rect(textureX, middleY + panelHeight * 0.135f, textureWidth, textureHeight), blackArrowShortTexture);
			GUI.DrawTexture(new Rect(textureX, lowerY + panelHeight * 0.135f, textureWidth, textureHeight), blackArrowShortTexture);
		}

		// PREDATION CIRCLES and DEER

		GUI.color = new Color(1f, 1f, 1f, 0.75f * panelOpacity);

		if (USE_NEW_GUI == false) {
			textureX = panelX + panelWidth * 0.245f;
			textureY = panelY + panelHeight * 0.22f;
			textureHeight = panelHeight * 0.40f;
			textureWidth = panelWidth * 0.225f;
			GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), predationCirclesTexture);
			
			textureX = panelX + panelWidth * 0.115f;
			textureY = panelY + panelHeight * 0.04f;
			textureWidth = panelWidth * 0.33f;
			textureHeight = predationArrowsTexture.height * (textureWidth / predationArrowsTexture.width);
			GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), predationArrowsTexture);
			
			textureX = panelX + panelWidth * 0.32f;
			textureY = panelY + panelHeight * 0.25f;
			textureWidth = panelWidth * 0.046f;
			textureHeight = buckStandTexture.height * (textureWidth / buckStandTexture.width);
			GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), buckStandTexture);

			textureX = panelX + panelWidth * 0.29f;
			textureY = panelY + panelHeight * 0.4f;
			textureWidth = panelWidth * 0.03f;
			textureHeight = doeStandTexture.height * (textureWidth / doeStandTexture.width);
			GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), doeStandTexture);


			textureX = panelX + panelWidth * 0.39f;
			textureY = panelY + panelHeight * 0.41f;
			textureWidth = panelWidth * 0.03f;
			textureHeight = fawnStandTexture.height * (textureWidth / fawnStandTexture.width);
			GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), fawnStandTexture);

			// PUMA ATTACKS DEER
			textureX = panelX + panelWidth * 0.23f;
			textureY = panelY + panelHeight * 0.77f;
			textureWidth = panelWidth * 0.19f;
			textureHeight = pumaAttackTexture.height * (textureWidth / pumaAttackTexture.width);
			GUI.color = new Color(1f, 1f, 1f, 0.7f * panelOpacity);
			GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), pumaAttackTexture);
			GUI.color = new Color(1f, 1f, 1f, 0.75f * panelOpacity);
		}
	}	



	//======================================================================
	//======================================================================
	//======================================================================
	//								ECOLOGY
	//======================================================================
	//======================================================================
	//======================================================================
	
	GameObject ecologyPanel;
	GameObject ecologyHeaderTextL;
	GameObject ecologyHeaderTextR;

	// LEFT PANEL
	
	// upper diagram
	GameObject ecologyLeftUpperDiagramBgnd;
	GameObject ecologyLeftUpperDiagramText1;
	GameObject ecologyLeftUpperDiagramText2;
	GameObject ecologyLeftUpperDiagramPuma;
	GameObject ecologyLeftUpperDiagramDeer;
	GameObject ecologyLeftUpperDiagramArrows;

	// lower diagram
	GameObject ecologyLeftLowerDiagramBgnd;
	GameObject ecologyLeftLowerDiagramText1;
	GameObject ecologyLeftLowerDiagramText2;
	GameObject ecologyLeftLowerDiagramPuma;
	GameObject ecologyLeftLowerDiagramDeer;
	GameObject ecologyLeftLowerDiagramArrows;

	// upper math section
	GameObject ecologyLeftUpperMathLeftBgnd;
	GameObject ecologyLeftUpperMathLeftChart;
	GameObject ecologyLeftUpperMathLeftText1;
	GameObject ecologyLeftUpperMathLeftText2;
	
	GameObject ecologyLeftUpperMathRightBgnd;
	GameObject ecologyLeftUpperMathRightChart;
	GameObject ecologyLeftUpperMathRightText1;
	GameObject ecologyLeftUpperMathRightText2;
	
	GameObject ecologyLeftUpperMathArrow;
	GameObject ecologyLeftUpperMathArrowText1;
	GameObject ecologyLeftUpperMathArrowText2;
	
	// lower math section
	GameObject ecologyLeftLowerMathLeftBgnd;
	GameObject ecologyLeftLowerMathLeftChart;
	GameObject ecologyLeftLowerMathLeftText1;
	GameObject ecologyLeftLowerMathLeftText2;
	
	GameObject ecologyLeftLowerMathRightBgnd;
	GameObject ecologyLeftLowerMathRightChart;
	GameObject ecologyLeftLowerMathRightText1;
	GameObject ecologyLeftLowerMathRightText2;
	
	GameObject ecologyLeftLowerMathArrow;
	GameObject ecologyLeftLowerMathArrowText1;
	GameObject ecologyLeftLowerMathArrowText2;

	// RIGHT PANEL

	// Panel
	GameObject ecologicalRolePanel;

	// Labels
	GameObject apexpredatorLabel;

	GameObject overpopulationLabel1;
	GameObject overpopulationLabel2;
	GameObject overpopulationLabel3;

	GameObject balancedsystemLabel1;
	GameObject balancedsystemLabel2;

	// Images
	GameObject ecologyImage;
	
	
	void CreateEcologyItems()
	{
		// invisible panel to hold items
		ecologyPanel = (GameObject)Instantiate(uiSubPanel);
		ecologyPanel.GetComponent<RectTransform>().SetParent(infoPanelMainPanel.GetComponent<RectTransform>(), false);

		// header text
		ecologyHeaderTextL = 			guiUtils.CreateText(ecologyPanel, "Population Health", new Color(0.99f * 0.82f, 0.88f * 0.82f, 0.66f * 0.82f, 1f), FontStyle.Normal);
		ecologyHeaderTextR = 			guiUtils.CreateText(ecologyPanel, "Ecological Role", new Color(0.99f * 0.82f, 0.88f * 0.82f, 0.66f * 0.82f, 1f), FontStyle.Normal);

		// LEFT PANEL
		
		// upper diagram
		ecologyLeftUpperDiagramBgnd = 		guiUtils.CreatePanel(ecologyPanel, new Color(0f, 0f, 0f, 0.4f));
		ecologyLeftUpperDiagramText1 = 		guiUtils.CreateText(ecologyPanel, "Long Chases", new Color(0.816f * 1.1f, 0.537f * 1.1f, 0.18f * 1.1f, 1f), FontStyle.BoldAndItalic, TextAnchor.MiddleLeft);
		ecologyLeftUpperDiagramText2 = 		guiUtils.CreateText(ecologyPanel, "bad for health", new Color(0.88f, 0.64f, 0f, 0.9f), FontStyle.BoldAndItalic, TextAnchor.MiddleLeft);
		ecologyLeftUpperDiagramPuma = 		guiUtils.CreateImage(ecologyPanel, pumaJumpTexture, new Color(1f, 1f, 1f, 1f));
		ecologyLeftUpperDiagramDeer = 		guiUtils.CreateImage(ecologyPanel, pumaPreyTexture, new Color(1f, 1f, 1f, 1f));
		ecologyLeftUpperDiagramArrows = 	guiUtils.CreateImage(ecologyPanel, huntLongTexture, new Color(1f, 1f, 1f, 1f));

		// lower diagram
		ecologyLeftLowerDiagramBgnd = 		guiUtils.CreatePanel(ecologyPanel, new Color(0f, 0f, 0f, 0.4f));
		ecologyLeftLowerDiagramText1 = 		guiUtils.CreateText(ecologyPanel, "Short Chases", new Color(0.816f * 1.1f, 0.537f * 1.1f, 0.18f * 1.1f, 1f), FontStyle.BoldAndItalic, TextAnchor.MiddleLeft);
		ecologyLeftLowerDiagramText2 = 		guiUtils.CreateText(ecologyPanel, "good for health +++", new Color(0.88f, 0.64f, 0f, 0.9f), FontStyle.BoldAndItalic, TextAnchor.MiddleLeft);
		ecologyLeftLowerDiagramPuma = 		guiUtils.CreateImage(ecologyPanel, pumaJumpTexture, new Color(1f, 1f, 1f, 1f));
		ecologyLeftLowerDiagramDeer = 		guiUtils.CreateImage(ecologyPanel, pumaPreyTexture, new Color(1f, 1f, 1f, 1f));
		ecologyLeftLowerDiagramArrows = 	guiUtils.CreateImage(ecologyPanel, huntShortTexture, new Color(1f, 1f, 1f, 1f));

		// upper math section
		ecologyLeftUpperMathLeftBgnd = 		guiUtils.CreatePanel(ecologyPanel, new Color(0f, 0f, 0f, 0.4f));
		ecologyLeftUpperMathLeftChart = 	guiUtils.CreateRect(ecologyPanel, new Color(0.3f, 0.06f, 0.05f, 0.8f));
		ecologyLeftUpperMathLeftText1 = 	guiUtils.CreateText(ecologyPanel, "Energy", new Color(0.99f * 0.65f, 0.88f * 0.65f, 0.66f * 0.65f, 1f), FontStyle.Bold);
		ecologyLeftUpperMathLeftText2 = 	guiUtils.CreateText(ecologyPanel, "Spent", new Color(0.99f * 0.65f, 0.88f * 0.65f, 0.66f * 0.65f, 1f), FontStyle.Bold);
		
		ecologyLeftUpperMathRightBgnd = 	guiUtils.CreatePanel(ecologyPanel, new Color(0f, 0f, 0f, 0.4f));
		ecologyLeftUpperMathRightChart = 	guiUtils.CreateRect(ecologyPanel, new Color(0.75f, 0.5f, 0f, 0.4f));
		ecologyLeftUpperMathRightText1 = 	guiUtils.CreateText(ecologyPanel, "Nutrition", new Color(0.99f * 0.65f, 0.88f * 0.65f, 0.66f * 0.65f, 1f), FontStyle.Bold);
		ecologyLeftUpperMathRightText2 = 	guiUtils.CreateText(ecologyPanel, "Taken", new Color(0.99f * 0.65f, 0.88f * 0.65f, 0.66f * 0.65f, 1f), FontStyle.Bold);
		
		ecologyLeftUpperMathArrow = 		guiUtils.CreateImage(ecologyPanel, infoArrowDownTexture, new Color(1f, 1f, 1f, 1f));
		ecologyLeftUpperMathArrowText1 = 	guiUtils.CreateText(ecologyPanel, "Health", new Color(0.88f, 0, 0, 0.85f), FontStyle.BoldAndItalic);
		ecologyLeftUpperMathArrowText2 = 	guiUtils.CreateText(ecologyPanel, "Loss", new Color(0.88f, 0, 0, 0.85f), FontStyle.BoldAndItalic);
		
		// lower math section
		ecologyLeftLowerMathLeftBgnd = 		guiUtils.CreatePanel(ecologyPanel, new Color(0f, 0f, 0f, 0.4f));
		ecologyLeftLowerMathLeftChart = 	guiUtils.CreateRect(ecologyPanel, new Color(0.06f, 0.3f, 0.05f, 0.9f));
		ecologyLeftLowerMathLeftText1 = 	guiUtils.CreateText(ecologyPanel, "Energy", new Color(0.99f * 0.65f, 0.88f * 0.65f, 0.66f * 0.65f, 1f), FontStyle.Bold);
		ecologyLeftLowerMathLeftText2 = 	guiUtils.CreateText(ecologyPanel, "Spent", new Color(0.99f * 0.65f, 0.88f * 0.65f, 0.66f * 0.65f, 1f), FontStyle.Bold);
		
		ecologyLeftLowerMathRightBgnd = 	guiUtils.CreatePanel(ecologyPanel, new Color(0f, 0f, 0f, 0.4f));
		ecologyLeftLowerMathRightChart = 	guiUtils.CreateRect(ecologyPanel, new Color(0.75f, 0.5f, 0f, 0.4f));
		ecologyLeftLowerMathRightText1 = 	guiUtils.CreateText(ecologyPanel, "Nutrition", new Color(0.99f * 0.65f, 0.88f * 0.65f, 0.66f * 0.65f, 1f), FontStyle.Bold);
		ecologyLeftLowerMathRightText2 = 	guiUtils.CreateText(ecologyPanel, "Taken", new Color(0.99f * 0.65f, 0.88f * 0.65f, 0.66f * 0.65f, 1f), FontStyle.Bold);
		
		ecologyLeftLowerMathArrow = 		guiUtils.CreateImage(ecologyPanel, infoArrowUpTexture, new Color(1f, 1f, 1f, 1f));
		ecologyLeftLowerMathArrowText1 = 	guiUtils.CreateText(ecologyPanel, "Health", new Color(0f, 0.74f, 0, 0.80f), FontStyle.BoldAndItalic);
		ecologyLeftLowerMathArrowText2 = 	guiUtils.CreateText(ecologyPanel, "Gain", new Color(0f, 0.74f, 0, 0.80f), FontStyle.BoldAndItalic);

		// RIGHT PANEL

		// Panels
		ecologicalRolePanel = 				guiUtils.CreatePanel(ecologyPanel, new Color(0f, 0f, 0f, 0.4f));

		// Labels
		apexpredatorLabel =					guiUtils.CreateText(ecologyPanel, "Apex Predator", new Color(0.816f * 1.1f, 0.537f * 1.1f, 0.18f * 1.1f, 1f), FontStyle.BoldAndItalic);

		overpopulationLabel1 = 				guiUtils.CreateText(ecologyPanel, "Overpopulation", new Color(0.88f, 0, 0, 0.85f), FontStyle.BoldAndItalic);
		overpopulationLabel2 = 				guiUtils.CreateText(ecologyPanel, "affects", new Color(0.88f, 0, 0, 0.85f), FontStyle.BoldAndItalic);
		overpopulationLabel3 = 				guiUtils.CreateText(ecologyPanel, "Ecosystem", new Color(0.88f, 0, 0, 0.85f), FontStyle.BoldAndItalic);

		balancedsystemLabel1 = 				guiUtils.CreateText(ecologyPanel, "Balanced", new Color(0f, 0.74f, 0, 0.80f), FontStyle.BoldAndItalic);
		balancedsystemLabel2 = 				guiUtils.CreateText(ecologyPanel, "Ecosystem", new Color(0f, 0.74f, 0, 0.80f), FontStyle.BoldAndItalic);

		ecologyImage = 						guiUtils.CreateImage(ecologyPanel, ecologyTexture, new Color(1f, 1f, 1f, 1f));
	}

	
	void PositionEcologyItems()
	{
		float panelX = overlayRect.x + overlayRect.width * 0.06f;
		float panelY = overlayRect.y + overlayRect.height * 0.205f;
		float panelWidth = overlayRect.width * 0.88f;
		float panelHeight = overlayRect.height * 0.6575f;
		float yOffset = overlayRect.height * 0.115f;
		float gapSize = overlayRect.width * 0.02f;
		float textGap = panelWidth * 0.012f;
		float huntPanelX = panelX + gapSize*0.75f;
		float huntPanelY1 = panelY  + panelHeight * 0.13f;
		float huntPanelY2 = panelY  + panelHeight * 0.58f;
		float huntPanelWidth = panelWidth * 0.45f - gapSize*0.75f;
		float huntPanelHeight = panelHeight * 0.42f - gapSize*0.75f;
		float mathBoxX1 = huntPanelX + huntPanelWidth * 0.65f;
		float mathBoxX2 = huntPanelX + huntPanelWidth * 0.825f;
		float mathBoxY = huntPanelY1 + huntPanelHeight * 0.05f;
		float mathBoxWidth = huntPanelWidth * 0.15f;
		float mathBoxHeight = huntPanelHeight * 0.9f;
		float textOffsetY = panelHeight * 0.02f;
		float textureX;
		float textureY;
		float textureHeight;
		float textureWidth;

		// header text
		guiUtils.SetTextOffsets(ecologyHeaderTextL, panelX, yOffset + overlayRect.y + overlayRect.height * 0.077f, panelWidth/2 - overlayRect.width * 0.02f - gapSize/2, overlayRect.height * 0.1f, (int)(overlayRect.width * 0.022f));
		guiUtils.SetTextOffsets(ecologyHeaderTextR, panelX + panelWidth/2 + overlayRect.width * 0.02f + gapSize/2, yOffset + overlayRect.y + overlayRect.height * 0.077f, panelWidth/2 - overlayRect.width * 0.02f - gapSize/2, overlayRect.height * 0.1f, (int)(overlayRect.width * 0.022f));

		// LEFT PANEL
		
		// upper diagram
		textureX = panelX + panelWidth * 0.02f;
		textureY = panelY + panelHeight * 0.110f;
		textureWidth = panelWidth * 0.1f;
		textureHeight = panelHeight * 0.1f;
		guiUtils.SetItemOffsets(ecologyLeftUpperDiagramBgnd, huntPanelX, huntPanelY1, huntPanelWidth, huntPanelHeight);
		guiUtils.SetTextOffsets(ecologyLeftUpperDiagramText1, huntPanelX + textGap, huntPanelY1 + huntPanelHeight * 0.0f, huntPanelWidth - textGap*2, huntPanelHeight * 0.2f, (int)(overlayRect.width * 0.019f));
		textureY = panelY + panelHeight * 0.17f + textOffsetY;
		guiUtils.SetTextOffsets(ecologyLeftUpperDiagramText2, huntPanelX + textGap, huntPanelY1 + huntPanelHeight * 0.12f, huntPanelWidth - textGap*2, huntPanelHeight * 0.2f, (int)(overlayRect.width * 0.015f));
		textureWidth = huntPanelWidth * 0.13f;
		textureHeight = pumaJumpTexture.height * (textureWidth / pumaJumpTexture.width);
		guiUtils.SetItemOffsets(ecologyLeftUpperDiagramPuma, huntPanelX + huntPanelWidth * 0.04f, huntPanelY1 + huntPanelHeight * 0.35f, textureWidth, textureHeight);
		textureWidth = huntPanelWidth * 0.1f;
		textureHeight = pumaPreyTexture.height * (textureWidth / pumaPreyTexture.width);
		guiUtils.SetItemOffsets(ecologyLeftUpperDiagramDeer, huntPanelX + huntPanelWidth * 0.52f, huntPanelY1 + huntPanelHeight * 0.65f, textureWidth, textureHeight);
		textureHeight = huntPanelHeight;
		textureWidth = huntLongTexture.width * (textureHeight / huntLongTexture.height);
		guiUtils.SetItemOffsets(ecologyLeftUpperDiagramArrows, huntPanelX + huntPanelWidth * 0.02f, huntPanelY1, textureWidth, textureHeight);

		// lower diagram
		textureY = panelY + panelHeight * 0.510f;
		guiUtils.SetItemOffsets(ecologyLeftLowerDiagramBgnd, huntPanelX, huntPanelY2, huntPanelWidth, huntPanelHeight);
		guiUtils.SetTextOffsets(ecologyLeftLowerDiagramText1, huntPanelX + textGap, huntPanelY2 + huntPanelHeight * 0.0f, huntPanelWidth - textGap*2, huntPanelHeight * 0.2f, (int)(overlayRect.width * 0.019f));
		textureY = panelY + panelHeight * 0.52f + textOffsetY;
		guiUtils.SetTextOffsets(ecologyLeftLowerDiagramText2, huntPanelX + textGap, huntPanelY2 + huntPanelHeight * 0.12f, huntPanelWidth - textGap*2, huntPanelHeight * 0.2f, (int)(overlayRect.width * 0.015f));
		textureWidth = huntPanelWidth * 0.13f;
		textureHeight = pumaJumpTexture.height * (textureWidth / pumaJumpTexture.width);
		guiUtils.SetItemOffsets(ecologyLeftLowerDiagramPuma, huntPanelX + huntPanelWidth * 0.06f, huntPanelY2 + huntPanelHeight * 0.40f, textureWidth, textureHeight);
		textureWidth = huntPanelWidth * 0.1f;
		textureHeight = pumaPreyTexture.height * (textureWidth / pumaPreyTexture.width);
		guiUtils.SetItemOffsets(ecologyLeftLowerDiagramDeer, huntPanelX + huntPanelWidth * 0.39f, huntPanelY2 + huntPanelHeight * 0.62f, textureWidth, textureHeight);
		textureHeight = huntPanelHeight;
		textureWidth = huntShortTexture.width * (textureHeight / huntShortTexture.height);
		guiUtils.SetItemOffsets(ecologyLeftLowerDiagramArrows, huntPanelX + huntPanelWidth * 0.02f + huntPanelWidth*0.008f, huntPanelY2, textureWidth, textureHeight);

		// upper math section
		guiUtils.SetItemOffsets(ecologyLeftUpperMathLeftBgnd, mathBoxX1, mathBoxY, mathBoxWidth, mathBoxHeight);
		guiUtils.SetItemOffsets(ecologyLeftUpperMathLeftChart, mathBoxX1 + mathBoxWidth * 0.25f, mathBoxY + mathBoxHeight * 0.05f, mathBoxWidth * 0.5f, mathBoxHeight * 0.70f);
		guiUtils.SetTextOffsets(ecologyLeftUpperMathLeftText1, mathBoxX1, mathBoxY + mathBoxHeight * 0.72f, mathBoxWidth, mathBoxHeight * 0.2f, (int)(overlayRect.width * 0.012f));
		guiUtils.SetTextOffsets(ecologyLeftUpperMathLeftText2, mathBoxX1, mathBoxY + mathBoxHeight * 0.82f, mathBoxWidth, mathBoxHeight * 0.2f, (int)(overlayRect.width * 0.012f));
		
		guiUtils.SetItemOffsets(ecologyLeftUpperMathRightBgnd, mathBoxX2, mathBoxY + mathBoxHeight*0.3f, mathBoxWidth, mathBoxHeight - mathBoxHeight*0.3f);
		guiUtils.SetItemOffsets(ecologyLeftUpperMathRightChart, mathBoxX2 + mathBoxWidth * 0.25f, mathBoxY + mathBoxHeight * 0.35f, mathBoxWidth * 0.5f, mathBoxHeight * 0.4f);
		guiUtils.SetTextOffsets(ecologyLeftUpperMathRightText1, mathBoxX2, mathBoxY + mathBoxHeight * 0.72f, mathBoxWidth, mathBoxHeight * 0.2f, (int)(overlayRect.width * 0.012f));
		guiUtils.SetTextOffsets(ecologyLeftUpperMathRightText2, mathBoxX2, mathBoxY + mathBoxHeight * 0.82f, mathBoxWidth, mathBoxHeight * 0.2f, (int)(overlayRect.width * 0.012f));
		
		textureWidth = mathBoxWidth * 0.2f;
		textureHeight = infoArrowDownTexture.height * (textureWidth / infoArrowDownTexture.width);
		guiUtils.SetItemOffsets(ecologyLeftUpperMathArrow, mathBoxX2 - mathBoxWidth * 0.12f, mathBoxY + mathBoxHeight * 0.02f, textureWidth, textureHeight);
		guiUtils.SetTextOffsets(ecologyLeftUpperMathArrowText1, mathBoxX2, mathBoxY + mathBoxHeight * 0.00f, mathBoxWidth, mathBoxHeight * 0.2f, (int)(overlayRect.width * 0.013f));
		guiUtils.SetTextOffsets(ecologyLeftUpperMathArrowText2, mathBoxX2, mathBoxY + mathBoxHeight * 0.10f, mathBoxWidth, mathBoxHeight * 0.2f, (int)(overlayRect.width * 0.014f));
		
		// lower math section
		mathBoxY = huntPanelY2 + huntPanelHeight * 0.05f;

		guiUtils.SetItemOffsets(ecologyLeftLowerMathLeftBgnd, mathBoxX1, mathBoxY + mathBoxHeight*0.6f, mathBoxWidth, mathBoxHeight - mathBoxHeight*0.6f);
		guiUtils.SetItemOffsets(ecologyLeftLowerMathLeftChart, mathBoxX1 + mathBoxWidth * 0.25f, mathBoxY + mathBoxHeight * 0.65f, mathBoxWidth * 0.5f, mathBoxHeight * 0.10f);
		guiUtils.SetTextOffsets(ecologyLeftLowerMathLeftText1, mathBoxX1, mathBoxY + mathBoxHeight * 0.72f, mathBoxWidth, mathBoxHeight * 0.2f, (int)(overlayRect.width * 0.012f));
		guiUtils.SetTextOffsets(ecologyLeftLowerMathLeftText2, mathBoxX1, mathBoxY + mathBoxHeight * 0.82f, mathBoxWidth, mathBoxHeight * 0.2f, (int)(overlayRect.width * 0.012f));
		
		guiUtils.SetItemOffsets(ecologyLeftLowerMathRightBgnd, mathBoxX2, mathBoxY + mathBoxHeight*0.3f, mathBoxWidth, mathBoxHeight - mathBoxHeight*0.3f);
		guiUtils.SetItemOffsets(ecologyLeftLowerMathRightChart, mathBoxX2 + mathBoxWidth * 0.25f, mathBoxY + mathBoxHeight * 0.35f, mathBoxWidth * 0.5f, mathBoxHeight * 0.4f);
		guiUtils.SetTextOffsets(ecologyLeftLowerMathRightText1, mathBoxX2, mathBoxY + mathBoxHeight * 0.72f, mathBoxWidth, mathBoxHeight * 0.2f, (int)(overlayRect.width * 0.012f));
		guiUtils.SetTextOffsets(ecologyLeftLowerMathRightText2, mathBoxX2, mathBoxY + mathBoxHeight * 0.82f, mathBoxWidth, mathBoxHeight * 0.2f, (int)(overlayRect.width * 0.012f));
		
		textureWidth = mathBoxWidth * 0.2f;
		textureHeight = infoArrowUpTexture.height * (textureWidth / infoArrowUpTexture.width);
		guiUtils.SetItemOffsets(ecologyLeftLowerMathArrow, mathBoxX2 - mathBoxWidth * 0.27f, mathBoxY + mathBoxHeight * 0.315f, textureWidth, textureHeight);
		guiUtils.SetTextOffsets(ecologyLeftLowerMathArrowText1, mathBoxX1, mathBoxY + mathBoxHeight * 0.30f, mathBoxWidth, mathBoxHeight * 0.2f, (int)(overlayRect.width * 0.013f));
		guiUtils.SetTextOffsets(ecologyLeftLowerMathArrowText2, mathBoxX1, mathBoxY + mathBoxHeight * 0.405f, mathBoxWidth, mathBoxHeight * 0.2f, (int)(overlayRect.width * 0.014f));

		// RIGHT PANEL

		// Panel
		guiUtils.SetItemOffsets(ecologicalRolePanel, textGap + panelX + gapSize*0.75f + panelWidth*0.525f, textureY + panelHeight*-0.425f, panelWidth * 0.425f, panelHeight * 0.825f);

		// Labels
		textureX = panelX + panelWidth * 0.34f;
		textureY = panelY + panelHeight * 0.11f;
		textureWidth = panelWidth * 0.1f;
		textureHeight = panelHeight * 0.1f;

		// Apex Predator labels
		guiUtils.SetTextOffsets(apexpredatorLabel, textGap + panelX + gapSize*0.75f + panelWidth*0.69f, textureY + panelHeight*0.005f, (panelWidth/2 + overlayRect.width*0.02f - gapSize*4.0f) * 0.25f - gapSize*0.5f, textureHeight, (int)(overlayRect.width * 0.0175f));

		// Overpopulation label
		guiUtils.SetTextOffsets(overpopulationLabel1, textGap + panelX + gapSize*0.75f + panelWidth*0.56f, textureY + panelHeight*0.1f, (panelWidth/2 + overlayRect.width*0.02f - gapSize*4.0f) * 0.25f - gapSize*0.5f, textureHeight, (int)(overlayRect.width * 0.0125f));
		guiUtils.SetTextOffsets(overpopulationLabel2, textGap + panelX + gapSize*0.75f + panelWidth*0.56f, textureY + panelHeight*0.15f, (panelWidth/2 + overlayRect.width*0.02f - gapSize*4.0f) * 0.25f - gapSize*0.5f, textureHeight, (int)(overlayRect.width * 0.0125f));
		guiUtils.SetTextOffsets(overpopulationLabel3, textGap + panelX + gapSize*0.75f + panelWidth*0.56f, textureY + panelHeight*0.2f, (panelWidth/2 + overlayRect.width*0.02f - gapSize*4.0f) * 0.25f - gapSize*0.5f, textureHeight, (int)(overlayRect.width * 0.0125f));

		// Balanced Ecosystem Label
		guiUtils.SetTextOffsets(balancedsystemLabel1, textGap + panelX + gapSize*0.75f + panelWidth*0.835f, textureY + panelHeight*0.1f, (panelWidth/2 + overlayRect.width*0.02f - gapSize*4.0f) * 0.25f - gapSize*0.5f, textureHeight, (int)(overlayRect.width * 0.0125f));
		guiUtils.SetTextOffsets(balancedsystemLabel2, textGap + panelX + gapSize*0.75f + panelWidth*0.835f, textureY + panelHeight*0.15f, (panelWidth/2 + overlayRect.width*0.02f - gapSize*4.0f) * 0.25f - gapSize*0.5f, textureHeight, (int)(overlayRect.width * 0.0125f));


		// Image
		guiUtils.SetItemOffsets(ecologyImage, panelX + panelWidth*0.585f, textureY + panelHeight*0.0275f, panelWidth*0.35f, panelWidth*0.35f);
	}

	
	void UpdateEcologyItems()
	{
		ecologyPanel.SetActive((newLevelFlag == false && currentLevel != 6 && currentScreen == 2) ? true : false);
	}	

	
	void DrawEcologyScreen(float panelOpacity) 
	{ 
		//////////////////
		//////////////////
		// OLD GUI CODE
		//////////////////
		//////////////////

		float panelX = overlayRect.x + overlayRect.width * 0.06f;
		float panelY = overlayRect.y + overlayRect.height * 0.205f;
		float panelWidth = overlayRect.width * 0.88f;
		float panelHeight = overlayRect.height * 0.6575f;
		float fontScale = 0.8f;

		GUIStyle style = new GUIStyle();
		style.alignment = TextAnchor.MiddleCenter;

		float yOffset = overlayRect.height * 0.022f;

		if (USE_NEW_GUI == false) {
			// two labels at top of panel	
			GUI.color = new Color(0f, 0f, 0f, 1f * panelOpacity);
			GUI.Box(new Rect(panelX - overlayRect.width * 0.02f, yOffset + overlayRect.y + overlayRect.height * 0.095f, overlayRect.width * 0.14f, overlayRect.height * 0.064f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * panelOpacity);
			style.fontSize = (int)(overlayRect.width * 0.021f);
			style.fontStyle = FontStyle.BoldAndItalic;
			style.normal.textColor = new Color(0.816f, 0.537f, 0.18f, 1f);
			GUI.Button(new Rect(panelX - overlayRect.width * 0.02f, yOffset + overlayRect.y + overlayRect.height * 0.077f, overlayRect.width * 0.14f, overlayRect.height * 0.1f), "In Game", style);

			GUI.color = new Color(0f, 0f, 0f, 1f * panelOpacity);
			GUI.Box(new Rect(overlayRect.x + overlayRect.width - overlayRect.width * 0.2f, yOffset + overlayRect.y + overlayRect.height * 0.095f, overlayRect.width * 0.16f, overlayRect.height * 0.064f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * panelOpacity);
			style.fontSize = (int)(overlayRect.width * 0.021f);
			style.fontStyle = FontStyle.BoldAndItalic;
			style.normal.textColor = new Color(0.816f, 0.537f, 0.18f, 1f);
			GUI.Button(new Rect(overlayRect.x + overlayRect.width - overlayRect.width * 0.2f, yOffset + overlayRect.y + overlayRect.height * 0.077f, overlayRect.width * 0.16f, overlayRect.height * 0.1f), "Real World", style);
		}

		// background rectangles
		GUI.color = new Color(1f, 1f, 1f, 1f * panelOpacity);
		float gapSize = overlayRect.width * 0.02f;
		
		//left side
		if (USE_NEW_GUI == false) {
			GUI.color = new Color(0f, 0f, 0f, 1f * panelOpacity);
			GUI.Box(new Rect(panelX - overlayRect.width * 0.02f, panelY - overlayRect.height * 0.025f, panelWidth/2 + overlayRect.width * 0.02f - gapSize/2, panelHeight + overlayRect.height * 0.05f), "");
			GUI.color = new Color(0f, 0f, 0f, 0.5f * panelOpacity);
			GUI.Box(new Rect(panelX, panelY, panelWidth/2 - overlayRect.width * 0.02f - gapSize/2, panelHeight), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * panelOpacity);
		}

		float huntPanelX = panelX + gapSize*0.75f;
		float huntPanelY1 = panelY  + panelHeight * 0.13f;
		float huntPanelY2 = panelY  + panelHeight * 0.58f;
		float huntPanelWidth = panelWidth * 0.45f - gapSize*0.75f;
		float huntPanelHeight = panelHeight * 0.42f - gapSize*0.75f;
		if (USE_NEW_GUI == false) {
			GUI.Box(new Rect(huntPanelX, huntPanelY1, huntPanelWidth, huntPanelHeight), "");
			GUI.Box(new Rect(huntPanelX, huntPanelY2, huntPanelWidth, huntPanelHeight), "");
		}

		// right side
		if (USE_NEW_GUI == false) {
			GUI.color = new Color(0f, 0f, 0f, 1f * panelOpacity);
			GUI.Box(new Rect(panelX + panelWidth/2 + gapSize/2, panelY - overlayRect.height * 0.025f, panelWidth/2 + overlayRect.width * 0.02f - gapSize/2, panelHeight + overlayRect.height * 0.05f), "");
			GUI.color = new Color(0f, 0f, 0f, 0.5f * panelOpacity);
			GUI.Box(new Rect(panelX + panelWidth/2 + overlayRect.width * 0.02f + gapSize/2, panelY, panelWidth/2 - overlayRect.width * 0.02f - gapSize/2, panelHeight), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * panelOpacity);
		}
		
		
		
		// HEADER TEXT 
		
		float textureX;
		float textureY;
		float textureHeight;
		float textureWidth;

		string headerTextL = "Population Health";
		string headerTextR = "Ecological Role";
		
		yOffset = overlayRect.height * 0.115f;
		float colorScale = 0.74f;
		
		if (USE_NEW_GUI == false) {
			GUI.color = new Color(1f, 1f, 1f, 1f * panelOpacity);
			style.fontSize = (int)(overlayRect.width * 0.022f);
			style.fontStyle = FontStyle.Normal;
			style.normal.textColor = new Color(0.99f * colorScale, 0.88f * colorScale, 0.66f * colorScale, 1f);
			GUI.Button(new Rect(panelX, yOffset + overlayRect.y + overlayRect.height * 0.077f, panelWidth/2 - overlayRect.width * 0.02f - gapSize/2, overlayRect.height * 0.1f), headerTextL, style);

			GUI.color = new Color(1f, 1f, 1f, 1f * panelOpacity);
			style.fontSize = (int)(overlayRect.width * 0.022f);
			style.fontStyle = FontStyle.Normal;
			style.normal.textColor = new Color(0.99f * colorScale, 0.88f * colorScale, 0.66f * colorScale, 1f);
			GUI.Button(new Rect(panelX + panelWidth/2 + overlayRect.width * 0.02f + gapSize/2, yOffset + overlayRect.y + overlayRect.height * 0.077f, panelWidth/2 - overlayRect.width * 0.02f - gapSize/2, overlayRect.height * 0.1f), headerTextR, style);
		}

		float topRowOffsetY = panelHeight * -0.01f;
		
		

		// labels
		
		GUI.color = new Color(1f, 1f, 1f, panelOpacity);
		float textGap = panelWidth * 0.012f;

		colorScale = 0.74f;
		style.normal.textColor = new Color(0.99f * colorScale, 0.88f * colorScale, 0.66f * colorScale, 1f);
		style.fontSize = (int)(overlayRect.width * 0.014f);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.MiddleLeft;

		style.fontSize = (int)(overlayRect.width * 0.019f);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.normal.textColor = new Color(0.816f * 1.1f, 0.537f * 1.1f, 0.18f * 1.1f, 1f);
		textureX = panelX + panelWidth * 0.02f;
		textureY = panelY + panelHeight * 0.110f;
		textureWidth = panelWidth * 0.1f;
		textureHeight = panelHeight * 0.1f;
		if (USE_NEW_GUI == false) {
			GUI.Button(new Rect(huntPanelX + textGap, huntPanelY1 + huntPanelHeight * 0.0f, huntPanelWidth - textGap*2, huntPanelHeight * 0.2f), "Long Chases", style);	
			textureY = panelY + panelHeight * 0.510f;
			GUI.Button(new Rect(huntPanelX + textGap, huntPanelY2 + huntPanelHeight * 0.0f, huntPanelWidth - textGap*2, huntPanelHeight * 0.2f), "Short Chases", style);
		}

		// explanatory text
		
		float textOffsetY = panelHeight * 0.02f;
		style.fontSize = (int)(overlayRect.width * 0.015f);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.normal.textColor = new Color(0.88f, 0.64f, 0f, 0.9f);
		textureY = panelY + panelHeight * 0.17f + textOffsetY;
		if (USE_NEW_GUI == false) {
			GUI.Button(new Rect(huntPanelX + textGap, huntPanelY1 + huntPanelHeight * 0.12f, huntPanelWidth - textGap*2, huntPanelHeight * 0.2f), "bad for health", style);
			textureY = panelY + panelHeight * 0.52f + textOffsetY;
			GUI.Button(new Rect(huntPanelX + textGap, huntPanelY2 + huntPanelHeight * 0.12f, huntPanelWidth - textGap*2, huntPanelHeight * 0.2f), "good for health +++", style);
		}

		// HUNT REPRESENTATION
		if (USE_NEW_GUI == false) {
			textureWidth = huntPanelWidth * 0.13f;
			textureHeight = pumaJumpTexture.height * (textureWidth / pumaJumpTexture.width);
			GUI.DrawTexture(new Rect(huntPanelX + huntPanelWidth * 0.04f, huntPanelY1 + huntPanelHeight * 0.35f, textureWidth, textureHeight), pumaJumpTexture);
			GUI.DrawTexture(new Rect(huntPanelX + huntPanelWidth * 0.06f, huntPanelY2 + huntPanelHeight * 0.40f, textureWidth, textureHeight), pumaJumpTexture);

			textureWidth = huntPanelWidth * 0.1f;
			textureHeight = pumaPreyTexture.height * (textureWidth / pumaPreyTexture.width);
			GUI.DrawTexture(new Rect(huntPanelX + huntPanelWidth * 0.52f, huntPanelY1 + huntPanelHeight * 0.65f, textureWidth, textureHeight), pumaPreyTexture);
			GUI.DrawTexture(new Rect(huntPanelX + huntPanelWidth * 0.39f, huntPanelY2 + huntPanelHeight * 0.62f, textureWidth, textureHeight), pumaPreyTexture);

			textureHeight = huntPanelHeight;
			textureWidth = huntLongTexture.width * (textureHeight / huntLongTexture.height);
			GUI.DrawTexture(new Rect(huntPanelX + huntPanelWidth * 0.02f, huntPanelY1, textureWidth, textureHeight), huntLongTexture);
			textureWidth = huntShortTexture.width * (textureHeight / huntShortTexture.height);
			GUI.DrawTexture(new Rect(huntPanelX + huntPanelWidth * 0.02f + huntPanelWidth*0.008f, huntPanelY2, textureWidth, textureHeight), huntShortTexture);
		}
		
		if (USE_NEW_GUI == false) {
		
			// RIGHT SIDE MATH SECTION
			
			float mathBoxX1 = huntPanelX + huntPanelWidth * 0.65f;
			float mathBoxX2 = huntPanelX + huntPanelWidth * 0.825f;
			float mathBoxY = huntPanelY1 + huntPanelHeight * 0.05f;
			float mathBoxWidth = huntPanelWidth * 0.15f;
			float mathBoxHeight = huntPanelHeight * 0.9f;
			

			// UPPER

			GUI.color = new Color(0f, 0f, 0f, 1f * panelOpacity);
			GUI.Box(new Rect(mathBoxX1, mathBoxY, mathBoxWidth, mathBoxHeight), "");
			GUI.Box(new Rect(mathBoxX2, mathBoxY + mathBoxHeight*0.3f, mathBoxWidth, mathBoxHeight - mathBoxHeight*0.3f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * panelOpacity);

			style.fontSize = (int)(overlayRect.width * 0.012f);
			style.fontStyle = FontStyle.Bold;
			colorScale = 0.65f;
			style.alignment = TextAnchor.MiddleCenter;
			style.normal.textColor = new Color(0.99f * colorScale, 0.88f * colorScale, 0.66f * colorScale, 1f);
			GUI.Button(new Rect(mathBoxX1, mathBoxY + mathBoxHeight * 0.72f, mathBoxWidth, mathBoxHeight * 0.2f), "Energy", style);
			GUI.Button(new Rect(mathBoxX1, mathBoxY + mathBoxHeight * 0.82f, mathBoxWidth, mathBoxHeight * 0.2f), "Spent", style);
			GUI.Button(new Rect(mathBoxX2, mathBoxY + mathBoxHeight * 0.72f, mathBoxWidth, mathBoxHeight * 0.2f), "Nutrition", style);
			GUI.Button(new Rect(mathBoxX2, mathBoxY + mathBoxHeight * 0.82f, mathBoxWidth, mathBoxHeight * 0.2f), "Taken", style);
			guiUtils.DrawRect(new Rect(mathBoxX1 + mathBoxWidth * 0.25f, mathBoxY + mathBoxHeight * 0.05f, mathBoxWidth * 0.5f, mathBoxHeight * 0.70f), new Color(0.3f, 0.06f, 0.05f, 0.8f));	
			guiUtils.DrawRect(new Rect(mathBoxX2 + mathBoxWidth * 0.25f, mathBoxY + mathBoxHeight * 0.35f, mathBoxWidth * 0.5f, mathBoxHeight * 0.4f), new Color(0.75f, 0.5f, 0f, 0.4f));	
			textureWidth = mathBoxWidth * 0.2f;
			textureHeight = infoArrowDownTexture.height * (textureWidth / infoArrowDownTexture.width);
			GUI.DrawTexture(new Rect(mathBoxX2 - mathBoxWidth * 0.12f, mathBoxY + mathBoxHeight * 0.02f, textureWidth, textureHeight), infoArrowDownTexture);
			style.fontStyle = FontStyle.BoldAndItalic;
			style.fontSize = (int)(overlayRect.width * 0.013f);
			style.normal.textColor = new Color(0.88f, 0, 0, 0.85f);
			GUI.Button(new Rect(mathBoxX2, mathBoxY + mathBoxHeight * 0.00f, mathBoxWidth, mathBoxHeight * 0.2f), "Health", style);
			style.fontSize = (int)(overlayRect.width * 0.014f);
			GUI.Button(new Rect(mathBoxX2, mathBoxY + mathBoxHeight * 0.10f, mathBoxWidth, mathBoxHeight * 0.2f), "Loss", style);

			// LOWER


			mathBoxY = huntPanelY2 + huntPanelHeight * 0.05f;

			GUI.color = new Color(0f, 0f, 0f, 1f * panelOpacity);
			GUI.Box(new Rect(mathBoxX1, mathBoxY + mathBoxHeight*0.6f, mathBoxWidth, mathBoxHeight - mathBoxHeight*0.6f), "");
			GUI.Box(new Rect(mathBoxX2, mathBoxY + mathBoxHeight*0.3f, mathBoxWidth, mathBoxHeight - mathBoxHeight*0.3f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * panelOpacity);

			style.fontSize = (int)(overlayRect.width * 0.012f);
			style.fontStyle = FontStyle.Bold;
			colorScale = 0.65f;
			style.alignment = TextAnchor.MiddleCenter;
			style.normal.textColor = new Color(0.99f * colorScale, 0.88f * colorScale, 0.66f * colorScale, 1f);
			GUI.Button(new Rect(mathBoxX1, mathBoxY + mathBoxHeight * 0.72f, mathBoxWidth, mathBoxHeight * 0.2f), "Energy", style);
			GUI.Button(new Rect(mathBoxX1, mathBoxY + mathBoxHeight * 0.82f, mathBoxWidth, mathBoxHeight * 0.2f), "Spent", style);
			GUI.Button(new Rect(mathBoxX2, mathBoxY + mathBoxHeight * 0.72f, mathBoxWidth, mathBoxHeight * 0.2f), "Nutrition", style);
			GUI.Button(new Rect(mathBoxX2, mathBoxY + mathBoxHeight * 0.82f, mathBoxWidth, mathBoxHeight * 0.2f), "Taken", style);
			guiUtils.DrawRect(new Rect(mathBoxX1 + mathBoxWidth * 0.25f, mathBoxY + mathBoxHeight * 0.65f, mathBoxWidth * 0.5f, mathBoxHeight * 0.10f), new Color(0.06f, 0.3f, 0.05f, 0.9f));	
			guiUtils.DrawRect(new Rect(mathBoxX2 + mathBoxWidth * 0.25f, mathBoxY + mathBoxHeight * 0.35f, mathBoxWidth * 0.5f, mathBoxHeight * 0.4f), new Color(0.75f, 0.5f, 0f, 0.4f));	
			textureWidth = mathBoxWidth * 0.2f;
			textureHeight = infoArrowUpTexture.height * (textureWidth / infoArrowUpTexture.width);
			GUI.DrawTexture(new Rect(mathBoxX2 - mathBoxWidth * 0.27f, mathBoxY + mathBoxHeight * 0.315f, textureWidth, textureHeight), infoArrowUpTexture);
			style.fontStyle = FontStyle.BoldAndItalic;
			style.fontSize = (int)(overlayRect.width * 0.013f);
			style.normal.textColor = new Color(0f, 0.74f, 0, 0.80f);
			GUI.Button(new Rect(mathBoxX1, mathBoxY + mathBoxHeight * 0.30f, mathBoxWidth, mathBoxHeight * 0.2f), "Health", style);
			style.fontSize = (int)(overlayRect.width * 0.014f);
			GUI.Button(new Rect(mathBoxX1, mathBoxY + mathBoxHeight * 0.405f, mathBoxWidth, mathBoxHeight * 0.2f), "Gain", style);
		}
	}	


	//======================================================================
	//======================================================================
	//======================================================================
	//								SURVIVAL
	//======================================================================
	//======================================================================
	//======================================================================

	GameObject survivalPanel;
	GameObject survivalHeaderTextL;
	GameObject survivalHeaderTextR;

	// LEFT PANEL
	GameObject survivalLeftUpperBackground;
	GameObject survivalLeftLowerBackground;
	GameObject survivalLeftUpperImage;
	GameObject survivalLeftLowerImageL;
	GameObject survivalLeftLowerImageR;
	GameObject survivalLeftUpperTextCenter;
	GameObject survivalLeftLowerTextCenter;
	GameObject survivalLeftUpperTextLeftAbove;
	GameObject survivalLeftUpperTextLeftBelow;
	GameObject survivalLeftUpperTextRightAbove;
	GameObject survivalLeftUpperTextRightBelow;
	GameObject survivalLeftLowerTextLeftAbove;
	GameObject survivalLeftLowerTextLeftBelow;
	GameObject survivalLeftLowerTextRightAbove;
	GameObject survivalLeftLowerTextRightBelow;

	// RIGHT PANEL
	GameObject survivalThreatsImage;


	void CreateSurvivalItems()
	{
		// invisible panel to hold items
		survivalPanel = (GameObject)Instantiate(uiSubPanel);
		survivalPanel.GetComponent<RectTransform>().SetParent(infoPanelMainPanel.GetComponent<RectTransform>(), false);
	
		// header text
		survivalHeaderTextL = (GameObject)Instantiate(uiText);
		survivalHeaderTextL.GetComponent<RectTransform>().SetParent(survivalPanel.GetComponent<RectTransform>(), false);
		survivalHeaderTextL.GetComponent<Text>().text = "Crossing Roads";
		survivalHeaderTextL.GetComponent<Text>().color =  new Color(0.99f * 0.82f, 0.88f * 0.82f, 0.66f * 0.82f, 1f);
		survivalHeaderTextL.GetComponent<Text>().fontStyle = FontStyle.Normal;

		survivalHeaderTextR = (GameObject)Instantiate(uiText);
		survivalHeaderTextR.GetComponent<RectTransform>().SetParent(survivalPanel.GetComponent<RectTransform>(), false);
		survivalHeaderTextR.GetComponent<Text>().text = "Survival Threats";
		survivalHeaderTextR.GetComponent<Text>().color =  new Color(0.99f * 0.82f, 0.88f * 0.82f, 0.66f * 0.82f, 1f);
		survivalHeaderTextR.GetComponent<Text>().fontStyle = FontStyle.Normal;

		// LEFT PANEL

		survivalLeftUpperBackground = (GameObject)Instantiate(uiPanel);
		survivalLeftUpperBackground.GetComponent<RectTransform>().SetParent(survivalPanel.GetComponent<RectTransform>(), false);

		survivalLeftLowerBackground = (GameObject)Instantiate(uiPanel);
		survivalLeftLowerBackground.GetComponent<RectTransform>().SetParent(survivalPanel.GetComponent<RectTransform>(), false);

		survivalLeftUpperImage = (GameObject)Instantiate(uiRawImage);
		survivalLeftUpperImage.GetComponent<RectTransform>().SetParent(survivalPanel.GetComponent<RectTransform>(), false);
		survivalLeftUpperImage.GetComponent<RawImage>().texture = crossingScreenshotTexture;

		survivalLeftLowerImageL = (GameObject)Instantiate(uiRawImage);
		survivalLeftLowerImageL.GetComponent<RectTransform>().SetParent(survivalPanel.GetComponent<RectTransform>(), false);
		survivalLeftLowerImageL.GetComponent<RawImage>().texture = crossingBadAngleTexture;

		survivalLeftLowerImageR = (GameObject)Instantiate(uiRawImage);
		survivalLeftLowerImageR.GetComponent<RectTransform>().SetParent(survivalPanel.GetComponent<RectTransform>(), false);
		survivalLeftLowerImageR.GetComponent<RawImage>().texture = crossingGoodAngleTexture;

		survivalLeftUpperTextCenter = (GameObject)Instantiate(uiText);
		survivalLeftUpperTextCenter.GetComponent<RectTransform>().SetParent(survivalPanel.GetComponent<RectTransform>(), false);
		survivalLeftUpperTextCenter.GetComponent<Text>().text = "Look Both Ways!";
		survivalLeftUpperTextCenter.GetComponent<Text>().color =  new Color(0.816f * 1.15f, 0.537f * 1.15f, 0.18f * 1.15f, 1f);
		survivalLeftUpperTextCenter.GetComponent<Text>().fontStyle = FontStyle.BoldAndItalic;

		survivalLeftLowerTextCenter = (GameObject)Instantiate(uiText);
		survivalLeftLowerTextCenter.GetComponent<RectTransform>().SetParent(survivalPanel.GetComponent<RectTransform>(), false);
		survivalLeftLowerTextCenter.GetComponent<Text>().text = "Pick a Safe Spot";
		survivalLeftLowerTextCenter.GetComponent<Text>().color =  new Color(0.816f * 1.2f, 0.537f * 1.2f, 0.18f * 1.2f, 1f);
		survivalLeftLowerTextCenter.GetComponent<Text>().fontStyle = FontStyle.BoldAndItalic;

		survivalLeftUpperTextLeftAbove = (GameObject)Instantiate(uiText);
		survivalLeftUpperTextLeftAbove.GetComponent<RectTransform>().SetParent(survivalPanel.GetComponent<RectTransform>(), false);
		survivalLeftUpperTextLeftAbove.GetComponent<Text>().text = "side";
		survivalLeftUpperTextLeftAbove.GetComponent<Text>().color =  new Color(0.88f * 1.1f, 0.64f * 1.1f, 0f, 0.9f);
		survivalLeftUpperTextLeftAbove.GetComponent<Text>().fontStyle = FontStyle.BoldAndItalic;

		survivalLeftUpperTextLeftBelow = (GameObject)Instantiate(uiText);
		survivalLeftUpperTextLeftBelow.GetComponent<RectTransform>().SetParent(survivalPanel.GetComponent<RectTransform>(), false);
		survivalLeftUpperTextLeftBelow.GetComponent<Text>().text = "view";
		survivalLeftUpperTextLeftBelow.GetComponent<Text>().color =  new Color(0.88f * 1.1f, 0.64f * 1.1f, 0f, 0.9f);
		survivalLeftUpperTextLeftBelow.GetComponent<Text>().fontStyle = FontStyle.BoldAndItalic;

		survivalLeftUpperTextRightAbove = (GameObject)Instantiate(uiText);
		survivalLeftUpperTextRightAbove.GetComponent<RectTransform>().SetParent(survivalPanel.GetComponent<RectTransform>(), false);
		survivalLeftUpperTextRightAbove.GetComponent<Text>().text = "side";
		survivalLeftUpperTextRightAbove.GetComponent<Text>().color =  new Color(0.88f * 1.1f, 0.64f * 1.1f, 0f, 0.9f);
		survivalLeftUpperTextRightAbove.GetComponent<Text>().fontStyle = FontStyle.BoldAndItalic;

		survivalLeftUpperTextRightBelow = (GameObject)Instantiate(uiText);
		survivalLeftUpperTextRightBelow.GetComponent<RectTransform>().SetParent(survivalPanel.GetComponent<RectTransform>(), false);
		survivalLeftUpperTextRightBelow.GetComponent<Text>().text = "view";
		survivalLeftUpperTextRightBelow.GetComponent<Text>().color =  new Color(0.88f * 1.1f, 0.64f * 1.1f, 0f, 0.9f);
		survivalLeftUpperTextRightBelow.GetComponent<Text>().fontStyle = FontStyle.BoldAndItalic;

		survivalLeftLowerTextLeftAbove = (GameObject)Instantiate(uiText);
		survivalLeftLowerTextLeftAbove.GetComponent<RectTransform>().SetParent(survivalPanel.GetComponent<RectTransform>(), false);
		survivalLeftLowerTextLeftAbove.GetComponent<Text>().text = "bad";
		survivalLeftLowerTextLeftAbove.GetComponent<Text>().color =  new Color(0.88f * 1.1f, 0.64f * 1.1f, 0f, 0.9f);
		survivalLeftLowerTextLeftAbove.GetComponent<Text>().fontStyle = FontStyle.BoldAndItalic;

		survivalLeftLowerTextLeftBelow = (GameObject)Instantiate(uiText);
		survivalLeftLowerTextLeftBelow.GetComponent<RectTransform>().SetParent(survivalPanel.GetComponent<RectTransform>(), false);
		survivalLeftLowerTextLeftBelow.GetComponent<Text>().text = "view";
		survivalLeftLowerTextLeftBelow.GetComponent<Text>().color =  new Color(0.88f * 1.1f, 0.64f * 1.1f, 0f, 0.9f);
		survivalLeftLowerTextLeftBelow.GetComponent<Text>().fontStyle = FontStyle.BoldAndItalic;

		survivalLeftLowerTextRightAbove = (GameObject)Instantiate(uiText);
		survivalLeftLowerTextRightAbove.GetComponent<RectTransform>().SetParent(survivalPanel.GetComponent<RectTransform>(), false);
		survivalLeftLowerTextRightAbove.GetComponent<Text>().text = "good";
		survivalLeftLowerTextRightAbove.GetComponent<Text>().color =  new Color(0.88f * 1.1f, 0.64f * 1.1f, 0f, 0.9f);
		survivalLeftLowerTextRightAbove.GetComponent<Text>().fontStyle = FontStyle.BoldAndItalic;

		survivalLeftLowerTextRightBelow = (GameObject)Instantiate(uiText);
		survivalLeftLowerTextRightBelow.GetComponent<RectTransform>().SetParent(survivalPanel.GetComponent<RectTransform>(), false);
		survivalLeftLowerTextRightBelow.GetComponent<Text>().text = "view";
		survivalLeftLowerTextRightBelow.GetComponent<Text>().color =  new Color(0.88f * 1.1f, 0.64f * 1.1f, 0f, 0.9f);
		survivalLeftLowerTextRightBelow.GetComponent<Text>().fontStyle = FontStyle.BoldAndItalic;

		// RIGHT PANEL
		survivalThreatsImage = (GameObject)Instantiate(uiRawImage);
		survivalThreatsImage.GetComponent<RectTransform>().SetParent(survivalPanel.GetComponent<RectTransform>(), false);
		survivalThreatsImage.GetComponent<RawImage>().texture = survivalThreatsTexture;
	}

	
	void PositionSurvivalItems()
	{
		float panelX = overlayRect.x + overlayRect.width * 0.06f;
		float panelY = overlayRect.y + overlayRect.height * 0.205f;
		float panelWidth = overlayRect.width * 0.88f;
		float panelHeight = overlayRect.height * 0.6575f;
		float yOffset = overlayRect.height * 0.115f;
		float gapSize = overlayRect.width * 0.02f;
		float roadPanelX = panelX + gapSize*0.75f;
		float roadPanelY1 = panelY  + panelHeight * 0.13f;
		float roadPanelY2 = panelY  + panelHeight * 0.58f;
		float roadPanelWidth = panelWidth * 0.45f - gapSize*0.75f;
		float roadPanelHeight = panelHeight * 0.42f - gapSize*0.75f;			
		float sideViewHeightOffset = roadPanelHeight * 0.03f;
		float sideViewOffsetX = roadPanelWidth * 0.02f;
		float textureX;
		float textureY;
		float textureHeight;
		float textureWidth;

		guiUtils.SetItemOffsets(survivalHeaderTextL, panelX, yOffset + overlayRect.y + overlayRect.height * 0.077f, panelWidth/2 - overlayRect.width * 0.02f - gapSize/2, overlayRect.height * 0.1f);
		survivalHeaderTextL.GetComponent<Text>().fontSize = (int)(overlayRect.width * 0.022f);

		guiUtils.SetItemOffsets(survivalHeaderTextR, panelX + panelWidth/2 + overlayRect.width * 0.02f + gapSize/2, yOffset + overlayRect.y + overlayRect.height * 0.077f, panelWidth/2 - overlayRect.width * 0.02f - gapSize/2, overlayRect.height * 0.1f);
		survivalHeaderTextR.GetComponent<Text>().fontSize = (int)(overlayRect.width * 0.022f);
	
		// LEFT PANEL

		guiUtils.SetItemOffsets(survivalLeftUpperBackground, roadPanelX, roadPanelY1, roadPanelWidth, roadPanelHeight);
		
		guiUtils.SetItemOffsets(survivalLeftLowerBackground, roadPanelX, roadPanelY2, roadPanelWidth, roadPanelHeight);
		
		textureHeight = roadPanelHeight * 0.73f;
		textureWidth = crossingScreenshotTexture.width * (textureHeight / crossingScreenshotTexture.height);
		guiUtils.SetItemOffsets(survivalLeftUpperImage, roadPanelX + roadPanelWidth * 0.5f - textureWidth/2, roadPanelY1 + roadPanelHeight * 0.05f, textureWidth, textureHeight);
		
		textureWidth = roadPanelWidth * 0.45f;
		textureHeight = crossingBadAngleTexture.height * (textureWidth / crossingBadAngleTexture.width);
		guiUtils.SetItemOffsets(survivalLeftLowerImageL, roadPanelX + roadPanelWidth * 0.033f, roadPanelY2 + roadPanelHeight * 0.1f, textureWidth, textureHeight);
		
		textureWidth = roadPanelWidth * 0.45f;
		textureHeight = crossingGoodAngleTexture.height * (textureWidth / crossingGoodAngleTexture.width);
		guiUtils.SetItemOffsets(survivalLeftLowerImageR, roadPanelX + roadPanelWidth * 0.516f, roadPanelY2 + roadPanelHeight * 0.1f, textureWidth, textureHeight);
				
		guiUtils.SetItemOffsets(survivalLeftUpperTextCenter, roadPanelX, roadPanelY1 + roadPanelHeight * 0.78f, roadPanelWidth, roadPanelHeight * 0.2f);
		survivalLeftUpperTextCenter.GetComponent<Text>().fontSize = (int)(overlayRect.width * 0.019f);

		guiUtils.SetItemOffsets(survivalLeftLowerTextCenter, roadPanelX, roadPanelY2 + roadPanelHeight * 0.72f, roadPanelWidth, roadPanelHeight * 0.2f);
		survivalLeftLowerTextCenter.GetComponent<Text>().fontSize = (int)(overlayRect.width * 0.019f);

		guiUtils.SetItemOffsets(survivalLeftUpperTextLeftAbove, roadPanelX, roadPanelY1 + roadPanelHeight * 0.3f + sideViewHeightOffset, roadPanelWidth * 0.2f, roadPanelHeight * 0.2f);
		survivalLeftUpperTextLeftAbove.GetComponent<Text>().fontSize = (int)(overlayRect.width * 0.015f);

		guiUtils.SetItemOffsets(survivalLeftUpperTextLeftBelow, roadPanelX, roadPanelY1 + roadPanelHeight * 0.41f + sideViewHeightOffset, roadPanelWidth * 0.2f, roadPanelHeight * 0.2f);
		survivalLeftUpperTextLeftBelow.GetComponent<Text>().fontSize = (int)(overlayRect.width * 0.015f);

		guiUtils.SetItemOffsets(survivalLeftUpperTextRightAbove, roadPanelX + roadPanelWidth*0.8f, roadPanelY1 + roadPanelHeight * 0.3f + sideViewHeightOffset, roadPanelWidth * 0.2f, roadPanelHeight * 0.2f);
		survivalLeftUpperTextRightAbove.GetComponent<Text>().fontSize = (int)(overlayRect.width * 0.015f);

		guiUtils.SetItemOffsets(survivalLeftUpperTextRightBelow, roadPanelX + roadPanelWidth*0.8f, roadPanelY1 + roadPanelHeight * 0.41f + sideViewHeightOffset, roadPanelWidth * 0.2f, roadPanelHeight * 0.2f);
		survivalLeftUpperTextRightBelow.GetComponent<Text>().fontSize = (int)(overlayRect.width * 0.015f);

		sideViewHeightOffset = roadPanelHeight * 0.32f;

		guiUtils.SetItemOffsets(survivalLeftLowerTextLeftAbove, roadPanelX + sideViewOffsetX, roadPanelY2 + roadPanelHeight * 0.35f + sideViewHeightOffset, roadPanelWidth * 0.2f, roadPanelHeight * 0.2f);
		survivalLeftLowerTextLeftAbove.GetComponent<Text>().fontSize = (int)(overlayRect.width * 0.015f);

		guiUtils.SetItemOffsets(survivalLeftLowerTextLeftBelow, roadPanelX + sideViewOffsetX, roadPanelY2 + roadPanelHeight * 0.46f + sideViewHeightOffset, roadPanelWidth * 0.2f, roadPanelHeight * 0.2f);
		survivalLeftLowerTextLeftBelow.GetComponent<Text>().fontSize = (int)(overlayRect.width * 0.015f);

		guiUtils.SetItemOffsets(survivalLeftLowerTextRightAbove, roadPanelX + roadPanelWidth*0.8f - sideViewOffsetX, roadPanelY2 + roadPanelHeight * 0.35f + sideViewHeightOffset, roadPanelWidth * 0.2f, roadPanelHeight * 0.2f);
		survivalLeftLowerTextRightAbove.GetComponent<Text>().fontSize = (int)(overlayRect.width * 0.015f);

		guiUtils.SetItemOffsets(survivalLeftLowerTextRightBelow, roadPanelX + roadPanelWidth*0.8f - sideViewOffsetX, roadPanelY2 + roadPanelHeight * 0.46f + sideViewHeightOffset, roadPanelWidth * 0.2f, roadPanelHeight * 0.2f);
		survivalLeftLowerTextRightBelow.GetComponent<Text>().fontSize = (int)(overlayRect.width * 0.015f);

		// RIGHT PANEL
		guiUtils.SetItemOffsets(survivalThreatsImage, panelX + panelWidth * 0.555f, panelY + panelHeight * 0.12f, panelWidth * 0.425f, panelHeight * 0.85f);

	}

	
	void UpdateSurvivalItems()
	{
		survivalPanel.SetActive((newLevelFlag == false && currentLevel != 6 && currentScreen == 3) ? true : false);
	}	

	
	void DrawSurvivalScreen(float panelOpacity) 
	{ 
		//////////////////
		//////////////////
		// OLD GUI CODE
		//////////////////
		//////////////////

		float panelX = overlayRect.x + overlayRect.width * 0.06f;
		float panelY = overlayRect.y + overlayRect.height * 0.205f;
		float panelWidth = overlayRect.width * 0.88f;
		float panelHeight = overlayRect.height * 0.6575f;
		float fontScale = 0.8f;

		GUIStyle style = new GUIStyle();
		style.alignment = TextAnchor.MiddleCenter;

		float yOffset = overlayRect.height * 0.022f;

		if (USE_NEW_GUI == false) {
			// two labels at top of panel	
			GUI.color = new Color(0f, 0f, 0f, 1f * panelOpacity);
			GUI.Box(new Rect(panelX - overlayRect.width * 0.02f, yOffset + overlayRect.y + overlayRect.height * 0.095f, overlayRect.width * 0.14f, overlayRect.height * 0.064f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * panelOpacity);
			style.fontSize = (int)(overlayRect.width * 0.021f);
			style.fontStyle = FontStyle.BoldAndItalic;
			style.normal.textColor = new Color(0.816f, 0.537f, 0.18f, 1f);
			GUI.Button(new Rect(panelX - overlayRect.width * 0.02f, yOffset + overlayRect.y + overlayRect.height * 0.077f, overlayRect.width * 0.14f, overlayRect.height * 0.1f), "In Game", style);

			GUI.color = new Color(0f, 0f, 0f, 1f * panelOpacity);
			GUI.Box(new Rect(overlayRect.x + overlayRect.width - overlayRect.width * 0.2f, yOffset + overlayRect.y + overlayRect.height * 0.095f, overlayRect.width * 0.16f, overlayRect.height * 0.064f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * panelOpacity);
			style.fontSize = (int)(overlayRect.width * 0.021f);
			style.fontStyle = FontStyle.BoldAndItalic;
			style.normal.textColor = new Color(0.816f, 0.537f, 0.18f, 1f);
			GUI.Button(new Rect(overlayRect.x + overlayRect.width - overlayRect.width * 0.2f, yOffset + overlayRect.y + overlayRect.height * 0.077f, overlayRect.width * 0.16f, overlayRect.height * 0.1f), "Real World", style);
		}

		// background rectangles
		GUI.color = new Color(1f, 1f, 1f, 1f * panelOpacity);
		float gapSize = overlayRect.width * 0.02f;
		
		//left side
		if (USE_NEW_GUI == false) {
			GUI.color = new Color(0f, 0f, 0f, 1f * panelOpacity);
			GUI.Box(new Rect(panelX - overlayRect.width * 0.02f, panelY - overlayRect.height * 0.025f, panelWidth/2 + overlayRect.width * 0.02f - gapSize/2, panelHeight + overlayRect.height * 0.05f), "");
			GUI.color = new Color(0f, 0f, 0f, 0.5f * panelOpacity);
			GUI.Box(new Rect(panelX, panelY, panelWidth/2 - overlayRect.width * 0.02f - gapSize/2, panelHeight), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * panelOpacity);
		}

		float roadPanelX = panelX + gapSize*0.75f;
		float roadPanelY1 = panelY  + panelHeight * 0.13f;
		float roadPanelY2 = panelY  + panelHeight * 0.58f;
		float roadPanelWidth = panelWidth * 0.45f - gapSize*0.75f;
		float roadPanelHeight = panelHeight * 0.42f - gapSize*0.75f;			
		if (USE_NEW_GUI == false) {
			GUI.Box(new Rect(roadPanelX, roadPanelY1, roadPanelWidth, roadPanelHeight), "");
			GUI.Box(new Rect(roadPanelX, roadPanelY2, roadPanelWidth, roadPanelHeight), "");
		}

		// right side
		if (USE_NEW_GUI == false) {
			GUI.color = new Color(0f, 0f, 0f, 1f * panelOpacity);
			GUI.Box(new Rect(panelX + panelWidth/2 + gapSize/2, panelY - overlayRect.height * 0.025f, panelWidth/2 + overlayRect.width * 0.02f - gapSize/2, panelHeight + overlayRect.height * 0.05f), "");
			GUI.color = new Color(0f, 0f, 0f, 0.5f * panelOpacity);
			GUI.Box(new Rect(panelX + panelWidth/2 + overlayRect.width * 0.02f + gapSize/2, panelY, panelWidth/2 - overlayRect.width * 0.02f - gapSize/2, panelHeight), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * panelOpacity);
		}
		
		
		
		// HEADER TEXT 
		
		float textureX;
		float textureY;
		float textureHeight;
		float textureWidth;

		string headerTextL = "Crossing Roads";
		string headerTextR = "Survival Threats";
		
		yOffset = overlayRect.height * 0.115f;
		float colorScale = 0.74f;
		
		if (USE_NEW_GUI == false) {
			GUI.color = new Color(1f, 1f, 1f, 1f * panelOpacity);
			style.fontSize = (int)(overlayRect.width * 0.022f);
			style.fontStyle = FontStyle.Normal;
			style.normal.textColor = new Color(0.99f * colorScale, 0.88f * colorScale, 0.66f * colorScale, 1f);
			GUI.Button(new Rect(panelX, yOffset + overlayRect.y + overlayRect.height * 0.077f, panelWidth/2 - overlayRect.width * 0.02f - gapSize/2, overlayRect.height * 0.1f), headerTextL, style);

			GUI.color = new Color(1f, 1f, 1f, 1f * panelOpacity);
			style.fontSize = (int)(overlayRect.width * 0.022f);
			style.fontStyle = FontStyle.Normal;
			style.normal.textColor = new Color(0.99f * colorScale, 0.88f * colorScale, 0.66f * colorScale, 1f);
			GUI.Button(new Rect(panelX + panelWidth/2 + overlayRect.width * 0.02f + gapSize/2, yOffset + overlayRect.y + overlayRect.height * 0.077f, panelWidth/2 - overlayRect.width * 0.02f - gapSize/2, overlayRect.height * 0.1f), headerTextR, style);
		}

		// labels
		
		GUI.color = new Color(1f, 1f, 1f, panelOpacity);

		colorScale = 0.74f;
		style.normal.textColor = new Color(0.99f * colorScale, 0.88f * colorScale, 0.66f * colorScale, 1f);
		style.fontSize = (int)(overlayRect.width * 0.014f);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.MiddleCenter;

		if (USE_NEW_GUI == false) {
			style.fontSize = (int)(overlayRect.width * 0.019f);
			style.fontStyle = FontStyle.BoldAndItalic;
			style.normal.textColor = new Color(0.816f * 1.0f, 0.537f * 1.0f, 0.18f * 1.0f, 1f);
			GUI.Button(new Rect(roadPanelX, roadPanelY1 + roadPanelHeight * 0.78f, roadPanelWidth, roadPanelHeight * 0.2f), "Look Both Ways!", style);	
			GUI.Button(new Rect(roadPanelX, roadPanelY2 + roadPanelHeight * 0.72f, roadPanelWidth, roadPanelHeight * 0.2f), "Pick a Safe Spot", style);
		}
			
		// explanatory text
		
		float textOffsetY = panelHeight * 0.02f;
		style.fontSize = (int)(overlayRect.width * 0.015f);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.normal.textColor = new Color(0.88f, 0.64f, 0f, 0.9f);
		float sideViewHeightOffset = roadPanelHeight * 0.03f;

		if (USE_NEW_GUI == false) {
			GUI.Button(new Rect(roadPanelX, roadPanelY1 + roadPanelHeight * 0.3f + sideViewHeightOffset, roadPanelWidth * 0.2f, roadPanelHeight * 0.2f), "side", style);
			GUI.Button(new Rect(roadPanelX + roadPanelWidth*0.8f, roadPanelY1 + roadPanelHeight * 0.3f + sideViewHeightOffset, roadPanelWidth * 0.2f, roadPanelHeight * 0.2f), "side", style);
			GUI.Button(new Rect(roadPanelX, roadPanelY1 + roadPanelHeight * 0.41f + sideViewHeightOffset, roadPanelWidth * 0.2f, roadPanelHeight * 0.2f), "view", style);
			GUI.Button(new Rect(roadPanelX + roadPanelWidth*0.8f, roadPanelY1 + roadPanelHeight * 0.41f + sideViewHeightOffset, roadPanelWidth * 0.2f, roadPanelHeight * 0.2f), "view", style);

			sideViewHeightOffset = roadPanelHeight * 0.32f;
			GUI.Button(new Rect(roadPanelX, roadPanelY2 + roadPanelHeight * 0.35f + sideViewHeightOffset, roadPanelWidth * 0.2f, roadPanelHeight * 0.2f), "bad", style);
			GUI.Button(new Rect(roadPanelX + roadPanelWidth*0.8f, roadPanelY2 + roadPanelHeight * 0.35f + sideViewHeightOffset, roadPanelWidth * 0.2f, roadPanelHeight * 0.2f), "good", style);
			GUI.Button(new Rect(roadPanelX, roadPanelY2 + roadPanelHeight * 0.46f + sideViewHeightOffset, roadPanelWidth * 0.2f, roadPanelHeight * 0.2f), "view", style);
			GUI.Button(new Rect(roadPanelX + roadPanelWidth*0.8f, roadPanelY2 + roadPanelHeight * 0.46f + sideViewHeightOffset, roadPanelWidth * 0.2f, roadPanelHeight * 0.2f), "view", style);
		}
			
		// ROAD REPRESENTATION
		GUI.color = new Color(1f, 1f, 1f, 0.85f * panelOpacity);

		if (USE_NEW_GUI == false) {
			textureHeight = roadPanelHeight * 0.73f;
			textureWidth = crossingScreenshotTexture.width * (textureHeight / crossingScreenshotTexture.height);
			GUI.DrawTexture(new Rect(roadPanelX + roadPanelWidth * 0.5f - textureWidth/2, roadPanelY1 + roadPanelHeight * 0.05f, textureWidth, textureHeight), crossingScreenshotTexture);

			GUI.color = new Color(1f, 1f, 1f, 1f * panelOpacity);

			textureWidth = roadPanelWidth * 0.45f;
			textureHeight = crossingBadAngleTexture.height * (textureWidth / crossingBadAngleTexture.width);
			GUI.DrawTexture(new Rect(roadPanelX + roadPanelWidth * 0.033f, roadPanelY2 + roadPanelHeight * 0.1f, textureWidth, textureHeight), crossingBadAngleTexture);

			textureWidth = roadPanelWidth * 0.45f;
			textureHeight = crossingGoodAngleTexture.height * (textureWidth / crossingGoodAngleTexture.width);
			GUI.DrawTexture(new Rect(roadPanelX + roadPanelWidth * 0.516f, roadPanelY2 + roadPanelHeight * 0.1f, textureWidth, textureHeight), crossingGoodAngleTexture);
		}
	}	


	//======================================================================
	//======================================================================
	//======================================================================
	//								DONATE
	//======================================================================
	//======================================================================
	//======================================================================

	GameObject donateText1;
	GameObject donateText2;
	GameObject donateText3;
	GameObject donateNowButton;
	GameObject felidaeButton;
	GameObject felidaeLogo;
	GameObject felidaeClickRect;
	GameObject bappButton;
	GameObject bappLogo;
	GameObject bappClickRect;
	GameObject socialLink1;
	GameObject socialLink2;
	GameObject socialLink3;
	GameObject socialLink4;
	GameObject socialLink5;
	GameObject socialLink6;
	GameObject pumaKittenImage;

	
	void CreateDonateItems()
	{
		// text items
		donateText1 = (GameObject)Instantiate(uiText);
		donateText1.GetComponent<RectTransform>().SetParent(donatePanel.GetComponent<RectTransform>(), false);
		donateText1.GetComponent<Text>().text = "Pumas in the Real World need your help";
		donateText1.GetComponent<Text>().color =  new Color(0.85f, 0.55f, 0.03f, 1f);
		donateText1.GetComponent<Text>().fontStyle = FontStyle.Bold;
		donateText1.GetComponent<Text>().horizontalOverflow = HorizontalWrapMode.Wrap;
		donateText1.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;

		donateText2 = (GameObject)Instantiate(uiText);
		donateText2.GetComponent<RectTransform>().SetParent(donatePanel.GetComponent<RectTransform>(), false);
		donateText2.GetComponent<Text>().text = "Help support our work to study and protect pumas and their habitats. You will help pumas overcome survival threats, and the challenges you saw in the game.";
		donateText2.GetComponent<Text>().color =  new Color(0.65f, 0.65f, 0.65f, 1f);
		donateText2.GetComponent<Text>().fontStyle = FontStyle.Bold;
		donateText2.GetComponent<Text>().horizontalOverflow = HorizontalWrapMode.Wrap;
		donateText2.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;

		donateText3 = (GameObject)Instantiate(uiText);
		donateText3.GetComponent<RectTransform>().SetParent(donatePanel.GetComponent<RectTransform>(), false);
		donateText3.GetComponent<Text>().text = "Visit us and learn more...";
		donateText3.GetComponent<Text>().color =  new Color(0.85f, 0.55f, 0.03f, 1f);
		donateText3.GetComponent<Text>().fontStyle = FontStyle.Bold;
		donateText3.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;

		// three big buttons	 
		donateNowButton = (GameObject)Instantiate(uiButton);
		donateNowButton.GetComponent<RectTransform>().SetParent(donatePanel.GetComponent<RectTransform>(), false);
		donateNowButton.GetComponent<RectTransform>().FindChild("Text").GetComponent<Text>().text = "DONATE NOW";
		donateNowButton.GetComponent<Button>().onClick.AddListener( delegate { Application.OpenURL("http://www.felidaefund.org/donate"); } );
		donateNowButton.GetComponent<RectTransform>().FindChild("Text").GetComponent<Text>().color = new Color(1f, 1f, 1f, 1f);
		donateNowButton.GetComponent<RectTransform>().FindChild("Text").GetComponent<Text>().fontStyle = FontStyle.Bold;
		donateNowButton.GetComponent<Image>().sprite = donateButtonSprite;

		felidaeButton = (GameObject)Instantiate(uiButton);
		felidaeButton.GetComponent<RectTransform>().SetParent(donatePanel.GetComponent<RectTransform>(), false);
		felidaeButton.GetComponent<RectTransform>().FindChild("Text").GetComponent<Text>().text = "";
		felidaeButton.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.8f);
		felidaeButton.GetComponent<Button>().onClick.AddListener( delegate { Application.OpenURL("http://www.felidaefund.org"); } );
		
		felidaeLogo = (GameObject)Instantiate(uiRawImage);
		felidaeLogo.GetComponent<RectTransform>().SetParent(donatePanel.GetComponent<RectTransform>(), false);
		felidaeLogo.GetComponent<RawImage>().texture = logoFelidaeTexture;
		felidaeLogo.GetComponent<RawImage>().color = new Color(1f, 1f, 1f, 0.85f);

		felidaeClickRect = (GameObject)Instantiate(uiButtonSeeThru);
		felidaeClickRect.GetComponent<RectTransform>().SetParent(donatePanel.GetComponent<RectTransform>(), false);
		felidaeClickRect.GetComponent<RectTransform>().FindChild("Text").GetComponent<Text>().text = "";
		felidaeClickRect.GetComponent<Button>().onClick.AddListener( delegate { Application.OpenURL("http://www.felidaefund.org"); } );

		bappButton = (GameObject)Instantiate(uiButton);
		bappButton.GetComponent<RectTransform>().SetParent(donatePanel.GetComponent<RectTransform>(), false);
		bappButton.GetComponent<RectTransform>().FindChild("Text").GetComponent<Text>().text = "";
		bappButton.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.8f);
		bappButton.GetComponent<Button>().onClick.AddListener( delegate { Application.OpenURL("http://www.bapp.org"); } );
		
		bappLogo = (GameObject)Instantiate(uiRawImage);
		bappLogo.GetComponent<RectTransform>().SetParent(donatePanel.GetComponent<RectTransform>(), false);
		bappLogo.GetComponent<RawImage>().texture = logoBappTexture;
		bappLogo.GetComponent<RawImage>().color = new Color(1f, 1f, 1f, 0.85f);

		bappClickRect = (GameObject)Instantiate(uiButtonSeeThru);
		bappClickRect.GetComponent<RectTransform>().SetParent(donatePanel.GetComponent<RectTransform>(), false);
		bappClickRect.GetComponent<RectTransform>().FindChild("Text").GetComponent<Text>().text = "";
		bappClickRect.GetComponent<Button>().onClick.AddListener( delegate { Application.OpenURL("http://www.bapp.org"); } );

		// social links
		
		socialLink1 = (GameObject)Instantiate(uiImageButton);
		socialLink1.GetComponent<RectTransform>().SetParent(donatePanel.GetComponent<RectTransform>(), false);
		socialLink1.GetComponent<RawImage>().texture = iconFacebookTexture;
		socialLink1.GetComponent<RawImage>().color = new Color(1f, 1f, 1f, 0.82f);
		socialLink1.GetComponent<Button>().onClick.AddListener( delegate { Application.OpenURL("http://www.facebook.com/felidaefund"); } );
		
		socialLink2 = (GameObject)Instantiate(uiImageButton);
		socialLink2.GetComponent<RectTransform>().SetParent(donatePanel.GetComponent<RectTransform>(), false);
		socialLink2.GetComponent<RawImage>().texture = iconTwitterTexture;
		socialLink2.GetComponent<RawImage>().color = new Color(1f, 1f, 1f, 0.82f);
		socialLink2.GetComponent<Button>().onClick.AddListener( delegate { Application.OpenURL("http://www.twitter.com/felidaefund"); } );
		
		socialLink3 = (GameObject)Instantiate(uiImageButton);
		socialLink3.GetComponent<RectTransform>().SetParent(donatePanel.GetComponent<RectTransform>(), false);
		socialLink3.GetComponent<RawImage>().texture = iconGoogleTexture;
		socialLink3.GetComponent<RawImage>().color = new Color(1f, 1f, 1f, 0.82f);
		socialLink3.GetComponent<Button>().onClick.AddListener( delegate { Application.OpenURL("http://plus.google.com/u/0/118124929806137459330/posts"); } );
		
		socialLink4 = (GameObject)Instantiate(uiImageButton);
		socialLink4.GetComponent<RectTransform>().SetParent(donatePanel.GetComponent<RectTransform>(), false);
		socialLink4.GetComponent<RawImage>().texture = iconPinterestTexture;
		socialLink4.GetComponent<RawImage>().color = new Color(1f, 1f, 1f, 0.82f);
		socialLink4.GetComponent<Button>().onClick.AddListener( delegate { Application.OpenURL("http://www.pinterest.com/felidaefund"); } );
		
		socialLink5 = (GameObject)Instantiate(uiImageButton);
		socialLink5.GetComponent<RectTransform>().SetParent(donatePanel.GetComponent<RectTransform>(), false);
		socialLink5.GetComponent<RawImage>().texture = iconYouTubeTexture;
		socialLink5.GetComponent<RawImage>().color = new Color(1f, 1f, 1f, 0.82f);
		socialLink5.GetComponent<Button>().onClick.AddListener( delegate { Application.OpenURL("http://www.youtube.com/felidaefund"); } );
		
		socialLink6 = (GameObject)Instantiate(uiImageButton);
		socialLink6.GetComponent<RectTransform>().SetParent(donatePanel.GetComponent<RectTransform>(), false);
		socialLink6.GetComponent<RawImage>().texture = iconLinkedInTexture;
		socialLink6.GetComponent<RawImage>().color = new Color(1f, 1f, 1f, 0.82f);
		socialLink6.GetComponent<Button>().onClick.AddListener( delegate { Application.OpenURL("http://www.linkedin.com/groups/Felidae-Conservation-Fund-1108927?gid=1108927&trk=hb_side_g"); } );

		// Images

		pumaKittenImage = guiUtils.CreateImage(donatePanel, pumaKittenTexture, new Color(1f, 1f, 1f, 1f));
	}

	
	void PositionDonateItems()
	{
		float panelX = overlayRect.x + overlayRect.width * 0.06f;
		float panelY = overlayRect.y + overlayRect.height * 0.205f;
		float panelWidth = overlayRect.width * 0.88f;
		float panelHeight = overlayRect.height * 0.6575f;

		// text items
		guiUtils.SetItemOffsets(donateText1, panelX + panelWidth * 0.025f, panelY + panelHeight * 0.075f, panelWidth * 0.6f, panelHeight * 0.193f);
		donateText1.GetComponent<Text>().fontSize = (int)(panelWidth * 0.05f);

		guiUtils.SetItemOffsets(donateText2, panelX + panelWidth * 0.025f, panelY + panelHeight * 0.325f, panelWidth * 0.6f, panelHeight * 0.293f);
		donateText2.GetComponent<Text>().fontSize = (int)(panelWidth * 0.02f);
		
		guiUtils.SetItemOffsets(donateText3, panelX + panelWidth * 0.025f, panelY + panelHeight * 0.425f , panelWidth * 0.6f, panelHeight * 0.313f);
		donateText3.GetComponent<Text>().fontSize = (int)(panelWidth * 0.02f);
		
		// three big buttons	
		guiUtils.SetItemOffsets(donateNowButton, panelX + panelWidth * 0.675f, panelY + panelHeight * 0.075f, panelWidth * 0.28f, panelHeight * 0.19f);
		donateNowButton.GetComponent<RectTransform>().FindChild("Text").GetComponent<Text>().fontSize = (int)(overlayRect.width * 0.028);

		guiUtils.SetItemOffsets(felidaeButton, panelX + panelWidth * 0.04f, panelY + panelHeight * 0.675f, panelWidth * 0.28f, panelHeight * 0.188f);
		float textureHeight = panelHeight * 0.14f;
		float textureWidth = logoFelidaeTexture.width * (textureHeight / logoFelidaeTexture.height);
		float textureX = panelX + panelWidth * 0.058f;
		float textureY = panelY + panelHeight * 0.7f;
		guiUtils.SetItemOffsets(felidaeLogo, textureX, textureY, textureWidth, textureHeight);
		guiUtils.SetItemOffsets(felidaeClickRect, panelX + panelWidth * 0.04f, panelY + panelHeight * 0.55f, panelWidth * 0.28f, panelHeight * 0.188f);
		
		guiUtils.SetItemOffsets(bappButton, panelX + panelWidth * 0.35f, panelY + panelHeight * 0.675f, panelWidth * 0.28f, panelHeight * 0.188f);
		textureHeight = panelHeight * 0.158f;
		textureWidth = logoBappTexture.width * (textureHeight / logoBappTexture.height);
		textureX = panelX + panelWidth * 0.39f;
		textureY = panelY + panelHeight * 0.7f;
		guiUtils.SetItemOffsets(bappLogo, textureX, textureY, textureWidth, textureHeight);
		guiUtils.SetItemOffsets(bappClickRect, panelX + panelWidth * 0.68f, panelY + panelHeight * 0.55f, panelWidth * 0.28f, panelHeight * 0.188f);
		
		// social links
		
		textureHeight = panelHeight * 0.07f;
		textureWidth = iconFacebookTexture.width * (textureHeight / iconFacebookTexture.height);
		textureX = panelX + panelWidth * 0.15f;
		textureY = panelY + panelHeight * 0.905f;
		guiUtils.SetItemOffsets(socialLink1, textureX, textureY, textureWidth, textureHeight);

		textureWidth = iconTwitterTexture.width * (textureHeight / iconTwitterTexture.height);
		textureX += panelWidth * 0.06f;
		guiUtils.SetItemOffsets(socialLink2, textureX, textureY, textureWidth, textureHeight);

		textureWidth = iconGoogleTexture.width * (textureHeight / iconGoogleTexture.height);
		textureX += panelWidth * 0.06f;
		guiUtils.SetItemOffsets(socialLink3, textureX, textureY, textureWidth, textureHeight);

		textureWidth = iconPinterestTexture.width * (textureHeight / iconPinterestTexture.height);
		textureX += panelWidth * 0.06f;
		guiUtils.SetItemOffsets(socialLink4, textureX, textureY, textureWidth, textureHeight);

		textureWidth = iconYouTubeTexture.width * (textureHeight / iconYouTubeTexture.height);
		textureX += panelWidth * 0.06f;
		guiUtils.SetItemOffsets(socialLink5, textureX, textureY, textureWidth, textureHeight);

		textureWidth = iconLinkedInTexture.width * (textureHeight / iconLinkedInTexture.height);
		textureX += panelWidth * 0.06f;
		guiUtils.SetItemOffsets(socialLink6, textureX, textureY, textureWidth, textureHeight);

		// Images
		guiUtils.SetItemOffsets(pumaKittenImage, panelX + panelWidth * 0.7f, panelY + panelHeight * 0.3f, panelWidth * 0.23f, panelHeight * 0.66f);
	}

	
	void UpdateDonateItems(float alpha)
	{
		donatePanel.SetActive((newLevelFlag == false && currentScreen == 4) ? true : false);
		donatePanel.GetComponent<CanvasGroup>().alpha = (currentLevel == 6) ? alpha : 1f;
	}	

	
	void DrawDonateScreen(float panelOpacity)
	{ 
		//////////////////
		//////////////////
		// OLD GUI CODE
		//////////////////
		//////////////////

		float panelX = overlayRect.x + overlayRect.width * 0.06f;
		float panelY = overlayRect.y + overlayRect.height * 0.205f;
		float panelWidth = overlayRect.width * 0.88f;
		float panelHeight = overlayRect.height * 0.6575f;
		float fontScale = 0.8f;

		GUIStyle style = new GUIStyle();
		style.alignment = TextAnchor.MiddleCenter;
		GUI.color = new Color(1f, 1f, 1f, 1f * panelOpacity);

		// background rectangle
		if (USE_NEW_GUI == false) {
			GUI.color = new Color(0f, 0f, 0f, 1f * panelOpacity);
			GUI.Box(new Rect(panelX - overlayRect.width * 0.02f, panelY - overlayRect.height * 0.025f, panelWidth + overlayRect.width * 0.04f, panelHeight + overlayRect.height * 0.05f), "");
			GUI.color = new Color(0f, 0f, 0f, 0.5f * panelOpacity);
			GUI.Box(new Rect(panelX, panelY, panelWidth, panelHeight), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * panelOpacity);
		}


		if (USE_NEW_GUI == false) {
			if (newLevelFlag == false && currentLevel != 6) {
				// label at top of panel	
				float yOffset = overlayRect.height * 0.022f;
				GUI.color = new Color(0f, 0f, 0f, 1f * panelOpacity);
				GUI.Box(new Rect(overlayRect.x + overlayRect.width - overlayRect.width * 0.2f, yOffset + overlayRect.y + overlayRect.height * 0.095f, overlayRect.width * 0.16f, overlayRect.height * 0.064f), "");
				GUI.color = new Color(1f, 1f, 1f, 1f * panelOpacity);
				style.fontSize = (int)(overlayRect.width * 0.021f);
				style.fontStyle = FontStyle.BoldAndItalic;
				style.normal.textColor = new Color(0.816f, 0.537f, 0.18f, 1f);
				GUI.Button(new Rect(overlayRect.x + overlayRect.width - overlayRect.width * 0.2f, yOffset + overlayRect.y + overlayRect.height * 0.077f, overlayRect.width * 0.16f, overlayRect.height * 0.1f), "Real World", style);
			}
		}

		// Help Pumas
		
		if (USE_NEW_GUI == false) {
			// title
			style.fontStyle = FontStyle.BoldAndItalic;
			style.alignment = TextAnchor.MiddleCenter;
			style.fontSize = (int)(panelWidth * 0.026f);
			style.normal.textColor = new Color(0.85f, 0.55f, 0.03f, 1f);
			GUI.Button(new Rect(panelX, panelY + panelHeight * 0.047f, panelWidth * 1f, panelHeight * 0.093f), "Pumas in the Real World need your help", style);
		
			style.fontStyle = FontStyle.BoldAndItalic;
			style.alignment = TextAnchor.MiddleCenter;
			style.fontSize = (int)(panelWidth * 0.019f);
			style.normal.textColor = new Color(0.6f, 0.6f, 0.6f, 1f);
			GUI.Button(new Rect(panelX + panelWidth * 0.4f, panelY + panelHeight * 0.135f, panelWidth * 0.2f, panelHeight * 0.06f), "Help support our work to study and protect pumas and their habitats", style);
		}

		
		if (USE_NEW_GUI == false) {
			// donate button
			guiManager.customGUISkin.button.fontSize = (int)(overlayRect.width * 0.028);
			guiManager.customGUISkin.button.fontStyle = FontStyle.BoldAndItalic;
			guiManager.customGUISkin.button.normal.textColor = new Color(0.85f, 0.1f, 0f, 1f);
			Color defaultHoverColor = guiManager.customGUISkin.button.hover.textColor;
			guiManager.customGUISkin.button.hover.textColor = new Color(0.9f, 0.12f, 0f, 1f);
			guiUtils.DrawRect(new Rect(panelX + panelWidth * 0.36f + panelHeight * 0.01f, panelY + panelHeight * 0.30f + panelHeight * 0.01f, (panelWidth * 0.28f - panelHeight * 0.02f) * 0.5f, panelHeight * 0.19f - panelHeight * 0.023f), new Color(1f, 1f, 1f, 0.15f));	
			if (GUI.Button(new Rect(panelX + panelWidth * 0.36f, panelY + panelHeight * 0.295f, panelWidth * 0.28f, panelHeight * 0.19f), "Donate Now")) {
				Application.OpenURL("http://www.felidaefund.org/donate");
			}
			guiManager.customGUISkin.button.normal.textColor = new Color(0.90f, 0.65f, 0f, 1f);
			guiManager.customGUISkin.button.hover.textColor = defaultHoverColor;
		}


		// "learn more" section

		guiManager.customGUISkin.button.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f);

		if (USE_NEW_GUI == false) {
			style.fontStyle = FontStyle.BoldAndItalic;
			style.alignment = TextAnchor.MiddleCenter;
			style.fontSize = (int)(panelWidth * 0.018f);
			style.normal.textColor = new Color(0.5f, 0.5f, 0.5f, 1f);
			GUI.Button(new Rect(panelX + panelWidth * 0.4f, panelY + panelHeight * 0.618f , panelWidth * 0.2f, panelHeight * 0.06f), "or learn more...", style);
		}
		
		GUI.color = new Color(1f, 1f, 1f, 0.9f * panelOpacity);

		if (USE_NEW_GUI == false) {
			guiManager.customGUISkin.button.fontSize = (int)(overlayRect.width * 0.016);
			guiManager.customGUISkin.button.fontStyle = FontStyle.BoldAndItalic;
			if (GUI.Button(new Rect(panelX + panelWidth * 0.04f, panelY + panelHeight * 0.55f, panelWidth * 0.28f, panelHeight * 0.188f), "")) {
				Application.OpenURL("http://www.felidaefund.org");
			}
		
			guiManager.customGUISkin.button.fontSize = (int)(overlayRect.width * 0.016);
			guiManager.customGUISkin.button.fontStyle = FontStyle.BoldAndItalic;
			if (GUI.Button(new Rect(panelX + panelWidth * 0.68f, panelY + panelHeight * 0.55f, panelWidth * 0.28f, panelHeight * 0.188f), "")) {
				Application.OpenURL("http://www.bapp.org");
			}	
		}
		
		GUI.color = new Color(1f, 1f, 1f, 1f * panelOpacity);
		guiManager.customGUISkin.button.normal.textColor = new Color(0.90f, 0.65f, 0f, 1f);

		float textureHeight;
		float textureWidth;
		float textureX;
		float textureY;

		if (USE_NEW_GUI == false) {
			// felidae and bapp logos
			textureHeight = panelHeight * 0.14f;
			textureWidth = logoFelidaeTexture.width * (textureHeight / logoFelidaeTexture.height);
			textureX = panelX + panelWidth * 0.058f;
			textureY = panelY + panelHeight * 0.57f;
			GUI.color = new Color(1f, 1f, 1f, 0.7f * panelOpacity);
			GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), logoFelidaeTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * panelOpacity);

			textureHeight = panelHeight * 0.158f;
			textureWidth = logoBappTexture.width * (textureHeight / logoBappTexture.height);
			textureX = panelX + panelWidth * 0.74f;
			textureY = panelY + panelHeight * 0.57f;
			GUI.color = new Color(1f, 1f, 1f, 0.6f * panelOpacity);
			GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), logoBappTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * panelOpacity);
		}


		GUI.color = new Color(1f, 1f, 1f, 0.7f * panelOpacity);
		
		
		if (USE_NEW_GUI == false) {
			// facebook
			textureHeight = panelHeight * 0.07f;
			textureWidth = iconFacebookTexture.width * (textureHeight / iconFacebookTexture.height);
			textureX = panelX + panelWidth * 0.333f;
			textureY = panelY + panelHeight * 0.83f;
			if (GUI.Button(new Rect(textureX, textureY, textureWidth, textureHeight), "")) {
				Application.OpenURL("http://www.facebook.com/felidaefund");
			}	
			GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), iconFacebookTexture);
			// twitter
			textureWidth = iconTwitterTexture.width * (textureHeight / iconTwitterTexture.height);
			textureX += panelWidth * 0.06f;
			if (GUI.Button(new Rect(textureX, textureY, textureWidth, textureHeight), "")) {
				Application.OpenURL("http://www.twitter.com/felidaefund");
			}	
			GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), iconTwitterTexture);
			// google
			textureWidth = iconGoogleTexture.width * (textureHeight / iconGoogleTexture.height);
			textureX += panelWidth * 0.06f;
			if (GUI.Button(new Rect(textureX, textureY, textureWidth, textureHeight), "")) {
				Application.OpenURL("http://plus.google.com/u/0/118124929806137459330/posts");
			}	
			GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), iconGoogleTexture);
			// pinterest
			textureWidth = iconPinterestTexture.width * (textureHeight / iconPinterestTexture.height);
			textureX += panelWidth * 0.06f;
			if (GUI.Button(new Rect(textureX, textureY, textureWidth, textureHeight), "")) {
				Application.OpenURL("http://www.pinterest.com/felidaefund");
			}	
			GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), iconPinterestTexture);
			// youtube
			textureWidth = iconYouTubeTexture.width * (textureHeight / iconYouTubeTexture.height);
			textureX += panelWidth * 0.06f;
			if (GUI.Button(new Rect(textureX, textureY, textureWidth, textureHeight), "")) {
				Application.OpenURL("http://www.youtube.com/felidaefund");
			}	
			GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), iconYouTubeTexture);
			// linkedin
			textureWidth = iconLinkedInTexture.width * (textureHeight / iconLinkedInTexture.height);
			textureX += panelWidth * 0.06f;
			if (GUI.Button(new Rect(textureX, textureY, textureWidth, textureHeight), "")) {
				Application.OpenURL("http://www.linkedin.com/groups/Felidae-Conservation-Fund-1108927?gid=1108927&trk=hb_side_g");
			}	
			GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), iconLinkedInTexture);
		}
		GUI.color = new Color(1f, 1f, 1f, 1f * panelOpacity);
	}	


	//===================================
	//===================================
	//	 PUBLIC ACCESS TO MODULE VARS
	//===================================
	//===================================
	
	public int GetCurrentScreen()
	{
		return currentScreen;
	}

	public void SetCurrentScreen(int newVal)
	{
		currentScreen = newVal;
	}
	
	public void SetNewLevelNumber(int levelNum)
	{
		currentLevel = levelNum;
		SetupLevelDisplay();
	}
		
	public int GetLevelNumber()
	{
		return currentLevel;
	}
		
	public void SetNewLevelFlag(bool boolVal)
	{
		newLevelFlag = boolVal;
	}
		
	public void SetBackgroundIsLocked(bool lockedFlag)
	{
		backgroundIsLocked = lockedFlag;
	}
	

	//===================================
	//===================================
	//	 UTILITY FUNCTION
	//===================================
	//===================================
	
	void CalculateOverlayRect()
	{ 
		// This rect is the basis for OverlayPanel and InfoPanel
		// OverlayPanel shape is based on (obsolete) backgroundTexture 
		// InfoPanel shape is wider, so we fit to that, then reduce
		float wideningFactor = 1.074f;

		// Calculate initial vals (with width expansion)
		float overlayRefWidth = backgroundTexture.width * wideningFactor;
		float overlayRefHeight = backgroundTexture.height;
		float overlayAspectRatio = overlayRefWidth / overlayRefHeight;

		// Fit panel to screen
		if (overlayAspectRatio > (float)Screen.width / (float)Screen.height) {
			// overlay wider than screen; tight to left/right
			overlayRect.width = Screen.width;
			overlayRect.height = overlayRect.width / overlayAspectRatio;
			overlayRect.x = 0;
			overlayRect.y = Screen.height/2 - overlayRect.height/2;
		}
		else {
			// overlay narrower than screen; tight to top/bottom
			overlayRect.height = Screen.height;
			overlayRect.width = overlayRect.height * overlayAspectRatio;
			overlayRect.y = 0;
			overlayRect.x = Screen.width/2 - overlayRect.width/2;
		}

		// compensate for width expansion
		float oldWidth = overlayRect.width;
		overlayRect.width /= wideningFactor;
		overlayRect.x += (oldWidth - overlayRect.width) / 2;
	}
}



