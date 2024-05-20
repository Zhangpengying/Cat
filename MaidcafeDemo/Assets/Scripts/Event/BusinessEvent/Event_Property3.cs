using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Property3 : MonoBase
{
    public Player player;

    public bool isArrive = false;
    private Action tempAct;
    //�¼�ID
    public int ID_Property3 = 503;
    //���¼�ִ�д���
    public int DoNum_Property3 = 0;
    private Hashtable Infor_Property3 = new Hashtable();

    //�õ����ϰ󶨵������¼�
    public Dictionary<int, Hashtable> Property3Events = new Dictionary<int, Hashtable>();

    // Use this for initialization
    void Start()
    {
        EventManager.instance.RegisterReceiver(this);

        //�¼���Ϣ��¼
        tempAct = AllMenuEvent;
        Infor_Property3.Add("DoNum", DoNum_Property3);
        Infor_Property3.Add("Action", tempAct);

        //ע���¼�
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
                //���뻥��
                if (!player.IsLockPlayer)
                {
                    //�����Ʒ
                    ItemInfo newItem = new ItemInfo();
                    newItem.itemType = ItemType.MoreItem;
                    newItem.itemName = "ħ���۳�";
                    newItem.itemNum = 10;
                    newItem.itemDesc = "��������Ч���ķ۳�������װ�䣩";
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
            //����A������ID��ȡA���Ӧ��Value��ֵ��B
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
