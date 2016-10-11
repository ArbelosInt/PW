@CustomEditor(EasyRoads3DTerrainID)
class TerrainEditorScript extends Editor
{
function OnSceneGUI()
{
if(Event.current.shift && RoadObjectScript.OCODQODOQC != null) Selection.activeGameObject = RoadObjectScript.OCODQODOQC.gameObject;
else RoadObjectScript.OCODQODOQC = null;
}
}
