using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using System;
using EasyRoads3D;

public class RoadObjectScript : MonoBehaviour {
static public string version = "";
public int objectType = 0;
public bool displayRoad = true;
public float roadWidth = 5.0f;
public float indent = 3.0f;
public float surrounding = 5.0f;
public float raise = 1.0f;
public float raiseMarkers = 0.5f;
public bool OOQDOOQQ = false;
public bool renderRoad = true;
public bool beveledRoad = false;
public bool applySplatmap = false;
public int splatmapLayer = 4;
public bool autoUpdate = true;
public float geoResolution = 5.0f;
public int roadResolution = 1;
public float tuw =  15.0f;
public int splatmapSmoothLevel;
public float opacity = 1.0f;
public int expand = 0;
public int offsetX = 0;
public int offsetY = 0;
private Material surfaceMaterial;
public float surfaceOpacity = 1.0f;
public float smoothDistance = 1.0f;
public float smoothSurDistance = 3.0f;
private bool handleInsertFlag;
public bool handleVegetation = true;
public float OODOCOQQQQ = 2.0f;
public float OQDQODQQDC = 1f;
public int materialType = 0;
String[] materialStrings;
public string uname;
public string email;
private MarkerScript[] mSc;

private bool OCCDCQOQDC;
private bool[] OOCCCDOOCQ = null;
private bool[] OCCCDQDOQC = null;
public string[] OODDCOQCOO;
public string[] ODODQOQO;
public int[] ODODQOQOInt;
public int OQOCDQDDOQ = -1;
public int ODCDCODQOC = -1;
static public GUISkin ODCQCOODCC;
static public GUISkin OQQOQODQDO;
public bool ODCQQDDCOQ = false;
private Vector3 cPos;
private Vector3 ePos;
public bool OQOCOOQOOO;
static public Texture2D OQQOQQODDQ;
public int markers = 1;
public OQQCCCOQCQ ODQQOCODOC;
private GameObject ODOQDQOO;
public bool OCOOQDQCQC;
public bool doTerrain;
private Transform OCCCCCQQQD = null;
public GameObject[] OCCCCCQQQDs;
private static string OODDOCDODO = null;
public Transform obj;
private string OOOCCCCQCC;
public static string erInit = "";
static public Transform OCODQODOQC;
private RoadObjectScript OQCQDDCCCC;
public bool flyby;


private Vector3 pos;
private float fl;
private float oldfl;
private bool OODODOOOQC;
private bool OQCDDDOODD;
private bool OOQDOCCCOC;
public Transform OCQDOCQQOC;
public int OdQODQOD = 1;
public float OOQQQDOD = 0f;
public float OOQQQDODOffset = 0f;
public float OOQQQDODLength = 0f;
public bool ODODDDOO = false;
static public string[] ODOQDOQO;
static public string[] ODODOQQO; 
static public string[] ODODQOOQ;
public int ODQDOOQO = 0;
public string[] ODQQQQQO;  
public string[] ODODDQOO; 
public bool[] ODODQQOD; 
public int[] OOQQQOQO; 
public int ODOQOOQO = 0; 

public bool forceY = false;
public float yChange = 0f;
public float floorDepth = 2f;
public float waterLevel = 1.5f; 
public bool lockWaterLevel = true;
public float lastY = 0f;
public string distance = "0";
public string markerDisplayStr = "Hide Markers";
static public string[] objectStrings;
public string objectText = "Road";
public bool applyAnimation = false;
public float waveSize = 1.5f;
public float waveHeight = 0.15f;
public bool snapY = true;

private TextAnchor origAnchor;
public bool autoODODDQQO;
public Texture2D roadTexture;
public Texture2D roadMaterial;
public string[] OQQQOOOOCC;
public string[] ODCCODOQQD;
public int selectedWaterMaterial;
public int selectedWaterScript;
private bool doRestore = false;
public bool doFlyOver;
public static GameObject tracer;
public Camera goCam;
public float speed = 1f;
public float offset = 0f;
public bool camInit;
public GameObject customMesh = null;
static public bool disableFreeAlerts = true;
public bool multipleTerrains;
public bool editRestore = true;
public Material roadMaterialEdit;
static public int backupLocation = 0;
public string[] backupStrings = new string[2]{"Outside Assets folder path","Inside Assets folder path"};
public Vector3[] leftVecs = new Vector3[0];
public Vector3[] rightVecs = new Vector3[0];
public bool applyTangents = false;
public bool sosBuild = false;
public float splinePos = 0;
public float camHeight = 3;
public Vector3 splinePosV3 = Vector3.zero;
public bool blendFlag; 
public float startBlendDistance = 5;
public float endBlendDistance = 5;
public bool iOS = false;
static public string extensionPath = "";
public void OODQOODDCO(List<ODODDQQO> arr, String[] DOODQOQO, String[] OODDQOQO){

OQDQQQDQDQ(transform, arr, DOODQOQO, OODDQOQO);
}
public void OOCDQOOQDQ(MarkerScript markerScript){

OCCCCCQQQD = markerScript.transform;



List<GameObject> tmp = new List<GameObject>();
for(int i=0;i<OCCCCCQQQDs.Length;i++){
if(OCCCCCQQQDs[i] != markerScript.gameObject)tmp.Add(OCCCCCQQQDs[i]);
}




tmp.Add(markerScript.gameObject);
OCCCCCQQQDs = tmp.ToArray();
OCCCCCQQQD = markerScript.transform;

ODQQOCODOC.OQCDCODODD(OCCCCCQQQD, OCCCCCQQQDs, markerScript.OQCQDDCQDQ, markerScript.OQDQOQQQOC, OCQDOCQQOC, out markerScript.OCCCCCQQQDs, out markerScript.trperc, OCCCCCQQQDs);

ODCDCODQOC = -1;
}
public void OOQCQDQCOC(MarkerScript markerScript){
if(markerScript.OQDQOQQQOC != markerScript.ODOOQQOO || markerScript.OQDQOQQQOC != markerScript.ODOOQQOO){
ODQQOCODOC.OQCDCODODD(OCCCCCQQQD, OCCCCCQQQDs, markerScript.OQCQDDCQDQ, markerScript.OQDQOQQQOC, OCQDOCQQOC, out markerScript.OCCCCCQQQDs, out markerScript.trperc, OCCCCCQQQDs);
markerScript.ODQDOQOO = markerScript.OQCQDDCQDQ;
markerScript.ODOOQQOO = markerScript.OQDQOQQQOC;
}
if(OQCQDDCCCC.autoUpdate) OQQQODQDDQ(OQCQDDCCCC.geoResolution, false, false);
}
public void ResetMaterials(MarkerScript markerScript){
if(ODQQOCODOC != null)ODQQOCODOC.OQCDCODODD(OCCCCCQQQD, OCCCCCQQQDs, markerScript.OQCQDDCQDQ, markerScript.OQDQOQQQOC, OCQDOCQQOC, out markerScript.OCCCCCQQQDs, out markerScript.trperc, OCCCCCQQQDs);
}
public void OQCDDDCOQO(MarkerScript markerScript){
if(markerScript.OQDQOQQQOC != markerScript.ODOOQQOO){
ODQQOCODOC.OQCDCODODD(OCCCCCQQQD, OCCCCCQQQDs, markerScript.OQCQDDCQDQ, markerScript.OQDQOQQQOC, OCQDOCQQOC, out markerScript.OCCCCCQQQDs, out markerScript.trperc, OCCCCCQQQDs);
markerScript.ODOOQQOO = markerScript.OQDQOQQQOC;
}
OQQQODQDDQ(OQCQDDCCCC.geoResolution, false, false);
}
private void OQDQOCDCCO(string ctrl, MarkerScript markerScript){
int i = 0;
foreach(Transform tr in markerScript.OCCCCCQQQDs){
MarkerScript wsScript = (MarkerScript) tr.GetComponent<MarkerScript>();
if(ctrl == "rs") wsScript.LeftSurrounding(markerScript.rs - markerScript.ODOQQOOO, markerScript.trperc[i]);
else if(ctrl == "ls") wsScript.RightSurrounding(markerScript.ls - markerScript.DODOQQOO, markerScript.trperc[i]);
else if(ctrl == "ri") wsScript.LeftIndent(markerScript.ri - markerScript.OOQOQQOO, markerScript.trperc[i]);
else if(ctrl == "li") wsScript.RightIndent(markerScript.li - markerScript.ODODQQOO, markerScript.trperc[i]);
else if(ctrl == "rt") wsScript.LeftTilting(markerScript.rt - markerScript.ODDQODOO, markerScript.trperc[i]);
else if(ctrl == "lt") wsScript.RightTilting(markerScript.lt - markerScript.ODDOQOQQ, markerScript.trperc[i]);
else if(ctrl == "floorDepth") wsScript.FloorDepth(markerScript.floorDepth - markerScript.oldFloorDepth, markerScript.trperc[i]);
i++;
}
}
public void OODQDODOCC(){
if(markers > 1) OQQQODQDDQ(OQCQDDCCCC.geoResolution, false, false);
}
public void OQDQQQDQDQ(Transform tr, List<ODODDQQO> arr, String[] DOODQOQO, String[] OODDQOQO){
version = "2.5.5";
ODCQCOODCC = (GUISkin)Resources.Load("ER3DSkin", typeof(GUISkin));


OQQOQQODDQ = (Texture2D)Resources.Load("ER3DLogo", typeof(Texture2D));
if(RoadObjectScript.objectStrings == null){
RoadObjectScript.objectStrings = new string[3];
RoadObjectScript.objectStrings[0] = "Road Object"; RoadObjectScript.objectStrings[1]="River Object";RoadObjectScript.objectStrings[2]="Procedural Mesh Object";
}
obj = tr;
ODQQOCODOC = new OQQCCCOQCQ();
OQCQDDCCCC = obj.GetComponent<RoadObjectScript>();
foreach(Transform child in obj){
if(child.name == "Markers") OCQDOCQQOC = child;
}
RoadObjectScript[] rscrpts = (RoadObjectScript[])FindObjectsOfType(typeof(RoadObjectScript));
OQQCCCOQCQ.terrainList.Clear();
Terrain[] terrains = (Terrain[])FindObjectsOfType(typeof(Terrain));
foreach(Terrain terrain in terrains) {
Terrains t = new Terrains();
t.terrain = terrain;
if(!terrain.gameObject.GetComponent<EasyRoads3DTerrainID>()){
EasyRoads3DTerrainID terrainscript = (EasyRoads3DTerrainID)terrain.gameObject.AddComponent<EasyRoads3DTerrainID>();
string id = UnityEngine.Random.Range(100000000,999999999).ToString();
terrainscript.terrainid = id;
t.id = id;
}else{
t.id = terrain.gameObject.GetComponent<EasyRoads3DTerrainID>().terrainid;
}
ODQQOCODOC.ODCOQODQQQ(t);
}
ODODOQDODD.ODCOQODQQQ();
if(roadMaterialEdit == null){
roadMaterialEdit = (Material)Resources.Load("materials/roadMaterialEdit", typeof(Material));
}
if(objectType == 0 && GameObject.Find(gameObject.name + "/road") == null){
GameObject road = new GameObject("road");
road.transform.parent = transform;
}

ODQQOCODOC.OCDDQQQDCO(obj, OODDOCDODO, OQCQDDCCCC.roadWidth, surfaceOpacity, out OQOCOOQOOO, out indent, applyAnimation, waveSize, waveHeight);
ODQQOCODOC.OQDQODQQDC = OQDQODQQDC;
ODQQOCODOC.OODOCOQQQQ = OODOCOQQQQ;
ODQQOCODOC.OdQODQOD = OdQODQOD + 1;
ODQQOCODOC.OOQQQDOD = OOQQQDOD;
ODQQOCODOC.OOQQQDODOffset = OOQQQDODOffset;
ODQQOCODOC.OOQQQDODLength = OOQQQDODLength;
ODQQOCODOC.objectType = objectType;
ODQQOCODOC.snapY = snapY;
ODQQOCODOC.terrainRendered = OCOOQDQCQC;
ODQQOCODOC.handleVegetation = handleVegetation;
ODQQOCODOC.raise = raise;
ODQQOCODOC.roadResolution = roadResolution;
ODQQOCODOC.multipleTerrains = multipleTerrains;
ODQQOCODOC.editRestore = editRestore;
ODQQOCODOC.roadMaterialEdit = roadMaterialEdit;
ODQQOCODOC.renderRoad = renderRoad;
ODQQOCODOC.rscrpts = rscrpts.Length;
ODQQOCODOC.blendFlag = blendFlag; 
ODQQOCODOC.startBlendDistance = startBlendDistance;
ODQQOCODOC.endBlendDistance = endBlendDistance;

ODQQOCODOC.iOS = iOS;

if(backupLocation == 0)OQOCDDCOQO.backupFolder = "/EasyRoads3D";
else OQOCDDCOQO.backupFolder =  OQOCDDCOQO.extensionPath + "/Backups";

ODODQOQO = ODQQOCODOC.ODQOQCDCQC();
ODODQOQOInt = ODQQOCODOC.OQOQODOQQO();


if(OCOOQDQCQC){




doRestore = true;
}


OOCDOCOCQC();

if(arr != null || ODODQOOQ == null) OODDDQDCDC(arr, DOODQOQO, OODDQOQO);


if(doRestore) return;
}
public void UpdateBackupFolder(){
}
public void OQCOOOQOOD(){
if(!ODODDDOO || objectType == 2){
if(OOCCCDOOCQ != null){
for(int i = 0; i < OOCCCDOOCQ.Length; i++){
OOCCCDOOCQ[i] = false;
OCCCDQDOQC[i] = false;
}
}
}
}

public void OCQQDDDCDO(Vector3 pos){


if(!displayRoad){
displayRoad = true;
ODQQOCODOC.OQQCODDOQO(displayRoad, OCQDOCQQOC);
}
pos.y += OQCQDDCCCC.raiseMarkers;
if(forceY && ODOQDQOO != null){
float dist = Vector3.Distance(pos, ODOQDQOO.transform.position);
pos.y = ODOQDQOO.transform.position.y + (yChange * (dist / 100f));
}else if(forceY && markers == 0) lastY = pos.y;
GameObject go = null;
if(ODOQDQOO != null) go = (GameObject)Instantiate(ODOQDQOO);
else go = (GameObject)Instantiate(Resources.Load("marker", typeof(GameObject)));
Transform newnode = go.transform;
newnode.position = pos;
newnode.parent = OCQDOCQQOC;
markers++;
string n;
if(markers < 10) n = "Marker000" + markers.ToString();
else if (markers < 100) n = "Marker00" + markers.ToString();
else n = "Marker0" + markers.ToString();
newnode.gameObject.name = n;
MarkerScript scr = newnode.GetComponent<MarkerScript>();
scr.OQOCOOQOOO = false;
scr.objectScript = obj.GetComponent<RoadObjectScript>();
if(ODOQDQOO == null){
scr.waterLevel = OQCQDDCCCC.waterLevel;
scr.floorDepth = OQCQDDCCCC.floorDepth;
scr.ri = OQCQDDCCCC.indent;
scr.li = OQCQDDCCCC.indent;
scr.rs = OQCQDDCCCC.surrounding;
scr.ls = OQCQDDCCCC.surrounding;
scr.tension = 0.5f;
if(objectType == 1){

pos.y -= waterLevel;
newnode.position = pos;
}
}
if(objectType == 2){
#if UNITY_3_5
if(scr.surface != null)scr.surface.gameObject.active = false;
#else
if(scr.surface != null)scr.surface.gameObject.SetActive(false);
#endif
}
ODOQDQOO = newnode.gameObject;
if(markers > 1){
OQQQODQDDQ(OQCQDDCCCC.geoResolution, false, false);
if(materialType == 0){

ODQQOCODOC.OQOCCQOQDQ(materialType);

}
}
}
public void OQQQODQDDQ(float geo, bool renderMode, bool camMode){
ODQQOCODOC.OOOQODOQQQ.Clear();
int ii = 0;
OCCOOOOCOO k;
foreach(Transform child  in obj)
{
if(child.name == "Markers"){
foreach(Transform marker   in child) {
MarkerScript markerScript = marker.GetComponent<MarkerScript>();
markerScript.objectScript = obj.GetComponent<RoadObjectScript>();
if(!markerScript.OQOCOOQOOO) markerScript.OQOCOOQOOO = ODQQOCODOC.OODOCQOQCO(marker);
k  = new OCCOOOOCOO();
k.position = marker.position;
k.num = ODQQOCODOC.OOOQODOQQQ.Count;
k.object1 = marker;
k.object2 = markerScript.surface;
k.tension = markerScript.tension;
k.ri = markerScript.ri;
if(k.ri < 1)k.ri = 1f;
k.li =markerScript.li;
if(k.li < 1)k.li = 1f;
k.rt = markerScript.rt;
k.lt = markerScript.lt;
k.rs = markerScript.rs;
if(k.rs < 1)k.rs = 1f;
k.OCDQQQODOO = markerScript.rs;
k.ls = markerScript.ls;
if(k.ls < 1)k.ls = 1f;
k.OCDQQQDOQC = markerScript.ls;
k.renderFlag = markerScript.bridgeObject;
k.OCOCQQQQDD = markerScript.distHeights;
k.newSegment = markerScript.newSegment;
k.tunnelFlag = markerScript.tunnelFlag;
k.floorDepth = markerScript.floorDepth;
k.waterLevel = waterLevel;
k.lockWaterLevel = markerScript.lockWaterLevel;
k.sharpCorner = markerScript.sharpCorner;
k.OOQOCCCQQO = ODQQOCODOC;
markerScript.markerNum = ii;
markerScript.distance = "-1";
markerScript.OCDODQCCCC = "-1";
ODQQOCODOC.OOOQODOQQQ.Add(k);
ii++;
}
}
}
distance = "-1";

ODQQOCODOC.OQQCOOODCO = OQCQDDCCCC.roadWidth;

ODQQOCODOC.ODDOQOCDOD(geo, obj, OQCQDDCCCC.OOQDOOQQ, renderMode, camMode, objectType);
if(ODQQOCODOC.leftVecs.Count > 0){
leftVecs = ODQQOCODOC.leftVecs.ToArray();
rightVecs = ODQQOCODOC.rightVecs.ToArray();
}
}
public void StartCam(){

OQQQODQDDQ(0.5f, false, true);

}
public void OOCDOCOCQC(){
int i = 0;
foreach(Transform child  in obj)
{
if(child.name == "Markers"){
i = 1;
string n;
foreach(Transform marker in child) {
if(i < 10) n = "Marker000" + i.ToString();
else if (i < 100) n = "Marker00" + i.ToString();
else n = "Marker0" + i.ToString();
marker.name = n;
ODOQDQOO = marker.gameObject;
i++;
}
}
}
markers = i - 1;

OQQQODQDDQ(OQCQDDCCCC.geoResolution, false, false);
}
public List<Transform> RebuildObjs(){
RoadObjectScript[] scripts = (RoadObjectScript[])FindObjectsOfType(typeof(RoadObjectScript));
List<Transform> rObj = new List<Transform>();
foreach (RoadObjectScript script in scripts) {
if(script.transform != transform) rObj.Add(script.transform);
}
return rObj;
}
public void RestoreTerrain1(){

OQQQODQDDQ(OQCQDDCCCC.geoResolution, false, false);
if(ODQQOCODOC != null) ODQQOCODOC.OCQOCDCCOO();
ODODDDOO = false;
}
public void OOOQDQDQDC(){
ODQQOCODOC.OOOQDQDQDC(OQCQDDCCCC.applySplatmap, OQCQDDCCCC.splatmapSmoothLevel, OQCQDDCCCC.renderRoad, OQCQDDCCCC.tuw, OQCQDDCCCC.roadResolution, OQCQDDCCCC.raise, OQCQDDCCCC.opacity, OQCQDDCCCC.expand, OQCQDDCCCC.offsetX, OQCQDDCCCC.offsetY, OQCQDDCCCC.beveledRoad, OQCQDDCCCC.splatmapLayer, OQCQDDCCCC.OdQODQOD, OOQQQDOD, OOQQQDODOffset, OOQQQDODLength);
}
public void OOOQDCCQDD(){
ODQQOCODOC.OOOQDCCQDD(OQCQDDCCCC.renderRoad, OQCQDDCCCC.tuw, OQCQDDCCCC.roadResolution, OQCQDDCCCC.raise, OQCQDDCCCC.beveledRoad, OQCQDDCCCC.OdQODQOD, OOQQQDOD, OOQQQDODOffset, OOQQQDODLength);
}
public void OOQQDOOOCD(Vector3 pos, bool doInsert){


if(!displayRoad){
displayRoad = true;
ODQQOCODOC.OQQCODDOQO(displayRoad, OCQDOCQQOC);
}

int first = -1;
int second = -1;
float dist1 = 10000;
float dist2 = 10000;
Vector3 newpos = pos;
OCCOOOOCOO k;
OCCOOOOCOO k1 = (OCCOOOOCOO)ODQQOCODOC.OOOQODOQQQ[0];
OCCOOOOCOO k2 = (OCCOOOOCOO)ODQQOCODOC.OOOQODOQQQ[1];

ODQQOCODOC.ODCCOODOCO(pos, out first, out second, out dist1, out dist2, out k1, out k2, out newpos);
pos = newpos;
if(doInsert && first >= 0 && second >= 0){
if(OQCQDDCCCC.OOQDOOQQ && second == ODQQOCODOC.OOOQODOQQQ.Count - 1){
OCQQDDDCDO(pos);
}else{
k = (OCCOOOOCOO)ODQQOCODOC.OOOQODOQQQ[second];
string name = k.object1.name;
string n;
int j = second + 2;
for(int i = second; i < ODQQOCODOC.OOOQODOQQQ.Count - 1; i++){
k = (OCCOOOOCOO)ODQQOCODOC.OOOQODOQQQ[i];
if(j < 10) n = "Marker000" + j.ToString();
else if (j < 100) n = "Marker00" + j.ToString();
else n = "Marker0" + j.ToString();
k.object1.name = n;
j++;
}
k = (OCCOOOOCOO)ODQQOCODOC.OOOQODOQQQ[first];
Transform newnode = (Transform)Instantiate(k.object1.transform, pos, k.object1.rotation);
newnode.gameObject.name = name;
newnode.parent = OCQDOCQQOC;
MarkerScript scr = newnode.GetComponent<MarkerScript>();
scr.OQOCOOQOOO = false;
float	totalDist = dist1 + dist2;
float perc1 = dist1 / totalDist;
float paramDif = k1.ri - k2.ri;
scr.ri = k1.ri - (paramDif * perc1);
paramDif = k1.li - k2.li;
scr.li = k1.li - (paramDif * perc1);
paramDif = k1.rt - k2.rt;
scr.rt = k1.rt - (paramDif * perc1);
paramDif = k1.lt - k2.lt;
scr.lt = k1.lt - (paramDif * perc1);
paramDif = k1.rs - k2.rs;
scr.rs = k1.rs - (paramDif * perc1);
paramDif = k1.ls - k2.ls;
scr.ls = k1.ls - (paramDif * perc1);
OQQQODQDDQ(OQCQDDCCCC.geoResolution, false, false);
if(materialType == 0)ODQQOCODOC.OQOCCQOQDQ(materialType);
#if UNITY_3_5
if(objectType == 2) scr.surface.gameObject.active = false;
#else
if(objectType == 2) scr.surface.gameObject.SetActive(false);
#endif
}
}
OOCDOCOCQC();
}
public void OOCCOCQODD(){

DestroyImmediate(OQCQDDCCCC.OCCCCCQQQD.gameObject);
OCCCCCQQQD = null;
OOCDOCOCQC();
}
public void OQDOCDOCOQ(){

ODQQOCODOC.OCQCDCDOOQ(12);

}

public List<SideObjectParams> OQOODOQDCO(){

List<SideObjectParams> param = new List<SideObjectParams>();
SideObjectParams sop;
foreach(Transform child in obj){
if(child.name == "Markers"){
foreach(Transform marker in child){
MarkerScript markerScript = marker.GetComponent<MarkerScript>();
sop  = new SideObjectParams();
sop.ODDGDOOO = markerScript.ODDGDOOO;
sop.ODDQOODO = markerScript.ODDQOODO;
sop.ODDQOOO = markerScript.ODDQOOO;
param.Add(sop);
}
}
}
return param;

}
public void OQQQQQCCDD(){

List<string> arrNames = new List<string>();
List<int> arrInts = new List<int>();
List<string> arrIDs = new List<string>();

for(int i=0;i<ODODOQQO.Length;i++){
if(ODODQQOD[i] == true){
arrNames.Add(ODODQOOQ[i]);
arrIDs.Add(ODODOQQO[i]);
arrInts.Add(i);
}
}
ODODDQOO = arrNames.ToArray();
OOQQQOQO = arrInts.ToArray();

}
public void OODDDQDCDC(List<ODODDQQO> arr, String[] DOODQOQO, String[] OODDQOQO){




bool saveSOs = false;
ODODOQQO = DOODQOQO;
ODODQOOQ = OODDQOQO;






List<MarkerScript> markerArray = new List<MarkerScript>();
if(obj == null)OQDQQQDQDQ(transform, null, null, null);
foreach(Transform child  in obj) {
if(child.name == "Markers"){
foreach(Transform marker  in child) {
MarkerScript markerScript = marker.GetComponent<MarkerScript>();
markerScript.OQODQQDO.Clear();
markerScript.ODOQQQDO.Clear();
markerScript.OQQODQQOO.Clear();
markerScript.ODDOQQOO.Clear();
markerArray.Add(markerScript);
}
}
}
mSc = markerArray.ToArray();





List<bool> arBools = new List<bool>();



int counter1 = 0;
int counter2 = 0;

if(ODQQQQQO != null){

if(arr.Count == 0) return;



for(int i = 0; i < ODODOQQO.Length; i++){
ODODDQQO so = (ODODDQQO)arr[i];

for(int j = 0; j < ODQQQQQO.Length; j++){
if(ODODOQQO[i] == ODQQQQQO[j]){
counter1++;


if(ODODQQOD.Length > j ) arBools.Add(ODODQQOD[j]);
else arBools.Add(false);

foreach(MarkerScript scr  in mSc) {


int l = -1;
for(int k = 0; k < scr.ODDOOQDO.Length; k++){
if(so.id == scr.ODDOOQDO[k]){
l = k;
break;
}
}
if(l >= 0){
scr.OQODQQDO.Add(scr.ODDOOQDO[l]);
scr.ODOQQQDO.Add(scr.ODDGDOOO[l]);
scr.OQQODQQOO.Add(scr.ODDQOOO[l]);

if(so.sidewaysDistanceUpdate == 0 || (so.sidewaysDistanceUpdate == 2 && (float)scr.ODDQOODO[l] != so.oldSidwaysDistance)){
scr.ODDOQQOO.Add(scr.ODDQOODO[l]);

}else{
scr.ODDOQQOO.Add(so.splinePosition);

}




}else{
scr.OQODQQDO.Add(so.id);
scr.ODOQQQDO.Add(so.markerActive);
scr.OQQODQQOO.Add(true);
scr.ODDOQQOO.Add(so.splinePosition);
}

}
}
}
if(so.sidewaysDistanceUpdate != 0){



}
saveSOs = false;
}
}


for(int i = 0; i < ODODOQQO.Length; i++){
ODODDQQO so = (ODODDQQO)arr[i];
bool flag = false;
for(int j = 0; j < ODQQQQQO.Length; j++){

if(ODODOQQO[i] == ODQQQQQO[j])flag = true;
}
if(!flag){
counter2++;

arBools.Add(false);

foreach(MarkerScript scr  in mSc) {
scr.OQODQQDO.Add(so.id);
scr.ODOQQQDO.Add(so.markerActive);
scr.OQQODQQOO.Add(true);
scr.ODDOQQOO.Add(so.splinePosition);
}

}
}

ODODQQOD = arBools.ToArray();


ODQQQQQO = new String[ODODOQQO.Length];
ODODOQQO.CopyTo(ODQQQQQO,0);





List<int> arInt= new List<int>();
for(int i = 0; i < ODODQQOD.Length; i++){
if(ODODQQOD[i]) arInt.Add(i);
}
OOQQQOQO  = arInt.ToArray();


foreach(MarkerScript scr  in mSc) {
scr.ODDOOQDO = scr.OQODQQDO.ToArray();
scr.ODDGDOOO = scr.ODOQQQDO.ToArray();
scr.ODDQOOO = scr.OQQODQQOO.ToArray();
scr.ODDQOODO = scr.ODDOQQOO.ToArray();

}
if(saveSOs){

}




}
public bool CheckWaterHeights(){
if(ODODOQDODD.terrain == null) return false;
bool flag = true;

float y = ODODOQDODD.terrain.transform.position.y;
foreach(Transform child  in obj) {
if(child.name == "Markers"){
foreach(Transform marker  in child) {

if(marker.position.y - y <= 0.1f) flag = false;
}
}
}
return flag;
}
}
