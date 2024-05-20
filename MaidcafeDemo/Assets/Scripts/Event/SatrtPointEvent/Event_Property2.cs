using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Property2 : MonoBase
{
    public Player player;

    public bool isArrive = false;
    private Action tempAct;
    //�¼�ID
    public int ID_Property2 = 501;
    //���¼�ִ�д���
    public int DoNum_Property2 = 0;
    private Hashtable Infor_Property2 = new Hashtable();

    //�õ����ϰ󶨵������¼�
    public Dictionary<int, Hashtable> Property2Events = new Dictionary<int, Hashtable>();

    // Use this for initialization
    void Start()
    {
        EventManager.instance.RegisterReceiver(this);

        //�¼���Ϣ��¼
        tempAct = AllMenuEvent;
        Infor_Property2.Add("DoNum", DoNum_Property2);
        Infor_Property2.Add("Action", tempAct);

        //ע���¼�
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
                //���뻥��
                if (!player.IsLockPlayer)
                {
                    StaticVar.MessageSendToFungus(transform.parent.name, player);
                    //�����Ʒ
                    ItemInfo newItem = new ItemInfo();
                    newItem.itemType = ItemType.SingleItem;
                    newItem.itemName = "����";
                    newItem.itemNum = 1;
                    newItem.itemDesc = "һ�Ѻܹ�Ľ�";
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
            //����A������ID��ȡA���Ӧ��Value��ֵ��B
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
