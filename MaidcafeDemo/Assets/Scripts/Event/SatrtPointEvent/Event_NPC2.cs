using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_NPC2 : MonoBase
{
    public Player player;

    public bool isArrive = false;
    private Action tempAct;
    //事件ID
    public int ID_NPC2 = 601;
    //该事件执行次数
    public int DoNum_NPC2 = 0;
    private Hashtable Infor_NPC2 = new Hashtable();

    //该道具上绑定的所有事件
    public Dictionary<int, Hashtable> NPC2Events = new Dictionary<int, Hashtable>();

    // Use this for initialization
    void Start()
    {
        EventManager.instance.RegisterReceiver(this);

        //事件信息记录
        tempAct = AllMenuEvent;
        Infor_NPC2.Add("DoNum", DoNum_NPC2);
        Infor_NPC2.Add("Action", tempAct);

        //注册事件
        NPC2Events.Add(ID_NPC2, Infor_NPC2);

        StaticVar.AddEvents(ID_NPC2, Infor_NPC2);
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
        if (msg.Command == MyMessageType.Event_TouchNPC2)
        {
            player = msg.Content as Player;
            //遍历A，根据ID读取A里对应的Value赋值给B
            foreach (var item in MessageSend.instance.Events)
            {
                if (NPC2Events.ContainsKey(item.Key))
                {
                    NPC2Events[item.Key] = item.Value;
                }
            }
            ((Action)NPC2Events[ID_NPC2]["Action"])();
            isArrive = true;
        }
        else if (msg.Command == MyMessageType.Event_LeaveNPC2)
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
