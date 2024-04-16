﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBase
{
    //玩家是否到达位置
    public static bool arriveBed = false;
    private Player player;
    private Action tempAct;
    //事件ID
    public int ID_Bed = 600;
    //该事件执行次数
    public int DoNum_Bed = 0;

    private Hashtable Infor_Bed = new Hashtable();

    //该道具上绑定的所有事件
    public Dictionary<int, Hashtable> BedEvents = new Dictionary<int, Hashtable>();

    // Use this for initialization
    void Start()
    {
        EventManager.instance.RegisterReceiver(this);

        //事件信息记录
        tempAct = PropertyEvent;
        Infor_Bed.Add("DoNum", DoNum_Bed);
        Infor_Bed.Add("Action", tempAct);

        //注册事件
        BedEvents.Add(ID_Bed, Infor_Bed);

        StaticVar.AddEvents(ID_Bed, Infor_Bed);
    }

    public void PropertyEvent()
    {
       
    }
    private void Update()
    {
        if (arriveBed)
        {
            //显示提示符
            if (GameObject.Find("Environment/Events/Tips") == null)
            {
                GameObject.Find("UI").GetComponent<GlobalVariable>().ActiveTips();

            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    if (GameObject.Find("Environment/Events/Tips") != null)
                    {
                        Destroy(GameObject.Find("Environment/Events/Tips"));
                        arriveBed = false;
                    }
                    //进入互动
                    if (!player.IsLockPlayer)
                    {
                        StaticVar.MessageSendToFungus(transform.parent.name, player);
                    }
                }
            }
        }
    }

    public override void ReciveMessage(Message msg)
    {
        base.ReciveMessage(msg);
        
        if (msg.Command == MyMessageType.Event_Bed)
        {
            player = msg.Content as Player;
            //遍历A，根据ID读取A里对应的Value赋值给B
            foreach (var item in MessageSend.instance.Events)
            {
                if (BedEvents.ContainsKey(item.Key))
                {
                    BedEvents[item.Key] = item.Value;
                }
            }
            ((Action)BedEvents[ID_Bed]["Action"])();
            arriveBed = true;
        }
        else if (msg.Command == MyMessageType.Event_LeaveBed)
        {
            if (GameObject.Find("Environment/Events/Tips") != null)
            {
                Destroy(GameObject.Find("Environment/Events/Tips"));
            }
            arriveBed = false;
          
        }
    }
}
