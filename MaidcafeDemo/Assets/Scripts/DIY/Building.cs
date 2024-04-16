﻿
using UnityEngine;
using System.Collections;


//	diy物品的状态
public enum BuildingStates {
	Normal = 0,	//	正常状态
	Build,	//	创建状态（跟随鼠标）
}

public class Building : MonoBehaviour
{
    public int propertyID; //物品的ID
    public int width;    //	物品的宽度
    public int height;   //	物品的高度
    public BuildingStates state;    //	当前状态
   
    public  NewBuildingCon bc; //diy控制器
    public Transform targetDiyPoint; //物品在diy区域的投影位置

    public Vector3 offsetVec; //物品偏移量  

    void Awake()
    {
       
        if (GameObject.FindWithTag("DIYController")!= null)
        {
            bc = GameObject.FindWithTag("DIYController").GetComponent<NewBuildingCon>();
        }
       
    }

   

   

 
}
