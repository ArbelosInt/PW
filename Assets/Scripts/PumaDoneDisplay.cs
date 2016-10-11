using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// PumaDoneDisplay
/// Draw the screen that comes up when puma dies or reaches full health

public class PumaDoneDisplay : MonoBehaviour
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
	private Texture2D closeup1Texture;
	private Texture2D closeup2Texture;
	private Texture2D closeup3Texture;
	private Texture2D closeup4Texture;
	private Texture2D closeup5Texture;
	private Texture2D closeup6Texture;
	private Texture2D pumaCrossbonesRedTexture;
	private Texture2D greenHeartTexture;

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
		closeup1Texture = guiManager.closeup1Texture;
		closeup2Texture = guiManager.closeup2Texture;
		closeup3Texture = guiManager.closeup3Texture;
		closeup4Texture = guiManager.closeup4Texture;
		closeup5Texture = guiManager.closeup5Texture;
		closeup6Texture = guiManager.closeup6Texture;
		pumaCrossbonesRedTexture = guiManager.pumaCrossbonesRedTexture;		
		greenHeartTexture = guiManager.greenHeartTexture;		
		
		// create and position GUI elements
		CreateGUIItems();
		PositionGUIItems("threatTypeStarvation");
		lastSeenScreenWidth = Screen.width;
		lastSeenScreenHeight = Screen.height;
	}


	//===========================
	//===========================
	//	  GUI ELEMENTS
	//===========================
	//===========================
	
	public GameObject pumaDoneMainPanel;
	public GameObject pumaDoneOkButton;

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
	
	// left side text
	private GameObject leftTextBackground;
	private GameObject leftText1;
	private GameObject leftText2;
	private GameObject leftText3;
	private GameObject leftText4;
	private GameObject leftText5;
	private GameObject leftTextGameOver;
	
	// center area
	private GameObject centerBackground;
	private GameObject pumaImage;
	private GameObject pumaName;
	private GameObject crossbonesLeft;
	private GameObject crossbonesRight;
	private GameObject healthBar;
		
	// right side text and button
	private GameObject rightTextBackground;
	private GameObject rightText1;
	private GameObject rightText2;
	private GameObject rightText3;
	private GameObject rightText4;
	private GameObject rightText5;
	private GameObject rightButtonBackground;
	private GameObject rightButtonHunting;
	private GameObject rightButtonSurvival;

	// ok button
	private GameObject okBackground;
	private GameObject okButton;
	
	
	void CreateGUIItems()
	{
		// set enables to 'off' before populating sub-items
		pumaDoneMainPanel.SetActive(false);
		pumaDoneOkButton.SetActive(false);
		
		// background and title
		mainBackground = 		guiUtils.CreatePanel(pumaDoneMainPanel, new Color(0f, 0f, 0f, 0.4f * 1.1f));
		upperBackground = 		guiUtils.CreatePanel(pumaDoneMainPanel, new Color(0f, 0f, 0f, 0.4f * 0.8f));
		titleBackground = 		guiUtils.CreatePanel(pumaDoneMainPanel, new Color(0f, 0f, 0f, 0.4f * 1.1f));
		titleText = 			guiUtils.CreateText(pumaDoneMainPanel, "text", new Color(0.75f, 0f, 0f, 1f), FontStyle.Bold);
		
		// left side text
		leftTextBackground = 	guiUtils.CreatePanel(pumaDoneMainPanel, new Color(0f, 0f, 0f, 0.4f));
		leftText1 = 			guiUtils.CreateText(pumaDoneMainPanel, "text", new Color(0f, 0f, 0f, 1f), FontStyle.Bold, TextAnchor.MiddleLeft);
		leftText2 = 			guiUtils.CreateText(pumaDoneMainPanel, "text", new Color(0f, 0f, 0f, 1f), FontStyle.Bold, TextAnchor.MiddleLeft);
		leftText3 = 			guiUtils.CreateText(pumaDoneMainPanel, "text", new Color(0f, 0f, 0f, 1f), FontStyle.Bold, TextAnchor.MiddleLeft);
		leftText4 = 			guiUtils.CreateText(pumaDoneMainPanel, "text", new Color(0f, 0f, 0f, 1f), FontStyle.Bold, TextAnchor.MiddleLeft);
		leftText5 = 			guiUtils.CreateText(pumaDoneMainPanel, "text", new Color(0f, 0f, 0f, 1f), FontStyle.Bold, TextAnchor.MiddleLeft);
		leftTextGameOver = 		guiUtils.CreateText(pumaDoneMainPanel, "- GAME OVER -", new Color(0.75f, 0.75f, 0.75f, 1f), FontStyle.Bold, TextAnchor.MiddleLeft);
		
		// center area
		centerBackground = 		guiUtils.CreatePanel(pumaDoneMainPanel, new Color(0f, 0f, 0f, 0.4f * 1.4f));
		pumaImage = 			guiUtils.CreateImage(pumaDoneMainPanel, closeup1Texture, new Color(0.9f, 0.2f, 0.2f, 1f));
		pumaName = 				guiUtils.CreateText(pumaDoneMainPanel, "Eric", new Color(0.6f, 0.6f, 0.6f, 1f), FontStyle.BoldAndItalic);
		crossbonesLeft = 		guiUtils.CreateImage(pumaDoneMainPanel, pumaCrossbonesRedTexture, new Color(0.8f, 0.8f, 0.8f, 1f));
		crossbonesRight = 		guiUtils.CreateImage(pumaDoneMainPanel, pumaCrossbonesRedTexture, new Color(0.8f, 0.8f, 0.8f, 1f));
		healthBar =				guiComponents.CreatePumaHealthBar(pumaDoneMainPanel);
		
		// right side text and button
		rightTextBackground = 	guiUtils.CreatePanel(pumaDoneMainPanel, new Color(0f, 0f, 0f, 0.4f));
		rightText1 = 			guiUtils.CreateText(pumaDoneMainPanel, "text", new Color(0f, 0f, 0f, 1f), FontStyle.Bold, TextAnchor.MiddleLeft);
		rightText2 = 			guiUtils.CreateText(pumaDoneMainPanel, "text", new Color(0f, 0f, 0f, 1f), FontStyle.Bold, TextAnchor.MiddleLeft);
		rightText3 = 			guiUtils.CreateText(pumaDoneMainPanel, "text", new Color(0f, 0f, 0f, 1f), FontStyle.Bold, TextAnchor.MiddleLeft);
		rightText4 = 			guiUtils.CreateText(pumaDoneMainPanel, "text", new Color(0f, 0f, 0f, 1f), FontStyle.Bold, TextAnchor.MiddleLeft);
		rightText5 = 			guiUtils.CreateText(pumaDoneMainPanel, "text", new Color(0f, 0f, 0f, 1f), FontStyle.Bold, TextAnchor.MiddleLeft);
		rightButtonBackground = guiUtils.CreatePanel(pumaDoneMainPanel, new Color(0f, 0f, 0f, 0.4f));
		rightButtonHunting = 	guiUtils.CreateButton(pumaDoneMainPanel, "Hunting Tips", new Color(230f/255f, 166f/255f, 0f, 1f), FontStyle.Bold);
		rightButtonHunting.GetComponent<Button>().onClick.AddListener( delegate { guiManager.OpenInfoPanel(1); } );
		rightButtonSurvival = 	guiUtils.CreateButton(pumaDoneMainPanel, "Survival Tips", new Color(230f/255f, 166f/255f, 0f, 1f), FontStyle.Bold);
		rightButtonSurvival.GetComponent<Button>().onClick.AddListener( delegate { guiManager.OpenInfoPanel(3); } );

		// ok button
		okBackground = 			guiUtils.CreatePanel(pumaDoneOkButton, new Color(0f, 0f, 0f, 0.4f * 1.4f));
		okButton = 				guiUtils.CreateButton(pumaDoneOkButton, "Go", new Color(230f/255f, 166f/255f, 0f, 1f), FontStyle.Normal);
		okButton.GetComponent<Button>().onClick.AddListener( delegate {
			guiManager.SetGuiState("guiStateLeavingPumaDone");
			if (levelManager.CheckCarCollision() == true)
				levelManager.EndCarCollision();
			else if (levelManager.CheckStarvation() == true)
				levelManager.EndStarvation();		
			levelManager.SetGameState("gameStateLeavingGameplay");
		} );

		initComplete = true;
	}


	void PositionGUIItems(string threatType)
	{
		if (initComplete == false)
			return;
	
		float pumaDoneDisplayX = (Screen.width / 2) - (Screen.height * 0.6f);
		float pumaDoneDisplayY = Screen.height * 0.025f;
		float pumaDoneDisplayWidth = Screen.height * 1.2f;
		float pumaDoneDisplayHeight = Screen.height * 0.37f;
		float leftShiftAmount = pumaDoneDisplayWidth * -0.32f;
		float vertShiftAmount = pumaDoneDisplayHeight * 0.05f;
		float fontRef = pumaDoneDisplayHeight * 0.5f;
		float panelOffsetY = -0.1f;
		float textureX;
		float textureY;
		float textureWidth;
		float textureHeight;
		float leftLabelX;
		float leftLabelY1;
		float leftLabelY2;
		float leftLabelY3;
		float leftLabelY4;
		float leftLabelY5;
		float leftLabelW;
		float leftLabelH;
		float rightLabelX;
		float rightLabelY1;
		float rightLabelY2;
		float rightLabelY3;
		float rightLabelY4;
		float rightLabelY5;
		float rightLabelW;
		float rightLabelH;

		// background and title
		guiUtils.SetItemOffsets(mainBackground, pumaDoneDisplayX, pumaDoneDisplayY + pumaDoneDisplayHeight * 0.06f, pumaDoneDisplayWidth, pumaDoneDisplayHeight * 1.2f - pumaDoneDisplayHeight * 0.06f);
		guiUtils.SetItemOffsets(upperBackground, pumaDoneDisplayX, pumaDoneDisplayY + pumaDoneDisplayHeight * 0.06f, pumaDoneDisplayWidth, pumaDoneDisplayHeight * 0.17f);
		guiUtils.SetItemOffsets(titleBackground, pumaDoneDisplayX + pumaDoneDisplayWidth * 0.26f, pumaDoneDisplayY + pumaDoneDisplayHeight * 0.094f, pumaDoneDisplayWidth * 0.48f, pumaDoneDisplayHeight * 0.11f);
		guiUtils.SetTextOffsets(titleText, pumaDoneDisplayX, pumaDoneDisplayY, pumaDoneDisplayWidth, pumaDoneDisplayHeight * 0.3f, (int)(fontRef * 0.18f));

		// left side background
		guiUtils.SetItemOffsets(leftTextBackground, pumaDoneDisplayX + pumaDoneDisplayWidth/2 - pumaDoneDisplayWidth * 0.22f/2 - pumaDoneDisplayWidth * 0.35f, pumaDoneDisplayY + pumaDoneDisplayHeight * 0.30f, pumaDoneDisplayWidth * 0.22f, pumaDoneDisplayHeight * 0.8f);

		// center area
		guiUtils.SetItemOffsets(centerBackground, pumaDoneDisplayX + pumaDoneDisplayWidth * 0.3f, pumaDoneDisplayY + pumaDoneDisplayHeight * 0.3f, pumaDoneDisplayWidth * 0.4f, pumaDoneDisplayHeight * 0.8f);
		float headshotTextureWidth = pumaImage.GetComponent<RawImage>().texture.width;
		float headshotTextureHeight = pumaImage.GetComponent<RawImage>().texture.height;
		guiUtils.SetItemOffsets(pumaImage, pumaDoneDisplayX + pumaDoneDisplayWidth * 0.77f + leftShiftAmount, pumaDoneDisplayY + pumaDoneDisplayHeight * (0.42f + panelOffsetY) + vertShiftAmount, pumaDoneDisplayHeight * 0.39f, headshotTextureHeight * ((pumaDoneDisplayHeight * 0.39f) / headshotTextureWidth));
		guiUtils.SetTextOffsets(pumaName, pumaDoneDisplayX, pumaDoneDisplayY + pumaDoneDisplayHeight * 0.72f, pumaDoneDisplayWidth, pumaDoneDisplayHeight * 0.1f, (int)(fontRef * 0.16f));
		textureY = pumaDoneDisplayY + pumaDoneDisplayHeight * (0.5f + panelOffsetY) + vertShiftAmount;
		textureWidth = pumaDoneDisplayHeight * 0.3f;
		textureHeight = pumaCrossbonesRedTexture.height * (textureWidth / pumaCrossbonesRedTexture.width);
		textureX = pumaDoneDisplayX + pumaDoneDisplayWidth/2  - textureWidth/2 - pumaDoneDisplayWidth * 0.13f;
		guiUtils.SetItemOffsets(crossbonesLeft, textureX, textureY, textureWidth, textureHeight);
		textureX = pumaDoneDisplayX + pumaDoneDisplayWidth/2  - textureWidth/2 + pumaDoneDisplayWidth * 0.13f;
		guiUtils.SetItemOffsets(crossbonesRight, textureX, textureY, textureWidth, textureHeight);
		float barX = pumaDoneDisplayX + pumaDoneDisplayWidth * 0.670f + pumaDoneDisplayHeight * 0.03f + leftShiftAmount;
		float barY = pumaDoneDisplayY + pumaDoneDisplayHeight * 0.83f + vertShiftAmount;
		float barW = pumaDoneDisplayWidth * 0.29f - pumaDoneDisplayHeight * 0.06f;
		float barH = pumaDoneDisplayHeight * 0.11f;
		guiComponents.PositionPumaHealthBar(healthBar, guiManager.selectedPuma, barX, barY, barW, barH, true);

		// right side backgrounds
		guiUtils.SetItemOffsets(rightTextBackground, pumaDoneDisplayX + pumaDoneDisplayWidth/2 - pumaDoneDisplayWidth * 0.22f/2 + pumaDoneDisplayWidth * 0.35f, pumaDoneDisplayY + pumaDoneDisplayHeight * 0.30f, pumaDoneDisplayWidth * 0.22f, pumaDoneDisplayHeight * ((scoringSystem.GetPopulationHealth() > 0f) ? 0.51f : 0.8f));
		
		// right and left text
		if (threatType == "threatTypeStarvation" && scoringSystem.GetPopulationHealth() > 0f) {
			// starvation
			leftLabelX = pumaDoneDisplayX + pumaDoneDisplayWidth * 0.375f + leftShiftAmount;
			leftLabelY1 = pumaDoneDisplayY + pumaDoneDisplayHeight * (0.48f + panelOffsetY) + vertShiftAmount;
			leftLabelY2 = pumaDoneDisplayY + pumaDoneDisplayHeight * (0.59f + panelOffsetY) + vertShiftAmount;
			leftLabelY3 = pumaDoneDisplayY + pumaDoneDisplayHeight * (0.70f + panelOffsetY) + vertShiftAmount;
			leftLabelY4 = pumaDoneDisplayY + pumaDoneDisplayHeight * (0.88f + panelOffsetY) + vertShiftAmount;
			leftLabelY5 = pumaDoneDisplayY + pumaDoneDisplayHeight * (0.99f + panelOffsetY) + vertShiftAmount;
			leftLabelW = pumaDoneDisplayWidth * 0.1f;
			leftLabelH = pumaDoneDisplayHeight * 0.03f;

			guiUtils.SetTextOffsets(leftText1, leftLabelX, leftLabelY1, leftLabelW, leftLabelH, (int)(fontRef * 0.16f));
			guiUtils.SetTextOffsets(leftText2, leftLabelX, leftLabelY2, leftLabelW, leftLabelH, (int)(fontRef * 0.16f));
			guiUtils.SetTextOffsets(leftText3, leftLabelX, leftLabelY3, leftLabelW, leftLabelH, (int)(fontRef * 0.16f));
			guiUtils.SetTextOffsets(leftText4, leftLabelX, leftLabelY4, leftLabelW, leftLabelH, (int)(fontRef * 0.16f));
			guiUtils.SetTextOffsets(leftText5, leftLabelX, leftLabelY5, leftLabelW, leftLabelH, (int)(fontRef * 0.16f));

			rightLabelX = pumaDoneDisplayX + pumaDoneDisplayWidth * 0.775f;
			rightLabelY1 = pumaDoneDisplayY + pumaDoneDisplayHeight * (0.48f + panelOffsetY) + vertShiftAmount;
			rightLabelY2 = pumaDoneDisplayY + pumaDoneDisplayHeight * (0.59f + panelOffsetY) + vertShiftAmount;
			rightLabelY3 = pumaDoneDisplayY + pumaDoneDisplayHeight * (0.70f + panelOffsetY) + vertShiftAmount;
			rightLabelW = pumaDoneDisplayWidth * 0.1f;
			rightLabelH = pumaDoneDisplayHeight * 0.06f;

			guiUtils.SetTextOffsets(rightText1, rightLabelX, rightLabelY1, rightLabelW, rightLabelH, (int)(fontRef * 0.16f));
			guiUtils.SetTextOffsets(rightText2, rightLabelX, rightLabelY2, rightLabelW, rightLabelH, (int)(fontRef * 0.16f));
			guiUtils.SetTextOffsets(rightText3, rightLabelX, rightLabelY3, rightLabelW, rightLabelH, (int)(fontRef * 0.16f));
		}
		else if (threatType == "threatTypeVehicle" && scoringSystem.GetPopulationHealth() > 0f) {	
			// vehicle
			leftLabelX = pumaDoneDisplayX + pumaDoneDisplayWidth * 0.385f + leftShiftAmount;
			leftLabelY1 = pumaDoneDisplayY + pumaDoneDisplayHeight * (0.50f + panelOffsetY) + vertShiftAmount;
			leftLabelY2 = pumaDoneDisplayY + pumaDoneDisplayHeight * (0.61f + panelOffsetY) + vertShiftAmount;
			leftLabelY3 = pumaDoneDisplayY + pumaDoneDisplayHeight * (0.72f + panelOffsetY) + vertShiftAmount;
			leftLabelY4 = pumaDoneDisplayY + pumaDoneDisplayHeight * (0.83f + panelOffsetY) + vertShiftAmount;
			leftLabelY5 = pumaDoneDisplayY + pumaDoneDisplayHeight * (0.94f + panelOffsetY) + vertShiftAmount;
			leftLabelW = pumaDoneDisplayWidth * 0.1f;
			leftLabelH = pumaDoneDisplayHeight * 0.03f;

			guiUtils.SetTextOffsets(leftText1, leftLabelX, leftLabelY1, leftLabelW, leftLabelH, (int)(fontRef * 0.16f));
			guiUtils.SetTextOffsets(leftText2, leftLabelX, leftLabelY2, leftLabelW, leftLabelH, (int)(fontRef * 0.16f));
			guiUtils.SetTextOffsets(leftText3, leftLabelX, leftLabelY3, leftLabelW, leftLabelH, (int)(fontRef * 0.16f));
			guiUtils.SetTextOffsets(leftText4, leftLabelX, leftLabelY4, leftLabelW, leftLabelH, (int)(fontRef * 0.16f));
			guiUtils.SetTextOffsets(leftText5, leftLabelX, leftLabelY5, leftLabelW, leftLabelH, (int)(fontRef * 0.16f));

			rightLabelX = pumaDoneDisplayX + pumaDoneDisplayWidth * 0.765f;
			rightLabelY1 = pumaDoneDisplayY + pumaDoneDisplayHeight * (0.48f + panelOffsetY) + vertShiftAmount;
			rightLabelY2 = pumaDoneDisplayY + pumaDoneDisplayHeight * (0.59f + panelOffsetY) + vertShiftAmount;
			rightLabelY3 = pumaDoneDisplayY + pumaDoneDisplayHeight * (0.70f + panelOffsetY) + vertShiftAmount;
			rightLabelW = pumaDoneDisplayWidth * 0.1f;
			rightLabelH = pumaDoneDisplayHeight * 0.03f;

			guiUtils.SetTextOffsets(rightText1, rightLabelX, rightLabelY1, rightLabelW, rightLabelH, (int)(fontRef * 0.16f));
			guiUtils.SetTextOffsets(rightText2, rightLabelX, rightLabelY2, rightLabelW, rightLabelH, (int)(fontRef * 0.16f));
			guiUtils.SetTextOffsets(rightText3, rightLabelX, rightLabelY3, rightLabelW, rightLabelH, (int)(fontRef * 0.16f));
		}
		else {
			// extinction
			leftLabelX = pumaDoneDisplayX + pumaDoneDisplayWidth * 0.375f + leftShiftAmount;
			leftLabelY1 = pumaDoneDisplayY + pumaDoneDisplayHeight * (0.48f + panelOffsetY) + vertShiftAmount;
			leftLabelY2 = pumaDoneDisplayY + pumaDoneDisplayHeight * (0.63f + panelOffsetY) + vertShiftAmount;
			leftLabelY3 = pumaDoneDisplayY + pumaDoneDisplayHeight * (0.74f + panelOffsetY) + vertShiftAmount;
			leftLabelY4 = pumaDoneDisplayY + pumaDoneDisplayHeight * (0.89f + panelOffsetY) + vertShiftAmount;
			leftLabelY5 = pumaDoneDisplayY + pumaDoneDisplayHeight * (0.99f + panelOffsetY) + vertShiftAmount;
			leftLabelW = pumaDoneDisplayWidth * 0.1f;
			leftLabelH = pumaDoneDisplayHeight * 0.03f;

			guiUtils.SetTextOffsets(leftTextGameOver, leftLabelX, leftLabelY1, leftLabelW, leftLabelH, (int)(fontRef * 0.17f));
			guiUtils.SetTextOffsets(leftText2, leftLabelX, leftLabelY2, leftLabelW, leftLabelH, (int)(fontRef * 0.15f));
			guiUtils.SetTextOffsets(leftText3, leftLabelX, leftLabelY3, leftLabelW, leftLabelH, (int)(fontRef * 0.15f));
			guiUtils.SetTextOffsets(leftText4, leftLabelX, leftLabelY4, leftLabelW, leftLabelH, (int)(fontRef * 0.15f));
			guiUtils.SetTextOffsets(leftText5, leftLabelX, leftLabelY5, leftLabelW, leftLabelH, (int)(fontRef * 0.15f));

			rightLabelX = pumaDoneDisplayX + pumaDoneDisplayWidth * 0.758f;
			rightLabelY1 = pumaDoneDisplayY + pumaDoneDisplayHeight * (0.48f + panelOffsetY) + vertShiftAmount;
			rightLabelY2 = pumaDoneDisplayY + pumaDoneDisplayHeight * (0.595f + panelOffsetY) + vertShiftAmount;
			rightLabelY3 = pumaDoneDisplayY + pumaDoneDisplayHeight * (0.695f + panelOffsetY) + vertShiftAmount;
			rightLabelY4 = pumaDoneDisplayY + pumaDoneDisplayHeight * (0.88f + panelOffsetY) + vertShiftAmount;
			rightLabelY5 = pumaDoneDisplayY + pumaDoneDisplayHeight * (0.99f + panelOffsetY) + vertShiftAmount;
			rightLabelW = pumaDoneDisplayWidth * 0.1f;
			rightLabelH = pumaDoneDisplayHeight * 0.03f;

			guiUtils.SetTextOffsets(rightText1, rightLabelX, rightLabelY1, rightLabelW, rightLabelH, (int)(fontRef * 0.14f));
			guiUtils.SetTextOffsets(rightText2, rightLabelX, rightLabelY2, rightLabelW, rightLabelH, (int)(fontRef * 0.16f));
			guiUtils.SetTextOffsets(rightText3, rightLabelX, rightLabelY3, rightLabelW, rightLabelH, (int)(fontRef * 0.13f));
			guiUtils.SetTextOffsets(rightText4, rightLabelX, rightLabelY4, rightLabelW, rightLabelH, (int)(fontRef * 0.14f));
			guiUtils.SetTextOffsets(rightText5, rightLabelX, rightLabelY5, rightLabelW, rightLabelH, (int)(fontRef * 0.16f));
		}
		
		// right side button
		float boxY = pumaDoneDisplayY + pumaDoneDisplayHeight * 0.86f;
		float boxW = pumaDoneDisplayWidth * 0.22f;
		float boxH = pumaDoneDisplayHeight * 0.24f;
		float boxX = pumaDoneDisplayX + pumaDoneDisplayWidth/2 - boxW/2 + pumaDoneDisplayWidth * 0.35f;
		guiUtils.SetItemOffsets(rightButtonBackground, boxX, boxY, boxW, boxH);
		guiUtils.SetButtonOffsets(rightButtonHunting, pumaDoneDisplayX + pumaDoneDisplayWidth * 0.77f,  pumaDoneDisplayY + pumaDoneDisplayHeight * 0.9f, pumaDoneDisplayWidth * 0.16f, pumaDoneDisplayHeight * 0.15f, (int)(pumaDoneDisplayHeight * 0.070));
		guiUtils.SetButtonOffsets(rightButtonSurvival, pumaDoneDisplayX + pumaDoneDisplayWidth * 0.77f,  pumaDoneDisplayY + pumaDoneDisplayHeight * 0.9f, pumaDoneDisplayWidth * 0.16f, pumaDoneDisplayHeight * 0.15f, (int)(pumaDoneDisplayHeight * 0.070));
		
		// ok button
		pumaDoneDisplayX -= pumaDoneDisplayWidth * 0.02f;
		pumaDoneDisplayY += pumaDoneDisplayHeight * 1.3f;
		guiUtils.SetItemOffsets(okBackground, pumaDoneDisplayX + pumaDoneDisplayWidth * 0.78f, pumaDoneDisplayY + pumaDoneDisplayHeight * 0.67f, pumaDoneDisplayWidth * 0.20f, pumaDoneDisplayHeight * 0.37f);
		guiUtils.SetButtonOffsets(okButton, pumaDoneDisplayX + pumaDoneDisplayWidth * 0.81f,  pumaDoneDisplayY + pumaDoneDisplayHeight * 0.727f, pumaDoneDisplayWidth * 0.14f, pumaDoneDisplayHeight * 0.25f, (int)(pumaDoneDisplayHeight * 0.14));
	}
	
	
	public void PrepareGUIItems(string threatType)  // called once per showing, right before panel fades in
	{
		if (initComplete == false)
			return;
	
		// title
		titleText.GetComponent<Text>().text = (threatType == "threatTypeStarvation") ? "STARVED TO DEATH!" : "KILLED BY A VEHICLE!";
		
		// center area -- puma identity
		Texture2D headshotTexture = closeup1Texture;
		string pumaNameString = "no name";
		switch (guiManager.selectedPuma) {
		case 0:
			headshotTexture = closeup1Texture;
			pumaNameString = "Eric";
			break;
		case 1:
			headshotTexture = closeup2Texture;
			pumaNameString = "Palo";
			break;
		case 2:
			headshotTexture = closeup3Texture;
			pumaNameString = "Mitch";
			break;
		case 3:
			headshotTexture = closeup4Texture;
			pumaNameString = "Trish";
			break;
		case 4:
			headshotTexture = closeup5Texture;
			pumaNameString = "Liam";
			break;
		case 5:
			headshotTexture = closeup6Texture;
			pumaNameString = "Barb";
			break;
		}
		pumaImage.GetComponent<RawImage>().texture = headshotTexture;
		pumaName.GetComponent<Text>().text = pumaNameString;
		
		// right and left text
		if (threatType == "threatTypeStarvation" && scoringSystem.GetPopulationHealth() > 0f) {
			// starvation
			leftText1.SetActive(true);
			leftTextGameOver.SetActive(false);
			leftText1.GetComponent<Text>().text = "Deer provide the";
			leftText2.GetComponent<Text>().text = "main puma prey";
			leftText3.GetComponent<Text>().text = "in North America";
			leftText4.GetComponent<Text>().text = "Pumas help limit";
			leftText5.GetComponent<Text>().text = "deer populations";

			rightText1.GetComponent<Text>().text = "Pumas need";
			rightText2.GetComponent<Text>().text = "to hunt smart";
			rightText3.GetComponent<Text>().text = "to catch deer";
			rightText3.GetComponent<Text>().fontStyle = FontStyle.Bold;
			rightText4.SetActive(false);
			rightText5.SetActive(false);

			leftText1.GetComponent<Text>().color = new Color(0.75f, 0.75f, 0.75f, 1f);
			leftText2.GetComponent<Text>().color = new Color(0.75f, 0.75f, 0.75f, 1f);
			leftText3.GetComponent<Text>().color = new Color(0.75f, 0.75f, 0.75f, 1f);
			leftText4.GetComponent<Text>().color = new Color(0.75f, 0.75f, 0.75f, 1f);
			leftText5.GetComponent<Text>().color = new Color(0.75f, 0.75f, 0.75f, 1f);

			rightText1.GetComponent<Text>().color = new Color(0.75f, 0.75f, 0.75f, 1f);
			rightText2.GetComponent<Text>().color = new Color(0.75f, 0.75f, 0.75f, 1f);
			rightText3.GetComponent<Text>().color = new Color(0.75f, 0.75f, 0.75f, 1f);

			// right side button
			rightButtonBackground.SetActive(true);
			rightButtonHunting.SetActive(true);
			rightButtonSurvival.SetActive(false);
		}
		else if (threatType == "threatTypeVehicle" && scoringSystem.GetPopulationHealth() > 0f) {	
			// vehicle
			leftText1.SetActive(true);
			leftTextGameOver.SetActive(false);
			leftText1.GetComponent<Text>().text = "Over 60 pumas";
			leftText2.GetComponent<Text>().text = "die each year";
			leftText3.GetComponent<Text>().text = "in California";
			leftText4.GetComponent<Text>().text = "from collisions";
			leftText5.GetComponent<Text>().text = "with vehicles";

			rightText1.GetComponent<Text>().text = "Pumas need to";
			rightText2.GetComponent<Text>().text = "move carefully";
			rightText3.GetComponent<Text>().text = "to avoid cars";
			rightText3.GetComponent<Text>().fontStyle = FontStyle.Bold;
			rightText4.SetActive(false);
			rightText5.SetActive(false);

			leftText1.GetComponent<Text>().color = new Color(0.75f, 0.75f, 0.75f, 1f);
			leftText2.GetComponent<Text>().color = new Color(0.75f, 0.75f, 0.75f, 1f);
			leftText3.GetComponent<Text>().color = new Color(0.75f, 0.75f, 0.75f, 1f);
			leftText4.GetComponent<Text>().color = new Color(0.75f, 0.75f, 0.75f, 1f);
			leftText5.GetComponent<Text>().color = new Color(0.75f, 0.75f, 0.75f, 1f);

			rightText1.GetComponent<Text>().color = new Color(0.75f, 0.75f, 0.75f, 1f);
			rightText2.GetComponent<Text>().color = new Color(0.75f, 0.75f, 0.75f, 1f);
			rightText3.GetComponent<Text>().color = new Color(0.75f, 0.75f, 0.75f, 1f);

			// right side button
			rightButtonBackground.SetActive(true);
			rightButtonHunting.SetActive(false);
			rightButtonSurvival.SetActive(true);
		}
		else {
			// extinction
			leftText1.SetActive(false);
			leftTextGameOver.SetActive(true);
			leftText2.GetComponent<Text>().text = "The last puma in";
			leftText3.GetComponent<Text>().text = "the area has died";
			leftText4.GetComponent<Text>().text = "Local population";
			leftText5.GetComponent<Text>().text = "is now extinct!";

			rightText1.GetComponent<Text>().text = "BIG WINNER:";
			rightText2.GetComponent<Text>().text = "The Deer";
			rightText3.GetComponent<Text>().text = "can graze anywhere";
			rightText3.GetComponent<Text>().fontStyle = FontStyle.BoldAndItalic;
			rightText4.SetActive(true);
			rightText5.SetActive(true);
			rightText4.GetComponent<Text>().text = "BIG LOSER:";
			rightText5.GetComponent<Text>().text = "The Ecosystem";

			leftText2.GetComponent<Text>().color = new Color(0.75f, 0.75f, 0.75f, 1f);
			leftText3.GetComponent<Text>().color = new Color(0.75f, 0.75f, 0.75f, 1f);
			leftText4.GetComponent<Text>().color = new Color(0.75f, 0.65f, 0.55f, 1f);
			leftText5.GetComponent<Text>().color = new Color(0.75f, 0.65f, 0.55f, 1f);

			rightText1.GetComponent<Text>().color = new Color(0.65f, 0.60f, 0.55f, 1f);
			rightText2.GetComponent<Text>().color = new Color(0.85f, 0.85f, 0.85f, 1f);
			rightText3.GetComponent<Text>().color = new Color(0.75f, 0.75f, 0.75f, 1f);
			rightText4.GetComponent<Text>().color = new Color(0.65f, 0.60f, 0.55f, 1f);
			rightText5.GetComponent<Text>().color = new Color(0.85f, 0.85f, 0.85f, 1f);

			// right side button
			rightButtonBackground.SetActive(false);
			rightButtonHunting.SetActive(false);
			rightButtonSurvival.SetActive(false);
		}
			
		PositionGUIItems(threatType);
	}
	
	
	public void UpdateGUIItems(float pumaDonePanelOpacity, string threatType, float okButtonOpacity) 
	{ 
		if (USE_NEW_GUI == true) {

			// check for screen size change
			if (lastSeenScreenWidth != Screen.width || lastSeenScreenHeight != Screen.height) {
				lastSeenScreenWidth = Screen.width;
				lastSeenScreenHeight = Screen.height;
				PositionGUIItems(threatType);
			}

			// top level enables and opacities
			pumaDoneMainPanel.SetActive(pumaDonePanelOpacity > 0f ? true : false);
			pumaDoneMainPanel.GetComponent<CanvasGroup>().alpha = pumaDonePanelOpacity;
			pumaDoneOkButton.SetActive(okButtonOpacity > 0f ? true : false);
			pumaDoneOkButton.GetComponent<CanvasGroup>().alpha = okButtonOpacity;
			
			if (scoringSystem.GetPopulationHealth() == 0f) {
				// flashing for "Game Over" text
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
				
				leftTextGameOver.GetComponent<Text>().color = new Color(0.75f, 0.75f, 0.75f, flashingOpacity);
			}
		}
		else {
			// set enables to 'off'
			pumaDoneMainPanel.SetActive(false);
			pumaDoneOkButton.SetActive(false);
		}
	}

	
	//===================================
	//===================================
	//		DRAW THE PUMA DONE DISPLAY
	//===================================
	//===================================

	public void Draw(float pumaDonePanelOpacity, string threatType, float okButtonOpacity) 
	{ 

		if (USE_NEW_GUI == true)
			return; 
		
		
		//////////////////////////////////
		//////////////////////////////////
		
		// LEGACY DRAW CODE

		//////////////////////////////////
		//////////////////////////////////

		
		float pumaDoneDisplayX = (Screen.width / 2) - (Screen.height * 0.6f);
		float pumaDoneDisplayY = Screen.height * 0.025f;
		float pumaDoneDisplayWidth = Screen.height * 1.2f;
		float pumaDoneDisplayHeight = Screen.height * 0.37f;
		
		GUIStyle style = new GUIStyle();
		style.alignment = TextAnchor.MiddleCenter;
		
		float panelOffsetY = -0.1f;
		
		float textureX;
		float textureY;
		float textureWidth;
		float textureHeight;
			
		float titleX;
		float titleY;
		float titleW;
		float titleH;

		float boxX;
		float boxY;
		float boxW;
		float boxH;

		
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

		float leftShiftAmount = pumaDoneDisplayWidth * -0.32f;
		float vertShiftAmount = pumaDoneDisplayHeight * 0.05f;


		//********************
		// BACKGROUND CONTENT
		//********************

		// panel background
		if (USE_NEW_GUI == false) {
			GUI.color = new Color(1f, 1f, 1f, 0.8f * pumaDonePanelOpacity);
			GUI.Box(new Rect(pumaDoneDisplayX, pumaDoneDisplayY + pumaDoneDisplayHeight * 0.06f, pumaDoneDisplayWidth, pumaDoneDisplayHeight * 1.2f - pumaDoneDisplayHeight * 0.06f), "");
			GUI.color = new Color(1f, 1f, 1f, 0.3f * pumaDonePanelOpacity);
			GUI.Box(new Rect(pumaDoneDisplayX, pumaDoneDisplayY + pumaDoneDisplayHeight * 0.06f, pumaDoneDisplayWidth, pumaDoneDisplayHeight * 1.2f - pumaDoneDisplayHeight * 0.06f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * pumaDonePanelOpacity);
		}
		
		// main text
		
		float leftLabelX;
		float leftLabelY1;
		float leftLabelY2;
		float leftLabelY3;
		float leftLabelY4;
		float leftLabelY5;
		float leftLabelW;
		float leftLabelH;
		
		float rightLabelX;
		float rightLabelY1;
		float rightLabelY2;
		float rightLabelY3;
		float rightLabelY4;
		float rightLabelY5;
		float rightLabelW;
		float rightLabelH;

		float fontRef = pumaDoneDisplayHeight * 0.5f;
		style.fontStyle = FontStyle.BoldAndItalic;

		if (USE_NEW_GUI == false) {
			// main title
			GUI.color = new Color(1f, 1f, 1f, 0.8f * pumaDonePanelOpacity);
			GUI.Box(new Rect(pumaDoneDisplayX, pumaDoneDisplayY + pumaDoneDisplayHeight * 0.06f, pumaDoneDisplayWidth, pumaDoneDisplayHeight * 0.17f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * pumaDonePanelOpacity);

			GUI.color = new Color(1f, 1f, 1f, 0.9f * pumaDonePanelOpacity);
			GUI.Box(new Rect(pumaDoneDisplayX + pumaDoneDisplayWidth * 0.22f, pumaDoneDisplayY + pumaDoneDisplayHeight * 0.1f, pumaDoneDisplayWidth * 0.56f, pumaDoneDisplayHeight * 0.11f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * pumaDonePanelOpacity);
		}


		// background box
		boxX = pumaDoneDisplayX + pumaDoneDisplayWidth * 0.3f;
		boxY = pumaDoneDisplayY + pumaDoneDisplayHeight * 0.3f;
		boxW = pumaDoneDisplayWidth * 0.4f;
		boxH = pumaDoneDisplayHeight * 0.8f;
		if (USE_NEW_GUI == false) {
			GUI.color = new Color(1f, 1f, 1f, 0.8f * pumaDonePanelOpacity);
			GUI.Box(new Rect(boxX, boxY, boxW, boxH), "");
			GUI.color = new Color(1f, 1f, 1f, 0.5f * pumaDonePanelOpacity);
			GUI.Box(new Rect(boxX, boxY, boxW, boxH), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * pumaDonePanelOpacity);
		}

		
		if (USE_NEW_GUI == false) {
			// puma head
			GUI.color = (scoringSystem.GetPumaHealth(guiManager.selectedPuma) >= 1f) ? new Color(1f, 1f, 1f, 1f * pumaDonePanelOpacity) : new Color(0.9f, 0.2f, 0.2f, 1f * pumaDonePanelOpacity);
			textureX = pumaDoneDisplayX + pumaDoneDisplayWidth * 0.77f + leftShiftAmount;
			textureY = pumaDoneDisplayY + pumaDoneDisplayHeight * (0.42f + panelOffsetY) + vertShiftAmount;
			textureWidth = pumaDoneDisplayHeight * 0.39f;
			textureHeight = headshotTexture.height * (textureWidth / headshotTexture.width);
			GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), headshotTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * pumaDonePanelOpacity);
		}

		
		if (USE_NEW_GUI == false) {
			// puma name
			float nameX = pumaDoneDisplayX + pumaDoneDisplayWidth * 0.767f + leftShiftAmount;
			float nameY = pumaDoneDisplayY + pumaDoneDisplayHeight * (0.79f + panelOffsetY) + vertShiftAmount;
			float nameW = pumaDoneDisplayWidth * 0.1f;
			float nameH = pumaDoneDisplayHeight * 0.03f;
			style.normal.textColor = new Color(0.55f, 0.55f, 0.55f, 1f);
			style.fontSize = (int)(fontRef * 0.16f);
			GUI.Button(new Rect(nameX, nameY, nameW, nameH), pumaName, style);
		}
	
		if (USE_NEW_GUI == false) {
			// crossbones
			GUI.color = new Color(0.95f, 0.95f, 0.95f, 1f * pumaDonePanelOpacity);
			textureY = pumaDoneDisplayY + pumaDoneDisplayHeight * (0.5f + panelOffsetY) + vertShiftAmount;
			textureWidth = pumaDoneDisplayHeight * 0.3f;
			textureHeight = pumaCrossbonesRedTexture.height * (textureWidth / pumaCrossbonesRedTexture.width);
			textureX = pumaDoneDisplayX + pumaDoneDisplayWidth/2  - textureWidth/2 - pumaDoneDisplayWidth * 0.13f;
			GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), pumaCrossbonesRedTexture);
			textureX = pumaDoneDisplayX + pumaDoneDisplayWidth/2  - textureWidth/2 + pumaDoneDisplayWidth * 0.13f;
			GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), pumaCrossbonesRedTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * pumaDonePanelOpacity);
		}

		// health bar
		float barX = pumaDoneDisplayX + pumaDoneDisplayWidth * 0.670f + pumaDoneDisplayHeight * 0.03f + leftShiftAmount;
		float barY = pumaDoneDisplayY + pumaDoneDisplayHeight * 0.83f + vertShiftAmount;
		float barW = pumaDoneDisplayWidth * 0.29f - pumaDoneDisplayHeight * 0.06f;
		float barH = pumaDoneDisplayHeight * 0.11f;
		guiComponents.DrawPumaHealthBar(guiManager.selectedPuma, pumaDonePanelOpacity, barX, barY, barW, barH, true);
		
		
		//************************
		// CAR COLLISION CONTENT
		//************************
					
		GUI.color = new Color(1f, 1f, 1f, 1f * pumaDonePanelOpacity);

		if (threatType == "threatTypeVehicle") {
		
			if (USE_NEW_GUI == false) {
				// header title
				titleX = pumaDoneDisplayX + pumaDoneDisplayWidth * 0.3f;
				titleY = pumaDoneDisplayY + pumaDoneDisplayHeight * 0.136f;
				titleW = pumaDoneDisplayWidth * 0.4f;
				titleH = pumaDoneDisplayHeight * 0.03f;
				style.normal.textColor =  new Color(0.70f, 0f, 0f, 1f);
				style.fontStyle = FontStyle.Bold;
				style.fontSize = (int)(fontRef * 0.18f);
				GUI.Button(new Rect(titleX, titleY, titleW, titleH), "KILLED BY A VEHICLE!", style);
			}
			
			
			if (scoringSystem.GetPopulationHealth() > 0f) {
			
				if (USE_NEW_GUI == false) {
					// left and right label boxes
					boxY = pumaDoneDisplayY + pumaDoneDisplayHeight * 0.30f;
					boxW = pumaDoneDisplayWidth * 0.22f;
					boxH = pumaDoneDisplayHeight * 0.8f;
					boxX = pumaDoneDisplayX + pumaDoneDisplayWidth/2 - boxW/2 - pumaDoneDisplayWidth * 0.35f;
					GUI.color = new Color(1f, 1f, 1f, 0.8f * pumaDonePanelOpacity);
					GUI.Box(new Rect(boxX, boxY, boxW, boxH), "");
					GUI.color = new Color(1f, 1f, 1f, 1f * pumaDonePanelOpacity);
					boxX = pumaDoneDisplayX + pumaDoneDisplayWidth/2 - boxW/2 + pumaDoneDisplayWidth * 0.35f;
					boxY = pumaDoneDisplayY + pumaDoneDisplayHeight * 0.30f;
					boxH = pumaDoneDisplayHeight * 0.51f;
					GUI.color = new Color(1f, 1f, 1f, 0.8f * pumaDonePanelOpacity);
					GUI.Box(new Rect(boxX, boxY, boxW, boxH), "");
					GUI.color = new Color(1f, 1f, 1f, 1f * pumaDonePanelOpacity);
				}
						
				if (USE_NEW_GUI == false) {
					// left label
					style.alignment = TextAnchor.MiddleLeft;
					leftLabelX = pumaDoneDisplayX + pumaDoneDisplayWidth * 0.385f + leftShiftAmount;
					leftLabelY1 = pumaDoneDisplayY + pumaDoneDisplayHeight * (0.50f + panelOffsetY) + vertShiftAmount;
					leftLabelY2 = pumaDoneDisplayY + pumaDoneDisplayHeight * (0.61f + panelOffsetY) + vertShiftAmount;
					leftLabelY3 = pumaDoneDisplayY + pumaDoneDisplayHeight * (0.72f + panelOffsetY) + vertShiftAmount;
					leftLabelY4 = pumaDoneDisplayY + pumaDoneDisplayHeight * (0.83f + panelOffsetY) + vertShiftAmount;
					leftLabelY5 = pumaDoneDisplayY + pumaDoneDisplayHeight * (0.94f + panelOffsetY) + vertShiftAmount;
					leftLabelW = pumaDoneDisplayWidth * 0.1f;
					leftLabelH = pumaDoneDisplayHeight * 0.03f;
					style.normal.textColor = new Color(0.65f, 0.65f, 0.65f, 1f);
					style.fontSize = (int)(fontRef * 0.16f);
					GUI.Button(new Rect(leftLabelX, leftLabelY1, leftLabelW, leftLabelH), "Over 60 pumas", style);
					GUI.Button(new Rect(leftLabelX, leftLabelY2, leftLabelW, leftLabelH), "die each year", style);
					GUI.Button(new Rect(leftLabelX, leftLabelY3, leftLabelW, leftLabelH), "in California", style);
					GUI.Button(new Rect(leftLabelX, leftLabelY4, leftLabelW, leftLabelH), "from collisions", style);
					GUI.Button(new Rect(leftLabelX, leftLabelY5, leftLabelW, leftLabelH), "with vehicles", style);

					// right label
					rightLabelX = pumaDoneDisplayX + pumaDoneDisplayWidth * 0.765f;
					rightLabelY1 = pumaDoneDisplayY + pumaDoneDisplayHeight * (0.48f + panelOffsetY) + vertShiftAmount;
					rightLabelY2 = pumaDoneDisplayY + pumaDoneDisplayHeight * (0.59f + panelOffsetY) + vertShiftAmount;
					rightLabelY3 = pumaDoneDisplayY + pumaDoneDisplayHeight * (0.70f + panelOffsetY) + vertShiftAmount;
					rightLabelW = pumaDoneDisplayWidth * 0.1f;
					rightLabelH = pumaDoneDisplayHeight * 0.03f;
					style.normal.textColor = new Color(0.65f, 0.65f, 0.65f, 1f);
					style.fontSize = (int)(fontRef * 0.16f);
					GUI.Button(new Rect(rightLabelX, rightLabelY1, rightLabelW, rightLabelH), "Pumas need to", style);
					GUI.Button(new Rect(rightLabelX, rightLabelY2, rightLabelW, rightLabelH), "move carefully", style);
					GUI.Button(new Rect(rightLabelX, rightLabelY3, rightLabelW, rightLabelH), "to avoid cars", style);
					style.alignment = TextAnchor.MiddleCenter;

					// survival tips button
					boxY = pumaDoneDisplayY + pumaDoneDisplayHeight * 0.86f;
					boxW = pumaDoneDisplayWidth * 0.22f;
					boxH = pumaDoneDisplayHeight * 0.24f;
					boxX = pumaDoneDisplayX + pumaDoneDisplayWidth/2 - boxW/2 + pumaDoneDisplayWidth * 0.35f;
					GUI.color = new Color(1f, 1f, 1f, 0.8f * pumaDonePanelOpacity);
					GUI.Box(new Rect(boxX, boxY, boxW, boxH), "");
					GUI.color = new Color(1f, 1f, 1f, 1f * pumaDonePanelOpacity);
					GUI.skin = guiManager.customGUISkin;
					guiManager.customGUISkin.button.fontSize = (int)(pumaDoneDisplayHeight * 0.070);
					guiManager.customGUISkin.button.fontStyle = FontStyle.Bold;
					if (GUI.Button(new Rect(pumaDoneDisplayX + pumaDoneDisplayWidth * 0.77f,  pumaDoneDisplayY + pumaDoneDisplayHeight * 0.9f, pumaDoneDisplayWidth * 0.16f, pumaDoneDisplayHeight * 0.15f), "")) {
						guiManager.OpenInfoPanel(3);
					}
					if (GUI.Button(new Rect(pumaDoneDisplayX + pumaDoneDisplayWidth * 0.77f,  pumaDoneDisplayY + pumaDoneDisplayHeight * 0.9f, pumaDoneDisplayWidth * 0.16f, pumaDoneDisplayHeight * 0.15f), "Survival Tips")) {
						guiManager.OpenInfoPanel(3);
					}
				}
			}
		}		


		//********************
		// STARVED CONTENT
		//********************

		GUI.color = new Color(1f, 1f, 1f, 1f * pumaDonePanelOpacity);

		if (threatType == "threatTypeStarvation") {
		
			if (USE_NEW_GUI == false) {
				// header title
				titleX = pumaDoneDisplayX + pumaDoneDisplayWidth * 0.3f;
				titleY = pumaDoneDisplayY + pumaDoneDisplayHeight * 0.136f;
				titleW = pumaDoneDisplayWidth * 0.4f;
				titleH = pumaDoneDisplayHeight * 0.03f;
				style.fontSize = (int)(fontRef * 0.22f);
				style.normal.textColor =  new Color(0.70f, 0f, 0f, 1f);
				style.fontStyle = FontStyle.Bold;
				style.fontSize = (int)(fontRef * 0.18f);
				GUI.Button(new Rect(titleX, titleY, titleW, titleH), "STARVED TO DEATH!", style);
			}
			
			if (scoringSystem.GetPopulationHealth() > 0f) {
			
				if (USE_NEW_GUI == false) {
					// left and right label boxes
					boxY = pumaDoneDisplayY + pumaDoneDisplayHeight * 0.30f;
					boxW = pumaDoneDisplayWidth * 0.22f;
					boxH = pumaDoneDisplayHeight * 0.8f;
					boxX = pumaDoneDisplayX + pumaDoneDisplayWidth/2 - boxW/2 - pumaDoneDisplayWidth * 0.35f;
					GUI.color = new Color(1f, 1f, 1f, 0.8f * pumaDonePanelOpacity);
					GUI.Box(new Rect(boxX, boxY, boxW, boxH), "");
					GUI.color = new Color(1f, 1f, 1f, 1f * pumaDonePanelOpacity);
					boxX = pumaDoneDisplayX + pumaDoneDisplayWidth/2 - boxW/2 + pumaDoneDisplayWidth * 0.35f;
					boxY = pumaDoneDisplayY + pumaDoneDisplayHeight * 0.30f;
					boxH = pumaDoneDisplayHeight * 0.51f;
					GUI.color = new Color(1f, 1f, 1f, 0.8f * pumaDonePanelOpacity);
					GUI.Box(new Rect(boxX, boxY, boxW, boxH), "");
					GUI.color = new Color(1f, 1f, 1f, 1f * pumaDonePanelOpacity);
				}
					
				if (USE_NEW_GUI == false) {
					// left label
					style.alignment = TextAnchor.MiddleLeft;
					leftLabelX = pumaDoneDisplayX + pumaDoneDisplayWidth * 0.375f + leftShiftAmount;
					leftLabelY1 = pumaDoneDisplayY + pumaDoneDisplayHeight * (0.48f + panelOffsetY) + vertShiftAmount;
					leftLabelY2 = pumaDoneDisplayY + pumaDoneDisplayHeight * (0.59f + panelOffsetY) + vertShiftAmount;
					leftLabelY3 = pumaDoneDisplayY + pumaDoneDisplayHeight * (0.70f + panelOffsetY) + vertShiftAmount;
					leftLabelY4 = pumaDoneDisplayY + pumaDoneDisplayHeight * (0.88f + panelOffsetY) + vertShiftAmount;
					leftLabelY5 = pumaDoneDisplayY + pumaDoneDisplayHeight * (0.99f + panelOffsetY) + vertShiftAmount;
					leftLabelW = pumaDoneDisplayWidth * 0.1f;
					leftLabelH = pumaDoneDisplayHeight * 0.03f;
					style.normal.textColor = new Color(0.65f, 0.65f, 0.65f, 1f);
					style.fontSize = (int)(fontRef * 0.16f);
					GUI.Button(new Rect(leftLabelX, leftLabelY1, leftLabelW, leftLabelH), "Deer = up to 80%", style);
					GUI.Button(new Rect(leftLabelX, leftLabelY2, leftLabelW, leftLabelH), "of the puma diet", style);
					GUI.Button(new Rect(leftLabelX, leftLabelY3, leftLabelW, leftLabelH), "in North America", style);
					GUI.Button(new Rect(leftLabelX, leftLabelY4, leftLabelW, leftLabelH), "Pumas help limit", style);
					GUI.Button(new Rect(leftLabelX, leftLabelY5, leftLabelW, leftLabelH), "deer populations", style);

					// right label
					rightLabelX = pumaDoneDisplayX + pumaDoneDisplayWidth * 0.775f;
					rightLabelY1 = pumaDoneDisplayY + pumaDoneDisplayHeight * (0.48f + panelOffsetY) + vertShiftAmount;
					rightLabelY2 = pumaDoneDisplayY + pumaDoneDisplayHeight * (0.59f + panelOffsetY) + vertShiftAmount;
					rightLabelY3 = pumaDoneDisplayY + pumaDoneDisplayHeight * (0.70f + panelOffsetY) + vertShiftAmount;
					rightLabelW = pumaDoneDisplayWidth * 0.1f;
					rightLabelH = pumaDoneDisplayHeight * 0.03f;
					style.normal.textColor = new Color(0.65f, 0.65f, 0.65f, 1f);
					style.fontSize = (int)(fontRef * 0.16f);
					GUI.Button(new Rect(rightLabelX, rightLabelY1, rightLabelW, rightLabelH), "Pumas need", style);
					GUI.Button(new Rect(rightLabelX, rightLabelY2, rightLabelW, rightLabelH), "to hunt smart", style);
					GUI.Button(new Rect(rightLabelX, rightLabelY3, rightLabelW, rightLabelH), "to catch deer", style);
					style.alignment = TextAnchor.MiddleCenter;
						
					// hunting tips button
					boxY = pumaDoneDisplayY + pumaDoneDisplayHeight * 0.86f;
					boxW = pumaDoneDisplayWidth * 0.22f;
					boxH = pumaDoneDisplayHeight * 0.24f;
					boxX = pumaDoneDisplayX + pumaDoneDisplayWidth/2 - boxW/2 + pumaDoneDisplayWidth * 0.35f;
					GUI.color = new Color(1f, 1f, 1f, 0.8f * pumaDonePanelOpacity);
					GUI.Box(new Rect(boxX, boxY, boxW, boxH), "");
					GUI.color = new Color(1f, 1f, 1f, 1f * pumaDonePanelOpacity);
					GUI.skin = guiManager.customGUISkin;
					guiManager.customGUISkin.button.fontSize = (int)(pumaDoneDisplayHeight * 0.070);
					guiManager.customGUISkin.button.fontStyle = FontStyle.Bold;
					if (GUI.Button(new Rect(pumaDoneDisplayX + pumaDoneDisplayWidth * 0.77f,  pumaDoneDisplayY + pumaDoneDisplayHeight * 0.9f, pumaDoneDisplayWidth * 0.16f, pumaDoneDisplayHeight * 0.15f), "")) {
						guiManager.OpenInfoPanel(1);
					}
					if (GUI.Button(new Rect(pumaDoneDisplayX + pumaDoneDisplayWidth * 0.77f,  pumaDoneDisplayY + pumaDoneDisplayHeight * 0.9f, pumaDoneDisplayWidth * 0.16f, pumaDoneDisplayHeight * 0.15f), "Hunting Tips")) {
						guiManager.OpenInfoPanel(1);
					}
				}			
			}
		}		


		//**************************
		// END OF POPULATION CONTENT
		//**************************

		if (scoringSystem.GetPopulationHealth() == 0f) {
			// end of population

			if (USE_NEW_GUI == false) {
				// left and right label boxes
				boxY = pumaDoneDisplayY + pumaDoneDisplayHeight * 0.30f;
				boxW = pumaDoneDisplayWidth * 0.22f;
				boxH = pumaDoneDisplayHeight * 0.8f;
				boxX = pumaDoneDisplayX + pumaDoneDisplayWidth/2 - boxW/2 - pumaDoneDisplayWidth * 0.35f;
				GUI.color = new Color(1f, 1f, 1f, 1f * pumaDonePanelOpacity);
				GUI.Box(new Rect(boxX, boxY, boxW, boxH), "");
				GUI.color = new Color(1f, 1f, 1f, 1f * pumaDonePanelOpacity);
				boxX = pumaDoneDisplayX + pumaDoneDisplayWidth/2 - boxW/2 + pumaDoneDisplayWidth * 0.35f;
				GUI.color = new Color(1f, 1f, 1f, 1f * pumaDonePanelOpacity);
				GUI.Box(new Rect(boxX, boxY, boxW, boxH), "");
				GUI.color = new Color(1f, 1f, 1f, 1f * pumaDonePanelOpacity);
			}

			if (USE_NEW_GUI == false) {
				// left label
				style.alignment = TextAnchor.MiddleLeft;
				leftLabelX = pumaDoneDisplayX + pumaDoneDisplayWidth * 0.375f + leftShiftAmount;
				leftLabelY1 = pumaDoneDisplayY + pumaDoneDisplayHeight * (0.48f + panelOffsetY) + vertShiftAmount;
				leftLabelY2 = pumaDoneDisplayY + pumaDoneDisplayHeight * (0.63f + panelOffsetY) + vertShiftAmount;
				leftLabelY3 = pumaDoneDisplayY + pumaDoneDisplayHeight * (0.74f + panelOffsetY) + vertShiftAmount;
				leftLabelY4 = pumaDoneDisplayY + pumaDoneDisplayHeight * (0.89f + panelOffsetY) + vertShiftAmount;
				leftLabelY5 = pumaDoneDisplayY + pumaDoneDisplayHeight * (0.99f + panelOffsetY) + vertShiftAmount;
				leftLabelW = pumaDoneDisplayWidth * 0.1f;
				leftLabelH = pumaDoneDisplayHeight * 0.03f;
				style.normal.textColor = new Color(0.7f, 0.7f, 0.7f, 1f);
				style.fontSize = (int)(fontRef * 0.16f);
				style.fontSize = (int)(fontRef * 0.17f);
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
				style.normal.textColor = new Color(0.75f, 0.75f, 0.75f, flashingOpacity);
				GUI.Button(new Rect(leftLabelX, leftLabelY1, leftLabelW, leftLabelH), "- GAME OVER -", style);
				style.fontSize = (int)(fontRef * 0.15f);
				style.normal.textColor = new Color(0.7f, 0.7f, 0.7f, 1f);
				GUI.Button(new Rect(leftLabelX, leftLabelY2, leftLabelW, leftLabelH), "The last puma in", style);
				GUI.Button(new Rect(leftLabelX, leftLabelY3, leftLabelW, leftLabelH), "the area has died", style);
				style.normal.textColor = new Color(0.7f, 0.6f, 0.5f, 1f);
				GUI.Button(new Rect(leftLabelX, leftLabelY4, leftLabelW, leftLabelH), "Extinction: local", style);
				GUI.Button(new Rect(leftLabelX, leftLabelY5, leftLabelW, leftLabelH), "puma population", style);
				style.normal.textColor = new Color(0.7f, 0.7f, 0.7f, 1f);

				// right label
				rightLabelX = pumaDoneDisplayX + pumaDoneDisplayWidth * 0.758f;
				rightLabelY1 = pumaDoneDisplayY + pumaDoneDisplayHeight * (0.48f + panelOffsetY) + vertShiftAmount;
				rightLabelY2 = pumaDoneDisplayY + pumaDoneDisplayHeight * (0.595f + panelOffsetY) + vertShiftAmount;
				rightLabelY3 = pumaDoneDisplayY + pumaDoneDisplayHeight * (0.695f + panelOffsetY) + vertShiftAmount;
				rightLabelY4 = pumaDoneDisplayY + pumaDoneDisplayHeight * (0.88f + panelOffsetY) + vertShiftAmount;
				rightLabelY5 = pumaDoneDisplayY + pumaDoneDisplayHeight * (0.99f + panelOffsetY) + vertShiftAmount;
				rightLabelW = pumaDoneDisplayWidth * 0.1f;
				rightLabelH = pumaDoneDisplayHeight * 0.03f;
				style.normal.textColor = new Color(0.7f, 0.7f, 0.7f, 1f);
				style.fontSize = (int)(fontRef * 0.16f);
				style.normal.textColor = new Color(0.6f, 0.55f, 0.5f, 1f);
				style.fontSize = (int)(fontRef * 0.14f);
				GUI.Button(new Rect(rightLabelX, rightLabelY1, rightLabelW, rightLabelH), "BIG WINNER:", style);
				style.fontSize = (int)(fontRef * 0.16f);
				style.normal.textColor = new Color(0.8f, 0.8f, 0.8f, 1f);
				GUI.Button(new Rect(rightLabelX, rightLabelY2, rightLabelW, rightLabelH), "The Deer", style);
				style.fontSize = (int)(fontRef * 0.13f);
				style.normal.textColor = new Color(0.7f, 0.7f, 0.7f, 1f);
				style.fontStyle = FontStyle.BoldAndItalic;
				GUI.Button(new Rect(rightLabelX, rightLabelY3, rightLabelW, rightLabelH), "can graze anywhere", style);
				style.fontStyle = FontStyle.Bold;
				style.normal.textColor = new Color(0.7f, 0.7f, 0.7f, 1f);
				style.normal.textColor = new Color(0.6f, 0.55f, 0.5f, 1f);
				style.fontSize = (int)(fontRef * 0.14f);
				GUI.Button(new Rect(rightLabelX, rightLabelY4, rightLabelW, rightLabelH), "BIG LOSER:", style);
				style.fontSize = (int)(fontRef * 0.16f);
				style.normal.textColor = new Color(0.8f, 0.8f, 0.8f, 1f);
				GUI.Button(new Rect(rightLabelX, rightLabelY5, rightLabelW, rightLabelH), "The Ecosystem", style);
				style.alignment = TextAnchor.MiddleCenter;	
			}				
		}

		
		//********************
		// 'OK' BUTTON
		//********************
		
		if (USE_NEW_GUI == false) {
			
			pumaDoneDisplayX -= pumaDoneDisplayWidth * 0.02f;
			pumaDoneDisplayY += pumaDoneDisplayHeight * 1.3f;

			GUI.color = new Color(1f, 1f, 1f, 0.8f * okButtonOpacity);
			GUI.Box(new Rect(pumaDoneDisplayX + pumaDoneDisplayWidth * 0.78f, pumaDoneDisplayY + pumaDoneDisplayHeight * 0.67f, pumaDoneDisplayWidth * 0.20f, pumaDoneDisplayHeight * 0.37f), "");
			GUI.color = new Color(1f, 1f, 1f, 0.6f * okButtonOpacity);
			GUI.Box(new Rect(pumaDoneDisplayX + pumaDoneDisplayWidth * 0.78f, pumaDoneDisplayY + pumaDoneDisplayHeight * 0.67f, pumaDoneDisplayWidth * 0.20f, pumaDoneDisplayHeight * 0.37f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * okButtonOpacity);

			GUI.color = new Color(1f, 1f, 1f, 0.8f * okButtonOpacity);
			GUI.Box(new Rect(pumaDoneDisplayX + pumaDoneDisplayWidth * 0.81f,  pumaDoneDisplayY + pumaDoneDisplayHeight * 0.727f, pumaDoneDisplayWidth * 0.14f, pumaDoneDisplayHeight * 0.25f), "");
			GUI.color = new Color(1f, 1f, 1f, 0.8f * okButtonOpacity);
			GUI.Box(new Rect(pumaDoneDisplayX + pumaDoneDisplayWidth * 0.81f,  pumaDoneDisplayY + pumaDoneDisplayHeight * 0.727f, pumaDoneDisplayWidth * 0.14f, pumaDoneDisplayHeight * 0.25f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * okButtonOpacity);

			GUI.skin = guiManager.customGUISkin;
			guiManager.customGUISkin.button.fontSize = (int)(pumaDoneDisplayHeight * 0.14);
			guiManager.customGUISkin.button.fontStyle = FontStyle.Normal;
			if (GUI.Button(new Rect(pumaDoneDisplayX + pumaDoneDisplayWidth * 0.81f,  pumaDoneDisplayY + pumaDoneDisplayHeight * 0.727f, pumaDoneDisplayWidth * 0.14f, pumaDoneDisplayHeight * 0.25f), "Go")) {
				guiManager.SetGuiState("guiStateLeavingPumaDone");
				if (levelManager.CheckCarCollision() == true)
					levelManager.EndCarCollision();
				else if (levelManager.CheckStarvation() == true)
					levelManager.EndStarvation();		
				levelManager.SetGameState("gameStateLeavingGameplay");
			}	
			
			pumaDoneDisplayX += pumaDoneDisplayWidth * 0.02f;
			pumaDoneDisplayY -= pumaDoneDisplayHeight * 1.3f;
		}
	}
	
}