﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestSceneInit : MonoBehaviour,SceneInitManager
{

    public void Start()
    {
        GameObject.Find("UI/Canvas").GetComponent<Canvas>().worldCamera = GameObject.Find("Environment/Camera/UICamera").GetComponent<Camera>();

        Initialize();
    }

    public void Initialize()
    {
        StaticVar.LastGateway = MessageSend.instance.bornCfgs[603].Position;
        //创建玩家
        ActorManager.instance.CreateActorCon();
        StaticVar.player = ActorManager.instance.GetActor(0) as Player;

        //创建中场休息的顾客
        MessageSend.instance.RestCustomers.Clear();
        foreach (var item in MessageSend.instance.allcustomers)
        {
            MessageSend.instance.RestCustomers.Add(item);
          
        }
        //创建DIY物品
        CreateDIY();

        //创建顾客
        List<Chair> chairs = new List<Chair>();
        foreach (var child in transform.Find("Events").GetComponent<EventManager>().ReceiveList)
        {
            if (child.GetComponent<Chair>() != null)
            {
                chairs.Add(child.GetComponent<Chair>());
            }
        }
        foreach (var item in MessageSend.instance.RestCustomers)
        {
            ActorManager.instance.CreatCustomer(item);
            Customer1 cus = ActorManager.instance.GetActor(item.ID) as Customer1;
            cus.TransState(cus, ActorStateType.PlayComputer);

            Transform chair = chairs[MessageSend.instance.RestCustomers.IndexOf(item)].transform;
            cus.transform.Find("BG"). GetComponent<SpriteRenderer>().flipX = chair.Find("BG").GetComponent<SpriteRenderer>().flipX;
            cus.transform.Find("BG").GetComponent<SpriteRenderer>().sortingLayerName = "NearView";
            cus.transform.position = chair.Find("SitPoint").position;
            //该顾客对应坐的椅子封闭与玩家的互动
            chair.GetComponent<Chair>().isEmpty = false;
            chair.Find("WaiterStay").GetComponent<BoxCollider2D>().enabled = true;
        }

       
        
        //事件监测
        StaticVar.CheckEvents();


    }

    public void Finalise()
    {
        //场景的清算
        StaticVar.ClearScene();
    }
    public void EndInteraction()
    {
        StaticVar.EndInteraction();
    }
    
    public void ToInterScene1()
    {
        StaticVar.ToNextSecens("InteractionScene1", StaticVar.player);
    }
    public void OpenSelectWnd()
    {
        WindowManager.instance.Open<SelectWnd1>().Initialize();
    }
    void CreateDIY()
    {
        foreach (KeyValuePair<Property, Vector3> kvp in MessageSend.instance.CreatPropertysInfo)
        {
            GameObject property = Instantiate(Resources.Load("Prefabs/Items/DIYItems/" + kvp.Key.PropertyName) as GameObject);
            property.name = kvp.Key.PropertyName;
            property.transform.SetParent(GameObject.Find("DIYCreatPropertys").transform);
            Building b = property.GetComponent<Building>();
            b.propertyID = kvp.Key.ID;
            b.state = BuildingStates.Build;
            b.transform.position = kvp.Value;
        }
    }
}
