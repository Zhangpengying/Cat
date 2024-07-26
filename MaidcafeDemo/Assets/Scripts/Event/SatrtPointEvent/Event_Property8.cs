using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Property8 : MonoBase
{
    public Player player;

    public bool isArrive = false;
    private Action tempAct;
    //事件ID
    public int ID_Property8 = 503;
    //该事件执行次数
    public int DoNum_Property8 = 0;
    private Hashtable Infor_Property8 = new Hashtable();

    //该道具上绑定的所有事件
    public Dictionary<int, Hashtable> Property8Events = new Dictionary<int, Hashtable>();

    // Use this for initialization
    void Start()
    {
        EventManager.instance.RegisterReceiver(this);

        //事件信息记录
        tempAct = AllMenuEvent;
        Infor_Property8.Add("DoNum", DoNum_Property8);
        Infor_Property8.Add("Action", tempAct);

        //注册事件
        Property8Events.Add(ID_Property8, Infor_Property8);

        StaticVar.AddEvents(ID_Property8, Infor_Property8);
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
                    //获得物品
                    ItemInfo newItem = new ItemInfo();
                    newItem.itemID = 1005;
                    newItem.itemType = ItemType.MoreItem;
                    newItem.itemName = "魔法粉尘";
                    newItem.itemNum = 10;
                    newItem.itemDesc = "具有神奇效果的粉尘（可以装配）";
                   
                    if (!StaticVar.GetItem(1005))
                    {
                        player.ItemList.Add(newItem);
                        Destroy(transform.parent.gameObject);
                        StaticVar.MessageSendToFungus(transform.parent.name, player);
                    }
                }
            }
        }
    }

    public override void ReciveMessage(Message msg)
    {
        base.ReciveMessage(msg);
        player = msg.Content as Player;
        if (msg.Command == MyMessageType.Event_TouchProperty8)
        {
            player = msg.Content as Player;
            //遍历A，根据ID读取A里对应的Value赋值给B
            foreach (var item in MessageSend.instance.Events)
            {
                if (Property8Events.ContainsKey(item.Key))
                {
                    Property8Events[item.Key] = item.Value;
                }
            }
            ((Action)Property8Events[ID_Property8]["Action"])();
            isArrive = true;
        }
        else if (msg.Command == MyMessageType.Event_LeaveProperty8)
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
