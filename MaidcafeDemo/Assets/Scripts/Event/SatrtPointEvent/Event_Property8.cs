using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Property8 : MonoBase
{
    public Player player;

    public bool isArrive = false;
    private Action tempAct;
    //�¼�ID
    public int ID_Property8 = 503;
    //���¼�ִ�д���
    public int DoNum_Property8 = 0;
    private Hashtable Infor_Property8 = new Hashtable();

    //�õ����ϰ󶨵������¼�
    public Dictionary<int, Hashtable> Property8Events = new Dictionary<int, Hashtable>();

    // Use this for initialization
    void Start()
    {
        EventManager.instance.RegisterReceiver(this);

        //�¼���Ϣ��¼
        tempAct = AllMenuEvent;
        Infor_Property8.Add("DoNum", DoNum_Property8);
        Infor_Property8.Add("Action", tempAct);

        //ע���¼�
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
                //���뻥��
                if (!player.IsLockPlayer)
                {
                    //�����Ʒ
                    ItemInfo newItem = new ItemInfo();
                    newItem.itemID = 1005;
                    newItem.itemType = ItemType.MoreItem;
                    newItem.itemName = "ħ���۳�";
                    newItem.itemNum = 10;
                    newItem.itemDesc = "��������Ч���ķ۳�������װ�䣩";
                   
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
            //����A������ID��ȡA���Ӧ��Value��ֵ��B
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
