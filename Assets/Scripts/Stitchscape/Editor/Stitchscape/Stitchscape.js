// Stitchscape 1.5 ©2011 Starscene Software. All rights reserved. Redistribution without permission not allowed.

import UnityEngine.GUILayout;
import UnityEditor.EditorGUILayout;
import System.Collections.Generic;

enum Direction {Across, Down}

class Stitchscape extends ScriptableWizard {
	static var across : int;
	static var down : int;
	static var tWidth : int;
	static var tHeight : int;
	static var terrains : Object[];
	static var stitchWidth : int;
	static var message : String;
	static var terrainRes : int;
	static var lineTex : Texture2D;
	static var maxStitchWidth = 100;
	static var playError = false;
	static var gridPixelHeight = 28;
	static var gridPixelWidth = 121;
#if UNITY_3_4
	static var y = 92;
#else
	static var y = 84;
#endif
	
	@MenuItem ("Terrain/Stitch...")
	static function CreateWizard () {
		if (lineTex == null) {	// across/down etc. defined here, so closing and re-opening wizard doesn't reset vars
			across = down = tWidth = tHeight = 2;
			stitchWidth = 10;
			SetNumberOfTerrains();
			lineTex = EditorGUIUtility.whiteTexture;
		}
		message = "";
		playError = false;
		ScriptableWizard.DisplayWizard("Stitch Terrains", Stitchscape);
	}
	
	new function OnGUI () {
		if (Application.isPlaying) {
			playError = true;
		}
		if (playError) {	// Needs to continue showing this even if play mode is stopped
			Label("Stitchscape can't run in play mode");
			return;
		}

		BeginHorizontal(Width(220));
			BeginVertical();
				BeginHorizontal(Width(190));
					Label("Number of terrains across:");
					across = Mathf.Max(IntField(across, Width(30)), 1);
				EndHorizontal();
				BeginHorizontal(Width(190));
					Label("Number of terrains down:");
					down = Mathf.Max(IntField(down, Width(30)), 1);
				EndHorizontal();
			EndVertical();
			BeginVertical();
				Space(12);
				if (Button("Apply")) {
					tWidth = across;
					tHeight = down;
					SetNumberOfTerrains();
				}
			EndVertical();
		EndHorizontal();
		
		Space(7);
		
		BeginHorizontal();
			Space(15);
			if (Button("Autofill from scene", Width(gridPixelWidth * tWidth + 2)) ) {
				AutoFill();
			}
		EndHorizontal();
		Space(9);
		
		var counter = 0;
		for (h = 0; h < tHeight; h++) {
			BeginHorizontal();
				Space(20);
				for (w = 0; w < tWidth; w++) {
					terrains[counter] = ObjectField(terrains[counter++], Terrain, true, Width(112));
					Space(5);
				}
			EndHorizontal();
			Space(9);
		}
		DrawGrid(Color.black, 1, y);
		DrawGrid(Color.white, 0, y);
#if UNITY_3_4
		GUI.Label(Rect(5, y-4, 20, 20), "Z");
#else
		GUI.Label(Rect(2, y-4, 20, 20), "Z");
#endif
		GUI.Label(Rect(gridPixelWidth*tWidth + 10, y+2 + gridPixelHeight*tHeight, 20, 20), "X");
		GUI.color = Color.black;
		GUI.DrawTexture(Rect(7, y+12, 1, gridPixelHeight*tHeight - 2), lineTex);
		GUI.DrawTexture(Rect(7, y+10 + gridPixelHeight*tHeight, gridPixelWidth*tWidth, 1), lineTex);
		GUI.color = Color.white;
		
		Space(15);
		
		BeginHorizontal();
			if (terrains[0] != null) {
				maxStitchWidth = terrains[0].terrainData.heightmapWidth/2;
			}
			Label("Stitch width: " + stitchWidth, Width(90));
			stitchWidth = HorizontalSlider(stitchWidth, 2, maxStitchWidth);
		EndHorizontal();
		
		Space(8);

		Label(message);

		Space(1);
		
		BeginHorizontal();
			Space(10);
			if (Button("Clear")) {
				SetNumberOfTerrains();
			}
			if (Button("Stitch")) {
				StitchTerrains();
			}
			Space(10);
		EndHorizontal();
	}
	
	private static function AutoFill () {
		var sceneTerrains = FindObjectsOfType(Terrain);
		if (sceneTerrains.Length == 0) {
			message = "No terrains found";
			return;
		}
		
		var xPositions = new List.<float>();
		var zPositions = new List.<float>();
		var tPosition = sceneTerrains[0].transform.position;
		xPositions.Add(tPosition.x);
		zPositions.Add(tPosition.z);
		for (i = 0; i < sceneTerrains.Length; i++) {
			tPosition = sceneTerrains[i].transform.position;
			if (!ListContains(xPositions, tPosition.x)) {
				xPositions.Add(tPosition.x);
			}
			if (!ListContains(zPositions, tPosition.z)) {
				zPositions.Add(tPosition.z);
			}
		}
		if (xPositions.Count * zPositions.Count != sceneTerrains.Length) {
			message = "Unable to autofill. Terrains should line up closely in the form of a grid.";
			return;
		}
		
		xPositions.Sort();
		zPositions.Sort();
		zPositions.Reverse();
		across = tWidth = xPositions.Count;
		down = tHeight = zPositions.Count;
		terrains = new Object[tWidth * tHeight];
		var count = 0;
		for (z = 0; z < zPositions.Count; z++) {
			for (x = 0; x < xPositions.Count; x++) {
				for (i = 0; i < sceneTerrains.Length; i++) {
					tPosition = sceneTerrains[i].transform.position;
					if (Approx(tPosition.x, xPositions[x]) && Approx(tPosition.z, zPositions[z])) {
						terrains[count++] = sceneTerrains[i];
						break;
					}
				}				
			}
		}
	}
	
	private static function ListContains (list : List.<float>, pos : float) : boolean {
		for (i = 0; i < list.Count; i++) {
			if (Approx(pos, list[i])) {
				return true;
			}
		}
		return false;
	}

	private static function Approx (pos1 : float, pos2 : float) : boolean {
		if (pos1 >= pos2-1.0 && pos1 <= pos2+1.0) {
			return true;
		}
		return false;
	}
	
	private function DrawGrid (color : Color, offset : int, top : int) {
		GUI.color = color;
		for (i = 0; i < tHeight+1; i++) {
			GUI.DrawTexture(Rect(15 + offset, top + offset + gridPixelHeight*i, gridPixelWidth*tWidth, 1), lineTex);
		}
		for (i = 0; i < tWidth+1; i++) {
			GUI.DrawTexture(Rect(15 + offset + gridPixelWidth*i, top + offset, 1, gridPixelHeight*tHeight + 1), lineTex);		
		}
	}
	
	private static function SetNumberOfTerrains () {
		terrains = new Object[tWidth * tHeight];
		message = "";
	}

	private static function StitchTerrains () {
		for (t in terrains) {
			if (t == null) {
				message = "All terrain slots must have a terrain assigned";
				return;
			}
		}
	
		terrainRes = terrains[0].terrainData.heightmapWidth;
		if (terrains[0].terrainData.heightmapHeight != terrainRes) {
			message = "Heightmap width and height must be the same";
			return;
		}
		
		for (t in terrains) {
			if (t.terrainData.heightmapWidth != terrainRes || t.terrainData.heightmapHeight != terrainRes) {
				message = "All heightmaps must be the same resolution";
				return;
			}
		}
		
		for (t in terrains) {
			Undo.RegisterUndo(t.terrainData, "Stitch");
		}

		stitchWidth = Mathf.Clamp(stitchWidth, 1, (terrainRes-1)/2);
		var counter = 0;
		var total = tHeight*(tWidth-1) + (tHeight-1)*tWidth;
		
		if (tWidth == 1 && tHeight == 1) {
			BlendData (terrains[0].terrainData, terrains[0].terrainData, Direction.Across, true);
			BlendData (terrains[0].terrainData, terrains[0].terrainData, Direction.Down, true);
			message = "Terrain has been made repeatable with itself";
		}
		else {
			for (h = 0; h < tHeight; h++) {
				for (w = 0; w < tWidth-1; w++) {
					EditorUtility.DisplayProgressBar("Stitching...", "", Mathf.InverseLerp(0, total, ++counter));
					BlendData (terrains[h*tWidth + w].terrainData, terrains[h*tWidth + w + 1].terrainData, Direction.Across, false);
				}
			}
			for (h = 0; h < tHeight-1; h++) {
				for (w = 0; w < tWidth; w++) {
					EditorUtility.DisplayProgressBar("Stitching...", "", Mathf.InverseLerp(0, total, ++counter));
					BlendData (terrains[h*tWidth + w].terrainData, terrains[(h+1)*tWidth + w].terrainData, Direction.Down, false);
				}
			}
			message = "Terrains stitched successfully";
		}
		
		EditorUtility.ClearProgressBar();
	}
	
	private static function BlendData (terrain1 : TerrainData, terrain2 : TerrainData, thisDirection : Direction, singleTerrain : boolean) {
		var heightmapData = terrain1.GetHeights(0, 0, terrainRes, terrainRes);
		var heightmapData2 = terrain2.GetHeights(0, 0, terrainRes, terrainRes);
		var pos = terrainRes-1;
		
		if (thisDirection == Direction.Across) {
			for (i = 0; i < terrainRes; i++) {
				for (j = 1; j < stitchWidth; j++) {
					var mix = Mathf.Lerp(heightmapData[i, pos-j], heightmapData2[i, j], .5);
					if (j == 1) {
						heightmapData[i, pos] = mix;
						heightmapData2[i, 0] = mix;
					}
					var t = Mathf.SmoothStep(0.0, 1.0, Mathf.InverseLerp(1, stitchWidth-1, j));
					heightmapData[i, pos-j] = Mathf.Lerp(mix, heightmapData[i, pos-j], t);
					if (!singleTerrain) {
						heightmapData2[i, j]    = Mathf.Lerp(mix, heightmapData2[i, j], t);
					}
					else {
						heightmapData[i, j]    = Mathf.Lerp(mix, heightmapData2[i, j], t);
					}
				}
			}
			if (singleTerrain) {
				for (i = 0; i < terrainRes; i++) {
					heightmapData[i, 0] = heightmapData[i, pos];
				}
			}
		}
		else {
			for (i = 0; i < terrainRes; i++) {
				for (j = 1; j < stitchWidth; j++) {
					mix = Mathf.Lerp(heightmapData2[pos-j, i], heightmapData[j, i], .5);
					if (j == 1) {
						heightmapData2[pos, i] = mix;
						heightmapData[0, i] = mix;
					}
					t = Mathf.SmoothStep(0.0, 1.0, Mathf.InverseLerp(1, stitchWidth-1, j));
					if (!singleTerrain) {
						heightmapData2[pos-j, i] = Mathf.Lerp(mix, heightmapData2[pos-j, i], t);
					}
					else {
						heightmapData[pos-j, i] = Mathf.Lerp(mix, heightmapData2[pos-j, i], t);
					}
					heightmapData[j, i]      = Mathf.Lerp(mix, heightmapData[j, i], t);
				}
			}
			if (singleTerrain) {
				for (i = 0; i < terrainRes; i++) {
					heightmapData[pos, i] = heightmapData[0, i];
				}
			}
		}
		
		terrain1.SetHeights(0, 0, heightmapData);
		if (!singleTerrain) {
			terrain2.SetHeights(0, 0, heightmapData2);
		}
	}
}