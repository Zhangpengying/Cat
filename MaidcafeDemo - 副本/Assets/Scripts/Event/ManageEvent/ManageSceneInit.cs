using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageSceneInit : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject.Find("UI/Canvas").GetComponent<Canvas>().worldCamera = GameObject.Find("Environment/Camera/UICamera").GetComponent<Camera>();
        WindowManager.instance.Open<JingYing>().Initialize();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
