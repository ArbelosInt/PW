using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// FeedingDisplay
/// Draw the scorecard that comes up after every kill

public class FeedingDisplay : MonoBehaviour
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
	
	// textures based on bitmap files
	private Texture2D buckHeadTexture;
	private Texture2D doeHeadTexture;
	private Texture2D fawnHeadTexture;
	private Texture2D closeup1Texture;
	private Texture2D closeup2Texture;
	private Texture2D closeup3Texture;
	private Texture2D closeup4Texture;
	private Texture2D closeup5Texture;
	private Texture2D closeup6Texture;
	private Texture2D greenHeartTexture;	
	private Texture2D greenCheckTexture;	
	private Texture2D greenOutlineRectTexture;	
	private Texture2D redXTexture;	
	private Texture2D statsScreenTexture;	
	private Texture2D levelImage1Texture; 
	private Texture2D levelImage2Texture; 
	private Texture2D levelImage3Texture; 
	private Texture2D levelImage4Texture; 
	private Texture2D levelImage5Texture; 

	// external modules
	private GuiManager guiManager;
	private GuiComponents guiComponents;
	private GuiUtils guiUtils;
	private LevelManager levelManager;
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
		scoringSystem = GetComponent<ScoringSystem>();

		// texture references from GuiManager
		buckHeadTexture = guiManager.buckHeadTexture;
		doeHeadTexture = guiManager.doeHeadTexture;
		fawnHeadTexture = guiManager.fawnHeadTexture;
		closeup1Texture = guiManager.closeup1Texture;
		closeup2Texture = guiManager.closeup2Texture;
		closeup3Texture = guiManager.closeup3Texture;
		closeup4Texture = guiManager.closeup4Texture;
		closeup5Texture = guiManager.closeup5Texture;
		closeup6Texture = guiManager.closeup6Texture;
		greenHeartTexture = guiManager.greenHeartTexture;
		greenCheckTexture = guiManager.greenCheckTexture;
		greenOutlineRectTexture = guiManager.greenOutlineRectTexture;
		redXTexture = guiManager.redXTexture;
		statsScreenTexture = guiManager.statsScreenTexture;
		levelImage1Texture = guiManager.levelImage1Texture;
		levelImage2Texture = guiManager.levelImage2Texture;
		levelImage3Texture = guiManager.levelImage3Texture;
		levelImage4Texture = guiManager.levelImage4Texture;
		levelImage5Texture = guiManager.levelImage5Texture;
		
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
	
	public GameObject feedingDisplayMainPanel;
	public GameObject feedingDisplayLevelComplete;
	public GameObject feedingDisplayOkButton;

	// prefab gui components
	public GameObject uiPanel;
	public GameObject uiSubPanel;
	public GameObject uiRect;
	public GameObject uiText;
	public GameObject uiRawImage;
	public GameObject uiButton;
	public GameObject uiButtonSeeThru;
	public GameObject uiImageButton;
		
	// background and title
	private GameObject mainBackground;
	private GameObject upperBackground;
	private GameObject titleBackground;
	private GameObject titleText;
	
	// left side items
	private GameObject leftBackground;
	private GameObject leftDeerImage;
	private GameObject leftDeerText;
	
	
	// center area
	private GameObject centerBackground;
	
	
	// right side items
	private GameObject rightBackground;
	
	
	
	
	
	void CreateGUIItems()
	{
		//return;
	
	
		// set enables to 'off' before populating sub-items
		feedingDisplayMainPanel.SetActive(false);
		feedingDisplayLevelComplete.SetActive(false);
		feedingDisplayOkButton.SetActive(false);

		// background and title
		mainBackground = 		guiUtils.CreatePanel(feedingDisplayMainPanel, new Color(0f, 0f, 0f, 0.4f * 1.1f));
		upperBackground = 		guiUtils.CreatePanel(feedingDisplayMainPanel, new Color(0f, 0f, 0f, 0.4f * 0.9f));
		titleBackground = 		guiUtils.CreatePanel(feedingDisplayMainPanel, new Color(0f, 0f, 0f, 0.4f * 1.1f));
		titleText = 			guiUtils.CreateText(feedingDisplayMainPanel, "text", new Color(0.75f, 0f, 0f, 1f), FontStyle.Bold);
		
		// left side items
		leftBackground = 		guiUtils.CreatePanel(feedingDisplayMainPanel, new Color(0f, 0f, 0f, 0.4f * 1.3f));
		leftDeerImage = 		guiUtils.CreateImage(feedingDisplayMainPanel, closeup1Texture, new Color(1f, 1f, 1f, 1f));
		leftDeerText = 			guiUtils.CreateText(feedingDisplayMainPanel, "text", new Color(0f, 0f, 0f, 1f), FontStyle.Bold);
		
		// center area
		centerBackground = 		guiUtils.CreatePanel(feedingDisplayMainPanel, new Color(0f, 0f, 0f, 0.4f * 1.6f));
			
		// right side items
		rightBackground = 		guiUtils.CreatePanel(feedingDisplayMainPanel, new Color(0f, 0f, 0f, 0.4f * 1.3f));
	
		initComplete = true;
	}


	void PositionGUIItems(float backgroundOffset = 0f)
	{
		if (initComplete == false)
			return;
	
		float feedingDisplayX = (Screen.width / 2) - (Screen.height * 0.6f);
		float feedingDisplayY = Screen.height * 0.025f;
		float feedingDisplayWidth = Screen.height * 1.2f;
		float feedingDisplayHeight = Screen.height * 0.37f;		

		float feedingDisplayScaleFactor = 1.17f;
		feedingDisplayWidth *= (feedingDisplayScaleFactor * 0.935f);
		feedingDisplayHeight *= feedingDisplayScaleFactor;
		feedingDisplayX = (Screen.width / 2) - (feedingDisplayWidth / 2);
		feedingDisplayY = Screen.height * -0.02f;

		float fontRef = feedingDisplayHeight * 0.5f;
		float panelOffsetY = -0.1f;
		float textureX;
		float textureY;
		float textureWidth;
		float textureHeight;

		// background and title
		guiUtils.SetItemOffsets(mainBackground, feedingDisplayX, feedingDisplayY + feedingDisplayHeight * 0.06f, feedingDisplayWidth, feedingDisplayHeight * 1.15f - feedingDisplayHeight * 0.06f);
		guiUtils.SetItemOffsets(upperBackground, feedingDisplayX, feedingDisplayY + feedingDisplayHeight * 0.06f, feedingDisplayWidth, feedingDisplayHeight * 0.17f);
		guiUtils.SetItemOffsets(titleBackground, feedingDisplayX + feedingDisplayWidth * 0.19f + backgroundOffset, feedingDisplayY + feedingDisplayHeight * 0.1f, feedingDisplayWidth * 0.62f - backgroundOffset*2f, feedingDisplayHeight * 0.11f);
		guiUtils.SetTextOffsets(titleText, feedingDisplayX + feedingDisplayWidth * 0.3f, feedingDisplayY + feedingDisplayHeight * 0.136f, feedingDisplayWidth * 0.4f, feedingDisplayHeight * 0.03f, (int)(fontRef * 0.18f));

		// left side items
		guiUtils.SetItemOffsets(leftBackground, feedingDisplayX + feedingDisplayWidth * 0.035f, feedingDisplayY + feedingDisplayHeight * 0.3f, feedingDisplayWidth * 0.3f, feedingDisplayHeight * 0.48f);
		textureX = feedingDisplayX + feedingDisplayWidth * 0.075f;
		textureWidth = feedingDisplayHeight * 0.4f;
		textureHeight = leftDeerImage.GetComponent<RawImage>().texture.height * (textureWidth / leftDeerImage.GetComponent<RawImage>().texture.width);
		textureY = feedingDisplayY + feedingDisplayHeight * (0.32f + panelOffsetY);
		guiUtils.SetItemOffsets(leftDeerImage, textureX, textureY, textureWidth, textureHeight);
		guiUtils.SetTextOffsets(leftDeerText, feedingDisplayX + feedingDisplayWidth * 0.087f, feedingDisplayY + feedingDisplayHeight * (0.78f + panelOffsetY), feedingDisplayWidth * 0.1f, feedingDisplayHeight * 0.03f, (int)(fontRef * 0.13f));




		
		// center area
		guiUtils.SetItemOffsets(centerBackground, feedingDisplayX + feedingDisplayWidth * 0.335f - 2f, feedingDisplayY + feedingDisplayHeight * 0.3f, feedingDisplayWidth * 0.33f + 4f, feedingDisplayHeight * 0.16f);
			
		// right side items
		guiUtils.SetItemOffsets(rightBackground, feedingDisplayX + feedingDisplayWidth * 0.665f, feedingDisplayY + feedingDisplayHeight * 0.3f, feedingDisplayWidth * 0.3f, feedingDisplayHeight * 0.48f);
	
	}

	
	public void PrepareGUIItems()
	{
		if (initComplete == false)
			return;

		float feedingDisplayX = (Screen.width / 2) - (Screen.height * 0.6f);
		float feedingDisplayY = Screen.height * 0.025f;
		float feedingDisplayWidth = Screen.height * 1.2f;
		float feedingDisplayHeight = Screen.height * 0.37f;		

		float feedingDisplayScaleFactor = 1.17f;
		feedingDisplayWidth *= (feedingDisplayScaleFactor * 0.935f);
		feedingDisplayHeight *= feedingDisplayScaleFactor;
		feedingDisplayX = (Screen.width / 2) - (feedingDisplayWidth / 2);
		feedingDisplayY = Screen.height * -0.02f;

		bool failedHuntFlag = (levelManager.caughtDeer == null) ? true : false;
		Color titleColor;
		Color bottomColor1;
		Color bottomColor2;
		string titleString;
		int efficiencyLevel;
		string bottomString1;
		string bottomString2;
		float backgroundOffset = feedingDisplayWidth * 0f;

		float lastKillExpense = scoringSystem.GetLastKillExpense(guiManager.selectedPuma);
		float lastKillCaloriesEaten = scoringSystem.GetLastKillCaloriesEaten();

		if (lastKillExpense > 1.2f * lastKillCaloriesEaten)
			efficiencyLevel = 0;
		else if (lastKillExpense > lastKillCaloriesEaten)
			efficiencyLevel = 1;
		else if (lastKillExpense > 0.8 * lastKillCaloriesEaten)
			efficiencyLevel = 2;
		else
			efficiencyLevel = 3;

		float calorieChange = lastKillCaloriesEaten - lastKillExpense;
		if (calorieChange < 0)
			calorieChange = -calorieChange;
		calorieChange *= 0;  // temp val for rolling score factor
		int calorieDisplay = (int)calorieChange;

		switch (efficiencyLevel) {
		case 0:
			titleColor = new Color(0.8f, 0f, 0f, 1f);
			bottomColor1 = new Color(0.8f, 0f, 0f, 1f);
			bottomColor2 = new Color(0.82f, 0f, 0f, 1f);
			titleString = failedHuntFlag == true ? "OH NO: Your prey got away!" : "WARNING: Your hunt was very inefficient";
			bottomString1 = failedHuntFlag == true ? "TOTAL  LOSS !" : "NET  LOSS -";
			bottomString2 = calorieDisplay.ToString("n0"); // + " calories";
			backgroundOffset = feedingDisplayWidth * 0.03f;
			break;
		
		case 1:
			titleColor = new Color(0.83f, 0.78f, 0f, 1f);
			bottomColor1 = new Color(0.8f, 0f, 0f, 1f);
			bottomColor2 = new Color(0.85f, 0.80f, 0f, 1f);
			titleString = "CAREFUL - Your hunt was somewhat inefficient";
			bottomString1 = "NET  LOSS -";
			bottomString2 = calorieDisplay.ToString("n0"); // + " calories";
			break;
		
		case 2:
			titleColor = new Color(0.83f, 0.78f, 0f, 1f);
			bottomColor1 = new Color(0f, 0.66f, 0f, 1f);
			bottomColor2 = new Color(0.85f, 0.80f, 0f, 1f);
			titleString = "WELL DONE - Your hunt was slightly efficient";
			bottomString1 = "NET  GAIN +";
			bottomString2 = calorieDisplay.ToString("n0"); // + " calories";
			backgroundOffset = feedingDisplayWidth * 0.01f;
			break;
		
		default:
			titleColor = new Color(0f, 0.66f, 0f, 1f);
			bottomColor1 = new Color(0f, 0.66f, 0f, 1f);
			bottomColor2 = new Color(0f, 0.70f, 0f, 1f);
			titleString = "NICE JOB! Your hunt was very efficient";
			bottomString1 = "NET GAIN +";
			bottomString2 = calorieDisplay.ToString("n0"); // + " calories";
			backgroundOffset = feedingDisplayWidth * 0.035f;
			break;
		}

		titleText.GetComponent<Text>().text = titleString;
		titleText.GetComponent<Text>().color = titleColor;
		
		// left side
		
		Texture2D displayHeadTexture = buckHeadTexture;
		string displayHeadLabel = "-- -- -- --";
		
		switch (scoringSystem.GetLastKillDeerType()) {
			case "Buck":
				displayHeadTexture = buckHeadTexture;
				displayHeadLabel = "Buck";
				break;
			case "Doe":
				displayHeadTexture = doeHeadTexture;
				displayHeadLabel = "Doe";
				break;
			case "Fawn":
				displayHeadTexture = fawnHeadTexture;
				displayHeadLabel = "Fawn";
				break;	
		}

		leftDeerImage.GetComponent<RawImage>().texture = displayHeadTexture;
		leftDeerImage.GetComponent<RawImage>().color = failedHuntFlag == true ? new Color(0.02f, 0.02f, 0.02f, 1f) : new Color(1f, 1f, 1f, 1f);

		leftDeerText.GetComponent<Text>().text = displayHeadLabel;
		leftDeerText.GetComponent<Text>().color = failedHuntFlag == true ? new Color(0f, 0f, 0f, 1f) : new Color(0.99f * 0.9f, 0.63f * 0.8f, 0f, 1f);


		
		
		
		PositionGUIItems(backgroundOffset);
	}

	
	public void UpdateGUIItems(float mainContentOpacity, float rollingScoreFactor, float levelCompleteOpacity, float okButtonOpacity) 
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

			// top level enables and opacities
			feedingDisplayMainPanel.SetActive(mainContentOpacity > 0f ? true : false);
			feedingDisplayMainPanel.GetComponent<CanvasGroup>().alpha = mainContentOpacity;
			feedingDisplayLevelComplete.SetActive(levelCompleteOpacity > 0f ? true : false);
			feedingDisplayLevelComplete.GetComponent<CanvasGroup>().alpha = levelCompleteOpacity;
			feedingDisplayOkButton.SetActive(okButtonOpacity > 0f ? true : false);
			feedingDisplayOkButton.GetComponent<CanvasGroup>().alpha = okButtonOpacity;



		}
		else {
			// set enables to 'off'
			feedingDisplayMainPanel.SetActive(false);
			feedingDisplayLevelComplete.SetActive(false);
			feedingDisplayOkButton.SetActive(false);
		}
	}

	
	//===================================
	//===================================
	//		DRAW THE FEEDING DISPLAY
	//===================================
	//===================================

	public void Draw(float mainContentOpacity, float rollingScoreFactor, float levelCompleteOpacity, float okButtonOpacity) 
	{ 

		//if (USE_NEW_GUI == true)
			//return; 
		
		
		//////////////////////////////////
		//////////////////////////////////
		
		// LEGACY DRAW CODE

		//////////////////////////////////
		//////////////////////////////////

		
		float feedingDisplayX = (Screen.width / 2) - (Screen.height * 0.6f);
		float feedingDisplayY = Screen.height * 0.025f;
		float feedingDisplayWidth = Screen.height * 1.2f;
		float feedingDisplayHeight = Screen.height * 0.37f;

		float feedingDisplayScaleFactor = 1.17f;
		feedingDisplayWidth *= (feedingDisplayScaleFactor * 0.935f);
		feedingDisplayHeight *= feedingDisplayScaleFactor;
		feedingDisplayX = (Screen.width / 2) - (feedingDisplayWidth / 2);
		feedingDisplayY = Screen.height * -0.02f;

		bool failedHuntFlag = (levelManager.caughtDeer == null) ? true : false;

		GUIStyle style = new GUIStyle();
		style.alignment = TextAnchor.MiddleCenter;
		
		
		//********************
		// BACKGROUND CONTENT
		//********************

		// panel background
		if (USE_NEW_GUI == false) {
			GUI.color = new Color(1f, 1f, 1f, 0.8f * mainContentOpacity);
			GUI.Box(new Rect(feedingDisplayX, feedingDisplayY + feedingDisplayHeight * 0.06f, feedingDisplayWidth, feedingDisplayHeight * 1.15f - feedingDisplayHeight * 0.06f), "");
			GUI.color = new Color(1f, 1f, 1f, 0.3f * mainContentOpacity);
			GUI.Box(new Rect(feedingDisplayX, feedingDisplayY + feedingDisplayHeight * 0.06f, feedingDisplayWidth, feedingDisplayHeight * 1.15f - feedingDisplayHeight * 0.06f), "");
		}
		GUI.color = new Color(1f, 1f, 1f, 1f * mainContentOpacity);
	
		// main text
		Color titleColor;
		Color bottomColor1;
		Color bottomColor2;
		string titleString;
		int efficiencyLevel;
		string bottomString1;
		string bottomString2;
		float backgroundOffset = feedingDisplayWidth * 0f;

		float lastKillExpense = scoringSystem.GetLastKillExpense(guiManager.selectedPuma);
		float lastKillCaloriesEaten = scoringSystem.GetLastKillCaloriesEaten();
		
		if (lastKillExpense > 1.2f * lastKillCaloriesEaten)
			efficiencyLevel = 0;
		else if (lastKillExpense > lastKillCaloriesEaten)
			efficiencyLevel = 1;
		else if (lastKillExpense > 0.8 * lastKillCaloriesEaten)
			efficiencyLevel = 2;
		else
			efficiencyLevel = 3;
				
		float calorieChange = lastKillCaloriesEaten - lastKillExpense;
		if (calorieChange < 0)
			calorieChange = -calorieChange;
		calorieChange *= rollingScoreFactor;
		int calorieDisplay = (int)calorieChange;

		switch (efficiencyLevel) {
		case 0:
			titleColor = new Color(0.8f, 0f, 0f, 1f);
			bottomColor1 = new Color(0.8f, 0f, 0f, 1f);
			bottomColor2 = new Color(0.82f, 0f, 0f, 1f);
			titleString = failedHuntFlag == true ? "OH NO: Your prey got away!" : "WARNING: Your hunt was very inefficient";
			bottomString1 = failedHuntFlag == true ? "TOTAL  LOSS !" : "NET  LOSS -";
			bottomString2 = calorieDisplay.ToString("n0"); // + " calories";
			backgroundOffset = feedingDisplayWidth * 0.03f;
			break;
		
		case 1:
			titleColor = new Color(0.83f, 0.78f, 0f, 1f);
			bottomColor1 = new Color(0.8f, 0f, 0f, 1f);
			bottomColor2 = new Color(0.85f, 0.80f, 0f, 1f);
			titleString = "CAREFUL - Your hunt was somewhat inefficient";
			bottomString1 = "NET  LOSS -";
			bottomString2 = calorieDisplay.ToString("n0"); // + " calories";
			break;
		
		case 2:
			titleColor = new Color(0.83f, 0.78f, 0f, 1f);
			bottomColor1 = new Color(0f, 0.66f, 0f, 1f);
			bottomColor2 = new Color(0.85f, 0.80f, 0f, 1f);
			titleString = "WELL DONE - Your hunt was slightly efficient";
			bottomString1 = "NET  GAIN +";
			bottomString2 = calorieDisplay.ToString("n0"); // + " calories";
			backgroundOffset = feedingDisplayWidth * 0.01f;
			break;
		
		default:
			titleColor = new Color(0f, 0.66f, 0f, 1f);
			bottomColor1 = new Color(0f, 0.66f, 0f, 1f);
			bottomColor2 = new Color(0f, 0.70f, 0f, 1f);
			titleString = "NICE JOB! Your hunt was very efficient";
			bottomString1 = "NET GAIN +";
			bottomString2 = calorieDisplay.ToString("n0"); // + " calories";
			backgroundOffset = feedingDisplayWidth * 0.035f;
			break;
		}
		
		float fontRef = feedingDisplayHeight * 0.5f;
		style.fontStyle = FontStyle.BoldAndItalic;

		// main title

		GUI.color = new Color(1f, 1f, 1f, 0.8f * mainContentOpacity);
		if (USE_NEW_GUI == false)
			GUI.Box(new Rect(feedingDisplayX, feedingDisplayY + feedingDisplayHeight * 0.06f, feedingDisplayWidth, feedingDisplayHeight * 0.17f), "");
		GUI.color = new Color(1f, 1f, 1f, 1f * mainContentOpacity);

		GUI.color = new Color(1f, 1f, 1f, 0.9f * mainContentOpacity);
		if (USE_NEW_GUI == false)
			GUI.Box(new Rect(feedingDisplayX + feedingDisplayWidth * 0.19f + backgroundOffset, feedingDisplayY + feedingDisplayHeight * 0.1f, feedingDisplayWidth * 0.62f - backgroundOffset*2f, feedingDisplayHeight * 0.11f), "");
		GUI.color = new Color(1f, 1f, 1f, 1f * mainContentOpacity);


		//********************
		// MAIN CONTENT
		//********************

		GUI.color = new Color(1f, 1f, 1f, 1f * mainContentOpacity);

		style.fontSize = (int)(fontRef * 0.22f);
		style.normal.textColor =  titleColor;
		style.fontStyle = FontStyle.Bold;
		style.fontSize = (int)(fontRef * 0.18f);
		if (USE_NEW_GUI == false)
			GUI.Button(new Rect(feedingDisplayX + feedingDisplayWidth * 0.3f, feedingDisplayY + feedingDisplayHeight * 0.136f, feedingDisplayWidth * 0.4f, feedingDisplayHeight * 0.03f), titleString, style);


		
		// "main menu" and "hunting tips" buttons
/*		
		GUI.skin = guiManager.customGUISkin;
		guiManager.customGUISkin.button.fontSize = (int)(feedingDisplayHeight * 0.067);
		guiManager.customGUISkin.button.fontStyle = FontStyle.Normal;
		guiManager.customGUISkin.button.fontStyle = FontStyle.Bold;
		guiManager.customGUISkin.button.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f);

		if (GUI.Button(new Rect(feedingDisplayX + feedingDisplayWidth * 0.035f,  feedingDisplayY + feedingDisplayHeight * 0.095f, feedingDisplayWidth * 0.14f, feedingDisplayHeight * 0.11f), "")) {
			guiManager.SetGuiState("guiStateLeavingGameplay");
			levelManager.SetGameState("gameStateLeavingGameplay");
		}	
		if (GUI.Button(new Rect(feedingDisplayX + feedingDisplayWidth * 0.035f,  feedingDisplayY + feedingDisplayHeight * 0.095f, feedingDisplayWidth * 0.14f, feedingDisplayHeight * 0.11f), "Main Menu")) {
			guiManager.SetGuiState("guiStateLeavingGameplay");
			levelManager.SetGameState("gameStateLeavingGameplay");
		}	
		
		GUI.skin = guiManager.customGUISkin;
		guiManager.customGUISkin.button.fontSize = (int)(feedingDisplayHeight * 0.0635);
		guiManager.customGUISkin.button.fontStyle = FontStyle.Normal;
		guiManager.customGUISkin.button.fontStyle = FontStyle.Bold;
		if (GUI.Button(new Rect(feedingDisplayX + feedingDisplayWidth * 0.825f,  feedingDisplayY + feedingDisplayHeight * 0.095f, feedingDisplayWidth * 0.14f, feedingDisplayHeight * 0.11f), "")) {
			guiManager.OpenInfoPanel(3);
		}
		if (GUI.Button(new Rect(feedingDisplayX + feedingDisplayWidth * 0.825f,  feedingDisplayY + feedingDisplayHeight * 0.095f, feedingDisplayWidth * 0.14f, feedingDisplayHeight * 0.11f), "Hunting Tips")) {
			guiManager.OpenInfoPanel(3);
		}
		
		guiManager.customGUISkin.button.normal.textColor = new Color(1f, 0f, 0f, 1f);
		
		guiManager.customGUISkin.button.normal.textColor = new Color(0.90f, 0.65f, 0f, 1f);
*/		
		

		// center panel

		if (USE_NEW_GUI == false) {
			GUI.color = new Color(1f, 1f, 1f, 0.8f * mainContentOpacity);
			GUI.Box(new Rect(feedingDisplayX + feedingDisplayWidth * 0.335f, feedingDisplayY + feedingDisplayHeight * 0.3f, feedingDisplayWidth * 0.33f, feedingDisplayHeight * 0.16f), "");
			GUI.Box(new Rect(feedingDisplayX + feedingDisplayWidth * 0.335f, feedingDisplayY + feedingDisplayHeight * 0.3f, feedingDisplayWidth * 0.33f, feedingDisplayHeight * 0.16f), "");
		}

		style.fontSize = (int)(fontRef * 0.15f);
		style.normal.textColor = new Color(0.90f, 0.65f, 0f, 1f);
		style.normal.textColor =  bottomColor1;
		GUI.color = new Color(1f, 1f, 1f, 1f * mainContentOpacity);
		GUI.Button(new Rect(feedingDisplayX + feedingDisplayWidth * 0.31f, feedingDisplayY + feedingDisplayHeight * 0.366f, feedingDisplayWidth * 0.5f, feedingDisplayHeight * 0.03f), bottomString1, style);

		style.fontSize = (int)(fontRef * 0.19f);
		style.normal.textColor = new Color(0.90f, 0.65f, 0f, 1f);
		style.normal.textColor =  bottomColor2;
		GUI.Button(new Rect(feedingDisplayX + feedingDisplayWidth * 0.18f, feedingDisplayY + feedingDisplayHeight * 0.36f, feedingDisplayWidth * 0.5f, feedingDisplayHeight * 0.03f), bottomString2, style);
		
		// deer head & status info
		
		float panelOffsetY = -0.1f;

		if (USE_NEW_GUI == false) {
			GUI.color = new Color(1f, 1f, 1f, 0.8f * mainContentOpacity);
			GUI.Box(new Rect(feedingDisplayX + feedingDisplayWidth * 0.035f, feedingDisplayY + feedingDisplayHeight * 0.3f, feedingDisplayWidth * 0.3f, feedingDisplayHeight * 0.48f), "");
			GUI.color = new Color(1f, 1f, 1f, 0.5f * mainContentOpacity);
			GUI.Box(new Rect(feedingDisplayX + feedingDisplayWidth * 0.035f, feedingDisplayY + feedingDisplayHeight * 0.3f, feedingDisplayWidth * 0.3f, feedingDisplayHeight * 0.48f), "");
		}
		GUI.color = new Color(1f, 1f, 1f, 1f * mainContentOpacity);


		Texture2D displayHeadTexture = buckHeadTexture;
		string displayHeadLabel = "-- -- -- --";
		
		switch (scoringSystem.GetLastKillDeerType()) {
			case "Buck":
				displayHeadTexture = buckHeadTexture;
				displayHeadLabel = "Buck";
				break;
			case "Doe":
				displayHeadTexture = doeHeadTexture;
				displayHeadLabel = "Doe";
				break;
			case "Fawn":
				displayHeadTexture = fawnHeadTexture;
				displayHeadLabel = "Fawn";
				break;	
		}

		
		float textureX = feedingDisplayX + feedingDisplayWidth * 0.075f;
		float textureWidth = feedingDisplayHeight * 0.4f;
		float textureHeight = displayHeadTexture.height * (textureWidth / displayHeadTexture.width);
		float textureY = feedingDisplayY + feedingDisplayHeight * (0.32f + panelOffsetY);
		if (USE_NEW_GUI == false) {
			if (failedHuntFlag == false)
				GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), displayHeadTexture);
			else {
				GUI.color = new Color(0.02f, 0.02f, 0.02f, 1f * mainContentOpacity);	
				GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), displayHeadTexture);
				GUI.color = new Color(1f, 1f, 1f, 1f * mainContentOpacity);
			}
		}

		Color failedHuntTextColor = new Color(0f, 0f, 0f, 1f);

		if (USE_NEW_GUI == false) {	
			style.normal.textColor = failedHuntFlag == true ? failedHuntTextColor : new Color(0.99f * 0.9f, 0.63f * 0.8f, 0f, 1f);
			style.fontSize = (int)(fontRef * 0.13f);
			GUI.Button(new Rect(feedingDisplayX + feedingDisplayWidth * 0.087f, feedingDisplayY + feedingDisplayHeight * (0.78f + panelOffsetY), feedingDisplayWidth * 0.1f, feedingDisplayHeight * 0.03f), displayHeadLabel, style);
		}

		style.normal.textColor = failedHuntFlag == true ? failedHuntTextColor : new Color(0.1f, 0.67f, 0.1f, 1f);
		style.fontSize = (int)(fontRef * 0.18f);
		int caloriesGained = (int)scoringSystem.GetLastKillCaloriesEaten();
		GUI.Button(new Rect(feedingDisplayX + feedingDisplayWidth * 0.203f, feedingDisplayY + feedingDisplayHeight * (0.60f + panelOffsetY), feedingDisplayWidth * 0.1f, feedingDisplayHeight * 0.03f), (failedHuntFlag == false) ? caloriesGained.ToString("n0") : "-- --", style);
		style.fontSize = (int)(fontRef * 0.12f);
		GUI.Button(new Rect(feedingDisplayX + feedingDisplayWidth * 0.205f, feedingDisplayY + feedingDisplayHeight * (0.68f + panelOffsetY), feedingDisplayWidth * 0.1f, feedingDisplayHeight * 0.03f), "calories +", style);

		
		
		// puma head & status info

		
		if (USE_NEW_GUI == false) {
			GUI.color = new Color(1f, 1f, 1f, 0.8f * mainContentOpacity);
			GUI.Box(new Rect(feedingDisplayX + feedingDisplayWidth * 0.665f, feedingDisplayY + feedingDisplayHeight * 0.3f, feedingDisplayWidth * 0.3f, feedingDisplayHeight * 0.48f), "");
			GUI.color = new Color(1f, 1f, 1f, 0.5f * mainContentOpacity);
			GUI.Box(new Rect(feedingDisplayX + feedingDisplayWidth * 0.665f, feedingDisplayY + feedingDisplayHeight * 0.3f, feedingDisplayWidth * 0.3f, feedingDisplayHeight * 0.48f), "");
		}
		GUI.color = new Color(1f, 1f, 1f, 1f * mainContentOpacity);


		// puma identity
		Texture2D headshotTexture = closeup1Texture;
		string pumaName = "no name";
		switch (guiManager.selectedPuma) {
		case 0:
			headshotTexture = closeup1Texture;
			pumaName = "Eric";
			break;
		case 1:
			headshotTexture = closeup2Texture;
			pumaName = "Palo";
			break;
		case 2:
			headshotTexture = closeup3Texture;
			pumaName = "Mitch";
			break;
		case 3:
			headshotTexture = closeup4Texture;
			pumaName = "Trish";
			break;
		case 4:
			headshotTexture = closeup5Texture;
			pumaName = "Liam";
			break;
		case 5:
			headshotTexture = closeup6Texture;
			pumaName = "Barb";
			break;
		}


		// puma head
		textureX = feedingDisplayX + feedingDisplayWidth * 0.81f;
		textureY = feedingDisplayY + feedingDisplayHeight * (0.42f + panelOffsetY);
		textureWidth = feedingDisplayHeight * 0.39f;
		textureHeight = headshotTexture.height * (textureWidth / headshotTexture.width);
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), headshotTexture);

		// puma name
		style.normal.textColor = new Color(0.99f * 0.9f, 0.63f * 0.8f, 0f, 1f);
		style.fontSize = (int)(fontRef * 0.13f);
		GUI.Button(new Rect(feedingDisplayX + feedingDisplayWidth * 0.817f, feedingDisplayY + feedingDisplayHeight * (0.78f + panelOffsetY), feedingDisplayWidth * 0.1f, feedingDisplayHeight * 0.03f), pumaName, style);

		// effort
		style.normal.textColor = new Color(0.65f, 0f, 0f, 1f);
		style.fontSize = (int)(fontRef * 0.18f);
		int caloriesExpended = (int)scoringSystem.GetLastKillExpense(guiManager.selectedPuma);
		GUI.Button(new Rect(feedingDisplayX + feedingDisplayWidth * 0.685f, feedingDisplayY + feedingDisplayHeight * (0.60f + panelOffsetY), feedingDisplayWidth * 0.1f, feedingDisplayHeight * 0.03f), caloriesExpended.ToString("n0"), style);
		style.fontSize = (int)(fontRef * 0.125f);
		GUI.Button(new Rect(feedingDisplayX + feedingDisplayWidth * 0.69f, feedingDisplayY + feedingDisplayHeight * (0.68f + panelOffsetY), feedingDisplayWidth * 0.1f, feedingDisplayHeight * 0.03f), "effort -", style);


		
		// population bar
		
		GUI.color = new Color(1f, 1f, 1f, 0.8f * mainContentOpacity);
		GUI.Box(new Rect(feedingDisplayX + feedingDisplayWidth * 0.37f, feedingDisplayY + feedingDisplayHeight * 0.59f, feedingDisplayWidth * 0.26f, feedingDisplayHeight * 0.34f), "");
		GUI.color = new Color(1f, 1f, 1f, 0.4f * mainContentOpacity);
		GUI.Box(new Rect(feedingDisplayX + feedingDisplayWidth * 0.37f, feedingDisplayY + feedingDisplayHeight * 0.59f, feedingDisplayWidth * 0.26f, feedingDisplayHeight * 0.34f), "");
		GUI.color = new Color(1f, 1f, 1f, 0.4f * mainContentOpacity);
		GUI.Box(new Rect(feedingDisplayX + feedingDisplayWidth * 0.37f, feedingDisplayY + feedingDisplayHeight * 0.59f, feedingDisplayWidth * 0.26f, feedingDisplayHeight * 0.34f), "");
	
		GUI.color = new Color(1f, 1f, 1f, 1f * mainContentOpacity);
		guiComponents.DrawPopulationHealthBar(mainContentOpacity, feedingDisplayX + feedingDisplayWidth * 0.035f, feedingDisplayY + feedingDisplayHeight * 0.93f, feedingDisplayWidth * 0.93f, feedingDisplayHeight * 0.145f, true, true);
		
		
		guiComponents.DrawPumaHealthBar(guiManager.selectedPuma, mainContentOpacity, feedingDisplayX + feedingDisplayWidth * 0.38f, feedingDisplayY + feedingDisplayHeight * 0.64f, feedingDisplayWidth * 0.24f, feedingDisplayHeight * 0.085f);

		
		
		//********************
		// LEVEL DISPLAY
		//********************

		int successCount = scoringSystem.GetHuntSuccessCount();

		float levelDisplayY = feedingDisplayY + feedingDisplayHeight * 1.282f;
		float levelDisplayW = feedingDisplayWidth * 0.28f; 
		float levelDisplayH = feedingDisplayHeight * 0.55f;
		float levelDisplayX = Screen.width - feedingDisplayX - levelDisplayW;
		
		float borderPercent = 0.05f;
		float innerX = levelDisplayX + levelDisplayW * borderPercent;
		float innerY = levelDisplayY + levelDisplayW * borderPercent;
		float innerW = levelDisplayW - levelDisplayW * borderPercent * 2f; 
		float innerH = levelDisplayH - levelDisplayW * borderPercent * 2f;
		
		// background boxes
		GUI.color = new Color(1f, 1f, 1f, 0.8f * levelCompleteOpacity);
		GUI.Box(new Rect(levelDisplayX, levelDisplayY, levelDisplayW, levelDisplayH), "");
		GUI.color = new Color(1f, 1f, 1f, 0.4f * levelCompleteOpacity);
		GUI.Box(new Rect(levelDisplayX, levelDisplayY, levelDisplayW, levelDisplayH), "");
		GUI.color = new Color(1f, 1f, 1f, 1f * levelCompleteOpacity);

		// text background
		GUI.color = new Color(1f, 1f, 1f, 0.8f * levelCompleteOpacity);
		GUI.Box(new Rect(innerX, innerY, innerW, innerH * 0.45f), "");
		GUI.color = new Color(1f, 1f, 1f, 0.4f * levelCompleteOpacity);
		GUI.Box(new Rect(innerX, innerY, innerW, innerH * 0.45f), "");
		GUI.color = new Color(1f, 1f, 1f, 1f * levelCompleteOpacity);
		
		// text labels
		style.normal.textColor = new Color(0.90f, 0.65f, 0f, 0.8f);
		style.fontSize = (int)(fontRef * 0.135f);
		GUI.Button(new Rect(levelDisplayX, levelDisplayY + levelDisplayH * 0.03f, levelDisplayW, levelDisplayH * 0.3f), successCount == 3 ? "That was 3 in a row!" : "3 good hunts in a row", style);
		style.normal.textColor = new Color(0.85f, 0.75f, 0f, 0.8f);
		style.fontSize = (int)(fontRef * 0.12f);
		GUI.Button(new Rect(levelDisplayX, levelDisplayY + levelDisplayH * 0.185f, levelDisplayW, levelDisplayH * 0.3f), successCount == 3 ? (levelManager.currentLevel == 4 ? "You've finished the game!" : "You've finished the level!") : (levelManager.currentLevel == 4 ? "completes the game!" : "opens the next level"), style);

		// hunt stat backgrounds
		float gapSize = innerW * 0.03f;
		GUI.color = new Color(1f, 1f, 1f, 0.8f * levelCompleteOpacity);
		GUI.Box(new Rect(innerX, innerY + innerH * 0.50f, (innerW - gapSize * 2f) *  0.33333f, innerH * 0.50f), "");
		GUI.color = new Color(1f, 1f, 1f, 0.4f * levelCompleteOpacity);
		GUI.Box(new Rect(innerX, innerY + innerH * 0.50f, (innerW - gapSize * 2f) *  0.33333f, innerH * 0.50f), "");
		GUI.color = new Color(1f, 1f, 1f, 1f * levelCompleteOpacity);

		GUI.color = new Color(1f, 1f, 1f, 0.8f * levelCompleteOpacity);
		GUI.Box(new Rect(innerX + innerW * 0.33333f + gapSize/4, innerY + innerH * 0.50f, (innerW - gapSize * 2f) *  0.33333f, innerH * 0.50f), "");
		GUI.color = new Color(1f, 1f, 1f, 0.4f * levelCompleteOpacity);
		GUI.Box(new Rect(innerX + innerW * 0.33333f + gapSize/4, innerY + innerH * 0.50f, (innerW - gapSize * 2f) *  0.33333f, innerH * 0.50f), "");
		GUI.color = new Color(1f, 1f, 1f, 1f * levelCompleteOpacity);

		GUI.color = new Color(1f, 1f, 1f, 0.8f * levelCompleteOpacity);
		GUI.Box(new Rect(innerX + innerW * 0.66666f + gapSize/2, innerY + innerH * 0.50f, (innerW - gapSize * 2f) *  0.33333f, innerH * 0.50f), "");
		GUI.color = new Color(1f, 1f, 1f, 0.4f * levelCompleteOpacity);
		GUI.Box(new Rect(innerX + innerW * 0.666666f + gapSize/2, innerY + innerH * 0.50f, (innerW - gapSize * 2f) *  0.33333f, innerH * 0.50f), "");
		GUI.color = new Color(1f, 1f, 1f, 1f * levelCompleteOpacity);

		// hunt indicators
		float inset = innerW * 0.04f;
		switch (successCount) {
		case 0:
			GUI.DrawTexture(new Rect(inset + innerX, inset + innerY + innerH * 0.50f, (innerW - gapSize * 2f) *  0.33333f - inset*2, innerH * 0.50f - inset*2), redXTexture);
			break;
		case 1:
			GUI.DrawTexture(new Rect(inset + innerX, inset + innerY + innerH * 0.50f, (innerW - gapSize * 2f) *  0.33333f - inset*2, innerH * 0.50f - inset*2), greenCheckTexture);
			break;
		case 2:
			GUI.DrawTexture(new Rect(inset + innerX, inset + innerY + innerH * 0.50f, (innerW - gapSize * 2f) *  0.33333f - inset*2, innerH * 0.50f - inset*2), greenCheckTexture);
			GUI.DrawTexture(new Rect(inset + innerX + innerW * 0.33333f + gapSize/4, inset + innerY + innerH * 0.50f, (innerW - gapSize * 2f) *  0.33333f - inset*2, innerH * 0.50f - inset*2), greenCheckTexture);
			break;
		case 3:
			GUI.DrawTexture(new Rect(inset + innerX, inset + innerY + innerH * 0.50f, (innerW - gapSize * 2f) *  0.33333f - inset*2, innerH * 0.50f - inset*2), greenCheckTexture);
			GUI.DrawTexture(new Rect(inset + innerX + innerW * 0.33333f + gapSize/4, inset + innerY + innerH * 0.50f, (innerW - gapSize * 2f) *  0.33333f - inset*2, innerH * 0.50f - inset*2), greenCheckTexture);
			GUI.DrawTexture(new Rect(inset + innerX + innerW * 0.66666f + gapSize/2, inset + innerY + innerH * 0.50f, (innerW - gapSize * 2f) *  0.33333f - inset*2, innerH * 0.50f - inset*2), greenCheckTexture);
			break;
		}
		
		// hunt success flashing rect	
		if (successCount == 3) {
			float flashingPeriod = 0.4f;
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
			flashingOpacity = 0.3f + flashingOpacity * 0.7f;
			flashingOpacity = flashingOpacity * flashingOpacity;
			GUI.color = new Color(1f, 1f, 1f, flashingOpacity * 0.8f * levelCompleteOpacity);
			GUI.DrawTexture(new Rect(levelDisplayX, levelDisplayY, levelDisplayW, levelDisplayH), greenOutlineRectTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * levelCompleteOpacity);
		}

		// hunt cheat buttons

		GUI.color = new Color(1f, 1f, 1f, 0f);

		if (GUI.Button(new Rect(inset + innerX, inset + innerY + innerH * 0.50f, (innerW - gapSize * 2f) *  0.33333f - inset*2, innerH * 0.50f - inset*2), "")) {
			scoringSystem.SetHuntSuccessCount(1);
		}
		if (GUI.Button(new Rect(inset + innerX + innerW * 0.33333f + gapSize/4, inset + innerY + innerH * 0.50f, (innerW - gapSize * 2f) *  0.33333f - inset*2, innerH * 0.50f - inset*2), "")) {
			scoringSystem.SetHuntSuccessCount(2);
		}
		if (GUI.Button(new Rect(inset + innerX + innerW * 0.66666f + gapSize/2, inset + innerY + innerH * 0.50f, (innerW - gapSize * 2f) *  0.33333f - inset*2, innerH * 0.50f - inset*2), "")) {
			scoringSystem.SetHuntSuccessCount(3);
		}



		
		//********************
		// LINK TO STATS PAGE
		//********************

		GUI.color = new Color(1f, 1f, 1f, 1f * levelCompleteOpacity);

		float statsLinkX = feedingDisplayX;
		float statsLinkY = feedingDisplayY + feedingDisplayHeight * 1.282f;
		float statsLinkW = feedingDisplayWidth * 0.28f; 
		float statsLinkH = feedingDisplayHeight * (successCount == 3 ? 0.95f : 0.65f);
		
		borderPercent = 0.05f;
		inset = innerW * 0.04f;		
		innerX = statsLinkX + statsLinkW * borderPercent;
		innerY = statsLinkY + statsLinkW * borderPercent;
		innerW = statsLinkW - statsLinkW * borderPercent * 2f; 
		innerH = statsLinkH - statsLinkW * borderPercent * 2f;
		
		// background boxes
		GUI.color = new Color(1f, 1f, 1f, 0.8f * levelCompleteOpacity);
		GUI.Box(new Rect(statsLinkX, statsLinkY, statsLinkW, statsLinkH), "");
		GUI.color = new Color(1f, 1f, 1f, 0.4f * levelCompleteOpacity);
		GUI.Box(new Rect(statsLinkX, statsLinkY, statsLinkW, statsLinkH), "");
		GUI.color = new Color(1f, 1f, 1f, 1f * levelCompleteOpacity);

		if (successCount != 3) {
		
			// NORMAL CASE:  draw stats link

			// image dimensions
			float imageWidth = innerW - gapSize * 2f;
			float imageHeight = statsScreenTexture.height * (imageWidth / statsScreenTexture.width);

			// image background
			GUI.color = new Color(1f, 1f, 1f, 0.8f * levelCompleteOpacity);
			GUI.Box(new Rect(innerX, innerY, innerW, imageHeight + inset*2), "");
			GUI.color = new Color(1f, 1f, 1f, 0.4f * levelCompleteOpacity);
			GUI.Box(new Rect(innerX, innerY, innerW, imageHeight + inset*2), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * levelCompleteOpacity);
			
			if (scoringSystem.GetPumaHealth(guiManager.selectedPuma) == 1f) {
				// puma at full health
			
				// puma head
				GUI.DrawTexture(new Rect(statsLinkX + statsLinkW/2 - imageWidth/2 + imageWidth*0.06f, inset*0.5f + innerY, imageWidth*0.4f, headshotTexture.height * ((imageWidth*0.4f) / headshotTexture.width)), headshotTexture);

				// puma name
				style.normal.textColor = new Color(0.99f * 0.8f, 0.63f * 0.7f, 0f, 1f);
				style.fontSize = (int)(fontRef * 0.13f);
				GUI.Button(new Rect(statsLinkX + statsLinkW/2 - imageWidth/2 + imageWidth*0.01f, inset + innerY + imageHeight*0.8f, imageWidth*0.5f, imageHeight * 0.2f), pumaName, style);

				// hunt success flashing rect	
				float flashingPeriod = 0.6f;
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
				flashingOpacity = 0.3f + flashingOpacity * 0.7f;
				flashingOpacity = flashingOpacity * flashingOpacity;
				GUI.color = new Color(1f, 1f, 1f, flashingOpacity * 0.5f * levelCompleteOpacity);
				GUI.DrawTexture(new Rect(statsLinkX, statsLinkY, statsLinkW, statsLinkH), greenOutlineRectTexture);

				style.normal.textColor = new Color(0f, 0.8f, 0f, 1f);
				style.fontSize = (int)(fontRef * 0.11f);
				style.alignment = TextAnchor.MiddleRight;
				GUI.color = new Color(1f, 1f, 1f, (flashingOpacity*0.65f + 0.35f) * levelCompleteOpacity);
				GUI.DrawTexture(new Rect(statsLinkX + statsLinkW/2 - imageWidth/2 + imageWidth*0.6f, innerY + inset*1.6f, imageWidth*0.28f, greenHeartTexture.height * ((imageWidth*0.28f) / greenHeartTexture.width)), greenHeartTexture);
				GUI.Button(new Rect(innerX + inset*0.2f, statsLinkY + statsLinkW * 0.32f, innerW - inset * 2, statsLinkH * 0.2f), "100% Health !", style);
				style.alignment = TextAnchor.MiddleCenter;

				GUI.color = new Color(1f, 1f, 1f, 1f * levelCompleteOpacity);
			}
			else {
				// normal case: draw image
				GUI.DrawTexture(new Rect(statsLinkX + statsLinkW/2 - imageWidth/2, inset + innerY, imageWidth, imageHeight), statsScreenTexture);
			}


			// invisible button over image
			GUI.color = new Color(1f, 1f, 1f, 0f);
			if (GUI.Button(new Rect(statsLinkX + statsLinkW/2 - imageWidth/2, inset + innerY, imageWidth, imageHeight), "")) {
				// go to stats screen
				guiManager.SetGuiState("guiStateLeavingFeeding");
				levelManager.SetGameState("gameStateLeavingGameplayA");
			}
			GUI.color = new Color(1f, 1f, 1f, 1f * levelCompleteOpacity);

			// main button
			GUI.skin = guiManager.customGUISkin;
			guiManager.customGUISkin.button.fontSize = (int)(feedingDisplayHeight * 0.08);
			guiManager.customGUISkin.button.fontStyle = FontStyle.Normal;
			if (GUI.Button(new Rect(innerX,  innerY + innerH*0.7f, innerW, innerH * 0.3f), scoringSystem.GetPumaHealth(guiManager.selectedPuma) == 1f ? "Select Another" : "View Stats")) {
				// go to stats screen
				guiManager.SetGuiState("guiStateLeavingFeeding");
				levelManager.SetGameState("gameStateLeavingGameplayA");
			}	
		}
		else {
		
			// LEVEL COMPLETE:  draw level info




			// determine level image
			Texture2D imageTexture = levelImage1Texture;
			float imageOpacity = 1f;
			
			switch (levelManager.GetCurrentLevel()) {
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
			}		
		
			// image dimensions
			float imageWidth = innerW - gapSize * 2f;
			float imageHeight = imageTexture.height * (imageWidth / imageTexture.width);
			float imageOffsetY = statsLinkW * 0.13f;

			// image background
			GUI.color = new Color(1f, 1f, 1f, 0.8f * levelCompleteOpacity);
			GUI.Box(new Rect(innerX, innerY + imageOffsetY, innerW, imageHeight + inset*2), "");
			GUI.color = new Color(1f, 1f, 1f, 0.4f * levelCompleteOpacity);
			GUI.Box(new Rect(innerX, innerY + imageOffsetY, innerW, imageHeight + inset*2), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * levelCompleteOpacity);
			
			// image
			GUI.color = new Color(1f, 1f, 1f, levelCompleteOpacity * imageOpacity);
			GUI.DrawTexture(new Rect(statsLinkX + statsLinkW/2 - imageWidth/2, inset + innerY + imageOffsetY, imageWidth, imageHeight), imageTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * levelCompleteOpacity);
		

			// level text	
			string titleText = "empty text";
			Color labelColor = new Color(0f, 0f, 0f);
			
			switch (levelManager.GetCurrentLevel()) {
			case 0:
				titleText = "WILD NATURE";
				labelColor = new Color(0.2f, 0.7f, 0.14f);
				break;
			case 1:
				titleText = "HUMAN ARRIVAL";
				labelColor = new Color(0.85f, 0.66f, 0.0f);
				break;
			case 2:
				titleText = "DEVELOPMENT";
				labelColor = new Color(1f, 0.5f, 0.0f);
				break;
			case 3:
				titleText = "FRAGMENTATION";
				labelColor = new Color(1f, 0.2f, 0.0f);
				break;
			case 4:
				titleText = "CONNECTIVITY";
				labelColor = new Color(0.14f, 0.7f, 0.14f);
				break;
			}

			style.alignment = TextAnchor.MiddleLeft;
			style.normal.textColor = new Color(0.90f, 0.65f, 0f, 0.8f);
			style.fontSize = (int)(fontRef * 0.135f);
			GUI.Button(new Rect(innerX + inset, statsLinkY + statsLinkW * 0.023f, innerW - inset * 2, statsLinkH * 0.2f), "Level" + (levelManager.GetCurrentLevel() + 1), style);
			style.alignment = TextAnchor.MiddleRight;
			style.normal.textColor = labelColor;
			style.fontSize = (int)(fontRef * 0.12f);
			GUI.Button(new Rect(innerX + inset, statsLinkY + statsLinkW * 0.023f, innerW - inset * 2, statsLinkH * 0.2f), titleText, style);
			style.alignment = TextAnchor.MiddleCenter;


			// hunt success flashing rect	
			float flashingPeriod = 0.4f;
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
			flashingOpacity = 0.3f + flashingOpacity * 0.7f;
			flashingOpacity = flashingOpacity * flashingOpacity;
			GUI.color = new Color(1f, 1f, 1f, flashingOpacity * 0.8f * levelCompleteOpacity);
			GUI.DrawTexture(new Rect(statsLinkX, statsLinkY, statsLinkW, statsLinkH), greenOutlineRectTexture);

			style.normal.textColor = new Color(0f, 0.8f, 0f, 1f);
			style.fontSize = (int)(fontRef * 0.14f);
			GUI.color = new Color(1f, 1f, 1f, (flashingOpacity*0.65f + 0.35f) * levelCompleteOpacity);
			GUI.Button(new Rect(innerX + inset, statsLinkY + statsLinkW * 0.825f, innerW - inset * 2, statsLinkH * 0.2f), "Complete !", style);

			GUI.color = new Color(1f, 1f, 1f, 1f * levelCompleteOpacity);
		}
		

		//********************
		// 'OK' BUTTON
		//********************
					
		GUI.color = new Color(1f, 1f, 1f, 1f * okButtonOpacity);

		feedingDisplayX -= feedingDisplayWidth * 0.02f;
		feedingDisplayY += feedingDisplayHeight * 1.3f;

		GUI.color = new Color(1f, 1f, 1f, 0.8f * okButtonOpacity);
		GUI.Box(new Rect(feedingDisplayX + feedingDisplayWidth * 0.78f, feedingDisplayY + feedingDisplayHeight * 0.67f, feedingDisplayWidth * 0.20f, feedingDisplayHeight * 0.37f), "");
		GUI.color = new Color(1f, 1f, 1f, 0.6f * okButtonOpacity);
		GUI.Box(new Rect(feedingDisplayX + feedingDisplayWidth * 0.78f, feedingDisplayY + feedingDisplayHeight * 0.67f, feedingDisplayWidth * 0.20f, feedingDisplayHeight * 0.37f), "");
		GUI.color = new Color(1f, 1f, 1f, 1f * okButtonOpacity);

		GUI.color = new Color(1f, 1f, 1f, 0.8f * okButtonOpacity);
		GUI.Box(new Rect(feedingDisplayX + feedingDisplayWidth * 0.81f,  feedingDisplayY + feedingDisplayHeight * 0.727f, feedingDisplayWidth * 0.14f, feedingDisplayHeight * 0.25f), "");
		GUI.color = new Color(1f, 1f, 1f, 0.8f * okButtonOpacity);
		GUI.Box(new Rect(feedingDisplayX + feedingDisplayWidth * 0.81f,  feedingDisplayY + feedingDisplayHeight * 0.727f, feedingDisplayWidth * 0.14f, feedingDisplayHeight * 0.25f), "");
		GUI.color = new Color(1f, 1f, 1f, 1f * okButtonOpacity);

		GUI.skin = guiManager.customGUISkin;
		guiManager.customGUISkin.button.fontSize = (int)(feedingDisplayHeight * 0.14);
		guiManager.customGUISkin.button.fontStyle = FontStyle.Normal;
		if (GUI.Button(new Rect(feedingDisplayX + feedingDisplayWidth * 0.81f,  feedingDisplayY + feedingDisplayHeight * 0.727f, feedingDisplayWidth * 0.14f, feedingDisplayHeight * 0.25f), "Go")) {
			if (scoringSystem.GetHuntSuccessCount() >= 3) {
				// use keyboard to go to next level
				guiManager.SetGuiState("guiStateNextLevel1");
				levelManager.SetGameState("gameStateLeavingGameplayA");
			}
			else {
				// use keyboard to resume gameplay
				guiManager.SetGuiState("guiStateFeeding7");
				levelManager.SetGameState("gameStateFeeding5");
			}
		}	
		
		feedingDisplayX += feedingDisplayWidth * 0.02f;
		feedingDisplayY -= feedingDisplayHeight * 1.3f;
	}
	
}