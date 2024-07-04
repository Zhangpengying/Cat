using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 出口
/// </summary>
public class Event_MovePoint : MonoBase
{
    public bool isArrive;
    public Player player;
    private Action tempAct;
    //事件ID
    public int ID_GateWay = 550;
    //该事件执行次数
    public int DoNum_GateWay = 0;
    public Transform NextMovePoint;

    private Hashtable Infor_GateWay = new Hashtable();

    //该道具上绑定的所有事件
    public Dictionary<int, Hashtable> DoorEvents = new Dictionary<int, Hashtable>();

    // Use this for initialization
    void Start()
    {
        EventManager.instance.RegisterReceiver(this);

        //事件信息记录
        tempAct = PropertyEvent;
        Infor_GateWay.Add("DoNum", DoNum_GateWay);
        Infor_GateWay.Add("Action", tempAct);

        //注册事件
        DoorEvents.Add(ID_GateWay, Infor_GateWay);

        StaticVar.AddEvents(ID_GateWay, Infor_GateWay);
    }

    public void PropertyEvent()
    {
        GameObject.Find("UI").GetComponent<GlobalVariable>().ActiveTips();

    }

    private void Update()
    {
        if (isArrive)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                player.transform.position = NextMovePoint.position;
            }
        }

    }

    public override void ReciveMessage(Message msg)
    {
        base.ReciveMessage(msg);


        if (msg.Command == MyMessageType.Event_TouchMovePoint)
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
            if (StaticVar.InteractiveProp.name== transform.parent.name)
            {
                ((Action)DoorEvents[ID_GateWay]["Action"])();
                isArrive = true;
            }
            
        }
        else if (msg.Command == MyMessageType.Event_LeaveMovePoint)
        {
            if (GameObject.Find("Environment/Events/Tips") != null )
            {
                Destroy(GameObject.Find("Environment/Events/Tips"));
            }
            isArrive = false;
        }
    }

  
}
