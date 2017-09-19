using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// OverlayPanel
/// Draw the GUI panel that provides the main user interface

public class OverlayPanel : MonoBehaviour
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

	private Rect overlayRect;
	private int currentScreen;
	private int soundEnable;
	private float soundVolume;
	private float pawRightFlag;
	private bool flashingInProgress = false;
	private float flashPumasStartTime = 0f;
	private float flashPumasInitialStartTime = 0f;
	
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
	private Texture2D greenOutlineRectVertTexture;
	private Texture2D radioButtonTexture;
	private Texture2D radioSelectTexture;
	private Texture2D arrowTrayTexture;
	private Texture2D arrowTrayTopTexture;
	private Texture2D swapButtonTexture;
	private Texture2D swapButtonHoverTexture;
	private Texture2D sliderBarTexture;
	private Texture2D sliderThumbTexture;
	private Texture2D buckHeadTexture;
	private Texture2D doeHeadTexture;
	private Texture2D fawnHeadTexture;
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
	private Texture2D closeup1OutlineTexture;
	private Texture2D closeup2OutlineTexture;
	private Texture2D closeup3OutlineTexture;
	private Texture2D closeup4OutlineTexture;
	private Texture2D closeup5OutlineTexture;
	private Texture2D closeup6OutlineTexture;
	private Texture2D closeupBackgroundTexture;
	private Texture2D pumaCrossbonesDarkRedTexture;
	private Texture2D pumaStealthTexture;
	private Texture2D pumaRunTexture;
	private Texture2D greenHeartTexture;
	private Texture2D pumaCrossbonesRedTexture;	
	private Texture2D redXTexture;	
	
	// external modules
	private GuiManager guiManager;
	private GuiComponents guiComponents;
	private GuiUtils guiUtils;
	private LevelManager levelManager;
	private TrafficManager trafficManager;
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
		trafficManager = GetComponent<TrafficManager>();
		scoringSystem = GetComponent<ScoringSystem>();
		
		// texture references from GuiManager
		logoTexture = guiManager.logoTexture;
		backgroundTexture = guiManager.backgroundTexture;
		pumaIconTexture = guiManager.pumaIconTexture;
		pumaIconShadowTexture = guiManager.pumaIconShadowTexture;
		greenCheckTexture = guiManager.greenCheckTexture;
		greenOutlineRectVertTexture = guiManager.greenOutlineRectVertTexture;
//		radioButtonTexture = guiManager.radioButtonTexture;
//		radioSelectTexture = guiManager.radioSelectTexture;
//		arrowTrayTexture = guiManager.arrowTrayTexture;
//		arrowTrayTopTexture = guiManager.arrowTrayTopTexture;
//		swapButtonTexture = guiManager.swapButtonTexture;
//		swapButtonHoverTexture = guiManager.swapButtonHoverTexture;
		sliderBarTexture = guiManager.sliderBarTexture;
		sliderThumbTexture = guiManager.sliderThumbTexture;
		buckHeadTexture = guiManager.buckHeadTexture;
		doeHeadTexture = guiManager.doeHeadTexture;
		fawnHeadTexture = guiManager.fawnHeadTexture;
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
		closeup1OutlineTexture = guiManager.closeup1OutlineTexture;
		closeup2OutlineTexture = guiManager.closeup2OutlineTexture;
		closeup3OutlineTexture = guiManager.closeup3OutlineTexture;
		closeup4OutlineTexture = guiManager.closeup4OutlineTexture;
		closeup5OutlineTexture = guiManager.closeup5OutlineTexture;
		closeup6OutlineTexture = guiManager.closeup6OutlineTexture;
		closeupBackgroundTexture = guiManager.closeupBackgroundTexture;
		pumaCrossbonesDarkRedTexture = guiManager.pumaCrossbonesDarkRedTexture;		
		pumaStealthTexture = guiManager.pumaStealthTexture;		
		pumaRunTexture = guiManager.pumaRunTexture;		
		greenHeartTexture = guiManager.greenHeartTexture;		
		pumaCrossbonesRedTexture = guiManager.pumaCrossbonesRedTexture;		
		redXTexture = guiManager.redXTexture;		
		
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
	
	public GameObject overlayMainPanel;

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
	private GameObject obj;
	
	
	void CreateGUIItems()
	{
		// set enable to 'off' before populating sub-items
		overlayMainPanel.SetActive(false);

		initComplete = true;
	}


	void PositionGUIItems()
	{
		if (initComplete == false)
			return;
	

	}
	
	public void UpdateGUIItems(float overlayPanelOpacity) 
	{ 
		if (initComplete == false)
			return;
	
		if (USE_NEW_GUI == true) {

			// check for screen size change
			if (lastSeenScreenWidth != Screen.width || lastSeenScreenHeight != Screen.height) {
				lastSeenScreenWidth = Screen.width;
				lastSeenScreenHeight = Screen.height;
				PositionGUIItems();
			}

			// top level enable and opacity
			overlayMainPanel.SetActive(overlayPanelOpacity > 0f ? true : false);
			overlayMainPanel.GetComponent<CanvasGroup>().alpha = overlayPanelOpacity;
		}
		else {
			// set enable to 'off'
			overlayMainPanel.SetActive(false);
		}
	}
	
	
	//===================================
	//===================================
	//	  DRAW THE OVERLAY PANEL
	//===================================
	//===================================
	
	public void Draw(float overlayPanelOpacity) 
	{ 

		//if (USE_NEW_GUI == true)
			//return; 
		
		
		//////////////////////////////////
		//////////////////////////////////
		
		// LEGACY DRAW CODE

		//////////////////////////////////
		//////////////////////////////////

		
		CalculateOverlayRect();

		// background panel
		GUI.color = new Color(0f, 0f, 0f, 1f * overlayPanelOpacity);
		GUI.Box(new Rect(overlayRect.x, overlayRect.y, overlayRect.width, overlayRect.height), "");
		//GUI.color = new Color(0f, 0f, 0f, 0.3f * overlayPanelOpacity);
		//GUI.Box(new Rect(overlayRect.x, overlayRect.y, overlayRect.width, overlayRect.height), "");
		GUI.color = new Color(1f, 1f, 1f, 1f * overlayPanelOpacity);
		
		//background image
		//GUI.color = new Color(1f, 1f, 1f, 0.75f * overlayPanelOpacity);
		//GUI.color = new Color(1f, 1f, 1f, 0f * overlayPanelOpacity);
		//GUI.DrawTexture(new Rect(overlayRect.x + 4, overlayRect.y + 4, overlayRect.width-8, overlayRect.height-8), backgroundTexture);
		//GUI.color = new Color(1f, 1f, 1f, 1f * overlayPanelOpacity);
			
		GUIStyle style = new GUIStyle();
		style.alignment = TextAnchor.MiddleCenter;
		
		// MAIN TTTLE
		
		float upperItemsYShift = overlayRect.height * 0.01f;

		if (true) {
			// graphical logo
			float logoX = overlayRect.x + overlayRect.width * 0.36f;
			float logoY = overlayRect.y - overlayRect.height * 0.023f + upperItemsYShift;
			float logoWidth = overlayRect.width * 0.28f;
			float logoHeight = logoTexture.height * (logoWidth / logoTexture.width);
			//GUI.color = new Color(1f, 1f, 1f, 0.75f * overlayPanelOpacity);
			GUI.color = new Color(0f, 0f, 0f, 1f * overlayPanelOpacity);
			GUI.Box(new Rect(logoX, logoY + logoHeight * 0.2f, logoWidth, logoHeight * 0.6f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * overlayPanelOpacity);			
			GUI.DrawTexture(new Rect(logoX, logoY, logoWidth, logoHeight), logoTexture);
			//GUI.color = new Color(1f, 1f, 1f, 1f * overlayPanelOpacity);
		}
		else {
			// text-based logo -- for reference only		
			GUI.color = new Color(1f, 1f, 1f, 0.93f * overlayPanelOpacity);
			float xPos = overlayRect.x + overlayRect.width * 0.349f;
			style.fontSize = (int)(overlayRect.width * 0.048f);
			style.fontStyle = FontStyle.BoldAndItalic;
			style.normal.textColor = new Color(0.2f, 0f, 0f, 1f);
			GUI.Button(new Rect(xPos - overlayRect.width * 0.003f, overlayRect.y + overlayRect.height * 0.008f + upperItemsYShift, overlayRect.width * 0.30f, overlayRect.height * 0.10f), "PumaWild", style);
			GUI.Button(new Rect(xPos + overlayRect.width * 0.003f, overlayRect.y + overlayRect.height * 0.008f + upperItemsYShift, overlayRect.width * 0.30f, overlayRect.height * 0.10f), "PumaWild", style);
			GUI.Button(new Rect(xPos - overlayRect.width * 0.003f, overlayRect.y + overlayRect.height * 0.016f + upperItemsYShift, overlayRect.width * 0.30f, overlayRect.height * 0.10f), "PumaWild", style);
			GUI.Button(new Rect(xPos + overlayRect.width * 0.003f, overlayRect.y + overlayRect.height * 0.016f + upperItemsYShift, overlayRect.width * 0.30f, overlayRect.height * 0.10f), "PumaWild", style);
			style.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f);
			GUI.Button(new Rect(xPos, overlayRect.y + overlayRect.height * 0.012f + upperItemsYShift, overlayRect.width * 0.30f, overlayRect.height * 0.10f), "PumaWild", style);
			style.fontSize = (int)(overlayRect.width * 0.0228f);
			style.normal.textColor = new Color(0.2f, 0f, 0f, 1f);
			xPos = overlayRect.x + overlayRect.width * 0.351f;
			GUI.Button(new Rect(xPos - overlayRect.width * 0.001f, overlayRect.y + overlayRect.height * 0.084f + upperItemsYShift, overlayRect.width * 0.30f, overlayRect.height * 0.05f), "survival is not a given", style);
			GUI.Button(new Rect(xPos + overlayRect.width * 0.001f, overlayRect.y + overlayRect.height * 0.084f + upperItemsYShift, overlayRect.width * 0.30f, overlayRect.height * 0.05f), "survival is not a given", style);
			GUI.Button(new Rect(xPos - overlayRect.width * 0.001f, overlayRect.y + overlayRect.height * 0.088f + upperItemsYShift, overlayRect.width * 0.30f, overlayRect.height * 0.05f), "survival is not a given", style);
			GUI.Button(new Rect(xPos + overlayRect.width * 0.001f, overlayRect.y + overlayRect.height * 0.088f + upperItemsYShift, overlayRect.width * 0.30f, overlayRect.height * 0.05f), "survival is not a given", style);
			style.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f);
			GUI.Button(new Rect(xPos, overlayRect.y + overlayRect.height * 0.086f + upperItemsYShift, overlayRect.width * 0.30f, overlayRect.height * 0.05f), "survival is not a given", style);
			GUI.color = new Color(1f, 1f, 1f, 1f * overlayPanelOpacity);
		}

		// LEVEL and STATUS DISPLAYS

		float levelPanelX = overlayRect.x + overlayRect.width * 0.04f;
		float levelPanelY = overlayRect.y + overlayRect.width * 0.016f + upperItemsYShift;
		float levelPanelWidth = overlayRect.width * 0.30f;
		float levelPanelHeight = overlayRect.width * 0.076f;
		//guiComponents.DrawLevelPanel(overlayPanelOpacity, levelPanelX, levelPanelY, levelPanelWidth, levelPanelHeight, false);
					
		float statusPanelX = overlayRect.x + overlayRect.width * 0.66f;
		float statusPanelY = overlayRect.y + overlayRect.width * 0.016f + upperItemsYShift;
		float statusPanelWidth = overlayRect.width * 0.30f;
		float statusPanelHeight = overlayRect.width * 0.076f;
		//guiComponents.DrawStatusPanel(overlayPanelOpacity, statusPanelX, statusPanelY, statusPanelWidth, statusPanelHeight, false);

		
		
		//=====================================
		// ADD BUTTONS
		//=====================================
		
		// background rectangle
		GUI.color = new Color(0f, 0f, 0f, 1f * overlayPanelOpacity);
		GUI.Box(new Rect(overlayRect.x + overlayRect.width * 0f, overlayRect.y + overlayRect.height * 0.926f, overlayRect.width * 1f, overlayRect.height * 0.074f), "");
		GUI.color = new Color(1f, 1f, 1f, 1f * overlayPanelOpacity);

/*		
		// 'help' button
		// background rectangle
		float helpButtonX = overlayRect.x + overlayRect.width * 0.61f;
		float helpButtonY = overlayRect.y + overlayRect.height * 0.937f;
		float helpButtonWidth = overlayRect.width * 0.06f;
		float helpButtonHeight = overlayRect.height * 0.05f;
		GUI.color = new Color(1f, 1f, 1f, 1f * overlayPanelOpacity);
		GUI.skin = guiManager.customGUISkin;
		guiManager.customGUISkin.button.fontSize = (int)(overlayRect.width * 0.026);
		guiManager.customGUISkin.button.fontStyle = FontStyle.Normal;
		GUI.backgroundColor = new Color(1f, 1f, 1f, 1f);
		GUI.color = new Color(1f, 1f, 1f, 0.3f * overlayPanelOpacity);
		if (GUI.Button(new Rect(helpButtonX, helpButtonY, helpButtonWidth, helpButtonHeight), "")) {
			guiManager.OpenInfoPanel(-1); // opens to current page
		}
		GUI.color = new Color(1f, 1f, 1f, 0.9f * overlayPanelOpacity);
		if (GUI.Button(new Rect(helpButtonX, helpButtonY, helpButtonWidth, helpButtonHeight), "?")) {
			guiManager.OpenInfoPanel(-1); // opens to current page
		}
		GUI.color = new Color(1f, 1f, 1f, 1f * overlayPanelOpacity);
*/
		
		// other buttons...
		
		float leftShift = overlayRect.width * 0.055f * -1f;

		buttonStyle.fontSize = buttonDownStyle.fontSize = (int)(overlayRect.width * 0.024);
		buttonDisabledStyle.fontSize = buttonDownStyle.fontSize = (int)(overlayRect.width * 0.024);
		float buttonWidth = overlayRect.width * 0.11f;
		float buttonGap = overlayRect.width * 0.011f;
		float buttonMargin = overlayRect.x + overlayRect.width * 0.14f - leftShift;
		float buttonY = overlayRect.y + overlayRect.height * 0.924f;
		float buttonheight = overlayRect.height * 0.075f;
		
		int buttonIndex = (currentScreen == 1) ? 2 : ((currentScreen == 2) ? 1 : currentScreen);
		float backgroundRectWidthAdjust = (currentScreen == 1) ? buttonWidth * 0.1f : ((currentScreen == 3) ? buttonWidth * -0.1f : 0f);
		guiUtils.DrawRect(new Rect(buttonMargin + buttonWidth*buttonIndex + buttonGap*buttonIndex + buttonWidth*0.05f - backgroundRectWidthAdjust*0.5f, buttonY + buttonheight * 0.15f, buttonWidth - buttonWidth*0.1f + backgroundRectWidthAdjust, buttonheight - buttonheight * 0.29f), new Color(0f, 0f, 0f, 0.5f));	


		buttonDownStyle.normal.textColor = new Color(0.99f, 0.7f, 0.2f, 1f);
		buttonStyle.normal.textColor = new Color(0.99f, 0.88f, 0.6f, 1f);

		// select, stats and options tabs
		if (GUI.Button(new Rect(buttonMargin, buttonY, buttonWidth, buttonheight), "Select", (currentScreen == 0) ? buttonDownStyle : buttonStyle))
			currentScreen = 0;
		if (guiManager.selectedPuma != -1) {
			if (GUI.Button(new Rect(buttonMargin + buttonWidth + buttonGap, buttonY, buttonWidth, buttonheight), "Stats", (currentScreen == 2) ? buttonDownStyle : buttonStyle))
				currentScreen = 2;
			if (GUI.Button(new Rect(buttonMargin + buttonWidth*2f + buttonGap*2f, buttonY, buttonWidth, buttonheight), "Options", (currentScreen == 1) ? buttonDownStyle : buttonStyle))
				currentScreen = 1;
		}
		else {
			GUI.Button(new Rect(buttonMargin + buttonWidth + buttonGap, buttonY, buttonWidth, buttonheight), "Stats", buttonDisabledStyle);
			GUI.Button(new Rect(buttonMargin + buttonWidth*2f + buttonGap*2f, buttonY, buttonWidth, buttonheight), "Options", buttonDisabledStyle);
		}


		// 'more'
		buttonStyle.normal.textColor = new Color(0.99f, 0.88f, 0.6f, 1f);

		bigButtonStyle.fontSize = (int)(overlayRect.width * 0.032);;
		bigButtonDisabledStyle.fontSize = (int)(overlayRect.width * 0.022);;
		GUI.skin = guiManager.customGUISkin;
		guiManager.customGUISkin.button.fontSize = (int)(overlayRect.width * 0.022);
		guiManager.customGUISkin.button.fontStyle = FontStyle.Normal;
		GUI.backgroundColor = new Color(1f, 1f, 1f, 1f);
		float closeButtonX = overlayRect.x + overlayRect.width * 0.537f - leftShift;
		float closeButtonY = overlayRect.y + overlayRect.height * 0.937f;
		float closeButtonWidth = overlayRect.width * 0.085f;
		float closeButtonHeight = overlayRect.height * 0.05f;
		if (guiManager.selectedPuma != -1) {
			if (GUI.Button(new Rect(closeButtonX, closeButtonY, closeButtonWidth, closeButtonHeight), "", buttonStyle)) {
				guiManager.OpenInfoPanel(-1); // opens to current page
			}
			if (GUI.Button(new Rect(closeButtonX, closeButtonY, closeButtonWidth, closeButtonHeight), "More >", buttonStyle)) {
				guiManager.OpenInfoPanel(-1); // opens to current page
			}
		}
		else {
			GUI.Button(new Rect(closeButtonX, closeButtonY, closeButtonWidth, closeButtonHeight), "More >", bigButtonDisabledStyle);
		}



		buttonStyle.normal.textColor = new Color(0.99f, 0.7f, 0.2f, 1f);
		buttonDownStyle.normal.textColor = new Color(0.99f, 0.88f, 0.6f, 1f);
		buttonStyle.fontSize = buttonDownStyle.fontSize = (int)(overlayRect.width * 0.023);
		buttonDisabledStyle.fontSize = buttonDownStyle.fontSize = (int)(overlayRect.width * 0.023);
		buttonWidth = overlayRect.width * 0.12f;


		// 'play'
		// background rectangle
		float startButtonX = overlayRect.x + overlayRect.width * 0.811f;
		float startButtonY = overlayRect.y + overlayRect.height * 0.932f;
		float startButtonWidth = overlayRect.width * 0.15f;
		float startButtonHeight = overlayRect.height * 0.06f;
		GUI.color = (guiManager.selectedPuma != -1) ? new Color(1f, 1f, 1f, 1f * overlayPanelOpacity) : new Color(1f, 1f, 1f, 0.5f * overlayPanelOpacity);
		bigButtonStyle.fontSize = (int)(overlayRect.width * 0.032);;
		bigButtonDisabledStyle.fontSize = (int)(overlayRect.width * 0.03);;
		GUI.skin = guiManager.customGUISkin;
		guiManager.customGUISkin.button.fontSize = (int)(overlayRect.width * 0.025);
		guiManager.customGUISkin.button.fontStyle = FontStyle.Bold;
		GUI.backgroundColor = new Color(1f, 1f, 1f, 1f);
		if (guiManager.selectedPuma != -1) {
			if (GUI.Button(new Rect(startButtonX, startButtonY, startButtonWidth, startButtonHeight), "")) {
				guiManager.SetGuiState("guiStateLeavingOverlay");
				levelManager.SetGameState("gameStateLeavingGui");
			}
			if (GUI.Button(new Rect(startButtonX, startButtonY, startButtonWidth, startButtonHeight), "PLAY")) {
			//if (GUI.Button(new Rect(startButtonX, startButtonY, startButtonWidth, startButtonHeight), "Go", (guiManager.selectedPuma != -1) ? bigButtonStyle : bigButtonDisabledStyle) && (guiManager.selectedPuma != -1)) {
				guiManager.SetGuiState("guiStateLeavingOverlay");
				levelManager.SetGameState("gameStateLeavingGui");
			}
		}
		else {
			GUI.Button(new Rect(startButtonX, startButtonY, startButtonWidth, startButtonHeight), "PLAY", bigButtonDisabledStyle);
		}
		
		GUI.color = new Color(1f, 1f, 1f, 1f * overlayPanelOpacity);
		
		
		// add selected screen
		switch (currentScreen) {
		case 0:
			DrawSelectScreen(overlayPanelOpacity);
			break;		
		case 1:
			DrawOptionsScreen(overlayPanelOpacity);
			break;
		case 2:
			DrawStatsScreen(overlayPanelOpacity);
			break;
		case 3:
			DrawQuitScreen(overlayPanelOpacity);
			break;
		}
		
	}


	

	void DrawSelectScreen(float selectScreenOpacity) 
	{ 
		float textureX;
		float textureY;
		float textureWidth;
		float textureHeight;
		float oldTextureX;
		
		GUIStyle style = new GUIStyle();
		style.alignment = TextAnchor.MiddleCenter;

		// background rectangle

		float backRectX = overlayRect.x + overlayRect.width * 0.06f;
		float backRectY = overlayRect.y + overlayRect.height * 0.205f;
		float backRectWidth = overlayRect.width * 0.88f;
		float backRectHeight = overlayRect.height * 0.578f;
		float fontScale = 0.8f;

		GUI.color = new Color(0f, 0f, 0f, 1f * selectScreenOpacity);
		GUI.Box(new Rect(backRectX - overlayRect.width * 0.02f, backRectY - overlayRect.height * 0.025f, backRectWidth + overlayRect.width * 0.04f, backRectHeight + overlayRect.height * 0.05f), "");
		GUI.color = new Color(0f, 0f, 0f, 0.5f * selectScreenOpacity);
		GUI.Box(new Rect(backRectX, backRectY, backRectWidth, backRectHeight), "");
		GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);


		// select screen prompt

		if (guiManager.selectedPuma == -1) {
		
			float yOffset = overlayRect.height * 0.015f;
		
			//guiUtils.DrawRect(new Rect(overlayRect.width * 0.32f, overlayRect.height * 0.32f, overlayRect.width * 0.36f, overlayRect.height * 0.12f), new Color(1f, 1f, 1f, 0.4f));	
			//guiUtils.DrawRect(new Rect(overlayRect.width * 0.30f, overlayRect.height * 0.26f, overlayRect.width * 0.40f, overlayRect.height * 0.16f), new Color(1f, 1f, 1f, 0.6f));	
			//GUI.color = new Color(0f, 0f, 0f, 1f * selectScreenOpacity);
			//GUI.Box(new Rect(overlayRect.x + overlayRect.width * 0.37f, yOffset + overlayRect.y + overlayRect.height * 0.195f, overlayRect.width * 0.26f, overlayRect.height * 0.064f), "");
			//GUI.color = new Color(0f, 0f, 0f, 0.5f * selectScreenOpacity);
			//GUI.Box(new Rect(overlayRect.width * 0.32f, overlayRect.height * 0.31f, overlayRect.width * 0.36f, overlayRect.height * 0.16f), "");

			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
			style.fontSize = (int)(overlayRect.width * 0.025f);
			style.fontStyle = FontStyle.Italic;
			//style.normal.textColor = new Color(0.392f, 0.0588f, 0.0588f, 1f);
			//style.normal.textColor = new Color(1f, 1f, 1f, 1f);
			style.normal.textColor = new Color(0.816f, 0.69f, 0.18f, 1f);
			GUI.Button(new Rect(overlayRect.x + overlayRect.width * 0.3f, yOffset + overlayRect.y + overlayRect.height * 0.184f, overlayRect.width * 0.4f, overlayRect.height * 0.1f), "Select  Puma...", style);
		}

		style.normal.textColor = Color.white;
		style.fontStyle = FontStyle.BoldAndItalic;
		
		
		

		// add population bar
		
		float yOffsetForAddingPopulationBar = overlayRect.height * -0.012f;
		float actualselectScreenOpacity = selectScreenOpacity;
		selectScreenOpacity = selectScreenOpacity * 0.9f;
		GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);		
		
		float healthBarX = overlayRect.x + overlayRect.width * 0.04f;
		float healthBarY = overlayRect.y + overlayRect.height * 0.844f + yOffsetForAddingPopulationBar;
		float healthBarWidth = overlayRect.width * 0.92f;
		float healthBarHeight = overlayRect.height * 0.048f;	
		float healthBarLabelWidth = healthBarWidth * 0.13f;

		GUI.color = new Color(0f, 0f, 0f, 0.8f * selectScreenOpacity);
		GUI.Box(new Rect(healthBarX, healthBarY, healthBarLabelWidth * 2, healthBarHeight), "");
		GUI.Box(new Rect(healthBarX, healthBarY, healthBarLabelWidth * 2, healthBarHeight), "");
		//GUI.Box(new Rect(healthBarX + healthBarLabelWidth, healthBarY, healthBarLabelWidth, healthBarHeight), "");
		//GUI.Box(new Rect(healthBarX + healthBarLabelWidth, healthBarY, healthBarLabelWidth, healthBarHeight), "");
		GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);

		guiComponents.DrawPopulationHealthBar(selectScreenOpacity, healthBarX + healthBarLabelWidth * 2, healthBarY, healthBarWidth - healthBarLabelWidth * 2, healthBarHeight, false, false);

		selectScreenOpacity = actualselectScreenOpacity;
		GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);		
		
		//
		
		yOffsetForAddingPopulationBar += overlayRect.height * -0.005f;
		Texture headshotTexture;
		float headshotX;
		float headshotY;
		float headshotWidth;
		float headshotHeight;
		
		Color unselectedTextColor = new Color(0.85f, 0.74f, 0.5f, 0.85f);
		Color unselectedNameColor = new Color(0.83f * 0.93f, 0.57f * 0.93f, 0f, 0.88f);

		
		// used in DrawSelectScreen() and DrawSelectScreenDetails()
		float headUpShift = overlayRect.height * -0.03f;
		float textUpShift = overlayRect.height * -0.283f;
		float barsDownShift = overlayRect.height * 0.06f;
		float iconDownShift = overlayRect.height * 0.06f;
		float healthDownShift = overlayRect.height * 0.315f;
		float headshotBackgroundHeightAddition = overlayRect.height * 0.06f;

		headUpShift = 0f;
		healthDownShift = 0f;

		textUpShift = 0f;
		barsDownShift = 0f;
		iconDownShift = 0f;
		headshotBackgroundHeightAddition = 0f;


		//Color pumaFullHealthColor = new Color(0.84f, 0.99f, 0.0f, 0.72f * selectScreenOpacity);
		//Color pumaDeadColor = new Color(0.76f, 0.0f, 0f, 0.47f * selectScreenOpacity);
		//Color pumaDeadColor = new Color(0.1f, 0.1f, 0.1f, 0.6f * selectScreenOpacity);



		//Color fullHealthPumaHeadshotColor = new Color(0.2f, 1f, 0.05f, 1f * selectScreenOpacity);
		//Color fullHealthPumaIconColor = new Color(0.1f, 1f, 0f, 0.65f * selectScreenOpacity);
		Color fullHealthPumaHeadshotColor = new Color(0.88f, 0.88f, 0.88f, 0.98f * selectScreenOpacity);
		Color fullHealthPumaIconColor = new Color(0.7f, 0.7f, 0.7f, 0.8f * selectScreenOpacity);
		//Color fullHealthPumaTextColor = new Color(0.1f, 0.5f, 0f, 0.8f * selectScreenOpacity);
		//Color fullHealthPumaTextColor = new Color(0.5f, 0.4f, 0.05f, 0.8f * selectScreenOpacity);
		Color fullHealthPumaTextColor = unselectedTextColor;
		
		
		Color fullHealthPumaAnnounceColor = new Color(0f, 0.70f, 0f, 0.8f * selectScreenOpacity);

		//Color deadPumaHeadshotColor = new Color(0.8f, 0.1f, 0f, 0.55f * selectScreenOpacity);
		Color deadPumaHeadshotColor = new Color(0.09f, 0.09f, 0.09f, 1f * selectScreenOpacity);
		//Color deadPumaHeadshotColor = new Color(0.4f, 0.4f, 0.4f, 0.7f * selectScreenOpacity);
		//Color deadPumaIconColor = new Color(0.8f, 0f, 0f, 0.3f * selectScreenOpacity);
		Color deadPumaIconColor = new Color(0.08f, 0.08f, 0.08f, 0.99f * selectScreenOpacity);
		//Color deadPumaTextColor = new Color(0.05f, 0.05f, 0.02f, 1f * selectScreenOpacity);
		Color deadPumaTextColor = new Color(0.32f * 1.3f, 0.32f * 1.3f, 0.22f * 1.3f, 0.8f * selectScreenOpacity);
		Color deadPumaAnnounceColor = new Color(0.52f, 0.05f, 0f, 1f * selectScreenOpacity);

		
		Color selectablePumaOutlineColor = new Color(0.05f, 0.05f, 0.05f, 0f * selectScreenOpacity);
		Color selectedPumaOutlineColor = new Color(0.14f, 0.14f, 0.14f, 0f * selectScreenOpacity);
		
		
		
		float endingLabelDownshift = overlayRect.height * 0.036f;


		// random flashing when no puma selected
		bool flashPumas = false;
		float flashPumasOpacity = 0f;
		if (guiManager.selectedPuma == -1 && selectScreenOpacity > 0.9f) {
			flashPumas = true;
			float flashingPeriod = 0.9f;
			if (Time.time > flashPumasStartTime + flashingPeriod) {
				flashPumasStartTime = Time.time;
			}
			if (Time.time < flashPumasStartTime + flashingPeriod * 0.4f) {
				// first half
				flashPumasOpacity = (Time.time - flashPumasStartTime) / (flashingPeriod * 0.4f);
			}
			else {
				// second half
				flashPumasOpacity = 1f - ((Time.time - flashPumasStartTime - flashingPeriod * 0.4f) / (flashingPeriod * 0.6f));			
			}	
			flashPumasOpacity = 0.4f + flashPumasOpacity * 0.25f;
			flashPumasOpacity = flashPumasOpacity * flashPumasOpacity;
			if (flashingInProgress == false)
				flashPumasInitialStartTime = Time.time;
			flashingInProgress = true;
			float rampInTime = 0.5f;
			if (Time.time < flashPumasInitialStartTime + rampInTime)
				flashPumasOpacity = flashPumasOpacity * ((Time.time - flashPumasInitialStartTime) / rampInTime);
		}
		else {
			flashingInProgress = false;
		}
		
		Color statsColor = new Color(0f, 0f, 0f, 1f);
		
		// young male
		textureX = overlayRect.x + overlayRect.width * ((guiManager.selectedPuma == -1) ? 0.102f : 0.089f);
		textureX += overlayRect.width * ((guiManager.selectedPuma == 0) ? -0.002f : 0f);
		textureY = overlayRect.y + overlayRect.height * ((guiManager.selectedPuma == 0) ? 0.572f : 0.585f) + yOffsetForAddingPopulationBar;
		textureWidth = overlayRect.width * ((guiManager.selectedPuma == 0) ? 0.16f : 0.115f);
		textureHeight = pumaIconTexture.height * (textureWidth / pumaIconTexture.width);
		if (flashPumas == true && PumaIsSelectable(0)) {
			GUI.color = new Color(1f, 1f, 1f, flashPumasOpacity * selectScreenOpacity);		
			GUI.DrawTexture(new Rect(textureX - textureWidth * 0.05f, textureY - textureHeight * 2.24f, textureWidth * 1.1f, textureHeight * 3.45f), greenOutlineRectVertTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);		
		}
		else if (PumaIsSelectable(0) && guiManager.selectedPuma != 0) {
			GUI.color = selectablePumaOutlineColor;	
			GUI.DrawTexture(new Rect(textureX - textureWidth * 0.05f, textureY - textureHeight * 2.24f, textureWidth * 1.1f, textureHeight * 3.45f), greenOutlineRectVertTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);		
		}
		else if (guiManager.selectedPuma == 0) {
			GUI.color = selectedPumaOutlineColor;	
			GUI.DrawTexture(new Rect(textureX - textureWidth * 0.07f, textureY - textureHeight * 2.10f, textureWidth * 1.14f, textureHeight * 3.06f), greenOutlineRectVertTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);		
		}
		if (guiManager.selectedPuma != 0) {
			// background panel and puma head
			headshotY = overlayRect.y + overlayRect.height * 0.355f + yOffsetForAddingPopulationBar + headUpShift;
			GUI.color = new Color(0f, 0f, 0f, 0.8f * selectScreenOpacity);
			GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY, textureWidth - overlayRect.width * .0f, overlayRect.height * 0.1f + headshotBackgroundHeightAddition), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
			// puma head
			headshotTexture = closeup1Texture;
			headshotHeight = overlayRect.height * 0.085f;
			headshotWidth = headshotTexture.width * (headshotHeight / headshotTexture.height);
			headshotX = textureX + (textureWidth - headshotWidth) * 0.5f;
			if (scoringSystem.GetPumaHealth(0) <= 0f)
				GUI.color = deadPumaHeadshotColor;
			GUI.DrawTexture(new Rect(headshotX, headshotY, headshotWidth, headshotHeight), headshotTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
			// health bar
			if (scoringSystem.GetPumaHealth(0) > 0f) 
				guiComponents.DrawPumaHealthBar(0, selectScreenOpacity, textureX, headshotY - headshotHeight * 0.36f + healthDownShift, textureWidth, headshotHeight * 0.26f, true);
			// background panel for puma middle
			headshotY = overlayRect.y + overlayRect.height * 0.47f + yOffsetForAddingPopulationBar + barsDownShift;
			GUI.color = new Color(0f, 0f, 0f, 0.8f * selectScreenOpacity);
			GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY, textureWidth - overlayRect.width * .0f, overlayRect.height * 0.1f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
			// display bars in puma middle
			DrawDisplayBars(0, selectScreenOpacity, headshotX, headshotY, headshotWidth, headshotHeight);
			// final ending label at very top
			if (scoringSystem.GetPumaHealth(0) <= 0f) {
				// puma is dead
				headshotY = overlayRect.y + overlayRect.height * 0.2885f + yOffsetForAddingPopulationBar + headUpShift;
				GUI.color = new Color(0f, 0f, 0f, 0.8f * selectScreenOpacity);
				GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY + endingLabelDownshift, textureWidth - overlayRect.width * .0f, headshotHeight * 0.3f), "");
				GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
				style.normal.textColor = deadPumaAnnounceColor;
				style.fontSize = (int)(overlayRect.width * 0.012f);
				GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * 0.26f + yOffsetForAddingPopulationBar + textUpShift + endingLabelDownshift, textureWidth, overlayRect.height * 0.08f), scoringSystem.WasKilledByCar(0) ? "VEHICLE" : "STARVED", style);
			}
		}
		// background panel for text or puma icon
		if (guiManager.selectedPuma == 0) {
			GUI.color = new Color(0f, 0f, 0f, 0.4f * selectScreenOpacity);
			//GUI.Box(new Rect(textureX + overlayRect.width * .015f, overlayRect.y + overlayRect.height * 0.73f + yOffsetForAddingPopulationBar, textureWidth - overlayRect.width * .03f, overlayRect.height * 0.065f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
		}
		else {
			// not selected
			GUI.color = new Color(0f, 0f, 0f, 0.8f * selectScreenOpacity);
			GUI.Box(new Rect(textureX + overlayRect.width * .0f, overlayRect.y + overlayRect.height * 0.585f + yOffsetForAddingPopulationBar + iconDownShift, textureWidth - overlayRect.width * .0f, overlayRect.height * 0.14f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
		}
		// puma icon
		GUI.color = new Color(1f, 1f, 1f, 0f * selectScreenOpacity);
		if (GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * 0.315f, textureWidth, overlayRect.height * 0.47f), "") && PumaIsSelectable(0)) {
			guiManager.selectedPuma = 0;
			levelManager.SetSelectedPuma(0);
		}
		GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
		if (guiManager.selectedPuma == 0) {
			float selectedIconX = textureX + overlayRect.width * 0.024f;
			float selectedIconY = textureY + overlayRect.height * 0.018f;
			float selectedIconWidth = textureWidth * 0.74f;
			float selectedIconHeight = textureHeight * 0.73f;
			GUI.DrawTexture(new Rect(selectedIconX - (selectedIconWidth * 0.05f), selectedIconY - (selectedIconHeight * 0.055f) + iconDownShift, selectedIconWidth * 1.116f, selectedIconHeight * 1.116f), pumaIconShadowTexture);
			GUI.DrawTexture(new Rect(selectedIconX, selectedIconY + iconDownShift, selectedIconWidth, selectedIconHeight), pumaIconTexture);
		}
		else {
			if (scoringSystem.GetPumaHealth(0) <= 0f)
				GUI.color = deadPumaIconColor;
			else
				GUI.color = new Color(1f, 1f, 1f, 0.7f * selectScreenOpacity);
			GUI.DrawTexture(new Rect(textureX + textureWidth * 0.05f, textureY + textureHeight * 0.12f + iconDownShift, textureWidth * 0.9f, textureHeight * 0.9f), pumaIconTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
		}
		if (guiManager.selectedPuma == 0)
			DrawSelectScreenDetailsPanel(selectScreenOpacity, style, textureX, textureWidth, headshot1Texture);
		// text
		if (guiManager.selectedPuma == 0) {
			style.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f);
			statsColor = new Color(0.88f, 0.55f, 0f, 1f);
		}
		else if (scoringSystem.GetPumaHealth(0) <= 0f) {
			style.normal.textColor = deadPumaTextColor;
			statsColor = deadPumaTextColor;
		}
		else if (scoringSystem.GetPumaHealth(0) >= 1f) {
			style.normal.textColor = fullHealthPumaTextColor;
			statsColor = fullHealthPumaTextColor;
		}
		else {
			style.normal.textColor = unselectedNameColor;
			statsColor = unselectedTextColor;
		}
		style.fontSize = (int)(overlayRect.width * ((guiManager.selectedPuma == 0) ? 0.021f : 0.019f));
		GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * ((guiManager.selectedPuma == 0) ? 0.71f : 0.71f) + yOffsetForAddingPopulationBar + textUpShift, textureWidth, overlayRect.height * 0.08f), "Eric", style);
		style.fontSize = (int)(overlayRect.width * ((guiManager.selectedPuma == 0) ? 0.015f : 0.014f));
		style.normal.textColor = statsColor;
		GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * ((guiManager.selectedPuma == 0) ? 0.735f : 0.735f) + yOffsetForAddingPopulationBar + textUpShift, textureWidth, overlayRect.height * 0.08f), "2 years - male", style);
		style.normal.textColor = Color.white;
		


		// young female
		textureX += overlayRect.width * ((guiManager.selectedPuma == 0) ? 0.18f : 0.135f);
		textureX += overlayRect.width * ((guiManager.selectedPuma == 1) ? -0.002f : 0f);
		textureY = overlayRect.y + overlayRect.height * ((guiManager.selectedPuma == 1) ? 0.572f : 0.585f) + yOffsetForAddingPopulationBar;
		textureWidth = overlayRect.width * ((guiManager.selectedPuma == 1) ? 0.16f : 0.115f);
		textureHeight = pumaIconTexture.height * (textureWidth / pumaIconTexture.width);
		if (flashPumas == true && PumaIsSelectable(1)) {
			GUI.color = new Color(1f, 1f, 1f, flashPumasOpacity * selectScreenOpacity);		
			GUI.DrawTexture(new Rect(textureX - textureWidth * 0.05f, textureY - textureHeight * 2.24f, textureWidth * 1.1f, textureHeight * 3.45f), greenOutlineRectVertTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);		
		}		
		else if (PumaIsSelectable(1) && guiManager.selectedPuma != 1) {
			GUI.color = selectablePumaOutlineColor;	
			GUI.DrawTexture(new Rect(textureX - textureWidth * 0.05f, textureY - textureHeight * 2.24f, textureWidth * 1.1f, textureHeight * 3.45f), greenOutlineRectVertTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);		
		}
		else if (guiManager.selectedPuma == 1) {
			GUI.color = selectedPumaOutlineColor;	
			GUI.DrawTexture(new Rect(textureX - textureWidth * 0.07f, textureY - textureHeight * 2.10f, textureWidth * 1.14f, textureHeight * 3.06f), greenOutlineRectVertTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);		
		}
		if (guiManager.selectedPuma != 1) {
			// background panel for puma head
			headshotY = overlayRect.y + overlayRect.height * 0.355f + yOffsetForAddingPopulationBar + headUpShift;
			GUI.color = new Color(0f, 0f, 0f, 0.8f * selectScreenOpacity);
			GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY, textureWidth - overlayRect.width * .0f, overlayRect.height * 0.1f + headshotBackgroundHeightAddition), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
			// puma head
			headshotTexture = closeup2Texture;
			headshotHeight = overlayRect.height * 0.085f;
			headshotWidth = headshotTexture.width * (headshotHeight / headshotTexture.height);
			headshotX = textureX + (textureWidth - headshotWidth) * 0.5f;
			if (scoringSystem.GetPumaHealth(1) <= 0f)
				GUI.color = deadPumaHeadshotColor;
			GUI.DrawTexture(new Rect(headshotX, headshotY, headshotWidth, headshotHeight), headshotTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
			// health bar
			if (scoringSystem.GetPumaHealth(1) > 0f)
				guiComponents.DrawPumaHealthBar(1, selectScreenOpacity, textureX, headshotY - headshotHeight * 0.36f + healthDownShift, textureWidth, headshotHeight * 0.26f, true);
			// background panel for puma middle
			headshotY = overlayRect.y + overlayRect.height * 0.47f + yOffsetForAddingPopulationBar + barsDownShift;
			GUI.color = new Color(0f, 0f, 0f, 0.8f * selectScreenOpacity);
			GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY, textureWidth - overlayRect.width * .0f, overlayRect.height * 0.1f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
			// display bars in puma middle
			DrawDisplayBars(1, selectScreenOpacity, headshotX, headshotY, headshotWidth, headshotHeight);
			// final ending label at very top
			if (scoringSystem.GetPumaHealth(1) <= 0f) {
				// puma is dead
				headshotY = overlayRect.y + overlayRect.height * 0.2885f + yOffsetForAddingPopulationBar + headUpShift;
				GUI.color = new Color(0f, 0f, 0f, 0.8f * selectScreenOpacity);
				GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY + endingLabelDownshift, textureWidth - overlayRect.width * .0f, headshotHeight * 0.3f), "");
				GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
				style.normal.textColor = deadPumaAnnounceColor;
				style.fontSize = (int)(overlayRect.width * 0.012f);
				GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * 0.26f + yOffsetForAddingPopulationBar + textUpShift + endingLabelDownshift, textureWidth, overlayRect.height * 0.08f), scoringSystem.WasKilledByCar(1) ? "VEHICLE" : "STARVED", style);
			}
		}
		// background panel for text or puma icon
		if (guiManager.selectedPuma == 1) {
			GUI.color = new Color(0f, 0f, 0f, 0.4f * selectScreenOpacity);
			//GUI.Box(new Rect(textureX + overlayRect.width * .015f, overlayRect.y + overlayRect.height * 0.73f + yOffsetForAddingPopulationBar, textureWidth - overlayRect.width * .03f, overlayRect.height * 0.065f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
		}
		else {
			// not selected
			GUI.color = new Color(0f, 0f, 0f, 0.8f * selectScreenOpacity);
			GUI.Box(new Rect(textureX + overlayRect.width * .0f, overlayRect.y + overlayRect.height * 0.585f + yOffsetForAddingPopulationBar + iconDownShift, textureWidth - overlayRect.width * .0f, overlayRect.height * 0.14f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
		}
		// puma icon
		GUI.color = new Color(1f, 1f, 1f, 0f * selectScreenOpacity);
		if (GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * 0.315f, textureWidth, overlayRect.height * 0.47f), "") && PumaIsSelectable(1)) {
			guiManager.selectedPuma = 1;
			levelManager.SetSelectedPuma(1);
		}
		GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
		if (guiManager.selectedPuma == 1) {
			float selectedIconX = textureX + overlayRect.width * 0.024f;
			float selectedIconY = textureY + overlayRect.height * 0.018f;
			float selectedIconWidth = textureWidth * 0.74f;
			float selectedIconHeight = textureHeight * 0.73f;
			GUI.DrawTexture(new Rect(selectedIconX - (selectedIconWidth * 0.05f), selectedIconY - (selectedIconHeight * 0.055f) + iconDownShift, selectedIconWidth * 1.116f, selectedIconHeight * 1.116f), pumaIconShadowTexture);
			GUI.DrawTexture(new Rect(selectedIconX, selectedIconY + iconDownShift, selectedIconWidth, selectedIconHeight), pumaIconTexture);
		}
		else {
			if (scoringSystem.GetPumaHealth(1) <= 0f)
				GUI.color = deadPumaIconColor;
			else
				GUI.color = new Color(1f, 1f, 1f, 0.7f * selectScreenOpacity);
			GUI.DrawTexture(new Rect(textureX + textureWidth * 0.05f, textureY + textureHeight * 0.12f + iconDownShift, textureWidth * 0.9f, textureHeight * 0.9f), pumaIconTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
		}
		if (guiManager.selectedPuma == 1)
			DrawSelectScreenDetailsPanel(selectScreenOpacity, style, textureX, textureWidth, headshot2Texture);
		// text
		if (guiManager.selectedPuma == 1) {
			style.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f);
			statsColor = new Color(0.88f, 0.55f, 0f, 1f);
		}
		else if (scoringSystem.GetPumaHealth(1) <= 0f) {
			style.normal.textColor = deadPumaTextColor;
			statsColor = deadPumaTextColor;
		}
		else if (scoringSystem.GetPumaHealth(1) >= 1f) {
			style.normal.textColor = fullHealthPumaTextColor;
			statsColor = fullHealthPumaTextColor;
		}
		else {
			style.normal.textColor = unselectedNameColor;
			statsColor = unselectedTextColor;
		}
		style.fontSize = (int)(overlayRect.width * ((guiManager.selectedPuma == 1) ? 0.021f : 0.019f));
		GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * ((guiManager.selectedPuma == 1) ? 0.71f : 0.71f) + yOffsetForAddingPopulationBar + textUpShift, textureWidth, overlayRect.height * 0.08f), "Palo", style);
		style.fontSize = (int)(overlayRect.width * ((guiManager.selectedPuma == 1) ? 0.015f : 0.014f));
		style.normal.textColor = statsColor;
		GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * ((guiManager.selectedPuma == 1) ? 0.735f : 0.735f) + yOffsetForAddingPopulationBar + textUpShift, textureWidth, overlayRect.height * 0.08f), "2 years - female", style);
		style.normal.textColor = Color.white;

		// adult male
		textureX += overlayRect.width * ((guiManager.selectedPuma == 1) ? 0.18f : 0.135f);
		textureX += overlayRect.width * ((guiManager.selectedPuma == 2) ? -0.002f : 0f);
		textureY = overlayRect.y + overlayRect.height * ((guiManager.selectedPuma == 2) ? 0.572f : 0.585f) + yOffsetForAddingPopulationBar;
		textureWidth = overlayRect.width * ((guiManager.selectedPuma == 2) ? 0.16f : 0.115f);
		textureHeight = pumaIconTexture.height * (textureWidth / pumaIconTexture.width);
		if (flashPumas == true && PumaIsSelectable(2)) {
			GUI.color = new Color(1f, 1f, 1f, flashPumasOpacity * selectScreenOpacity);		
			GUI.DrawTexture(new Rect(textureX - textureWidth * 0.05f, textureY - textureHeight * 2.24f, textureWidth * 1.1f, textureHeight * 3.45f), greenOutlineRectVertTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);		
		}		
		else if (PumaIsSelectable(2) && guiManager.selectedPuma != 2) {
			GUI.color = selectablePumaOutlineColor;	
			GUI.DrawTexture(new Rect(textureX - textureWidth * 0.05f, textureY - textureHeight * 2.24f, textureWidth * 1.1f, textureHeight * 3.45f), greenOutlineRectVertTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);		
		}
		else if (guiManager.selectedPuma == 2) {
			GUI.color = selectedPumaOutlineColor;	
			GUI.DrawTexture(new Rect(textureX - textureWidth * 0.07f, textureY - textureHeight * 2.10f, textureWidth * 1.14f, textureHeight * 3.06f), greenOutlineRectVertTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);		
		}
		if (guiManager.selectedPuma != 2) {
			// background panel for puma head
			headshotY = overlayRect.y + overlayRect.height * 0.355f + yOffsetForAddingPopulationBar + headUpShift;
			GUI.color = new Color(0f, 0f, 0f, 0.8f * selectScreenOpacity);
			GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY, textureWidth - overlayRect.width * .0f, overlayRect.height * 0.1f + headshotBackgroundHeightAddition), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
			// puma head
			headshotTexture = closeup3Texture;
			headshotHeight = overlayRect.height * 0.085f;
			headshotWidth = headshotTexture.width * (headshotHeight / headshotTexture.height);
			headshotX = textureX + (textureWidth - headshotWidth) * 0.5f;
			if (scoringSystem.GetPumaHealth(2) <= 0f)
				GUI.color = deadPumaHeadshotColor;
			GUI.DrawTexture(new Rect(headshotX, headshotY, headshotWidth, headshotHeight), headshotTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
			// health bar
			if (scoringSystem.GetPumaHealth(2) > 0f)
				guiComponents.DrawPumaHealthBar(2, selectScreenOpacity, textureX, headshotY - headshotHeight * 0.36f + healthDownShift, textureWidth, headshotHeight * 0.26f, true);
			// background panel for puma middle
			headshotY = overlayRect.y + overlayRect.height * 0.47f + yOffsetForAddingPopulationBar + barsDownShift;
			GUI.color = new Color(0f, 0f, 0f, 0.8f * selectScreenOpacity);
			GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY, textureWidth - overlayRect.width * .0f, overlayRect.height * 0.1f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
			// display bars in puma middle
			DrawDisplayBars(2, selectScreenOpacity, headshotX, headshotY, headshotWidth, headshotHeight);
			// final ending label at very top
			if (scoringSystem.GetPumaHealth(2) <= 0f) {
				// puma is dead
				headshotY = overlayRect.y + overlayRect.height * 0.2885f + yOffsetForAddingPopulationBar + headUpShift;
				GUI.color = new Color(0f, 0f, 0f, 0.8f * selectScreenOpacity);
				GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY + endingLabelDownshift, textureWidth - overlayRect.width * .0f, headshotHeight * 0.3f), "");
				GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
				style.normal.textColor = deadPumaAnnounceColor;
				style.fontSize = (int)(overlayRect.width * 0.012f);
				GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * 0.26f + yOffsetForAddingPopulationBar + textUpShift + endingLabelDownshift, textureWidth, overlayRect.height * 0.08f), scoringSystem.WasKilledByCar(2) ? "VEHICLE" : "STARVED", style);
			}
		}
		// background panel for text or puma icon
		if (guiManager.selectedPuma == 2) {
			GUI.color = new Color(0f, 0f, 0f, 0.4f * selectScreenOpacity);
			//GUI.Box(new Rect(textureX + overlayRect.width * .015f, overlayRect.y + overlayRect.height * 0.73f + yOffsetForAddingPopulationBar, textureWidth - overlayRect.width * .03f, overlayRect.height * 0.065f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
		}
		else {
			// not selected
			GUI.color = new Color(0f, 0f, 0f, 0.8f * selectScreenOpacity);
			GUI.Box(new Rect(textureX + overlayRect.width * .0f, overlayRect.y + overlayRect.height * 0.585f + yOffsetForAddingPopulationBar + iconDownShift, textureWidth - overlayRect.width * .0f, overlayRect.height * 0.14f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
		}
		// puma icon
		GUI.color = new Color(1f, 1f, 1f, 0f * selectScreenOpacity);
		if (GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * 0.315f, textureWidth, overlayRect.height * 0.47f), "") && PumaIsSelectable(2)) {
			guiManager.selectedPuma = 2;
			levelManager.SetSelectedPuma(2);
		}
		GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
		if (guiManager.selectedPuma == 2) {
			float selectedIconX = textureX + overlayRect.width * 0.024f;
			float selectedIconY = textureY + overlayRect.height * 0.018f;
			float selectedIconWidth = textureWidth * 0.74f;
			float selectedIconHeight = textureHeight * 0.73f;
			GUI.DrawTexture(new Rect(selectedIconX - (selectedIconWidth * 0.05f), selectedIconY - (selectedIconHeight * 0.055f) + iconDownShift, selectedIconWidth * 1.116f, selectedIconHeight * 1.116f), pumaIconShadowTexture);
			GUI.DrawTexture(new Rect(selectedIconX, selectedIconY + iconDownShift, selectedIconWidth, selectedIconHeight), pumaIconTexture);
		}
		else {
			if (scoringSystem.GetPumaHealth(2) <= 0f)
				GUI.color = deadPumaIconColor;
			else
				GUI.color = new Color(1f, 1f, 1f, 0.7f * selectScreenOpacity);
			GUI.DrawTexture(new Rect(textureX + textureWidth * 0.05f, textureY + textureHeight * 0.12f + iconDownShift, textureWidth * 0.9f, textureHeight * 0.9f), pumaIconTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
		}
		if (guiManager.selectedPuma == 2)
			DrawSelectScreenDetailsPanel(selectScreenOpacity, style, textureX, textureWidth, headshot3Texture);
		// text
		if (guiManager.selectedPuma == 2) {
			style.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f);
			statsColor = new Color(0.88f, 0.55f, 0f, 1f);
		}
		else if (scoringSystem.GetPumaHealth(2) <= 0f) {
			style.normal.textColor = deadPumaTextColor;
			statsColor = deadPumaTextColor;
		}
		else if (scoringSystem.GetPumaHealth(2) >= 1f) {
			style.normal.textColor = fullHealthPumaTextColor;
			statsColor = fullHealthPumaTextColor;
		}
		else {
			style.normal.textColor = unselectedNameColor;
			statsColor = unselectedTextColor;
		}
		style.fontSize = (int)(overlayRect.width * ((guiManager.selectedPuma == 2) ? 0.021f : 0.019f));
		GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * ((guiManager.selectedPuma == 2) ? 0.71f : 0.71f) + yOffsetForAddingPopulationBar + textUpShift, textureWidth, overlayRect.height * 0.08f), "Mitch", style);
		style.fontSize = (int)(overlayRect.width * ((guiManager.selectedPuma == 2) ? 0.015f : 0.014f));
		style.normal.textColor = statsColor;
		GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * ((guiManager.selectedPuma == 2) ? 0.735f : 0.735f) + yOffsetForAddingPopulationBar + textUpShift, textureWidth, overlayRect.height * 0.08f), "5 years - male", style);
		style.normal.textColor = Color.white;

		// adult female
		textureX += overlayRect.width * ((guiManager.selectedPuma == 2) ? 0.18f : 0.135f);
		textureX += overlayRect.width * ((guiManager.selectedPuma == 3) ? -0.002f : 0f);
		textureY = overlayRect.y + overlayRect.height * ((guiManager.selectedPuma == 3) ? 0.572f : 0.585f) + yOffsetForAddingPopulationBar;
		textureWidth = overlayRect.width * ((guiManager.selectedPuma == 3) ? 0.16f : 0.115f);
		textureHeight = pumaIconTexture.height * (textureWidth / pumaIconTexture.width);
		if (flashPumas == true && PumaIsSelectable(3)) {
			GUI.color = new Color(1f, 1f, 1f, flashPumasOpacity * selectScreenOpacity);		
			GUI.DrawTexture(new Rect(textureX - textureWidth * 0.05f, textureY - textureHeight * 2.24f, textureWidth * 1.1f, textureHeight * 3.45f), greenOutlineRectVertTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);		
		}		
		else if (PumaIsSelectable(3) && guiManager.selectedPuma != 3) {
			GUI.color = selectablePumaOutlineColor;	
			GUI.DrawTexture(new Rect(textureX - textureWidth * 0.05f, textureY - textureHeight * 2.24f, textureWidth * 1.1f, textureHeight * 3.45f), greenOutlineRectVertTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);		
		}
		else if (guiManager.selectedPuma == 3) {
			GUI.color = selectedPumaOutlineColor;	
			GUI.DrawTexture(new Rect(textureX - textureWidth * 0.07f, textureY - textureHeight * 2.10f, textureWidth * 1.14f, textureHeight * 3.06f), greenOutlineRectVertTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);		
		}
		if (guiManager.selectedPuma != 3) {
			// background panel for puma head
			headshotY = overlayRect.y + overlayRect.height * 0.355f + yOffsetForAddingPopulationBar + headUpShift;
			GUI.color = new Color(0f, 0f, 0f, 0.8f * selectScreenOpacity);
			GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY, textureWidth - overlayRect.width * .0f, overlayRect.height * 0.1f + headshotBackgroundHeightAddition), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
			// puma head
			headshotTexture = closeup4Texture;
			headshotHeight = overlayRect.height * 0.085f;
			headshotWidth = headshotTexture.width * (headshotHeight / headshotTexture.height);
			headshotX = textureX + (textureWidth - headshotWidth) * 0.5f;
			if (scoringSystem.GetPumaHealth(3) <= 0f)
				GUI.color = deadPumaHeadshotColor;
			GUI.DrawTexture(new Rect(headshotX, headshotY, headshotWidth, headshotHeight), headshotTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
			// health bar
			if (scoringSystem.GetPumaHealth(3) > 0f)
				guiComponents.DrawPumaHealthBar(3, selectScreenOpacity, textureX, headshotY - headshotHeight * 0.36f + healthDownShift, textureWidth, headshotHeight * 0.26f, true);
			// background panel for puma middle
			headshotY = overlayRect.y + overlayRect.height * 0.47f + yOffsetForAddingPopulationBar + barsDownShift;
			GUI.color = new Color(0f, 0f, 0f, 0.8f * selectScreenOpacity);
			GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY, textureWidth - overlayRect.width * .0f, overlayRect.height * 0.1f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
			// display bars in puma middle
			DrawDisplayBars(3, selectScreenOpacity, headshotX, headshotY, headshotWidth, headshotHeight);
			// final ending label at very top
			if (scoringSystem.GetPumaHealth(3) <= 0f) {
				// puma is dead
				headshotY = overlayRect.y + overlayRect.height * 0.2885f + yOffsetForAddingPopulationBar + headUpShift;
				GUI.color = new Color(0f, 0f, 0f, 0.8f * selectScreenOpacity);
				GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY + endingLabelDownshift, textureWidth - overlayRect.width * .0f, headshotHeight * 0.3f), "");
				GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
				style.normal.textColor = deadPumaAnnounceColor;
				style.fontSize = (int)(overlayRect.width * 0.012f);
				GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * 0.26f + yOffsetForAddingPopulationBar + textUpShift + endingLabelDownshift, textureWidth, overlayRect.height * 0.08f), scoringSystem.WasKilledByCar(3) ? "VEHICLE" : "STARVED", style);
			}
		}
		// background panel for text or puma icon
		if (guiManager.selectedPuma == 3) {
			GUI.color = new Color(0f, 0f, 0f, 0.4f * selectScreenOpacity);
			//GUI.Box(new Rect(textureX + overlayRect.width * .015f, overlayRect.y + overlayRect.height * 0.73f + yOffsetForAddingPopulationBar, textureWidth - overlayRect.width * .03f, overlayRect.height * 0.065f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
		}
		else {
			// not selected
			GUI.color = new Color(0f, 0f, 0f, 0.8f * selectScreenOpacity);
			GUI.Box(new Rect(textureX + overlayRect.width * .0f, overlayRect.y + overlayRect.height * 0.585f + yOffsetForAddingPopulationBar + iconDownShift, textureWidth - overlayRect.width * .0f, overlayRect.height * 0.14f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
		}
		// puma icon
		GUI.color = new Color(1f, 1f, 1f, 0f * selectScreenOpacity);
		if (GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * 0.315f, textureWidth, overlayRect.height * 0.47f), "") && PumaIsSelectable(3)) {
			guiManager.selectedPuma = 3;
			levelManager.SetSelectedPuma(3);
		}
		GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
		if (guiManager.selectedPuma == 3) {
			float selectedIconX = textureX + overlayRect.width * 0.024f;
			float selectedIconY = textureY + overlayRect.height * 0.018f;
			float selectedIconWidth = textureWidth * 0.74f;
			float selectedIconHeight = textureHeight * 0.73f;
			GUI.DrawTexture(new Rect(selectedIconX - (selectedIconWidth * 0.05f), selectedIconY - (selectedIconHeight * 0.055f) + iconDownShift, selectedIconWidth * 1.116f, selectedIconHeight * 1.116f), pumaIconShadowTexture);
			GUI.DrawTexture(new Rect(selectedIconX, selectedIconY + iconDownShift, selectedIconWidth, selectedIconHeight), pumaIconTexture);
		}
		else {
			if (scoringSystem.GetPumaHealth(3) <= 0f)
				GUI.color = deadPumaIconColor;
			else
				GUI.color = new Color(1f, 1f, 1f, 0.7f * selectScreenOpacity);
			GUI.DrawTexture(new Rect(textureX + textureWidth * 0.05f, textureY + textureHeight * 0.12f + iconDownShift, textureWidth * 0.9f, textureHeight * 0.9f), pumaIconTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
		}
		if (guiManager.selectedPuma == 3)
			DrawSelectScreenDetailsPanel(selectScreenOpacity, style, textureX, textureWidth, headshot4Texture);
		// text
		if (guiManager.selectedPuma == 3) {
			style.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f);
			statsColor = new Color(0.88f, 0.55f, 0f, 1f);
		}
		else if (scoringSystem.GetPumaHealth(3) <= 0f) {
			style.normal.textColor = deadPumaTextColor;
			statsColor = deadPumaTextColor;
		}
		else if (scoringSystem.GetPumaHealth(3) >= 1f) {
			style.normal.textColor = fullHealthPumaTextColor;
			statsColor = fullHealthPumaTextColor;
		}
		else {
			style.normal.textColor = unselectedNameColor;
			statsColor = unselectedTextColor;
		}
		style.fontSize = (int)(overlayRect.width * ((guiManager.selectedPuma == 3) ? 0.021f : 0.019f));
		GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * ((guiManager.selectedPuma == 3) ? 0.71f : 0.71f) + yOffsetForAddingPopulationBar + textUpShift, textureWidth, overlayRect.height * 0.08f), "Trish", style);
		style.fontSize = (int)(overlayRect.width * ((guiManager.selectedPuma == 3) ? 0.015f : 0.014f));
		style.normal.textColor = statsColor;
		GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * ((guiManager.selectedPuma == 3) ? 0.735f : 0.735f) + yOffsetForAddingPopulationBar + textUpShift, textureWidth, overlayRect.height * 0.08f), "5 years - female", style);
		style.normal.textColor = Color.white;

		// old male
		textureX += overlayRect.width * ((guiManager.selectedPuma == 3) ? 0.18f : 0.135f);
		textureX += overlayRect.width * ((guiManager.selectedPuma == 4) ? -0.002f : 0f);
		textureY = overlayRect.y + overlayRect.height * ((guiManager.selectedPuma == 4) ? 0.572f : 0.585f) + yOffsetForAddingPopulationBar;
		textureWidth = overlayRect.width * ((guiManager.selectedPuma == 4) ? 0.16f : 0.115f);
		textureHeight = pumaIconTexture.height * (textureWidth / pumaIconTexture.width);
		if (flashPumas == true && PumaIsSelectable(4)) {
			GUI.color = new Color(1f, 1f, 1f, flashPumasOpacity * selectScreenOpacity);		
			GUI.DrawTexture(new Rect(textureX - textureWidth * 0.05f, textureY - textureHeight * 2.24f, textureWidth * 1.1f, textureHeight * 3.45f), greenOutlineRectVertTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);		
		}		
		else if (PumaIsSelectable(4) && guiManager.selectedPuma != 4) {
			GUI.color = selectablePumaOutlineColor;	
			GUI.DrawTexture(new Rect(textureX - textureWidth * 0.05f, textureY - textureHeight * 2.24f, textureWidth * 1.1f, textureHeight * 3.45f), greenOutlineRectVertTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);		
		}
		else if (guiManager.selectedPuma == 4) {
			GUI.color = selectedPumaOutlineColor;	
			GUI.DrawTexture(new Rect(textureX - textureWidth * 0.07f, textureY - textureHeight * 2.10f, textureWidth * 1.14f, textureHeight * 3.06f), greenOutlineRectVertTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);		
		}
		if (guiManager.selectedPuma != 4) {
			// background panel for puma head
			headshotY = overlayRect.y + overlayRect.height * 0.355f + yOffsetForAddingPopulationBar + headUpShift;
			GUI.color = new Color(0f, 0f, 0f, 0.8f * selectScreenOpacity);
			GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY, textureWidth - overlayRect.width * .0f, overlayRect.height * 0.1f + headshotBackgroundHeightAddition), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
			// puma head
			headshotTexture = closeup5Texture;
			headshotHeight = overlayRect.height * 0.085f;
			headshotWidth = headshotTexture.width * (headshotHeight / headshotTexture.height);
			headshotX = textureX + (textureWidth - headshotWidth) * 0.5f;
			if (scoringSystem.GetPumaHealth(4) <= 0f)
				GUI.color = deadPumaHeadshotColor;
			GUI.DrawTexture(new Rect(headshotX, headshotY, headshotWidth, headshotHeight), headshotTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
			// health bar
			if (scoringSystem.GetPumaHealth(4) > 0f)
				guiComponents.DrawPumaHealthBar(4, selectScreenOpacity, textureX, headshotY - headshotHeight * 0.36f + healthDownShift, textureWidth, headshotHeight * 0.26f, true);
			// background panel for puma middle
			headshotY = overlayRect.y + overlayRect.height * 0.47f + yOffsetForAddingPopulationBar + barsDownShift;
			GUI.color = new Color(0f, 0f, 0f, 0.8f * selectScreenOpacity);
			GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY, textureWidth - overlayRect.width * .0f, overlayRect.height * 0.1f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
			// display bars in puma middle
			DrawDisplayBars(4, selectScreenOpacity, headshotX, headshotY, headshotWidth, headshotHeight);
			// final ending label at very top
			if (scoringSystem.GetPumaHealth(4) <= 0f) {
				// puma is dead
				headshotY = overlayRect.y + overlayRect.height * 0.2885f + yOffsetForAddingPopulationBar + headUpShift;
				GUI.color = new Color(0f, 0f, 0f, 0.8f * selectScreenOpacity);
				GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY + endingLabelDownshift, textureWidth - overlayRect.width * .0f, headshotHeight * 0.3f), "");
				GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
				style.normal.textColor = deadPumaAnnounceColor;
				style.fontSize = (int)(overlayRect.width * 0.012f);
				GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * 0.26f + yOffsetForAddingPopulationBar + textUpShift + endingLabelDownshift, textureWidth, overlayRect.height * 0.08f), scoringSystem.WasKilledByCar(4) ? "VEHICLE" : "STARVED", style);
			}
		}
		// background panel for text or puma icon
		if (guiManager.selectedPuma == 4) {
			GUI.color = new Color(0f, 0f, 0f, 0.4f * selectScreenOpacity);
			//GUI.Box(new Rect(textureX + overlayRect.width * .015f, overlayRect.y + overlayRect.height * 0.73f + yOffsetForAddingPopulationBar, textureWidth - overlayRect.width * .03f, overlayRect.height * 0.065f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
		}
		else {
			// not selected
			GUI.color = new Color(0f, 0f, 0f, 0.8f * selectScreenOpacity);
			GUI.Box(new Rect(textureX + overlayRect.width * .0f, overlayRect.y + overlayRect.height * 0.585f + yOffsetForAddingPopulationBar + iconDownShift, textureWidth - overlayRect.width * .0f, overlayRect.height * 0.14f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
		}
		// puma icon
		GUI.color = new Color(1f, 1f, 1f, 0f * selectScreenOpacity);
		if (GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * 0.315f, textureWidth, overlayRect.height * 0.47f), "") && PumaIsSelectable(4)) {
			guiManager.selectedPuma = 4;
			levelManager.SetSelectedPuma(4);
		}
		GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
		if (guiManager.selectedPuma == 4) {
			float selectedIconX = textureX + overlayRect.width * 0.024f;
			float selectedIconY = textureY + overlayRect.height * 0.018f;
			float selectedIconWidth = textureWidth * 0.74f;
			float selectedIconHeight = textureHeight * 0.73f;
			GUI.DrawTexture(new Rect(selectedIconX - (selectedIconWidth * 0.05f), selectedIconY - (selectedIconHeight * 0.055f) + iconDownShift, selectedIconWidth * 1.116f, selectedIconHeight * 1.116f), pumaIconShadowTexture);
			GUI.DrawTexture(new Rect(selectedIconX, selectedIconY + iconDownShift, selectedIconWidth, selectedIconHeight), pumaIconTexture);
		}
		else {
			if (scoringSystem.GetPumaHealth(4) <= 0f)
				GUI.color = deadPumaIconColor;
			else
				GUI.color = new Color(1f, 1f, 1f, 0.7f * selectScreenOpacity);
			GUI.DrawTexture(new Rect(textureX + textureWidth * 0.05f, textureY + textureHeight * 0.12f + iconDownShift, textureWidth * 0.9f, textureHeight * 0.9f), pumaIconTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
		}
		if (guiManager.selectedPuma == 4)
			DrawSelectScreenDetailsPanel(selectScreenOpacity, style, textureX, textureWidth, headshot5Texture);
		// text
		if (guiManager.selectedPuma == 4) {
			style.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f);
			statsColor = new Color(0.88f, 0.55f, 0f, 1f);
		}
		else if (scoringSystem.GetPumaHealth(4) <= 0f) {
			style.normal.textColor = deadPumaTextColor;
			statsColor = deadPumaTextColor;
		}
		else if (scoringSystem.GetPumaHealth(4) >= 1f) {
			style.normal.textColor = fullHealthPumaTextColor;
			statsColor = fullHealthPumaTextColor;
		}
		else {
			style.normal.textColor = unselectedNameColor;
			statsColor = unselectedTextColor;
		}
		style.fontSize = (int)(overlayRect.width * ((guiManager.selectedPuma == 4) ? 0.021f : 0.019f));
		GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * ((guiManager.selectedPuma == 4) ? 0.71f : 0.71f) + yOffsetForAddingPopulationBar + textUpShift, textureWidth, overlayRect.height * 0.08f), "Liam", style);
		style.fontSize = (int)(overlayRect.width * ((guiManager.selectedPuma == 4) ? 0.015f : 0.014f));
		style.normal.textColor = statsColor;
		GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * ((guiManager.selectedPuma == 4) ? 0.735f : 0.735f) + yOffsetForAddingPopulationBar + textUpShift, textureWidth, overlayRect.height * 0.08f), "8 years - male", style);
		style.normal.textColor = Color.white;

		// old female
		textureX += overlayRect.width * ((guiManager.selectedPuma == 4) ? 0.18f : 0.135f);
		textureX += overlayRect.width * ((guiManager.selectedPuma == 5) ? -0.002f : 0f);
		textureY = overlayRect.y + overlayRect.height * ((guiManager.selectedPuma == 5) ? 0.572f : 0.585f) + yOffsetForAddingPopulationBar;
		textureWidth = overlayRect.width * ((guiManager.selectedPuma == 5) ? 0.16f : 0.115f);
		textureHeight = pumaIconTexture.height * (textureWidth / pumaIconTexture.width);
		if (flashPumas == true && PumaIsSelectable(5)) {
			GUI.color = new Color(1f, 1f, 1f, flashPumasOpacity * selectScreenOpacity);		
			GUI.DrawTexture(new Rect(textureX - textureWidth * 0.05f, textureY - textureHeight * 2.24f, textureWidth * 1.1f, textureHeight * 3.45f), greenOutlineRectVertTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);		
		}		
		else if (PumaIsSelectable(5) && guiManager.selectedPuma != 5) {
			GUI.color = selectablePumaOutlineColor;	
			GUI.DrawTexture(new Rect(textureX - textureWidth * 0.05f, textureY - textureHeight * 2.24f, textureWidth * 1.1f, textureHeight * 3.45f), greenOutlineRectVertTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);		
		}
		else if (guiManager.selectedPuma == 5) {
			GUI.color = selectedPumaOutlineColor;	
			GUI.DrawTexture(new Rect(textureX - textureWidth * 0.07f, textureY - textureHeight * 2.10f, textureWidth * 1.14f, textureHeight * 3.06f), greenOutlineRectVertTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);		
		}
		if (guiManager.selectedPuma != 5) {
			// background panel for puma head
			headshotY = overlayRect.y + overlayRect.height * 0.355f + yOffsetForAddingPopulationBar + headUpShift;
			GUI.color = new Color(0f, 0f, 0f, 0.8f * selectScreenOpacity);
			GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY, textureWidth - overlayRect.width * .0f, overlayRect.height * 0.1f + headshotBackgroundHeightAddition), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
			// puma head
			headshotTexture = closeup6Texture;
			headshotHeight = overlayRect.height * 0.085f;
			headshotWidth = headshotTexture.width * (headshotHeight / headshotTexture.height);
			headshotX = textureX + (textureWidth - headshotWidth) * 0.5f;
			if (scoringSystem.GetPumaHealth(5) <= 0f)
				GUI.color = deadPumaHeadshotColor;
			GUI.DrawTexture(new Rect(headshotX, headshotY, headshotWidth, headshotHeight), headshotTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
			// health bar
			if (scoringSystem.GetPumaHealth(5) > 0f)
				guiComponents.DrawPumaHealthBar(5, selectScreenOpacity, textureX, headshotY - headshotHeight * 0.36f + healthDownShift, textureWidth, headshotHeight * 0.26f, true);
			// background panel for puma middle
			headshotY = overlayRect.y + overlayRect.height * 0.47f + yOffsetForAddingPopulationBar + barsDownShift;
			GUI.color = new Color(0f, 0f, 0f, 0.8f * selectScreenOpacity);
			GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY, textureWidth - overlayRect.width * .0f, overlayRect.height * 0.1f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
			// display bars in puma middle
			DrawDisplayBars(5, selectScreenOpacity, headshotX, headshotY, headshotWidth, headshotHeight);
			// final ending label at very top
			if (scoringSystem.GetPumaHealth(5) <= 0f) {
				// puma is dead
				headshotY = overlayRect.y + overlayRect.height * 0.2885f + yOffsetForAddingPopulationBar + headUpShift;
				GUI.color = new Color(0f, 0f, 0f, 0.8f * selectScreenOpacity);
				GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY + endingLabelDownshift, textureWidth - overlayRect.width * .0f, headshotHeight * 0.3f), "");
				GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
				style.normal.textColor = deadPumaAnnounceColor;
				style.fontSize = (int)(overlayRect.width * 0.012f);
				GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * 0.26f + yOffsetForAddingPopulationBar + textUpShift + endingLabelDownshift, textureWidth, overlayRect.height * 0.08f), scoringSystem.WasKilledByCar(5) ? "VEHICLE" : "STARVED", style);
			}
		}
		// background panel for text or puma icon
		if (guiManager.selectedPuma == 5) {
			GUI.color = new Color(0f, 0f, 0f, 0.4f * selectScreenOpacity);
			//GUI.Box(new Rect(textureX + overlayRect.width * .015f, overlayRect.y + overlayRect.height * 0.73f + yOffsetForAddingPopulationBar, textureWidth - overlayRect.width * .03f, overlayRect.height * 0.065f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
		}
		else {
			// not selected
			GUI.color = new Color(0f, 0f, 0f, 0.8f * selectScreenOpacity);
			GUI.Box(new Rect(textureX + overlayRect.width * .0f, overlayRect.y + overlayRect.height * 0.585f + yOffsetForAddingPopulationBar + iconDownShift, textureWidth - overlayRect.width * .0f, overlayRect.height * 0.14f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
		}
		// puma icon
		GUI.color = new Color(1f, 1f, 1f, 0f * selectScreenOpacity);
		if (GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * 0.315f, textureWidth, overlayRect.height * 0.47f), "") && PumaIsSelectable(5)) {
			guiManager.selectedPuma = 5;
			levelManager.SetSelectedPuma(5);
		}
		GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
		if (guiManager.selectedPuma == 5) {
			float selectedIconX = textureX + overlayRect.width * 0.024f;
			float selectedIconY = textureY + overlayRect.height * 0.018f;
			float selectedIconWidth = textureWidth * 0.74f;
			float selectedIconHeight = textureHeight * 0.73f;
			GUI.DrawTexture(new Rect(selectedIconX - (selectedIconWidth * 0.05f), selectedIconY - (selectedIconHeight * 0.055f) + iconDownShift, selectedIconWidth * 1.116f, selectedIconHeight * 1.116f), pumaIconShadowTexture);
			GUI.DrawTexture(new Rect(selectedIconX, selectedIconY + iconDownShift, selectedIconWidth, selectedIconHeight), pumaIconTexture);
		}
		else {
			if (scoringSystem.GetPumaHealth(5) <= 0f)
				GUI.color = deadPumaIconColor;
			else
				GUI.color = new Color(1f, 1f, 1f, 0.7f * selectScreenOpacity);
			GUI.DrawTexture(new Rect(textureX + textureWidth * 0.05f, textureY + textureHeight * 0.12f + iconDownShift, textureWidth * 0.9f, textureHeight * 0.9f), pumaIconTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
		}
		if (guiManager.selectedPuma == 5)
			DrawSelectScreenDetailsPanel(selectScreenOpacity, style, textureX, textureWidth, headshot6Texture);
		// text
		if (guiManager.selectedPuma == 5) {
			style.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f);
			statsColor = new Color(0.88f, 0.55f, 0f, 1f);
		}
		else if (scoringSystem.GetPumaHealth(5) <= 0f) {
			style.normal.textColor = deadPumaTextColor;
			statsColor = deadPumaTextColor;
		}
		else if (scoringSystem.GetPumaHealth(5) >= 1f) {
			style.normal.textColor = fullHealthPumaTextColor;
			statsColor = fullHealthPumaTextColor;
		}
		else {
			style.normal.textColor = unselectedNameColor;
			statsColor = unselectedTextColor;
		}
		style.fontSize = (int)(overlayRect.width * ((guiManager.selectedPuma == 5) ? 0.021f : 0.019f));
		GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * ((guiManager.selectedPuma == 5) ? 0.71f : 0.71f) + yOffsetForAddingPopulationBar + textUpShift, textureWidth, overlayRect.height * 0.08f), "Barb", style);
		style.fontSize = (int)(overlayRect.width * ((guiManager.selectedPuma == 5) ? 0.015f : 0.014f));
		style.normal.textColor = statsColor;
		GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * ((guiManager.selectedPuma == 5) ? 0.735f : 0.735f) + yOffsetForAddingPopulationBar + textUpShift, textureWidth, overlayRect.height * 0.08f), "8 years - female", style);
		style.normal.textColor = Color.white;
		
	}	
	

	void DrawSelectScreenDetailsPanel(float selectScreenDetailsOpacity, GUIStyle style, float textureX, float textureWidth, Texture2D headshotTexture) 
	{ 
		float headshotY = overlayRect.y + overlayRect.height * 0.23f;
		float headshotHeight = overlayRect.height * 0.17f;
		float headshotWidth = headshotTexture.width * (headshotHeight / headshotTexture.height);
		float headshotX = textureX + (0.75f * (textureWidth - headshotWidth));

		float detailsPanelX = headshotX - overlayRect.width * 0.01f;
		float detailsPanelY = headshotY - overlayRect.height * 0.015f;
		float detailsPanelWidth = headshotWidth + overlayRect.width * 0.02f;
		float detailsPanelHeight = headshotHeight + overlayRect.height * 0.18f;
		
		
		float upperPanelShrinkFactor = 0.05f;
		

		// used in DrawSelectScreen() and DrawSelectScreenDetailsPanel()
		float headUpShift = overlayRect.height * -0.03f;
		float textUpShift = overlayRect.height * -0.303f;
		float barsDownShift = overlayRect.height * 0.06f;
		float healthDownShift = overlayRect.height * 0.37f;
		float headshotBackgroundHeightAddition = overlayRect.height * 0.06f;

		headUpShift = 0f;
		healthDownShift = 0f;


		textUpShift = 0f;
		barsDownShift = 0f;
		headshotBackgroundHeightAddition = 0f;

		
		// background panel
		GUI.color = new Color(0f, 0f, 0f, 1f * selectScreenDetailsOpacity);
		GUI.Box(new Rect(detailsPanelX + detailsPanelWidth * 0.03f, detailsPanelY + (detailsPanelHeight * 0.113f) + headUpShift, detailsPanelWidth - detailsPanelWidth * 0.06f, detailsPanelHeight * 0.526f + headshotBackgroundHeightAddition), "");
		GUI.Box(new Rect(headshotX, detailsPanelY + detailsPanelHeight * 0.68f + barsDownShift, headshotWidth, detailsPanelHeight * 0.285f), "");
		if (headshotTexture != headshot1Texture && headshotTexture != headshot2Texture && headshotTexture != headshot3Texture) {
			//GUI.color = new Color(0f, 0f, 0f, 1f * selectScreenDetailsOpacity);
			//GUI.Box(new Rect(detailsPanelX + detailsPanelWidth * 0.03f, detailsPanelY + (detailsPanelHeight * 0.113f) + headUpShift, detailsPanelWidth - detailsPanelWidth * 0.06f, detailsPanelHeight * 0.526f + headshotBackgroundHeightAddition), "");
			//GUI.Box(new Rect(headshotX, detailsPanelY + detailsPanelHeight * 0.68f + barsDownShift, headshotWidth, detailsPanelHeight * 0.285f), "");
		}
		//GUI.color = new Color(1f, 1f, 1f, 0.8f);
		//GUI.Box(new Rect(detailsPanelX, detailsPanelY, detailsPanelWidth, detailsPanelHeight * 0.85f), "");

		float origHeadshotYPos = headshotY + (headshotHeight * upperPanelShrinkFactor) - headshotHeight * 0.03f;
		float origHealthbarYPos = headshotY + headshotHeight + headshotHeight * 0.025f;
		
		// puma closeup headshot
		GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenDetailsOpacity);
		GUI.DrawTexture(new Rect(headshotX + (headshotWidth * upperPanelShrinkFactor * 0.5f), origHeadshotYPos + headshotHeight * 0.19f + headUpShift, headshotWidth * (1f - upperPanelShrinkFactor), headshotHeight * (1f - upperPanelShrinkFactor)), headshotTexture);

		// health bar
		GUI.color = new Color(0f, 0f, 0f, 1f * selectScreenDetailsOpacity);
		GUI.Box(new Rect(detailsPanelX + detailsPanelWidth * 0.03f, origHeadshotYPos - headshotHeight * 0.091f + healthDownShift, detailsPanelWidth - detailsPanelWidth * 0.06f, headshotHeight * 0.17f), "");
		GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenDetailsOpacity);
		guiComponents.DrawPumaHealthBar(guiManager.selectedPuma, selectScreenDetailsOpacity, detailsPanelX + detailsPanelWidth * 0.03f, origHeadshotYPos - headshotHeight * 0.091f + healthDownShift, detailsPanelWidth - detailsPanelWidth * 0.06f, headshotHeight * 0.17f);

		
		float displayBarsRightShift = headshotWidth * -0.04f;
		
		// list of characteristics
		float yOffset = headshotHeight * 0.31f + barsDownShift;
		style.fontSize = (int)(overlayRect.width * 0.0125f);
		style.alignment = TextAnchor.UpperRight;
		style.fontStyle = FontStyle.Bold;

		if (scoringSystem.GetPumaHealth(guiManager.selectedPuma) < 1f) {
			// normal case
			style.normal.textColor = new Color(0.88f * 0.8f, 0.82f * 0.8f, 0.5f * 0.8f, 0.7f); //new Color(0.9f, 0.58f, 0f, 1f);
			//GUI.Button(new Rect(headshotX - overlayRect.width * 0.007f + headshotWidth * 0.27f + displayBarsRightShift, yOffset + headshotY + headshotHeight + overlayRect.height * 0.026f, headshotWidth * 0.22f, headshotHeight), "Stealth", style);
			//GUI.Button(new Rect(headshotX - overlayRect.width * 0.007f + headshotWidth * 0.27f + displayBarsRightShift, yOffset + headshotY + headshotHeight + overlayRect.height * 0.050f, headshotWidth * 0.22f, headshotHeight), "Speed", style);
		}
		else {
			// full health
			//style.normal.textColor = new Color(0f, 0.6f, 0f, 0.8f); 
			//GUI.Button(new Rect(headshotX - overlayRect.width * 0.007f + headshotWidth * 0.30f + displayBarsRightShift, yOffset + headshotY + headshotHeight + overlayRect.height * 0.026f, headshotWidth * 0.22f, headshotHeight), "100%", style);
			//GUI.Button(new Rect(headshotX - overlayRect.width * 0.007f + headshotWidth * 0.30f + displayBarsRightShift, yOffset + headshotY + headshotHeight + overlayRect.height * 0.050f, headshotWidth * 0.22f, headshotHeight), "health", style);
		}

		//GUI.Button(new Rect(headshotX - overlayRect.width * 0.007f + displayBarsRightShift, yOffset + headshotY + headshotHeight + overlayRect.height * 0.050f, headshotWidth * 0.52f, headshotHeight), "Endurance", style);
		//GUI.Button(new Rect(headshotX - overlayRect.width * 0.007f + displayBarsRightShift, yOffset + headshotY + headshotHeight + overlayRect.height * 0.071f, headshotWidth * 0.52f, headshotHeight), "Experience", style);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.MiddleCenter;

		// display bars for characteristics
		
		DrawDisplayBars(guiManager.selectedPuma, selectScreenDetailsOpacity, headshotX + displayBarsRightShift - overlayRect.height * 0.02f, headshotY + overlayRect.height * 0.223f + barsDownShift, headshotWidth, headshotHeight / 2f);
	}
	
	
	//======================================================================
	//======================================================================
	//======================================================================
	//								OPTIONS
	//======================================================================
	//======================================================================
	//======================================================================
	
	void DrawOptionsScreen(float optionsScreenOpacity) 
	{ 
		float optionsScreenX = overlayRect.x + overlayRect.width * 0.06f;
		float optionsScreenY = overlayRect.y + overlayRect.height * 0.205f;
		float optionsScreenWidth = overlayRect.width * 0.88f;
		float optionsScreenHeight = overlayRect.height * 0.65f;
		float fontScale = 0.8f;

		GUIStyle style = new GUIStyle();
		style.alignment = TextAnchor.MiddleCenter;

		// background rectangles
		GUI.color = new Color(0f, 0f, 0f, 1f * optionsScreenOpacity);
		GUI.Box(new Rect(optionsScreenX - overlayRect.width * 0.02f, optionsScreenY - overlayRect.height * 0.025f, optionsScreenWidth + overlayRect.width * 0.04f, optionsScreenHeight + overlayRect.height * 0.05f), "");
		GUI.color = new Color(0f, 0f, 0f, 0.5f * optionsScreenOpacity);
		GUI.Box(new Rect(optionsScreenX, optionsScreenY, optionsScreenWidth, optionsScreenHeight), "");
		GUI.color = new Color(1f, 1f, 1f, 1f * optionsScreenOpacity);

		// title
		GUI.color = new Color(0f, 0f, 0f, 0.8f * optionsScreenOpacity);
		GUI.Box(new Rect(optionsScreenX + optionsScreenWidth * 0.25f, optionsScreenY + optionsScreenHeight * 0.1f, optionsScreenWidth * 0.5f, optionsScreenHeight * 0.14f), "");
		GUI.color = new Color(1f, 1f, 1f, 1f * optionsScreenOpacity);

		style.normal.textColor = new Color(0.90f, 0.75f, 0.4f, 0.7f);	
		style.fontSize = (int)(overlayRect.width * 0.03f);
		style.fontStyle = FontStyle.BoldAndItalic;
		GUI.Button(new Rect(optionsScreenX + optionsScreenWidth * 0.2f, optionsScreenY + optionsScreenHeight * 0.1f, optionsScreenWidth * 0.6f, optionsScreenHeight * 0.14f), "Difficulty Level", style);
	
		// difficulty options
		float optionWidth = optionsScreenWidth * 0.2f;
		float optionMargin =  (optionsScreenWidth - (optionWidth*3)) / 4;
		float optionY =  optionsScreenY + optionsScreenHeight * 0.4f;
		float optionHeight =  optionsScreenHeight * 0.4f;
		GUI.color = new Color(0f, 0f, 0f, 0.8f * optionsScreenOpacity);
		GUI.Box(new Rect(optionsScreenX + optionMargin, optionY, optionWidth, optionHeight), "");
		GUI.Box(new Rect(optionsScreenX + optionMargin  + optionWidth + optionMargin, optionY, optionWidth, optionHeight), "");
		GUI.Box(new Rect(optionsScreenX + optionMargin  + optionWidth + optionMargin  + optionWidth + optionMargin, optionY, optionWidth, optionHeight), "");
		GUI.color = new Color(1f, 1f, 1f, 1f * optionsScreenOpacity);
	
		float neonX = optionsScreenX;
		if (levelManager.difficultyLevel == 0.8f)
			neonX = optionsScreenX + optionMargin;
		else if (levelManager.difficultyLevel == 0.9f)
			neonX = optionsScreenX + optionMargin  + optionWidth + optionMargin;
		else if (levelManager.difficultyLevel == 1f)
			neonX = optionsScreenX + optionMargin  + optionWidth + optionMargin  + optionWidth + optionMargin;
		GUI.color = new Color(0.7f, 0.7f, 0.7f, 0.7f * optionsScreenOpacity);
		GUI.DrawTexture(new Rect(neonX, optionY, optionWidth, optionHeight), greenOutlineRectVertTexture);
		GUI.color = new Color(1f, 1f, 1f, 1f * optionsScreenOpacity);
	
	
		style.fontSize = (int)(overlayRect.width * 0.03f);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.normal.textColor = levelManager.difficultyLevel == 0.8f ? (new Color(0.816f * 1.05f, 0.537f * 1.05f, 0.18f * 1.05f, 1f)) : (new Color(0.90f, 0.75f, 0.4f, 0.7f));
		if (GUI.Button(new Rect(optionsScreenX + optionMargin, optionY, optionWidth, optionHeight), "Easy", style)) {
			levelManager.difficultyLevel = 0.8f;
			trafficManager.InitLevel(levelManager.currentLevel);
		}
		style.normal.textColor = levelManager.difficultyLevel == 0.9f ? (new Color(0.816f * 1.05f, 0.537f * 1.05f, 0.18f * 1.05f, 1f)) : (new Color(0.90f, 0.75f, 0.4f, 0.7f));
		if (GUI.Button(new Rect(optionsScreenX + optionMargin  + optionWidth + optionMargin, optionY, optionWidth, optionHeight), "Medium", style)) {
			levelManager.difficultyLevel = 0.9f;
			trafficManager.InitLevel(levelManager.currentLevel);
		}
		style.normal.textColor = levelManager.difficultyLevel == 1.0f ? (new Color(0.816f * 1.05f, 0.537f * 1.05f, 0.18f * 1.05f, 1f)) : (new Color(0.90f, 0.75f, 0.4f, 0.7f));
		if (GUI.Button(new Rect(optionsScreenX + optionMargin  + optionWidth + optionMargin  + optionWidth + optionMargin, optionY, optionWidth, optionHeight), "Hard", style)) {
			levelManager.difficultyLevel = 1f;
			trafficManager.InitLevel(levelManager.currentLevel);
		}
				
		style.normal.textColor = new Color(0.816f * 1.05f, 0.537f * 1.05f, 0.18f * 1.05f, 1f);
		
		
	}


	//======================================================================
	//======================================================================
	//======================================================================
	//								STATS
	//======================================================================
	//======================================================================
	//======================================================================
	
	void DrawStatsScreen(float statsScreenOpacity) 
	{ 
		float statsX = overlayRect.x + overlayRect.width * 0.06f;
		float statsY = overlayRect.y + overlayRect.height * 0.205f;
		float statsWidth = overlayRect.width * 0.88f;
		float statsHeight = overlayRect.height * 0.65f;
		float fontScale = 1f;

		GUIStyle style = new GUIStyle();
		style.alignment = TextAnchor.MiddleCenter;
		
		Color fullHealthPumaHeadshotColor = new Color(0.99f, 0.92f, 0f, 0.7f * statsScreenOpacity);
		Color fullHealthPumaDeerIconColor = new Color(0.99f, 0.92f, 0f, 0.4f * statsScreenOpacity);
		Color fullHealthPumaTextColor = new Color(0.32f, 0.32f, 0.22f, 0.8f * statsScreenOpacity);

		//Color deadPumaHeadshotColor = new Color(0.8f, 0.1f, 0f, 0.55f * statsScreenOpacity);
		Color deadPumaDeerIconColor = new Color(0.5f, 0.02f, 0f, 0.8f * statsScreenOpacity);
		Color deadPumaTextColor = new Color(0.22f, 0.22f, 0.12f, 0.8f * statsScreenOpacity);

				
		
		//Color fullHealthPumaHeadshotColor = new Color(0.88f, 0.88f, 0.88f, 0.98f * statsScreenOpacity);
		//Color fullHealthPumaIconColor = new Color(0.7f, 0.7f, 0.7f, 0.8f * statsScreenOpacity);
		//Color fullHealthPumaTextColor = unselectedTextColor;

		Color deadPumaHeadshotColor = new Color(0.0f, 0.0f, 0.0f, 0.9f * statsScreenOpacity);
		//Color deadPumaIconColor = new Color(0.08f, 0.08f, 0.08f, 0.99f * statsScreenOpacity);
		//Color deadPumaTextColor = new Color(0.32f * 1.3f, 0.32f * 1.3f, 0.22f * 1.3f, 0.8f * statsScreenOpacity);

		
		
		// background rectangles
		
		GUI.color = new Color(0f, 0f, 0f, 1f * statsScreenOpacity);
		GUI.Box(new Rect(statsX - overlayRect.width * 0.02f, statsY - overlayRect.height * 0.025f, statsWidth + overlayRect.width * 0.04f, statsHeight + overlayRect.height * 0.05f), "");
		GUI.color = new Color(0f, 0f, 0f, 0.2f * statsScreenOpacity);
		GUI.Box(new Rect(statsX, statsY, statsWidth, statsHeight), "");
		
		float topCompression = statsHeight * 0.1f;
		statsY += topCompression;
		statsHeight -= topCompression;

		float rowCount = 7f;
		float rowGap = statsHeight * 0.02f;
		float midRowSizeIncrease = rowGap * 3f;
		float rowHeight = (statsHeight - (rowGap * (rowCount-1)) - midRowSizeIncrease) / rowCount;
		float topRowIndent = statsWidth * 0.175f;

		GUI.color = new Color(0f, 0f, 0f, 0.8f * statsScreenOpacity);
		GUI.Box(new Rect(statsX + topRowIndent, statsY - topCompression, statsWidth * 0.705f, topCompression - rowGap), "");
		GUI.Box(new Rect(statsX, statsY + rowHeight*0 + rowGap*0, statsWidth, rowHeight), "");
		GUI.Box(new Rect(statsX, statsY + rowHeight*1 + rowGap*1, statsWidth, rowHeight), "");
		GUI.Box(new Rect(statsX, statsY + rowHeight*2 + rowGap*2, statsWidth, rowHeight), "");
		GUI.Box(new Rect(statsX, statsY + rowHeight*3 + rowGap*3, statsWidth, rowHeight), "");
		GUI.Box(new Rect(statsX, statsY + rowHeight*4 + rowGap*4, statsWidth, rowHeight), "");
		GUI.Box(new Rect(statsX, statsY + rowHeight*5 + rowGap*5, statsWidth, rowHeight), "");
		GUI.Box(new Rect(statsX, statsY + rowHeight*6 + rowGap*6, statsWidth, rowHeight + midRowSizeIncrease), "");
		GUI.color = new Color(0f, 0f, 0f, 0.7f * statsScreenOpacity);
		GUI.Box(new Rect(statsX, statsY + rowHeight*0 + rowGap*0, statsWidth, rowHeight), "");
		GUI.Box(new Rect(statsX, statsY + rowHeight*1 + rowGap*1, statsWidth, rowHeight), "");
		GUI.Box(new Rect(statsX, statsY + rowHeight*2 + rowGap*2, statsWidth, rowHeight), "");
		GUI.Box(new Rect(statsX, statsY + rowHeight*3 + rowGap*3, statsWidth, rowHeight), "");
		GUI.Box(new Rect(statsX, statsY + rowHeight*4 + rowGap*4, statsWidth, rowHeight), "");
		GUI.Box(new Rect(statsX, statsY + rowHeight*5 + rowGap*5, statsWidth, rowHeight), "");
		GUI.Box(new Rect(statsX, statsY + rowHeight*6 + rowGap*6, statsWidth, rowHeight + midRowSizeIncrease), "");
		GUI.color = new Color(1f, 1f, 1f, 1f * statsScreenOpacity);
		
				
		
		// puma heads at left of rows
		
		float textureX;
		float textureY;
		float textureHeight;
		float textureWidth;
		Texture2D headshotTexture;
		
		GUI.color = new Color(1f, 1f, 1f, 0.85f * statsScreenOpacity);
		
		float downShift = rowHeight * 0.06f;
		float backgroundInset = rowHeight * 0.05f;
		float headSize = 0.095f;
		float headshotOpacity = 0.97f;
		
		textureX = statsX + statsWidth * 0.068f;

		// textures 1-6
		headshotTexture = (guiManager.selectedPuma == 0) ? closeup1OutlineTexture : closeup1Texture;
		textureY = downShift + statsY + rowHeight*0 + rowGap*0;
		textureHeight = statsHeight * headSize;
		textureWidth = headshotTexture.width * (textureHeight / headshotTexture.height);
		if (scoringSystem.GetPumaHealth(0) <= 0f)
			GUI.color = deadPumaHeadshotColor;
		//else if (scoringSystem.GetPumaHealth(0) >= 1f)
			//GUI.color = fullHealthPumaHeadshotColor;
		else
			GUI.color = new Color(1f, 1f, 1f, headshotOpacity * statsScreenOpacity);
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), headshotTexture);
		GUI.color = new Color(1f, 1f, 1f, headshotOpacity * statsScreenOpacity);

		headshotTexture = (guiManager.selectedPuma == 1) ? closeup2OutlineTexture : closeup2Texture;
		textureY = downShift + statsY + rowHeight*1 + rowGap*1;
		textureHeight = statsHeight * headSize;
		textureWidth = headshotTexture.width * (textureHeight / headshotTexture.height);
		if (scoringSystem.GetPumaHealth(1) <= 0f)
			GUI.color = deadPumaHeadshotColor;
		//else if (scoringSystem.GetPumaHealth(1) >= 1f)
			//GUI.color = fullHealthPumaHeadshotColor;
		else
			GUI.color = new Color(1f, 1f, 1f, headshotOpacity * statsScreenOpacity);
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), headshotTexture);
		GUI.color = new Color(1f, 1f, 1f, headshotOpacity * statsScreenOpacity);
		
		headshotTexture = (guiManager.selectedPuma == 2) ? closeup3OutlineTexture : closeup3Texture;
		textureY = downShift + statsY + rowHeight*2 + rowGap*2;
		textureHeight = statsHeight * headSize;
		textureWidth = headshotTexture.width * (textureHeight / headshotTexture.height);
		if (scoringSystem.GetPumaHealth(2) <= 0f)
			GUI.color = deadPumaHeadshotColor;
		//else if (scoringSystem.GetPumaHealth(2) >= 1f)
			//GUI.color = fullHealthPumaHeadshotColor;
		else
			GUI.color = new Color(1f, 1f, 1f, headshotOpacity * statsScreenOpacity);
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), headshotTexture);
		GUI.color = new Color(1f, 1f, 1f, headshotOpacity * statsScreenOpacity);
		
		headshotTexture = (guiManager.selectedPuma == 3) ? closeup4OutlineTexture : closeup4Texture;
		textureY = downShift + statsY + rowHeight*3 + rowGap*3;
		textureHeight = statsHeight * headSize;
		textureWidth = headshotTexture.width * (textureHeight / headshotTexture.height);
		if (scoringSystem.GetPumaHealth(3) <= 0f)
			GUI.color = deadPumaHeadshotColor;
		//else if (scoringSystem.GetPumaHealth(3) >= 1f)
			//GUI.color = fullHealthPumaHeadshotColor;
		else
			GUI.color = new Color(1f, 1f, 1f, headshotOpacity * statsScreenOpacity);
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), headshotTexture);
		GUI.color = new Color(1f, 1f, 1f, headshotOpacity * statsScreenOpacity);
		
		headshotTexture = (guiManager.selectedPuma == 4) ? closeup5OutlineTexture : closeup5Texture;
		textureY = downShift + statsY + rowHeight*4 + rowGap*4;
		textureHeight = statsHeight * headSize;
		textureWidth = headshotTexture.width * (textureHeight / headshotTexture.height);
		if (scoringSystem.GetPumaHealth(4) <= 0f)
			GUI.color = deadPumaHeadshotColor;
		//else if (scoringSystem.GetPumaHealth(4) >= 1f)
			//GUI.color = fullHealthPumaHeadshotColor;
		else
			GUI.color = new Color(1f, 1f, 1f, headshotOpacity * statsScreenOpacity);
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), headshotTexture);
		GUI.color = new Color(1f, 1f, 1f, headshotOpacity * statsScreenOpacity);
		
		headshotTexture = (guiManager.selectedPuma == 5) ? closeup6OutlineTexture : closeup6Texture;
		textureY = downShift + statsY + rowHeight*5 + rowGap*5;
		textureHeight = statsHeight * headSize;
		textureWidth = headshotTexture.width * (textureHeight / headshotTexture.height);
		if (scoringSystem.GetPumaHealth(5) <= 0f)
			GUI.color = deadPumaHeadshotColor;
		//else if (scoringSystem.GetPumaHealth(5) >= 1f)
			//GUI.color = fullHealthPumaHeadshotColor;
		else
			GUI.color = new Color(1f, 1f, 1f, headshotOpacity * statsScreenOpacity);
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), headshotTexture);
		GUI.color = new Color(1f, 1f, 1f, headshotOpacity * statsScreenOpacity);
		
	

		// puma names and info

		GUI.color = new Color(1f, 1f, 1f, 1f * statsScreenOpacity);
		
		float bigTextFont = 0.017f;
		float smallTextFont = 0.0115f;
		float textX = statsX + statsWidth * 0.02f;		
		float textWidth = statsWidth * 0.03f;
		downShift = rowHeight * 0.2f;

		int pumaNum;
		float textY;

		Color unselectedTextColor = new Color(0.85f, 0.74f, 0.5f, 0.85f);	
		Color selectedTextColor = new Color(0.72f, 0.45f, 0f, 1f);
		//selectedTextColor = unselectedTextColor;
		fullHealthPumaTextColor = unselectedTextColor;

		pumaNum = 0;
		style.normal.textColor = (guiManager.selectedPuma == pumaNum) ? selectedTextColor : ((scoringSystem.GetPumaHealth(pumaNum) <= 0f) ? deadPumaTextColor : ((scoringSystem.GetPumaHealth(pumaNum) >= 1f) ? fullHealthPumaTextColor : unselectedTextColor));
		style.fontSize = (int)(overlayRect.width * bigTextFont * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		textY = statsY + downShift + rowHeight*0 + rowGap*0;
		GUI.Button(new Rect(textX, textY, textWidth, rowHeight), "Eric", style);
		style.alignment = TextAnchor.MiddleCenter;	
		style.normal.textColor = (guiManager.selectedPuma == pumaNum) ? selectedTextColor : ((scoringSystem.GetPumaHealth(pumaNum) <= 0f) ? deadPumaTextColor : ((scoringSystem.GetPumaHealth(pumaNum) >= 1f) ? fullHealthPumaTextColor : unselectedTextColor));
		style.fontSize = (int)(overlayRect.width * smallTextFont * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		textY += statsHeight * 0.045f;
		GUI.Button(new Rect(textX - statsWidth * 0.003f, textY, textWidth, rowHeight), "2 - M", style);

		pumaNum = 1;
		style.normal.textColor = (guiManager.selectedPuma == pumaNum) ? selectedTextColor : ((scoringSystem.GetPumaHealth(pumaNum) <= 0f) ? deadPumaTextColor : ((scoringSystem.GetPumaHealth(pumaNum) >= 1f) ? fullHealthPumaTextColor : unselectedTextColor));
		style.fontSize = (int)(overlayRect.width * bigTextFont * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		textY = statsY + downShift + rowHeight*1 + rowGap*1;
		GUI.Button(new Rect(textX, textY, textWidth, rowHeight), "Palo", style);
		style.alignment = TextAnchor.MiddleCenter;	
		style.normal.textColor = (guiManager.selectedPuma == pumaNum) ? selectedTextColor : ((scoringSystem.GetPumaHealth(pumaNum) <= 0f) ? deadPumaTextColor : ((scoringSystem.GetPumaHealth(pumaNum) >= 1f) ? fullHealthPumaTextColor : unselectedTextColor));
		style.fontSize = (int)(overlayRect.width * smallTextFont * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		textY += statsHeight * 0.045f;
		GUI.Button(new Rect(textX - statsWidth * 0.003f, textY, textWidth, rowHeight), "2 - F", style);

		pumaNum = 2;
		style.normal.textColor = (guiManager.selectedPuma == pumaNum) ? selectedTextColor : ((scoringSystem.GetPumaHealth(pumaNum) <= 0f) ? deadPumaTextColor : ((scoringSystem.GetPumaHealth(pumaNum) >= 1f) ? fullHealthPumaTextColor : unselectedTextColor));
		style.fontSize = (int)(overlayRect.width * bigTextFont * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		textY = statsY + downShift + rowHeight*2 + rowGap*2;
		GUI.Button(new Rect(textX, textY, textWidth, rowHeight), "Mitch", style);
		style.alignment = TextAnchor.MiddleCenter;	
		style.normal.textColor = (guiManager.selectedPuma == pumaNum) ? selectedTextColor : ((scoringSystem.GetPumaHealth(pumaNum) <= 0f) ? deadPumaTextColor : ((scoringSystem.GetPumaHealth(pumaNum) >= 1f) ? fullHealthPumaTextColor : unselectedTextColor));
		style.fontSize = (int)(overlayRect.width * smallTextFont * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		textY += statsHeight * 0.045f;
		GUI.Button(new Rect(textX - statsWidth * 0.003f, textY, textWidth, rowHeight), "5 - M", style);

		pumaNum = 3;
		style.normal.textColor = (guiManager.selectedPuma == pumaNum) ? selectedTextColor : ((scoringSystem.GetPumaHealth(pumaNum) <= 0f) ? deadPumaTextColor : ((scoringSystem.GetPumaHealth(pumaNum) >= 1f) ? fullHealthPumaTextColor : unselectedTextColor));
		style.fontSize = (int)(overlayRect.width * bigTextFont * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		textY = statsY + downShift + rowHeight*3 + rowGap*3;
		GUI.Button(new Rect(textX, textY, textWidth, rowHeight), "Trish", style);
		style.alignment = TextAnchor.MiddleCenter;	
		style.normal.textColor = (guiManager.selectedPuma == pumaNum) ? selectedTextColor : ((scoringSystem.GetPumaHealth(pumaNum) <= 0f) ? deadPumaTextColor : ((scoringSystem.GetPumaHealth(pumaNum) >= 1f) ? fullHealthPumaTextColor : unselectedTextColor));
		style.fontSize = (int)(overlayRect.width * smallTextFont * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		textY += statsHeight * 0.045f;
		GUI.Button(new Rect(textX - statsWidth * 0.003f, textY, textWidth, rowHeight), "5 - F", style);

		pumaNum = 4;
		style.normal.textColor = (guiManager.selectedPuma == pumaNum) ? selectedTextColor : ((scoringSystem.GetPumaHealth(pumaNum) <= 0f) ? deadPumaTextColor : ((scoringSystem.GetPumaHealth(pumaNum) >= 1f) ? fullHealthPumaTextColor : unselectedTextColor));
		style.fontSize = (int)(overlayRect.width * bigTextFont * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		textY = statsY + downShift + rowHeight*4 + rowGap*4;
		GUI.Button(new Rect(textX, textY, textWidth, rowHeight), "Liam", style);
		style.alignment = TextAnchor.MiddleCenter;	
		style.normal.textColor = (guiManager.selectedPuma == pumaNum) ? selectedTextColor : ((scoringSystem.GetPumaHealth(pumaNum) <= 0f) ? deadPumaTextColor : ((scoringSystem.GetPumaHealth(pumaNum) >= 1f) ? fullHealthPumaTextColor : unselectedTextColor));
		style.fontSize = (int)(overlayRect.width * smallTextFont * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		textY += statsHeight * 0.045f;
		GUI.Button(new Rect(textX - statsWidth * 0.003f, textY, textWidth, rowHeight), "8 - M", style);

		pumaNum = 5;
		style.normal.textColor = (guiManager.selectedPuma == pumaNum) ? selectedTextColor : ((scoringSystem.GetPumaHealth(pumaNum) <= 0f) ? deadPumaTextColor : ((scoringSystem.GetPumaHealth(pumaNum) >= 1f) ? fullHealthPumaTextColor : unselectedTextColor));
		style.fontSize = (int)(overlayRect.width * bigTextFont * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		textY = statsY + downShift + rowHeight*5 + rowGap*5;
		GUI.Button(new Rect(textX, textY, textWidth, rowHeight), "Barb", style);
		style.alignment = TextAnchor.MiddleCenter;	
		style.normal.textColor = (guiManager.selectedPuma == pumaNum) ? selectedTextColor : ((scoringSystem.GetPumaHealth(pumaNum) <= 0f) ? deadPumaTextColor : ((scoringSystem.GetPumaHealth(pumaNum) >= 1f) ? fullHealthPumaTextColor : unselectedTextColor));
		style.fontSize = (int)(overlayRect.width * smallTextFont * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		textY += statsHeight * 0.045f;
		GUI.Button(new Rect(textX - statsWidth * 0.003f, textY, textWidth, rowHeight), "8 - F", style);
		
		// population labels
		
		textX = statsX + statsWidth * 0.05f;		
		downShift = rowHeight * 0.15f;
		
		style.normal.textColor = new Color(0.90f, 0.75f, 0.4f, 0.8f);	
		style.fontSize = (int)(overlayRect.width * smallTextFont * fontScale * 1.4f);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		textY = statsY + downShift + rowHeight*6 + rowGap*6;
		GUI.Button(new Rect(textX, textY, textWidth, rowHeight), "Puma", style);
		textY += statsHeight * 0.037f;
		GUI.Button(new Rect(textX, textY, textWidth, rowHeight), "Population", style);
		style.alignment = TextAnchor.MiddleCenter;

		style.normal.textColor = new Color(0.90f, 0.85f, 0.4f, 0.7f);	
		style.fontSize = (int)(overlayRect.width * smallTextFont * fontScale * 1.05f);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		textY += statsHeight * 0.047f;
		GUI.Button(new Rect(textX, textY, textWidth, rowHeight), "overall", style);
		textY += statsHeight * 0.03f;
		GUI.Button(new Rect(textX, textY, textWidth, rowHeight), "results", style);
		style.alignment = TextAnchor.MiddleCenter;
		
		


		// turn-by-turn health display
		
		float verticalBarX = statsX + statsWidth * 0.152f;
		float verticalBarWidth = statsWidth * 0.01f;
		float vertBarXOffset = 0f;
		downShift = rowHeight * 0.1f;
		
		style.normal.textColor = new Color(0.90f, 0.75f, 0.4f, 0.8f);	
		style.fontSize = (int)(overlayRect.width * smallTextFont * fontScale * 1.1f);
		style.fontStyle = FontStyle.Bold;
		style.alignment = TextAnchor.UpperLeft;
		float levelLabelY = statsY - statsHeight * 0.076f;
		float levelLabelWidth = statsWidth * 0.2f;
		int currentLevel = 1;
		GUI.Button(new Rect(verticalBarX + vertBarXOffset + verticalBarWidth * 3.3f, levelLabelY, levelLabelWidth, rowHeight), "Level " + currentLevel, style);
		currentLevel++;
		
		float loweredBarOpactity = statsScreenOpacity * 0.4f;
		float loweredCenterOpactity = 0.35f;
			
		float puma1Health = 0.5f;
		float puma2Health = 0.5f;
		float puma3Health = 0.5f;
		float puma4Health = 0.5f;
		float puma5Health = 0.5f;
		float puma6Health = 0.5f;
		float pumaAllHealth = 0.5f;

		vertBarXOffset = 0f;
		DrawVertDisplayBar(puma1Health, statsScreenOpacity, 1f, verticalBarX + vertBarXOffset, statsY + downShift + rowHeight*0 + rowGap*0, verticalBarWidth, rowHeight * 0.8f, scoringSystem.GetPumaHealth(0) <= 0f ? true : false);
		DrawVertDisplayBar(puma2Health, statsScreenOpacity, 1f, verticalBarX + vertBarXOffset, statsY + downShift + rowHeight*1 + rowGap*1, verticalBarWidth, rowHeight * 0.8f, scoringSystem.GetPumaHealth(1) <= 0f ? true : false);
		DrawVertDisplayBar(puma3Health, statsScreenOpacity, 1f, verticalBarX + vertBarXOffset, statsY + downShift + rowHeight*2 + rowGap*2, verticalBarWidth, rowHeight * 0.8f, scoringSystem.GetPumaHealth(2) <= 0f ? true : false);
		DrawVertDisplayBar(puma4Health, statsScreenOpacity, 1f, verticalBarX + vertBarXOffset, statsY + downShift + rowHeight*3 + rowGap*3, verticalBarWidth, rowHeight * 0.8f, scoringSystem.GetPumaHealth(3) <= 0f ? true : false);
		DrawVertDisplayBar(puma5Health, statsScreenOpacity, 1f, verticalBarX + vertBarXOffset, statsY + downShift + rowHeight*4 + rowGap*4, verticalBarWidth, rowHeight * 0.8f, scoringSystem.GetPumaHealth(4) <= 0f ? true : false);
		DrawVertDisplayBar(puma6Health, statsScreenOpacity, 1f, verticalBarX + vertBarXOffset, statsY + downShift + rowHeight*5 + rowGap*5, verticalBarWidth, rowHeight * 0.8f, scoringSystem.GetPumaHealth(5) <= 0f ? true : false);
		DrawVertDisplayBar(pumaAllHealth, statsScreenOpacity, 1f, verticalBarX + vertBarXOffset, statsY + downShift + rowHeight*6 + rowGap*6, verticalBarWidth, rowHeight * 0.8f + midRowSizeIncrease, false);
		

		Color lColor = new Color(0f, 0f, 0f, 0.7f);
		Color rColor = new Color(0.5f, 0.49f, 0.47f, 0.25f);
		float divWidth = overlayRect.width * 0.002f;


		vertBarXOffset += verticalBarWidth * 1.5f;
		vertBarXOffset += verticalBarWidth * 0.65f;
		//guiUtils.DrawRect(new Rect(verticalBarX + vertBarXOffset - divWidth, statsY - topCompression, divWidth, topCompression - rowGap), lColor);	
		//guiUtils.DrawRect(new Rect(verticalBarX + vertBarXOffset, statsY - topCompression, divWidth, topCompression - rowGap), rColor);	
		guiUtils.DrawRect(new Rect(verticalBarX + vertBarXOffset - divWidth, statsY + rowHeight*0 + rowGap*0, divWidth, rowHeight), lColor);	
		guiUtils.DrawRect(new Rect(verticalBarX + vertBarXOffset, statsY + rowHeight*0 + rowGap*0, divWidth, rowHeight), rColor);	
		guiUtils.DrawRect(new Rect(verticalBarX + vertBarXOffset - divWidth, statsY + rowHeight*1 + rowGap*1, divWidth, rowHeight), lColor);	
		guiUtils.DrawRect(new Rect(verticalBarX + vertBarXOffset, statsY + rowHeight*1 + rowGap*1, divWidth, rowHeight), rColor);	
		guiUtils.DrawRect(new Rect(verticalBarX + vertBarXOffset - divWidth, statsY + rowHeight*2 + rowGap*2, divWidth, rowHeight), lColor);	
		guiUtils.DrawRect(new Rect(verticalBarX + vertBarXOffset, statsY + rowHeight*2 + rowGap*2, divWidth, rowHeight), rColor);	
		guiUtils.DrawRect(new Rect(verticalBarX + vertBarXOffset - divWidth, statsY + rowHeight*3 + rowGap*3, divWidth, rowHeight), lColor);	
		guiUtils.DrawRect(new Rect(verticalBarX + vertBarXOffset, statsY + rowHeight*3 + rowGap*3, divWidth, rowHeight), rColor);	
		guiUtils.DrawRect(new Rect(verticalBarX + vertBarXOffset - divWidth, statsY + rowHeight*4 + rowGap*4, divWidth, rowHeight), lColor);	
		guiUtils.DrawRect(new Rect(verticalBarX + vertBarXOffset, statsY + rowHeight*4 + rowGap*4, divWidth, rowHeight), rColor);	
		guiUtils.DrawRect(new Rect(verticalBarX + vertBarXOffset - divWidth, statsY + rowHeight*5 + rowGap*5, divWidth, rowHeight), lColor);	
		guiUtils.DrawRect(new Rect(verticalBarX + vertBarXOffset, statsY + rowHeight*5 + rowGap*5, divWidth, rowHeight), rColor);	
		guiUtils.DrawRect(new Rect(verticalBarX + vertBarXOffset - divWidth, statsY + rowHeight*6 + rowGap*6, divWidth, rowHeight + midRowSizeIncrease), lColor);	
		guiUtils.DrawRect(new Rect(verticalBarX + vertBarXOffset, statsY + rowHeight*6 + rowGap*6, divWidth, rowHeight + midRowSizeIncrease), rColor);	
		vertBarXOffset -= verticalBarWidth * 0.3f;


		int huntCount = scoringSystem.GetHuntCount();
		int currentHunt = huntCount > 44 ? huntCount - 44 : 0;
		int huntsThisLevel = 0;
		
		
		for (int i = currentHunt; i < huntCount; i++) {
		
			int huntStatsSelectedPuma = scoringSystem.GetHuntStatsSelectedPuma(i);
			huntsThisLevel += 1;
			
			switch (huntStatsSelectedPuma) {
			case 0:
				puma1Health = scoringSystem.GetHuntStatsPumaHealth(i);
				break;
			case 1:
				puma2Health = scoringSystem.GetHuntStatsPumaHealth(i);
				break;
			case 2:
				puma3Health = scoringSystem.GetHuntStatsPumaHealth(i);
				break;
			case 3:
				puma4Health = scoringSystem.GetHuntStatsPumaHealth(i);
				break;
			case 4:
				puma5Health = scoringSystem.GetHuntStatsPumaHealth(i);
				break;
			case 5:
				puma6Health = scoringSystem.GetHuntStatsPumaHealth(i);
				break;
			case 6:
				vertBarXOffset += verticalBarWidth * 1.5f;
				vertBarXOffset += verticalBarWidth * 0.65f;
				if (huntsThisLevel == 4)
					vertBarXOffset += verticalBarWidth * 0.7f;
				huntsThisLevel = 0;
				GUI.Button(new Rect(verticalBarX + vertBarXOffset + verticalBarWidth * 1.2f, levelLabelY, levelLabelWidth, rowHeight), "Level " + currentLevel, style);
				currentLevel++;
				guiUtils.DrawRect(new Rect(verticalBarX + vertBarXOffset - divWidth, statsY - topCompression, divWidth, topCompression - rowGap), lColor);	
				guiUtils.DrawRect(new Rect(verticalBarX + vertBarXOffset, statsY - topCompression, divWidth, topCompression - rowGap), rColor);	
				guiUtils.DrawRect(new Rect(verticalBarX + vertBarXOffset - divWidth, statsY + rowHeight*0 + rowGap*0, divWidth, rowHeight), lColor);	
				guiUtils.DrawRect(new Rect(verticalBarX + vertBarXOffset, statsY + rowHeight*0 + rowGap*0, divWidth, rowHeight), rColor);	
				guiUtils.DrawRect(new Rect(verticalBarX + vertBarXOffset - divWidth, statsY + rowHeight*1 + rowGap*1, divWidth, rowHeight), lColor);	
				guiUtils.DrawRect(new Rect(verticalBarX + vertBarXOffset, statsY + rowHeight*1 + rowGap*1, divWidth, rowHeight), rColor);	
				guiUtils.DrawRect(new Rect(verticalBarX + vertBarXOffset - divWidth, statsY + rowHeight*2 + rowGap*2, divWidth, rowHeight), lColor);	
				guiUtils.DrawRect(new Rect(verticalBarX + vertBarXOffset, statsY + rowHeight*2 + rowGap*2, divWidth, rowHeight), rColor);	
				guiUtils.DrawRect(new Rect(verticalBarX + vertBarXOffset - divWidth, statsY + rowHeight*3 + rowGap*3, divWidth, rowHeight), lColor);	
				guiUtils.DrawRect(new Rect(verticalBarX + vertBarXOffset, statsY + rowHeight*3 + rowGap*3, divWidth, rowHeight), rColor);	
				guiUtils.DrawRect(new Rect(verticalBarX + vertBarXOffset - divWidth, statsY + rowHeight*4 + rowGap*4, divWidth, rowHeight), lColor);	
				guiUtils.DrawRect(new Rect(verticalBarX + vertBarXOffset, statsY + rowHeight*4 + rowGap*4, divWidth, rowHeight), rColor);	
				guiUtils.DrawRect(new Rect(verticalBarX + vertBarXOffset - divWidth, statsY + rowHeight*5 + rowGap*5, divWidth, rowHeight), lColor);	
				guiUtils.DrawRect(new Rect(verticalBarX + vertBarXOffset, statsY + rowHeight*5 + rowGap*5, divWidth, rowHeight), rColor);	
				guiUtils.DrawRect(new Rect(verticalBarX + vertBarXOffset - divWidth, statsY + rowHeight*6 + rowGap*6, divWidth, rowHeight + midRowSizeIncrease), lColor);	
				guiUtils.DrawRect(new Rect(verticalBarX + vertBarXOffset, statsY + rowHeight*6 + rowGap*6, divWidth, rowHeight + midRowSizeIncrease), rColor);	
				vertBarXOffset -= verticalBarWidth * 0.3f;
				continue;
			}
		
			pumaAllHealth = (puma1Health + puma2Health + puma3Health + puma4Health + puma5Health + puma6Health) / 6f;
					
			vertBarXOffset += verticalBarWidth * 1.3f;
			
			
			float barOpacity;
			float centerOpacity;		

			barOpacity = huntStatsSelectedPuma == 0 ? statsScreenOpacity : (puma1Health > 0f ? loweredBarOpactity : 0f);
			centerOpacity = huntStatsSelectedPuma == 0 ? 1f : loweredCenterOpactity;
			if (barOpacity != 0f)
				DrawVertDisplayBar(puma1Health, barOpacity, centerOpacity, verticalBarX + vertBarXOffset, statsY + downShift + rowHeight*0 + rowGap*0, verticalBarWidth, rowHeight * 0.8f, scoringSystem.GetPumaHealth(0) <= 0f ? true : false);		

			barOpacity = huntStatsSelectedPuma == 1 ? statsScreenOpacity : (puma2Health > 0f ? loweredBarOpactity : 0f);
			centerOpacity = huntStatsSelectedPuma == 1 ? 1f : loweredCenterOpactity;		
			if (barOpacity != 0f)
				DrawVertDisplayBar(puma2Health, barOpacity, centerOpacity, verticalBarX + vertBarXOffset, statsY + downShift + rowHeight*1 + rowGap*1, verticalBarWidth, rowHeight * 0.8f, scoringSystem.GetPumaHealth(1) <= 0f ? true : false);			

			barOpacity = huntStatsSelectedPuma == 2 ? statsScreenOpacity : (puma3Health > 0f ? loweredBarOpactity : 0f);
			centerOpacity = huntStatsSelectedPuma == 2 ? 1f : loweredCenterOpactity;		
			if (barOpacity != 0f)
				DrawVertDisplayBar(puma3Health, barOpacity, centerOpacity, verticalBarX + vertBarXOffset, statsY + downShift + rowHeight*2 + rowGap*2, verticalBarWidth, rowHeight * 0.8f, scoringSystem.GetPumaHealth(2) <= 0f ? true : false);		

			barOpacity = huntStatsSelectedPuma == 3 ? statsScreenOpacity : (puma4Health > 0f ? loweredBarOpactity : 0f);
			centerOpacity = huntStatsSelectedPuma == 3 ? 1f : loweredCenterOpactity;		
			if (barOpacity != 0f)
				DrawVertDisplayBar(puma4Health, barOpacity, centerOpacity, verticalBarX + vertBarXOffset, statsY + downShift + rowHeight*3 + rowGap*3, verticalBarWidth, rowHeight * 0.8f, scoringSystem.GetPumaHealth(3) <= 0f ? true : false);

			barOpacity = huntStatsSelectedPuma == 4 ? statsScreenOpacity : (puma5Health > 0f ? loweredBarOpactity : 0f);
			centerOpacity = huntStatsSelectedPuma == 4 ? 1f : loweredCenterOpactity;		
			if (barOpacity != 0f)
				DrawVertDisplayBar(puma5Health, barOpacity, centerOpacity, verticalBarX + vertBarXOffset, statsY + downShift + rowHeight*4 + rowGap*4, verticalBarWidth, rowHeight * 0.8f, scoringSystem.GetPumaHealth(4) <= 0f ? true : false);

			barOpacity = huntStatsSelectedPuma == 5 ? statsScreenOpacity : (puma6Health > 0f ? loweredBarOpactity : 0f);
			centerOpacity = huntStatsSelectedPuma == 5 ? 1f : loweredCenterOpactity;		
			if (barOpacity != 0f)
				DrawVertDisplayBar(puma6Health, barOpacity, centerOpacity, verticalBarX + vertBarXOffset, statsY + downShift + rowHeight*5 + rowGap*5, verticalBarWidth, rowHeight * 0.8f, scoringSystem.GetPumaHealth(5) <= 0f ? true : false);

			DrawVertDisplayBar(pumaAllHealth, statsScreenOpacity, 1f, verticalBarX + vertBarXOffset, statsY + downShift + rowHeight*6 + rowGap*6, verticalBarWidth, rowHeight * 0.8f + midRowSizeIncrease, false);
		}
		

		// prey-specific vertical display bars
		
		float verticalBarsX = statsX + statsWidth * 0.795f;
		float verticalBarsWidth = statsWidth * 0.07f;
		downShift = rowHeight * 0.1f;
		DrawDisplayBarsVert(0, statsScreenOpacity, verticalBarsX, statsY + downShift + rowHeight*0 + rowGap*0, verticalBarsWidth, rowHeight * 0.8f);
		DrawDisplayBarsVert(1, statsScreenOpacity, verticalBarsX, statsY + downShift + rowHeight*1 + rowGap*1, verticalBarsWidth, rowHeight * 0.8f);
		DrawDisplayBarsVert(2, statsScreenOpacity, verticalBarsX, statsY + downShift + rowHeight*2 + rowGap*2, verticalBarsWidth, rowHeight * 0.8f);
		DrawDisplayBarsVert(3, statsScreenOpacity, verticalBarsX, statsY + downShift + rowHeight*3 + rowGap*3, verticalBarsWidth, rowHeight * 0.8f);
		DrawDisplayBarsVert(4, statsScreenOpacity, verticalBarsX, statsY + downShift + rowHeight*4 + rowGap*4, verticalBarsWidth, rowHeight * 0.8f);
		DrawDisplayBarsVert(5, statsScreenOpacity, verticalBarsX, statsY + downShift + rowHeight*5 + rowGap*5, verticalBarsWidth, rowHeight * 0.8f);

		
	
		// deer head labels above prey-specific vertical bars
	
		Texture2D displayHeadTexture;
		textureWidth = statsWidth * 0.025f;
		float deerHeadsY = statsY - statsHeight * 0.1f;

		displayHeadTexture = buckHeadTexture;
		textureHeight = displayHeadTexture.height * (textureWidth / displayHeadTexture.width);
		GUI.DrawTexture(new Rect(verticalBarsX - statsWidth * 0.004f, deerHeadsY, textureWidth, textureHeight), displayHeadTexture);
		displayHeadTexture = doeHeadTexture;
		deerHeadsY -= statsHeight * 0.005f;
		textureHeight = displayHeadTexture.height * (textureWidth / displayHeadTexture.width);
		GUI.DrawTexture(new Rect(verticalBarsX + statsWidth * 0.023f, deerHeadsY, textureWidth, textureHeight), displayHeadTexture);
		displayHeadTexture = fawnHeadTexture;
		deerHeadsY += statsHeight * 0.002f;
		textureHeight = displayHeadTexture.height * (textureWidth / displayHeadTexture.width);
		GUI.DrawTexture(new Rect(verticalBarsX + statsWidth * 0.053f, deerHeadsY, textureWidth, textureHeight), displayHeadTexture);
		
		
		// health bars
		
		float healthBarsX = statsX + statsWidth * 0.89f;
		float healthBarsWidth = statsWidth * 0.1f;
		downShift = rowHeight * 0.6f;

		guiComponents.DrawPumaHealthBar(0, statsScreenOpacity, healthBarsX, statsY + downShift + rowHeight*0 + rowGap*0, healthBarsWidth, rowHeight * 0.32f, true, true);
		guiComponents.DrawPumaHealthBar(1, statsScreenOpacity, healthBarsX, statsY + downShift + rowHeight*1 + rowGap*1, healthBarsWidth, rowHeight * 0.32f, true, true);
		guiComponents.DrawPumaHealthBar(2, statsScreenOpacity, healthBarsX, statsY + downShift + rowHeight*2 + rowGap*2, healthBarsWidth, rowHeight * 0.32f, true, true);
		guiComponents.DrawPumaHealthBar(3, statsScreenOpacity, healthBarsX, statsY + downShift + rowHeight*3 + rowGap*3, healthBarsWidth, rowHeight * 0.32f, true, true);
		guiComponents.DrawPumaHealthBar(4, statsScreenOpacity, healthBarsX, statsY + downShift + rowHeight*4 + rowGap*4, healthBarsWidth, rowHeight * 0.32f, true, true);
		guiComponents.DrawPumaHealthBar(5, statsScreenOpacity, healthBarsX, statsY + downShift + rowHeight*5 + rowGap*5, healthBarsWidth, rowHeight * 0.32f, true, true);



		// population bar

		float populationBarX = statsX + statsWidth * 0.795f;
		float populationBarWidth = statsWidth * 0.195f;
		downShift = rowHeight * 0.75f;
		guiComponents.DrawPopulationHealthBar(statsScreenOpacity, populationBarX, statsY + downShift + rowHeight*6 + rowGap*6, populationBarWidth, rowHeight * 0.6f, false, true, true);


		
		// ======================================
		
		// DIVIDERS

		Color leftColor = new Color(0f, 0f, 0f, 0.7f);
		Color rightColor = new Color(0.5f, 0.49f, 0.47f, 0.25f);
		float dividerWidth = overlayRect.width * 0.003f;
		float dividerX = statsX + statsWidth * 0.14f;

		// vertical - left	
		guiUtils.DrawRect(new Rect(dividerX - dividerWidth, statsY + rowHeight*0 + rowGap*0, dividerWidth, rowHeight), leftColor);	
		guiUtils.DrawRect(new Rect(dividerX, statsY + rowHeight*0 + rowGap*0, dividerWidth, rowHeight), rightColor);	
		guiUtils.DrawRect(new Rect(dividerX - dividerWidth, statsY + rowHeight*1 + rowGap*1, dividerWidth, rowHeight), leftColor);	
		guiUtils.DrawRect(new Rect(dividerX, statsY + rowHeight*1 + rowGap*1, dividerWidth, rowHeight), rightColor);	
		guiUtils.DrawRect(new Rect(dividerX - dividerWidth, statsY + rowHeight*2 + rowGap*2, dividerWidth, rowHeight), leftColor);	
		guiUtils.DrawRect(new Rect(dividerX, statsY + rowHeight*2 + rowGap*2, dividerWidth, rowHeight), rightColor);	
		guiUtils.DrawRect(new Rect(dividerX - dividerWidth, statsY + rowHeight*3 + rowGap*3, dividerWidth, rowHeight), leftColor);	
		guiUtils.DrawRect(new Rect(dividerX, statsY + rowHeight*3 + rowGap*3, dividerWidth, rowHeight), rightColor);	
		guiUtils.DrawRect(new Rect(dividerX - dividerWidth, statsY + rowHeight*4 + rowGap*4, dividerWidth, rowHeight), leftColor);	
		guiUtils.DrawRect(new Rect(dividerX, statsY + rowHeight*4 + rowGap*4, dividerWidth, rowHeight), rightColor);	
		guiUtils.DrawRect(new Rect(dividerX - dividerWidth, statsY + rowHeight*5 + rowGap*5, dividerWidth, rowHeight), leftColor);	
		guiUtils.DrawRect(new Rect(dividerX, statsY + rowHeight*5 + rowGap*5, dividerWidth, rowHeight), rightColor);	
		guiUtils.DrawRect(new Rect(dividerX - dividerWidth, statsY + rowHeight*6 + rowGap*6, dividerWidth, rowHeight + midRowSizeIncrease), leftColor);	
		guiUtils.DrawRect(new Rect(dividerX, statsY + rowHeight*6 + rowGap*6, dividerWidth, rowHeight + midRowSizeIncrease), rightColor);	


		// vertical - right	
		dividerX = statsX + statsWidth * 0.88f;
		guiUtils.DrawRect(new Rect(dividerX - dividerWidth, statsY + rowHeight*0 + rowGap*0, dividerWidth, rowHeight), leftColor);	
		guiUtils.DrawRect(new Rect(dividerX, statsY + rowHeight*0 + rowGap*0, dividerWidth, rowHeight), rightColor);	
		guiUtils.DrawRect(new Rect(dividerX - dividerWidth, statsY + rowHeight*1 + rowGap*1, dividerWidth, rowHeight), leftColor);	
		guiUtils.DrawRect(new Rect(dividerX, statsY + rowHeight*1 + rowGap*1, dividerWidth, rowHeight), rightColor);	
		guiUtils.DrawRect(new Rect(dividerX - dividerWidth, statsY + rowHeight*2 + rowGap*2, dividerWidth, rowHeight), leftColor);	
		guiUtils.DrawRect(new Rect(dividerX, statsY + rowHeight*2 + rowGap*2, dividerWidth, rowHeight), rightColor);	
		guiUtils.DrawRect(new Rect(dividerX - dividerWidth, statsY + rowHeight*3 + rowGap*3, dividerWidth, rowHeight), leftColor);	
		guiUtils.DrawRect(new Rect(dividerX, statsY + rowHeight*3 + rowGap*3, dividerWidth, rowHeight), rightColor);	
		guiUtils.DrawRect(new Rect(dividerX - dividerWidth, statsY + rowHeight*4 + rowGap*4, dividerWidth, rowHeight), leftColor);	
		guiUtils.DrawRect(new Rect(dividerX, statsY + rowHeight*4 + rowGap*4, dividerWidth, rowHeight), rightColor);	
		guiUtils.DrawRect(new Rect(dividerX - dividerWidth, statsY + rowHeight*5 + rowGap*5, dividerWidth, rowHeight), leftColor);	
		guiUtils.DrawRect(new Rect(dividerX, statsY + rowHeight*5 + rowGap*5, dividerWidth, rowHeight), rightColor);	
		//guiUtils.DrawRect(new Rect(dividerX - dividerWidth, statsY + rowHeight*6 + rowGap*6, dividerWidth, rowHeight + midRowSizeIncrease), leftColor);	
		//guiUtils.DrawRect(new Rect(dividerX, statsY + rowHeight*6 + rowGap*6, dividerWidth, rowHeight + midRowSizeIncrease), rightColor);	


		// vertical - 2nd from right	
		dividerX = verticalBarsX - statsWidth * 0.0145f;
		guiUtils.DrawRect(new Rect(dividerX - dividerWidth, statsY + rowHeight*0 + rowGap*0, dividerWidth, rowHeight), leftColor);	
		guiUtils.DrawRect(new Rect(dividerX, statsY + rowHeight*0 + rowGap*0, dividerWidth, rowHeight), rightColor);	
		guiUtils.DrawRect(new Rect(dividerX - dividerWidth, statsY + rowHeight*1 + rowGap*1, dividerWidth, rowHeight), leftColor);	
		guiUtils.DrawRect(new Rect(dividerX, statsY + rowHeight*1 + rowGap*1, dividerWidth, rowHeight), rightColor);	
		guiUtils.DrawRect(new Rect(dividerX - dividerWidth, statsY + rowHeight*2 + rowGap*2, dividerWidth, rowHeight), leftColor);	
		guiUtils.DrawRect(new Rect(dividerX, statsY + rowHeight*2 + rowGap*2, dividerWidth, rowHeight), rightColor);	
		guiUtils.DrawRect(new Rect(dividerX - dividerWidth, statsY + rowHeight*3 + rowGap*3, dividerWidth, rowHeight), leftColor);	
		guiUtils.DrawRect(new Rect(dividerX, statsY + rowHeight*3 + rowGap*3, dividerWidth, rowHeight), rightColor);	
		guiUtils.DrawRect(new Rect(dividerX - dividerWidth, statsY + rowHeight*4 + rowGap*4, dividerWidth, rowHeight), leftColor);	
		guiUtils.DrawRect(new Rect(dividerX, statsY + rowHeight*4 + rowGap*4, dividerWidth, rowHeight), rightColor);	
		guiUtils.DrawRect(new Rect(dividerX - dividerWidth, statsY + rowHeight*5 + rowGap*5, dividerWidth, rowHeight), leftColor);	
		guiUtils.DrawRect(new Rect(dividerX, statsY + rowHeight*5 + rowGap*5, dividerWidth, rowHeight), rightColor);	
		guiUtils.DrawRect(new Rect(dividerX - dividerWidth, statsY + rowHeight*6 + rowGap*6, dividerWidth, rowHeight + midRowSizeIncrease), leftColor);	
		guiUtils.DrawRect(new Rect(dividerX, statsY + rowHeight*6 + rowGap*6, dividerWidth, rowHeight + midRowSizeIncrease), rightColor);	


		

		
/*		
		float columnCount = 7f;
		float columnGap = statsWidth * 0.02f;
		float midColumnSizeIncrease = columnGap * 2f;
		float columnWidth = (statsWidth - (columnGap * (columnCount-1)) - midColumnSizeIncrease) / columnCount;
		GUI.color = new Color(0f, 0f, 0f, 0.8f * statsScreenOpacity);
		GUI.Box(new Rect(statsX + columnWidth*0 + columnGap*0, statsY, columnWidth, statsHeight), "");
		GUI.Box(new Rect(statsX + columnWidth*1 + columnGap*1, statsY, columnWidth, statsHeight), "");
		GUI.Box(new Rect(statsX + columnWidth*2 + columnGap*2, statsY, columnWidth, statsHeight), "");
		GUI.Box(new Rect(statsX + columnWidth*3 + columnGap*3, statsY, columnWidth + midColumnSizeIncrease, statsHeight), "");
		GUI.Box(new Rect(statsX + columnWidth*4 + columnGap*4 + midColumnSizeIncrease, statsY, columnWidth, statsHeight), "");
		GUI.Box(new Rect(statsX + columnWidth*5 + columnGap*5 + midColumnSizeIncrease, statsY, columnWidth, statsHeight), "");
		GUI.Box(new Rect(statsX + columnWidth*6 + columnGap*6 + midColumnSizeIncrease, statsY, columnWidth, statsHeight), "");
		GUI.color = new Color(0f, 0f, 0f, 0.7f * statsScreenOpacity);
		GUI.Box(new Rect(statsX + columnWidth*0 + columnGap*0, statsY, columnWidth, statsHeight), "");
		GUI.Box(new Rect(statsX + columnWidth*1 + columnGap*1, statsY, columnWidth, statsHeight), "");
		GUI.Box(new Rect(statsX + columnWidth*2 + columnGap*2, statsY, columnWidth, statsHeight), "");
		GUI.color = new Color(0f, 0f, 0f, 0.9f * statsScreenOpacity);
		GUI.Box(new Rect(statsX + columnWidth*3 + columnGap*3, statsY, columnWidth + midColumnSizeIncrease, statsHeight), "");
		GUI.color = new Color(0f, 0f, 0f, 0.8f * statsScreenOpacity);
		GUI.Box(new Rect(statsX + columnWidth*4 + columnGap*4 + midColumnSizeIncrease, statsY, columnWidth, statsHeight), "");
		GUI.color = new Color(0f, 0f, 0f, 0.85f * statsScreenOpacity);
		GUI.Box(new Rect(statsX + columnWidth*5 + columnGap*5 + midColumnSizeIncrease, statsY, columnWidth, statsHeight), "");
		GUI.color = new Color(0f, 0f, 0f, 0.9f * statsScreenOpacity);
		GUI.Box(new Rect(statsX + columnWidth*6 + columnGap*6 + midColumnSizeIncrease, statsY, columnWidth, statsHeight), "");
		GUI.color = new Color(1f, 1f, 1f, 1f * statsScreenOpacity);
	
		
		
		// population bar
		
		float yOffsetForAddingPopulationBar = overlayRect.height * -0.012f;
		float actualstatsScreenOpacity = statsScreenOpacity;
		statsScreenOpacity = statsScreenOpacity * 0.9f;
		GUI.color = new Color(1f, 1f, 1f, 1f * statsScreenOpacity);		
		
		float healthBarX = overlayRect.x + overlayRect.width * 0.04f;
		float healthBarY = overlayRect.y + overlayRect.height * 0.844f + yOffsetForAddingPopulationBar;
		float healthBarWidth = overlayRect.width * 0.92f;
		float healthBarHeight = overlayRect.height * 0.048f;	
		float healthBarLabelWidth = 0f; //healthBarWidth * 0.13f;

		if (healthBarLabelWidth > 0f) {
			GUI.color = new Color(0f, 0f, 0f, 0.8f * statsScreenOpacity);
			GUI.Box(new Rect(healthBarX, healthBarY, healthBarLabelWidth * 0.985f, healthBarHeight), "");
			GUI.Box(new Rect(healthBarX, healthBarY, healthBarLabelWidth * 0.985f, healthBarHeight), "");
			GUI.Box(new Rect(healthBarX + healthBarWidth - healthBarLabelWidth * 0.985f, healthBarY, healthBarLabelWidth * 0.985f, healthBarHeight), "");
			GUI.Box(new Rect(healthBarX + healthBarWidth - healthBarLabelWidth * 0.985f, healthBarY, healthBarLabelWidth * 0.985f, healthBarHeight), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * statsScreenOpacity);
		}

		guiComponents.DrawPopulationHealthBar(statsScreenOpacity, healthBarX + healthBarLabelWidth, healthBarY, healthBarWidth - healthBarLabelWidth * 2f, healthBarHeight, false, true);

		statsScreenOpacity = actualstatsScreenOpacity;
		GUI.color = new Color(1f, 1f, 1f, 1f * statsScreenOpacity);		


		// puma heads at tops of columns
		
		float textureX;
		float textureY;
		float textureHeight;
		float textureWidth;
		Texture2D headshotTexture;
		
		GUI.color = new Color(1f, 1f, 1f, 0.85f * statsScreenOpacity);
		
		float rightShift = columnWidth * 0.18f;
		float backgroundInset = columnWidth * 0.05f;
		float headSize = 0.135f;
		float headshotOpacity = 0.97f;
		
		textureY = statsY + statsHeight * 0.022f;

		// textures 1-6
		headshotTexture = closeup1Texture;
		textureX = rightShift + statsX + columnWidth*0 + columnGap*0;
		textureHeight = statsHeight * headSize;
		textureWidth = headshotTexture.width * (textureHeight / headshotTexture.height);
		if (scoringSystem.GetPumaHealth(0) <= 0f)
			GUI.color = deadPumaHeadshotColor;
		else if (scoringSystem.GetPumaHealth(0) >= 1f)
			GUI.color = fullHealthPumaHeadshotColor;
		else
			GUI.color = new Color(1f, 1f, 1f, headshotOpacity * statsScreenOpacity);
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), headshotTexture);
		GUI.color = new Color(1f, 1f, 1f, headshotOpacity * statsScreenOpacity);
		/////
		headshotTexture = closeup2Texture;
		textureX = rightShift + statsX + columnWidth*1 + columnGap*1;
		textureHeight = statsHeight * headSize;
		textureWidth = headshotTexture.width * (textureHeight / headshotTexture.height);
		if (scoringSystem.GetPumaHealth(1) <= 0f)
			GUI.color = deadPumaHeadshotColor;
		else if (scoringSystem.GetPumaHealth(1) >= 1f)
			GUI.color = fullHealthPumaHeadshotColor;
		else
			GUI.color = new Color(1f, 1f, 1f, headshotOpacity * statsScreenOpacity);
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), headshotTexture);
		GUI.color = new Color(1f, 1f, 1f, headshotOpacity * statsScreenOpacity);
		/////
		headshotTexture = closeup3Texture;
		textureX = rightShift + statsX + columnWidth*2 + columnGap*2;
		textureHeight = statsHeight * headSize;
		textureWidth = headshotTexture.width * (textureHeight / headshotTexture.height);
		if (scoringSystem.GetPumaHealth(2) <= 0f)
			GUI.color = deadPumaHeadshotColor;
		else if (scoringSystem.GetPumaHealth(2) >= 1f)
			GUI.color = fullHealthPumaHeadshotColor;
		else
			GUI.color = new Color(1f, 1f, 1f, headshotOpacity * statsScreenOpacity);
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), headshotTexture);
		GUI.color = new Color(1f, 1f, 1f, headshotOpacity * statsScreenOpacity);
		/////
		headshotTexture = closeup4Texture;
		textureX = rightShift + statsX + columnWidth*4 + columnGap*4 + midColumnSizeIncrease;
		textureHeight = statsHeight * headSize;
		textureWidth = headshotTexture.width * (textureHeight / headshotTexture.height);
		if (scoringSystem.GetPumaHealth(3) <= 0f)
			GUI.color = deadPumaHeadshotColor;
		else if (scoringSystem.GetPumaHealth(3) >= 1f)
			GUI.color = fullHealthPumaHeadshotColor;
		else
			GUI.color = new Color(1f, 1f, 1f, headshotOpacity * statsScreenOpacity);
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), headshotTexture);
		GUI.color = new Color(1f, 1f, 1f, headshotOpacity * statsScreenOpacity);
		/////
		headshotTexture = closeup5Texture;
		textureX = rightShift + statsX + columnWidth*5 + columnGap*5 + midColumnSizeIncrease;
		textureHeight = statsHeight * headSize;
		textureWidth = headshotTexture.width * (textureHeight / headshotTexture.height);
		if (scoringSystem.GetPumaHealth(4) <= 0f)
			GUI.color = deadPumaHeadshotColor;
		else if (scoringSystem.GetPumaHealth(4) >= 1f)
			GUI.color = fullHealthPumaHeadshotColor;
		else
			GUI.color = new Color(1f, 1f, 1f, headshotOpacity * statsScreenOpacity);
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), headshotTexture);
		GUI.color = new Color(1f, 1f, 1f, headshotOpacity * statsScreenOpacity);
		/////
		headshotTexture = closeup6Texture;
		textureX = rightShift + statsX + columnWidth*6 + columnGap*6 + midColumnSizeIncrease;
		textureHeight = statsHeight * headSize;
		textureWidth = headshotTexture.width * (textureHeight / headshotTexture.height);
		if (scoringSystem.GetPumaHealth(5) <= 0f)
			GUI.color = deadPumaHeadshotColor;
		else if (scoringSystem.GetPumaHealth(5) >= 1f)
			GUI.color = fullHealthPumaHeadshotColor;
		else
			GUI.color = new Color(1f, 1f, 1f, headshotOpacity * statsScreenOpacity);
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), headshotTexture);
		GUI.color = new Color(1f, 1f, 1f, 1f * statsScreenOpacity);

	
		

		// puma names

		GUI.color = new Color(1f, 1f, 1f, 1f * statsScreenOpacity);
		
		float bigTextFont = 0.016f;
		float textY = statsY + statsHeight * 0.1705f;		
		textureHeight = statsHeight * headSize;
		
		//style.normal.textColor = new Color(0.99f, 0.62f, 0f, 0.95f);
		//style.normal.textColor = new Color(0.99f, 0.75f, 0.3f, 0.95f);
		Color unselectedTextColor = new Color(0.85f, 0.74f, 0.5f, 0.85f);	
		Color selectedTextColor = new Color(0.88f, 0.55f, 0f, 1f);
		selectedTextColor = unselectedTextColor;
		
		style.normal.textColor = (guiManager.selectedPuma == 0) ? selectedTextColor : ((scoringSystem.GetPumaHealth(0) <= 0f) ? deadPumaTextColor : ((scoringSystem.GetPumaHealth(0) >= 1f) ? fullHealthPumaTextColor : unselectedTextColor));
		style.fontSize = (int)(overlayRect.width * bigTextFont * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		textureX = statsX + columnWidth*0 + columnGap*0;
		GUI.Button(new Rect(textureX, textY, columnWidth, textureHeight), "Eric", style);
		style.alignment = TextAnchor.MiddleCenter;
		
		style.normal.textColor = (guiManager.selectedPuma == 1) ? selectedTextColor : ((scoringSystem.GetPumaHealth(1) <= 0f) ? deadPumaTextColor : ((scoringSystem.GetPumaHealth(1) >= 1f) ? fullHealthPumaTextColor : unselectedTextColor));
		style.fontSize = (int)(overlayRect.width * bigTextFont * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		textureX = statsX + columnWidth*1 + columnGap*1;
		GUI.Button(new Rect(textureX, textY, columnWidth, textureHeight), "Palo", style);
		style.alignment = TextAnchor.MiddleCenter;

		style.normal.textColor = (guiManager.selectedPuma == 2) ? selectedTextColor : ((scoringSystem.GetPumaHealth(2) <= 0f) ? deadPumaTextColor : ((scoringSystem.GetPumaHealth(2) >= 1f) ? fullHealthPumaTextColor : unselectedTextColor));
		style.fontSize = (int)(overlayRect.width * bigTextFont * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		textureX = statsX + columnWidth*2 + columnGap*2;
		GUI.Button(new Rect(textureX, textY, columnWidth, textureHeight), "Mitch", style);
		style.alignment = TextAnchor.MiddleCenter;

		style.normal.textColor = (guiManager.selectedPuma == 3) ? selectedTextColor : ((scoringSystem.GetPumaHealth(3) <= 0f) ? deadPumaTextColor : ((scoringSystem.GetPumaHealth(3) >= 1f) ? fullHealthPumaTextColor : unselectedTextColor));
		style.fontSize = (int)(overlayRect.width * bigTextFont * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		textureX = statsX + columnWidth*4 + columnGap*4 + midColumnSizeIncrease;
		GUI.Button(new Rect(textureX, textY, columnWidth, textureHeight), "Trish", style);
		style.alignment = TextAnchor.MiddleCenter;

		style.normal.textColor = (guiManager.selectedPuma == 4) ? selectedTextColor : ((scoringSystem.GetPumaHealth(4) <= 0f) ? deadPumaTextColor : ((scoringSystem.GetPumaHealth(4) >= 1f) ? fullHealthPumaTextColor : unselectedTextColor));
		style.fontSize = (int)(overlayRect.width * bigTextFont * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		textureX = statsX + columnWidth*5 + columnGap*5 + midColumnSizeIncrease;
		GUI.Button(new Rect(textureX, textY, columnWidth, textureHeight), "Liam", style);
		style.alignment = TextAnchor.MiddleCenter;

		style.normal.textColor = (guiManager.selectedPuma == 5) ? selectedTextColor : ((scoringSystem.GetPumaHealth(5) <= 0f) ? deadPumaTextColor : ((scoringSystem.GetPumaHealth(5) >= 1f) ? fullHealthPumaTextColor : unselectedTextColor));
		style.fontSize = (int)(overlayRect.width * bigTextFont * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		textureX = statsX + columnWidth*6 + columnGap*6 + midColumnSizeIncrease;
		GUI.Button(new Rect(textureX, textY, columnWidth, textureHeight), "Barb", style);
		style.alignment = TextAnchor.MiddleCenter;

		
		
		// puma labels
		
		float smallTextFont = 0.013f;
		textY = statsY + statsHeight * 0.212f;	

		style.normal.textColor = (guiManager.selectedPuma == 0) ? selectedTextColor : ((scoringSystem.GetPumaHealth(0) <= 0f) ? deadPumaTextColor : ((scoringSystem.GetPumaHealth(0) >= 1f) ? fullHealthPumaTextColor : unselectedTextColor));
		style.fontSize = (int)(overlayRect.width * smallTextFont * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		textureX = statsX + columnWidth*0 + columnGap*0;
		GUI.Button(new Rect(textureX, textY, columnWidth, textureHeight), "2 years - male", style);
		style.alignment = TextAnchor.MiddleCenter;

		style.normal.textColor = (guiManager.selectedPuma == 1) ? selectedTextColor : ((scoringSystem.GetPumaHealth(1) <= 0f) ? deadPumaTextColor : ((scoringSystem.GetPumaHealth(1) >= 1f) ? fullHealthPumaTextColor : unselectedTextColor));
		style.fontSize = (int)(overlayRect.width * smallTextFont * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		textureX = statsX + columnWidth*1 + columnGap*1;
		GUI.Button(new Rect(textureX, textY, columnWidth, textureHeight), "2 years - female", style);
		style.alignment = TextAnchor.MiddleCenter;

		style.normal.textColor = (guiManager.selectedPuma == 2) ? selectedTextColor : ((scoringSystem.GetPumaHealth(2) <= 0f) ? deadPumaTextColor : ((scoringSystem.GetPumaHealth(2) >= 1f) ? fullHealthPumaTextColor : unselectedTextColor));
		style.fontSize = (int)(overlayRect.width * smallTextFont * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		textureX = statsX + columnWidth*2 + columnGap*2;
		GUI.Button(new Rect(textureX, textY, columnWidth, textureHeight), "5 years - male", style);
		style.alignment = TextAnchor.MiddleCenter;

		style.normal.textColor = (guiManager.selectedPuma == 3) ? selectedTextColor : ((scoringSystem.GetPumaHealth(3) <= 0f) ? deadPumaTextColor : ((scoringSystem.GetPumaHealth(3) >= 1f) ? fullHealthPumaTextColor : unselectedTextColor));
		style.fontSize = (int)(overlayRect.width * smallTextFont * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		textureX = statsX + columnWidth*4 + columnGap*4 + midColumnSizeIncrease;
		GUI.Button(new Rect(textureX, textY, columnWidth, textureHeight), "5 years - female", style);
		style.alignment = TextAnchor.MiddleCenter;

		style.normal.textColor = (guiManager.selectedPuma == 4) ? selectedTextColor : ((scoringSystem.GetPumaHealth(4) <= 0f) ? deadPumaTextColor : ((scoringSystem.GetPumaHealth(4) >= 1f) ? fullHealthPumaTextColor : unselectedTextColor));
		style.fontSize = (int)(overlayRect.width * smallTextFont * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		textureX = statsX + columnWidth*5 + columnGap*5 + midColumnSizeIncrease;
		GUI.Button(new Rect(textureX, textY, columnWidth, textureHeight), "8 years - male", style);
		style.alignment = TextAnchor.MiddleCenter;

		style.normal.textColor = (guiManager.selectedPuma == 5) ? selectedTextColor : ((scoringSystem.GetPumaHealth(5) <= 0f) ? deadPumaTextColor : ((scoringSystem.GetPumaHealth(5) >= 1f) ? fullHealthPumaTextColor : unselectedTextColor));
		style.fontSize = (int)(overlayRect.width * smallTextFont * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		textureX = statsX + columnWidth*6 + columnGap*6 + midColumnSizeIncrease;
		GUI.Button(new Rect(textureX, textY, columnWidth, textureHeight), "8 years - female", style);
		style.alignment = TextAnchor.MiddleCenter;

		// population labels
		
		style.normal.textColor = new Color(0.90f, 0.75f, 0.4f, 0.8f);	
		style.fontSize = (int)(overlayRect.width * smallTextFont * fontScale * 1.15f);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		textureX = statsX + columnWidth*3 + columnGap*3;
		GUI.Button(new Rect(textureX, statsY + statsHeight * 0.02f, columnWidth + midColumnSizeIncrease, textureHeight), "Puma Population", style);
		style.alignment = TextAnchor.MiddleCenter;

		style.normal.textColor = new Color(0.90f, 0.85f, 0.4f, 0.7f);	
		style.fontSize = (int)(overlayRect.width * smallTextFont * fontScale * 1f);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		textureX = statsX + columnWidth*3 + columnGap*3;
		GUI.Button(new Rect(textureX, statsY + statsHeight * 0.068f, columnWidth + midColumnSizeIncrease, textureHeight), "overall results", style);
		style.alignment = TextAnchor.MiddleCenter;

		
		// deer heads

		float headstackBaseY = statsY + statsHeight * 0.291f;
		Texture2D displayHeadTexture;
		int columnNum;
		float columnShift;
		float incrementHeight = 0f;

		for (int j = 0; j < 6; j++) {
		
			if (scoringSystem.GetPumaHealth(j) <= 0f)
				GUI.color = deadPumaDeerIconColor;
			else if (scoringSystem.GetPumaHealth(j) >= 1f)
				GUI.color = fullHealthPumaDeerIconColor;

			columnNum = (j < 3) ? j : j+1;
			columnShift = (j < 3) ? 0 : midColumnSizeIncrease;
		
			displayHeadTexture = buckHeadTexture;
			textureX = statsX + columnWidth*columnNum + columnGap*columnNum + columnWidth*0.03f + columnShift;
			textureWidth = columnWidth * 0.24f;
			textureHeight = displayHeadTexture.height * (textureWidth / displayHeadTexture.width);
			incrementHeight = textureHeight * 1.1f;
			textureY = headstackBaseY - textureHeight * 0.0f;
			int kills = scoringSystem.GetBucksKilled(j);
			for (int i = 0; i < kills; i++) {
				GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), displayHeadTexture);
				textureY += incrementHeight;
			}

			displayHeadTexture = doeHeadTexture;
			textureX = statsX + columnWidth*columnNum + columnGap*columnNum + columnWidth*0.38f + columnShift;
			textureWidth = columnWidth * 0.26f;
			textureHeight = displayHeadTexture.height * (textureWidth / displayHeadTexture.width);
			textureY = headstackBaseY - textureHeight * 0.08f;
			kills = scoringSystem.GetDoesKilled(j);
			for (int i = 0; i < kills; i++) {
				GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), displayHeadTexture);
				textureY += incrementHeight;
			}
			
			displayHeadTexture = fawnHeadTexture;
			textureX = statsX + columnWidth*columnNum + columnGap*columnNum + columnWidth*0.72f + columnShift;
			textureY = headstackBaseY - textureHeight * 0.08f;
			textureWidth = columnWidth * 0.27f;
			textureHeight = displayHeadTexture.height * (textureWidth / displayHeadTexture.width);
			kills = scoringSystem.GetFawnsKilled(j);
			for (int i = 0; i < kills; i++) {
				GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), displayHeadTexture);
				textureY += incrementHeight;
			}
			
			GUI.color = new Color(1f, 1f, 1f, 1f * statsScreenOpacity);
		}
		
		// deer heads in center column
		
		columnNum = 3;
		columnShift = 0f;
		
		float centerColumnOffsetY = statsHeight * -0.15f;
		headstackBaseY += centerColumnOffsetY;
	
		// buck
		int bucksKilled = 0;
		for (int i = 0; i < 6; i++)
			bucksKilled += scoringSystem.GetBucksKilled(i);
		displayHeadTexture = buckHeadTexture;
		textureX = statsX + columnWidth*columnNum + columnGap*columnNum + columnWidth*0.03f + columnShift;
		textureWidth = columnWidth * 0.3f * 0.66f;
		textureHeight = displayHeadTexture.height * (textureWidth / displayHeadTexture.width);
		incrementHeight = textureHeight * 1.1f;
		textureY = headstackBaseY;
		for (int i = 0; i < (bucksKilled/2+bucksKilled%2); i++) {
			GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), displayHeadTexture);
			textureY += incrementHeight;
		}
		textureX = statsX + columnWidth*columnNum + columnGap*columnNum + columnWidth*0.22f + columnShift;
		textureY = headstackBaseY;
		for (int i = 0; i < (bucksKilled/2); i++) {
			GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), displayHeadTexture);
			textureY += incrementHeight;
		}

		// doe
		int doesKilled = 0;
		for (int i = 0; i < 6; i++)
			doesKilled += scoringSystem.GetDoesKilled(i);
		displayHeadTexture = doeHeadTexture;
		textureX = statsX + columnWidth*columnNum + columnGap*columnNum + columnWidth*0.48f + columnShift;
		textureWidth = columnWidth * 0.31f * 0.66f;
		textureHeight = displayHeadTexture.height * (textureWidth / displayHeadTexture.width);
		textureY = headstackBaseY - textureHeight * 0.10f;
		for (int i = 0; i < (doesKilled/2+doesKilled%2); i++) {
			GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), displayHeadTexture);
			textureY += incrementHeight;
		}
		textureX = statsX + columnWidth*columnNum + columnGap*columnNum + columnWidth*0.68f + columnShift;
		textureY = headstackBaseY - textureHeight * 0.10f;
		for (int i = 0; i < (doesKilled/2); i++) {
			GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), displayHeadTexture);
			textureY += incrementHeight;
		}
		
		// fawn
		int fawnsKilled = 0;
		for (int i = 0; i < 6; i++)
			fawnsKilled += scoringSystem.GetFawnsKilled(i);
		displayHeadTexture = fawnHeadTexture;
		textureX = statsX + columnWidth*columnNum + columnGap*columnNum + columnWidth*0.94f + columnShift;
		textureY = headstackBaseY;
		textureWidth = columnWidth * 0.3f * 0.66f;
		textureHeight = displayHeadTexture.height * (textureWidth / displayHeadTexture.width);
		for (int i = 0; i < (fawnsKilled/2+fawnsKilled%2); i++) {
			GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), displayHeadTexture);
			textureY += incrementHeight;
		}
		textureX = statsX + columnWidth*columnNum + columnGap*columnNum + columnWidth*1.08f + columnShift;
		textureY = headstackBaseY;
		for (int i = 0; i < (fawnsKilled/2); i++) {
			GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), displayHeadTexture);
			textureY += incrementHeight;
		}
	
		
		
		
		

		// vertical display bars
		
		float verticalBarsY = statsY + statsHeight * 0.71f;
		DrawDisplayBarsVert(0, statsScreenOpacity, statsX + columnWidth*0 + columnGap*0 + columnWidth*0.15f, verticalBarsY, columnWidth * 0.7f, statsHeight * 0.13f);
		DrawDisplayBarsVert(1, statsScreenOpacity, statsX + columnWidth*1 + columnGap*1 + columnWidth*0.15f, verticalBarsY, columnWidth * 0.7f, statsHeight * 0.13f);
		DrawDisplayBarsVert(2, statsScreenOpacity, statsX + columnWidth*2 + columnGap*2 + columnWidth*0.15f, verticalBarsY, columnWidth * 0.7f, statsHeight * 0.13f);
		DrawDisplayBarsVert(3, statsScreenOpacity, statsX + columnWidth*4 + columnGap*4 + columnWidth*0.15f + midColumnSizeIncrease, verticalBarsY, columnWidth * 0.7f, statsHeight * 0.13f);
		DrawDisplayBarsVert(4, statsScreenOpacity, statsX + columnWidth*5 + columnGap*5 + columnWidth*0.15f + midColumnSizeIncrease, verticalBarsY, columnWidth * 0.7f, statsHeight * 0.13f);
		DrawDisplayBarsVert(5, statsScreenOpacity, statsX + columnWidth*6 + columnGap*6 + columnWidth*0.15f + midColumnSizeIncrease, verticalBarsY, columnWidth * 0.7f, statsHeight * 0.13f);
		DrawDisplayBarsVert(6, statsScreenOpacity, statsX + columnWidth*3 + columnGap*3 + columnWidth*0.16f, verticalBarsY, columnWidth * 0.68f + midColumnSizeIncrease, statsHeight * 0.13f);

		
		// health bar
		
		float healthBarYFactor = 0.94f;

		guiComponents.DrawPumaHealthBar(0, statsScreenOpacity, statsX + columnWidth*0 + columnGap*0, statsY + statsHeight * healthBarYFactor, columnWidth, statsHeight * 0.04f, true, true);
		guiComponents.DrawPumaHealthBar(1, statsScreenOpacity, statsX + columnWidth*1 + columnGap*1, statsY + statsHeight * healthBarYFactor, columnWidth, statsHeight * 0.04f, true, true);
		guiComponents.DrawPumaHealthBar(2, statsScreenOpacity, statsX + columnWidth*2 + columnGap*2, statsY + statsHeight * healthBarYFactor, columnWidth, statsHeight * 0.04f, true, true);
		guiComponents.DrawPumaHealthBar(3, statsScreenOpacity, statsX + columnWidth*4 + columnGap*4 + midColumnSizeIncrease, statsY + statsHeight * healthBarYFactor, columnWidth, statsHeight * 0.04f, true, true);
		guiComponents.DrawPumaHealthBar(4, statsScreenOpacity, statsX + columnWidth*5 + columnGap*5 + midColumnSizeIncrease, statsY + statsHeight * healthBarYFactor, columnWidth, statsHeight * 0.04f, true, true);
		guiComponents.DrawPumaHealthBar(5, statsScreenOpacity, statsX + columnWidth*6 + columnGap*6 + midColumnSizeIncrease, statsY + statsHeight * healthBarYFactor, columnWidth, statsHeight * 0.04f, true, true);


		
		// ======================================
		
		// DIVIDERS

		float dividerY;
		Color upperColor;
		Color lowerColor;
		

		// horizontal - upper	
		dividerY = statsY + statsHeight * 0.28f;
		upperColor = new Color(0f, 0f, 0f, 0.55f);
		lowerColor = new Color(0.5f, 0.49f, 0.47f, 0.3f);
		guiUtils.DrawRect(new Rect(statsX + columnWidth*0 + columnGap*0, dividerY - overlayRect.height * 0.005f, columnWidth, overlayRect.height * 0.005f), upperColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*0 + columnGap*0, dividerY, columnWidth, overlayRect.height * 0.003f), lowerColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*1 + columnGap*1, dividerY - overlayRect.height * 0.005f, columnWidth, overlayRect.height * 0.005f), upperColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*1 + columnGap*1, dividerY, columnWidth, overlayRect.height * 0.003f), lowerColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*2 + columnGap*2, dividerY - overlayRect.height * 0.005f, columnWidth, overlayRect.height * 0.005f), upperColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*2 + columnGap*2, dividerY, columnWidth, overlayRect.height * 0.003f), lowerColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*3 + columnGap*3, dividerY - overlayRect.height * 0.005f + centerColumnOffsetY, columnWidth + midColumnSizeIncrease, overlayRect.height * 0.005f), upperColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*3 + columnGap*3, dividerY + centerColumnOffsetY, columnWidth + midColumnSizeIncrease, overlayRect.height * 0.003f), lowerColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*4 + columnGap*4 + midColumnSizeIncrease, dividerY - overlayRect.height * 0.005f, columnWidth, overlayRect.height * 0.005f), upperColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*4 + columnGap*4 + midColumnSizeIncrease, dividerY, columnWidth, overlayRect.height * 0.003f), lowerColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*5 + columnGap*5 + midColumnSizeIncrease, dividerY - overlayRect.height * 0.005f, columnWidth, overlayRect.height * 0.005f), upperColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*5 + columnGap*5 + midColumnSizeIncrease, dividerY, columnWidth, overlayRect.height * 0.003f), lowerColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*6 + columnGap*6 + midColumnSizeIncrease, dividerY - overlayRect.height * 0.005f, columnWidth, overlayRect.height * 0.005f), upperColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*6 + columnGap*6 + midColumnSizeIncrease, dividerY, columnWidth, overlayRect.height * 0.003f), lowerColor);	

		// vertical dividers
		Color leftColor = new Color(0f, 0f, 0f, 0.8f);
		Color rightColor = new Color(0.3f, 0.3f, 0.3f, 0.3f);
		float leftWidth = columnWidth * 0.02f;
		float rightWidth = columnWidth * 0.017f;
		//float barsHeight = statsHeight * 0.395f;
		float barsHeight = ((statsY + statsHeight * 0.86f) - (overlayRect.height * 0.005f)) - (dividerY + overlayRect.height * 0.003f);
		float leftPercent = 0.34f;
		float rightPercent = 0.67f;
		float columnWideWidth = columnWidth + midColumnSizeIncrease;
		guiUtils.DrawRect(new Rect(statsX + columnWidth*0 + columnGap*0 + columnWidth * leftPercent - leftWidth, dividerY + overlayRect.height * 0.003f, leftWidth, barsHeight), leftColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*0 + columnGap*0 + columnWidth * leftPercent, dividerY + overlayRect.height * 0.003f, rightWidth, barsHeight), rightColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*0 + columnGap*0 + columnWidth * rightPercent - leftWidth, dividerY + overlayRect.height * 0.003f, leftWidth, barsHeight), leftColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*0 + columnGap*0 + columnWidth * rightPercent, dividerY + overlayRect.height * 0.003f, rightWidth, barsHeight), rightColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*1 + columnGap*1 + columnWidth * leftPercent - leftWidth, dividerY + overlayRect.height * 0.003f, leftWidth, barsHeight), leftColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*1 + columnGap*1 + columnWidth * leftPercent, dividerY + overlayRect.height * 0.003f, rightWidth, barsHeight), rightColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*1 + columnGap*1 + columnWidth * rightPercent - leftWidth, dividerY + overlayRect.height * 0.003f, leftWidth, barsHeight), leftColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*1 + columnGap*1 + columnWidth * rightPercent, dividerY + overlayRect.height * 0.003f, rightWidth, barsHeight), rightColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*2 + columnGap*2 + columnWidth * leftPercent - leftWidth, dividerY + overlayRect.height * 0.003f, leftWidth, barsHeight), leftColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*2 + columnGap*2 + columnWidth * leftPercent, dividerY + overlayRect.height * 0.003f, rightWidth, barsHeight), rightColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*2 + columnGap*2 + columnWidth * rightPercent - leftWidth, dividerY + overlayRect.height * 0.003f, leftWidth, barsHeight), leftColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*2 + columnGap*2 + columnWidth * rightPercent, dividerY + overlayRect.height * 0.003f, rightWidth, barsHeight), rightColor);	
		rightPercent = 0.69f;
		guiUtils.DrawRect(new Rect(statsX + columnWidth*3 + columnGap*3 + columnWideWidth * leftPercent - leftWidth, dividerY + overlayRect.height * 0.003f + centerColumnOffsetY, leftWidth, barsHeight - centerColumnOffsetY), leftColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*3 + columnGap*3 + columnWideWidth * leftPercent, dividerY + overlayRect.height * 0.003f + centerColumnOffsetY, rightWidth, barsHeight - centerColumnOffsetY), rightColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*3 + columnGap*3 + columnWideWidth * rightPercent - leftWidth, dividerY + overlayRect.height * 0.003f + centerColumnOffsetY, leftWidth, barsHeight - centerColumnOffsetY), leftColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*3 + columnGap*3 + columnWideWidth * rightPercent, dividerY + overlayRect.height * 0.003f + centerColumnOffsetY, rightWidth, barsHeight - centerColumnOffsetY), rightColor);	
		rightPercent = 0.67f;
		guiUtils.DrawRect(new Rect(statsX + columnWidth*4 + columnGap*4 + midColumnSizeIncrease+ columnWidth * leftPercent - leftWidth, dividerY + overlayRect.height * 0.003f, leftWidth, barsHeight), leftColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*4 + columnGap*4 + midColumnSizeIncrease+ columnWidth * leftPercent, dividerY + overlayRect.height * 0.003f, rightWidth, barsHeight), rightColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*4 + columnGap*4 + midColumnSizeIncrease+ columnWidth * rightPercent - leftWidth, dividerY + overlayRect.height * 0.003f, leftWidth, barsHeight), leftColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*4 + columnGap*4 + midColumnSizeIncrease+ columnWidth * rightPercent, dividerY + overlayRect.height * 0.003f, rightWidth, barsHeight), rightColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*5 + columnGap*5 + midColumnSizeIncrease+ columnWidth * leftPercent - leftWidth, dividerY + overlayRect.height * 0.003f, leftWidth, barsHeight), leftColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*5 + columnGap*5 + midColumnSizeIncrease+ columnWidth * leftPercent, dividerY + overlayRect.height * 0.003f, rightWidth, barsHeight), rightColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*5 + columnGap*5 + midColumnSizeIncrease+ columnWidth * rightPercent - leftWidth, dividerY + overlayRect.height * 0.003f, leftWidth, barsHeight), leftColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*5 + columnGap*5 + midColumnSizeIncrease+ columnWidth * rightPercent, dividerY + overlayRect.height * 0.003f, rightWidth, barsHeight), rightColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*6 + columnGap*6 + midColumnSizeIncrease+ columnWidth * leftPercent - leftWidth, dividerY + overlayRect.height * 0.003f, leftWidth, barsHeight), leftColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*6 + columnGap*6 + midColumnSizeIncrease+ columnWidth * leftPercent, dividerY + overlayRect.height * 0.003f, rightWidth, barsHeight), rightColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*6 + columnGap*6 + midColumnSizeIncrease+ columnWidth * rightPercent - leftWidth, dividerY + overlayRect.height * 0.003f, leftWidth, barsHeight), leftColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*6 + columnGap*6 + midColumnSizeIncrease+ columnWidth * rightPercent, dividerY + overlayRect.height * 0.003f, rightWidth, barsHeight), rightColor);	

*/
		
		
	/*
		// horizontal - middle	
		dividerY = statsY + statsHeight * 0.68f;
		upperColor = new Color(0f, 0f, 0f, 0.55f);
		lowerColor = new Color(0.4f, 0.4f, 0.4f, 0.3f);
		guiUtils.DrawRect(new Rect(statsX + columnWidth*0 + columnGap*0, dividerY - overlayRect.height * 0.005f, columnWidth, overlayRect.height * 0.005f), upperColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*0 + columnGap*0, dividerY, columnWidth, overlayRect.height * 0.003f), lowerColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*1 + columnGap*1, dividerY - overlayRect.height * 0.005f, columnWidth, overlayRect.height * 0.005f), upperColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*1 + columnGap*1, dividerY, columnWidth, overlayRect.height * 0.003f), lowerColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*2 + columnGap*2, dividerY - overlayRect.height * 0.005f, columnWidth, overlayRect.height * 0.005f), upperColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*2 + columnGap*2, dividerY, columnWidth, overlayRect.height * 0.003f), lowerColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*3 + columnGap*3, dividerY - overlayRect.height * 0.005f, columnWidth + midColumnSizeIncrease, overlayRect.height * 0.005f), upperColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*3 + columnGap*3, dividerY, columnWidth + midColumnSizeIncrease, overlayRect.height * 0.003f), lowerColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*4 + columnGap*4 + midColumnSizeIncrease, dividerY - overlayRect.height * 0.005f, columnWidth, overlayRect.height * 0.005f), upperColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*4 + columnGap*4 + midColumnSizeIncrease, dividerY, columnWidth, overlayRect.height * 0.003f), lowerColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*5 + columnGap*5 + midColumnSizeIncrease, dividerY - overlayRect.height * 0.005f, columnWidth, overlayRect.height * 0.005f), upperColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*5 + columnGap*5 + midColumnSizeIncrease, dividerY, columnWidth, overlayRect.height * 0.003f), lowerColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*6 + columnGap*6 + midColumnSizeIncrease, dividerY - overlayRect.height * 0.005f, columnWidth, overlayRect.height * 0.005f), upperColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*6 + columnGap*6 + midColumnSizeIncrease, dividerY, columnWidth, overlayRect.height * 0.003f), lowerColor);	
*/

/*
		// horizontal - lower	
		dividerY = statsY + statsHeight * 0.86f;
		upperColor = new Color(0f, 0f, 0f, 0.55f);
		lowerColor = new Color(0.5f, 0.49f, 0.47f, 0.3f);
		guiUtils.DrawRect(new Rect(statsX + columnWidth*0 + columnGap*0, dividerY - overlayRect.height * 0.005f, columnWidth, overlayRect.height * 0.005f), upperColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*0 + columnGap*0, dividerY, columnWidth, overlayRect.height * 0.003f), lowerColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*1 + columnGap*1, dividerY - overlayRect.height * 0.005f, columnWidth, overlayRect.height * 0.005f), upperColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*1 + columnGap*1, dividerY, columnWidth, overlayRect.height * 0.003f), lowerColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*2 + columnGap*2, dividerY - overlayRect.height * 0.005f, columnWidth, overlayRect.height * 0.005f), upperColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*2 + columnGap*2, dividerY, columnWidth, overlayRect.height * 0.003f), lowerColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*3 + columnGap*3, dividerY - overlayRect.height * 0.005f, columnWidth + midColumnSizeIncrease, overlayRect.height * 0.005f), upperColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*3 + columnGap*3, dividerY, columnWidth + midColumnSizeIncrease, overlayRect.height * 0.003f), lowerColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*4 + columnGap*4 + midColumnSizeIncrease, dividerY - overlayRect.height * 0.005f, columnWidth, overlayRect.height * 0.005f), upperColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*4 + columnGap*4 + midColumnSizeIncrease, dividerY, columnWidth, overlayRect.height * 0.003f), lowerColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*5 + columnGap*5 + midColumnSizeIncrease, dividerY - overlayRect.height * 0.005f, columnWidth, overlayRect.height * 0.005f), upperColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*5 + columnGap*5 + midColumnSizeIncrease, dividerY, columnWidth, overlayRect.height * 0.003f), lowerColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*6 + columnGap*6 + midColumnSizeIncrease, dividerY - overlayRect.height * 0.005f, columnWidth, overlayRect.height * 0.005f), upperColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*6 + columnGap*6 + midColumnSizeIncrease, dividerY, columnWidth, overlayRect.height * 0.003f), lowerColor);	

		// ======================================
		
*/		
		
		
		
		
		return;

		// ======================================
		
		// DIVIDER

		guiUtils.DrawRect(new Rect(statsX, statsY + (statsHeight * 0.375f) - overlayRect.height * 0.005f, statsWidth, overlayRect.height * 0.005f), new Color(0f, 0f, 0f, 0.35f));	
		guiUtils.DrawRect(new Rect(statsX, statsY + (statsHeight * 0.375f), statsWidth, overlayRect.height * 0.005f), new Color(1f, 1f, 1f, 0.5f));	

		guiUtils.DrawRect(new Rect(statsX, statsY + (statsHeight/2) - overlayRect.height * 0.005f, statsWidth, overlayRect.height * 0.005f), new Color(0f, 0f, 0f, 0.35f));	
		guiUtils.DrawRect(new Rect(statsX, statsY + (statsHeight/2), statsWidth, overlayRect.height * 0.005f), new Color(1f, 1f, 1f, 0.5f));	

		guiUtils.DrawRect(new Rect(statsX, statsY + (statsHeight * 0.875f) - overlayRect.height * 0.005f, statsWidth, overlayRect.height * 0.005f), new Color(0f, 0f, 0f, 0.35f));	
		guiUtils.DrawRect(new Rect(statsX, statsY + (statsHeight * 0.875f), statsWidth, overlayRect.height * 0.005f), new Color(1f, 1f, 1f, 0.5f));	

		// ======================================
		
		float lowerSectionStatsY = statsY + statsHeight/2 - overlayRect.height * 0.01f;

		statsX = overlayRect.x + overlayRect.width * 0.15f;
		statsY = lowerSectionStatsY;
		statsWidth = overlayRect.width * 0.7f;
		statsHeight = overlayRect.height * 0.43f;
			
		// HUNTING STATS

		// title
		style.fontSize = (int)(overlayRect.width * 0.034f * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.MiddleRight;
		style.normal.textColor = new Color(0.063f, 0.059f, 0.161f, 1f);
		GUI.Button(new Rect(statsX + statsWidth * 0.05f, statsY + statsHeight * 0.044f, statsWidth * 0.21f, statsHeight * 0.1f), "Predation", style);

		// column headings
		style.fontSize = (int)(overlayRect.width * 0.026f * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.MiddleCenter;
		style.normal.textColor = new Color(0.063f, 0.059f, 0.161f, 1f);
		GUI.Button(new Rect(statsX + statsWidth * 0.30f, statsY + statsHeight * 0.051f, statsWidth * 0.16f, statsHeight * 0.1f), "Buck", style);
		GUI.Button(new Rect(statsX + statsWidth * 0.46f, statsY + statsHeight * 0.051f, statsWidth * 0.16f, statsHeight * 0.1f), "Doe", style);
		GUI.Button(new Rect(statsX + statsWidth * 0.62f, statsY + statsHeight * 0.051f, statsWidth * 0.16f, statsHeight * 0.1f), "Fawn", style);
		GUI.Button(new Rect(statsX + statsWidth * 0.80f, statsY + statsHeight * 0.051f, statsWidth * 0.15f, statsHeight * 0.1f), "TOTAL", style);

		// columns
		style.fontSize = (int)(overlayRect.width * 0.026f * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.MiddleCenter;
		style.normal.textColor = new Color(0.392f, 0.0588f, 0.0588f, 1f);

		GUI.Button(new Rect(statsX + statsWidth * 0.30f, statsY + statsHeight * 0.14f, statsWidth * 0.16f, statsHeight * 0.1f), "0", style);
		GUI.Button(new Rect(statsX + statsWidth * 0.30f, statsY + statsHeight * 0.215f, statsWidth * 0.16f, statsHeight * 0.1f), "0", style);
		GUI.Button(new Rect(statsX + statsWidth * 0.30f, statsY + statsHeight * 0.29f, statsWidth * 0.16f, statsHeight * 0.1f), "0", style);
		style.normal.textColor = new Color(0f, 0.35f, 0f, 1f);
		style.normal.textColor = new Color(0.063f, 0.059f, 0.161f, 1f);
		GUI.Button(new Rect(statsX + statsWidth * 0.30f, statsY + statsHeight * 0.38f, statsWidth * 0.16f, statsHeight * 0.1f), "0", style);
		style.normal.textColor = new Color(0.392f, 0.0588f, 0.0588f, 1f);

		GUI.Button(new Rect(statsX + statsWidth * 0.46f, statsY + statsHeight * 0.14f, statsWidth * 0.16f, statsHeight * 0.1f), "0", style);
		GUI.Button(new Rect(statsX + statsWidth * 0.46f, statsY + statsHeight * 0.215f, statsWidth * 0.16f, statsHeight * 0.1f), "0", style);
		GUI.Button(new Rect(statsX + statsWidth * 0.46f, statsY + statsHeight * 0.29f, statsWidth * 0.16f, statsHeight * 0.1f), "0", style);
		style.normal.textColor = new Color(0f, 0.35f, 0f, 1f);
		style.normal.textColor = new Color(0.063f, 0.059f, 0.161f, 1f);
		GUI.Button(new Rect(statsX + statsWidth * 0.46f, statsY + statsHeight * 0.38f, statsWidth * 0.16f, statsHeight * 0.1f), "0", style);
		style.normal.textColor = new Color(0.392f, 0.0588f, 0.0588f, 1f);

		GUI.Button(new Rect(statsX + statsWidth * 0.62f, statsY + statsHeight * 0.14f, statsWidth * 0.16f, statsHeight * 0.1f), "0", style);
		GUI.Button(new Rect(statsX + statsWidth * 0.62f, statsY + statsHeight * 0.215f, statsWidth * 0.16f, statsHeight * 0.1f), "0", style);
		GUI.Button(new Rect(statsX + statsWidth * 0.62f, statsY + statsHeight * 0.29f, statsWidth * 0.16f, statsHeight * 0.1f), "0", style);
		style.normal.textColor = new Color(0f, 0.35f, 0f, 1f);
		style.normal.textColor = new Color(0.063f, 0.059f, 0.161f, 1f);
		GUI.Button(new Rect(statsX + statsWidth * 0.62f, statsY + statsHeight * 0.38f, statsWidth * 0.16f, statsHeight * 0.1f), "0", style);
		style.normal.textColor = new Color(0.392f, 0.0588f, 0.0588f, 1f);

		style.normal.textColor = new Color(0.063f, 0.059f, 0.161f, 1f);
		GUI.Button(new Rect(statsX + statsWidth * 0.80f, statsY + statsHeight * 0.14f, statsWidth * 0.16f, statsHeight * 0.1f), "0", style);
		GUI.Button(new Rect(statsX + statsWidth * 0.80f, statsY + statsHeight * 0.215f, statsWidth * 0.16f, statsHeight * 0.1f), "0", style);
		GUI.Button(new Rect(statsX + statsWidth * 0.80f, statsY + statsHeight * 0.29f, statsWidth * 0.16f, statsHeight * 0.1f), "0", style);
		style.normal.textColor = new Color(0f, 0.35f, 0f, 1f);
		style.normal.textColor = new Color(0.063f, 0.059f, 0.161f, 1f);
		GUI.Button(new Rect(statsX + statsWidth * 0.80f, statsY + statsHeight * 0.38f, statsWidth * 0.16f, statsHeight * 0.1f), "0", style);
		style.normal.textColor = new Color(0.392f, 0.0588f, 0.0588f, 1f);

		// row labels
		style.fontSize = (int)(overlayRect.width * 0.023f * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.MiddleRight;
		style.normal.textColor = new Color(0.392f, 0.0588f, 0.0588f, 1f);
		GUI.Button(new Rect(statsX + statsWidth * 0.05f, statsY + statsHeight * 0.14f, statsWidth * 0.21f, statsHeight * 0.1f), "Prey Killed", style);
		GUI.Button(new Rect(statsX + statsWidth * 0.05f, statsY + statsHeight * 0.215f, statsWidth * 0.21f, statsHeight * 0.1f), "Effort Spent", style);
		GUI.Button(new Rect(statsX + statsWidth * 0.05f, statsY + statsHeight * 0.29f, statsWidth * 0.21f, statsHeight * 0.1f), "Calories Eaten", style);
		style.fontSize = (int)(overlayRect.width * 0.028f * fontScale);
		style.normal.textColor = new Color(0.063f, 0.059f, 0.161f, 1f);
		GUI.Button(new Rect(statsX + statsWidth * 0.05f, statsY + statsHeight * 0.38f, statsWidth * 0.21f, statsHeight * 0.1f), "Energy Gain", style);

		// grid lines
		//guiUtils.DrawRect(new Rect(statsX + statsWidth * 0.05f, statsY + statsHeight * 0.14f, statsWidth * 0.9f, statsHeight * 0.007f), new Color(1f, 1f, 1f, 0.5f));	
		//guiUtils.DrawRect(new Rect(statsX + statsWidth * 0.05f, statsY + statsHeight * 0.38f, statsWidth * 0.9f, statsHeight * 0.007f), new Color(1f, 1f, 1f, 0.5f));	

		// button
		GUI.skin = guiManager.customGUISkin;
		guiManager.customGUISkin.button.fontSize = (int)(overlayRect.width * 0.0165);
		guiManager.customGUISkin.button.fontStyle = FontStyle.Normal;
		GUI.color = new Color(1f, 1f, 1f, 1f);
		GUI.backgroundColor = new Color(1f, 0f, 0f, 1f);
		if (GUI.Button(new Rect(overlayRect.width * -0.003f + statsX + statsWidth * 0.24f, overlayRect.width * -0.0025f + statsY + statsHeight * 0.56f, overlayRect.width * 0.006f + statsWidth * 0.3f, overlayRect.width * 0.005f + statsHeight * 0.098f), "")) {
			guiManager.OpenInfoPanel(2);
		}
		GUI.backgroundColor = new Color(0.5f, 0.5f, 0.9f, 1f);
		if (GUI.Button(new Rect(statsX + statsWidth * 0.24f, statsY + statsHeight * 0.56f, statsWidth * 0.3f, statsHeight * 0.098f), "")) {
			guiManager.OpenInfoPanel(2);
		}
		if (GUI.Button(new Rect(statsX + statsWidth * 0.24f, statsY + statsHeight * 0.56f, statsWidth * 0.3f, statsHeight * 0.098f), "How Pumas Hunt....")) {
			guiManager.OpenInfoPanel(2);
		}
		GUI.backgroundColor = new Color(1f, 1f, 1f, 1f);
		GUI.color = new Color(1f, 1f, 1f, 1f);
		//buttonStyle.fontSize = buttonDownStyle.fontSize = (int)(overlayRect.width * 0.0165);
		//GUI.color = new Color(1f, 1f, 1f, 0.84f);
		//if (GUI.Button(new Rect(statsX + statsWidth * 0.24f, statsY + statsHeight * 0.554f, statsWidth * 0.52f, statsHeight * 0.11f), "HOW REAL PUMAS HUNT THEIR PREY...", buttonStyle))
			//currentScreen = currentScreen;
		//GUI.color = new Color(1f, 1f, 1f, 1f);
		

		// ======================================

		//statsY = lowerSectionStatsY;
		statsY = overlayRect.y + overlayRect.height * 0.21f;
		
		// ======================================

		// SURVIVAL STATS

		// title
		style.fontSize = (int)(overlayRect.width * 0.034f * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.MiddleRight;
		style.normal.textColor = new Color(0.063f, 0.059f, 0.161f, 1f);
		GUI.Button(new Rect(statsX + statsWidth * 0.05f, statsY + statsHeight * 0.044f, statsWidth * 0.21f, statsHeight * 0.1f), "Survival", style);

		// column headings
		style.fontSize = (int)(overlayRect.width * 0.026f * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.MiddleCenter;
		style.normal.textColor = new Color(0.063f, 0.059f, 0.161f, 1f);
		GUI.Button(new Rect(statsX + statsWidth * 0.30f, statsY + statsHeight * 0.051f, statsWidth * 0.16f, statsHeight * 0.1f), "Level 1", style);
		GUI.Button(new Rect(statsX + statsWidth * 0.46f, statsY + statsHeight * 0.051f, statsWidth * 0.16f, statsHeight * 0.1f), "Level 2", style);
		GUI.Button(new Rect(statsX + statsWidth * 0.62f, statsY + statsHeight * 0.051f, statsWidth * 0.16f, statsHeight * 0.1f), "Level 3", style);
		GUI.Button(new Rect(statsX + statsWidth * 0.80f, statsY + statsHeight * 0.051f, statsWidth * 0.15f, statsHeight * 0.1f), "TOTAL", style);

		// columns
		style.fontSize = (int)(overlayRect.width * 0.026f * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.MiddleCenter;
		style.normal.textColor = new Color(0.392f, 0.0588f, 0.0588f, 1f);

		GUI.Button(new Rect(statsX + statsWidth * 0.30f, statsY + statsHeight * 0.14f, statsWidth * 0.16f, statsHeight * 0.1f), "0", style);
		//GUI.Button(new Rect(statsX + statsWidth * 0.30f, statsY + statsHeight * 0.215f, statsWidth * 0.16f, statsHeight * 0.1f), "--", style);
		//guiUtils.DrawRect(new Rect(statsX + statsWidth * 0.365f, statsY + statsHeight * 0.245f, statsWidth * 0.03f, statsHeight * 0.04f), new Color(0.06f, 0.06f, 0.16f, 0.17f));	
		//GUI.Button(new Rect(statsX + statsWidth * 0.30f, statsY + statsHeight * 0.29f, statsWidth * 0.16f, statsHeight * 0.1f), "--", style);
		//guiUtils.DrawRect(new Rect(statsX + statsWidth * 0.365f, statsY + statsHeight * 0.32f, statsWidth * 0.03f, statsHeight * 0.04f), new Color(1f, 1f, 1f, 0.0f));	
		style.normal.textColor = new Color(0.063f, 0.059f, 0.161f, 1f);
		GUI.Button(new Rect(statsX + statsWidth * 0.30f, statsY + statsHeight * 0.38f, statsWidth * 0.16f, statsHeight * 0.1f), "6", style);
		style.normal.textColor = new Color(0.392f, 0.0588f, 0.0588f, 1f);

		GUI.Button(new Rect(statsX + statsWidth * 0.46f, statsY + statsHeight * 0.14f, statsWidth * 0.16f, statsHeight * 0.1f), "0", style);
		GUI.Button(new Rect(statsX + statsWidth * 0.46f, statsY + statsHeight * 0.215f, statsWidth * 0.16f, statsHeight * 0.1f), "0", style);
		//GUI.Button(new Rect(statsX + statsWidth * 0.46f, statsY + statsHeight * 0.29f, statsWidth * 0.16f, statsHeight * 0.1f), "--", style);
		//guiUtils.DrawRect(new Rect(statsX + statsWidth * 0.525f, statsY + statsHeight * 0.32f, statsWidth * 0.03f, statsHeight * 0.04f), new Color(1f, 1f, 1f, 0.0f));	
		style.normal.textColor = new Color(0.063f, 0.059f, 0.161f, 1f);
		GUI.Button(new Rect(statsX + statsWidth * 0.46f, statsY + statsHeight * 0.38f, statsWidth * 0.16f, statsHeight * 0.1f), "6", style);
		style.normal.textColor = new Color(0.392f, 0.0588f, 0.0588f, 1f);

		GUI.Button(new Rect(statsX + statsWidth * 0.62f, statsY + statsHeight * 0.14f, statsWidth * 0.16f, statsHeight * 0.1f), "0", style);
		GUI.Button(new Rect(statsX + statsWidth * 0.62f, statsY + statsHeight * 0.215f, statsWidth * 0.16f, statsHeight * 0.1f), "0", style);
		GUI.Button(new Rect(statsX + statsWidth * 0.62f, statsY + statsHeight * 0.29f, statsWidth * 0.16f, statsHeight * 0.1f), "0", style);
		style.normal.textColor = new Color(0.063f, 0.059f, 0.161f, 1f);
		GUI.Button(new Rect(statsX + statsWidth * 0.62f, statsY + statsHeight * 0.38f, statsWidth * 0.16f, statsHeight * 0.1f), "6", style);
		style.normal.textColor = new Color(0.392f, 0.0588f, 0.0588f, 1f);

		style.normal.textColor = new Color(0.063f, 0.059f, 0.161f, 1f);
		GUI.Button(new Rect(statsX + statsWidth * 0.80f, statsY + statsHeight * 0.14f, statsWidth * 0.16f, statsHeight * 0.1f), "0", style);
		GUI.Button(new Rect(statsX + statsWidth * 0.80f, statsY + statsHeight * 0.215f, statsWidth * 0.16f, statsHeight * 0.1f), "0", style);
		GUI.Button(new Rect(statsX + statsWidth * 0.80f, statsY + statsHeight * 0.29f, statsWidth * 0.16f, statsHeight * 0.1f), "0", style);
		style.normal.textColor = new Color(0.063f, 0.059f, 0.161f, 1f);
		GUI.Button(new Rect(statsX + statsWidth * 0.80f, statsY + statsHeight * 0.38f, statsWidth * 0.16f, statsHeight * 0.1f), "18", style);
		style.normal.textColor = new Color(0.392f, 0.0588f, 0.0588f, 1f);

		// row labels
		style.fontSize = (int)(overlayRect.width * 0.023f * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.MiddleRight;
		style.normal.textColor = new Color(0.392f, 0.0588f, 0.0588f, 1f);
		GUI.Button(new Rect(statsX + statsWidth * 0.05f, statsY + statsHeight * 0.14f, statsWidth * 0.21f, statsHeight * 0.1f), "Starved", style);
		GUI.Button(new Rect(statsX + statsWidth * 0.05f, statsY + statsHeight * 0.215f, statsWidth * 0.21f, statsHeight * 0.1f), "Poached", style);
		GUI.Button(new Rect(statsX + statsWidth * 0.05f, statsY + statsHeight * 0.29f, statsWidth * 0.21f, statsHeight * 0.1f), "Road Killed", style);
		style.fontSize = (int)(overlayRect.width * 0.028f * fontScale);
		style.normal.textColor = new Color(0.063f, 0.059f, 0.161f, 1f);
		GUI.Button(new Rect(statsX + statsWidth * 0.05f, statsY + statsHeight * 0.38f, statsWidth * 0.21f, statsHeight * 0.1f), "Pumas Left", style);

		// grid lines
		//guiUtils.DrawRect(new Rect(statsX + statsWidth * 0.05f, statsY + statsHeight * 0.14f, statsWidth * 0.9f, statsHeight * 0.007f), new Color(1f, 1f, 1f, 0.5f));	
		//guiUtils.DrawRect(new Rect(statsX + statsWidth * 0.05f, statsY + statsHeight * 0.38f, statsWidth * 0.9f, statsHeight * 0.007f), new Color(1f, 1f, 1f, 0.5f));	
		
		// button
		GUI.skin = guiManager.customGUISkin;
		guiManager.customGUISkin.button.fontSize = (int)(overlayRect.width * 0.0165);
		guiManager.customGUISkin.button.fontStyle = FontStyle.Normal;
		GUI.color = new Color(1f, 1f, 1f, 1f);
		GUI.backgroundColor = new Color(1f, 0f, 0f, 1f);
		if (GUI.Button(new Rect(overlayRect.width * -0.003f + statsX + statsWidth * 0.24f, overlayRect.width * -0.0025f + statsY + statsHeight * 0.56f, overlayRect.width * 0.006f + statsWidth * 0.3f, overlayRect.width * 0.005f + statsHeight * 0.098f), "")) {
			guiManager.OpenInfoPanel(4);
		}
		GUI.backgroundColor = new Color(0.5f, 0.5f, 0.9f, 1f);
		if (GUI.Button(new Rect(statsX + statsWidth * 0.24f, statsY + statsHeight * 0.56f, statsWidth * 0.3f, statsHeight * 0.098f), "")) {
			guiManager.OpenInfoPanel(4);
		}
		if (GUI.Button(new Rect(statsX + statsWidth * 0.24f, statsY + statsHeight * 0.56f, statsWidth * 0.3f, statsHeight * 0.098f), "Threats to Pumas....")) {
			guiManager.OpenInfoPanel(4);
		}
		GUI.backgroundColor = new Color(1f, 1f, 1f, 1f);
		GUI.color = new Color(1f, 1f, 1f, 1f);

		//buttonStyle.fontSize = buttonDownStyle.fontSize = (int)(overlayRect.width * 0.0165);
		//GUI.color = new Color(1f, 1f, 1f, 0.89f);
		//if (GUI.Button(new Rect(statsX + statsWidth * 0.24f, statsY + statsHeight * 0.545f, statsWidth * 0.52f, statsHeight * 0.11f), "THE THREATS REAL PUMAS FACE...", buttonStyle))
			//currentScreen = currentScreen;
		//GUI.color = new Color(1f, 1f, 1f, 1f);

	}


	void DrawQuitScreen(float quitScreenOpacity) 
	{ 
		float quitScreenY = overlayRect.y + overlayRect.height * 0.37f;

		GUIStyle style = new GUIStyle();
		style.alignment = TextAnchor.MiddleCenter;

		// background rectangle
		//guiUtils.DrawRect(new Rect(overlayRect.width * 0.315f, overlayRect.height * 0.3f, overlayRect.width * 0.37f, overlayRect.height * 0.3f), new Color(1f, 1f, 1f, 0.6f));	
		GUI.color = new Color(0f, 0f, 0f, 1f * quitScreenOpacity);
		GUI.Box(new Rect(overlayRect.x + overlayRect.width * 0.315f, quitScreenY, overlayRect.width * 0.37f, overlayRect.height * 0.3f), "");
		GUI.color = new Color(0f, 0f, 0f, 0.55f * quitScreenOpacity);
		GUI.Box(new Rect(overlayRect.x + overlayRect.width * 0.315f, quitScreenY, overlayRect.width * 0.37f, overlayRect.height * 0.3f), "");
		GUI.color = new Color(0f, 0f, 0f, 1f * quitScreenOpacity);

		GUI.color = new Color(1f, 1f, 1f, 1f * quitScreenOpacity);
		style.fontSize = (int)(overlayRect.width * 0.036f);
		style.fontStyle = FontStyle.BoldAndItalic;
		//style.normal.textColor = new Color(0.392f, 0.0588f, 0.0588f, 1f);
		//style.normal.textColor = new Color(1f, 1f, 1f, 1f);
			
		// quit screen prompt
		//guiUtils.DrawRect(new Rect(overlayRect.width * 0.32f, overlayRect.height * 0.34f, overlayRect.width * 0.36f, overlayRect.height * 0.08f), new Color(1f, 1f, 1f, 0.7f));	
		style.fontSize = (int)(overlayRect.width * 0.036f);
		style.fontStyle = FontStyle.BoldAndItalic;
		//style.normal.textColor = new Color(0.392f, 0.0588f, 0.0588f, 1f);
		style.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f * quitScreenOpacity);
		//GUI.Button(new Rect(overlayRect.x + overlayRect.width * 0.3f, quitScreenY + overlayRect.height * 0.03f, overlayRect.width * 0.4f, overlayRect.height * 0.1f), "Really Quit?", style);
		style.normal.textColor = Color.white;

		// quit button
		GUI.color = new Color(1f, 1f, 1f, 0.15f * quitScreenOpacity);
		guiUtils.DrawRect(new Rect(overlayRect.x + overlayRect.width * 0.415f, quitScreenY + overlayRect.height * 0.115f, overlayRect.width * 0.17f, overlayRect.height * 0.09f), new Color(1f, 1f, 1f, 1f));	
		GUI.skin = guiManager.customGUISkin;
		guiManager.customGUISkin.button.fontSize = (int)(overlayRect.width * 0.026);
		guiManager.customGUISkin.button.fontStyle = FontStyle.Normal;
		GUI.color = new Color(1f, 1f, 1f, 1f * quitScreenOpacity);
		GUI.backgroundColor = new Color(1f, 1f, 1f, 1f);
		if (GUI.Button(new Rect(overlayRect.x + overlayRect.width * 0.42f, quitScreenY + overlayRect.height * 0.12f, overlayRect.width * 0.16f, overlayRect.height * 0.08f), "")) {
			Application.Quit();
		}
		if (GUI.Button(new Rect(overlayRect.x + overlayRect.width * 0.42f, quitScreenY + overlayRect.height * 0.12f, overlayRect.width * 0.16f, overlayRect.height * 0.08f), "Quit")) {
			Application.Quit();
		}
		GUI.color = new Color(1f, 1f, 1f, 1f * quitScreenOpacity);
		GUI.backgroundColor = new Color(0f, 0f, 0f, 0f);
	}
	
	
	
	void DrawDisplayBars(int pumaNum, float displayBarsOpacity, float refX, float refY, float refWidth, float refHeight, bool brightFlag = false) 
	{ 
		float yOffset = overlayRect.height * -0.1f;
		float displayBarsRightShift = overlayRect.height * -0.1265f;
		
		refWidth = refHeight * ((pumaNum == guiManager.selectedPuma) ? 5f : 3f);
		
		refHeight *= 1.4f;
		
		if (scoringSystem.GetPumaHealth(pumaNum) >= 1f) {
			// puma at full health, draw heart
			GUI.color = new Color(0.8f, 0.8f, 0.8f, (pumaNum == guiManager.selectedPuma ? 0.6f : 0.5f) * displayBarsOpacity);
			float textureWidth = refWidth * (pumaNum == guiManager.selectedPuma ? 0.17f : 0.20f);
			float textureX = refX + (pumaNum == guiManager.selectedPuma ? overlayRect.width * 0.1f : overlayRect.width * 0.0385f) - textureWidth/2;
			float textureHeight = greenHeartTexture.height * (textureWidth / greenHeartTexture.width);
			float textureY = refY + overlayRect.height * 0.052f - textureHeight/2;
			GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), greenHeartTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * displayBarsOpacity);
		}

		else if (scoringSystem.GetPumaHealth(pumaNum) <= 0f) {
			// puma has died, draw crossbones
			GUI.color = new Color(0.7f, 0.7f, 0.7f, 0.6f * displayBarsOpacity);
			float textureX = refX + refWidth * 0.09f;
			float textureY = refY + refHeight * 0.21f;
			float textureWidth = refWidth * 0.27f;
			float textureHeight = pumaCrossbonesDarkRedTexture.height * (textureWidth / pumaCrossbonesDarkRedTexture.width);
			GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), pumaCrossbonesDarkRedTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * displayBarsOpacity);
		}

		else {
			// normal case, draw bars
					
			// stealth and running icons
			Texture2D iconTexture = pumaRunTexture;
			float iconHeight = overlayRect.height * 0.033f;
			float iconWidth = iconTexture.width * (iconHeight / iconTexture.height);
			float iconX = refX + refWidth * 0.4f + displayBarsRightShift;
			float iconY = yOffset + refY + refHeight - overlayRect.height * 0.005f;
			GUI.color = new Color(1f, 1f, 1f, 0.8f * displayBarsOpacity);
			GUI.DrawTexture(new Rect(iconX, iconY, iconWidth, iconHeight), iconTexture);
			iconTexture = pumaStealthTexture;
			iconHeight = overlayRect.height * 0.031f;
			iconWidth = iconTexture.width * (iconHeight / iconTexture.height);
			iconX = refX + refWidth * 0.4f + displayBarsRightShift;
			iconY = yOffset + refY + refHeight + overlayRect.height * 0.032f;
			GUI.DrawTexture(new Rect(iconX, iconY, iconWidth, iconHeight), iconTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * displayBarsOpacity);

			// display bars for characteristics
			float barHeightSpread = overlayRect.height * 0.007f;
			refX += overlayRect.width * 0.052f;
			refWidth *= 0.75f;
			
			// backgrounds
			GUI.color = new Color(0f, 0f, 0f, (brightFlag ? 0.9f : 0.75f) * displayBarsOpacity);
			GUI.Box(new Rect(refX + refWidth * 0.52f + displayBarsRightShift,  yOffset + refY + refHeight + overlayRect.height * 0.012f - overlayRect.height * 0.00f - barHeightSpread, refWidth * 0.38f, overlayRect.height * 0.012f), "");
			GUI.Box(new Rect(refX + refWidth * 0.52f + displayBarsRightShift,  yOffset + refY + refHeight + overlayRect.height * 0.036f - overlayRect.height * 0.00f + barHeightSpread, refWidth * 0.38f, overlayRect.height * 0.012f), "");
			//GUI.Box(new Rect(refX + refWidth * 0.52f + displayBarsRightShift,  yOffset + refY + refHeight + overlayRect.height * 0.056f - overlayRect.height * 0.00f, refWidth * 0.38f, overlayRect.height * 0.012f), "");
			//GUI.Box(new Rect(refX + refWidth * 0.52f + displayBarsRightShift,  yOffset + refY + refHeight + overlayRect.height * 0.078f - overlayRect.height * 0.00f, refWidth * 0.38f, overlayRect.height * 0.012f), "");


			// display bars for characteristics: backgrounds again to darken them
			//GUI.Box(new Rect(headshotX + headshotWidth * 0.52f,  yOffset + headshotY + headshotHeight + overlayRect.height * 0.012f - overlayRect.height * 0.002f, headshotWidth * 0.38f, overlayRect.height * 0.012f), "");
			//GUI.Box(new Rect(headshotX + headshotWidth * 0.52f,  yOffset + headshotY + headshotHeight + overlayRect.height * 0.035f - overlayRect.height * 0.002f, headshotWidth * 0.38f, overlayRect.height * 0.012f), "");
			//GUI.Box(new Rect(headshotX + headshotWidth * 0.52f,  yOffset + headshotY + headshotHeight + overlayRect.height * 0.058f - overlayRect.height * 0.002f, headshotWidth * 0.38f, overlayRect.height * 0.012f), "");
			//GUI.Box(new Rect(headshotX + headshotWidth * 0.52f,  yOffset + headshotY + headshotHeight + overlayRect.height * 0.081f - overlayRect.height * 0.002f, headshotWidth * 0.38f, overlayRect.height * 0.012f), "");

			// fillers
			GUI.color = new Color(0.75f, 0.75f, 0.75f, (brightFlag ? 0.5f : 0.45f) * displayBarsOpacity);
			
			guiUtils.DrawRect(new Rect(refX + refWidth * 0.54f + displayBarsRightShift,  yOffset + refY + refHeight + overlayRect.height * 0.018f - overlayRect.height * 0.002f - barHeightSpread, refWidth * 0.34f, overlayRect.height * 0.0048f), new Color(0.42f, 0.404f, 0.533f, 1f));	
			guiUtils.DrawRect(new Rect(refX + refWidth * 0.54f + displayBarsRightShift,  yOffset + refY + refHeight + overlayRect.height * 0.042f - overlayRect.height * 0.002f + barHeightSpread, refWidth * 0.34f, overlayRect.height * 0.0048f), new Color(0.5f, 0.5f, 0.5f, 1f));	
			//guiUtils.DrawRect(new Rect(refX + refWidth * 0.54f + displayBarsRightShift,  yOffset + refY + refHeight + overlayRect.height * 0.062f - overlayRect.height * 0.002f, refWidth * 0.34f, overlayRect.height * 0.0048f), new Color(0.5f, 0.5f, 0.5f, 1f));	
			//guiUtils.DrawRect(new Rect(refX + refWidth * 0.54f + displayBarsRightShift,  yOffset + refY + refHeight + overlayRect.height * 0.084f - overlayRect.height * 0.002f, refWidth * 0.34f, overlayRect.height * 0.0048f), new Color(0.5f, 0.5f, 0.5f, 1f));	

			GUI.color = new Color(1f, 1f, 1f, displayBarsOpacity);

			float highVal = 0.5f * 0.9f;
			float lowVal = 0.32f * 0.9f;
			Color grayBarColor = new Color(0.50f * 0.95f, 0.45f * 0.95f, 0.02f * 0.95f, 1f);
			
			float speed = guiManager.GetPumaSpeed(pumaNum);
			Color speedColor = (speed > 0.66f) ? new Color(lowVal, highVal, lowVal,  1f) : ((speed > 0.33f) ? new Color(highVal, highVal, lowVal, 1f) : new Color(highVal, lowVal, lowVal, 1f));
			guiUtils.DrawRect(new Rect(refX + refWidth * 0.54f + displayBarsRightShift,  yOffset + refY + refHeight + overlayRect.height * 0.018f - overlayRect.height * 0.002f - barHeightSpread, refWidth * 0.34f * speed, overlayRect.height * 0.0048f), speedColor);	

			float stealth = guiManager.GetPumaStealth(pumaNum);
			Color stealthColor = (stealth > 0.66f) ? new Color(lowVal, highVal, lowVal, 1f) : ((stealth > 0.33f) ? new Color(highVal, highVal, lowVal, 1f) : new Color(highVal, lowVal, lowVal, 1f));
			guiUtils.DrawRect(new Rect(refX + refWidth * 0.54f + displayBarsRightShift,  yOffset + refY + refHeight + overlayRect.height * 0.042f - overlayRect.height * 0.002f + barHeightSpread, refWidth * 0.34f * stealth, overlayRect.height * 0.0048f), stealthColor);	

/*
			float endurance = guiManager.GetPumaEndurance(pumaNum);
			Color enduranceColor = (endurance > 0.66f) ? new Color(0f, 1f, 0f,  0.8f) : ((endurance > 0.33f) ? new Color(1f, 1f, 0f, 0.85f) : new Color(1f, 0f, 0f, 1f));
			guiUtils.DrawRect(new Rect(refX + refWidth * 0.54f + displayBarsRightShift,  yOffset + refY + refHeight + overlayRect.height * 0.062f - overlayRect.height * 0.002f, refWidth * 0.34f * endurance, overlayRect.height * 0.0048f), enduranceColor);	

			float power = guiManager.GetPumaPower(pumaNum);
			Color powerColor = (power > 0.66f) ? new Color(0f, 1f, 0f,  0.8f) : ((power > 0.33f) ? new Color(1f, 1f, 0f, 0.85f) : new Color(1f, 0f, 0f, 1f));
			guiUtils.DrawRect(new Rect(refX + refWidth * 0.54f + displayBarsRightShift,  yOffset + refY + refHeight + overlayRect.height * 0.084f - overlayRect.height * 0.002f, refWidth * 0.34f * power, overlayRect.height * 0.0048f), powerColor);	
*/

		}
		
		GUI.color = new Color(1f, 1f, 1f, 1f * displayBarsOpacity);
	}
	
	
	void DrawDisplayBarsVert(int pumaNum, float displayBarsOpacity, float barsX, float barsY, float barsWidth, float barsHeight) 
	{ 
		float rectWidth = barsWidth * 0.20f;
		float rect1X = barsX;
		float rect2X = barsX + barsWidth * 0.4f;
		float rect3X = barsX + barsWidth * 0.8f;
		float margin = rectWidth * 0.3f;
		
		// display bars for characteristics: backgrounds
		GUI.color = new Color(0f, 0f, 0f, 0.9f * displayBarsOpacity);
		GUI.Box(new Rect(rect1X,  barsY, rectWidth, barsHeight), "");
		GUI.Box(new Rect(rect2X,  barsY, rectWidth, barsHeight), "");
		GUI.Box(new Rect(rect3X,  barsY, rectWidth, barsHeight), "");
		GUI.Box(new Rect(rect1X,  barsY, rectWidth, barsHeight), "");
		GUI.Box(new Rect(rect2X,  barsY, rectWidth, barsHeight), "");
		GUI.Box(new Rect(rect3X,  barsY, rectWidth, barsHeight), "");
		// display bars for characteristics: backgrounds again to darken them
		//GUI.Box(new Rect(headshotX + headshotWidth * 0.52f,  yOffset + headshotY + headshotHeight + overlayRect.height * 0.012f - overlayRect.height * 0.002f, headshotWidth * 0.38f, overlayRect.height * 0.012f), "");
		//GUI.Box(new Rect(headshotX + headshotWidth * 0.52f,  yOffset + headshotY + headshotHeight + overlayRect.height * 0.035f - overlayRect.height * 0.002f, headshotWidth * 0.38f, overlayRect.height * 0.012f), "");
		//GUI.Box(new Rect(headshotX + headshotWidth * 0.52f,  yOffset + headshotY + headshotHeight + overlayRect.height * 0.058f - overlayRect.height * 0.002f, headshotWidth * 0.38f, overlayRect.height * 0.012f), "");
		//GUI.Box(new Rect(headshotX + headshotWidth * 0.52f,  yOffset + headshotY + headshotHeight + overlayRect.height * 0.081f - overlayRect.height * 0.002f, headshotWidth * 0.38f, overlayRect.height * 0.012f), "");
		
		// display bars for characteristics: filler backgrounds
		Color barBackgroundColor = new Color(0.38f, 0.38f, 0.38f, 1f);	
		guiUtils.DrawRect(new Rect(rect1X+margin,  barsY+margin, rectWidth-margin*2f, barsHeight-margin*2f), barBackgroundColor);	
		guiUtils.DrawRect(new Rect(rect2X+margin,  barsY+margin, rectWidth-margin*2f, barsHeight-margin*2f), barBackgroundColor);	
		guiUtils.DrawRect(new Rect(rect3X+margin,  barsY+margin, rectWidth-margin*2f, barsHeight-margin*2f), barBackgroundColor);	
		
		// display bars for characteristics: fillers
		GUI.color = new Color(1f, 1f, 1f, 0.6f * displayBarsOpacity);
		if (pumaNum < 6 && scoringSystem.GetPumaHealth(pumaNum) <= 0f)
			GUI.color = new Color(1f, 1f, 1f, 0.25f * displayBarsOpacity);

		Color upperColor = new Color(0f, 0.75f, 0f, 1f);
		Color upperMiddleColor = new Color(0.5f * 1.04f, 0.7f * 1.04f, 0f, 1f);
		Color middleColor = new Color(0.85f * 0.99f, 0.85f * 0.99f, 0f, 1f);
		Color lowerMiddleColor = new Color(0.99f, 0.40f, 0f, 1f);
		Color lowerColor = new Color(0.86f, 0f, 0f, 1f);
		
		float buckCalories = scoringSystem.GetBuckCalories(pumaNum);
		float doeCalories  = scoringSystem.GetDoeCalories(pumaNum);
		float fawnCalories = scoringSystem.GetFawnCalories(pumaNum);
		
		if (buckCalories > 0f) {
			float buckExpenditures = scoringSystem.GetBuckExpenses(pumaNum);		
			float buckSuccess = 0f;
			if (buckCalories > buckExpenditures) {
				float percent = buckExpenditures / buckCalories;
				buckSuccess = 1f - percent * 0.5f;
			}
			else {
				float percent = buckCalories/ buckExpenditures;
				buckSuccess = percent * 0.5f;
			}
			buckSuccess = (buckSuccess < 0.05f) ? 0.05f : buckSuccess;
			Color buckColor = upperColor;
			if (buckSuccess < 0.2f)
				buckColor = lowerColor;
			else if (buckSuccess < 0.4f)
				buckColor = lowerMiddleColor;
			else if (buckSuccess < 0.6f)
				buckColor = middleColor;
			else if (buckSuccess < 0.8f)
				buckColor = upperMiddleColor;
			guiUtils.DrawRect(new Rect(rect1X+margin,  barsY+margin + (1f - buckSuccess) * (barsHeight-margin*2f), rectWidth-margin*2f, (barsHeight-margin*2f) * buckSuccess), buckColor);	
		}
		if (doeCalories > 0f) {
			float doeExpenditures = scoringSystem.GetDoeExpenses(pumaNum);		
			float doeSuccess = 0f;
			if (doeCalories > doeExpenditures) {
				float percent = doeExpenditures / doeCalories;
				doeSuccess = 1f - percent * 0.5f;
			}
			else {
				float percent = doeCalories/ doeExpenditures;
				doeSuccess = percent * 0.5f;
			}
			doeSuccess = (doeSuccess <  0.05f) ? 0.05f : doeSuccess;
			Color doeColor = upperColor;
			if (doeSuccess < 0.2f)
				doeColor = lowerColor;
			else if (doeSuccess < 0.4f)
				doeColor = lowerMiddleColor;
			else if (doeSuccess < 0.6f)
				doeColor = middleColor;
			else if (doeSuccess < 0.8f)
				doeColor = upperMiddleColor;
			guiUtils.DrawRect(new Rect(rect2X+margin,  barsY+margin + (1f - doeSuccess) * (barsHeight-margin*2f), rectWidth-margin*2f, (barsHeight-margin*2f) * doeSuccess), doeColor);	
		}
		if (fawnCalories > 0f) {
			float fawnExpenditures = scoringSystem.GetFawnExpenses(pumaNum);		
			float fawnSuccess = 0f;
			if (fawnCalories > fawnExpenditures) {
				float percent = fawnExpenditures / fawnCalories;
				fawnSuccess = 1f - percent * 0.5f;
			}
			else {
				float percent = fawnCalories/ fawnExpenditures;
				fawnSuccess = percent * 0.5f;
			}
			fawnSuccess = (fawnSuccess < 0.05f) ? 0.05f : fawnSuccess;
			Color fawnColor = upperColor;
			if (fawnSuccess < 0.2f)
				fawnColor = lowerColor;
			else if (fawnSuccess < 0.4f)
				fawnColor = lowerMiddleColor;
			else if (fawnSuccess < 0.6f)
				fawnColor = middleColor;
			else if (fawnSuccess < 0.8f)
				fawnColor = upperMiddleColor;
			guiUtils.DrawRect(new Rect(rect3X+margin,  barsY+margin + (1f - fawnSuccess) * (barsHeight-margin*2f), rectWidth-margin*2f, (barsHeight-margin*2f) * fawnSuccess), fawnColor);	
		}
		
		GUI.color = new Color(1f, 1f, 1f, 1f * displayBarsOpacity);
	}
	
		
	
	void DrawVertDisplayBar(float health, float displayBarsOpacity, float centerOpacity, float barX, float barY, float barWidth, float barHeight, bool lowerBarOpacity) 
	{ 
		float rectWidth = barWidth * 1.2f;
		float rectX = barX;
		float margin = rectWidth * 0.3f;
		
		if (true) {
		
			// display bars for characteristics: backgrounds
			GUI.color = new Color(0f, 0f, 0f, 1f * displayBarsOpacity);
			GUI.Box(new Rect(rectX,  barY, rectWidth, barHeight), "");
			GUI.Box(new Rect(rectX,  barY, rectWidth, barHeight), "");
			// display bars for characteristics: backgrounds again to darken them
			//GUI.Box(new Rect(headshotX + headshotWidth * 0.52f,  yOffset + headshotY + headshotHeight + overlayRect.height * 0.012f - overlayRect.height * 0.002f, headshotWidth * 0.38f, overlayRect.height * 0.012f), "");
			//GUI.Box(new Rect(headshotX + headshotWidth * 0.52f,  yOffset + headshotY + headshotHeight + overlayRect.height * 0.035f - overlayRect.height * 0.002f, headshotWidth * 0.38f, overlayRect.height * 0.012f), "");
			//GUI.Box(new Rect(headshotX + headshotWidth * 0.52f,  yOffset + headshotY + headshotHeight + overlayRect.height * 0.058f - overlayRect.height * 0.002f, headshotWidth * 0.38f, overlayRect.height * 0.012f), "");
			//GUI.Box(new Rect(headshotX + headshotWidth * 0.52f,  yOffset + headshotY + headshotHeight + overlayRect.height * 0.081f - overlayRect.height * 0.002f, headshotWidth * 0.38f, overlayRect.height * 0.012f), "");
			
			// display bars for characteristics: filler backgrounds
			Color barBackgroundColor = new Color(0.38f, 0.38f, 0.38f, 1f);	
			if (health == 0f)
				barBackgroundColor = new Color(0f, 0f, 0f, 1f);			
			
			guiUtils.DrawRect(new Rect(rectX+margin,  barY+margin, rectWidth-margin*2f, barHeight-margin*2f), barBackgroundColor);	
			
			// display bars for characteristics: fillers
			GUI.color = new Color(1f, 1f, 1f, centerOpacity * displayBarsOpacity);

			Color upperColor = new Color(0f, 0.75f, 0f, 1f);
			Color upperMiddleColor = new Color(0.5f * 1.04f, 0.7f * 1.04f, 0f, 1f);
			Color middleColor = new Color(0.85f * 0.99f, 0.85f * 0.99f, 0f, 1f);
			Color lowerMiddleColor = new Color(0.99f, 0.40f, 0f, 1f);
			Color lowerColor = new Color(0.86f, 0f, 0f, 1f);
			
			Color barColor = upperColor;
			if (health < 0.2f)
				barColor = lowerColor;
			else if (health < 0.4f)
				barColor = lowerMiddleColor;
			else if (health < 0.6f)
				barColor = middleColor;
			else if (health < 0.8f)
				barColor = upperMiddleColor;
			guiUtils.DrawRect(new Rect(rectX+margin,  barY+margin + (1f - health) * (barHeight-margin*2f), rectWidth-margin*2f, (barHeight-margin*2f) * health), barColor);	
		}

		if (health == 0f) {
			// health == 0; display crossbones to represent death of puma
			GUI.color = new Color(0.6f, 0.6f, 0.6f, 1f * displayBarsOpacity);				
			float textureHeight = barHeight*0.7f;
			float textureWidth = pumaCrossbonesRedTexture.width * (textureHeight / pumaCrossbonesRedTexture.height) * 0.85f;
			GUI.DrawTexture(new Rect(rectX + rectWidth/2 - textureWidth/2 + textureWidth * 0.68f, barY + barHeight*0.19f, textureWidth, textureHeight), pumaCrossbonesRedTexture);
		}

		GUI.color = new Color(1f, 1f, 1f, 1f * displayBarsOpacity);
	}
	


	bool PumaIsSelectable(int pumaNum)
	{	
		if (scoringSystem.GetPumaHealth(pumaNum) <= 0f)
			return false;

		return true;
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



