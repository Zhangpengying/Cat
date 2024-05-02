using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_NPC1 : MonoBase
{
    public Player player;

    public bool isArrive = false;
    private Action tempAct;
    //事件ID
    public int ID_NPC1 = 600;
    //该事件执行次数
    public int DoNum_NPC1 = 0;
    private Hashtable Infor_NPC1 = new Hashtable();

    //该道具上绑定的所有事件
    public Dictionary<int, Hashtable> NPC1Events = new Dictionary<int, Hashtable>();

    // Use this for initialization
    void Start()
    {
        EventManager.instance.RegisterReceiver(this);

        //事件信息记录
        tempAct = AllMenuEvent;
        Infor_NPC1.Add("DoNum", DoNum_NPC1);
        Infor_NPC1.Add("Action", tempAct);

        //注册事件
        NPC1Events.Add(ID_NPC1, Infor_NPC1);

        StaticVar.AddEvents(ID_NPC1, Infor_NPC1);
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
        if (msg.Command == MyMessageType.Event_TouchNPC1)
        {
            player = msg.Content as Player;
            //遍历A，根据ID读取A里对应的Value赋值给B
            foreach (var item in MessageSend.instance.Events)
            {
                if (NPC1Events.ContainsKey(item.Key))
                {
                    NPC1Events[item.Key] = item.Value;
                }
            }
            ((Action)NPC1Events[ID_NPC1]["Action"])();
            isArrive = true;
        }
        else if (msg.Command == MyMessageType.Event_LeaveNPC1)
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
