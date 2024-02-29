using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Timer : MonoBehaviour
{
    public float i =2;


    // Update is called once per frame
    void Update () {
        i -= Time.deltaTime;
        if ( i<= 0)
        {
            print("2秒了");
            i = 2;
        }
	}

}   

