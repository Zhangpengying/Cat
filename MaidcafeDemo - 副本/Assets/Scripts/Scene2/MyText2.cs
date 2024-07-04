using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyText2 : MonoBehaviour {

    public GameObject obj1;
    public GameObject obj2;
	// Use this for initialization
	void Start () {
        print(obj1.transform.localPosition - obj2.transform.localPosition);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
