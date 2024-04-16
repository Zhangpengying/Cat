﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeSceneInit : MonoBehaviour,SceneInitManager
{
    private void Start()
    {
        
        GameObject.Find("UI/Canvas").GetComponent<Canvas>().worldCamera = GameObject.Find("Environment/Camera/UICamera").GetComponent<Camera>();
        Initialize();
        
    }

    public void Initialize()
    {
        StaticVar.LastGateway = MessageSend.instance.gatewaysCfg[1101].Position - new Vector3(0.3f, 0, 0);
        //创建玩家
        ActorManager.instance.CreateActorCon();
        Player player = GameObject.FindWithTag("Player").GetComponent<Player>();
        //StaticVar.SetCamera();
        //改变互动NPC位置
        if (player._stateType != ActorStateType.Player_Idle)
        {
            if (StaticVar.InteractiveNpc._uid == 714)
            {
                //npc为RainFa
                StaticVar.InteractiveNpc._transform.localPosition = StaticVar.InteractiveNpc.GetComponent<InteractiveNpc>().player._transform.localPosition + new Vector3(0, 4.5f, 0);
                //触发事件
                StaticVar.MessageSendToFungus("FindNothing", StaticVar.InteractiveNpc.GetComponent<InteractiveNpc>().player);
                //解除互动关系
                player.TransState(player, ActorStateType.Player_Idle);
                StaticVar.InteractiveNpc = null;
            }
        }
        CreateDIY();
    }

    //创建之前创建的DIY物品
    void CreateDIY()
    {
        //清理数据
        MessageSend.instance.propertyIDs.Clear();
        MessageSend.instance.CreatPropertys.Clear();
        foreach (KeyValuePair<Property, Vector3> kvp in MessageSend.instance.CreatPropertysInfo)
        {
            GameObject property = Instantiate(Resources.Load("Prefabs/Items/DIYItems/" + kvp.Key.PropertyName) as GameObject);
            property.name = kvp.Key.PropertyName;
            property.transform.SetParent(GameObject.Find("DIYCreatPropertys").transform);
            Building b = property.GetComponent<Building>();
            b.propertyID = kvp.Key.ID;
            b.state = BuildingStates.Normal;
            b.transform.position = kvp.Value;
            //删除背包已拥有
            MessageSend.instance.CurrentHavePropertys.Remove(kvp.Key);
            property.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = kvp.Key.OrderLayer;
        }
        
      
    }

    public void Finalise()
    {
        StaticVar.ClearScene();
    }
    public void EndInteraction()
    {
        StaticVar.EndInteraction();
    }
    public void OpenSelectWnd()
    {
        WindowManager.instance.Open<SelectWnd1>().Initialize();
    }
    
   
}
