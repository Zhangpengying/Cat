﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{   
    //临时计时器
    public float temp;
    //创建客人数量
    public int creatNumber = 0;
    //玩家当前穿的衣服
    public string PlayerCustome = "Player_Normal";
    //玩家是否正在交互
    private bool isLockPlayer = false;
    //玩家当前身上的钱
    public int PlayerMoney = 100;
    //玩家当前交互的NPC
    public Actor InteractNPC;
    //背包物品
    public List<ItemInfo> ItemList = new List<ItemInfo>();
    //当前血量
    public int PlayerHP = 3;

    public bool IsLockPlayer
    {
        get
        {
            return isLockPlayer;

        }
        set
        {
            isLockPlayer = value;
        }
    }

    protected override void InitState()
    {
        _actorStateDic[ActorStateType.Player_Idle] = new PlayerIdleState();
        _actorStateDic[ActorStateType.Player_Work] = new PlayerWorkState();
        _actorStateDic[ActorStateType.Player_Date] = new PlayerDateState();
        _actorStateDic[ActorStateType.Player_Sleep] = new PlayerSleepState();
        _actorStateDic[ActorStateType.Player_Chivied] = new PlayerChiviedState();
    }

   

  
    private void Start()
    {
        temp = StaticVar.betweenCustomers;
    }

    private new void Update()
    {
        if (gameObject.GetComponent<Player>()._stateType == ActorStateType.Player_Work)
        {
            //有空位
            if (StaticVar.haveEmptySeat)
            {
                temp -= Time.deltaTime;
                if (temp <= 0)
                {
                    temp = StaticVar.betweenCustomers;
                    CreatCustomers();
                }
            }
            //满客
            else
            {

            }
        }
    }
    //创建客人
    public void CreatCustomers()
    {

        //是否在经营时间范围内
        if (StaticVar.currentManageTime < StaticVar.totalOperatingtime)
        {
            if (creatNumber % MessageSend.instance.allcustomers.Count == 0)
            {
                ActorManager.instance.CreatCustomer(MessageSend.instance.allcustomers[0]);
                Customer1 cus = ActorManager.instance.GetActor(MessageSend.instance.allcustomers[0].ID) as Customer1;
                cus.TransState(cus, ActorStateType.InSeat);
            }
            else
            {
                CustomerCfg cfg = MessageSend.instance.allcustomers[creatNumber % MessageSend.instance.allcustomers.Count];
                ActorManager.instance.CreatCustomer(cfg);
                Customer1 cus =  ActorManager.instance.GetActor(cfg.ID) as Customer1;
                cus.TransState(cus, ActorStateType.InSeat);
            }
            creatNumber++;
            //检测是否满员
            if (MessageSend.instance.customer.Count == MessageSend.instance.firstFloorSeats.Count)
            {
                StaticVar.haveEmptySeat = false;
             
            }
        }

    }

    
    protected override void InitCurState()
    {
       
    }
}
