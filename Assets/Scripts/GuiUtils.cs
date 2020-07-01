using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// GuiUtils
/// Low-level GUI drawing utilities

public class GuiUtils : MonoBehaviour
{
	//===================================
	//===================================
	//		MODULE VARIABLES
	//===================================
	//===================================

	//===================================
	//===================================
	//		INITIALIZATION
	//===================================
	//===================================

	Texture2D bgTexture;
	GUIStyle textureStyle;
	void Start() 
	{	
		bgTexture = new Texture2D(1, 1);
		bgTexture.SetPixel(0, 0, Color.white);

		textureStyle = new GUIStyle {
			normal = new GUIStyleState {
				background = bgTexture
			}
		};
	}

	//===================================
	//===================================
	//		UTILS
	//===================================
	//===================================

	
	public void DrawRect(Rect position, Color color)
	{
		/*
		 * Some of this code is based on the code snippet provided by user Cameron860 in the Unity3D Forums here:
		 * https://forum.unity3d.com/threads/draw-a-simple-rectangle-filled-with-a-color.116348/
		 * 
		 * */

		Texture2D bgTexture = Texture2D.whiteTexture;
		bgTexture.SetPixel(0, 0, color);
		bgTexture.wrapMode = TextureWrapMode.Repeat;
		bgTexture.Apply();

		// Save the old background colour
		Color oldBackgroundColour = GUI.backgroundColor;

		// Set the new background colour
		GUI.backgroundColor = color;


		GUI.Box(position, GUIContent.none, textureStyle);

		// Reset the background colour
		GUI.backgroundColor = oldBackgroundColour;
	}

	
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
	
	
	//===================================
	// GUI ITEMS 
	//===================================
	
	// prefab gui components
	public GameObject uiPanel;
	public GameObject uiSubPanel;
	public GameObject uiRect;
	public GameObject uiText;
	public GameObject uiRawImage;
	public GameObject uiButton;
	public GameObject uiButtonSeeThru;
	public GameObject uiImageButton;

	// create
	
	public GameObject CreatePanel(GameObject parentObj, Color imageColor)
	{
		GameObject obj = (GameObject)Instantiate(uiPanel);
		obj.GetComponent<RectTransform>().SetParent(parentObj.GetComponent<RectTransform>(), false);
		obj.GetComponent<Image>().color = imageColor;
		return obj;
	}
	
	
	public GameObject CreateImage(GameObject parentObj, Texture2D texture, Color imageColor)
	{
		GameObject obj = (GameObject)Instantiate(uiRawImage);
		obj.GetComponent<RectTransform>().SetParent(parentObj.GetComponent<RectTransform>(), false);
		obj.GetComponent<RawImage>().texture = texture;
		obj.GetComponent<RawImage>().color = imageColor;
		return obj;
	}
	
	public GameObject CreateRect(GameObject parentObj, Color rectColor)
	{
		GameObject obj = (GameObject)Instantiate(uiRect);
		obj.GetComponent<RectTransform>().SetParent(parentObj.GetComponent<RectTransform>(), false);
		obj.GetComponent<Image>().color = rectColor;
		return obj;
	}
	
	public GameObject CreateText(GameObject parentObj, string text, Color textColor, FontStyle font, TextAnchor alignment = TextAnchor.MiddleCenter)
	{
		GameObject obj = (GameObject)Instantiate(uiText);
		obj.GetComponent<RectTransform>().SetParent(parentObj.GetComponent<RectTransform>(), false);
		Text t = obj.GetComponent<Text>();		
		t.text = text;
		t.color =  textColor;
		t.fontStyle = font;
		t.alignment = alignment;
		return obj;
	}
	
	public GameObject CreateButton(GameObject parentObj, string text, Color textColor, FontStyle font)
	{
		GameObject obj = (GameObject)Instantiate(uiButton);
		obj.GetComponent<RectTransform>().SetParent(parentObj.GetComponent<RectTransform>(), false);
		Text t = obj.GetComponent<RectTransform>().Find("Text").GetComponent<Text>();
		t.text = text;
		t.color =  textColor;
		t.fontStyle = font;
		return obj;
	}
	

	public GameObject CreateSeeThruButton(GameObject parentObj, string text, Color textColor, FontStyle font)
	{
		GameObject obj = (GameObject)Instantiate(uiButtonSeeThru);
		obj.GetComponent<RectTransform>().SetParent(parentObj.GetComponent<RectTransform>(), false);
		Text t = obj.GetComponent<RectTransform>().Find("Text").GetComponent<Text>();
		t.text = text;
		t.color =  textColor;
		t.fontStyle = font;
		return obj;
	}
	


	
	// set offsets

	public void SetItemOffsets(GameObject gameObj, float x, float y, float width, float height)
	{
		gameObj.GetComponent<RectTransform>().offsetMin = new Vector2(x, Screen.height - (y + height));
		gameObj.GetComponent<RectTransform>().offsetMax = new Vector2(-Screen.width + (x + width), -y + 1);
	}
	

	public void SetTextOffsets(GameObject gameObj, float x, float y, float width, float height, int fontSize)
	{
		gameObj.GetComponent<RectTransform>().offsetMin = new Vector2(x, Screen.height - (y + height));
		gameObj.GetComponent<RectTransform>().offsetMax = new Vector2(-Screen.width + (x + width), -y + 1);
		gameObj.GetComponent<Text>().fontSize = fontSize;
	}

	
	public void SetButtonOffsets(GameObject gameObj, float x, float y, float width, float height, int fontSize)
	{
		gameObj.GetComponent<RectTransform>().offsetMin = new Vector2(x, Screen.height - (y + height));
		gameObj.GetComponent<RectTransform>().offsetMax = new Vector2(-Screen.width + (x + width), -y + 1);
		gameObj.GetComponent<RectTransform>().Find("Text").GetComponent<Text>().fontSize = fontSize;
	}

	
}

















