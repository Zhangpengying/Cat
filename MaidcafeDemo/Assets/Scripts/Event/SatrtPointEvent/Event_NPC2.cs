using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_NPC2 : MonoBase
{
    public Player player;

    public bool isArrive = false;
    private Action tempAct;
    //�¼�ID
    public int ID_NPC2 = 601;
    //���¼�ִ�д���
    public int DoNum_NPC2 = 0;
    private Hashtable Infor_NPC2 = new Hashtable();

    //�õ����ϰ󶨵������¼�
    public Dictionary<int, Hashtable> NPC2Events = new Dictionary<int, Hashtable>();

    // Use this for initialization
    void Start()
    {
        EventManager.instance.RegisterReceiver(this);

        //�¼���Ϣ��¼
        tempAct = AllMenuEvent;
        Infor_NPC2.Add("DoNum", DoNum_NPC2);
        Infor_NPC2.Add("Action", tempAct);

        //ע���¼�
        NPC2Events.Add(ID_NPC2, Infor_NPC2);

        StaticVar.AddEvents(ID_NPC2, Infor_NPC2);
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
                }
            }
        }
    }

    public override void ReciveMessage(Message msg)
    {
        base.ReciveMessage(msg);
        player = msg.Content as Player;
        if (msg.Command == MyMessageType.Event_TouchNPC2)
        {
            player = msg.Content as Player;
            //����A������ID��ȡA���Ӧ��Value��ֵ��B
            foreach (var item in MessageSend.instance.Events)
            {
                if (NPC2Events.ContainsKey(item.Key))
                {
                    NPC2Events[item.Key] = item.Value;
                }
            }
            ((Action)NPC2Events[ID_NPC2]["Action"])();
            isArrive = true;
        }
        else if (msg.Command == MyMessageType.Event_LeaveNPC2)
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
