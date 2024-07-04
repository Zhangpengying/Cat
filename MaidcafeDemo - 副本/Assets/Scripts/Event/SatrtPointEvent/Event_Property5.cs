using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Property5 : MonoBase
{
    public Player player;

    public bool isArrive = false;
    private Action tempAct;
    //事件ID
    public int ID_Property5 = 507;
    //该事件执行次数
    public int DoNum_Property5 = 0;
    private Hashtable Infor_Property5 = new Hashtable();

    //该道具上绑定的所有事件
    public Dictionary<int, Hashtable> Property5Events = new Dictionary<int, Hashtable>();

    // Use this for initialization
    void Start()
    {
        EventManager.instance.RegisterReceiver(this);

        //事件信息记录
        tempAct = AllMenuEvent;
        Infor_Property5.Add("DoNum", DoNum_Property5);
        Infor_Property5.Add("Action", tempAct);

        //注册事件
        Property5Events.Add(ID_Property5, Infor_Property5);

        StaticVar.AddEvents(ID_Property5, Infor_Property5);
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
                    //解锁障碍物
                    SendCustomerMessage(MyMessageType.Type_Event, MyMessageType.Event_UnlockObstacles1, player);

                    //获得物品
                    ItemInfo newItem = new ItemInfo();
                    newItem.itemID = 1004;
                    newItem.itemType = ItemType.SingleItem;
                    newItem.itemName = "魔法杖";
                    newItem.itemNum = 1;
                    newItem.itemDesc = "神器魔法杖";
                    player.ItemList.Add(newItem);
                    Destroy(this);

                }
            }
        }
    }

    public override void ReciveMessage(Message msg)
    {
        base.ReciveMessage(msg);
        player = msg.Content as Player;
        if (msg.Command == MyMessageType.Event_TouchProperty5)
        {
            player = msg.Content as Player;
            //遍历A，根据ID读取A里对应的Value赋值给B
            foreach (var item in MessageSend.instance.Events)
            {
                if (Property5Events.ContainsKey(item.Key))
                {
                    Property5Events[item.Key] = item.Value;
                }
            }
            ((Action)Property5Events[ID_Property5]["Action"])();
            isArrive = true;
        }
        else if (msg.Command == MyMessageType.Event_LeaveProperty5)
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
