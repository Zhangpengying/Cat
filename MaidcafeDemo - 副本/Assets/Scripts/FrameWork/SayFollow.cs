using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SayFollow : MonoBehaviour
{
    //跟随的物体
    public Transform target;
    //画布的renderCamera
     Camera uiCamera;
    //画布
     Canvas canvas;
   
    // Use this for initialization
    void Start()
    {
        
        
    }

    void Update()
    {
        UpdatePosition();
       
    }
    //随着目标点变动更新位置
    public void UpdatePosition()
    {
        uiCamera = GameObject.Find("UICamera").GetComponent<Camera>();
        canvas = GameObject.Find("UI/Canvas").GetComponent<Canvas>();
        //目标世界坐标转画布坐标
        Vector3 worldToScreenPoint = Camera.main.WorldToScreenPoint(target.position);
        //在画布上对应的点
        worldToScreenPoint = new Vector3(worldToScreenPoint.x, worldToScreenPoint.y, canvas.planeDistance);
        Vector3 screenToWorldPoint = uiCamera.ScreenToWorldPoint(worldToScreenPoint);
        //得到最终画布坐标系中的投影点
        Vector3 projPoint = canvas.transform.worldToLocalMatrix.MultiplyPoint(screenToWorldPoint);

         transform.localPosition = projPoint + new Vector3(100,130,0);
        transform.localScale = Vector3.one;
    }

    
}
