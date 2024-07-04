using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipsWnd : BaseWnd
{
    public void Initialize()
    {
        //调整tips的位置
        _transform.Find("Image").localPosition = StaticVar.WorldPosToUI(_transform.parent.GetComponent<Canvas>(), _transform.Find("Image").gameObject,StaticVar.InteractiveProp)+new Vector3(0,50,0);
        Debug.Log(_transform.Find("Image").position);
    }
	
}
