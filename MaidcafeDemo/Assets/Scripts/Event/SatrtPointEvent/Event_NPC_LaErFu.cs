using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_NPC_LaErFu : MonoBase
{
    public Player player;

    public bool isArrive = false;
    private Action tempAct;
    //�¼�ID
    public int ID_NPC_LaErFu = 650;
    //���¼�ִ�д���
    public int DoNum_NPC_LaErFu = 0;
    private Hashtable Infor_NPC_LaErFu = new Hashtable();

    //�õ����ϰ󶨵������¼�
    public Dictionary<int, Hashtable> NPC_LaErFuEvents = new Dictionary<int, Hashtable>();

    // Use this for initialization
    void Start()
    {
        EventManager.instance.RegisterReceiver(this);

        //�¼���Ϣ��¼
        tempAct = AllMenuEvent;
        Infor_NPC_LaErFu.Add("DoNum", DoNum_NPC_LaErFu);
        Infor_NPC_LaErFu.Add("Action", tempAct);

        //ע���¼�
        NPC_LaErFuEvents.Add(ID_NPC_LaErFu, Infor_NPC_LaErFu);

        StaticVar.AddEvents(ID_NPC_LaErFu, Infor_NPC_LaErFu);
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
                //���뻥��
                if (!player.IsLockPlayer)
                {
                    //�ж��Ƿ��в�ҩ
                   
                    if (StaticVar.GetItem(1007))
                    {
                        StaticVar.MessageSendToFungus(transform.parent.name + 1, player);
                        //SendCustomerMessage(MyMessageType.Type_Event, MyMessageType.Event_UnlockObstacles2, player);
                        //���Կ��
                        ItemInfo newItem = new ItemInfo();
                        newItem.itemID = 1006;
                        newItem.itemType = ItemType.SingleItem;
                        newItem.itemName = "���³�Կ��";
                        newItem.itemNum = 1;
                        newItem.itemDesc = "���³����Կ��";
                        player.ItemList.Add(newItem);
                    }
                    else
                    {
                        StaticVar.MessageSendToFungus(transform.parent.name + 0, player);
                    }
                    
                   
                }
            }
        }
    }

    public override void ReciveMessage(Message msg)
    {
        base.ReciveMessage(msg);
        player = msg.Content as Player;
        if (msg.Command == MyMessageType.Event_TouchNPC_LaErFu)
        {
            player = msg.Content as Player;
            //����A������ID��ȡA���Ӧ��Value��ֵ��B
            foreach (var item in MessageSend.instance.Events)
            {
                if (NPC_LaErFuEvents.ContainsKey(item.Key))
                {
                    NPC_LaErFuEvents[item.Key] = item.Value;
                }
            }
            ((Action)NPC_LaErFuEvents[ID_NPC_LaErFu]["Action"])();
            isArrive = true;
        }
        else if (msg.Command == MyMessageType.Event_LeaveNPC_LaErFu)
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
