using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Property3 : MonoBase
{
    public Player player;

    public bool isArrive = false;
    private Action tempAct;
    //事件ID
    public int ID_Property3 = 503;
    //该事件执行次数
    public int DoNum_Property3 = 0;
    private Hashtable Infor_Property3 = new Hashtable();

    //该道具上绑定的所有事件
    public Dictionary<int, Hashtable> Property3Events = new Dictionary<int, Hashtable>();

    // Use this for initialization
    void Start()
    {
        EventManager.instance.RegisterReceiver(this);

        //事件信息记录
        tempAct = AllMenuEvent;
        Infor_Property3.Add("DoNum", DoNum_Property3);
        Infor_Property3.Add("Action", tempAct);

        //注册事件
        Property3Events.Add(ID_Property3, Infor_Property3);

        StaticVar.AddEvents(ID_Property3, Infor_Property3);
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
                    newItem.itemType = ItemType.MoreItem;
                    newItem.itemName = "魔法粉尘";
                    newItem.itemNum = 10;
                    newItem.itemDesc = "具有神奇效果的粉尘（可以装配）";
                    bool ifContain = false;
                    foreach (var item in player.ItemList)
                    {
                        if (item.itemName== newItem.itemName)
                        {
                            item.itemNum += newItem.itemNum;
                            ifContain = true;
                        }
                    }
                    if (!ifContain)
                    {
                        player.ItemList.Add(newItem);
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
        if (msg.Command == MyMessageType.Event_TouchProperty3)
        {
            player = msg.Content as Player;
            //遍历A，根据ID读取A里对应的Value赋值给B
            foreach (var item in MessageSend.instance.Events)
            {
                if (Property3Events.ContainsKey(item.Key))
                {
                    Property3Events[item.Key] = item.Value;
                }
            }
            ((Action)Property3Events[ID_Property3]["Action"])();
            isArrive = true;
        }
        else if (msg.Command == MyMessageType.Event_LeaveProperty3)
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
