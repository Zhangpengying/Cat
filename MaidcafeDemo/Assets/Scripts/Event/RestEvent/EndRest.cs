using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 结束中场休息
/// </summary>
public class EndRest : MonoBase
{
    //是否到达结束中场休息位置
    public static bool canEnd = false;
    public Player player;
    private Action tempAct;
    //事件ID
    public int ID_ToBus = 400;
    //该事件执行次数
    public int DoNum_ToBus = 0;

    private Hashtable Infor_ToBus = new Hashtable();

    //该道具上绑定的所有事件
    public Dictionary<int, Hashtable> DoorEvents = new Dictionary<int, Hashtable>();

    // Use this for initialization
    void Start()
    {
        EventManager.instance.RegisterReceiver(this);

        //事件信息记录
        tempAct = PropertyEvent;
        Infor_ToBus.Add("DoNum", DoNum_ToBus);
        Infor_ToBus.Add("Action", tempAct);

        //注册事件
        DoorEvents.Add(ID_ToBus, Infor_ToBus);

        StaticVar.AddEvents(ID_ToBus, Infor_ToBus);
    }

    public void PropertyEvent()
    {
       
    }

    private void Update()
    {
        if (canEnd)
        {
            StaticVar.GateWay(1102, player);
        }
       
    }

    public override void ReciveMessage(Message msg)
    {
        base.ReciveMessage(msg);
     

        if (msg.Command == MyMessageType.Event_EndRest)
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
            canEnd = true;
            ((Action)DoorEvents[ID_ToBus]["Action"])();


        }
        else
        {
            canEnd = false;
            StaticVar.InteractiveProp = null;
        }
    }
}
