using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	public void StartMainScene()
	{
		SceneManager.LoadScene ("Main Scene");
	}

	// Update is called once per frame
	void Update () {
		
	}
}
