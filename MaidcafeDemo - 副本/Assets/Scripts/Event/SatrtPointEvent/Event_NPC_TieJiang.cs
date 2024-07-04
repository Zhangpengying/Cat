using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_NPC_TieJiang : MonoBase
{
    public Player player;

    public bool isArrive = false;
    private Action tempAct;
    //事件ID
    public int ID_NPC_TieJiang = 606;
    //该事件执行次数
    public int DoNum_NPC_TieJiang = 0;
    private Hashtable Infor_NPC_TieJiang = new Hashtable();

    //该道具上绑定的所有事件
    public Dictionary<int, Hashtable> NPC_TieJiangEvents = new Dictionary<int, Hashtable>();

    // Use this for initialization
    void Start()
    {
        EventManager.instance.RegisterReceiver(this);

        //事件信息记录
        tempAct = AllMenuEvent;
        Infor_NPC_TieJiang.Add("DoNum", DoNum_NPC_TieJiang);
        Infor_NPC_TieJiang.Add("Action", tempAct);

        //注册事件
        NPC_TieJiangEvents.Add(ID_NPC_TieJiang, Infor_NPC_TieJiang);

        StaticVar.AddEvents(ID_NPC_TieJiang, Infor_NPC_TieJiang);
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
                    //if (StaticVar.player.PlayerHP>0)
                    //{
                    //    StaticVar.player.PlayerHP -= 1;
                    //}
                }
            }
        }
    }

    public override void ReciveMessage(Message msg)
    {
        base.ReciveMessage(msg);
        player = msg.Content as Player;
        if (msg.Command == MyMessageType.Event_TouchNPC_TieJiang)
        {
            player = msg.Content as Player;
            //遍历A，根据ID读取A里对应的Value赋值给B
            foreach (var item in MessageSend.instance.Events)
            {
                if (NPC_TieJiangEvents.ContainsKey(item.Key))
                {
                    NPC_TieJiangEvents[item.Key] = item.Value;
                }
            }
            ((Action)NPC_TieJiangEvents[ID_NPC_TieJiang]["Action"])();
            isArrive = true;
        }
        else if (msg.Command == MyMessageType.Event_LeaveNPC_TieJiang)
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
