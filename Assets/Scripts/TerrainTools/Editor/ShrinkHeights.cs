using UnityEditor;
using UnityEngine;
 
internal class ShrinkHeights : ScriptableWizard
{
    private static TerrainData _terrainData;
    public float NewHeight = 100f;
 
    [MenuItem("Terrain/Shrink Heights")]
    public static void CreateWizard()
    {
        string buttonText = "Cancel";
        _terrainData = null;
 
        Terrain terrainObject = Selection.activeObject as Terrain ?? Terrain.activeTerrain;
 
        if (terrainObject)
        {
            _terrainData = terrainObject.terrainData;
            buttonText = "Shrink Heights";
        }
 
        DisplayWizard<ShrinkHeights>("Shrink Heights", buttonText);
    }
 
    private void OnWizardUpdate()
    {
        if (!_terrainData)
        {
            helpString = "No terrain found";
            return;
        }
 
        NewHeight = Mathf.Clamp(NewHeight, 0f, _terrainData.size.y);
        helpString = ("scale change is " + (NewHeight * 100f / _terrainData.size.y) + "%");
    }
 
    private void OnWizardCreate()
    {
        if (!_terrainData) return;
 
        Undo.RegisterUndo(_terrainData, "Shrink Heights");
 
        float[,] heights = _terrainData.GetHeights(0, 0, _terrainData.heightmapResolution, _terrainData.heightmapResolution);
 
        for (int y = 0; y < _terrainData.heightmapResolution; y++)
        {
            for (int x = 0; x < _terrainData.heightmapResolution; x++)
            {
                heights[y, x] = heights[y, x] * (NewHeight / _terrainData.size.y);
            }
        }
 
        _terrainData.SetHeights(0, 0, heights);
        _terrainData = null;
    }
}

