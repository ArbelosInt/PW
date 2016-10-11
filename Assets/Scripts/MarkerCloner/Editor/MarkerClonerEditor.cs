using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(MarkerCloner))]

public class MarkerClonerEditor : Editor 
{
    public override void OnInspectorGUI()
    {
		MarkerCloner myTarget = (MarkerCloner)target;
        
		GUILayout.Space(20);
		
		DrawDefaultInspector();
		
		GUILayout.Space(20);
		
		if (GUILayout.Button("PROCESS MARKERS")) {
			GameObject sourceSet = myTarget.sourceMarkerSet;
			GameObject destSet = myTarget.destMarkerSet;
			
			foreach (Transform srcTransform in sourceSet.transform)
			{
				switch (srcTransform.name) {
				case "Marker0001":
					Debug.Log("Got Marker0001");
					foreach (Transform destTransform in destSet.transform) {
						if (destTransform.name == "Marker0001") {
							destTransform.position = new Vector3(srcTransform.position.x, srcTransform.position.y, srcTransform.position.z);
						}
					}
					break;
				case "Marker0002":
					Debug.Log("Got Marker0002");
					foreach (Transform destTransform in destSet.transform) {
						if (destTransform.name == "Marker0002") {
							destTransform.position = new Vector3(srcTransform.position.x, srcTransform.position.y, srcTransform.position.z);
						}
					}
					break;
				case "Marker0003":
					Debug.Log("Got Marker0003");
					foreach (Transform destTransform in destSet.transform) {
						if (destTransform.name == "Marker0003") {
							destTransform.position = new Vector3(srcTransform.position.x, srcTransform.position.y, srcTransform.position.z);
						}
					}
					break;
				case "Marker0004":
					Debug.Log("Got Marker0004");
					foreach (Transform destTransform in destSet.transform) {
						if (destTransform.name == "Marker0004") {
							destTransform.position = new Vector3(srcTransform.position.x, srcTransform.position.y, srcTransform.position.z);
						}
					}
					break;
				case "Marker0005":
					Debug.Log("Got Marker0005");
					foreach (Transform destTransform in destSet.transform) {
						if (destTransform.name == "Marker0005") {
							destTransform.position = new Vector3(srcTransform.position.x, srcTransform.position.y, srcTransform.position.z);
						}
					}
					break;
				case "Marker0006":
					Debug.Log("Got Marker0006");
					foreach (Transform destTransform in destSet.transform) {
						if (destTransform.name == "Marker0006") {
							destTransform.position = new Vector3(srcTransform.position.x, srcTransform.position.y, srcTransform.position.z);
						}
					}
					break;
				case "Marker0007":
					Debug.Log("Got Marker0007");
					foreach (Transform destTransform in destSet.transform) {
						if (destTransform.name == "Marker0007") {
							destTransform.position = new Vector3(srcTransform.position.x, srcTransform.position.y, srcTransform.position.z);
						}
					}
					break;
				case "Marker0008":
					Debug.Log("Got Marker0008");
					foreach (Transform destTransform in destSet.transform) {
						if (destTransform.name == "Marker0008") {
							destTransform.position = new Vector3(srcTransform.position.x, srcTransform.position.y, srcTransform.position.z);
						}
					}
					break;
				case "Marker0009":
					Debug.Log("Got Marker0009");
					foreach (Transform destTransform in destSet.transform) {
						if (destTransform.name == "Marker0009") {
							destTransform.position = new Vector3(srcTransform.position.x, srcTransform.position.y, srcTransform.position.z);
						}
					}
					break;
				case "Marker0010":
					Debug.Log("Got Marker0010");
					foreach (Transform destTransform in destSet.transform) {
						if (destTransform.name == "Marker0010") {
							destTransform.position = new Vector3(srcTransform.position.x, srcTransform.position.y, srcTransform.position.z);
						}
					}
					break;
				case "Marker0011":
					Debug.Log("Got Marker0011");
					foreach (Transform destTransform in destSet.transform) {
						if (destTransform.name == "Marker0011") {
							destTransform.position = new Vector3(srcTransform.position.x, srcTransform.position.y, srcTransform.position.z);
						}
					}
					break;
				case "Marker0012":
					Debug.Log("Got Marker0012");
					foreach (Transform destTransform in destSet.transform) {
						if (destTransform.name == "Marker0012") {
							destTransform.position = new Vector3(srcTransform.position.x, srcTransform.position.y, srcTransform.position.z);
						}
					}
					break;
				case "Marker0013":
					Debug.Log("Got Marker0013");
					foreach (Transform destTransform in destSet.transform) {
						if (destTransform.name == "Marker0013") {
							destTransform.position = new Vector3(srcTransform.position.x, srcTransform.position.y, srcTransform.position.z);
						}
					}
					break;
				case "Marker0014":
					Debug.Log("Got Marker0014");
					foreach (Transform destTransform in destSet.transform) {
						if (destTransform.name == "Marker0014") {
							destTransform.position = new Vector3(srcTransform.position.x, srcTransform.position.y, srcTransform.position.z);
						}
					}
					break;
				case "Marker0015":
					Debug.Log("Got Marker0015");
					foreach (Transform destTransform in destSet.transform) {
						if (destTransform.name == "Marker0015") {
							destTransform.position = new Vector3(srcTransform.position.x, srcTransform.position.y, srcTransform.position.z);
						}
					}
					break;
				case "Marker0016":
					Debug.Log("Got Marker0016");
					foreach (Transform destTransform in destSet.transform) {
						if (destTransform.name == "Marker0016") {
							destTransform.position = new Vector3(srcTransform.position.x, srcTransform.position.y, srcTransform.position.z);
						}
					}
					break;
				case "Marker0017":
					Debug.Log("Got Marker0017");
					foreach (Transform destTransform in destSet.transform) {
						if (destTransform.name == "Marker0017") {
							destTransform.position = new Vector3(srcTransform.position.x, srcTransform.position.y, srcTransform.position.z);
						}
					}
					break;
				case "Marker0018":
					Debug.Log("Got Marker0018");
					foreach (Transform destTransform in destSet.transform) {
						if (destTransform.name == "Marker0018") {
							destTransform.position = new Vector3(srcTransform.position.x, srcTransform.position.y, srcTransform.position.z);
						}
					}
					break;
				case "Marker0019":
					Debug.Log("Got Marker0019");
					foreach (Transform destTransform in destSet.transform) {
						if (destTransform.name == "Marker0019") {
							destTransform.position = new Vector3(srcTransform.position.x, srcTransform.position.y, srcTransform.position.z);
						}
					}
					break;
				case "Marker0020":
					Debug.Log("Got Marker0020");
					foreach (Transform destTransform in destSet.transform) {
						if (destTransform.name == "Marker0020") {
							destTransform.position = new Vector3(srcTransform.position.x, srcTransform.position.y, srcTransform.position.z);
						}
					}
					break;
				case "Marker0021":
					Debug.Log("Got Marker0021");
					foreach (Transform destTransform in destSet.transform) {
						if (destTransform.name == "Marker0021") {
							destTransform.position = new Vector3(srcTransform.position.x, srcTransform.position.y, srcTransform.position.z);
						}
					}
					break;
				case "Marker0022":
					Debug.Log("Got Marker0022");
					foreach (Transform destTransform in destSet.transform) {
						if (destTransform.name == "Marker0022") {
							destTransform.position = new Vector3(srcTransform.position.x, srcTransform.position.y, srcTransform.position.z);
						}
					}
					break;
				case "Marker0023":
					Debug.Log("Got Marker0023");
					foreach (Transform destTransform in destSet.transform) {
						if (destTransform.name == "Marker0023") {
							destTransform.position = new Vector3(srcTransform.position.x, srcTransform.position.y, srcTransform.position.z);
						}
					}
					break;
				case "Marker0024":
					Debug.Log("Got Marker0024");
					foreach (Transform destTransform in destSet.transform) {
						if (destTransform.name == "Marker0024") {
							destTransform.position = new Vector3(srcTransform.position.x, srcTransform.position.y, srcTransform.position.z);
						}
					}
					break;
				case "Marker0025":
					Debug.Log("Got Marker0025");
					foreach (Transform destTransform in destSet.transform) {
						if (destTransform.name == "Marker0025") {
							destTransform.position = new Vector3(srcTransform.position.x, srcTransform.position.y, srcTransform.position.z);
						}
					}
					break;
				case "Marker0026":
					Debug.Log("Got Marker0026");
					foreach (Transform destTransform in destSet.transform) {
						if (destTransform.name == "Marker0026") {
							destTransform.position = new Vector3(srcTransform.position.x, srcTransform.position.y, srcTransform.position.z);
						}
					}
					break;
				case "Marker0027":
					Debug.Log("Got Marker0027");
					foreach (Transform destTransform in destSet.transform) {
						if (destTransform.name == "Marker0027") {
							destTransform.position = new Vector3(srcTransform.position.x, srcTransform.position.y, srcTransform.position.z);
						}
					}
					break;
				case "Marker0028":
					Debug.Log("Got Marker0028");
					foreach (Transform destTransform in destSet.transform) {
						if (destTransform.name == "Marker0028") {
							destTransform.position = new Vector3(srcTransform.position.x, srcTransform.position.y, srcTransform.position.z);
						}
					}
					break;
				case "Marker0029":
					Debug.Log("Got Marker0029");
					foreach (Transform destTransform in destSet.transform) {
						if (destTransform.name == "Marker0029") {
							destTransform.position = new Vector3(srcTransform.position.x, srcTransform.position.y, srcTransform.position.z);
						}
					}
					break;
				case "Marker0030":
					Debug.Log("Got Marker0030");
					foreach (Transform destTransform in destSet.transform) {
						if (destTransform.name == "Marker0030") {
							destTransform.position = new Vector3(srcTransform.position.x, srcTransform.position.y, srcTransform.position.z);
						}
					}
					break;
				}
			}
						
			//float x = destObj.transform.position.x;
			//float y = destObj.transform.position.y;
			//destObj.transform.position = new Vector3(x, y, myTarget.numberOfMarkers);
			
			
			
		}
		
		GUILayout.Space(20);
    }
}