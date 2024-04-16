﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HomeDoorEvents : MonoBase
{
    public bool openDoor = false;
    public Player player;
    private Action tempAct;
    //事件ID
    public int ID_ToCoffee = 100;
    //该事件执行次数
    public int DoNum_ToCoffee = 0;

    private Hashtable Infor_ToCoffee = new Hashtable();

    //该道具上绑定的所有事件
    public Dictionary<int, Hashtable> DoorEvents = new Dictionary<int, Hashtable>();

    // Use this for initialization
    void Start()
    {
        EventManager.instance.RegisterReceiver(this);

        //事件信息记录
        tempAct = ToCoffeeEvent;
        Infor_ToCoffee.Add("DoNum", DoNum_ToCoffee);
        Infor_ToCoffee.Add("Action", tempAct);

        //注册事件
        DoorEvents.Add(ID_ToCoffee, Infor_ToCoffee);

        StaticVar.AddEvents(ID_ToCoffee, Infor_ToCoffee);
    }


    private void Update()
    {
        if (openDoor)
        {
            if (player!=null)
            {
                StaticVar.Door(901, player,610);
            }
           
        }
    }
    
    public override void ReciveMessage(Message msg)
    {
        base.ReciveMessage(msg);
        if (msg.Command == MyMessageType.Event_OpenHomeToStreet)
        {
            player = msg.Content as Player;
            //遍历A，根据ID读取A里对应的Value赋值给B
            foreach (var item in MessageSend.instance.Events)
            {
                if (DoorEvents.ContainsKey(item.Key))
                {
                    DoorEvents[item.Key] = item.Value;
                }
            }
            ((Action)DoorEvents[ID_ToCoffee]["Action"])();
            openDoor = true;
        }
        else if (msg.Command == MyMessageType.Event_CloseHomeToStreet)
        {
            openDoor = false;
            StaticVar.InteractiveProp = null;
            StaticVar.NextScene = "";
        }
    }

    public void ToCoffeeEvent()
    {
        
        StaticVar.LastGateway = MessageSend.instance.gatewaysCfg[1101].Position + new Vector3(1.0f, 0, 0);
        StaticVar.NextScene = "CoffeeStore";
    }
}
