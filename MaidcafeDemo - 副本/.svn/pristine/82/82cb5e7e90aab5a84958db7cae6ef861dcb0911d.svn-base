using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionScene1Init : MonoBehaviour, SceneInitManager
{
    

    // Use this for initialization
    void Start () {
        GameObject.Find("UI/Canvas").GetComponent<Canvas>().worldCamera = GameObject.Find("Environment/Camera/UICamera").GetComponent<Camera>();
        Initialize();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Finalise()
    {
        throw new NotImplementedException();
    }

    public void Initialize()
    {
        WindowManager.instance.Open<ReturnHomeWnd>().Initialize();
    }
}
