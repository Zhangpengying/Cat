using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Property2 : MonoBase
{
    public Player player;

    public bool isArrive = false;
    private Action tempAct;
    //事件ID
    public int ID_Property2 = 501;
    //该事件执行次数
    public int DoNum_Property2 = 0;
    private Hashtable Infor_Property2 = new Hashtable();

    //该道具上绑定的所有事件
    public Dictionary<int, Hashtable> Property2Events = new Dictionary<int, Hashtable>();

    // Use this for initialization
    void Start()
    {
        EventManager.instance.RegisterReceiver(this);

        //事件信息记录
        tempAct = AllMenuEvent;
        Infor_Property2.Add("DoNum", DoNum_Property2);
        Infor_Property2.Add("Action", tempAct);

        //注册事件
        Property2Events.Add(ID_Property2, Infor_Property2);

        StaticVar.AddEvents(ID_Property2, Infor_Property2);
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
                    //获得物品
                    ItemInfo newItem = new ItemInfo();
                    newItem.itemType = ItemType.SingleItem;
                    newItem.itemName = "宝剑";
                    newItem.itemNum = 1;
                    newItem.itemDesc = "一把很贵的剑";
                    player.ItemList.Add(newItem);
                }
            }
        }
    }

    public override void ReciveMessage(Message msg)
    {
        base.ReciveMessage(msg);
        player = msg.Content as Player;
        if (msg.Command == MyMessageType.Event_TouchProperty2)
        {
            player = msg.Content as Player;
            //遍历A，根据ID读取A里对应的Value赋值给B
            foreach (var item in MessageSend.instance.Events)
            {
                if (Property2Events.ContainsKey(item.Key))
                {
                    Property2Events[item.Key] = item.Value;
                }
            }
            ((Action)Property2Events[ID_Property2]["Action"])();
            isArrive = true;
        }
        else if (msg.Command == MyMessageType.Event_LeaveProperty2)
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
