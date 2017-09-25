using UnityEngine;
using System.Collections;

/// Main handling of all level-based play
/// 

public class LevelManager : MonoBehaviour 
{
	// DEBUG & DEV
	public bool goStraightToFeeding = false;
	public float speedOverdrive = 1f;
	public float guiFlybyOverdrive = 1f;
	private float travelledDistanceOverdrive = 1f;
	public float difficultyLevel = 1f;
	
	private float guiFlybySpeed = 0f;
	public bool guiFlybyOverdriveRampFlag = false;
	public float guiFlybyOverdriveRampStartVal;
	public float guiFlybyOverdriveRampEndVal;
	public float guiFlybyOverdriveRampStartTime;
	public float guiFlybyOverdriveRampEndTime;

	public string displayVar1;
	public string displayVar2;
	public string displayVar3;
	public string displayVar4;
	public string displayVar5;
	public string displayVar6;
	
	private int frameCount = 0;
	private int frameFirstTime;
	private int framePrevTime;
	public int frameCurrentDuration;
	public int frameAverageDuration;

	//===================================
	//===================================
	//		MODULE VARIABLES
	//===================================
	//===================================

	// STATES

	public string gameState;
	private string gameSubState;
	private float stateStartTime;
	private bool stateInitFlag;
	public int currentLevel;
	public bool beginLevelFlag;
	private string carCollisionState = "None";
	private string treeCollisionState = "None";
	private string starvationState = "None";

	// TERRAIN

	public GameObject terrain1;
	public GameObject terrain2;
	public GameObject terrain3;
	public GameObject terrain4;
	public GameObject terrain5;
		
	private Terrain terrainMaster;
		
	private GameObject terrainA;
	private GameObject terrainB;
	private GameObject terrainC;
	private GameObject terrainD;
	
	private float terrainPosInitNEG = -1000f;
	private float terrainPosInitPOS = 0f;
	private float terrainSideLength = 1000f;
	private float terrainShiftDistance = 2000f;

	// VEHICLES
	public GameObject vehicleParentNode;
	
	// ROADS
	
	private float roadBaseY = 10.01f;

	public GameObject L2Road1;
	public GameObject L2Road2;
	public GameObject L2Road3;
	
	public GameObject L3Road1;
	public GameObject L3Road2;
	public GameObject L3Road3;

	public GameObject L4Road1;
	public GameObject L4Road2;
	public GameObject L4Road3;

	public GameObject L5Road1;
	public GameObject L5Road2;
	public GameObject L5Road3;

	private GameObject road1A;
	private GameObject road2A;
	private GameObject road3A;
	
	private GameObject road1B;
	private GameObject road2B;
	private GameObject road3B;
	
	private GameObject road1C;
	private GameObject road2C;
	private GameObject road3C;
	
	private GameObject road1D;
	private GameObject road2D;
	private GameObject road3D;
	
	// BRIDGES
	
	public GameObject L3Bridge1;
	public GameObject L3Bridge2;
	public GameObject L4Bridge1;
	public GameObject L4Bridge2;
	public GameObject L5Bridge1;
	public GameObject L5Bridge2;

	private GameObject bridge1A;
	private GameObject bridge2A;
	private GameObject bridge1B;
	private GameObject bridge2B;
	private GameObject bridge1C;
	private GameObject bridge2C;
	private GameObject bridge1D;
	private GameObject bridge2D;
	
	// OVERPASSES
	
	public GameObject overpass1;
	public GameObject overpass2;
	public GameObject overpass3;
	public GameObject overpass4;
	public GameObject overpass5;
	public GameObject overpass6;
	public GameObject overpass7;
	public GameObject overpass8;
	public GameObject overpass9;
	
	private GameObject overpass1A;
	private GameObject overpass2A;
	private GameObject overpass3A;
	private GameObject overpass4A;
	private GameObject overpass5A;
	private GameObject overpass6A;
	private GameObject overpass7A;
	private GameObject overpass8A;
	private GameObject overpass9A;
	
	private GameObject overpass1B;
	private GameObject overpass2B;
	private GameObject overpass3B;
	private GameObject overpass4B;
	private GameObject overpass5B;
	private GameObject overpass6B;
	private GameObject overpass7B;
	private GameObject overpass8B;
	private GameObject overpass9B;
	
	private GameObject overpass1C;
	private GameObject overpass2C;
	private GameObject overpass3C;
	private GameObject overpass4C;
	private GameObject overpass5C;
	private GameObject overpass6C;
	private GameObject overpass7C;
	private GameObject overpass8C;
	private GameObject overpass9C;
	
	private GameObject overpass1D;
	private GameObject overpass2D;
	private GameObject overpass3D;
	private GameObject overpass4D;
	private GameObject overpass5D;
	private GameObject overpass6D;
	private GameObject overpass7D;
	private GameObject overpass8D;
	private GameObject overpass9D;
	
	// UNDERPASSES
	
	public GameObject underpass1;
	public GameObject underpass2;
	public GameObject underpass3;
	public GameObject underpass4;
	public GameObject underpass5;
	public GameObject underpass6;
	public GameObject underpass7;
	
	private GameObject underpass1A;
	private GameObject underpass2A;
	private GameObject underpass3A;
	private GameObject underpass4A;
	private GameObject underpass5A;
	private GameObject underpass6A;
	private GameObject underpass7A;
	
	private GameObject underpass1B;
	private GameObject underpass2B;
	private GameObject underpass3B;
	private GameObject underpass4B;
	private GameObject underpass5B;
	private GameObject underpass6B;
	private GameObject underpass7B;
	
	private GameObject underpass1C;
	private GameObject underpass2C;
	private GameObject underpass3C;
	private GameObject underpass4C;
	private GameObject underpass5C;
	private GameObject underpass6C;
	private GameObject underpass7C;
	
	private GameObject underpass1D;
	private GameObject underpass2D;
	private GameObject underpass3D;
	private GameObject underpass4D;
	private GameObject underpass5D;
	private GameObject underpass6D;
	private GameObject underpass7D;
	
	// PUMA

	public GameObject pumaObj;
	private BoxCollider pumaObjCollider;
	private int selectedPuma = -1;
	public float mainHeading;
	public LayerMask pumaRoadCheckLayerMask;
	public bool pumaFeedingOnRoad = false;
	private float pumaX;
	private float pumaY;
	private float pumaZ;
	private float pumaHeading = 0f;
	private float pumaHeadingOffset = 0f;
	private float pumaHeadingOffsetStartVal = 0f;
	private float pumaHeadingOffsetTargetVal = 0f;
	private float pumaHeadingOffsetStepSize;
	private bool pumaSideStalkFlag = false;
	private float pumaStalkingSpeed = 6f;
	private float pumaChasingSpeed = 20.2f;
	private float basePumaChasingSpeed = 20.2f;
	private float chaseTriggerDistance = 10.5f;
	private float baseChaseTriggerDistance = 15f;
	private float caughtTriggerDistance = 1f;
	private float deerGotAwayDistance = 130f;
	private bool pumaCollisionFlag = false;
	private float pumaCollisionBarrierHeading;
	private float pumaCollisionHeadingOffset;	
	private float nextFlybyHeadingUpdateTime;
	private float flybyHeadingRotationSpeed = 0f;
	private float pumaPhysicsInProgressTime;
	private float pumaPhysicsConcludedTime;
	private float pumaPhysicsOffsetY;
	private float pumaPhysicsPreviousY;
	private bool pumaJumpFlag = false;
	private float pumaJumpVelocityUp = 0f;
	private float pumaJumpGravityDown = 0f;
	private float pumaJumpOffsetY = 0f;
	private float pumaJumpVelocityForward = 0f;
	private float pumaJumpGravityBack = 0f;
	private float pumaJumpOffsetD = 0f;

	// PUMA CHARACTERISTICS

	private float[] powerArray = new float[] {0.6f, 0.4f, 0.9f, 0.7f, 0.7f, 0.5f};
	private float[] speedArray = new float[] {1.5f, 1.5f, 0f, 0f, -1.5f, -1.5f};
	private float[] enduranceArray = new float[] {0.6f, 0.4f, 0.9f, 0.8f, 0.6f, 0.4f};
	private float[] stealthArray = new float[] {2.5f, 2.5f, 0f, 0f, -2.5f, -2.5f};
	
	// DEER

	public class DeerClass {
		public string type;
		public GameObject gameObj;
        public DeerCollisionChecker collisionChecker;
		public float heading = 0f;
		public float targetHeading = 0f;
		public float nextTurnTime = 0f;
		public float forwardRate = 0f;
		public float turnRate = 0f;
		public float baseY;
		public float deerCaughtFinalOffsetForward;
		public float deerCaughtFinalOffsetSideways;
        
	}
    public bool deerAvoidsRoad = true;
	public DeerClass buck;
	public DeerClass doe;
	public DeerClass fawn;

	private bool deerSet1Selected;	

	private DeerClass buck1;
	private DeerClass doe1;
	private DeerClass fawn1;
	private DeerClass buck2;
	private DeerClass doe2;
	private DeerClass fawn2;

	private bool newChaseFlag = false;

	private float buckDefaultForwardRate = 17f;
	private float buckDefaultTurnRate = 15f;
	private float doeDefaultForwardRate = 16f;
	private float doeDefaultTurnRate = 15f;
	private float fawnDefaultForwardRate = 15f;
	private float fawnDefaultTurnRate = 15f;
	private float nextDeerRunUpdateTime = 0f;
	private int lastAutoKilledDeerType = 0;

	// CAUGHT DEER
	
	public DeerClass caughtDeer = null;
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

	// ANIMATORS
	
	public Animator pumaAnimator;
	
	private Animator buckAnimator;
	private Animator doeAnimator;
	private Animator fawnAnimator;
	
	public Animator buck1Animator;
	public Animator doe1Animator;
	public Animator fawn1Animator;
	
	public Animator buck2Animator;
	public Animator doe2Animator;
	public Animator fawn2Animator;

	// PREFABS

	public GameObject DeerBloodProjector;
    public GameObject PumaBloodProjector;
    public GameObject bloodParticles;

	// EXTERNAL MODULES
	private GuiManager guiManager;
	private GuiUtils guiUtils;
	private ScoringSystem scoringSystem;
	private InputControls inputControls;
	private CameraController cameraController;
	private TrafficManager trafficManager;
	private PumaController pumaController;

	//===================================
	//===================================
	//		INITIALIZATION
	//===================================
	//===================================

    void Awake()
	{
        Application.targetFrameRate = 120;
    }

	void Start() 
	{	
		// connect to external modules
		guiManager = GetComponent<GuiManager>();
		guiUtils = GetComponent<GuiUtils>();
		scoringSystem = GetComponent<ScoringSystem>();
		inputControls = GetComponent<InputControls>();
		cameraController = GetComponent<CameraController>();
		trafficManager = GetComponent<TrafficManager>();

        ResetPuma();
				
		// deer
		buck1 = new DeerClass();
		buck1.type = "Buck";
		buck1.gameObj = GameObject.Find("Buck1");
        buck1.collisionChecker = buck1.gameObj.GetComponentInChildren<DeerCollisionChecker>();
		buck1.forwardRate = 0; //30f;
		buck1.turnRate = 0; //22.5f;
		buck1.baseY = 0f;
		buck1.deerCaughtFinalOffsetForward = 0.42f;
		buck1.deerCaughtFinalOffsetSideways = 0.28f;

		doe1 = new DeerClass();
		doe1.type = "Doe";
		doe1.gameObj = GameObject.Find("Doe1");
        doe1.collisionChecker = doe1.gameObj.GetComponentInChildren<DeerCollisionChecker>();
        doe1.forwardRate = 0; //30f;
		doe1.turnRate = 0; //22.5f;
		doe1.baseY = 0f;
		doe1.deerCaughtFinalOffsetForward = 0.375f;
		doe1.deerCaughtFinalOffsetSideways = 0.25f;

		fawn1 = new DeerClass();
		fawn1.type = "Fawn";
		fawn1.gameObj = GameObject.Find("Fawn1");
        fawn1.collisionChecker = fawn1.gameObj.GetComponentInChildren<DeerCollisionChecker>();
        fawn1.forwardRate = 0; //30f;
		fawn1.turnRate = 0; //22.5f;
		fawn1.baseY = 0f;
		fawn1.deerCaughtFinalOffsetForward = 0.35f;
		fawn1.deerCaughtFinalOffsetSideways = 0.125f;

		buck2 = new DeerClass();
		buck2.type = "Buck";
		buck2.gameObj = GameObject.Find("Buck2");
        buck2.collisionChecker = buck2.gameObj.GetComponentInChildren<DeerCollisionChecker>();
        buck2.forwardRate = 0; //30f;
		buck2.turnRate = 0; //22.5f;
		buck2.baseY = 0f;
		buck2.deerCaughtFinalOffsetForward = 0.42f;
		buck2.deerCaughtFinalOffsetSideways = 0.28f;

		doe2 = new DeerClass();
		doe2.type = "Doe";
		doe2.gameObj = GameObject.Find("Doe2");
        doe2.collisionChecker = doe2.gameObj.GetComponentInChildren<DeerCollisionChecker>();
        doe2.forwardRate = 0; //30f;
		doe2.turnRate = 0; //22.5f;
		doe2.baseY = 0f;
		doe2.deerCaughtFinalOffsetForward = 0.375f;
		doe2.deerCaughtFinalOffsetSideways = 0.25f;

		fawn2 = new DeerClass();
		fawn2.type = "Fawn";
		fawn2.gameObj = GameObject.Find("Fawn2");
        fawn2.collisionChecker = fawn2.gameObj.GetComponentInChildren<DeerCollisionChecker>();
        fawn2.forwardRate = 0; //30f;
		fawn2.turnRate = 0; //22.5f;
		fawn2.baseY = 0f;
		fawn2.deerCaughtFinalOffsetForward = 0.35f;
		fawn2.deerCaughtFinalOffsetSideways = 0.125f;
		
		buck = buck2;
		doe = doe2;
		fawn = fawn2;
		buckAnimator = buck2Animator;
		doeAnimator = doe2Animator;
		fawnAnimator = fawn2Animator;
		deerSet1Selected = false;
		
		Physics.gravity = new Vector3(0f, -20f, 0f);
		
		InitLevel(0);
		
		displayVar1 = "";
		displayVar2 = "";
		displayVar3 = "";
		displayVar4 = "";
		displayVar5 = "";
		displayVar6 = "";
			
		difficultyLevel = 0.9f;
	}


    public void ResetPuma()
    {
        pumaObj = AssetManager.puma.gameObject;
        pumaObjCollider = pumaObj.GetComponent<BoxCollider>();
        pumaController = pumaObj.GetComponent<PumaController>();
        pumaAnimator = pumaObj.GetComponent<Animator>();
        pumaController.SetNormalCollider();
        cameraController.puma = pumaController;
    }

	public void InitLevel(int level)
	{
		//================================
		// Initialize Objects & Variables
		//================================

		currentLevel = level;
		beginLevelFlag = true;
		gameState = "gameStateGui";
		stateStartTime = Time.time;
		mainHeading = 180f; //Random.Range(0f, 360f);

		pumaX = -691f;
		pumaY = 0f;
		pumaZ = 832f;	
		pumaObj.transform.position = new Vector3(pumaX, pumaY, pumaZ);		
		
		//================================
		// Set Up Terrain Objects
		//================================

		terrain1.SetActive(false);
		terrain2.SetActive(false);
		terrain3.SetActive(false);
		terrain4.SetActive(false);
		terrain5.SetActive(false);

		if (terrainB != null)
			Destroy(terrainB);
		if (terrainC != null)
			Destroy(terrainC);
		if (terrainD != null)
			Destroy(terrainD);
		
		switch (currentLevel) {
		case 0:
			terrainA = terrain1;
			terrainMaster = terrain1.GetComponent<Terrain>();
			break;
		case 1:
			terrainA = terrain2;
			terrainMaster = terrain2.GetComponent<Terrain>();
			break;
		case 2:
			terrainA = terrain3;
			terrainMaster = terrain3.GetComponent<Terrain>();
			break;
		case 3:
			terrainA = terrain4;
			terrainMaster = terrain4.GetComponent<Terrain>();
			break;
		case 4:
			terrainA = terrain5;
			terrainMaster = terrain5.GetComponent<Terrain>();
			break;		
		}
		
		terrainA.SetActive(true);
		terrainA.transform.position = new Vector3(terrainPosInitNEG, 0, terrainPosInitPOS);
		terrainA.GetComponent<TerrainCollider>().enabled = false; // make sure tree colliders work
		terrainA.GetComponent<TerrainCollider>().enabled = true;
		
		//terrainB = Terrain.CreateTerrainGameObject(terrainMaster.terrainData);
		terrainB = (GameObject)Instantiate(terrainA, new Vector3(0, 0, 0), Quaternion.identity);
		terrainB.transform.position = new Vector3(terrainPosInitPOS, 0, terrainPosInitPOS);
		terrainB.GetComponent<TerrainCollider>().enabled = false; // make sure tree colliders work
		terrainB.GetComponent<TerrainCollider>().enabled = true;
		
		//terrainC = Terrain.CreateTerrainGameObject(terrainMaster.terrainData);
		terrainC = (GameObject)Instantiate(terrainA, new Vector3(0, 0, 0), Quaternion.identity);
		terrainC.transform.position = new Vector3(terrainPosInitNEG, 0, terrainPosInitNEG);
		terrainC.GetComponent<TerrainCollider>().enabled = false; // make sure tree colliders work
		terrainC.GetComponent<TerrainCollider>().enabled = true;
		
		//terrainD = Terrain.CreateTerrainGameObject(terrainMaster.terrainData);
		terrainD = (GameObject)Instantiate(terrainA, new Vector3(0, 0, 0), Quaternion.identity);
		terrainD.transform.position = new Vector3(terrainPosInitPOS, 0, terrainPosInitNEG);
		terrainD.GetComponent<TerrainCollider>().enabled = false; // make sure tree colliders work
		terrainD.GetComponent<TerrainCollider>().enabled = true;
		
		SetTerrainNeighbors();
/*		
		//================================
		// Set Up Road Objects
		//================================
	
		if (road1A != null) {
			road1A = null;
			road2A = null;
			road3A = null;
			Destroy(road1B);
			road1B = null;
			Destroy(road2B);
			road2B = null;
			Destroy(road3B);
			road3B = null;
			Destroy(road1C);
			road1C = null;
			Destroy(road2C);
			road2C = null;
			Destroy(road3C);
			road3C = null;
			Destroy(road1D);
			road1D = null;
			Destroy(road2D);
			road2D = null;
			Destroy(road3D);
			road3D = null;
		}

		switch (currentLevel) {
		case 0:
			road1A = null;
			road2A = null;
			road3A = null;
			road1B = null;
			road2B = null;
			road3B = null;
			road1C = null;
			road2C = null;
			road3C = null;
			road1D = null;
			road2D = null;
			road3D = null;
			break;
		case 1:
			road1A = L2Road1;
			road2A = L2Road2;
			road3A = L2Road3;
			break;
		case 2:
			road1A = L3Road1;
			road2A = L3Road2;
			road3A = L3Road3;
			break;
		case 3:
			road1A = L4Road1;
			road2A = L4Road2;
			road3A = L4Road3;
			break;
		case 4:
			road1A = L5Road1;
			road2A = L5Road2;
			road3A = L5Road3;
			break;		
		}

		if (road1A != null) {
		
			road1A.transform.position = new Vector3(0, roadBaseY, 0);
			road1B = (GameObject)Instantiate(road1A, new Vector3(1000, roadBaseY, 0), Quaternion.identity);
			road1B.transform.parent = terrainB.transform;
			road1C = (GameObject)Instantiate(road1A, new Vector3(0, roadBaseY, -1000), Quaternion.identity);
			road1C.transform.parent = terrainC.transform;
			road1D = (GameObject)Instantiate(road1A, new Vector3(1000, roadBaseY, -1000), Quaternion.identity);
			road1D.transform.parent = terrainD.transform;
			
			road2A.transform.position = new Vector3(0, roadBaseY, 0);
			road2B = (GameObject)Instantiate(road2A, new Vector3(1000, roadBaseY, 0), Quaternion.identity);
			road2B.transform.parent = terrainB.transform;
			road2C = (GameObject)Instantiate(road2A, new Vector3(0, roadBaseY, -1000), Quaternion.identity);
			road2C.transform.parent = terrainC.transform;
			road2D = (GameObject)Instantiate(road2A, new Vector3(1000, roadBaseY, -1000), Quaternion.identity);
			road2D.transform.parent = terrainD.transform;
			
			road3A.transform.position = new Vector3(0, roadBaseY, 0);
			road3B = (GameObject)Instantiate(road3A, new Vector3(1000, roadBaseY, 0), Quaternion.identity);
			road3B.transform.parent = terrainB.transform;
			road3C = (GameObject)Instantiate(road3A, new Vector3(0, roadBaseY, -1000), Quaternion.identity);
			road3C.transform.parent = terrainC.transform;
			road3D = (GameObject)Instantiate(road3A, new Vector3(1000, roadBaseY, -1000), Quaternion.identity);
			road3D.transform.parent = terrainD.transform;
		}

		//================================
		// Set Up Bridge Objects
		//================================
	
		if (bridge1A != null) {
			bridge1A = null;
			bridge2A = null;
			Destroy(bridge1B);
			bridge1B = null;
			Destroy(bridge2B);
			bridge2B = null;
			Destroy(bridge1C);
			bridge1C = null;
			Destroy(bridge2C);
			bridge2C = null;
			Destroy(bridge1D);
			bridge1D = null;
			Destroy(bridge2D);
			bridge2D = null;
		}

		switch (currentLevel) {
		case 0:
		case 1:
			bridge1A = null;
			bridge2A = null;
			bridge1B = null;
			bridge2B = null;
			bridge1C = null;
			bridge2C = null;
			bridge1D = null;
			bridge2D = null;
			break;
		case 2:
			bridge1A = L3Bridge1;
			bridge2A = L3Bridge2;
			break;
		case 3:
			bridge1A = L4Bridge1;
			bridge2A = L4Bridge2;
			break;
		case 4:
			bridge1A = L5Bridge1;
			bridge2A = L5Bridge2;
			break;		
		}

		if (bridge1A != null) {
			float bridge1x = bridge1A.transform.position.x;
			float bridge1y = bridge1A.transform.position.y;
			float bridge1z = bridge1A.transform.position.z;		
			bridge1B = (GameObject)Instantiate(bridge1A, new Vector3(bridge1x + 1000, bridge1y, bridge1z), bridge1A.transform.rotation);
			bridge1B.transform.parent = terrainB.transform;
			bridge1C = (GameObject)Instantiate(bridge1A, new Vector3(bridge1x, bridge1y, bridge1z - 1000), bridge1A.transform.rotation);
			bridge1C.transform.parent = terrainC.transform;
			bridge1D = (GameObject)Instantiate(bridge1A, new Vector3(bridge1x + 1000, bridge1y, bridge1z - 1000), bridge1A.transform.rotation);
			bridge1D.transform.parent = terrainD.transform;
			
			float bridge2x = bridge2A.transform.position.x;
			float bridge2y = bridge2A.transform.position.y;
			float bridge2z = bridge2A.transform.position.z;
			bridge2B = (GameObject)Instantiate(bridge2A, new Vector3(bridge2x + 1000, bridge2y, bridge2z), bridge2A.transform.rotation);
			bridge2B.transform.parent = terrainB.transform;
			bridge2C = (GameObject)Instantiate(bridge2A, new Vector3(bridge2x, bridge2y, bridge2z - 1000), bridge2A.transform.rotation);
			bridge2C.transform.parent = terrainC.transform;
			bridge2D = (GameObject)Instantiate(bridge2A, new Vector3(bridge2x + 1000, bridge2y, bridge2z - 1000), bridge2A.transform.rotation);
			bridge2D.transform.parent = terrainD.transform;
		}

		//================================
		// Set Up Overpass Objects
		//================================
	
		if (overpass1A != null) {

			// delete objects and clear pointers

			overpass1A = null;
			overpass2A = null;
			overpass3A = null;
			overpass4A = null;
			overpass5A = null;
			overpass6A = null;
			overpass7A = null;
			overpass8A = null;
			overpass9A = null;
	
			Destroy(overpass1B);
			overpass1B = null;
			Destroy(overpass2B);
			overpass2B = null;
			Destroy(overpass3B);
			overpass3B = null;
			Destroy(overpass4B);
			overpass4B = null;
			Destroy(overpass5B);
			overpass5B = null;
			Destroy(overpass6B);
			overpass6B = null;
			Destroy(overpass7B);
			overpass7B = null;
			Destroy(overpass8B);
			overpass8B = null;
			Destroy(overpass9B);
			overpass9B = null;
	
			Destroy(overpass1C);
			overpass1C = null;
			Destroy(overpass2C);
			overpass2C = null;
			Destroy(overpass3C);
			overpass3C = null;
			Destroy(overpass4C);
			overpass4C = null;
			Destroy(overpass5C);
			overpass5C = null;
			Destroy(overpass6C);
			overpass6C = null;
			Destroy(overpass7C);
			overpass7C = null;
			Destroy(overpass8C);
			overpass8C = null;
			Destroy(overpass9C);
			overpass9C = null;
	
			Destroy(overpass1D);
			overpass1D = null;
			Destroy(overpass2D);
			overpass2D = null;
			Destroy(overpass3D);
			overpass3D = null;
			Destroy(overpass4D);
			overpass4D = null;
			Destroy(overpass5D);
			overpass5D = null;
			Destroy(overpass6D);
			overpass6D = null;
			Destroy(overpass7D);
			overpass7D = null;
			Destroy(overpass8D);
			overpass8D = null;
			Destroy(overpass9D);
			overpass9D = null;
		}

		switch (currentLevel) {
		case 0:
		case 1:
		case 2:
		case 3:
			break;

		case 4:
			overpass1A = overpass1;
			overpass2A = overpass2;
			overpass3A = overpass3;
			overpass4A = overpass4;
			overpass5A = overpass5;
			overpass6A = overpass6;
			overpass7A = overpass7;
			overpass8A = overpass8;
			overpass9A = overpass9;
			break;		
		}

		if (overpass1A != null) {
			Vector3 terrainOffsetB = new Vector3(1000f, 0f, 0f);
			Vector3 terrainOffsetC = new Vector3(0f, 0f, -1000f);
			Vector3 terrainOffsetD = new Vector3(1000f, 0f, -1000f);

			overpass1B = (GameObject)Instantiate(overpass1A, overpass1A.transform.position + terrainOffsetB, overpass1A.transform.rotation);
			overpass1B.transform.parent = terrainB.transform;
			overpass1C = (GameObject)Instantiate(overpass1A, overpass1A.transform.position + terrainOffsetC, overpass1A.transform.rotation);
			overpass1C.transform.parent = terrainC.transform;
			overpass1D = (GameObject)Instantiate(overpass1A, overpass1A.transform.position + terrainOffsetD, overpass1A.transform.rotation);
			overpass1D.transform.parent = terrainD.transform;

			overpass2B = (GameObject)Instantiate(overpass2A, overpass2A.transform.position + terrainOffsetB, overpass2A.transform.rotation);
			overpass2B.transform.parent = terrainB.transform;
			overpass2C = (GameObject)Instantiate(overpass2A, overpass2A.transform.position + terrainOffsetC, overpass2A.transform.rotation);
			overpass2C.transform.parent = terrainC.transform;
			overpass2D = (GameObject)Instantiate(overpass2A, overpass2A.transform.position + terrainOffsetD, overpass2A.transform.rotation);
			overpass2D.transform.parent = terrainD.transform;

			overpass3B = (GameObject)Instantiate(overpass3A, overpass3A.transform.position + terrainOffsetB, overpass3A.transform.rotation);
			overpass3B.transform.parent = terrainB.transform;
			overpass3C = (GameObject)Instantiate(overpass3A, overpass3A.transform.position + terrainOffsetC, overpass3A.transform.rotation);
			overpass3C.transform.parent = terrainC.transform;
			overpass3D = (GameObject)Instantiate(overpass3A, overpass3A.transform.position + terrainOffsetD, overpass3A.transform.rotation);
			overpass3D.transform.parent = terrainD.transform;

			overpass4B = (GameObject)Instantiate(overpass4A, overpass4A.transform.position + terrainOffsetB, overpass4A.transform.rotation);
			overpass4B.transform.parent = terrainB.transform;
			overpass4C = (GameObject)Instantiate(overpass4A, overpass4A.transform.position + terrainOffsetC, overpass4A.transform.rotation);
			overpass4C.transform.parent = terrainC.transform;
			overpass4D = (GameObject)Instantiate(overpass4A, overpass4A.transform.position + terrainOffsetD, overpass4A.transform.rotation);
			overpass4D.transform.parent = terrainD.transform;

			overpass5B = (GameObject)Instantiate(overpass5A, overpass5A.transform.position + terrainOffsetB, overpass5A.transform.rotation);
			overpass5B.transform.parent = terrainB.transform;
			overpass5C = (GameObject)Instantiate(overpass5A, overpass5A.transform.position + terrainOffsetC, overpass5A.transform.rotation);
			overpass5C.transform.parent = terrainC.transform;
			overpass5D = (GameObject)Instantiate(overpass5A, overpass5A.transform.position + terrainOffsetD, overpass5A.transform.rotation);
			overpass5D.transform.parent = terrainD.transform;

			overpass6B = (GameObject)Instantiate(overpass6A, overpass6A.transform.position + terrainOffsetB, overpass6A.transform.rotation);
			overpass6B.transform.parent = terrainB.transform;
			overpass6C = (GameObject)Instantiate(overpass6A, overpass6A.transform.position + terrainOffsetC, overpass6A.transform.rotation);
			overpass6C.transform.parent = terrainC.transform;
			overpass6D = (GameObject)Instantiate(overpass6A, overpass6A.transform.position + terrainOffsetD, overpass6A.transform.rotation);
			overpass6D.transform.parent = terrainD.transform;

			overpass7B = (GameObject)Instantiate(overpass7A, overpass7A.transform.position + terrainOffsetB, overpass7A.transform.rotation);
			overpass7B.transform.parent = terrainB.transform;
			overpass7C = (GameObject)Instantiate(overpass7A, overpass7A.transform.position + terrainOffsetC, overpass7A.transform.rotation);
			overpass7C.transform.parent = terrainC.transform;
			overpass7D = (GameObject)Instantiate(overpass7A, overpass7A.transform.position + terrainOffsetD, overpass7A.transform.rotation);
			overpass7D.transform.parent = terrainD.transform;

			overpass8B = (GameObject)Instantiate(overpass8A, overpass8A.transform.position + terrainOffsetB, overpass8A.transform.rotation);
			overpass8B.transform.parent = terrainB.transform;
			overpass8C = (GameObject)Instantiate(overpass8A, overpass8A.transform.position + terrainOffsetC, overpass8A.transform.rotation);
			overpass8C.transform.parent = terrainC.transform;
			overpass8D = (GameObject)Instantiate(overpass8A, overpass8A.transform.position + terrainOffsetD, overpass8A.transform.rotation);
			overpass8D.transform.parent = terrainD.transform;

			overpass9B = (GameObject)Instantiate(overpass9A, overpass9A.transform.position + terrainOffsetB, overpass9A.transform.rotation);
			overpass9B.transform.parent = terrainB.transform;
			overpass9C = (GameObject)Instantiate(overpass9A, overpass9A.transform.position + terrainOffsetC, overpass9A.transform.rotation);
			overpass9C.transform.parent = terrainC.transform;
			overpass9D = (GameObject)Instantiate(overpass9A, overpass9A.transform.position + terrainOffsetD, overpass9A.transform.rotation);
			overpass9D.transform.parent = terrainD.transform;
		}

		//================================
		// Set Up Underpass Objects
		//================================
	
		if (underpass1A != null) {

			// delete objects and clear pointers

			underpass1A = null;
			underpass2A = null;
			underpass3A = null;
			underpass4A = null;
			underpass5A = null;
			underpass6A = null;
			underpass7A = null;
	
			Destroy(underpass1B);
			underpass1B = null;
			Destroy(underpass2B);
			underpass2B = null;
			Destroy(underpass3B);
			underpass3B = null;
			Destroy(underpass4B);
			underpass4B = null;
			Destroy(underpass5B);
			underpass5B = null;
			Destroy(underpass6B);
			underpass6B = null;
			Destroy(underpass7B);
			underpass7B = null;
	
			Destroy(underpass1C);
			underpass1C = null;
			Destroy(underpass2C);
			underpass2C = null;
			Destroy(underpass3C);
			underpass3C = null;
			Destroy(underpass4C);
			underpass4C = null;
			Destroy(underpass5C);
			underpass5C = null;
			Destroy(underpass6C);
			underpass6C = null;
			Destroy(underpass7C);
			underpass7C = null;
	
			Destroy(underpass1D);
			underpass1D = null;
			Destroy(underpass2D);
			underpass2D = null;
			Destroy(underpass3D);
			underpass3D = null;
			Destroy(underpass4D);
			underpass4D = null;
			Destroy(underpass5D);
			underpass5D = null;
			Destroy(underpass6D);
			underpass6D = null;
			Destroy(underpass7D);
			underpass7D = null;
		}

		switch (currentLevel) {
		case 0:
		case 1:
		case 2:
		case 3:
			break;

		case 4:
			underpass1A = underpass1;
			underpass2A = underpass2;
			underpass3A = underpass3;
			underpass4A = underpass4;
			underpass5A = underpass5;
			underpass6A = underpass6;
			underpass7A = underpass7;
			break;		
		}

		if (underpass1A != null) {
			Vector3 terrainOffsetB = new Vector3(1000f, 0f, 0f);
			Vector3 terrainOffsetC = new Vector3(0f, 0f, -1000f);
			Vector3 terrainOffsetD = new Vector3(1000f, 0f, -1000f);

			underpass1B = (GameObject)Instantiate(underpass1A, underpass1A.transform.position + terrainOffsetB, underpass1A.transform.rotation);
			underpass1B.transform.parent = terrainB.transform;
			underpass1C = (GameObject)Instantiate(underpass1A, underpass1A.transform.position + terrainOffsetC, underpass1A.transform.rotation);
			underpass1C.transform.parent = terrainC.transform;
			underpass1D = (GameObject)Instantiate(underpass1A, underpass1A.transform.position + terrainOffsetD, underpass1A.transform.rotation);
			underpass1D.transform.parent = terrainD.transform;

			underpass2B = (GameObject)Instantiate(underpass2A, underpass2A.transform.position + terrainOffsetB, underpass2A.transform.rotation);
			underpass2B.transform.parent = terrainB.transform;
			underpass2C = (GameObject)Instantiate(underpass2A, underpass2A.transform.position + terrainOffsetC, underpass2A.transform.rotation);
			underpass2C.transform.parent = terrainC.transform;
			underpass2D = (GameObject)Instantiate(underpass2A, underpass2A.transform.position + terrainOffsetD, underpass2A.transform.rotation);
			underpass2D.transform.parent = terrainD.transform;

			underpass3B = (GameObject)Instantiate(underpass3A, underpass3A.transform.position + terrainOffsetB, underpass3A.transform.rotation);
			underpass3B.transform.parent = terrainB.transform;
			underpass3C = (GameObject)Instantiate(underpass3A, underpass3A.transform.position + terrainOffsetC, underpass3A.transform.rotation);
			underpass3C.transform.parent = terrainC.transform;
			underpass3D = (GameObject)Instantiate(underpass3A, underpass3A.transform.position + terrainOffsetD, underpass3A.transform.rotation);
			underpass3D.transform.parent = terrainD.transform;

			underpass4B = (GameObject)Instantiate(underpass4A, underpass4A.transform.position + terrainOffsetB, underpass4A.transform.rotation);
			underpass4B.transform.parent = terrainB.transform;
			underpass4C = (GameObject)Instantiate(underpass4A, underpass4A.transform.position + terrainOffsetC, underpass4A.transform.rotation);
			underpass4C.transform.parent = terrainC.transform;
			underpass4D = (GameObject)Instantiate(underpass4A, underpass4A.transform.position + terrainOffsetD, underpass4A.transform.rotation);
			underpass4D.transform.parent = terrainD.transform;

			underpass5B = (GameObject)Instantiate(underpass5A, underpass5A.transform.position + terrainOffsetB, underpass5A.transform.rotation);
			underpass5B.transform.parent = terrainB.transform;
			underpass5C = (GameObject)Instantiate(underpass5A, underpass5A.transform.position + terrainOffsetC, underpass5A.transform.rotation);
			underpass5C.transform.parent = terrainC.transform;
			underpass5D = (GameObject)Instantiate(underpass5A, underpass5A.transform.position + terrainOffsetD, underpass5A.transform.rotation);
			underpass5D.transform.parent = terrainD.transform;

			underpass6B = (GameObject)Instantiate(underpass6A, underpass6A.transform.position + terrainOffsetB, underpass6A.transform.rotation);
			underpass6B.transform.parent = terrainB.transform;
			underpass6C = (GameObject)Instantiate(underpass6A, underpass6A.transform.position + terrainOffsetC, underpass6A.transform.rotation);
			underpass6C.transform.parent = terrainC.transform;
			underpass6D = (GameObject)Instantiate(underpass6A, underpass6A.transform.position + terrainOffsetD, underpass6A.transform.rotation);
			underpass6D.transform.parent = terrainD.transform;

			underpass7B = (GameObject)Instantiate(underpass7A, underpass7A.transform.position + terrainOffsetB, underpass7A.transform.rotation);
			underpass7B.transform.parent = terrainB.transform;
			underpass7C = (GameObject)Instantiate(underpass7A, underpass7A.transform.position + terrainOffsetC, underpass7A.transform.rotation);
			underpass7C.transform.parent = terrainC.transform;
			underpass7D = (GameObject)Instantiate(underpass7A, underpass7A.transform.position + terrainOffsetD, underpass7A.transform.rotation);
			underpass7D.transform.parent = terrainD.transform;
		}
*/
		//================================
		// Set Up Car Objects
		//================================
	
		trafficManager.InitLevel(currentLevel);
        ResetPuma();
	}
	
	public void SwapLevel(int level)
	{
		if (level == currentLevel)
			return;
		
		currentLevel = level;
			
		float terrainAx = terrainA.transform.position.x;
		float terrainAz = terrainA.transform.position.z;
		float terrainBx = terrainB.transform.position.x;
		float terrainBz = terrainB.transform.position.z;
		float terrainCx = terrainC.transform.position.x;
		float terrainCz = terrainC.transform.position.z;
		float terrainDx = terrainD.transform.position.x;
		float terrainDz = terrainD.transform.position.z;

		terrain1.SetActive(false);
		terrain2.SetActive(false);
		terrain3.SetActive(false);
		terrain4.SetActive(false);
		terrain5.SetActive(false);
		
		if (terrainB != null)
			Destroy(terrainB);
		if (terrainC != null)
			Destroy(terrainC);
		if (terrainD != null)
			Destroy(terrainD);
		
		switch (currentLevel) {
		case 0:
			terrainA = terrain1;
			terrainMaster = terrain1.GetComponent<Terrain>();
			break;
		case 1:
			terrainA = terrain2;
			terrainMaster = terrain2.GetComponent<Terrain>();
			break;
		case 2:
			terrainA = terrain3;
			terrainMaster = terrain3.GetComponent<Terrain>();
			break;
		case 3:
			terrainA = terrain4;
			terrainMaster = terrain4.GetComponent<Terrain>();
			break;
		case 4:
			terrainA = terrain5;
			terrainMaster = terrain5.GetComponent<Terrain>();
			break;		
		}

		terrainA.SetActive(true);
		terrainA.transform.position = new Vector3(terrainAx, 0, terrainAz);
		terrainA.GetComponent<TerrainCollider>().enabled = false; // make sure tree colliders work
		terrainA.GetComponent<TerrainCollider>().enabled = true;

		//terrainB = Terrain.CreateTerrainGameObject(terrainMaster.terrainData);
		terrainB = (GameObject)Instantiate(terrainA, new Vector3(0, 0, 0), Quaternion.identity);
		terrainB.transform.position = new Vector3(terrainBx, 0, terrainBz);
		terrainB.GetComponent<TerrainCollider>().enabled = false; // make sure tree colliders work
		terrainB.GetComponent<TerrainCollider>().enabled = true;
		
		//terrainC = Terrain.CreateTerrainGameObject(terrainMaster.terrainData);
		terrainC = (GameObject)Instantiate(terrainA, new Vector3(0, 0, 0), Quaternion.identity);
		terrainC.transform.position = new Vector3(terrainCx, 0, terrainCz);
		terrainC.GetComponent<TerrainCollider>().enabled = false; // make sure tree colliders work
		terrainC.GetComponent<TerrainCollider>().enabled = true;
		
		//terrainD = Terrain.CreateTerrainGameObject(terrainMaster.terrainData);
		terrainD = (GameObject)Instantiate(terrainA, new Vector3(0, 0, 0), Quaternion.identity);
		terrainD.transform.position = new Vector3(terrainDx, 0, terrainDz);
		terrainD.GetComponent<TerrainCollider>().enabled = false; // make sure tree colliders work
		terrainD.GetComponent<TerrainCollider>().enabled = true;
		
		SetTerrainNeighbors();
/*		
		//================================
		// Set Up Road Objects
		//================================
	
		if (road1A != null) {
			road1A = null;
			road2A = null;
			road3A = null;
			Destroy(road1B);
			road1B = null;
			Destroy(road2B);
			road2B = null;
			Destroy(road3B);
			road3B = null;
			Destroy(road1C);
			road1C = null;
			Destroy(road2C);
			road2C = null;
			Destroy(road3C);
			road3C = null;
			Destroy(road1D);
			road1D = null;
			Destroy(road2D);
			road2D = null;
			Destroy(road3D);
			road3D = null;
		}

		switch (currentLevel) {
		case 0:
			road1A = null;
			road2A = null;
			road3A = null;
			road1B = null;
			road2B = null;
			road3B = null;
			road1C = null;
			road2C = null;
			road3C = null;
			road1D = null;
			road2D = null;
			road3D = null;
			break;
		case 1:
			road1A = L2Road1;
			road2A = L2Road2;
			road3A = L2Road3;
			break;
		case 2:
			road1A = L3Road1;
			road2A = L3Road2;
			road3A = L3Road3;
			break;
		case 3:
			road1A = L4Road1;
			road2A = L4Road2;
			road3A = L4Road3;
			break;
		case 4:
			road1A = L5Road1;
			road2A = L5Road2;
			road3A = L5Road3;
			break;		
		}

		if (road1A != null) {
		
			road1A.transform.position = new Vector3(terrainAx + 1000f, roadBaseY, terrainAz);
			road1B = (GameObject)Instantiate(road1A, new Vector3(terrainBx + 1000f, roadBaseY, terrainBz), Quaternion.identity);
			road1B.transform.parent = terrainB.transform;
			road1C = (GameObject)Instantiate(road1A, new Vector3(terrainCx + 1000f, roadBaseY, terrainCz), Quaternion.identity);
			road1C.transform.parent = terrainC.transform;
			road1D = (GameObject)Instantiate(road1A, new Vector3(terrainDx + 1000f, roadBaseY, terrainDz), Quaternion.identity);
			road1D.transform.parent = terrainD.transform;
			
			road2A.transform.position = new Vector3(terrainAx + 1000f, roadBaseY, terrainAz);
			road2B = (GameObject)Instantiate(road2A, new Vector3(terrainBx + 1000f, roadBaseY, terrainBz), Quaternion.identity);
			road2B.transform.parent = terrainB.transform;
			road2C = (GameObject)Instantiate(road2A, new Vector3(terrainCx + 1000f, roadBaseY, terrainCz), Quaternion.identity);
			road2C.transform.parent = terrainC.transform;
			road2D = (GameObject)Instantiate(road2A, new Vector3(terrainDx + 1000f, roadBaseY, terrainDz), Quaternion.identity);
			road2D.transform.parent = terrainD.transform;
			
			road3A.transform.position = new Vector3(terrainAx + 1000f, roadBaseY, terrainAz);
			road3B = (GameObject)Instantiate(road3A, new Vector3(terrainBx + 1000f, roadBaseY, terrainBz), Quaternion.identity);
			road3B.transform.parent = terrainB.transform;
			road3C = (GameObject)Instantiate(road3A, new Vector3(terrainCx + 1000f, roadBaseY, terrainCz), Quaternion.identity);
			road3C.transform.parent = terrainC.transform;
			road3D = (GameObject)Instantiate(road3A, new Vector3(terrainDx + 1000f, roadBaseY, terrainDz), Quaternion.identity);
			road3D.transform.parent = terrainD.transform;
		}

		//================================
		// Set Up Bridge Objects
		//================================
	
		if (bridge1A != null) {
			bridge1A = null;
			bridge2A = null;
			Destroy(bridge1B);
			bridge1B = null;
			Destroy(bridge2B);
			bridge2B = null;
			Destroy(bridge1C);
			bridge1C = null;
			Destroy(bridge2C);
			bridge2C = null;
			Destroy(bridge1D);
			bridge1D = null;
			Destroy(bridge2D);
			bridge2D = null;
		}

		switch (currentLevel) {
		case 0:
		case 1:
			bridge1A = null;
			bridge2A = null;
			bridge1B = null;
			bridge2B = null;
			bridge1C = null;
			bridge2C = null;
			bridge1D = null;
			bridge2D = null;
			break;
		case 2:
			bridge1A = L3Bridge1;
			bridge2A = L3Bridge2;
			break;
		case 3:
			bridge1A = L4Bridge1;
			bridge2A = L4Bridge2;
			break;
		case 4:
			bridge1A = L5Bridge1;
			bridge2A = L5Bridge2;
			break;		
		}

		if (bridge1A != null) {
			Vector3 bridge1Position = bridge1A.transform.position - terrainA.transform.position;
			bridge1B = (GameObject)Instantiate(bridge1A, terrainB.transform.position + bridge1Position, bridge1A.transform.rotation);
			bridge1B.transform.parent = terrainB.transform;
			bridge1C = (GameObject)Instantiate(bridge1A, terrainC.transform.position + bridge1Position, bridge1A.transform.rotation);
			bridge1C.transform.parent = terrainC.transform;
			bridge1D = (GameObject)Instantiate(bridge1A, terrainD.transform.position + bridge1Position, bridge1A.transform.rotation);
			bridge1D.transform.parent = terrainD.transform;
			
			Vector3 bridge2Position = bridge2A.transform.position - terrainA.transform.position;
			bridge2B = (GameObject)Instantiate(bridge2A, terrainB.transform.position + bridge2Position, bridge2A.transform.rotation);
			bridge2B.transform.parent = terrainB.transform;
			bridge2C = (GameObject)Instantiate(bridge2A, terrainC.transform.position + bridge2Position, bridge2A.transform.rotation);
			bridge2C.transform.parent = terrainC.transform;
			bridge2D = (GameObject)Instantiate(bridge2A, terrainD.transform.position + bridge2Position, bridge2A.transform.rotation);
			bridge2D.transform.parent = terrainD.transform;
		}

		//================================
		// Set Up Overpass Objects
		//================================
	
		if (overpass1A != null) {

			// delete objects and clear pointers

			overpass1A = null;
			overpass2A = null;
			overpass3A = null;
			overpass4A = null;
			overpass5A = null;
			overpass6A = null;
			overpass7A = null;
			overpass8A = null;
			overpass9A = null;
	
			Destroy(overpass1B);
			overpass1B = null;
			Destroy(overpass2B);
			overpass2B = null;
			Destroy(overpass3B);
			overpass3B = null;
			Destroy(overpass4B);
			overpass4B = null;
			Destroy(overpass5B);
			overpass5B = null;
			Destroy(overpass6B);
			overpass6B = null;
			Destroy(overpass7B);
			overpass7B = null;
			Destroy(overpass8B);
			overpass8B = null;
			Destroy(overpass9B);
			overpass9B = null;
	
			Destroy(overpass1C);
			overpass1C = null;
			Destroy(overpass2C);
			overpass2C = null;
			Destroy(overpass3C);
			overpass3C = null;
			Destroy(overpass4C);
			overpass4C = null;
			Destroy(overpass5C);
			overpass5C = null;
			Destroy(overpass6C);
			overpass6C = null;
			Destroy(overpass7C);
			overpass7C = null;
			Destroy(overpass8C);
			overpass8C = null;
			Destroy(overpass9C);
			overpass9C = null;
	
			Destroy(overpass1D);
			overpass1D = null;
			Destroy(overpass2D);
			overpass2D = null;
			Destroy(overpass3D);
			overpass3D = null;
			Destroy(overpass4D);
			overpass4D = null;
			Destroy(overpass5D);
			overpass5D = null;
			Destroy(overpass6D);
			overpass6D = null;
			Destroy(overpass7D);
			overpass7D = null;
			Destroy(overpass8D);
			overpass8D = null;
			Destroy(overpass9D);
			overpass9D = null;
		}

		switch (currentLevel) {
		case 0:
		case 1:
		case 2:
		case 3:
			break;

		case 4:
			overpass1A = overpass1;
			overpass2A = overpass2;
			overpass3A = overpass3;
			overpass4A = overpass4;
			overpass5A = overpass5;
			overpass6A = overpass6;
			overpass7A = overpass7;
			overpass8A = overpass8;
			overpass9A = overpass9;
			break;		
		}
		if (overpass1A != null) {
			Vector3 overpassPosition;

			overpassPosition = overpass1A.transform.position - terrainA.transform.position;
			overpass1B = (GameObject)Instantiate(overpass1A, terrainB.transform.position + overpassPosition, overpass1A.transform.rotation);
			overpass1B.transform.parent = terrainB.transform;
			overpass1C = (GameObject)Instantiate(overpass1A, terrainC.transform.position + overpassPosition, overpass1A.transform.rotation);
			overpass1C.transform.parent = terrainC.transform;
			overpass1D = (GameObject)Instantiate(overpass1A, terrainD.transform.position + overpassPosition, overpass1A.transform.rotation);
			overpass1D.transform.parent = terrainD.transform;
			
			overpassPosition = overpass2A.transform.position - terrainA.transform.position;
			overpass2B = (GameObject)Instantiate(overpass2A, terrainB.transform.position + overpassPosition, overpass2A.transform.rotation);
			overpass2B.transform.parent = terrainB.transform;
			overpass2C = (GameObject)Instantiate(overpass2A, terrainC.transform.position + overpassPosition, overpass2A.transform.rotation);
			overpass2C.transform.parent = terrainC.transform;
			overpass2D = (GameObject)Instantiate(overpass2A, terrainD.transform.position + overpassPosition, overpass2A.transform.rotation);
			overpass2D.transform.parent = terrainD.transform;

			overpassPosition = overpass3A.transform.position - terrainA.transform.position;
			overpass3B = (GameObject)Instantiate(overpass3A, terrainB.transform.position + overpassPosition, overpass3A.transform.rotation);
			overpass3B.transform.parent = terrainB.transform;
			overpass3C = (GameObject)Instantiate(overpass3A, terrainC.transform.position + overpassPosition, overpass3A.transform.rotation);
			overpass3C.transform.parent = terrainC.transform;
			overpass3D = (GameObject)Instantiate(overpass3A, terrainD.transform.position + overpassPosition, overpass3A.transform.rotation);
			overpass3D.transform.parent = terrainD.transform;

			overpassPosition = overpass4A.transform.position - terrainA.transform.position;
			overpass4B = (GameObject)Instantiate(overpass4A, terrainB.transform.position + overpassPosition, overpass4A.transform.rotation);
			overpass4B.transform.parent = terrainB.transform;
			overpass4C = (GameObject)Instantiate(overpass4A, terrainC.transform.position + overpassPosition, overpass4A.transform.rotation);
			overpass4C.transform.parent = terrainC.transform;
			overpass4D = (GameObject)Instantiate(overpass4A, terrainD.transform.position + overpassPosition, overpass4A.transform.rotation);
			overpass4D.transform.parent = terrainD.transform;

			overpassPosition = overpass5A.transform.position - terrainA.transform.position;
			overpass5B = (GameObject)Instantiate(overpass5A, terrainB.transform.position + overpassPosition, overpass5A.transform.rotation);
			overpass5B.transform.parent = terrainB.transform;
			overpass5C = (GameObject)Instantiate(overpass5A, terrainC.transform.position + overpassPosition, overpass5A.transform.rotation);
			overpass5C.transform.parent = terrainC.transform;
			overpass5D = (GameObject)Instantiate(overpass5A, terrainD.transform.position + overpassPosition, overpass5A.transform.rotation);
			overpass5D.transform.parent = terrainD.transform;

			overpassPosition = overpass6A.transform.position - terrainA.transform.position;
			overpass6B = (GameObject)Instantiate(overpass6A, terrainB.transform.position + overpassPosition, overpass6A.transform.rotation);
			overpass6B.transform.parent = terrainB.transform;
			overpass6C = (GameObject)Instantiate(overpass6A, terrainC.transform.position + overpassPosition, overpass6A.transform.rotation);
			overpass6C.transform.parent = terrainC.transform;
			overpass6D = (GameObject)Instantiate(overpass6A, terrainD.transform.position + overpassPosition, overpass6A.transform.rotation);
			overpass6D.transform.parent = terrainD.transform;

			overpassPosition = overpass7A.transform.position - terrainA.transform.position;
			overpass7B = (GameObject)Instantiate(overpass7A, terrainB.transform.position + overpassPosition, overpass7A.transform.rotation);
			overpass7B.transform.parent = terrainB.transform;
			overpass7C = (GameObject)Instantiate(overpass7A, terrainC.transform.position + overpassPosition, overpass7A.transform.rotation);
			overpass7C.transform.parent = terrainC.transform;
			overpass7D = (GameObject)Instantiate(overpass7A, terrainD.transform.position + overpassPosition, overpass7A.transform.rotation);
			overpass7D.transform.parent = terrainD.transform;

			overpassPosition = overpass8A.transform.position - terrainA.transform.position;
			overpass8B = (GameObject)Instantiate(overpass8A, terrainB.transform.position + overpassPosition, overpass8A.transform.rotation);
			overpass8B.transform.parent = terrainB.transform;
			overpass8C = (GameObject)Instantiate(overpass8A, terrainC.transform.position + overpassPosition, overpass8A.transform.rotation);
			overpass8C.transform.parent = terrainC.transform;
			overpass8D = (GameObject)Instantiate(overpass8A, terrainD.transform.position + overpassPosition, overpass8A.transform.rotation);
			overpass8D.transform.parent = terrainD.transform;

			overpassPosition = overpass9A.transform.position - terrainA.transform.position;
			overpass9B = (GameObject)Instantiate(overpass9A, terrainB.transform.position + overpassPosition, overpass9A.transform.rotation);
			overpass9B.transform.parent = terrainB.transform;
			overpass9C = (GameObject)Instantiate(overpass9A, terrainC.transform.position + overpassPosition, overpass9A.transform.rotation);
			overpass9C.transform.parent = terrainC.transform;
			overpass9D = (GameObject)Instantiate(overpass9A, terrainD.transform.position + overpassPosition, overpass9A.transform.rotation);
			overpass9D.transform.parent = terrainD.transform;
		}

		//================================
		// Set Up Underpass Objects
		//================================
	
		if (underpass1A != null) {

			// delete objects and clear pointers

			underpass1A = null;
			underpass2A = null;
			underpass3A = null;
			underpass4A = null;
			underpass5A = null;
			underpass6A = null;
			underpass7A = null;
	
			Destroy(underpass1B);
			underpass1B = null;
			Destroy(underpass2B);
			underpass2B = null;
			Destroy(underpass3B);
			underpass3B = null;
			Destroy(underpass4B);
			underpass4B = null;
			Destroy(underpass5B);
			underpass5B = null;
			Destroy(underpass6B);
			underpass6B = null;
			Destroy(underpass7B);
			underpass7B = null;
	
			Destroy(underpass1C);
			underpass1C = null;
			Destroy(underpass2C);
			underpass2C = null;
			Destroy(underpass3C);
			underpass3C = null;
			Destroy(underpass4C);
			underpass4C = null;
			Destroy(underpass5C);
			underpass5C = null;
			Destroy(underpass6C);
			underpass6C = null;
			Destroy(underpass7C);
			underpass7C = null;
	
			Destroy(underpass1D);
			underpass1D = null;
			Destroy(underpass2D);
			underpass2D = null;
			Destroy(underpass3D);
			underpass3D = null;
			Destroy(underpass4D);
			underpass4D = null;
			Destroy(underpass5D);
			underpass5D = null;
			Destroy(underpass6D);
			underpass6D = null;
			Destroy(underpass7D);
			underpass7D = null;
		}

		switch (currentLevel) {
		case 0:
		case 1:
		case 2:
		case 3:
			break;

		case 4:
			underpass1A = underpass1;
			underpass2A = underpass2;
			underpass3A = underpass3;
			underpass4A = underpass4;
			underpass5A = underpass5;
			underpass6A = underpass6;
			underpass7A = underpass7;
			break;		
		}
		if (underpass1A != null) {
			Vector3 underpassPosition;

			underpassPosition = underpass1A.transform.position - terrainA.transform.position;
			underpass1B = (GameObject)Instantiate(underpass1A, terrainB.transform.position + underpassPosition, underpass1A.transform.rotation);
			underpass1B.transform.parent = terrainB.transform;
			underpass1C = (GameObject)Instantiate(underpass1A, terrainC.transform.position + underpassPosition, underpass1A.transform.rotation);
			underpass1C.transform.parent = terrainC.transform;
			underpass1D = (GameObject)Instantiate(underpass1A, terrainD.transform.position + underpassPosition, underpass1A.transform.rotation);
			underpass1D.transform.parent = terrainD.transform;
			
			underpassPosition = underpass2A.transform.position - terrainA.transform.position;
			underpass2B = (GameObject)Instantiate(underpass2A, terrainB.transform.position + underpassPosition, underpass2A.transform.rotation);
			underpass2B.transform.parent = terrainB.transform;
			underpass2C = (GameObject)Instantiate(underpass2A, terrainC.transform.position + underpassPosition, underpass2A.transform.rotation);
			underpass2C.transform.parent = terrainC.transform;
			underpass2D = (GameObject)Instantiate(underpass2A, terrainD.transform.position + underpassPosition, underpass2A.transform.rotation);
			underpass2D.transform.parent = terrainD.transform;

			underpassPosition = underpass3A.transform.position - terrainA.transform.position;
			underpass3B = (GameObject)Instantiate(underpass3A, terrainB.transform.position + underpassPosition, underpass3A.transform.rotation);
			underpass3B.transform.parent = terrainB.transform;
			underpass3C = (GameObject)Instantiate(underpass3A, terrainC.transform.position + underpassPosition, underpass3A.transform.rotation);
			underpass3C.transform.parent = terrainC.transform;
			underpass3D = (GameObject)Instantiate(underpass3A, terrainD.transform.position + underpassPosition, underpass3A.transform.rotation);
			underpass3D.transform.parent = terrainD.transform;

			underpassPosition = underpass4A.transform.position - terrainA.transform.position;
			underpass4B = (GameObject)Instantiate(underpass4A, terrainB.transform.position + underpassPosition, underpass4A.transform.rotation);
			underpass4B.transform.parent = terrainB.transform;
			underpass4C = (GameObject)Instantiate(underpass4A, terrainC.transform.position + underpassPosition, underpass4A.transform.rotation);
			underpass4C.transform.parent = terrainC.transform;
			underpass4D = (GameObject)Instantiate(underpass4A, terrainD.transform.position + underpassPosition, underpass4A.transform.rotation);
			underpass4D.transform.parent = terrainD.transform;

			underpassPosition = underpass5A.transform.position - terrainA.transform.position;
			underpass5B = (GameObject)Instantiate(underpass5A, terrainB.transform.position + underpassPosition, underpass5A.transform.rotation);
			underpass5B.transform.parent = terrainB.transform;
			underpass5C = (GameObject)Instantiate(underpass5A, terrainC.transform.position + underpassPosition, underpass5A.transform.rotation);
			underpass5C.transform.parent = terrainC.transform;
			underpass5D = (GameObject)Instantiate(underpass5A, terrainD.transform.position + underpassPosition, underpass5A.transform.rotation);
			underpass5D.transform.parent = terrainD.transform;

			underpassPosition = underpass6A.transform.position - terrainA.transform.position;
			underpass6B = (GameObject)Instantiate(underpass6A, terrainB.transform.position + underpassPosition, underpass6A.transform.rotation);
			underpass6B.transform.parent = terrainB.transform;
			underpass6C = (GameObject)Instantiate(underpass6A, terrainC.transform.position + underpassPosition, underpass6A.transform.rotation);
			underpass6C.transform.parent = terrainC.transform;
			underpass6D = (GameObject)Instantiate(underpass6A, terrainD.transform.position + underpassPosition, underpass6A.transform.rotation);
			underpass6D.transform.parent = terrainD.transform;

			underpassPosition = underpass7A.transform.position - terrainA.transform.position;
			underpass7B = (GameObject)Instantiate(underpass7A, terrainB.transform.position + underpassPosition, underpass7A.transform.rotation);
			underpass7B.transform.parent = terrainB.transform;
			underpass7C = (GameObject)Instantiate(underpass7A, terrainC.transform.position + underpassPosition, underpass7A.transform.rotation);
			underpass7C.transform.parent = terrainC.transform;
			underpass7D = (GameObject)Instantiate(underpass7A, terrainD.transform.position + underpassPosition, underpass7A.transform.rotation);
			underpass7D.transform.parent = terrainD.transform;
		}
*/
		//================================
		// Set Up Car Objects
		//================================
	
		trafficManager.InitLevel(currentLevel);
	}

	public int GetCurrentLevel()
	{
		return currentLevel;
	}
	
	//===================================
	//===================================
	//		PUBLICS & UTILS
	//===================================
	//===================================

	public void SetGameState(string newGameState)
	{
		gameState = newGameState;
		gameSubState = "gameSubStateNull";
		stateStartTime = Time.time;
		stateInitFlag = false;
	}

	public bool IsCaughtState()
	{
		return (gameState == "gameStateFeeding1") ? true : false;
	}

	public void SetSelectedPuma(int selection)
	{
		selectedPuma = selection;
        AssetManager.SetPuma(selectedPuma);
		
		pumaChasingSpeed = basePumaChasingSpeed + speedArray[selectedPuma];
		chaseTriggerDistance = baseChaseTriggerDistance + stealthArray[selectedPuma];
		
		if (carCollisionState == "Concluded") {
			carCollisionState = "None";
			pumaAnimator.SetBool("CarCollision", false);
		}
		
		if (starvationState == "Concluded") {
			starvationState = "None";
			pumaAnimator.SetBool("PumaStarved", false);
		}
	}

	void SelectCameraPosition(string targetPositionLabel, float targetRotOffsetY, float fadeTime, string mainCurve, string rotXCurve)
	{
		if (stateInitFlag == false) {
			cameraController.SelectTargetPosition(targetPositionLabel, targetRotOffsetY, fadeTime, mainCurve, rotXCurve);
			stateInitFlag = true;
		}
	}

	//===================================
	//===================================
	//		PERIODIC UPDATE
	//===================================
	//===================================

	void Update() 
	{	
		float fadeTime;
		float inputPercent = 1f;
		
		//pumaAnimator.SetLayerWeight(1, 1f);
	
		if (pumaObj == null || buck == null || doe == null || fawn == null)
			return;
			
		CalculateFrameRate();
			
		inputControls.ProcessControls(gameState);
		
		//=================================
		// Get distances from puma to deer
		//=================================

		float pumaDeerDistance1 = Vector3.Distance(pumaObj.transform.position, buck.gameObj.transform.position);
		float pumaDeerDistance2 = Vector3.Distance(pumaObj.transform.position, doe.gameObj.transform.position);
		float pumaDeerDistance3 = Vector3.Distance(pumaObj.transform.position, fawn.gameObj.transform.position);		
		
		//=================================
		// Check for Skip Ahead
		//=================================

		if (goStraightToFeeding == true && (gameState == "gameStateStalking" || gameState == "gameStateChasing")) {
			if (gameState == "gameStateStalking") {
				SetGameState("gameStateChasing");
				pumaAnimator.SetBool("Chasing", true);

				buckAnimator.SetBool("Looking", true);
				buckAnimator.SetBool("Running", true);
				buckAnimator.SetBool("Die", true);

				doeAnimator.SetBool("Looking", true);
				doeAnimator.SetBool("Running", true);
				doeAnimator.SetBool("Die", true);

				fawnAnimator.SetBool("Looking", true);
				fawnAnimator.SetBool("Running", true);
				fawnAnimator.SetBool("Die", true);
			}
			
			switch (lastAutoKilledDeerType) {
				case 0:
					pumaDeerDistance1 = 0;
					break;
				case 1:
					pumaDeerDistance2 = 0;
					break;
				case 2:
					pumaDeerDistance3 = 0;
					break;
			}
			
			lastAutoKilledDeerType++;
			if (lastAutoKilledDeerType >= 3)
				lastAutoKilledDeerType = 0;
		}
			
		//========================================
		// Check for Change to guiFlybyOverdrive
		//========================================

		if (guiFlybyOverdriveRampFlag == true) {
			float currentTime = Time.time;
			if (currentTime > guiFlybyOverdriveRampEndTime) {
				guiFlybyOverdrive = guiFlybyOverdriveRampEndVal;
				if (guiFlybyOverdrive == 1f) {
					// back to normal, we're done
					guiFlybyOverdriveRampFlag = false;
				}
				else {
					// we've reached the full level, now ramp it back down
					guiFlybyOverdriveRampStartVal = guiFlybyOverdrive;
					guiFlybyOverdriveRampEndVal = 1f;
					guiFlybyOverdriveRampStartTime = Time.time;
					guiFlybyOverdriveRampEndTime = Time.time + 4f;
				}
			}
			else {
				float totalRampTime = guiFlybyOverdriveRampEndTime - guiFlybyOverdriveRampStartTime;
				float totalRampAmount = guiFlybyOverdriveRampEndVal - guiFlybyOverdriveRampStartVal;
				float elapsedTime = currentTime - guiFlybyOverdriveRampStartTime;
				guiFlybyOverdrive = guiFlybyOverdriveRampStartVal + (totalRampAmount * (elapsedTime/totalRampTime));
			}
		}			

		//===========================
		// Update Game-State Logic
		//===========================

		switch (gameState) {
		
		//------------------------------
		// GUI States
		//
		// user interface states
		// main panel showing
		//------------------------------

		case "gameStateGui":
			// high in air, overlay panel showing
			guiFlybySpeed = 1f;
			SelectCameraPosition("cameraPosGui", -120f, 0f, null, null);
			break;
	
		case "gameStateLeavingGui":
			// zoom down into close up
			fadeTime = 2.5f;
			guiFlybySpeed = 1f - (Time.time - stateStartTime) / fadeTime;		
			if (stateInitFlag == false) {
				// init the level before zooming down
				PlaceDeerPositions();
				ResetAnimations();
                ResetPuma();
				// stateInitFlag set to TRUE in the next function
			}
			SelectCameraPosition("cameraPosCloseup", 1000000f, fadeTime, "mainCurveSForward", "curveRotXLogarithmicSecondHalf"); // 1000000 signifies no change for cameraRotOffsetY
			if (Time.time >= stateStartTime + fadeTime) {
				guiFlybySpeed = 0f;
				SetGameState("gameStateEnteringGameplay1");
			}
			break;
	
		//------------------------------
		// Gameplay States
		//
		// entering, leaving,
		// stalking and chasing
		//------------------------------

		case "gameStateEnteringGameplay1":
			// brief pause on close up
			fadeTime = 0.1f;
			if (Time.time >= stateStartTime + fadeTime) {
				SetGameState("gameStateEnteringGameplay2");
			}
			break;	
	
		case "gameStateEnteringGameplay2":
			// swing around to behind view
			fadeTime = 1.7f;
			SelectCameraPosition("cameraPosHigh", 0f, fadeTime, "mainCurveSBackward", "curveRotXLinearBackwardsSecondHalf"); 
			if (Time.time >= stateStartTime + fadeTime) {
				inputControls.ResetControls();		
				SetGameState("gameStateStalking");
			}
			break;	


		case "gameStateStalking":
			// Do a quick raycast to see if we need to respawn the vehicles.
			RaycastHit raycastHit1;
			if(Physics.Raycast(new Vector3(pumaX, pumaY + 1.0f, pumaZ), Vector3.down, out raycastHit1, 10.0f, pumaRoadCheckLayerMask) && pumaFeedingOnRoad)
			{
				if(raycastHit1.collider.tag == "Terrain") {
					pumaFeedingOnRoad = false;
					vehicleParentNode.SetActive(true);
				}
			}

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
			buck.forwardRate = 0f;
			buck.turnRate = 0f;
			doe.forwardRate = 0f;
			doe.turnRate = 0f;
			fawn.forwardRate = 0f;
			fawn.turnRate = 0f;
			break;
	
		case "gameStateChasing":
			// Do a quick raycast to see if we're should turn on th vehicles.
			RaycastHit raycastHit2;
			if(Physics.Raycast(new Vector3(pumaX, pumaY + 1.0f, pumaZ), Vector3.down, out raycastHit2, 10.0f, pumaRoadCheckLayerMask) && pumaFeedingOnRoad)
			{
				if(raycastHit2.collider.tag == "Terrain") {
					pumaFeedingOnRoad = false;
					vehicleParentNode.SetActive(true);
				}
			}

			// main chasing state - with a couple of quick initial camera moves handled via sub-states
			if (stateInitFlag == false) {
				gameSubState = "chasingSubState1";
				cameraController.SelectTargetPosition("cameraPosMedium", 1000000f, 0.75f, "mainCurveLinear", "curveRotXLinear");  // 1000000 signifies no change for cameraRotOffsetY
				stateInitFlag = true;
			}
			else if (gameSubState == "chasingSubState1" && (Time.time >= stateStartTime + 0.75f)) {
				gameSubState = "chasingSubState2";
				cameraController.SelectTargetPosition("cameraPosLow", 1000000f, 0.25f, "mainCurveLinear", "curveRotXLinear");  // 1000000 signifies no change for cameraRotOffsetY
			}

			// change deer running settings at random intervals
			if (Time.time > nextDeerRunUpdateTime) {
				nextDeerRunUpdateTime = Time.time + Random.Range(0.1f, 0.2f);
			
				buck.forwardRate = buckDefaultForwardRate * Random.Range(0.9f, 1.1f);
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
			}

			if ((pumaJumpFlag || goStraightToFeeding) && (pumaDeerDistance1 < caughtTriggerDistance || pumaDeerDistance2 < caughtTriggerDistance || pumaDeerDistance3 < caughtTriggerDistance)) {
			
				// DEER IS CAUGHT !!!
			
				if (pumaDeerDistance1 < caughtTriggerDistance) {
					buck.forwardRate = 0;
					buck.turnRate = 0f;
					buckAnimator.SetBool("Die", true);
					caughtDeer = buck;
					scoringSystem.DeerCaught(selectedPuma, "Buck");

				}
				else if (pumaDeerDistance2 < caughtTriggerDistance) {
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
					deerCaughtFinalHeading = mainHeading + 85f;
				}
				else {
					//deerCaughtFinalHeading = mainHeading - 90;  NOTE !! - need reverse anim to enable both final positions
					deerCaughtFinalHeading = mainHeading + 85f;
				}
				if (deerCaughtFinalHeading < 0)
					deerCaughtFinalHeading += 360;
				if (deerCaughtFinalHeading >= 360)
					deerCaughtFinalHeading -= 360;
				deerCaughtmainHeading = mainHeading;
				deerCaughtOffsetX = caughtDeer.gameObj.transform.position.x - pumaX;
				deerCaughtOffsetZ = caughtDeer.gameObj.transform.position.z - pumaZ;
				deerCaughtFinalOffsetX = (Mathf.Sin(mainHeading*Mathf.PI/180) * caughtDeer.deerCaughtFinalOffsetForward);
				deerCaughtFinalOffsetZ = (Mathf.Cos(mainHeading*Mathf.PI/180) * caughtDeer.deerCaughtFinalOffsetForward);
				deerCaughtFinalOffsetX += (Mathf.Sin((mainHeading-90f)*Mathf.PI/180) * caughtDeer.deerCaughtFinalOffsetSideways);
				deerCaughtFinalOffsetZ += (Mathf.Cos((mainHeading-90f)*Mathf.PI/180) * caughtDeer.deerCaughtFinalOffsetSideways);
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
                AddBloodSplatter(caughtDeer.gameObj);
                pumaController.Audio_SFX.PlaySound("Win");
			}
			else if (pumaDeerDistance1 > deerGotAwayDistance && pumaDeerDistance2 > deerGotAwayDistance && pumaDeerDistance3 > deerGotAwayDistance) {
				// DEER GOT AWAY !!	
				scoringSystem.PumaBadHunt(selectedPuma);                
                pumaController.Audio_SFX.PlaySound("Fail");
				guiManager.SetGuiState("guiStateFeeding1");
				SetGameState("gameStateFeeding1a");
			}
			break;

		case "gameStateLeavingGameplay":
			// zoom up to high in the air
			fadeTime = 2f;
			guiFlybySpeed = (Time.time - stateStartTime) / fadeTime;
			SelectCameraPosition("cameraPosGui", -120f, fadeTime, "mainCurveSBackward", "curveRotXLogarithmicBackwardsSecondHalf"); 
			if (Time.time >= stateStartTime + fadeTime) {
				guiFlybySpeed = 1f;
				ResetAnimations();
				SetGameState("gameStateGui");
			}
			break;	

		case "gameStateLeavingGameplayA":
			// same as above except called from feeding display
			fadeTime = 2f;
			guiFlybySpeed = (Time.time - stateStartTime) / fadeTime;
			SelectCameraPosition("cameraPosGui", -120f, fadeTime, "mainCurveSBackward", "curveRotXLogarithmicBackwardsSecondHalf"); 
			if (Time.time >= stateStartTime + fadeTime) {
				guiFlybySpeed = 1f;
				PlaceDeerPositions();
				ResetAnimations();
				scoringSystem.ClearLastKillInfo(selectedPuma);		
				caughtDeer = null;
				inputControls.ResetControls();		
				SetGameState("gameStateGui");
			}
			break;	

		//------------------------------
		// Feeding States
		//
		// puma has caught a deer
		// kills it and feeds on it
		//------------------------------

		case "gameStateFeeding1":
			// deer and puma slide to a stop as camera swings around to front
			fadeTime = 1.3f;
			SelectCameraPosition("cameraPosCloseup", -160f, fadeTime, "mainCurveSBackward", "curveRotXLogarithmic"); 
			
			if (Time.time < stateStartTime + fadeTime) {
				// puma and deer slide to a stop
				float percentDone = 1f - ((Time.time - stateStartTime) / fadeTime);
				float pumaMoveDistance = 1f * Time.deltaTime * pumaChasingSpeed * percentDone * 1.1f;
				pumaX += (Mathf.Sin(deerCaughtmainHeading*Mathf.PI/180) * pumaMoveDistance);
				pumaZ += (Mathf.Cos(deerCaughtmainHeading*Mathf.PI/180) * pumaMoveDistance);
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

				// Raycast downwards to determine if the puma is still on the road
				RaycastHit raycastHit;
				// The puma is still on the road, so we need to move the puma off the road before we do anything else
				if(Physics.Raycast(new Vector3(pumaX, pumaY + 0.5f, pumaZ), Vector3.down, out raycastHit, 10.0f, pumaRoadCheckLayerMask)) {
					if(raycastHit.collider.tag == "Road") {
						pumaFeedingOnRoad = true;
						// Despawn the cars
						if(currentLevel != 0) {
							vehicleParentNode.SetActive(false);
						}
					}
				}
			}
			else {
				float deerX = pumaX + deerCaughtFinalOffsetX;
				float deerY = caughtDeer.gameObj.transform.position.y;
				float deerZ = pumaZ + deerCaughtFinalOffsetZ;
				caughtDeer.gameObj.transform.rotation = Quaternion.Euler(0, deerCaughtFinalHeading, 0);
				caughtDeer.gameObj.transform.position = new Vector3(deerX, deerY, deerZ);
				SetGameState("gameStateFeeding2");
			}
			// calculate deerObj rotX based on terrain in front and behind
			float offsetDistance = 0.5f;
			float deerAheadX = caughtDeer.gameObj.transform.position.x + (Mathf.Sin(caughtDeer.heading*Mathf.PI/180) * offsetDistance * -1f);
			float deerAheadZ = caughtDeer.gameObj.transform.position.z + (Mathf.Cos(caughtDeer.heading*Mathf.PI/180) * offsetDistance * -1f);
			float deerBehindX = caughtDeer.gameObj.transform.position.x + (Mathf.Sin(caughtDeer.heading*Mathf.PI/180) * offsetDistance * 1f);
			float deerBehindZ = caughtDeer.gameObj.transform.position.z + (Mathf.Cos(caughtDeer.heading*Mathf.PI/180) * offsetDistance * 1f);
			float deerRotX = GetAngleFromOffset(0, GetTerrainHeight(deerAheadX, deerAheadZ), offsetDistance * 2f, GetTerrainHeight(deerBehindX, deerBehindZ)) - 90f;	
			float sidewaysHeading = caughtDeer.heading + 90f;
			float deerLeftX = caughtDeer.gameObj.transform.position.x + (Mathf.Sin(sidewaysHeading*Mathf.PI/180) * offsetDistance * 1f);
			float deerLeftZ = caughtDeer.gameObj.transform.position.z + (Mathf.Cos(sidewaysHeading*Mathf.PI/180) * offsetDistance * 1f);
			float deerRightX = caughtDeer.gameObj.transform.position.x + (Mathf.Sin(sidewaysHeading*Mathf.PI/180) * offsetDistance * -1f);
			float deerRightZ = caughtDeer.gameObj.transform.position.z + (Mathf.Cos(sidewaysHeading*Mathf.PI/180) * offsetDistance * -1f);
			float deerRotZ = GetAngleFromOffset(0, GetTerrainHeight(deerLeftX, deerLeftZ), offsetDistance * 2f, GetTerrainHeight(deerRightX, deerRightZ)) - 90f;	
			caughtDeer.gameObj.transform.rotation = Quaternion.Euler(deerRotX, caughtDeer.heading, deerRotZ);
			break;

		case "gameStateFeeding1a":
			// enter into feeding display after prey-less hunt
			fadeTime = 1.8f;
			SelectCameraPosition("cameraPosCloseup", -160f, fadeTime, "mainCurveSBackward", "curveRotXLogarithmic"); 
			scoringSystem.SetHuntSuccessCount(0);
			if (Time.time < stateStartTime + fadeTime) {
				inputPercent = 1f - ((Time.time - stateStartTime) / fadeTime);
			}
			else {
				inputPercent = 1f;
				SetGameState("gameStateFeeding2");
			}
			break;

		case "gameStateFeeding2":
			// brief pause
			fadeTime = (caughtDeer != null) ? 1.3f : 0.15f;
			if (Time.time >= stateStartTime + fadeTime) {
				SetGameState("gameStateFeeding3");
			}
			break;

		case "gameStateFeeding3":
			// camera slowly lifts as puma feeds on deer
			fadeTime = (caughtDeer != null) ? 5f : 3.5f;
			SelectCameraPosition("cameraPosEating", 1000000f, fadeTime, "mainCurveSBackward", "curveRotXLinear"); 
			if (Time.time >= stateStartTime + fadeTime) {
				SetGameState("gameStateFeeding4");
			}
			break;
			
		case "gameStateFeeding4":
			// camera spins slowly around puma as it feeds
			if (Time.time >= stateStartTime + 0.1f) {
				float spinningRotOffsetY = cameraController.GetCurrentRotOffsetY() - (Time.deltaTime + 0.03f);
				if (spinningRotOffsetY < -180f)
					spinningRotOffsetY += 360f;
				cameraController.SelectTargetPosition("cameraPosEating", spinningRotOffsetY, 0f, "mainCurveNull", "curveRotXNull"); 
			}
			break;
					
		case "gameStateFeeding5":
			// camera swings back into position for stalking
			fadeTime = 2.2f;
			pumaAnimator.SetBool("DeerKill", false);
			SelectCameraPosition("cameraPosHigh", 0f, fadeTime, "mainCurveSBackward", "curveRotXLogarithmic"); 
			if (Time.time >= stateStartTime + fadeTime) {
				PlaceDeerPositions();
				ResetAnimations();
				scoringSystem.ClearLastKillInfo(selectedPuma);		
				caughtDeer = null;
				inputControls.ResetControls();		
				SetGameState("gameStateFeeding6");
			}
			break;	
	
		case "gameStateFeeding6":
			// pause for puma to stand
			fadeTime = 0.65f;
			if (Time.time >= stateStartTime + fadeTime) {
				SetGameState("gameStateFeeding7");
				pumaAnimator.SetFloat("Distance", 1.0f);
				inputControls.SetInputVert((caughtDeer != null) ? 0.23f : 0.15f);
			}
			break;	
	
		case "gameStateFeeding7":
			// puma takes a few steps
			fadeTime = (caughtDeer != null) ? 3.0f : 3.0f;
			if (Time.time >= stateStartTime + fadeTime) {
				inputControls.SetInputVert(0f);
				SetGameState("gameStateStalking");
			}
			break;	
	
		//------------------------------
		// Tree Collision States
		//
		// puma has hit a tree
		//------------------------------

		case "gameStateTree1":
			 // to avoid triggering walking anim (needs to happen not too early, not too late)
			fadeTime = 0.2f;
			if (Time.time >= stateStartTime + fadeTime) {
				pumaAnimator.SetFloat("Distance", 0f);
				SetGameState("gameStateTree2");
			}
			break;

		case "gameStateTree2":
			// pause for puma to recover
			fadeTime = 1.8f;
			if (Time.time >= stateStartTime + fadeTime) {
				EndTreeCollision();
				SetGameState("gameStateTree3");
			}
			break;

		case "gameStateTree3":
			// pause for crossfade to idle anim
			fadeTime = 0.8f;
			if (Time.time >= stateStartTime + fadeTime) {
				SetGameState("gameStateStalking");
			}
			break;

		case "gameStateTree1a":
			 // to avoid triggering walking anim (needs to happen not too early, not too late)
			fadeTime = 0.2f;
			if (Time.time >= stateStartTime + fadeTime) {
				pumaAnimator.SetFloat("Distance", 0f);
				SetGameState("gameStateTree2a");
			}
			break;

		case "gameStateTree2a":
			// enter into feeding display after prey-less hunt
			fadeTime = 3.8f;
			SelectCameraPosition("cameraPosCloseup", -160f, fadeTime, "mainCurveSBackward", "curveRotXLogarithmic"); 
			scoringSystem.SetHuntSuccessCount(0);
			if (Time.time > stateStartTime + fadeTime) {
				EndTreeCollision();
				SetGameState("gameStateFeeding2");
			}
			break;

		//------------------------------
		// Died States
		//
		// puma has died
		// returns to overlay display
		//------------------------------

		case "gameStateDied1":
			// camera swings around to front of puma
			fadeTime = 3f;
			SelectCameraPosition("cameraPosCloseup", -160f, fadeTime, "mainCurveSBackward", "curveRotXLogarithmic"); 
			if (Time.time >= stateStartTime + fadeTime) {
				SetGameState("gameStateDied2");
			}
			break;

		case "gameStateDied2":
			// brief pause
			fadeTime = 0.2f;
			if (Time.time >= stateStartTime + fadeTime) {
				SetGameState("gameStateDied3");
			}
			break;

		case "gameStateDied3":
			// camera lifts slowly away from puma
			fadeTime = 5f;
			SelectCameraPosition("cameraPosEating", 1000000f, fadeTime, "mainCurveSBackward", "curveRotXLinear"); 
			if (Time.time >= stateStartTime + fadeTime) {
				SetGameState("gameStateDied4");
			}
			break;
			
		case "gameStateDied4":
			// camera spins slowly around puma
			if (Time.time >= stateStartTime + 0.1f) {
				float spinningRotOffsetY = cameraController.GetCurrentRotOffsetY() - (Time.deltaTime + 0.05f);
				if (spinningRotOffsetY < -180f)
					spinningRotOffsetY += 360f;
				cameraController.SelectTargetPosition("cameraPosEating", spinningRotOffsetY, 0f, "mainCurveNull", "curveRotXNull"); 
			}
			break;
					

		//------------------
		// Error Check
		//------------------
			
		default:
			Debug.Log("ERROR - LevelManager.Update() got bad state: " + gameState);
			break;
		}		

		//===============
		// Update Puma
		//===============
			
		float distance = 0f;
		pumaHeading = mainHeading;
		

		if (pumaHeadingOffset != pumaHeadingOffsetTargetVal) {
			if (pumaHeadingOffset < pumaHeadingOffsetTargetVal) {
				pumaHeadingOffset += pumaHeadingOffsetStepSize * Time.deltaTime;
				if (pumaHeadingOffset > pumaHeadingOffsetTargetVal)
					pumaHeadingOffset = pumaHeadingOffsetTargetVal;
			}
			else {
				pumaHeadingOffset += pumaHeadingOffsetStepSize * Time.deltaTime;
				if (pumaHeadingOffset < pumaHeadingOffsetTargetVal)
					pumaHeadingOffset = pumaHeadingOffsetTargetVal;
			}
		}

		
						
		if (pumaJumpGravityBack != 0f) {
			pumaJumpOffsetD += pumaJumpVelocityForward * Time.deltaTime;
			pumaJumpVelocityForward += pumaJumpGravityBack * Time.deltaTime;
			if (pumaJumpOffsetD < 0f) {
				pumaJumpOffsetD = 0f;
				pumaJumpVelocityForward = 0f;
				pumaJumpGravityBack = 0f;
				if (pumaJumpGravityDown == 0f) {
					pumaJumpFlag = false;
				}
			}
		}
		
		if (pumaJumpGravityDown != 0f) {
			pumaJumpOffsetY += pumaJumpVelocityUp * Time.deltaTime;
			pumaJumpVelocityUp += pumaJumpGravityDown * Time.deltaTime;
			if (pumaJumpOffsetY < 0f) {
				pumaJumpOffsetY = 0f;
				pumaJumpVelocityUp = 0f;
				pumaJumpGravityDown = 0f;
				if (pumaJumpGravityBack == 0f) {
					pumaJumpFlag = false;
				}
			}
		}
				
		
		// slowdown during proximity to deer
		float closestDeerDistance = pumaDeerDistance1;
		if (pumaDeerDistance2 < closestDeerDistance)
			closestDeerDistance = pumaDeerDistance2;
		if (pumaDeerDistance3 < closestDeerDistance)
			closestDeerDistance = pumaDeerDistance3;
		float deerProximityFactor = 1f;
		float deerProximityMinVal = 0.65f;
		float deerProximityMaxDist = chaseTriggerDistance * 2f;
		float deerProximityMinDist = chaseTriggerDistance * 1.1f;
		if (closestDeerDistance < deerProximityMaxDist) {
			if (closestDeerDistance > deerProximityMinDist)
				deerProximityFactor = ((closestDeerDistance-deerProximityMinDist) / (deerProximityMaxDist-deerProximityMinDist)) * (1f - deerProximityMinVal) + deerProximityMinVal;
			else
				deerProximityFactor = deerProximityMinVal;
		}
		float deerProximityFactorReversed = 1f - (deerProximityFactor - deerProximityMinVal);

		
		if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.LeftControl)) {
			// filter out the input when manual camera moves are in progress - DEV ONLY
		}		
		else if (gameState == "gameStateGui" || gameState == "gameStateLeavingGameplay" || gameState == "gameStateLeavingGui") {
			// process automatic puma walking during GUI state
			if ((gameState != "gameStateLeavingGui") || (Time.time - stateStartTime < 1.8f))
				pumaAnimator.SetBool("GuiMode", true);
			UpdateGuiStatePumaHeading();
			distance = guiFlybySpeed * Time.deltaTime  * 7f * guiFlybyOverdrive;			
			pumaX += (Mathf.Sin(mainHeading*Mathf.PI/180) * distance);
			pumaZ += (Mathf.Cos(mainHeading*Mathf.PI/180) * distance);
		}	
		else if (gameState == "gameStateStalking" || gameState == "gameStateFeeding7") {
			// main stalking state
			float rotationSpeed = 100f;
			if (pumaCollisionFlag == true) {
				// collision
				distance = inputControls.GetInputVert() * Time.deltaTime  * pumaStalkingSpeed * deerProximityFactor * speedOverdrive;
				mainHeading += inputControls.GetInputHorz() * Time.deltaTime * rotationSpeed;
				if (pumaCollisionHeadingOffset > 0f) {
					// turning right
					if (mainHeading < pumaCollisionBarrierHeading)
						mainHeading += Time.deltaTime * pumaCollisionHeadingOffset * rotationSpeed;
					pumaHeading = ((mainHeading < pumaCollisionBarrierHeading) ? pumaCollisionBarrierHeading : mainHeading) + pumaHeadingOffset*deerProximityFactorReversed;
					if (mainHeading < pumaCollisionBarrierHeading) {				
						float angleDelta = pumaCollisionBarrierHeading - mainHeading;
						distance = (Mathf.Cos(angleDelta*Mathf.PI/180) * distance);				
					}
				}
				else {
					// turning left
					if (mainHeading > pumaCollisionBarrierHeading)
						mainHeading += Time.deltaTime * pumaCollisionHeadingOffset * rotationSpeed;
					pumaHeading = ((mainHeading > pumaCollisionBarrierHeading) ? pumaCollisionBarrierHeading : mainHeading) + pumaHeadingOffset*deerProximityFactorReversed;
					if (mainHeading > pumaCollisionBarrierHeading) {				
						float angleDelta = mainHeading - pumaCollisionBarrierHeading;
						distance = (Mathf.Cos(angleDelta*Mathf.PI/180) * distance);				
					}
				}
			}
			else {
				// stalking
				distance = inputControls.GetInputVert() * Time.deltaTime  * pumaStalkingSpeed * deerProximityFactor * speedOverdrive;
				float inputHorz = inputControls.GetInputHorz();
				float pumaHeadingSpread = 50f;
				float inputHorzCutoff = 0.4f;
				if (inputHorz <= -inputHorzCutoff) {
					//pumaHeadingOffset = -pumaHeadingSpread;
					//inputHorz = (inputHorz + inputHorzCutoff) / (1f - inputHorzCutoff);
				}
				else if (inputHorz <= inputHorzCutoff) {
					//pumaHeadingOffset = pumaHeadingSpread * inputHorz / inputHorzCutoff;
					//inputHorz = 0f;
				}
				else {
					//pumaHeadingOffset = pumaHeadingSpread;
					//inputHorz = (inputHorz + inputHorzCutoff) / (1f - inputHorzCutoff);
				}
				mainHeading += inputHorz * Time.deltaTime * rotationSpeed * (treeCollisionState == "None" ? 1f : 0f);
				pumaHeading = mainHeading + pumaHeadingOffset*deerProximityFactorReversed;
			}
			float travelledDistance = (scoringSystem.GetPumaHealth(selectedPuma) > 0.025f) ? distance : distance * (scoringSystem.GetPumaHealth(selectedPuma) / 0.025f);
			pumaX += (Mathf.Sin(pumaHeading*Mathf.PI/180) * travelledDistance);
			pumaZ += (Mathf.Cos(pumaHeading*Mathf.PI/180) * travelledDistance);
			pumaHeading = mainHeading + pumaHeadingOffset*deerProximityFactorReversed; // restore normal setting in case of collision mode
			scoringSystem.PumaHasWalked(selectedPuma, distance * travelledDistanceOverdrive);
            //scoringSystem.PumaHasWalked(selectedPuma, distance * ((distance != travelledDistance) ? 1f : travelledDistanceOverdrive));


            if (scoringSystem.GetPumaHealth(selectedPuma) == 0f) {
                // STARVATION !!
                pumaObj.GetComponent<PumaController>().SetCarCollisionCollider(); // Reduce collider height for puma to fall to ground.
                SetGameState("gameStateDied1");	
				guiManager.SetGuiState("guiStatePumaDone1");
				pumaAnimator.SetBool("PumaStarved", true);
				starvationState = "InProgress";
				pumaPhysicsInProgressTime = Time.time;
				pumaPhysicsPreviousY = pumaY;
                pumaController.Audio_SFX.PlaySound("Fail");                
            }
		}
		else if (gameState == "gameStateChasing" || gameState == "gameStateFeeding1a") {
			// main chasing state
			float rotationSpeed = 150f;
			if (pumaCollisionFlag == true) {
				// collision
				distance = inputControls.GetInputVert() * Time.deltaTime  * pumaChasingSpeed * speedOverdrive * difficultyLevel * difficultyLevel * difficultyLevel * inputPercent;
				mainHeading += inputControls.GetInputHorz() * Time.deltaTime * rotationSpeed;
				if (pumaCollisionHeadingOffset > 0f) {
					// turning right
					if (mainHeading < pumaCollisionBarrierHeading)
						mainHeading += Time.deltaTime * pumaCollisionHeadingOffset * rotationSpeed;
					pumaHeading = ((mainHeading < pumaCollisionBarrierHeading) ? pumaCollisionBarrierHeading : mainHeading) + pumaHeadingOffset;
					if (mainHeading < pumaCollisionBarrierHeading) {				
						float angleDelta = pumaCollisionBarrierHeading - mainHeading;
						distance = (Mathf.Cos(angleDelta*Mathf.PI/180) * distance);				
					}
				}
				else {
					// turning left
					if (mainHeading > pumaCollisionBarrierHeading)
						mainHeading += Time.deltaTime * pumaCollisionHeadingOffset * rotationSpeed;
					pumaHeading = ((mainHeading > pumaCollisionBarrierHeading) ? pumaCollisionBarrierHeading : mainHeading) + pumaHeadingOffset;
					if (mainHeading > pumaCollisionBarrierHeading) {				
						float angleDelta = mainHeading - pumaCollisionBarrierHeading;
						distance = (Mathf.Cos(angleDelta*Mathf.PI/180) * distance);				
					}
				}
			}
			else {
				// chasing
				distance = inputControls.GetInputVert() * Time.deltaTime  * pumaChasingSpeed * speedOverdrive * difficultyLevel * difficultyLevel * difficultyLevel * inputPercent;
				mainHeading += inputControls.GetInputHorz() * Time.deltaTime * rotationSpeed* (treeCollisionState == "None" ? 1f : 0f);
				pumaHeading = mainHeading + pumaHeadingOffset;
			}
			distance += pumaJumpOffsetD * Time.deltaTime;
			
			float travelledDistance = (scoringSystem.GetPumaHealth(selectedPuma) > 0.05f) ? distance : distance * (scoringSystem.GetPumaHealth(selectedPuma) / 0.05f);
			pumaX += (Mathf.Sin(pumaHeading*Mathf.PI/180) * travelledDistance);
			pumaZ += (Mathf.Cos(pumaHeading*Mathf.PI/180) * travelledDistance);
			pumaHeading = mainHeading + pumaHeadingOffset; // restore normal setting in case of collision mode
			scoringSystem.PumaHasRun(selectedPuma, distance * travelledDistanceOverdrive);
            // scoringSystem.PumaHasRun(selectedPuma, distance * ((distance != travelledDistance) ? 1f : travelledDistanceOverdrive));
            
            if (scoringSystem.GetPumaHealth(selectedPuma) < 0.05f)
            {
                Debug.Log("Puma health " + scoringSystem.GetPumaHealth(selectedPuma)  + ". Switching to walk animation");
                pumaAnimator.SetBool("Chasing", false);
            }

            if (scoringSystem.GetPumaHealth(selectedPuma) == 0f) {
                // STARVATION !!
                pumaObj.GetComponent<PumaController>().SetCarCollisionCollider(); // Reduce collider height for puma to fall to ground.
                SetGameState("gameStateDied1");	
				guiManager.SetGuiState("guiStatePumaDone1");
				pumaAnimator.SetBool("PumaStarved", true);
				starvationState = "InProgress";
				pumaPhysicsInProgressTime = Time.time;
				pumaPhysicsPreviousY = pumaY;
				scoringSystem.PumaHasDied(selectedPuma, false);
                pumaController.Audio_SFX.PlaySound("Fail");
            }
		}
		
		while (mainHeading >= 360f)
			mainHeading -= 360f;
		while (mainHeading < 0f)
			mainHeading += 360f;	
		
		pumaAnimator.SetBool("GuiMode", false);
		if (treeCollisionState == "None") {
			// distance=0 switches to idle, but only if no tree collision
			pumaAnimator.SetFloat("Distance", distance);
		}

	
		// get the y pos of puma based on terrain and/or overpass height
		float pumaRotX;

		if (pumaController.CheckCollisionOverpassInProgress() == true) {
			// overpass
			pumaY = GetTerrainHeight(pumaX, pumaZ, pumaController.GetCollisionOverpassSurfaceHeight());
			pumaRotX = 0f;
		}
		else
        {
			// normal case
			pumaY = GetTerrainHeight(pumaX, pumaZ);
			// calculate puma rotX based on terrain in front and behind
			float offsetDistance = 0.5f;
			float pumaAheadX = pumaX + (Mathf.Sin(pumaHeading*Mathf.PI/180) * offsetDistance * 1f);
			float pumaAheadZ = pumaZ + (Mathf.Cos(pumaHeading*Mathf.PI/180) * offsetDistance * 1f);
			float pumaBehindX = pumaX + (Mathf.Sin(pumaHeading*Mathf.PI/180) * offsetDistance * -1f);
			float pumaBehindZ = pumaZ + (Mathf.Cos(pumaHeading*Mathf.PI/180) * offsetDistance * -1f);
			pumaRotX = GetAngleFromOffset(0, GetTerrainHeight(pumaAheadX, pumaAheadZ), offsetDistance * 2f, GetTerrainHeight(pumaBehindX, pumaBehindZ)) - 90f;
		}


		// write out the position and rotation of puma obj
		
		if (carCollisionState == "None" && treeCollisionState == "None" && starvationState == "None") {
			// normal case: update pumaObj every frame
						
			//Debug.Log(" ");
			//Debug.Log("====================PUMA was at   " + pumaObj.transform.position);

			pumaObj.transform.position = new Vector3(pumaX, pumaY + pumaJumpOffsetY, pumaZ);			
			pumaObj.transform.rotation = Quaternion.Euler(pumaRotX, (pumaHeading - 180f), 0);
						
			//Debug.Log("====================PUMA set to   " + pumaObj.transform.position);
			//Debug.Log(" ");
		
	
			/*
			private Vector3 moveDirection = Vector3.zero;
			public float jumpSpeed = 8.0F;
			public float gravity = 20.0F;
			private Vector3 moveDirection = Vector3.zero;
			void Update() {
				CharacterController controller = GetComponent<CharacterController>();
				if (controller.isGrounded) {
					moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
					moveDirection = transform.TransformDirection(moveDirection);
					moveDirection *= speed;
					if (Input.GetButton("Jump"))
						moveDirection.y = jumpSpeed;
					
				}
				moveDirection.y -= gravity * Time.deltaTime;
				controller.Move(moveDirection * Time.deltaTime);
			} */		
		}
		else if (carCollisionState == "InProgress" || treeCollisionState == "InProgress") {
			// physics takes over
			pumaX = pumaObj.transform.position.x;
			pumaY = pumaObj.transform.position.y;
			pumaZ = pumaObj.transform.position.z;

			if (carCollisionState == "InProgress") {
				// adjust position of puma within box collider based on which side is down
				bool flippedFlag = (Mathf.Abs(pumaObj.transform.rotation.x) + Mathf.Abs(pumaObj.transform.rotation.z) > 1f) ? true : false;	
				pumaObjCollider.center = new Vector3(pumaObjCollider.center.x, flippedFlag ? 0.14f : -0.04f, pumaObjCollider.center.z);
				if (pumaObj.transform.position.y < GetTerrainHeight(pumaObj.transform.position.x, pumaObj.transform.position.z)) {
					// prevent falling through terrain due to box collider adjustment
					pumaObj.transform.position = new Vector3(pumaObj.transform.position.x, GetTerrainHeight(pumaObj.transform.position.x, pumaObj.transform.position.z), pumaObj.transform.position.z);
				}
			}
			
			if (treeCollisionState == "InProgress") {
					
				//float officialPumaRotation = Quaternion.Euler(pumaRotX, (pumaHeading - 180f), 0);			
			
				mainHeading = pumaObj.transform.rotation.eulerAngles.y + 180f;
			}

			/* enable manual positioning post-collision */ {
				//float forceFactor = pumaObj.GetComponent<PumaController>().forceFactor;
				//pumaObj.rigidbody.AddForce(0, 0, forceFactor * Input.GetAxis("Vertical"));
				//pumaObj.rigidbody.AddForce(forceFactor * Input.GetAxis("Horizontal"), 0, 0);
			}
		}
		else if (starvationState == "InProgress") {
			// physics takes over
			pumaX = pumaObj.transform.position.x;
			pumaY = pumaObj.transform.position.y;
			pumaZ = pumaObj.transform.position.z;
			pumaObjCollider.center = new Vector3(pumaObjCollider.center.x, -0.04f, pumaObjCollider.center.z);

			// to smooth over transition from virtual puma to physics
			// need to ramp down the difference in the Y position
			float elapsedTime = Time.time - pumaPhysicsInProgressTime;
			float transTime = 1.5f;
			if (elapsedTime < transTime) {
				pumaY = pumaPhysicsPreviousY + ((pumaY - pumaPhysicsPreviousY) * elapsedTime/transTime);
			}
		}
		else if (carCollisionState == "Concluded" || starvationState == "Concluded") {
			// zoom up of camera - allow movement of virtual puma but leaves puma obj stationary
			// -- in order to smooth over transition from physics to virtual puma postion
			//       need to ramp down the difference in the Y position
			float elapsedTime = Time.time - pumaPhysicsConcludedTime;
			float transTime = 1.5f;
			if (elapsedTime < transTime) {
				pumaY += pumaPhysicsOffsetY * (1f - elapsedTime/transTime);
			}
		}	
		else {
			Debug.Log("ERROR: levelManager - got bad collision or starvation state");
		}	
		
		//displayVar1 = pumaObj.transform.position.x;
		//displayVar2 = pumaObj.transform.position.y;
		//displayVar3 = pumaObj.transform.position.z;
		
		//================
		// Update Camera
		//================
			
		cameraController.UpdateCameraPosition(pumaX, pumaY, pumaZ, mainHeading);
			
		//================
		// Update Deer
		//================

		UpdateDeerHeading(buck);
		UpdateDeerHeading(doe);
		UpdateDeerHeading(fawn);
		UpdateDeerPosition(buck);
		UpdateDeerPosition(doe);
		UpdateDeerPosition(fawn);
		
		//================================
		// Leap-Frog the Terrains
		//================================

		float terrainX;
		float terrainZ;
		bool terrainShiftedFlag = false;
		
		// TERRAIN A
		terrainX = terrainA.transform.position.x;
		terrainZ = terrainA.transform.position.z;
		while (pumaX - terrainX > 1500) {  terrainX += terrainShiftDistance; terrainShiftedFlag = true; }
		while (terrainX - pumaX >  500) {  terrainX -= terrainShiftDistance; terrainShiftedFlag = true; }
		while (pumaZ - terrainZ > 1500) {  terrainZ += terrainShiftDistance; terrainShiftedFlag = true; }
		while (terrainZ - pumaZ >  500) {  terrainZ -= terrainShiftDistance; terrainShiftedFlag = true; }
		terrainA.transform.position = new Vector3 (terrainX, 0, terrainZ);

		// TERRAIN B
		terrainX = terrainB.transform.position.x;
		terrainZ = terrainB.transform.position.z;
		while (pumaX - terrainX > 1500) {  terrainX += terrainShiftDistance; terrainShiftedFlag = true; }
		while (terrainX - pumaX >  500) {  terrainX -= terrainShiftDistance; terrainShiftedFlag = true; }
		while (pumaZ - terrainZ > 1500) {  terrainZ += terrainShiftDistance; terrainShiftedFlag = true; }
		while (terrainZ - pumaZ >  500) {  terrainZ -= terrainShiftDistance; terrainShiftedFlag = true; }
		terrainB.transform.position = new Vector3 (terrainX, 0, terrainZ);

		// TERRAIN C
		terrainX = terrainC.transform.position.x;
		terrainZ = terrainC.transform.position.z;
		while (pumaX - terrainX > 1500) {  terrainX += terrainShiftDistance; terrainShiftedFlag = true; }
		while (terrainX - pumaX >  500) {  terrainX -= terrainShiftDistance; terrainShiftedFlag = true; }
		while (pumaZ - terrainZ > 1500) {  terrainZ += terrainShiftDistance; terrainShiftedFlag = true; }
		while (terrainZ - pumaZ >  500) {  terrainZ -= terrainShiftDistance; terrainShiftedFlag = true; }
		terrainC.transform.position = new Vector3 (terrainX, 0, terrainZ);

		// TERRAIN D
		terrainX = terrainD.transform.position.x;
		terrainZ = terrainD.transform.position.z;
		while (pumaX - terrainX > 1500) {  terrainX += terrainShiftDistance; terrainShiftedFlag = true; }
		while (terrainX - pumaX >  500) {  terrainX -= terrainShiftDistance; terrainShiftedFlag = true; }
		while (pumaZ - terrainZ > 1500) {  terrainZ += terrainShiftDistance; terrainShiftedFlag = true; }
		while (terrainZ - pumaZ >  500) {  terrainZ -= terrainShiftDistance; terrainShiftedFlag = true; }
		terrainD.transform.position = new Vector3 (terrainX, 0, terrainZ);
		
		if (terrainShiftedFlag == true) {

			SetTerrainNeighbors();
			
			// make sure tree colliders get updated...
		
			terrainA.GetComponent<TerrainCollider>().enabled = false;
			terrainA.GetComponent<TerrainCollider>().enabled = true;

			terrainB.GetComponent<TerrainCollider>().enabled = false;
			terrainB.GetComponent<TerrainCollider>().enabled = true;

			terrainC.GetComponent<TerrainCollider>().enabled = false;
			terrainC.GetComponent<TerrainCollider>().enabled = true;

			terrainD.GetComponent<TerrainCollider>().enabled = false;
			terrainD.GetComponent<TerrainCollider>().enabled = true;	
		}
	}

		
	//===================================
	//===================================
	//		COLLISION HANDLING
	//===================================
	//===================================
	
	public void BeginCarCollision()
	{
		if (CheckCarCollision() == true || selectedPuma == -1)
			return;
	
		carCollisionState = "InProgress";
		SetGameState("gameStateDied1");
		guiManager.SetGuiState("guiStatePumaDone1");
		pumaAnimator.SetBool("CarCollision", true);
		scoringSystem.PumaHasDied(selectedPuma, true);
        AddBloodSplatter(pumaObj);
	}

	public bool CheckCarCollision()
	{	
		return (carCollisionState != "None");
	}

	public void EndCarCollision()
	{	
		carCollisionState = "Concluded";
		pumaObj.GetComponent<PumaController>().SetNormalCollider();  // happens here, or later ?  

		
		pumaPhysicsOffsetY = pumaY - GetTerrainHeight(pumaX, pumaZ);
		pumaPhysicsConcludedTime = Time.time;

		guiFlybyOverdriveRampFlag = true;
		guiFlybyOverdriveRampStartVal = guiFlybyOverdrive;
		guiFlybyOverdriveRampEndVal = 2f;
		guiFlybyOverdriveRampStartTime = Time.time;
		guiFlybyOverdriveRampEndTime = Time.time + 2f;
		
		UpdateGuiStatePumaHeading(true); // point puma away from road
	}

	////////////////////////////////
	////////////////////////////////

	public void BeginTreeCollision()
	{
		if (CheckTreeCollision() == true || selectedPuma == -1)
			return;

		treeCollisionState = "InProgress";
		pumaAnimator.SetBool("TreeCollision", true);

		if (gameState == "gameStateStalking") {
			SetGameState("gameStateTree1");
		}
		else if (gameState == "gameStateChasing") {
			scoringSystem.PumaBadHunt(selectedPuma);
			SetGameState("gameStateTree1a");
			guiManager.SetGuiState("guiStateFeeding1");
		}
	}

	public bool CheckTreeCollision()
	{	
		return (treeCollisionState != "None");
	}

	public void EndTreeCollision()
	{	
		treeCollisionState = "None";
		pumaObj.GetComponent<PumaController>().SetNormalCollider();
		pumaAnimator.SetBool("TreeCollision", false);


		//pumaPhysicsOffsetY = pumaY - GetTerrainHeight(pumaX, pumaZ);
		//pumaPhysicsConcludedTime = Time.time;

		//guiFlybyOverdriveRampFlag = true;
		//guiFlybyOverdriveRampStartVal = guiFlybyOverdrive;
		//guiFlybyOverdriveRampEndVal = 2f;
		//guiFlybyOverdriveRampStartTime = Time.time;
		//guiFlybyOverdriveRampEndTime = Time.time + 2f;
		
		//UpdateGuiStatePumaHeading(true); // point puma away from road
	}

	////////////////////////////////
	////////////////////////////////

	public bool CheckStarvation()
	{	
		return (starvationState != "None");
	}

	public void EndStarvation()
	{	
		starvationState = "Concluded";
		pumaPhysicsOffsetY = pumaY - GetTerrainHeight(pumaX, pumaZ);
		pumaPhysicsConcludedTime = Time.time;

		guiFlybyOverdriveRampFlag = true;
		guiFlybyOverdriveRampStartVal = guiFlybyOverdrive;
		guiFlybyOverdriveRampEndVal = 2f;
		guiFlybyOverdriveRampStartTime = Time.time;
		guiFlybyOverdriveRampEndTime = Time.time + 2f;
        pumaObj.GetComponent<PumaController>().SetNormalCollider();
    }


	//===================================
	//===================================
	//		PUMA HANDLING
	//===================================
	//===================================
	
	public void PumaJump()
	{
		if (pumaJumpFlag == true)
			return;
	
		pumaJumpFlag = true;
		pumaJumpVelocityUp = 3f;
		pumaJumpGravityDown = -10f;
		pumaJumpOffsetY = 0f;
		pumaJumpVelocityForward = 17.5f;
		pumaJumpGravityBack = -35f;
		pumaJumpOffsetD = 0f;
		pumaAnimator.SetTrigger("PumaPounce");
	}

	public void SetPumaSideStalk(bool stalkFlag)
	{	
		if (pumaSideStalkFlag == stalkFlag)
			return;
			
		// handle state change
			
		pumaSideStalkFlag = stalkFlag;
		
		float panSpeed = inputControls.GetInputVert() > 0f ? 100f : 300f;
		
		if (pumaSideStalkFlag == false) {
			pumaHeadingOffsetStartVal = pumaHeadingOffset;
			pumaHeadingOffsetTargetVal = 0f;
			pumaHeadingOffsetStepSize = pumaHeadingOffsetStartVal < 0f ? panSpeed : -panSpeed;
		}	
		else if (pumaHeadingOffset == 0f) {
			// set puma angle - do this only when coming from 0 position
			float averageDeerX = (buck.gameObj.transform.position.x + doe.gameObj.transform.position.x + fawn.gameObj.transform.position.x) / 3;
			float averageDeerZ = (buck.gameObj.transform.position.z + doe.gameObj.transform.position.z + fawn.gameObj.transform.position.z) / 3;
			// angle based on midpoint between camera and puma
			float refX = (pumaObj.transform.position.x + Camera.main.transform.position.x) / 2;
			float refZ = (pumaObj.transform.position.z + Camera.main.transform.position.z) / 2;
			float directionToDeer = guiUtils.GetAngleFromOffset(refX, refZ, averageDeerX, averageDeerZ);
			// set puma angle
			pumaHeadingOffsetStartVal = pumaHeadingOffset;
			if (mainHeading > directionToDeer)
				pumaHeadingOffsetTargetVal = (mainHeading - directionToDeer < 180f) ? 60f : -60f;
			else
				pumaHeadingOffsetTargetVal = (directionToDeer - mainHeading < 180f) ? -60f : 60f;
			pumaHeadingOffsetStepSize = pumaHeadingOffsetTargetVal < 0f ? -panSpeed : panSpeed;
		}
	}
	
	public bool GetPumaSideStalk()
	{
		return pumaSideStalkFlag;
	}

	
	public bool PumaSideStalkDirectionIsLeft()
	{
		if (buck == null || doe == null || fawn == null)
			return false;
	
		// code copied from previous function
		float averageDeerX = (buck.gameObj.transform.position.x + doe.gameObj.transform.position.x + fawn.gameObj.transform.position.x) / 3;
		float averageDeerZ = (buck.gameObj.transform.position.z + doe.gameObj.transform.position.z + fawn.gameObj.transform.position.z) / 3;
		float refX = (pumaObj.transform.position.x + Camera.main.transform.position.x) / 2;
		float refZ = (pumaObj.transform.position.z + Camera.main.transform.position.z) / 2;
		float directionToDeer = guiUtils.GetAngleFromOffset(refX, refZ, averageDeerX, averageDeerZ);
		if (mainHeading > directionToDeer)
			return (mainHeading - directionToDeer < 180f) ? false : true;
		else
			return (directionToDeer - mainHeading < 180f) ? true : false;
	}

	
	public void PumaBeginCollision(float headingOffset, float barrierHeading)
	{
		if (headingOffset < 0f && barrierHeading > mainHeading)
			barrierHeading -= 360f;
		if (headingOffset > 0f && barrierHeading < mainHeading)
			barrierHeading += 360f;
		pumaCollisionHeadingOffset = headingOffset;
		pumaCollisionBarrierHeading = barrierHeading;
		pumaCollisionFlag = true;
	}
	
	public void PumaEndCollision()
	{
		pumaCollisionFlag = false;
	}
		
	private void UpdateGuiStatePumaHeading(bool forceMaximumAvoidance = false)
	{
		// random component of heading change
		if (Time.time > nextFlybyHeadingUpdateTime) {
			flybyHeadingRotationSpeed += Random.Range(-1f, 1f);
			if (flybyHeadingRotationSpeed > 5f)
				flybyHeadingRotationSpeed = 5f;
			if (flybyHeadingRotationSpeed < -5f)
				flybyHeadingRotationSpeed = -5f;
			nextFlybyHeadingUpdateTime = Time.time + 1f;
		}
		float randomHeadingOffset = Time.deltaTime * flybyHeadingRotationSpeed;		
		
		// determine if we need to worry about avoiding roads
		float avoidanceMax = 40f;
		float avoidanceMin = 20f;
		Vector3 nearestRoadPos = trafficManager.FindClosestNode(new Vector3(pumaX, 0, pumaZ));
		float nearestRoadDistance = Vector3.Distance(nearestRoadPos, new Vector3(pumaX, 0, pumaZ));
		if (currentLevel == 0 || nearestRoadDistance > avoidanceMax) {
			mainHeading += randomHeadingOffset;
			return;
		}
				
		// yes, determine road avoidance component of heading change	
		float avoidanceRotation;
		float avoidanceAngle = GetAngleFromOffset(nearestRoadPos.x, nearestRoadPos.z, pumaX, pumaZ);	
		float avoidanceStrength = (nearestRoadDistance < avoidanceMin) ? 1f : (avoidanceMax-nearestRoadDistance) / (avoidanceMax-avoidanceMin);
		avoidanceStrength = 1f - ((1f - avoidanceStrength) * (1f - avoidanceStrength));
		if (avoidanceAngle > mainHeading)
			avoidanceRotation = (avoidanceAngle - mainHeading < 180f) ? (avoidanceAngle - mainHeading) : (((mainHeading + 360f) - avoidanceAngle) * -1f);
		else
			avoidanceRotation = (mainHeading - avoidanceAngle < 180f) ? ((mainHeading - avoidanceAngle) * -1f) : ((avoidanceAngle + 360f) - mainHeading);

		if (forceMaximumAvoidance == true) {
			// shortcut for case of starting up after road kill - point away from road
			cameraController.SetCurrentRotOffsetY(cameraController.GetCurrentRotOffsetY() - avoidanceRotation); 
			mainHeading = avoidanceAngle;
		}
		else {
			// apply gradual heading change
			mainHeading += Time.deltaTime * avoidanceRotation * avoidanceStrength * 0.3f;			
			mainHeading += randomHeadingOffset * (1f - avoidanceStrength);	
		}
	}

	//===================================
	//===================================
	//		DEER HANDLING
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

            // Check for roads and trees in the Target Heading direction

            if (deerAvoidsRoad)
            {              
                
                if (deer.collisionChecker != null) // If there is a collisionChecker attached to the deer
                {
                    if (deer.collisionChecker.roadDetected) // If a road is detected
                    {
                        Debug.Log(deer.gameObj.name + " avoiding Road");
                        if (deer.collisionChecker.roadDistanceLeft > deer.collisionChecker.roadDistanceRight) // If there is a road on the left side, turn right
                        {
                            deer.targetHeading += 90.0f;
                        }
                        else // If road on right side, turn left
                        {
                            deer.targetHeading -= 90.0f;
                        }
                        deer.nextTurnTime = Time.time;
                    }
                }
            }
        }

        // slew the change in heading

        float slewRate = 100f * Time.deltaTime;

		if(deer.collisionChecker.roadDetected) {
			deer.heading = deer.targetHeading;
		}

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

		float forwardRate = deer.forwardRate * difficultyLevel * difficultyLevel * difficultyLevel * difficultyLevel;
		
		if (newChaseFlag) 
			forwardRate = deer.forwardRate * ((Time.time - stateStartTime) / 0.3f);
		
		float deerX = deer.gameObj.transform.position.x + (Mathf.Sin(deer.heading*Mathf.PI/180) * Time.deltaTime  * forwardRate);
		float deerZ = deer.gameObj.transform.position.z + (Mathf.Cos(deer.heading*Mathf.PI/180) * Time.deltaTime  * forwardRate);
		float deerY = deer.baseY + GetTerrainHeight(deerX, deerZ);

		deer.gameObj.transform.position = new Vector3(deerX, deerY, deerZ);
		
		// calculate deerObj rotX based on terrain in front and behind
		float offsetDistance = 0.5f;
		float deerAheadX = deer.gameObj.transform.position.x + (Mathf.Sin(deer.heading*Mathf.PI/180) * offsetDistance * -1f);
		float deerAheadZ = deer.gameObj.transform.position.z + (Mathf.Cos(deer.heading*Mathf.PI/180) * offsetDistance * -1f);
		float deerBehindX = deer.gameObj.transform.position.x + (Mathf.Sin(deer.heading*Mathf.PI/180) * offsetDistance * 1f);
		float deerBehindZ = deer.gameObj.transform.position.z + (Mathf.Cos(deer.heading*Mathf.PI/180) * offsetDistance * 1f);
		float deerRotX = GetAngleFromOffset(0, GetTerrainHeight(deerAheadX, deerAheadZ), offsetDistance * 2f, GetTerrainHeight(deerBehindX, deerBehindZ)) - 90f;	
		float sidewaysHeading = deer.heading + 90f;
		float deerLeftX = deer.gameObj.transform.position.x + (Mathf.Sin(sidewaysHeading*Mathf.PI/180) * offsetDistance * 1f);
		float deerLeftZ = deer.gameObj.transform.position.z + (Mathf.Cos(sidewaysHeading*Mathf.PI/180) * offsetDistance * 1f);
		float deerRightX = deer.gameObj.transform.position.x + (Mathf.Sin(sidewaysHeading*Mathf.PI/180) * offsetDistance * -1f);
		float deerRightZ = deer.gameObj.transform.position.z + (Mathf.Cos(sidewaysHeading*Mathf.PI/180) * offsetDistance * -1f);
		float deerRotZ = GetAngleFromOffset(0, GetTerrainHeight(deerLeftX, deerLeftZ), offsetDistance * 2f, GetTerrainHeight(deerRightX, deerRightZ)) - 90f;	
		deer.gameObj.transform.rotation = Quaternion.Euler(deerRotX, deer.heading, deerRotZ);
	}

	void PlaceDeerPositions()
	{
		float newX;
		float newZ;
		float deerX;
		float deerZ;
		float deerY;
		float positionVariance = 20f;
		
		// determine center position for deer
		if (currentLevel == 0) {
			// random direction from puma pos
			float randomDirection = Random.Range(0f,360f);	
			float deerDistance = Random.Range(70f,100f);
			newX = pumaX + (Mathf.Sin(randomDirection*Mathf.PI/180) * deerDistance);
			newZ = pumaZ + (Mathf.Cos(randomDirection*Mathf.PI/180) * deerDistance);
		}
		else
        {
			// based on nearest road
			Vector3 closestRoadNode = trafficManager.FindClosestNode(new Vector3(pumaX, pumaY, pumaZ));
			newX = pumaX + (closestRoadNode.x - pumaX) + Random.Range(-positionVariance, positionVariance); 
			newZ = pumaZ + (closestRoadNode.z - pumaZ) + Random.Range(-positionVariance, positionVariance); 
		}


		if (caughtDeer != null) {
		
			// hide current deer set (except caught deer)
			if (caughtDeer != buck)
				buck.gameObj.transform.position = new Vector3(-2f, 0f, 0f);
			if (caughtDeer != doe)
				doe.gameObj.transform.position = new Vector3(0f, 0f, 0f);
			if (caughtDeer != fawn)
				fawn.gameObj.transform.position = new Vector3(2f, 0f, 0f);

			// swap the deer set so that caught deer can stay in place
			if (deerSet1Selected == true) {
				buck = buck2;
				doe = doe2;
				fawn = fawn2;
				buckAnimator = buck2Animator;
				doeAnimator = doe2Animator;
				fawnAnimator = fawn2Animator;
				deerSet1Selected = false;
			}
			else {
				buck = buck1;
				doe = doe1;
				fawn = fawn1;
				buckAnimator = buck1Animator;
				doeAnimator = doe1Animator;
				fawnAnimator = fawn1Animator;
				deerSet1Selected = true;
			}
		}
	
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

		buck.heading = buck.targetHeading = Random.Range(0f,360f);
		doe.heading = doe.targetHeading = Random.Range(0f,360f);		
		fawn.heading = fawn.targetHeading = Random.Range(0f,360f);	

		buck.gameObj.transform.rotation = Quaternion.Euler(0, buck.heading, 0);
		doe.gameObj.transform.rotation = Quaternion.Euler(0, doe.heading, 0);
		fawn.gameObj.transform.rotation = Quaternion.Euler(0, fawn.heading, 0);

		buck.forwardRate = 0f;
		buck.turnRate = 0f;
		doe.forwardRate = 0f;
		doe.turnRate = 0f;
		fawn.forwardRate = 0f;
		fawn.turnRate = 0f;

        //System.Console.WriteLine("PLACE DEER POSITIONS");	
        //System.Console.WriteLine("positive variance: " + positionVariance.ToString());	
        //System.Console.WriteLine("buck X: " + buck.gameObj.transform.position.x.ToString());	

        // Check if Deer is in road, update the spawn positions
        buck.collisionChecker.QuickRoadCheck();
        doe.collisionChecker.QuickRoadCheck();
        fawn.collisionChecker.QuickRoadCheck();
        if (buck.collisionChecker.roadDetected || doe.collisionChecker.roadDetected || fawn.collisionChecker.roadDetected)
        {
            Debug.Log("Deer on road ! Changing spawn position");
            PlaceDeerPositions();
        }

		// Check if any of the Deer are too close to the puma
		Vector3 pumaPosition = new Vector3(pumaX, pumaY, pumaZ);
		float buckDistance = Mathf.Abs((buck.gameObj.transform.position - pumaPosition).magnitude);
		float doeDistance = Mathf.Abs((doe.gameObj.transform.position - pumaPosition).magnitude);
		float fawnDistance = Mathf.Abs((fawn.gameObj.transform.position - pumaPosition).magnitude);
		if((buckDistance <= chaseTriggerDistance) || (doeDistance <= chaseTriggerDistance) || (fawnDistance <= chaseTriggerDistance)) {
			Debug.Log("Deer too close to puma! Changing spawn position!");
			PlaceDeerPositions();
		}
    }

	//===================================
	//===================================
	//		UTILITIES
	//===================================
	//===================================
	
	public float GetAngleFromOffset(float x1, float y1, float x2, float y2)
	{
        float deltaX = x2 - x1;
        float deltaY = y2 - y1;
        float angle = Mathf.Atan2(deltaY, -deltaX) * (180f / Mathf.PI);
        angle -= 90f;
        if (angle < 0f)
			angle += 360f;
		if (angle >= 360f)
			angle -= 360f;
		return angle;
	}	
	
	void CalculateFrameRate()	// for frame rate display
	{
		int currentMsec = (int)(Time.time * 1000);

		if (frameCount == 0 || currentMsec - frameFirstTime > 1500) {
			frameCount = 1;
			frameFirstTime = framePrevTime = currentMsec;
		}
		else {
			frameCurrentDuration = currentMsec - framePrevTime;
			frameAverageDuration =  (currentMsec - frameFirstTime) / frameCount;
			framePrevTime = currentMsec;
			frameCount++;
		}
	}

	public float GetTerrainHeight(float x, float z, float minHeight = 0f)
	{
		float terrainX = terrainA.transform.position.x;
		float terrainZ = terrainA.transform.position.z;
	
		while (x < terrainX)
			x += terrainSideLength;
		while (x >= terrainX + 1000)
			x -= terrainSideLength;
		while (z < terrainZ)
			z += terrainSideLength;
		while (z >= terrainZ + 1000)
			z -= terrainSideLength;

		float terrainHeight = terrainMaster.SampleHeight(new Vector3(x, 0, z));

		if (minHeight > terrainHeight)
			return minHeight;
		else
			return terrainHeight;
	}
	
	public float GetStartingTerrainX(int terrainNum)
	{
		switch (terrainNum) {
		case 0:
			return terrainPosInitNEG;
		case 1:
			return terrainPosInitPOS;
		case 2:
			return -terrainPosInitNEG;
		case 3:
			return terrainPosInitPOS;
		}	
		return 0f;
	}
	
	public float GetStartingTerrainZ(int terrainNum)
	{
		switch (terrainNum) {
		case 0:
			return terrainPosInitPOS;
		case 1:
			return terrainPosInitPOS;
		case 2:
			return terrainPosInitNEG;
		case 3:
			return terrainPosInitNEG;
		}	
		return 0f;
	}

	public Vector3 GetTerrainPosition(Vector3 referencePosition)
	{
		float refX = referencePosition.x;
		float refZ = referencePosition.z;

		if (refX >= terrainA.transform.position.x && refX < terrainA.transform.position.x + 1000f && refZ >= terrainA.transform.position.z && refZ < terrainA.transform.position.z + 1000f) {
			return terrainA.transform.position;
		}
		else if (refX >= terrainB.transform.position.x && refX < terrainB.transform.position.x + 1000f && refZ >= terrainB.transform.position.z && refZ < terrainB.transform.position.z + 1000f) {
			return terrainB.transform.position;
		}
		else if (refX >= terrainC.transform.position.x && refX < terrainC.transform.position.x + 1000f && refZ >= terrainC.transform.position.z && refZ < terrainC.transform.position.z + 1000f) {
			return terrainC.transform.position;
		}
		else if (refX >= terrainD.transform.position.x && refX < terrainD.transform.position.x + 1000f && refZ >= terrainD.transform.position.z && refZ < terrainD.transform.position.z + 1000f) {
			return terrainD.transform.position;
		}
		Debug.Log("ERROR: levelManager.GetTerrainPosition got point not in terrain");
		return new Vector3(0f, 0f, 0f);
	}
	
	public float GetTerrainMinX()
	{
		float terrainMinX = (terrainA.transform.position.x < terrainB.transform.position.x) ? terrainA.transform.position.x : terrainB.transform.position.x;
		terrainMinX = (terrainMinX < terrainC.transform.position.x) ? terrainMinX : terrainC.transform.position.x;
		terrainMinX = (terrainMinX < terrainD.transform.position.x) ? terrainMinX : terrainD.transform.position.x;
		return terrainMinX;
	}

	public float GetTerrainMinZ()
	{
		float terrainMinZ = (terrainA.transform.position.z < terrainB.transform.position.z) ? terrainA.transform.position.z : terrainB.transform.position.z;
		terrainMinZ = (terrainMinZ < terrainC.transform.position.z) ? terrainMinZ : terrainC.transform.position.z;
		terrainMinZ = (terrainMinZ < terrainD.transform.position.z) ? terrainMinZ : terrainD.transform.position.z;
		return terrainMinZ;
	}

	public float GetTerrainMaxX()
	{
		float terrainMaxX = (terrainA.transform.position.x > terrainB.transform.position.x) ? terrainA.transform.position.x : terrainB.transform.position.x;
		terrainMaxX = (terrainMaxX > terrainC.transform.position.x) ? terrainMaxX : terrainC.transform.position.x;
		terrainMaxX = (terrainMaxX > terrainD.transform.position.x) ? terrainMaxX : terrainD.transform.position.x;
		return terrainMaxX + terrainSideLength;
	}

	public float GetTerrainMaxZ()
	{
		float terrainMaxZ = (terrainA.transform.position.z > terrainB.transform.position.z) ? terrainA.transform.position.z : terrainB.transform.position.z;
		terrainMaxZ = (terrainMaxZ > terrainC.transform.position.z) ? terrainMaxZ : terrainC.transform.position.z;
		terrainMaxZ = (terrainMaxZ > terrainD.transform.position.z) ? terrainMaxZ : terrainD.transform.position.z;
		return terrainMaxZ + terrainSideLength;
	}

	void SetTerrainNeighbors()
	{
		// associate each terrain with its neighbors to hide level-of-detail seams

		float posAx = terrainA.transform.position.x;
		float posAz = terrainA.transform.position.z;
		float posBx = terrainB.transform.position.x;
		float posBz = terrainB.transform.position.z;
		float posCx = terrainC.transform.position.x;
		float posCz = terrainC.transform.position.z;
		float posDx = terrainD.transform.position.x;
		float posDz = terrainD.transform.position.z;

		Terrain terrainObjA = terrainA.GetComponent<Terrain>();
		Terrain terrainObjB = terrainB.GetComponent<Terrain>();
		Terrain terrainObjC = terrainC.GetComponent<Terrain>();
		Terrain terrainObjD = terrainD.GetComponent<Terrain>();
		
		if (posAx + terrainSideLength == posBx  &&  posCz + terrainSideLength == posAz) {
			// A  B
			// C  D
			terrainObjA.SetNeighbors(null, null, terrainObjB, terrainObjC);
			terrainObjB.SetNeighbors(terrainObjA, null, null, terrainObjD);
			terrainObjC.SetNeighbors(null, terrainObjA, terrainObjD, null);
			terrainObjD.SetNeighbors(terrainObjC, terrainObjB, null, null);
		}

		else if (posBx + terrainSideLength == posAx  &&  posCz + terrainSideLength == posAz) {
			// B  A
			// D  C
			terrainObjB.SetNeighbors(null, null, terrainObjA, terrainObjD);
			terrainObjA.SetNeighbors(terrainObjB, null, null, terrainObjC);
			terrainObjD.SetNeighbors(null, terrainObjB, terrainObjC, null);
			terrainObjC.SetNeighbors(terrainObjD, terrainObjA, null, null);
		}

		else if (posAx + terrainSideLength == posBx  &&  posAz + terrainSideLength == posCz) {
			// C  D
			// A  B
			terrainObjC.SetNeighbors(null, null, terrainObjD, terrainObjA);
			terrainObjD.SetNeighbors(terrainObjC, null, null, terrainObjB);
			terrainObjA.SetNeighbors(null, terrainObjC, terrainObjB, null);
			terrainObjB.SetNeighbors(terrainObjA, terrainObjD, null, null);
		}

		else if (posBx + terrainSideLength == posAx  &&  posAz + terrainSideLength == posCz) {
			// D  C
			// B  A
			terrainObjD.SetNeighbors(null, null, terrainObjC, terrainObjB);
			terrainObjC.SetNeighbors(terrainObjD, null, null, terrainObjA);
			terrainObjB.SetNeighbors(null, terrainObjD, terrainObjA, null);
			terrainObjA.SetNeighbors(terrainObjB, terrainObjC, null, null);
		}
		
		else {
			Debug.Log("ERROR - LevelManager.SetTerrainNeighbors() got bad terrain layout");
		}
	}
	
	
	void ResetAnimations()
	{
		buckAnimator.SetBool("Looking", false);
		buckAnimator.SetBool("Running", false);
		buckAnimator.SetBool("Die", false);
		
		doeAnimator.SetBool("Looking", false);
		doeAnimator.SetBool("Running", false);
		doeAnimator.SetBool("Die", false);
		
		fawnAnimator.SetBool("Looking", false);
		fawnAnimator.SetBool("Running", false);
		fawnAnimator.SetBool("Die", false);
	
		pumaAnimator.SetBool("Chasing", false);
		pumaAnimator.SetBool("DeerKill", false);
		pumaAnimator.SetBool("CarCollision", false);
		pumaAnimator.SetBool("PumaStarved", false);
		pumaAnimator.ResetTrigger("PumaPounce");

        PumaBloodProjector.transform.position = new Vector3(0, 0, 0);
		DeerBloodProjector.transform.position = new Vector3(0, 0, 0);
	}			


	// Functions added by Agnel

	// Adds a blood splatter projector and particle system on the dead puma / deer 
	void AddBloodSplatter(GameObject deadAnimal)
    {
        if (deadAnimal == pumaObj) // If dead animal is a puma
        {
            PumaBloodProjector.transform.parent = deadAnimal.transform;
            PumaBloodProjector.transform.position = deadAnimal.transform.position;
        }
        else // If dead animal is a deer
        {
            // Move the deer blood projector to the new dead spot. 
            DeerBloodProjector.transform.parent = deadAnimal.transform;
            DeerBloodProjector.transform.position = deadAnimal.transform.position;
        } 
        
        // Add Blood particles
        GameObject particles = (GameObject)Instantiate(bloodParticles, deadAnimal.transform.position, Quaternion.identity);
        particles.transform.parent = deadAnimal.gameObject.transform;
        Destroy(particles, 1.0f); // Destroy the particle system after the emission is complete; 
    }

}














