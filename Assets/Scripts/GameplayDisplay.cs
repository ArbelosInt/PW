using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// GameplayDisplay
/// Draw the heads up display that integrates with gameplay

public class GameplayDisplay : MonoBehaviour
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
	private float flashStartTime;
	
	// outside dimensions for deer head indicators
	private float indicatorMinX;
	private float indicatorMinY;
	private float indicatorMaxX;
	private float indicatorMaxY;

	// textures based on bitmap files
	private Texture2D arrowTrayTexture;
	private Texture2D arrowTrayTopTexture;
	private Texture2D arrowTrayFlippedTexture;
	private Texture2D arrowTrayTopFlippedTexture;
//	private Texture2D arrowUpTexture;
//	private Texture2D arrowDownTexture;
//	private Texture2D arrowLeftTexture;
//	private Texture2D arrowRightTexture;
	private Texture2D arrowTurnLeftTexture;
	private Texture2D arrowTurnRightTexture;
	private Texture2D pawDiagLeftTexture;
	private Texture2D pawDiagRightTexture;
	private Texture2D pawStraightTexture;
	private Texture2D controlRightIconsTexture;
	private Texture2D controlRightIconsLowerTexture;
	private Texture2D controlLeftJumpTexture;
	private Texture2D controlLeftExitTexture;
	private Texture2D controlLeftDiagLeftTexture;
	private Texture2D controlLeftDiagRightTexture;
	private Texture2D indicatorBuck; 
	private Texture2D indicatorDoe; 
	private Texture2D indicatorFawn; 
	private Texture2D indicatorBkgnd; 
	private Texture2D closeupBackgroundTexture;
	private Texture2D closeup1Texture;
	private Texture2D closeup2Texture;
	private Texture2D closeup3Texture;
	private Texture2D closeup4Texture;
	private Texture2D closeup5Texture;
	private Texture2D closeup6Texture;
	private Texture2D closeupSensesTexture;
	private Texture2D pumaStealthDarkTexture;
	private Texture2D pumaRunDarkTexture;
    private Texture2D blackArrowShortTexture;

	// external modules
	private GuiManager guiManager;
	private GuiComponents guiComponents;
	private GuiUtils guiUtils;
	private LevelManager levelManager;
	private InputControls inputControls;

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
		inputControls = GetComponent<InputControls>();
		
		// texture references from GuiManager
//		arrowTrayTexture = guiManager.arrowTrayTexture;
//		arrowTrayTopTexture = guiManager.arrowTrayTopTexture;
//		arrowTrayFlippedTexture = guiManager.arrowTrayTexture;
//		arrowTrayTopFlippedTexture = guiManager.arrowTrayTopTexture;
//		arrowUpTexture = guiManager.arrowUpTexture;
//		arrowDownTexture = guiManager.arrowDownTexture;
//		arrowLeftTexture = guiManager.arrowLeftTexture;
//		arrowRightTexture = guiManager.arrowRightTexture;
//		arrowTurnLeftTexture = guiManager.arrowTurnLeftTexture;
//		arrowTurnRightTexture = guiManager.arrowTurnRightTexture;
		pawDiagLeftTexture = guiManager.pawDiagLeftTexture;
		pawDiagRightTexture = guiManager.pawDiagRightTexture;
		pawStraightTexture = guiManager.pawStraightTexture;
		controlRightIconsTexture = guiManager.controlRightIconsTexture;
		controlRightIconsLowerTexture = guiManager.controlRightIconsLowerTexture;
		controlLeftJumpTexture = guiManager.controlLeftJumpTexture;
		controlLeftExitTexture = guiManager.controlLeftExitTexture;
		controlLeftDiagLeftTexture = guiManager.controlLeftDiagLeftTexture;
		controlLeftDiagRightTexture = guiManager.controlLeftDiagRightTexture;
		indicatorBuck = guiManager.indicatorBuck;
		indicatorDoe = guiManager.indicatorDoe;
		indicatorFawn = guiManager.indicatorFawn;
		indicatorBkgnd = guiManager.indicatorBkgnd;
		closeupBackgroundTexture = guiManager.closeupBackgroundTexture;
		closeup1Texture = guiManager.closeup1Texture;
		closeup2Texture = guiManager.closeup2Texture;
		closeup3Texture = guiManager.closeup3Texture;
		closeup4Texture = guiManager.closeup4Texture;
		closeup5Texture = guiManager.closeup5Texture;
		closeup6Texture = guiManager.closeup6Texture;
		closeupSensesTexture = guiManager.closeupSensesTexture;
		pumaStealthDarkTexture = guiManager.pumaStealthDarkTexture;
		pumaRunDarkTexture = guiManager.pumaRunDarkTexture;
		blackArrowShortTexture = guiManager.blackArrowShortTexture;

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
	
	public GameObject gameplayPositionIndicatorsBgnd;
	public GameObject gameplayPositionIndicators;
	public GameObject gameplayMovementControls;
	public GameObject gameplayStatusDisplay;

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
	private GameObject leftPawImage;
	private GameObject statusBackgroundPanel;
	private GameObject statusHeadImage;
	private GameObject statusNameText;
	private GameObject menuPawImage;
	private GameObject menuPawText;
	private GameObject healthBar;
	private GameObject navControlUpperPanel;
	private GameObject navControlLowerPanel;
	private GameObject navControlUpperArrowsImage;
	private GameObject navControlLowerArrowsImage;
	private GameObject navControlBigPumaImage;
	private GameObject navControlSmallPumaImage;
	private GameObject indicatorBackgroundPanel;
	private GameObject indicatorPumaHeadImage;
	private GameObject indicatorPumaSensesImage;
	private GameObject indicatorRectLeft;
	private GameObject indicatorRectTop;
	private GameObject indicatorRectRight;
	private GameObject indicatorRectBottom;
	private GameObject indicatorDeerHeadBuck;
	private GameObject indicatorDeerHeadDoe;
	private GameObject indicatorDeerHeadFawn;
	private GameObject cheatButton;
	private GameObject introSmellBackground;
	private GameObject introSmellText;
	private GameObject introSmellArrowLeft;
	private GameObject introSmellArrowRight;
	private GameObject introSmellArrowAbove1;
	private GameObject introSmellArrowAbove2;
	private GameObject introSmellArrowAbove3;
	private GameObject introLeftBtnBackground;
	private GameObject introLeftBtnText1;
	private GameObject introLeftBtnText2;
	private GameObject introLeftBtnText3;
	private GameObject introLeftBtnArrow;
	private GameObject introRightBtnBackground;
	private GameObject introRightBtnText;
	private GameObject introRightBtnArrow;
	private GameObject introOkBackground;
	private GameObject introOkButton;
	private GameObject introOkButtonOverlay;

	private bool previousChasingFlag = false;
	private bool previousDiagLeftFlag = false;
	private int previousSelectedPuma = 0;
	private float previousPositionIndicatorZoom = 0f;


	void CreateGUIItems()
	{
		// set enables to 'off' before populating sub-items
		gameplayPositionIndicatorsBgnd.SetActive(false);
		gameplayPositionIndicators.SetActive(false);
		gameplayMovementControls.SetActive(false);
		gameplayStatusDisplay.SetActive(false);

		// left side
		leftPawImage = 					guiUtils.CreateImage(gameplayMovementControls, pawDiagLeftTexture, new Color(1f, 1f, 1f, 0.8f));
		statusBackgroundPanel = 		guiUtils.CreatePanel(gameplayStatusDisplay, new Color(0f, 0f, 0f, 0.4f * 1.2f));
		statusHeadImage = 				guiUtils.CreateImage(gameplayStatusDisplay, closeup1Texture, new Color(1f, 1f, 1f, 0.85f));
		statusNameText = 				guiUtils.CreateText(gameplayStatusDisplay, "Name", new Color(0.99f, 0.66f, 0f, 0.7f), FontStyle.BoldAndItalic, TextAnchor.UpperCenter);
		statusBackgroundPanel.SetActive(false);
		statusHeadImage.SetActive(false);
		statusNameText.SetActive(false);
		menuPawImage = 					guiUtils.CreateImage(gameplayMovementControls, pawStraightTexture, new Color(1f, 1f, 1f, 0.8f));
		menuPawText = 					guiUtils.CreateText(gameplayMovementControls, "Menu", new Color(0f, 0f, 0f, 0.8f), FontStyle.Bold);
		healthBar =						guiComponents.CreatePumaHealthBar(gameplayStatusDisplay);
		
		// right side
		navControlUpperPanel = 			guiUtils.CreatePanel(gameplayMovementControls, new Color(0f, 0f, 0f, 0.4f * 0.9f));
		navControlLowerPanel = 			guiUtils.CreatePanel(gameplayMovementControls, new Color(0f, 0f, 0f, 0.4f * 1.6f));
		navControlUpperArrowsImage = 	guiUtils.CreateImage(gameplayMovementControls, controlRightIconsTexture, new Color(1f, 1f, 1f, 0.7f));
		navControlLowerArrowsImage = 	guiUtils.CreateImage(gameplayMovementControls, controlRightIconsLowerTexture, new Color(1f, 1f, 1f, 0.75f));
		navControlBigPumaImage = 		guiUtils.CreateImage(gameplayMovementControls, pumaStealthDarkTexture, new Color(1f, 1f, 1f, 0.9f));
		navControlSmallPumaImage = 		guiUtils.CreateImage(gameplayMovementControls, pumaStealthDarkTexture, new Color(1f, 1f, 1f, 0.9f));
		
		// deer head indicators
		indicatorBackgroundPanel = 		guiUtils.CreatePanel(gameplayPositionIndicatorsBgnd, new Color(0f, 0f, 0f, 0.4f));
		indicatorPumaHeadImage = 		guiUtils.CreateImage(gameplayPositionIndicatorsBgnd, closeup1Texture, new Color(1f, 1f, 1f, 0.82f));
		indicatorPumaSensesImage = 		guiUtils.CreateImage(gameplayPositionIndicatorsBgnd, closeupSensesTexture, new Color(1f, 1f, 1f, 0.5f));
		indicatorRectLeft = 			guiUtils.CreateRect(gameplayPositionIndicatorsBgnd, new Color(0f, 0f, 0f, 0.35f));
		indicatorRectTop = 				guiUtils.CreateRect(gameplayPositionIndicatorsBgnd, new Color(0f, 0f, 0f, 0.35f));
		indicatorRectRight = 			guiUtils.CreateRect(gameplayPositionIndicatorsBgnd, new Color(0f, 0f, 0f, 0.35f));
		indicatorRectBottom = 			guiUtils.CreateRect(gameplayPositionIndicatorsBgnd, new Color(0f, 0f, 0f, 0.35f));
		indicatorDeerHeadBuck =			CreateDeerHead(gameplayPositionIndicators, "Buck");
		indicatorDeerHeadDoe =			CreateDeerHead(gameplayPositionIndicators, "Doe");
		indicatorDeerHeadFawn =			CreateDeerHead(gameplayPositionIndicators, "Fawn");
		
		// cheat button
		cheatButton = 					guiUtils.CreateSeeThruButton(gameplayPositionIndicatorsBgnd, "", new Color(1f, 1f, 1f, 1f), FontStyle.Normal);
		cheatButton.GetComponent<Button>().onClick.AddListener( delegate { levelManager.goStraightToFeeding = true; } );

		// intro screen
		introSmellArrowLeft = 			guiUtils.CreateImage(gameplayStatusDisplay, blackArrowShortTexture, new Color(1f, 0.66f, 0f, 1f));
		introSmellArrowRight = 			guiUtils.CreateImage(gameplayStatusDisplay, blackArrowShortTexture, new Color(1f, 0.66f, 0f, 1f));
		introSmellArrowAbove1 = 		guiUtils.CreateImage(gameplayStatusDisplay, blackArrowShortTexture, new Color(1f, 0.66f, 0f, 1f));
		introSmellArrowAbove2 = 		guiUtils.CreateImage(gameplayStatusDisplay, blackArrowShortTexture, new Color(1f, 0.66f, 0f, 1f));
		introSmellArrowAbove3 = 		guiUtils.CreateImage(gameplayStatusDisplay, blackArrowShortTexture, new Color(1f, 0.66f, 0f, 1f));
		introSmellBackground = 			guiUtils.CreatePanel(gameplayStatusDisplay, new Color(0.2f, 0.2f, 0.2f, 1f));
		introSmellText =				guiUtils.CreateText(gameplayStatusDisplay, "Use your sense of smell to locate prey", new Color(0.99f, 0.66f, 0f, 0.9f), FontStyle.BoldAndItalic, TextAnchor.MiddleCenter);
		introLeftBtnArrow = 			guiUtils.CreateImage(gameplayStatusDisplay, blackArrowShortTexture, new Color(1f, 0.66f, 0f, 1f));
		introLeftBtnBackground =  		guiUtils.CreatePanel(gameplayStatusDisplay, new Color(0.2f, 0.2f, 0.2f, 1f));
		introLeftBtnText1 = 			guiUtils.CreateText(gameplayStatusDisplay, "  Use left paw to:", new Color(0.99f, 0.66f, 0f, 0.9f), FontStyle.BoldAndItalic, TextAnchor.UpperLeft);
		introLeftBtnText2 = 			guiUtils.CreateText(gameplayStatusDisplay, "  - SNEAK sideways\n     when stalking", new Color(0.99f, 0.66f, 0f, 0.9f), FontStyle.BoldAndItalic, TextAnchor.UpperLeft);
		introLeftBtnText3 = 			guiUtils.CreateText(gameplayStatusDisplay, "  - JUMP and KILL\n     when chasing", new Color(0.99f, 0.66f, 0f, 0.9f), FontStyle.BoldAndItalic, TextAnchor.UpperLeft);
		introRightBtnArrow = 			guiUtils.CreateImage(gameplayStatusDisplay, blackArrowShortTexture, new Color(1f, 0.66f, 0f, 1f));
		introRightBtnBackground =  		guiUtils.CreatePanel(gameplayStatusDisplay, new Color(0.2f, 0.2f, 0.2f, 1f));
		introRightBtnText = 			guiUtils.CreateText(gameplayStatusDisplay, "  Use this\n  to walk\n  and run", new Color(0.99f, 0.66f, 0f, 0.9f), FontStyle.BoldAndItalic, TextAnchor.MiddleLeft);
		introOkBackground =  			guiUtils.CreatePanel(gameplayStatusDisplay, new Color(0f, 0f, 0f, 0.6f));
        introOkButton = (GameObject)Instantiate(uiButton);
        introOkButton.GetComponent<RectTransform>().SetParent(gameplayStatusDisplay.GetComponent<RectTransform>(), false);
        introOkButton.GetComponent<RectTransform>().FindChild("Text").GetComponent<Text>().text = "Got It !";
        introOkButton.GetComponent<Button>().onClick.AddListener(delegate {    });
		introOkButtonOverlay = 			guiUtils.CreateSeeThruButton(gameplayPositionIndicatorsBgnd, "", new Color(1f, 1f, 1f, 1f), FontStyle.Normal);
		introOkButtonOverlay.GetComponent<Button>().onClick.AddListener( delegate { TurnOffIntroItems(); inputControls.EnableMovement(); } );

		initComplete = true;
	}


	void PositionGUIItems()
	{
		if (initComplete == false)
			return;

		// basic settings of control boxes
		float leftAreaX = Screen.width * 0.04f;
		float leftAreaWidth = Screen.width * 0.37f;
		float leftAreaHeight = Screen.width * 0.12f;
		float leftAreaY = Screen.height - leftAreaHeight - leftAreaX;

		float rightAreaWidth = Screen.width * 0.24f;
		float rightAreaHeight = Screen.width * 0.20f;
		float rightAreaExtraHeight = Screen.width * 0.05f;
		float rightAreaX = Screen.width - rightAreaWidth - leftAreaX;
		float rightAreaY = Screen.height - rightAreaHeight - rightAreaExtraHeight - leftAreaX;

		// change scale of control boxes based on screen size
		float leftAreaScale = 0.8f;
		float rightAreaScale = 0.8f;

		float rightAreaWidthDiff = rightAreaWidth * (rightAreaScale - 1.0f);
		float rightAreaHeightDiff = rightAreaHeight * (rightAreaScale - 1.0f);
		float rightAreaExtraHeightDiff = rightAreaExtraHeight * (rightAreaScale - 1.0f);
		rightAreaX -= rightAreaWidthDiff;
		rightAreaY -= rightAreaHeightDiff + rightAreaExtraHeightDiff;
		rightAreaWidth += rightAreaWidthDiff;
		rightAreaHeight += rightAreaHeightDiff;
		rightAreaExtraHeight += rightAreaExtraHeightDiff;

		float leftAreaWidthDiff = leftAreaWidth * (leftAreaScale - 1.0f);
		float leftAreaHeightDiff = leftAreaHeight * (leftAreaScale - 1.0f);
		leftAreaY -= leftAreaHeightDiff;
		leftAreaWidth += leftAreaWidthDiff;
		leftAreaHeight += leftAreaHeightDiff;

		//	
		//	the rest of the settings
		//

		float leftControlUpshift = leftAreaHeight * 0.6f;
		float leftItemsLeftshift = leftAreaWidth * 0.35f;

		// left side
		float textureX = leftAreaX - leftAreaWidth * 0.027f;
		float textureY = leftAreaY - leftAreaHeight * 0.16f;			
		float textureHeight = leftAreaHeight * 1.25f;
		float textureWidth = (pawDiagLeftTexture.width * (textureHeight / pawDiagLeftTexture.height)) * 1.1f;
		guiUtils.SetItemOffsets(leftPawImage, textureX + textureWidth * 0.10f, textureY + textureHeight * 0.2f - leftControlUpshift, textureWidth * 0.8f, textureHeight * 0.94f * 0.8f);
		float statusPanelWidth = leftAreaWidth * 0.11f / 0.37f;
		float statusPanelHeight = leftAreaHeight * 0.88f;
		float statusPanelX = leftAreaX + leftAreaWidth * 0.45f;
		float statusPanelY = leftAreaY + leftAreaHeight - statusPanelHeight - leftAreaHeight * 0.03f;
		guiUtils.SetItemOffsets(statusBackgroundPanel, statusPanelX - leftItemsLeftshift, statusPanelY, statusPanelWidth, statusPanelHeight);
		textureHeight = statusPanelHeight * 0.58f;
		textureWidth = statusHeadImage.GetComponent<RawImage>().texture.width * (textureHeight / statusHeadImage.GetComponent<RawImage>().texture.height);
		textureX = statusPanelX + statusPanelWidth - textureWidth - statusPanelWidth * 0.08f;
		textureY = statusPanelY + statusPanelHeight * 0.03f;
		guiUtils.SetItemOffsets(statusHeadImage,textureX - leftItemsLeftshift, textureY, textureWidth, textureHeight);
		float textX = statusPanelX;
		float textY = statusPanelY + statusPanelHeight * 0.70f;
		float textWidth = statusPanelWidth;
		float textHeight = statusPanelHeight * 0.3f;
		float fontRef = statusPanelWidth * 1000f / 320f;
		guiUtils.SetTextOffsets(statusNameText, textX - leftItemsLeftshift, textY, textWidth, textHeight, (int)(fontRef * 0.062f));
		guiUtils.SetItemOffsets(menuPawImage, leftAreaX + leftAreaWidth * 0.82f - leftItemsLeftshift, leftAreaY + leftAreaHeight * 0.17f, leftAreaWidth * 0.41f * 0.8f, leftAreaHeight * 0.8f);
		guiUtils.SetTextOffsets(menuPawText, leftAreaX + leftAreaWidth * 0.82f - leftItemsLeftshift, leftAreaY + leftAreaHeight * 0.5f, leftAreaWidth * 0.41f * 0.8f, leftAreaHeight * 0.5f, (int)(leftAreaWidth * 0.06f));
		float healthBarWidth = Screen.height * 0.36f;
		float healthBarHeight = Screen.height * 0.04f;
		float healthBarX = Screen.width/2 - healthBarWidth/2;
		float healthBarY = Screen.height - healthBarHeight - healthBarHeight * 0.3f;
		guiComponents.PositionPumaHealthBar(healthBar, guiManager.selectedPuma, healthBarX, healthBarY, healthBarWidth, healthBarHeight);
		
		// right side
		guiUtils.SetItemOffsets(navControlUpperPanel, rightAreaX, rightAreaY, rightAreaWidth, rightAreaHeight * 1.01f);
		guiUtils.SetItemOffsets(navControlLowerPanel, rightAreaX, rightAreaY + rightAreaHeight, rightAreaWidth, rightAreaExtraHeight);
		guiUtils.SetItemOffsets(navControlUpperArrowsImage, rightAreaX, rightAreaY - rightAreaExtraHeight*0.1f, rightAreaWidth, rightAreaHeight + rightAreaExtraHeight);
		guiUtils.SetItemOffsets(navControlLowerArrowsImage, rightAreaX, rightAreaY - rightAreaExtraHeight*0.1f, rightAreaWidth, rightAreaHeight + rightAreaExtraHeight);
		textureHeight = rightAreaExtraHeight * 1.5f;
		textureWidth = pumaStealthDarkTexture.width * (textureHeight / pumaStealthDarkTexture.height);
		guiUtils.SetItemOffsets(navControlBigPumaImage, rightAreaX + rightAreaWidth/2 - textureWidth*0.52f, rightAreaY + rightAreaHeight*0.0f, textureWidth, textureHeight);
		textureHeight = rightAreaExtraHeight * 0.9f;
		textureWidth = pumaStealthDarkTexture.width * (textureHeight / pumaStealthDarkTexture.height);
		guiUtils.SetItemOffsets(navControlSmallPumaImage, rightAreaX + rightAreaWidth/2 - textureWidth*0.52f, rightAreaY + rightAreaHeight*0.75f, textureWidth, textureHeight);
		
		// deer head indicators
		float positionIndicatorZoom = previousPositionIndicatorZoom;  // use last remembered value
		int borderThickness = (int)(Screen.width * 0.035f);
		indicatorMinX = Screen.width * 0.25f * (1f-positionIndicatorZoom);
		indicatorMinY = Screen.height * 0.05f * (1f-positionIndicatorZoom);
		indicatorMaxX = Screen.width - indicatorMinX;
		indicatorMaxY = Screen.height - (Screen.height * 0.45f * (1f-positionIndicatorZoom));
		float zoom = positionIndicatorZoom > 0.5f ? 1f : positionIndicatorZoom * 2f;
		float scaleFactor = 1f - (1f-zoom)*(1f-zoom);
		indicatorBackgroundPanel.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0.4f * (1f - scaleFactor));
		indicatorPumaHeadImage.GetComponent<RawImage>().color = new Color(1f, 1f, 1f, 0.82f * (1f - scaleFactor));
		float flashingPeriod = 0.5f;
		float flashingOpacity = 0f;
		if (Time.time > flashStartTime + flashingPeriod) {
			flashStartTime = Time.time;
		}
		if (Time.time < flashStartTime + flashingPeriod * 0.3f) {
			// first half
			flashingOpacity = (Time.time - flashStartTime) / (flashingPeriod * 0.3f);
		}
		else {
			// second half
			flashingOpacity = 1f - ((Time.time - flashStartTime - flashingPeriod * 0.3f) / (flashingPeriod * 0.7f));			
		}	
		flashingOpacity = 0.3f + flashingOpacity * 0.5f;
		flashingOpacity = flashingOpacity * flashingOpacity;
		indicatorPumaSensesImage.GetComponent<RawImage>().color = new Color(1f, 1f, 1f, 0.5f * (1f - scaleFactor) * flashingOpacity);
		guiUtils.SetItemOffsets(indicatorBackgroundPanel, indicatorMinX, indicatorMinY, indicatorMaxX - indicatorMinX, indicatorMaxY - indicatorMinY);
		textureWidth = (indicatorMaxX - indicatorMinX) * 0.2f;
		textureHeight = indicatorPumaHeadImage.GetComponent<RawImage>().texture.height * (textureWidth / indicatorPumaHeadImage.GetComponent<RawImage>().texture.width);
		textureX = (indicatorMaxX + indicatorMinX) / 2 - textureWidth/2;
		textureY = (indicatorMaxY + indicatorMinY) / 2 - textureHeight/2;
		guiUtils.SetItemOffsets(indicatorPumaHeadImage, textureX, textureY, textureWidth, textureHeight);
		guiUtils.SetItemOffsets(indicatorPumaSensesImage, indicatorMinX, indicatorMinY, indicatorMaxX - indicatorMinX, indicatorMaxY - indicatorMinY);
		guiUtils.SetItemOffsets(indicatorRectLeft, indicatorMinX, indicatorMinY + borderThickness + 1, borderThickness, (indicatorMaxY-indicatorMinY)-(borderThickness*2) - 2);
		guiUtils.SetItemOffsets(indicatorRectTop, indicatorMinX, indicatorMinY, indicatorMaxX-indicatorMinX, borderThickness);
		guiUtils.SetItemOffsets(indicatorRectRight, indicatorMaxX-borderThickness, indicatorMinY + borderThickness + 1, borderThickness, (indicatorMaxY-indicatorMinY)-(borderThickness*2) - 2);
		guiUtils.SetItemOffsets(indicatorRectBottom, indicatorMinX, indicatorMaxY-borderThickness, indicatorMaxX-indicatorMinX, borderThickness);
		
		// cheat button
		float killButtonX = Screen.width * 0.5f - Screen.height * 0.075f;
		float killButtonY = 0f;
		float killButtonWidth = Screen.height * 0.15f;
		float killButtonHeight = Screen.height * 0.15f;
		guiUtils.SetButtonOffsets(cheatButton, killButtonX,  killButtonY, killButtonWidth, killButtonHeight, 12);

		// intro screen - smell hint
		float height = Screen.height * 0.085f;
		float yPos = Screen.height * 0.12f;
		float width = Screen.width - yPos*2;
		float xPos = (Screen.width/2) - (width/2);
		float bkgndMidHeight = yPos + height/2;
		float bkgndX = xPos;
		float bkgndWidth = width;
		float bkgndY = yPos;
		float bkgndHeight = height;
		guiUtils.SetItemOffsets(introSmellBackground, xPos, yPos, width, height*1.15f);
		guiUtils.SetTextOffsets(introSmellText, xPos, yPos, width, height*1.15f, (int)(Screen.height * 0.045f));

		xPos = Screen.width * 0.03f;
		width = (bkgndX - xPos) * 2;
		float arrowWidth = width;
		xPos += width * 0.1f;
		height = introSmellArrowLeft.GetComponent<RawImage>().texture.height * (width / introSmellArrowLeft.GetComponent<RawImage>().texture.width);
		yPos = bkgndMidHeight - (height/2);
		guiUtils.SetItemOffsets(introSmellArrowLeft, xPos, yPos, width, height*1.3f);
		introSmellArrowLeft.transform.rotation = Quaternion.Euler (0, 0, 180f);

		xPos = Screen.width * 0.97f - width;
		xPos -= width * 0.1f;
		guiUtils.SetItemOffsets(introSmellArrowRight, xPos, yPos, width, height*1.3f);

		yPos = Screen.height * 0.09f;
		xPos = bkgndX + bkgndWidth*0.2f - width/2;
		guiUtils.SetItemOffsets(introSmellArrowAbove1, xPos, yPos, width, height*1.3f);
		introSmellArrowAbove1.transform.rotation = Quaternion.Euler (0, 0, 90f);

		xPos = bkgndX + bkgndWidth*0.5f - width/2;
		guiUtils.SetItemOffsets(introSmellArrowAbove2, xPos, yPos, width, height*1.3f);
		introSmellArrowAbove2.transform.rotation = Quaternion.Euler (0, 0, 90f);

		xPos = bkgndX + bkgndWidth*0.8f - width/2;
		guiUtils.SetItemOffsets(introSmellArrowAbove3, xPos, yPos, width, height*1.3f);
		introSmellArrowAbove3.transform.rotation = Quaternion.Euler (0, 0, 90f);

		// intro screen - left button hint
		height = Screen.height * 0.32f;
		yPos = bkgndY + bkgndHeight + bkgndHeight * 0.7f;
		width = Screen.height * 0.5f;
		xPos = bkgndX * 0.7f;
		bkgndX = xPos;
		bkgndWidth = width;
		bkgndY = yPos;
		bkgndHeight = height;
		float leftPanelRightEdge = xPos + width;
		guiUtils.SetItemOffsets(introLeftBtnBackground, xPos, yPos, width, height);
		guiUtils.SetTextOffsets(introLeftBtnText1, xPos, yPos + height*0.05f, width, height/3, (int)(Screen.height * 0.045f));
		guiUtils.SetTextOffsets(introLeftBtnText2, xPos, yPos + height*0.25f, width, height/3, (int)(Screen.height * 0.045f));
		guiUtils.SetTextOffsets(introLeftBtnText3, xPos, yPos + height*0.63f, width, height/3, (int)(Screen.height * 0.045f));

		xPos = bkgndX;
		width = arrowWidth;
		height = introLeftBtnArrow.GetComponent<RawImage>().texture.height * (width / introLeftBtnArrow.GetComponent<RawImage>().texture.width);
		yPos = bkgndY + bkgndHeight - width*0.4f;
		height *= 2;
		guiUtils.SetItemOffsets(introLeftBtnArrow, xPos, yPos, width, height);
		introLeftBtnArrow.transform.rotation = Quaternion.Euler (0, 0, 270f);

		// intro screen - right button hint
		height = Screen.height * 0.22f;
		yPos = bkgndY;
		width = Screen.height * 0.25f;
		xPos = Screen.width - width - bkgndX;
		bkgndX = xPos;
		bkgndHeight = height;
		bkgndWidth = width;
		guiUtils.SetItemOffsets(introRightBtnBackground, xPos, yPos, width, height);
		guiUtils.SetTextOffsets(introRightBtnText, xPos, yPos, width, height, (int)(Screen.height * 0.045f));

		width = arrowWidth;
		xPos = bkgndX + bkgndWidth - width;
		height = introRightBtnArrow.GetComponent<RawImage>().texture.height * (width / introRightBtnArrow.GetComponent<RawImage>().texture.width);
		yPos = bkgndY + bkgndHeight - width*0.4f;
		height *= 2;
		guiUtils.SetItemOffsets(introRightBtnArrow, xPos, yPos, width, height);
		introRightBtnArrow.transform.rotation = Quaternion.Euler (0, 0, 270f);

		// intro screen - ok button
		height = Screen.height * 0.18f;
		yPos = Screen.height * 0.3f;;
		width = Screen.height * 0.3f;
		xPos = leftPanelRightEdge + ((bkgndX - leftPanelRightEdge)/2) - width/2;
		guiUtils.SetItemOffsets(introOkBackground, xPos, yPos, width, height);
		guiUtils.SetButtonOffsets(introOkButton, xPos + width*0.13f, yPos + width*0.13f, width - width*0.26f, height - width*0.26f, (int)(Screen.height * 0.055f));
		guiUtils.SetButtonOffsets(introOkButtonOverlay, xPos, yPos, width, height, 12);
	}


	public void UpdateGUIItems(float movementControlsOpacity, float positionIndicatorBackgroundOpacity, float positionIndicatorOpacity, float positionIndicatorZoom, float statusDisplayOpacity) 
	{ 
		if (initComplete == false)
			return;
	
		int borderThickness = (int)(Screen.width * 0.035f);

		// basic settings of control boxes
		float leftAreaX = Screen.width * 0.04f;
		float leftAreaWidth = Screen.width * 0.37f;
		float leftAreaHeight = Screen.width * 0.12f;
		float leftAreaY = Screen.height - leftAreaHeight - leftAreaX;

		float rightAreaWidth = Screen.width * 0.24f;
		float rightAreaHeight = Screen.width * 0.20f;
		float rightAreaExtraHeight = Screen.width * 0.05f;
		float rightAreaX = Screen.width - rightAreaWidth - leftAreaX;
		float rightAreaY = Screen.height - rightAreaHeight - rightAreaExtraHeight - leftAreaX;

		// change scale of control boxes based on screen size
		float leftAreaScale = 0.8f; 
		float rightAreaScale = 0.8f;

		float rightAreaWidthDiff = rightAreaWidth * (rightAreaScale - 1.0f);
		float rightAreaHeightDiff = rightAreaHeight * (rightAreaScale - 1.0f);
		float rightAreaExtraHeightDiff = rightAreaExtraHeight * (rightAreaScale - 1.0f);
		rightAreaX -= rightAreaWidthDiff;
		rightAreaY -= rightAreaHeightDiff + rightAreaExtraHeightDiff;
		rightAreaWidth += rightAreaWidthDiff;
		rightAreaHeight += rightAreaHeightDiff;
		rightAreaExtraHeight += rightAreaExtraHeightDiff;

		float leftAreaWidthDiff = leftAreaWidth * (leftAreaScale - 1.0f);
		float leftAreaHeightDiff = leftAreaHeight * (leftAreaScale - 1.0f);
		leftAreaY -= leftAreaHeightDiff;
		leftAreaWidth += leftAreaWidthDiff;
		leftAreaHeight += leftAreaHeightDiff;

		//	
		//	the rest of the settings
		//

		float leftControlUpshift = leftAreaHeight * 0.6f;
		float leftItemsLeftshift = leftAreaWidth * 0.35f;

		bool diagLeftFlag = levelManager.PumaSideStalkDirectionIsLeft();
		bool chasingFlag;
		
		if (levelManager.gameState == "gameStateChasing" || levelManager.gameState == "gameStateFeeding1" || levelManager.gameState == "gameStateFeeding1a")
			chasingFlag = true;
		else if (levelManager.gameState == "gameStateEnteringGameplay1" || levelManager.gameState == "gameStateEnteringGameplay2" || levelManager.gameState == "gameStateStalking")
			chasingFlag = false;
		else
			chasingFlag = previousChasingFlag;

		if (USE_NEW_GUI == true) {

			// check for screen size change
			if (lastSeenScreenWidth != Screen.width || lastSeenScreenHeight != Screen.height) {
				lastSeenScreenWidth = Screen.width;
				lastSeenScreenHeight = Screen.height;
				PositionGUIItems();
			}

			// top level enables and opacities
			gameplayPositionIndicatorsBgnd.SetActive(positionIndicatorBackgroundOpacity > 0f ? true : false);
			gameplayPositionIndicatorsBgnd.GetComponent<CanvasGroup>().alpha = positionIndicatorBackgroundOpacity;
			gameplayPositionIndicators.SetActive(positionIndicatorOpacity > 0f ? true : false);
			gameplayPositionIndicators.GetComponent<CanvasGroup>().alpha = positionIndicatorOpacity;
			gameplayMovementControls.SetActive(movementControlsOpacity > 0f ? true : false);
			gameplayMovementControls.GetComponent<CanvasGroup>().alpha = movementControlsOpacity;
			gameplayStatusDisplay.SetActive(statusDisplayOpacity > 0f ? true : false);
			gameplayStatusDisplay.GetComponent<CanvasGroup>().alpha = statusDisplayOpacity;
			
			// set movement control rects
			float leftButtonExpansion = leftAreaHeight * 0.2f;
			if (movementControlsOpacity > 0f) {
				inputControls.SetRectLeftButton(new Rect(leftAreaX - leftButtonExpansion, leftAreaY - leftControlUpshift - leftButtonExpansion*2, leftAreaWidth * 0.41f + leftButtonExpansion, leftAreaHeight + leftButtonExpansion*4));
				inputControls.SetRectMiddleButton(new Rect(leftAreaX + leftAreaWidth * 0.82f - leftItemsLeftshift, leftAreaY + leftAreaHeight * 0.2f, leftAreaWidth * 0.41f * 0.8f, leftAreaHeight * 0.8f));
				inputControls.SetRectRightButton(new Rect(rightAreaX, rightAreaY, rightAreaWidth, rightAreaHeight + rightAreaExtraHeight));
			}
			
			// update textures that might have changed
			bool callPositionFunction = false;
			if (chasingFlag != previousChasingFlag) {
				if (chasingFlag == true) {
					leftPawImage.GetComponent<RawImage>().texture = controlLeftJumpTexture;
					navControlBigPumaImage.GetComponent<RawImage>().texture = pumaRunDarkTexture;
					navControlSmallPumaImage.GetComponent<RawImage>().texture = pumaRunDarkTexture;
				}
				else {
					leftPawImage.GetComponent<RawImage>().texture = (diagLeftFlag == true ? pawDiagLeftTexture : pawDiagRightTexture);
					previousDiagLeftFlag = diagLeftFlag;
					navControlBigPumaImage.GetComponent<RawImage>().texture = pumaStealthDarkTexture;
					navControlSmallPumaImage.GetComponent<RawImage>().texture = pumaStealthDarkTexture;
				}
				previousChasingFlag = chasingFlag;
			}
			if (chasingFlag == false && diagLeftFlag != previousDiagLeftFlag) {
				leftPawImage.GetComponent<RawImage>().texture = (diagLeftFlag == true ? pawDiagLeftTexture : pawDiagRightTexture);
				previousDiagLeftFlag = diagLeftFlag;
			}
			if (guiManager.selectedPuma != previousSelectedPuma) {
				Texture2D pumaHeadTexture = closeup1Texture;
				string pumaName = "no name";
				switch (guiManager.selectedPuma) {
				case 0:
					pumaHeadTexture = closeup1Texture;
					pumaName = "Eric";
					break;
				case 1:
					pumaHeadTexture = closeup2Texture;
					pumaName = "Palo";
					break;
				case 2:
					pumaHeadTexture = closeup3Texture;
					pumaName = "Mitch";
					break;
				case 3:
					pumaHeadTexture = closeup4Texture;
					pumaName = "Trish";
					break;
				case 4:
					pumaHeadTexture = closeup5Texture;
					pumaName = "Liam";
					break;
				case 5:
					pumaHeadTexture = closeup6Texture;
					pumaName = "Barb";
					break;
				}
				statusHeadImage.GetComponent<RawImage>().texture = pumaHeadTexture;
				statusNameText.GetComponent<Text>().text = pumaName;
				indicatorPumaHeadImage.GetComponent<RawImage>().texture = pumaHeadTexture;
				previousSelectedPuma = guiManager.selectedPuma;
				callPositionFunction = true;
			}
			
			// update positionIndicatorZoom if needed
			if (positionIndicatorZoom != previousPositionIndicatorZoom) {
				previousPositionIndicatorZoom = positionIndicatorZoom;	// must be done first -- previousPositionIndicatorZoom is used by PositionGUIItems()
				callPositionFunction = true;
			}	

			// update flashing of puma senses
			if (positionIndicatorZoom < 1f) {
				callPositionFunction = true;
			}
			
			if (callPositionFunction == true)
				PositionGUIItems();
				
			if (levelManager.buck != null && levelManager.doe != null && levelManager.fawn != null ) {
				PositionDeerHead(indicatorDeerHeadBuck, levelManager.mainHeading, levelManager.pumaObj, levelManager.buck.gameObj, levelManager.buck.type, borderThickness);
				PositionDeerHead(indicatorDeerHeadDoe, levelManager.mainHeading, levelManager.pumaObj, levelManager.doe.gameObj, levelManager.doe.type, borderThickness);
				PositionDeerHead(indicatorDeerHeadFawn, levelManager.mainHeading, levelManager.pumaObj, levelManager.fawn.gameObj, levelManager.fawn.type, borderThickness);
			}
			
			float healthBarWidth = Screen.height * 0.36f;
			float healthBarHeight = Screen.height * 0.032f;
			float healthBarX = Screen.width/2 - healthBarWidth/2;
			float healthBarY = Screen.height - healthBarHeight - healthBarHeight * 0.3f;
			guiComponents.PositionPumaHealthBar(healthBar, guiManager.selectedPuma, healthBarX, healthBarY, healthBarWidth, healthBarHeight);
		}
		else {
			// set enables to 'off'
			gameplayPositionIndicatorsBgnd.SetActive(false);
			gameplayPositionIndicators.SetActive(false);
			gameplayMovementControls.SetActive(false);
			gameplayStatusDisplay.SetActive(false);
		}
	}


	void TurnOffIntroItems() {
		introSmellBackground.SetActive(false);
		introSmellText.SetActive(false);
		introSmellArrowLeft.SetActive(false);
		introSmellArrowRight.SetActive(false);
		introSmellArrowAbove1.SetActive(false);
		introSmellArrowAbove2.SetActive(false);
		introSmellArrowAbove3.SetActive(false);
		introLeftBtnBackground.SetActive(false);
		introLeftBtnText1.SetActive(false);
		introLeftBtnText2.SetActive(false);
		introLeftBtnText3.SetActive(false);
		introLeftBtnArrow.SetActive(false);
		introRightBtnBackground.SetActive(false);
		introRightBtnText.SetActive(false);
		introRightBtnArrow.SetActive(false);
		introOkBackground.SetActive(false);
		introOkButton.SetActive(false);
		introOkButtonOverlay.SetActive(false);
	}

	
	//===================================
	//===================================
	//	  DRAW THE GAMEPLAY DISPLAY
	//===================================
	//===================================
	
	public void Draw(float movementControlsOpacity, float positionIndicatorBackgroundOpacity, float positionIndicatorOpacity, float positionIndicatorZoom, float statusDisplayOpacity) 
	{ 

		if (USE_NEW_GUI == true)
			return; 
		
		
		//////////////////////////////////
		//////////////////////////////////
		
		// LEGACY DRAW CODE

		//////////////////////////////////
		//////////////////////////////////

		
		int borderThickness = (int)(Screen.height * 0.06f);

		float leftAreaX = Screen.height * 0.08f;
		float leftAreaY = Screen.height * 0.80f;
		float leftAreaWidth = Screen.height * 0.37f;
		float leftAreaHeight = Screen.height * 0.12f;

		float rightAreaY = Screen.height * 0.67f;
		float rightAreaWidth = Screen.height * 0.24f;
		float rightAreaHeight = Screen.height * 0.20f;
		float rightAreaExtraHeight = Screen.height * 0.05f;
		float rightAreaX = Screen.width - rightAreaWidth - leftAreaX;

		bool diagLeftFlag = levelManager.PumaSideStalkDirectionIsLeft();
		bool chasingFlag;
		
		if (levelManager.gameState == "gameStateChasing" || levelManager.gameState == "gameStateFeeding1" || levelManager.gameState == "gameStateFeeding1a")
			chasingFlag = true;
		else if (levelManager.gameState == "gameStateEnteringGameplay1" || levelManager.gameState == "gameStateEnteringGameplay2" || levelManager.gameState == "gameStateStalking")
			chasingFlag = false;
		else
			chasingFlag = previousChasingFlag;

		
		GUIStyle style = new GUIStyle();
		style.alignment = TextAnchor.MiddleCenter;

		//guiUtils.DrawRect(new Rect(leftAreaX, leftAreaY, leftAreaWidth, leftAreaHeight), new Color(0f, 0f, 0f, 0.3f));
		//guiUtils.DrawRect(new Rect(rightAreaX, rightAreaY, rightAreaWidth, rightAreaHeight), new Color(0f, 0f, 0f, 0.3f));


		// establish scale factor for movement controls tray and health meter / exit button
//		float boxWidth = Screen.height * 0.30f * 1.4f;
//		float boxHeight = arrowTrayTexture.height * (boxWidth / arrowTrayTexture.width);

		//----------------------
		// MOVEMENT CONTROLS
		//----------------------
					
		// left paw

		float textureX = leftAreaX - leftAreaWidth * 0.027f;
		float textureY = leftAreaY - leftAreaHeight * 0.16f;			
		float textureWidth = leftAreaWidth * 0.47f;
		float textureHeight = leftAreaHeight * 1.25f;

		GUI.color = new Color(1f, 1f, 1f, 1f * movementControlsOpacity);
		if (USE_NEW_GUI == false) {
			if (movementControlsOpacity > 0f)
				inputControls.SetRectLeftButton(new Rect(leftAreaX, leftAreaY, leftAreaWidth * 0.41f, leftAreaHeight));
		}

		Texture2D iconTexture = null;

		if (chasingFlag) {
			//GUI.color = new Color(1f, 1f, 1f, 0.65f * movementControlsOpacity);
			//GUI.DrawTexture(new Rect(textureX, textureY + textureHeight * 0.03f, textureWidth, textureHeight * 0.94f), arrowTrayTopFlippedTexture);
			//GUI.DrawTexture(new Rect(textureX + textureWidth * 0.1f, textureY + textureHeight * 0.05f, textureWidth * 0.8f, textureHeight * 0.9f), arrowTrayFlippedTexture);
			GUI.color = new Color(1f, 1f, 1f, 0.8f * movementControlsOpacity);
			textureWidth = pawStraightTexture.width * (textureHeight / pawStraightTexture.height);
			if (USE_NEW_GUI == false)
				GUI.DrawTexture(new Rect(leftAreaX + leftAreaWidth * 0.0f, leftAreaY + leftAreaHeight * 0.07f, leftAreaWidth * 0.41f * 1.04f, leftAreaHeight * 1.04f), controlLeftJumpTexture);		
		}
		else  {
			GUI.color = new Color(1f, 1f, 1f, 0.8f * movementControlsOpacity);
			iconTexture = levelManager.PumaSideStalkDirectionIsLeft() == true ? pawDiagLeftTexture : pawDiagRightTexture;
			textureWidth = (iconTexture.width * (textureHeight / iconTexture.height)) * 1.1f;
			if (USE_NEW_GUI == false)
				GUI.DrawTexture(new Rect(textureX + textureWidth * 0.10f, textureY + textureHeight * 0.2f, textureWidth * 0.8f, textureHeight * 0.94f * 0.8f), iconTexture);
			//GUI.color = new Color(1f, 1f, 1f, 0.6f * movementControlsOpacity);
			//GUI.DrawTexture(new Rect(leftAreaX, leftAreaY, leftAreaWidth * 0.41f, leftAreaHeight), levelManager.PumaSideStalkDirectionIsLeft() == true ? controlLeftDiagLeftTexture : controlLeftDiagRightTexture);		
		}


		// exit button
		GUI.color = new Color(1f, 1f, 1f, 0.8f * movementControlsOpacity);
		textureWidth = pawStraightTexture.width * (textureHeight / pawStraightTexture.height);
		if (USE_NEW_GUI == false)
			GUI.DrawTexture(new Rect(leftAreaX + leftAreaWidth * 0.82f, leftAreaY + leftAreaHeight * 0.17f, leftAreaWidth * 0.41f * 0.8f, leftAreaHeight * 0.8f), pawStraightTexture);		
		if (USE_NEW_GUI == false) {
			if (movementControlsOpacity > 0f)
				inputControls.SetRectMiddleButton(new Rect(leftAreaX + leftAreaWidth * 0.82f, leftAreaY + leftAreaHeight * 0.2f, leftAreaWidth * 0.41f * 0.8f, leftAreaHeight * 0.8f));
		}
		GUI.color = new Color(1f, 1f, 1f, 1f * movementControlsOpacity);
		style.fontSize = (int)(leftAreaWidth * 0.06f);
		style.fontStyle = FontStyle.Bold;
		style.normal.textColor = new Color(0f, 0f, 0f, 0.8f);
		if (USE_NEW_GUI == false)
			GUI.Button(new Rect(leftAreaX + leftAreaWidth * 0.82f, leftAreaY + leftAreaHeight * 0.5f, leftAreaWidth * 0.41f * 0.8f, leftAreaHeight * 0.5f), "Menu", style);
		
		// right paw
	
		textureX = rightAreaX - rightAreaWidth * 0.065f;
		textureY = rightAreaY - rightAreaHeight * 0.16f;			
		textureWidth = rightAreaWidth * 1.14f;
		textureHeight = rightAreaHeight * 1.25f;
		
		GUI.color = new Color(1f, 1f, 1f, 0.8f * movementControlsOpacity);
		if (USE_NEW_GUI == false) {
			GUI.Box(new Rect(rightAreaX, rightAreaY, rightAreaWidth, rightAreaHeight), "");
			GUI.Box(new Rect(rightAreaX, rightAreaY + rightAreaHeight, rightAreaWidth, rightAreaExtraHeight), "");
			GUI.Box(new Rect(rightAreaX, rightAreaY + rightAreaHeight, rightAreaWidth, rightAreaExtraHeight), "");
		}
		GUI.color = new Color(1f, 1f, 1f, 0.7f * movementControlsOpacity);
		//GUI.Box(new Rect(rightAreaX, rightAreaY + rightAreaHeight - rightAreaExtraHeight, rightAreaWidth, rightAreaExtraHeight), "");
		GUI.color = new Color(1f, 1f, 1f, 1f * movementControlsOpacity);
		
		GUI.color = new Color(1f, 1f, 1f, 0.65f * movementControlsOpacity);
		//GUI.DrawTexture(new Rect(textureX, textureY + textureHeight * 0.03f, textureWidth, textureHeight * 0.94f), arrowTrayTopTexture);
		//GUI.DrawTexture(new Rect(textureX + textureWidth * 0.1f, textureY + textureHeight * 0.05f, textureWidth * 0.8f, textureHeight * 0.9f), arrowTrayTexture);
		GUI.color = new Color(1f, 1f, 1f, 1f * movementControlsOpacity);
		//GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), arrowLeftTexture);
		//GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), arrowRightTexture);
		//GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), arrowUpTexture);
		//GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), arrowDownTexture);
		if (USE_NEW_GUI == false) {
			if (movementControlsOpacity > 0f)
				inputControls.SetRectRightButton(new Rect(rightAreaX, rightAreaY, rightAreaWidth, rightAreaHeight + rightAreaExtraHeight));
		}
			
		if (USE_NEW_GUI == false) {
			GUI.color = new Color(1f, 1f, 1f, 0.7f * movementControlsOpacity);
			GUI.DrawTexture(new Rect(rightAreaX, rightAreaY - rightAreaExtraHeight*0.1f, rightAreaWidth, rightAreaHeight + rightAreaExtraHeight), controlRightIconsTexture);
			GUI.color = new Color(1f, 1f, 1f, 0.75f * movementControlsOpacity);
			GUI.DrawTexture(new Rect(rightAreaX, rightAreaY - rightAreaExtraHeight*0.1f, rightAreaWidth, rightAreaHeight + rightAreaExtraHeight), controlRightIconsLowerTexture);
		
			GUI.color = new Color(1f, 1f, 1f, 0.9f * movementControlsOpacity);
			iconTexture = chasingFlag ? pumaRunDarkTexture : pumaStealthDarkTexture;
			textureHeight = rightAreaExtraHeight * 1.5f;
			textureWidth = iconTexture.width * (textureHeight / iconTexture.height);
			GUI.DrawTexture(new Rect(rightAreaX + rightAreaWidth/2 - textureWidth*0.52f, rightAreaY + rightAreaHeight*0.0f, textureWidth, textureHeight), iconTexture);
			textureHeight = rightAreaExtraHeight * 0.9f;
			textureWidth = iconTexture.width * (textureHeight / iconTexture.height);
			GUI.DrawTexture(new Rect(rightAreaX + rightAreaWidth/2 - textureWidth*0.52f, rightAreaY + rightAreaHeight*0.75f, textureWidth, textureHeight), iconTexture);
		}
			
						
		//----------------------
		// POSITION INDICATORS
		//----------------------
					
		// outer edge display
		GUI.color = new Color(1f, 1f, 1f, 0.9f * positionIndicatorBackgroundOpacity);
		Color edgeColor = new Color(0f, 0f, 0f, 0.35f);	

		// initial onscreen rect
		indicatorMinX = Screen.width * 0.25f * (1f-positionIndicatorZoom);
		indicatorMinY = Screen.height * 0.05f * (1f-positionIndicatorZoom);
		indicatorMaxX = Screen.width - indicatorMinX;
		indicatorMaxY = Screen.height - (Screen.height * 0.45f * (1f-positionIndicatorZoom));

		float zoom = positionIndicatorZoom > 0.5f ? 1f : positionIndicatorZoom * 2f;
		float scaleFactor = 1f - (1f-zoom)*(1f-zoom);
		//float scaleFactor = zoom * zoom;
		//float scaleFactor = zoom;

		// background for indicator rect
		GUI.color = new Color(1f, 1f, 1f, 1f * positionIndicatorBackgroundOpacity * (1f - scaleFactor));
		if (USE_NEW_GUI == false)
			GUI.Box(new Rect(indicatorMinX, indicatorMinY, indicatorMaxX - indicatorMinX, indicatorMaxY - indicatorMinY), "");
		GUI.color = new Color(0.4f, 0.4f, 0.4f, 0.6f * positionIndicatorBackgroundOpacity * (1f - scaleFactor));
		//GUI.DrawTexture(new Rect(indicatorMinX, indicatorMinY, indicatorMaxX - indicatorMinX, indicatorMaxY - indicatorMinY), closeupBackgroundTexture);
			
		// puma head with flashing nose and ear
		
		Texture2D closeupTexture = null;
		
		switch (guiManager.selectedPuma) {
		case 0:
			closeupTexture = closeup1Texture;
			break;
		case 1:
			closeupTexture = closeup2Texture;
			break;
		case 2:
			closeupTexture = closeup3Texture;
			break;
		case 3:
			closeupTexture = closeup4Texture;
			break;
		case 4:
			closeupTexture = closeup5Texture;
			break;
		case 5:
			closeupTexture = closeup6Texture;
			break;
		}
				
		textureWidth = (indicatorMaxX - indicatorMinX) * 0.2f;
		textureHeight = closeupTexture.height * (textureWidth / closeupTexture.width);
		textureX = (indicatorMaxX + indicatorMinX) / 2 - textureWidth/2;
		textureY = (indicatorMaxY + indicatorMinY) / 2 - textureHeight/2;
		
		GUI.color = new Color(1f, 1f, 1f, 0.82f * positionIndicatorBackgroundOpacity * (1f - scaleFactor));
		if (USE_NEW_GUI == false)
			GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), closeupTexture);
		float flashingPeriod = 0.5f;
		float flashingOpacity = 0f;
		if (Time.time > flashStartTime + flashingPeriod) {
			flashStartTime = Time.time;
		}
		if (Time.time < flashStartTime + flashingPeriod * 0.3f) {
			// first half
			flashingOpacity = (Time.time - flashStartTime) / (flashingPeriod * 0.3f);
		}
		else {
			// second half
			flashingOpacity = 1f - ((Time.time - flashStartTime - flashingPeriod * 0.3f) / (flashingPeriod * 0.7f));			
		}	
		flashingOpacity = 0.3f + flashingOpacity * 0.5f;
		flashingOpacity = flashingOpacity * flashingOpacity;
		//flashingOpacity = 0f;
		GUI.color = new Color(1f, 1f, 1f, 0.5f * flashingOpacity * positionIndicatorBackgroundOpacity * (1f - scaleFactor));
		if (USE_NEW_GUI == false)
			GUI.DrawTexture(new Rect(indicatorMinX, indicatorMinY, indicatorMaxX - indicatorMinX, indicatorMaxY - indicatorMinY), closeupSensesTexture);
		GUI.color = new Color(1f, 1f, 1f, 1f * positionIndicatorBackgroundOpacity);

		// indicator borders
		if (USE_NEW_GUI == false) {
			guiUtils.DrawRect(new Rect(indicatorMinX, indicatorMinY, indicatorMaxX-indicatorMinX, borderThickness), edgeColor);
			guiUtils.DrawRect(new Rect(indicatorMinX, indicatorMaxY-borderThickness, indicatorMaxX-indicatorMinX, borderThickness), edgeColor);
			guiUtils.DrawRect(new Rect(indicatorMinX, indicatorMinY + borderThickness, borderThickness, (indicatorMaxY-indicatorMinY)-(borderThickness*2)), edgeColor);
			guiUtils.DrawRect(new Rect(indicatorMaxX-borderThickness, indicatorMinY + borderThickness, borderThickness, (indicatorMaxY-indicatorMinY)-(borderThickness*2)), edgeColor);
		}
		
		// deer head indicators
		if (USE_NEW_GUI == false) {
			if (levelManager.buck != null && levelManager.doe != null && levelManager.fawn != null ) {
				DrawIndicator(levelManager.mainHeading, levelManager.pumaObj, levelManager.buck.gameObj, levelManager.buck.type, borderThickness, positionIndicatorOpacity);
				DrawIndicator(levelManager.mainHeading, levelManager.pumaObj, levelManager.doe.gameObj, levelManager.doe.type, borderThickness, positionIndicatorOpacity);
				DrawIndicator(levelManager.mainHeading, levelManager.pumaObj, levelManager.fawn.gameObj, levelManager.fawn.type, borderThickness, positionIndicatorOpacity);
			}
		}
	
		//----------------------
		// STATUS DISPLAYS
		//----------------------
		
		float statusPanelWidth = Screen.height * 0.11f;
		float statusPanelHeight = leftAreaHeight * 0.88f;
		float statusPanelX = leftAreaX + leftAreaWidth * 0.45f;
		float statusPanelY = leftAreaY + leftAreaHeight - statusPanelHeight - leftAreaHeight * 0.03f;
		GUI.color = new Color(1f, 1f, 1f, 0.4f * statusDisplayOpacity);
		if (USE_NEW_GUI == false)
			GUI.Box(new Rect(statusPanelX, statusPanelY, statusPanelWidth, statusPanelHeight), "");
		GUI.color = new Color(1f, 1f, 1f, 1f * statusDisplayOpacity);
		if (USE_NEW_GUI == false)
			guiComponents.DrawStatusPanel(statusDisplayOpacity * 1f, statusPanelX, statusPanelY, statusPanelWidth, statusPanelHeight, false, true);
		GUI.color = new Color(1f, 1f, 1f, 1f * statusDisplayOpacity);

		
		//----------------------
		// CHEAT BUTTON
		//----------------------
		
		if (USE_NEW_GUI == false) {
			GUI.color = new Color(1f, 1f, 1f, 0f * statusDisplayOpacity);
			float killButtonX = Screen.width * 0.5f - Screen.height * 0.075f;
			float killButtonY = 0f;
			float killButtonWidth = Screen.height * 0.15f;
			float killButtonHeight = Screen.height * 0.15f;
			if (GUI.Button(new Rect(killButtonX,  killButtonY, killButtonWidth, killButtonHeight), "")) {
				levelManager.goStraightToFeeding = true;
			}
			GUI.color = new Color(1f, 1f, 1f, 1f * statusDisplayOpacity);
		}
	}

	
	//===================================
	//===================================
	//	  POSITION INDICATORS
	//===================================
	//===================================

	GameObject CreateDeerHead(GameObject parentObj, string type) 
	{ 
		GameObject deerHead;	
		GameObject deerHeadBackground;	
		GameObject deerHeadImage;
		
		// invisible panel to hold items
		deerHead = (GameObject)Instantiate(uiSubPanel);
		deerHead.GetComponent<RectTransform>().SetParent(parentObj.GetComponent<RectTransform>(), false);

		// background
		deerHeadBackground = guiUtils.CreateImage(deerHead, indicatorBkgnd, new Color(1f, 1f, 1f, 1f));
		deerHeadBackground.name = "DeerHeadBackground";
		
		// image
		Texture2D headTexture = null;
		switch (type) {
		case "Buck":
			headTexture = indicatorBuck;
			break;
		case "Doe":
			headTexture = indicatorDoe;
			break;
		case "Fawn":
			headTexture = indicatorFawn;
			break;
		}
		deerHeadImage = guiUtils.CreateImage(deerHead, headTexture, new Color(1f, 1f, 1f, 1f));
		deerHeadImage.name = "DeerHeadImage";
		
		return deerHead;
	}


	void PositionDeerHead(GameObject deerHead, float mainHeading, GameObject pumaObj, GameObject deerObj, string type, int borderThickness) 
	{ 
		float xPos = 0;
		float yPos = 0;
		float xOffset = 0;
		float yOffset = 0;
		float rangeX = (indicatorMaxX - indicatorMinX) - borderThickness;
		float rangeY = (indicatorMaxY - indicatorMinY) - borderThickness;
		float deerToPumaAngle = 0;
		float deerToPumaDistance = 0;

		//------------------------------------------
		// Determine onscreen position of indicator
		//------------------------------------------

		while (mainHeading < 0)
			mainHeading += 360;
		while (mainHeading > 360)
			mainHeading -= 360;
		
		// angle based on midpoint between camera and puma
		float refX = (pumaObj.transform.position.x + Camera.main.transform.position.x) / 2;
		float refY = (pumaObj.transform.position.z + Camera.main.transform.position.z) / 2;

		deerToPumaAngle = guiUtils.GetAngleFromOffset(refX, refY, deerObj.transform.position.x, deerObj.transform.position.z);
		deerToPumaDistance = Vector3.Distance(pumaObj.transform.position, deerObj.transform.position);

		xOffset = -(Mathf.Sin((mainHeading - deerToPumaAngle) * Mathf.PI / 180) * rangeY);
		yOffset = (Mathf.Cos((mainHeading - deerToPumaAngle) * Mathf.PI / 180) * rangeY);
		xOffset *= (rangeX/2) / rangeY; // adjust x offset to fit screen width			
			
		if (yOffset > 0) {
			// stretch out offsets to place deer head along screen edge
			float scaleVal;
			if (Mathf.Abs(xOffset / yOffset) > Mathf.Abs((rangeX/2) / rangeY)) {
				scaleVal = (rangeX/2) / Mathf.Abs(xOffset); // scale by X dimension
			}
			else {
				scaleVal = (rangeY) / Mathf.Abs(yOffset); // scale by Y dimension
			}
			xOffset *= scaleVal;
			yOffset *= scaleVal;
		}
		else {
			// clip y to keep deer head along lower edge
			yOffset = 0;
		}
					
		xPos = (Screen.width - borderThickness) / 2 + xOffset;
		yPos = indicatorMaxY - borderThickness - yOffset;

		//--------------------------------------------
		// Determine settings for distance indication
		//--------------------------------------------

		Color backdropColor;
		float fullRedDistance = 20f;
		float fullYellowDistance = 75f;
		float startYellowDistance = 175f;
		float transPercent;
				
		if (deerToPumaDistance < fullRedDistance) {
			backdropColor = new Color(1f, 0f, 0f, 1f);
		}
		else if (deerToPumaDistance < fullYellowDistance) {
			transPercent = (fullYellowDistance - deerToPumaDistance) / (fullYellowDistance - fullRedDistance);
			backdropColor = new Color(1f, 0.5f - (transPercent/2), 0f, 1f);
		}
		else if (deerToPumaDistance < startYellowDistance) {
			transPercent = (startYellowDistance - deerToPumaDistance) / (startYellowDistance - fullYellowDistance);
			backdropColor = new Color(1f, 0.5f, 0f, transPercent);
		}
		else {
			backdropColor = new Color(0f, 0f, 0f, 0f);
		}
			
		Color headColor;
		float fullHeadAlphaDistance = 150f;
		float startHeadAlphaDistance = 300f;

		if (deerToPumaDistance < fullHeadAlphaDistance) {
			headColor = new Color(1f, 1f, 1f, 1f);
		}
		else if (deerToPumaDistance < startHeadAlphaDistance) {
			headColor = new Color(1f, 1f, 1f, (0.5f + ((startHeadAlphaDistance - deerToPumaDistance) / (startHeadAlphaDistance - fullHeadAlphaDistance) * 0.5f)));
		}
		else {
			headColor = new Color(1f, 1f, 1f, 0.5f);
		}

		//-----------------------------------
		// Position and update the indicator
		//-----------------------------------

		GameObject deerHeadBackground;	
		GameObject deerHeadImage;
	
		// background
		deerHeadBackground = deerHead.GetComponent<RectTransform>().FindChild("DeerHeadBackground").gameObject;
		deerHeadBackground.GetComponent<RawImage>().color = backdropColor;
		guiUtils.SetItemOffsets(deerHeadBackground, xPos, yPos, borderThickness, borderThickness);
		
		// image
		deerHeadImage = deerHead.GetComponent<RectTransform>().FindChild("DeerHeadImage").gameObject;
		deerHeadImage.GetComponent<RawImage>().color = headColor;
		guiUtils.SetItemOffsets(deerHeadImage, xPos, yPos, borderThickness, borderThickness);		
	}


	void DrawIndicator(float mainHeading, GameObject pumaObj, GameObject deerObj, string type, int borderThickness, float guiOpacity)
    {
		float xPos = 0;
		float yPos = 0;
		float xOffset = 0;
		float yOffset = 0;
		float rangeX = (indicatorMaxX - indicatorMinX) - borderThickness;
		float rangeY = (indicatorMaxY - indicatorMinY) - borderThickness;
		float deerToPumaAngle = 0;
		float deerToPumaDistance = 0;

		//------------------------------------------
		// Determine onscreen position of indicator
		//------------------------------------------

		while (mainHeading < 0)
			mainHeading += 360;
		while (mainHeading > 360)
			mainHeading -= 360;
		
		// angle based on midpoint between camera and puma
		float refX = (pumaObj.transform.position.x + Camera.main.transform.position.x) / 2;
		float refY = (pumaObj.transform.position.z + Camera.main.transform.position.z) / 2;

		deerToPumaAngle = guiUtils.GetAngleFromOffset(refX, refY, deerObj.transform.position.x, deerObj.transform.position.z);
		deerToPumaDistance = Vector3.Distance(pumaObj.transform.position, deerObj.transform.position);

		xOffset = -(Mathf.Sin((mainHeading - deerToPumaAngle) * Mathf.PI / 180) * rangeY);
		yOffset = (Mathf.Cos((mainHeading - deerToPumaAngle) * Mathf.PI / 180) * rangeY);
		xOffset *= (rangeX/2) / rangeY; // adjust x offset to fit screen width			
			
		if (yOffset > 0) {
			// stretch out offsets to place deer head along screen edge
			float scaleVal;
			if (Mathf.Abs(xOffset / yOffset) > Mathf.Abs((rangeX/2) / rangeY)) {
				scaleVal = (rangeX/2) / Mathf.Abs(xOffset); // scale by X dimension
			}
			else {
				scaleVal = (rangeY) / Mathf.Abs(yOffset); // scale by Y dimension
			}
			xOffset *= scaleVal;
			yOffset *= scaleVal;
		}
		else {
			// clip y to keep deer head along lower edge
			yOffset = 0;
		}
					
		xPos = (Screen.width - borderThickness) / 2 + xOffset;
		yPos = indicatorMaxY - borderThickness - yOffset;

		//--------------------------------------------
		// Determine settings for distance indication
		//--------------------------------------------

		Color backdropColor;
		float fullRedDistance = 20f;
		float fullYellowDistance = 75f;
		float startYellowDistance = 175f;
		float transPercent;
				
		if (deerToPumaDistance < fullRedDistance) {
			backdropColor = new Color(1f, 0f, 0f, 1f * guiOpacity);
		}
		else if (deerToPumaDistance < fullYellowDistance) {
			transPercent = (fullYellowDistance - deerToPumaDistance) / (fullYellowDistance - fullRedDistance);
			backdropColor = new Color(1f, 0.5f - (transPercent/2), 0f, 1f * guiOpacity);
		}
		else if (deerToPumaDistance < startYellowDistance) {
			transPercent = (startYellowDistance - deerToPumaDistance) / (startYellowDistance - fullYellowDistance);
			backdropColor = new Color(1f, 0.5f, 0f, transPercent * guiOpacity);
		}
		else {
			backdropColor = new Color(0f, 0f, 0f, 0f * guiOpacity);
		}
			
		Color headColor;
		float fullHeadAlphaDistance = 150f;
		float startHeadAlphaDistance = 300f;

		if (deerToPumaDistance < fullHeadAlphaDistance) {
			headColor = new Color(1f, 1f, 1f, 1f * guiOpacity);
		}
		else if (deerToPumaDistance < startHeadAlphaDistance) {
			headColor = new Color(1f, 1f, 1f, (0.5f + ((startHeadAlphaDistance - deerToPumaDistance) / (startHeadAlphaDistance - fullHeadAlphaDistance) * 0.5f)) * guiOpacity);
		}
		else {
			headColor = new Color(1f, 1f, 1f, 0.5f * guiOpacity);
		}

		//--------------------------
		// Draw the indicator
		//--------------------------

		Texture2D headTexture = indicatorBkgnd;

		switch (type) {

		case "Buck":
			headTexture = indicatorBuck;
			break;

		case "Doe":
			headTexture = indicatorDoe;
			break;

		case "Fawn":
			headTexture = indicatorFawn;
			break;
		}

		GUI.color = backdropColor;
		GUI.DrawTexture(new Rect(xPos, yPos, borderThickness, borderThickness), indicatorBkgnd);
		GUI.color = headColor;
		GUI.DrawTexture(new Rect(xPos, yPos, borderThickness, borderThickness), headTexture);
	}

}