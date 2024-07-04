using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Property4 : MonoBase
{
    public Player player;

    public bool isArrive = false;
    private Action tempAct;
    //事件ID
    public int ID_Property4 = 506;
    //该事件执行次数
    public int DoNum_Property4 = 0;
    private Hashtable Infor_Property4 = new Hashtable();

    //该道具上绑定的所有事件
    public Dictionary<int, Hashtable> Property4Events = new Dictionary<int, Hashtable>();

    // Use this for initialization
    void Start()
    {
        EventManager.instance.RegisterReceiver(this);

        //事件信息记录
        tempAct = AllMenuEvent;
        Infor_Property4.Add("DoNum", DoNum_Property4);
        Infor_Property4.Add("Action", tempAct);

        //注册事件
        Property4Events.Add(ID_Property4, Infor_Property4);

        StaticVar.AddEvents(ID_Property4, Infor_Property4);
    }

    // Update is called once per frame
    void Update()
    {
        if (isArrive)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (GameObject.Find("Environment/Events/Tips") != null)
                {
                    Destroy(GameObject.Find("Environment/Events/Tips"));
                }
                //进入互动
                if (!player.IsLockPlayer)
                {
                    StaticVar.MessageSendToFungus(transform.parent.name, player);
                }
            }
        }
    }

    public override void ReciveMessage(Message msg)
    {
        base.ReciveMessage(msg);
        player = msg.Content as Player;
        if (msg.Command == MyMessageType.Event_TouchProperty4)
        {
            player = msg.Content as Player;
            //遍历A，根据ID读取A里对应的Value赋值给B
            foreach (var item in MessageSend.instance.Events)
            {
                if (Property4Events.ContainsKey(item.Key))
                {
                    Property4Events[item.Key] = item.Value;
                }
            }
            ((Action)Property4Events[ID_Property4]["Action"])();
            isArrive = true;
        }
        else if (msg.Command == MyMessageType.Event_LeaveProperty4)
        {
            if (GameObject.Find("Environment/Events/Tips") != null)
            {
                Destroy(GameObject.Find("Environment/Events/Tips"));
            }
            isArrive = false;
        }
    }

    public void AllMenuEvent()
    {

        GameObject.Find("UI").GetComponent<GlobalVariable>().ActiveTips();
    }
}
