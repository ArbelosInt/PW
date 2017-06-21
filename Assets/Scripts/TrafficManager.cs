using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// Manages flow of traffic along roads
/// 

public class TrafficManager : MonoBehaviour {

	//===================================
	//===================================
	//
	//	EasyRoads3D config
	//
	//	Level2 - dirt; dirt; dirt
	//	Level3 - 4-lane; 2-lane; 2-lane
	//	Level4 - 6-lane; 2-lane; 4-lane
	//	Level5 - 6-lane; 2-lane; 4-lane
	//
	//	dirt   - width:6.5, surrounding:2, IOS
	//	2-Lane - width:11, surrounding:3, IOS
	//	4-Lane - width:19, surrounding:4, IOS
	//	6-Lane - width:26, surrounding:5, IOS
	//
	//	Road1 - 23 markers
	//	Road2 - 26 markers
	//	Road3 - 28 markers
	//
	//	ROAD 1 - bridge markers: 12, 18; adjoining marker surround distances: +2, +1
	//	ROAD 3 - marker 3: surround L/R 7 and left-indent 3; marker 4: surround L 7 and left-indent 3
	//
	//	Special case for Road 1 Level 2 (road 1 is flat not bridged)
	//		- set all middle markers y to terrain height (press ctrl)
	//		- set markers 11 and 12 y to 2.0
	//		- set markers 17 and 18 y to 1.9
	//
	//	Tie new roads into LevelManager
	
	//===================================
	//===================================
	//
	//	Overpass & Underpass config
	//
	//	Overpass 4: custom terrain leveling
	//
	//	OVERPASSES
	//
	//	- set road width to 10
	//	- place road nodes at 6 points
	//	- set marker 4 as bridge
	//	- set end marker heights as +0.5 of overpass height
	//	- interpolate markers
	//	- indents, from center: (road-10)/2, 15, default
	//	- surround widths, from center: 40, 30, 20
	//	
	//
	//	UNDERPASSES
	//
	//	- place underpass just under low point of road; add 10
	//	- set road width to 10
	//	- place road nodes at 6 points
	//	- set end marker heights as +0.5 of overpass height
	//	- interpolate markers
	//	- drop markers by 17
	//	- indents, from center: (road-40)/2, default, default
	//	- surround widths, from center: 15, 50, 3
	//	- outer nodes go to surface height
	//	- middle nodes get adjusted to taste
	//	
	
	//===================================
	//===================================
	//		MODULE VARIABLES
	//===================================
	//===================================

	private bool disableCarsFlag = false;
	private bool moduleInitialized = false;
	private bool engineNoiseStarted = false;
	private int preInitLevelSelection = 0;
	private float carObjectCreationRadius = 500f;
	
	// NODES

	private float laneWidth = 4f;
	private float pavedRoadCenterWidth = 0.75f;
	private float dirtRoadCenterWidth = -0.65f;

	// external nodes created in the terrain
	public GameObject[] road1Nodes;
	public GameObject[] road1L2Nodes; // special case for flat road1-level2
	public GameObject[] road2Nodes;
	public GameObject[] road3Nodes;

	// internal arrays includes extra nodes for node -1 and node n+1
	private NodeInfo[] nodeArray1Ascending;  
	private NodeInfo[] nodeArray2Ascending;
	private NodeInfo[] nodeArray3Ascending;

	private NodeInfo[] nodeArray1Descending;  
	private NodeInfo[] nodeArray2Descending;
	private NodeInfo[] nodeArray3Descending;

	private NodeInfo[] nodeArray1L2Ascending;  
	private NodeInfo[] nodeArray2L2Ascending;  
	private NodeInfo[] nodeArray3L2Ascending;  

	private NodeInfo[] nodeArray1L2Descending;  
	private NodeInfo[] nodeArray2L2Descending;  
	private NodeInfo[] nodeArray3L2Descending;  
	
	// data structures for creating lane grids
	private class NodeInfo {
		public Vector3 position;
		public float segmentLength;
		public float segmentHeading;
		public float segmentPitch;
		public VirtualNodeInfo[] vNodes;   // lanes 1-3
	}
	private class VirtualNodeInfo {
		public Vector3 position;
		public float segmentLength;
		public float segmentHeading;
		public float segmentPitch;
		public float previousSegmentHeading;
		public float nextSegmentHeading;
	}
	
	// ROADS

	private class RoadInfo {
		public int lanesPerSide;
		public float laneSpeed1;
		public float laneSpeed2;
		public float laneSpeed3;
		public float followDistance1;
		public float followDistance2;
		public float followDistance3;
		public NodeInfo[] nodeArrayAscending;
		public NodeInfo[] nodeArrayDescending;
		public bool orientationIsX;
	}
	
	private RoadInfo[] roadArray;
	private bool road1OrientationIsX;  
	private bool road2OrientationIsX;  
	private bool road3OrientationIsX;  
	
	// VEHICLES
	
	private class VehicleInfo {
		public GameObject vehicle;
		public VehicleController vehicleController;
		public int lane;
		public float speed;
		public bool ascendingFlag;
		public NodeInfo[] nodeArray;
		public bool roadOrientationIsX;
		public int currentSegment;
		public float percentTravelled;
		public Vector3 segmentStartPos;
		public Vector3 segmentEndPos;
		public float segmentLength;
		public float segmentPitch;
		public float segmentHeading;
		public float previousSegmentHeading;
		public float nextSegmentHeading;
		public Vector3 terrainPos;
	}

	private List<VehicleInfo> vehicleList;
	private GameObject vehiclesContainerObj;
	
	public GameObject[] vehicleModels;
	public AudioModule audioModule;
	
	// EXTERNAL MODULES
	private LevelManager levelManager;

	//===================================
	//===================================
	//		INITIALIZATION
	//===================================
	//===================================

	void Start()
	{
		// connect to external modules
		levelManager = GetComponent<LevelManager>();

		// create NodeArrays with extra nodes for node -1 and node n+1
		
		// paved roads
		nodeArray1Ascending = InitNodeArray(road1Nodes, true, false, pavedRoadCenterWidth);
		nodeArray2Ascending = InitNodeArray(road2Nodes, true, true, pavedRoadCenterWidth);
		nodeArray3Ascending = InitNodeArray(road3Nodes, true, true, pavedRoadCenterWidth);

		nodeArray1Descending = InitNodeArray(road1Nodes, false, false, pavedRoadCenterWidth);
		nodeArray2Descending = InitNodeArray(road2Nodes, false, true, pavedRoadCenterWidth);
		nodeArray3Descending = InitNodeArray(road3Nodes, false, true, pavedRoadCenterWidth);

		// dirt roads
		nodeArray1L2Ascending = InitNodeArray(road1L2Nodes, true, false, dirtRoadCenterWidth);
		nodeArray2L2Ascending = InitNodeArray(road2Nodes, true, true, dirtRoadCenterWidth);
		nodeArray3L2Ascending = InitNodeArray(road3Nodes, true, true, dirtRoadCenterWidth);

		nodeArray1L2Descending = InitNodeArray(road1L2Nodes, false, false, dirtRoadCenterWidth);
		nodeArray2L2Descending = InitNodeArray(road2Nodes, false, true, dirtRoadCenterWidth);
		nodeArray3L2Descending = InitNodeArray(road3Nodes, false, true, dirtRoadCenterWidth);
	
	
		// create array of RoadInfo data structures
		roadArray = new RoadInfo[3];
		roadArray[0] = new RoadInfo();
		roadArray[1] = new RoadInfo();
		roadArray[2] = new RoadInfo();
		roadArray[0].nodeArrayAscending = nodeArray1Ascending;
		roadArray[1].nodeArrayAscending = nodeArray2Ascending;
		roadArray[2].nodeArrayAscending = nodeArray3Ascending;
		roadArray[0].nodeArrayDescending = nodeArray1Descending;
		roadArray[1].nodeArrayDescending = nodeArray2Descending;
		roadArray[2].nodeArrayDescending = nodeArray3Descending;
		roadArray[0].orientationIsX = false;
		roadArray[1].orientationIsX = true;
		roadArray[2].orientationIsX = true;
		
		// create empty vehicleList
		vehicleList = new List<VehicleInfo>();
		vehiclesContainerObj = GameObject.Find("Vehicles");

		moduleInitialized = true;
		
		if (preInitLevelSelection != 0) {
			InitLevel(preInitLevelSelection);
		}		
	}
	
	
	// initialize the NodeArrays with extra nodes for node -1 and node n+1
	
	private NodeInfo[] InitNodeArray(GameObject[] roadNodesAscending, bool ascendingFlag, bool orientationIsX, float centerWidth)
	{
		GameObject[] roadNodes = null;
		Vector3 nodeOffsetVector = new Vector3(0f, 0f, 0f);
		
		// access manually placed game objects (possibly with inversion)
		if (ascendingFlag == true) {
			// use existing roadNodes
			roadNodes = new GameObject[roadNodesAscending.Length];
			for (int i = 0; i < roadNodesAscending.Length; i++) {
				roadNodes[i] = roadNodesAscending[i];
			}
			nodeOffsetVector = (orientationIsX ? (new Vector3(-1000f, 0f, 0f)) : (new Vector3(0f, 0f, -1000f)));
		}
		else {
			// create descending version of roadNodes
			roadNodes = new GameObject[roadNodesAscending.Length];
			for (int i = 0; i < roadNodesAscending.Length; i++) {
				roadNodes[i] = roadNodesAscending[roadNodesAscending.Length-1-i];
			}
			nodeOffsetVector = (orientationIsX ? (new Vector3(1000f, 0f, 0f)) : (new Vector3(0f, 0f, 1000f)));
		}

		// create array
		int arraySize = roadNodes.Length+2;
		NodeInfo[] nodeArray = new NodeInfo[arraySize];

		// first node is node "-1" (projection of game object "n")
		nodeArray[0] = new NodeInfo();
		nodeArray[0].position = roadNodes[roadNodes.Length-1].transform.position + nodeOffsetVector;

		// pull nodes from game object array
		for (int i = 1; i <= roadNodes.Length; i++) {
			nodeArray[i] = new NodeInfo();
			nodeArray[i].position = roadNodes[i-1].transform.position;
		}

		// last node is node "n+1" (projection of game object "0")
		nodeArray[arraySize-1] = new NodeInfo();
		nodeArray[arraySize-1].position = roadNodes[0].transform.position - nodeOffsetVector;

		// initialize node data
		
		for (int i = 0; i < arraySize-1; i++) {
			// static info for each segment
			nodeArray[i].segmentLength = Vector3.Distance(nodeArray[i].position, nodeArray[i+1].position);					
			Vector2 segmentStartVector2 = new Vector2(nodeArray[i].position.x, nodeArray[i].position.z);
			Vector2 segmentEndVector2 = new Vector2(nodeArray[i+1].position.x, nodeArray[i+1].position.z);
			float segmentFlatDistance = Vector2.Distance(segmentStartVector2, segmentEndVector2);
			nodeArray[i].segmentHeading = levelManager.GetAngleFromOffset(nodeArray[i].position.x, nodeArray[i].position.z, nodeArray[i+1].position.x, nodeArray[i+1].position.z);
			nodeArray[i].segmentPitch = levelManager.GetAngleFromOffset(nodeArray[i+1].position.y, 0, nodeArray[i].position.y, segmentFlatDistance);
		}
	
		for (int i = 0; i < arraySize-1; i++) {
			// create vNodes for each segment
			nodeArray[i].vNodes = new VirtualNodeInfo[3];
			for (int lane = 0; lane < 3; lane++) {
				nodeArray[i].vNodes[lane] = new VirtualNodeInfo();
				// first determine composite heading based on previous segment and current segment
				float previousSegmentHeading = nodeArray[(i == 0) ? (arraySize-3) : (i-1) ].segmentHeading;
				float currentSegmentHeading = nodeArray[i].segmentHeading;
				float vNodeOffsetDirection = InterpolateAngles(previousSegmentHeading, currentSegmentHeading, 0.5f) + 90f;
				
				//Debug.Log("vNodeOffsetDirection is " + (int)vNodeOffsetDirection + " for item " + i);
				
				// now create the vNode for this lane		
				float laneOffset = centerWidth + laneWidth/2 + laneWidth * lane;
				float vNodeX = nodeArray[i].position.x + (Mathf.Sin(vNodeOffsetDirection*Mathf.PI/180) * laneOffset);
				float vNodeZ = nodeArray[i].position.z + (Mathf.Cos(vNodeOffsetDirection*Mathf.PI/180) * laneOffset);
				nodeArray[i].vNodes[lane].position = new Vector3(vNodeX, nodeArray[i].position.y, vNodeZ);
			}
		}

		// initialize vNode positions for last node
		{
			int i = arraySize-1;
			nodeArray[i].vNodes = new VirtualNodeInfo[3];
			for (int lane = 0; lane < 3; lane++) {
				nodeArray[i].vNodes[lane] = new VirtualNodeInfo();
				// first determine composite heading based on previous segment and current segment
				float previousSegmentHeading = nodeArray[arraySize-2].segmentHeading;
				float currentSegmentHeading = nodeArray[1].segmentHeading;
				float vNodeOffsetDirection = InterpolateAngles(previousSegmentHeading, currentSegmentHeading, 0.5f) + 90f;
				
				//Debug.Log("vNodeOffsetDirection is " + (int)vNodeOffsetDirection + " for item " + i);
				
				// now create the vNode for this lane		
				float laneOffset = centerWidth + laneWidth/2 + laneWidth * lane;
				float vNodeX = nodeArray[i].position.x + (Mathf.Sin(vNodeOffsetDirection*Mathf.PI/180) * laneOffset);
				float vNodeZ = nodeArray[i].position.z + (Mathf.Cos(vNodeOffsetDirection*Mathf.PI/180) * laneOffset);
				nodeArray[i].vNodes[lane].position = new Vector3(vNodeX, nodeArray[i].position.y, vNodeZ);
			}
		}

		// update static info for each vNode
		for (int i = 0; i < arraySize-1; i++) {
			// all vNodes except last one
			for (int lane = 0; lane < 3; lane++) {
				Vector2 segmentStartVector2 = new Vector2(nodeArray[i].vNodes[lane].position.x, nodeArray[i].vNodes[lane].position.z);
				Vector2 segmentEndVector2 = new Vector2(nodeArray[i+1].vNodes[lane].position.x, nodeArray[i+1].vNodes[lane].position.z);
				float segmentFlatDistance = Vector2.Distance(segmentStartVector2, segmentEndVector2);
				nodeArray[i].vNodes[lane].segmentLength = Vector3.Distance(nodeArray[i].vNodes[lane].position, nodeArray[i+1].vNodes[lane].position);					
				nodeArray[i].vNodes[lane].segmentPitch = levelManager.GetAngleFromOffset(nodeArray[i+1].vNodes[lane].position.y, 0, nodeArray[i].vNodes[lane].position.y, segmentFlatDistance);
				nodeArray[i].vNodes[lane].segmentHeading = levelManager.GetAngleFromOffset(nodeArray[i].vNodes[lane].position.x, nodeArray[i].vNodes[lane].position.z, nodeArray[i+1].vNodes[lane].position.x, nodeArray[i+1].vNodes[lane].position.z);
			}
		}
		for (int lane = 0; lane < 3; lane++) {
			// last vNode
			nodeArray[arraySize-1].vNodes[lane].segmentLength = nodeArray[1].vNodes[lane].segmentLength;
			nodeArray[arraySize-1].vNodes[lane].segmentPitch = nodeArray[1].vNodes[lane].segmentPitch;
			nodeArray[arraySize-1].vNodes[lane].segmentHeading = nodeArray[1].vNodes[lane].segmentHeading;
		}

		// update previousSegmentHeading and nextSegmentHeading for each vNode
		for (int i = 0; i < arraySize; i++) {
			for (int lane = 0; lane < 3; lane++) {
				int previousIndex = (i == 0) ? (arraySize-3) : (i-1);
				int nextIndex = (i == arraySize-1) ? (1) : (i+1);
				nodeArray[i].vNodes[lane].previousSegmentHeading = nodeArray[previousIndex].segmentHeading;
				nodeArray[i].vNodes[lane].nextSegmentHeading = nodeArray[nextIndex].segmentHeading;
			}
		}

		return nodeArray;
	}
	
	//===================================
	//===================================
	//		SET UP THE LEVEL
	//===================================
	//===================================

	public void InitLevel(int levelNum)
	{
		if (disableCarsFlag == true)
			return;
	
		if (moduleInitialized == false) {
			preInitLevelSelection = levelNum;
			return;
		}
	
		// remove any previously created vehicles
		for(int i=0; i<vehicleList.Count; i++)
			Destroy(vehicleList[i].vehicle);
		vehicleList.Clear();
		
		// check for valid level
		if (levelNum < 1 || levelNum > 4) {
			if (levelNum != 0)
				Debug.Log("ERROR: TrafficManager told to initialize invalid level");
			return;
		}
				
		// special casing for flat road 1 in level 2
		roadArray[0].nodeArrayAscending = (levelNum != 1) ? nodeArray1Ascending : nodeArray1L2Ascending;
		roadArray[1].nodeArrayAscending = (levelNum != 1) ? nodeArray2Ascending : nodeArray2L2Ascending;
		roadArray[2].nodeArrayAscending = (levelNum != 1) ? nodeArray3Ascending : nodeArray3L2Ascending;

		roadArray[0].nodeArrayDescending = (levelNum != 1) ? nodeArray1Descending : nodeArray1L2Descending;
		roadArray[1].nodeArrayDescending = (levelNum != 1) ? nodeArray2Descending : nodeArray2L2Descending;
		roadArray[2].nodeArrayDescending = (levelNum != 1) ? nodeArray3Descending : nodeArray3L2Descending;
		
		// configure the roads for the desired level
		SelectRoadConfig(levelNum);
		
		// add the vehicles to each of the roads in each of the terrains

		for (int t=0; t<4; t++) {
			for (int r=0; r<3; r++) {
				int laneCount = roadArray[r].lanesPerSide;
				for (int i = 0; i < laneCount; i++) {
					PopulateLane(r, i, true, levelManager.GetStartingTerrainX(t), levelManager.GetStartingTerrainZ(t));
					PopulateLane(r, i, false, levelManager.GetStartingTerrainX(t), levelManager.GetStartingTerrainZ(t));
				}
			}
		}

		// Startup the Engine Noise
		audioModule.PlaySound("EngineNoise");
	}
	
	private void PopulateLane(int roadNum, int laneNum, bool ascendingFlag, float terrainX, float terrainZ)
	{
		float segmentPercent = 0f;
		float followDistance = (laneNum == 0) ? roadArray[roadNum].followDistance1 : ((laneNum == 1) ? roadArray[roadNum].followDistance2 : roadArray[roadNum].followDistance3);
		followDistance = 0.8f * followDistance * (2f - levelManager.difficultyLevel) * (2f - levelManager.difficultyLevel);
		NodeInfo[] nodeArray = (ascendingFlag == true) ? roadArray[roadNum].nodeArrayAscending : roadArray[roadNum].nodeArrayDescending;

		int  i = 0;
		while (i < nodeArray.Length-2) {
			while (segmentPercent < 1f) {
				// add vehicles as long as the node has room
				VehicleInfo vehicleInfo = new VehicleInfo();
				vehicleInfo.terrainPos = new Vector3(terrainX, 0, terrainZ);
				vehicleInfo.vehicle = null;
				vehicleInfo.nodeArray = nodeArray;
				vehicleInfo.lane = laneNum;
				vehicleInfo.roadOrientationIsX = roadArray[roadNum].orientationIsX;	
				vehicleInfo.ascendingFlag = ascendingFlag;
				vehicleInfo.speed = (laneNum == 0) ? roadArray[roadNum].laneSpeed1 : ((laneNum == 1) ? roadArray[roadNum].laneSpeed2 : roadArray[roadNum].laneSpeed3);
				vehicleInfo.percentTravelled = segmentPercent;		
				vehicleInfo.currentSegment = i;
				vehicleInfo.segmentStartPos = nodeArray[i].vNodes[laneNum].position;
				vehicleInfo.segmentEndPos = nodeArray[i+1].vNodes[laneNum].position;
				vehicleInfo.segmentLength = nodeArray[i].vNodes[laneNum].segmentLength;
				vehicleInfo.segmentPitch = nodeArray[i].vNodes[laneNum].segmentPitch;
				vehicleInfo.segmentHeading = nodeArray[i].vNodes[laneNum].segmentHeading;
				vehicleInfo.previousSegmentHeading = nodeArray[i].vNodes[laneNum].previousSegmentHeading;
				vehicleInfo.nextSegmentHeading = nodeArray[i].vNodes[laneNum].nextSegmentHeading;
				vehicleList.Add(vehicleInfo);
				float followRandomize = Random.Range(0f, followDistance*2);
				segmentPercent += (followDistance+followRandomize) / nodeArray[i].vNodes[laneNum].segmentLength;
			}
			while (segmentPercent >= 1f) {
				// increment to next node where vehicle needs to go
				float extraDistance = (segmentPercent-1f) * nodeArray[i].vNodes[laneNum].segmentLength;
				if (++i >= nodeArray.Length-1)
					break;
				segmentPercent = extraDistance / nodeArray[i].vNodes[laneNum].segmentLength;
			}
		}	
	}
	
	
	//===================================
	//===================================
	//		PROCESS THE VEHICLES
	//===================================
	//===================================

	void Update ()
	{
		if (disableCarsFlag == true || vehicleList == null)
			return;
	
		for(int i=0; i<vehicleList.Count; i++) {
			VehicleInfo vehicleInfo = vehicleList[i];
			
			// add new distance to vehicle position
			float distanceTravelled = vehicleInfo.speed * Time.deltaTime * levelManager.difficultyLevel * levelManager.difficultyLevel * levelManager.difficultyLevel;

			float segmentLengthRemaining = vehicleInfo.segmentLength * (1f - vehicleInfo.percentTravelled);

			while (distanceTravelled >= segmentLengthRemaining) {
				distanceTravelled -= segmentLengthRemaining;
				vehicleInfo.currentSegment += 1;
			
				if (vehicleInfo.currentSegment == vehicleInfo.nodeArray.Length - 2) {
					// loop back to start of node list for next terrain
					vehicleInfo.currentSegment = 0;
					if (vehicleInfo.roadOrientationIsX == true)
						vehicleInfo.terrainPos += new Vector3(((vehicleInfo.ascendingFlag == true) ? 1000f : -1000f), 0f, 0f);
					else
						vehicleInfo.terrainPos += new Vector3(0f, 0f, ((vehicleInfo.ascendingFlag == true) ? 1000f : -1000f));
				}
				
				vehicleInfo.segmentStartPos = vehicleInfo.nodeArray[vehicleInfo.currentSegment].vNodes[vehicleInfo.lane].position;
				vehicleInfo.segmentEndPos = vehicleInfo.nodeArray[vehicleInfo.currentSegment+1].vNodes[vehicleInfo.lane].position;
				vehicleInfo.segmentLength = vehicleInfo.nodeArray[vehicleInfo.currentSegment].vNodes[vehicleInfo.lane].segmentLength;
				vehicleInfo.segmentPitch = vehicleInfo.nodeArray[vehicleInfo.currentSegment].vNodes[vehicleInfo.lane].segmentPitch;
				vehicleInfo.segmentHeading = vehicleInfo.nodeArray[vehicleInfo.currentSegment].vNodes[vehicleInfo.lane].segmentHeading;
				vehicleInfo.previousSegmentHeading = vehicleInfo.nodeArray[vehicleInfo.currentSegment].vNodes[vehicleInfo.lane].previousSegmentHeading;
				vehicleInfo.nextSegmentHeading = vehicleInfo.nodeArray[vehicleInfo.currentSegment].vNodes[vehicleInfo.lane].nextSegmentHeading;
				vehicleInfo.percentTravelled = 0f;		
				segmentLengthRemaining = vehicleInfo.segmentLength;
			}

			vehicleInfo.percentTravelled += distanceTravelled / vehicleInfo.segmentLength;
			
			// calculate new vehicle position
			Vector3 vehiclePos = vehicleInfo.terrainPos + Vector3.Lerp(vehicleInfo.segmentStartPos, vehicleInfo.segmentEndPos, vehicleInfo.percentTravelled);
			
			// if we're not over any terrain, adjust accordingly
			if (vehiclePos.x < levelManager.GetTerrainMinX()) {
				vehiclePos.x += 2000f;
				vehicleInfo.terrainPos.x += 2000f;
			}
			if (vehiclePos.z < levelManager.GetTerrainMinZ()) {
				vehiclePos.z += 2000f;
				vehicleInfo.terrainPos.z += 2000f;
			}
			if (vehiclePos.x > levelManager.GetTerrainMaxX()) {
				vehiclePos.x -= 2000f;
				vehicleInfo.terrainPos.x -= 2000f;
			}
			if (vehiclePos.z > levelManager.GetTerrainMaxZ()) {
				vehiclePos.z -= 2000f;
				vehicleInfo.terrainPos.z -= 2000f;
			}
						
			// no objects for vehicles far from puma
			float distanceToPuma = Vector3.Distance(levelManager.pumaObj.transform.position, vehiclePos);			
			if (distanceToPuma < carObjectCreationRadius && vehicleInfo.vehicle == null) {
				// close to puma; create object
				int min = 0;  int max = 10;
				int vehicleSelect = Random.Range(min, max);
				vehicleInfo.vehicle = Instantiate(vehicleModels[vehicleSelect], vehicleInfo.terrainPos, Quaternion.identity) as GameObject;
				vehicleInfo.vehicleController = vehicleInfo.vehicle.GetComponent<VehicleController>();
				vehicleInfo.vehicle.transform.parent = vehiclesContainerObj.transform;
			}
			else if (distanceToPuma >= carObjectCreationRadius && vehicleInfo.vehicle != null) {
				// far from puma; destroy object
				Destroy(vehicleInfo.vehicle);
				vehicleInfo.vehicle = null;
			}

			// if object, set location and rotation
			if (vehicleInfo.vehicle != null) {	
				vehicleInfo.vehicle.transform.position = vehiclePos;
				float heading = vehicleInfo.segmentHeading;
				float blendDistance = 0.2f;
				if (vehicleInfo.percentTravelled < blendDistance) {
					float scaleFactor = vehicleInfo.percentTravelled / blendDistance;
					scaleFactor = 1f - ((scaleFactor-1f) * (scaleFactor-1f));
					heading = InterpolateAngles(vehicleInfo.segmentHeading, vehicleInfo.previousSegmentHeading, 0.5f + (0.5f*scaleFactor));
				}
				else if (vehicleInfo.percentTravelled > 1f - blendDistance) {
					float scaleFactor = (vehicleInfo.percentTravelled - (1f - blendDistance)) / blendDistance;
					scaleFactor = scaleFactor * scaleFactor;
					heading = InterpolateAngles(vehicleInfo.segmentHeading, vehicleInfo.nextSegmentHeading, 1f - (0.5f*scaleFactor));
				}
				vehicleInfo.vehicle.transform.rotation = Quaternion.Euler(vehicleInfo.segmentPitch - 90f, heading, 0);

				VehicleController vCtrl = vehicleInfo.vehicle.GetComponent<VehicleController>();

				// store off heading and pitch for use during collision
				vehicleInfo.vehicleController.heading = heading;
				vehicleInfo.vehicleController.pitch = vehicleInfo.segmentPitch;
			}
		}
	}
	
	
	//===================================
	//===================================
	//		SELECT ROAD CONFIG
	//===================================
	//===================================
	
	
	private void SelectRoadConfig(int levelNum)
	{
		switch (levelNum) {

		case 1:  // level 2
			roadArray[0].lanesPerSide = 1;
			roadArray[0].laneSpeed1 = 25;
			roadArray[0].laneSpeed2 = 0;
			roadArray[0].laneSpeed3 = 0;
			roadArray[0].followDistance1 = 100;
			roadArray[0].followDistance2 = 0;
			roadArray[0].followDistance3 = 0;
			////////////
			roadArray[1].lanesPerSide = 1;
			roadArray[1].laneSpeed1 = 30;
			roadArray[1].laneSpeed2 = 0;
			roadArray[1].laneSpeed3 = 0;
			roadArray[1].followDistance1 = 140;
			roadArray[1].followDistance2 = 0;
			roadArray[1].followDistance3 = 0;
			////////////
			roadArray[2].lanesPerSide = 1;
			roadArray[2].laneSpeed1 = 27;
			roadArray[2].laneSpeed2 = 0;
			roadArray[2].laneSpeed3 = 0;
			roadArray[2].followDistance1 = 120;
			roadArray[2].followDistance2 = 0;
			roadArray[2].followDistance3 = 0;
			break;
		
		case 2:  // level 3
			roadArray[0].lanesPerSide = 2;
			roadArray[0].laneSpeed1 = 30;
			roadArray[0].laneSpeed2 =35;
			roadArray[0].laneSpeed3 = 0;
			roadArray[0].followDistance1 = 60;
			roadArray[0].followDistance2 = 70;
			roadArray[0].followDistance3 = 0;
			////////////
			roadArray[1].lanesPerSide = 1;
			roadArray[1].laneSpeed1 = 30;
			roadArray[1].laneSpeed2 = 0;
			roadArray[1].laneSpeed3 = 0;
			roadArray[1].followDistance1 = 90;
			roadArray[1].followDistance2 = 0;
			roadArray[1].followDistance3 = 0;
			////////////
			roadArray[2].lanesPerSide = 1;
			roadArray[2].laneSpeed1 = 32;
			roadArray[2].laneSpeed2 = 0;
			roadArray[2].laneSpeed3 = 0;
			roadArray[2].followDistance1 = 80;
			roadArray[2].followDistance2 = 0;
			roadArray[2].followDistance3 = 0;
			break;
		
		case 3:  // level 4
			bool followClose = false;
		
			roadArray[0].lanesPerSide = 3;
			roadArray[0].laneSpeed1 = 35;
			roadArray[0].laneSpeed2 = 37;
			roadArray[0].laneSpeed3 = 39;
			roadArray[0].followDistance1 = (followClose == true) ? 4 : 35;
			roadArray[0].followDistance2 = (followClose == true) ? 4 : 37;
			roadArray[0].followDistance3 = (followClose == true) ? 4 : 39;
			////////////
			roadArray[1].lanesPerSide = 1;
			roadArray[1].laneSpeed1 = 35;
			roadArray[1].laneSpeed2 = 0;
			roadArray[1].laneSpeed3 = 0;
			roadArray[1].followDistance1 = (followClose == true) ? 4 : 35;
			roadArray[1].followDistance2 = 0;
			roadArray[1].followDistance3 = 0;
			////////////
			roadArray[2].lanesPerSide = 2;
			roadArray[2].laneSpeed1 = 35;
			roadArray[2].laneSpeed2 = 37;
			roadArray[2].laneSpeed3 = 0;
			roadArray[2].followDistance1 = (followClose == true) ? 4 : 35;
			roadArray[2].followDistance2 = 37;
			roadArray[2].followDistance3 = 0;
			break;
		
		case 4:  // level 5
			roadArray[0].lanesPerSide = 3;
			roadArray[0].laneSpeed1 = 35;
			roadArray[0].laneSpeed2 = 37;
			roadArray[0].laneSpeed3 = 40;
			roadArray[0].followDistance1 = 35;
			roadArray[0].followDistance2 = 37;
			roadArray[0].followDistance3 = 40;
			////////////
			roadArray[1].lanesPerSide = 1;
			roadArray[1].laneSpeed1 = 35;
			roadArray[1].laneSpeed2 = 0;
			roadArray[1].laneSpeed3 = 0;
			roadArray[1].followDistance1 = 35;
			roadArray[1].followDistance2 = 0;
			roadArray[1].followDistance3 = 0;
			////////////
			roadArray[2].lanesPerSide = 2;
			roadArray[2].laneSpeed1 = 35;
			roadArray[2].laneSpeed2 = 37;
			roadArray[2].laneSpeed3 = 0;
			roadArray[2].followDistance1 = 35;
			roadArray[2].followDistance2 = 37;
			roadArray[2].followDistance3 = 0;
			break;
		}
	}

	//===================================
	//===================================
	//		FIND CLOSEST NODE
	//===================================
	//===================================

	public Vector3 FindClosestNode(Vector3 referencePosition)
	{
		Vector3 closestNodePos = referencePosition + new Vector3(2000f, 0, 0);	
		float closestNodeDistance = Vector3.Distance(referencePosition, closestNodePos);
		Vector3 terrainPos = levelManager.GetTerrainPosition(referencePosition);
		int closestRoad = 0;
	
		for (int i = 0; i < road1Nodes.Length; i++) {
			Vector3 currentPos = road1Nodes[i].transform.position + terrainPos + new Vector3(1000f, 0, 0);
			float currentDistance = Vector3.Distance(referencePosition, currentPos);
			if (currentDistance < closestNodeDistance) {
				closestNodePos = currentPos;
				closestNodeDistance = currentDistance;
				closestRoad = 1;
			}
		}
	
		for (int i = 0; i < road2Nodes.Length; i++) {
			Vector3 currentPos = road2Nodes[i].transform.position + terrainPos + new Vector3(1000f, 0, 0);
			float currentDistance = Vector3.Distance(referencePosition, currentPos);
			if (currentDistance < closestNodeDistance) {
				closestNodePos = currentPos;
				closestNodeDistance = currentDistance;
				closestRoad = 2;
			}
		}

		for (int i = 0; i < road3Nodes.Length; i++) {
			Vector3 currentPos = road3Nodes[i].transform.position + terrainPos + new Vector3(1000f, 0, 0);
			float currentDistance = Vector3.Distance(referencePosition, currentPos);
			if (currentDistance < closestNodeDistance) {
				closestNodePos = currentPos;
				closestNodeDistance = currentDistance;
				closestRoad = 3;
			}
		}

		return closestNodePos;
	}

	//===================================
	//===================================
	//		FIND CLOSEST ROAD
	//===================================
	//===================================

	public int FindClosestRoad(Vector3 referencePosition)
	{
		Vector3 closestNodePos = referencePosition + new Vector3(2000f, 0, 0);	
		float closestNodeDistance = Vector3.Distance(referencePosition, closestNodePos);
		Vector3 terrainPos = levelManager.GetTerrainPosition(referencePosition);
		int closestRoad = 0;
	
		for (int i = 0; i < road1Nodes.Length; i++) {
			Vector3 currentPos = road1Nodes[i].transform.position + terrainPos + new Vector3(1000f, 0, 0);
			float currentDistance = Vector3.Distance(referencePosition, currentPos);
			if (currentDistance < closestNodeDistance) {
				closestNodePos = currentPos;
				closestNodeDistance = currentDistance;
				closestRoad = 1;
			}
		}
	
		for (int i = 0; i < road2Nodes.Length; i++) {
			Vector3 currentPos = road2Nodes[i].transform.position + terrainPos + new Vector3(1000f, 0, 0);
			float currentDistance = Vector3.Distance(referencePosition, currentPos);
			if (currentDistance < closestNodeDistance) {
				closestNodePos = currentPos;
				closestNodeDistance = currentDistance;
				closestRoad = 2;
			}
		}

		for (int i = 0; i < road3Nodes.Length; i++) {
			Vector3 currentPos = road3Nodes[i].transform.position + terrainPos + new Vector3(1000f, 0, 0);
			float currentDistance = Vector3.Distance(referencePosition, currentPos);
			if (currentDistance < closestNodeDistance) {
				closestNodePos = currentPos;
				closestNodeDistance = currentDistance;
				closestRoad = 3;
			}
		}

		return closestRoad;
	}


	//===================================
	//===================================
	//		UTILS
	//===================================
	//===================================

	private float InterpolateAngles(float angle1, float angle2, float angle1Percent)
	{
		float interpolatedAngle = 0f;

		if (angle1 < 0f)
			angle1 += 360f;
		if (angle2 < 0f)
			angle2 += 360f;
	
		if (angle2 > angle1) {
			if (angle2 - angle1 < 180f) {
				interpolatedAngle = angle2 - ((angle2 - angle1) * angle1Percent);
			}
			else {
				angle1 += 360f;
				interpolatedAngle = angle1 - ((angle1 - angle2) * (1f-angle1Percent));
			}
		}
		else {
			if (angle1 - angle2 < 180f) {
				interpolatedAngle = angle1 - ((angle1 - angle2) * (1f-angle1Percent));
			}
			else {
				angle2 += 360f;
				interpolatedAngle = angle2 - ((angle2 - angle1) * angle1Percent);
			}
		}

		return interpolatedAngle;
	}
	
}
