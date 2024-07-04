using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToHome : MonoBase {

    public bool openDoor  ;
    private Player player;
    private Action tempAct;
    //事件ID
    public int ID_ToHome = 500;
    //该事件执行次数
    public int DoNum_ToHome = 0;

    private Hashtable Infor_ToHome = new Hashtable();

    //该道具上绑定的所有事件
    public Dictionary<int, Hashtable> DoorEvents = new Dictionary<int, Hashtable>();

    // Use this for initialization
    void Start()
    {
        EventManager.instance.RegisterReceiver(this);

        //事件信息记录
        tempAct = PropertyEvent;
        Infor_ToHome.Add("DoNum", DoNum_ToHome);
        Infor_ToHome.Add("Action", tempAct);

        //注册事件
        DoorEvents.Add(ID_ToHome, Infor_ToHome);

        StaticVar.AddEvents(ID_ToHome, Infor_ToHome);
    }

    public void PropertyEvent()
    {
        StaticVar.NextScene = "Home_Normal_02";
        
    }

    private void Update()
    {
        if (openDoor)
        {
            if (StaticVar.NextScene != "")
            {
                StaticVar.GateWay(1101,player);
            }
        }
    }
   
    public override void ReciveMessage(Message msg)
    {
        base.ReciveMessage(msg);
        if (msg.Command == MyMessageType.Event_OpenStreetToHome)
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
            openDoor = true;
            ((Action)DoorEvents[ID_ToHome]["Action"])();

        }
        else if (msg.Command == MyMessageType.Event_CloseStreetToHome)
        {
            StaticVar.NextScene = "";
            openDoor = false;
           
        }
    }
}
