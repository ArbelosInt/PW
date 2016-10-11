using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using EasyRoads3D;
using EasyRoads3DEditor;
public class ProceduralObjectsEditor : EditorWindow
{

public static ProceduralObjectsEditor instance;
public OQCOCODQDD so_editor;
public int sideObject;
private ODODDQQO so;

private GameObject so_go;

string[] traceStrings;

public ProceduralObjectsEditor() {
instance = this;
}
void OnDestroy(){
OQOODCDCDO.OnDestroy1();
instance = null;
}
public void DisplayNodes (int index, ODODDQQO soi, GameObject sso_go)

{
so_go = sso_go;
List<Vector2> tmpNodes = new List<Vector2>();
if(soi != null) tmpNodes.AddRange(soi.nodeList);

if(so_go != null && tmpNodes.Count == 0){

List<Vector2> arr = OQCOCODQDD.OQCCQCOOQC(2, so_go, OQOODCDCDO.traceOffset);
if(arr != null){
if(arr.Count > 1){
tmpNodes = arr;
}
}
}
bool clamped = false;
so = soi;
sideObject = index;
if (so_editor == null){
try{
so_editor = new OQCOCODQDD(position, tmpNodes, clamped);
}catch{
}
}



if(so_editor.OCOOCCODQQ.Count > 0){
if((Vector2)so_editor.OCOOCCODQQ[0] == (Vector2)so_editor.OCOOCCODQQ[so_editor.OCOOCCODQQ.Count - 1]){

so_editor.closed = true;
so_editor.OCOOCCODQQ.RemoveAt(so_editor.OCOOCCODQQ.Count - 1);
}
}
if(tmpNodes.Count != 0){
Rect rect = new Rect(stageSelectionGridWidth, 0, Screen.width - stageSelectionGridWidth, Screen.height);
so_editor.FrameSelected(rect);
}
OQOODCDCDO.OQOOQQCODC(index, soi, sso_go, so_editor);
return;
}
void OnGUI ()
{
Rect rect = new Rect(stageSelectionGridWidth, 0, Screen.width - stageSelectionGridWidth, Screen.height);
EditorGUILayout.BeginHorizontal();
GUILayout.Space(210);
GUILayout.Label(new GUIContent("Hit [r] to center the editor, hit [z] to zoom in on the nodes, click drag to move the canvas, Scrollwheel (or [shift] click drag) zoom in / out", ""), GUILayout.Width(800) );
EditorGUILayout.EndHorizontal();
GUILayout.Space(-15);
OQOODCDCDO.OQQOQDOQOD(rect);
DoGUI ();
so_editor.OnGUI(rect);
}
float stageSelectionGridWidth = 200;
void DoGUI ()
{

EditorGUILayout.BeginHorizontal();
GUILayout.Space(60);
if(GUILayout.Button ("Apply", GUILayout.Width(65))){
OQOODCDCDO.ODCODCOOOC();
instance.Close();
}
if(GUILayout.Button ("Close", GUILayout.Width(65))){
instance.Close();
}
EditorGUILayout.EndHorizontal();
GUILayout.Space(5);
if(so_editor.isChanged == false) GUI.enabled = false;
EditorGUILayout.BeginHorizontal();
GUILayout.Space(60);
if(GUILayout.Button ("Update Scene", GUILayout.Width(135))){

so.nodeList.Clear();
if(so_editor.closed) so_editor.OCOOCCODQQ.Add(so_editor.OCOOCCODQQ[0]);
so.nodeList.AddRange(so_editor.OCOOCCODQQ);
so_editor.isChanged = false;
OCDOQOCDCC.OCOQOOCCQD(OCDOQOCDCC.selectedObject);
OCDOQOCDCC.ODQDDQDOQC();

List<ODODDQQO> arr = ODCDDQQDDC.OCDOQDQCOQ(false);
RoadObjectScript.ODODOQQO = ODCDDQQDDC.OCQCOCOODC(arr);
RoadObjectScript[] scripts = (RoadObjectScript[])FindObjectsOfType(typeof(RoadObjectScript));
foreach(RoadObjectScript scr in scripts){

if(scr.ODQQOCODOC == null) {
List<ODODDQQO> arr1  = ODCDDQQDDC.OCDOQDQCOQ(false);
scr.OODQOODDCO(arr1, ODCDDQQDDC.OCQCOCOODC(arr1), ODCDDQQDDC.ODQQOQCDOC(arr1));
}
scr.OODDDQDCDC(arr, ODCDDQQDDC.OCQCOCOODC(arr), ODCDDQQDDC.ODQQOQCDOC(arr));
if(scr.OCOOQDQCQC == true || scr.objectType == 2){
GameObject go = GameObject.Find(scr.gameObject.name+"/Side Objects/"+so.name);


if(go != null){
OCODOCOQQD.OOCQCCDOQQ((sideObjectScript)go.GetComponent(typeof(sideObjectScript)), sideObject, scr, go.transform);
}
}
}
}
EditorGUILayout.EndHorizontal();
GUI.enabled = true;
if (GUI.changed)
{
so_editor.isChanged = true;

}
Handles.color = Color.black;
Handles.DrawLine(new Vector2 (stageSelectionGridWidth,0), new Vector2 (stageSelectionGridWidth,Screen.height));

Handles.DrawLine(new Vector2 (stageSelectionGridWidth - 1,0), new Vector2 (stageSelectionGridWidth - 1,Screen.height));

}

}
