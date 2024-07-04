using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Property5 : MonoBase
{
    public Player player;

    public bool isArrive = false;
    private Action tempAct;
    //�¼�ID
    public int ID_Property5 = 507;
    //���¼�ִ�д���
    public int DoNum_Property5 = 0;
    private Hashtable Infor_Property5 = new Hashtable();

    //�õ����ϰ󶨵������¼�
    public Dictionary<int, Hashtable> Property5Events = new Dictionary<int, Hashtable>();

    // Use this for initialization
    void Start()
    {
        EventManager.instance.RegisterReceiver(this);

        //�¼���Ϣ��¼
        tempAct = AllMenuEvent;
        Infor_Property5.Add("DoNum", DoNum_Property5);
        Infor_Property5.Add("Action", tempAct);

        //ע���¼�
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
                //���뻥��
                if (!player.IsLockPlayer)
                {
                    StaticVar.MessageSendToFungus(transform.parent.name, player);
                    //�����ϰ���
                    SendCustomerMessage(MyMessageType.Type_Event, MyMessageType.Event_UnlockObstacles1, player);

                    //�����Ʒ
                    ItemInfo newItem = new ItemInfo();
                    newItem.itemID = 1004;
                    newItem.itemType = ItemType.SingleItem;
                    newItem.itemName = "ħ����";
                    newItem.itemNum = 1;
                    newItem.itemDesc = "����ħ����";
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
            //����A������ID��ȡA���Ӧ��Value��ֵ��B
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
