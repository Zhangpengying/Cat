using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_NPC_ZhanBuShi : MonoBase
{
    public Player player;

    public bool isArrive = false;
    private Action tempAct;
    //�¼�ID
    public int ID_NPC_ZhanBuShi = 608;
    //���¼�ִ�д���
    public int DoNum_NPC_ZhanBuShi = 0;
    private Hashtable Infor_NPC_ZhanBuShi = new Hashtable();

    //�õ����ϰ󶨵������¼�
    public Dictionary<int, Hashtable> NPC_ZhanBuShiEvents = new Dictionary<int, Hashtable>();

    // Use this for initialization
    void Start()
    {
        EventManager.instance.RegisterReceiver(this);

        //�¼���Ϣ��¼
        tempAct = AllMenuEvent;
        Infor_NPC_ZhanBuShi.Add("DoNum", DoNum_NPC_ZhanBuShi);
        Infor_NPC_ZhanBuShi.Add("Action", tempAct);

        //ע���¼�
        NPC_ZhanBuShiEvents.Add(ID_NPC_ZhanBuShi, Infor_NPC_ZhanBuShi);

        StaticVar.AddEvents(ID_NPC_ZhanBuShi, Infor_NPC_ZhanBuShi);
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
        if (msg.Command == MyMessageType.Event_TouchNPC_ZhanBuShi)
        {
            player = msg.Content as Player;
            //����A������ID��ȡA���Ӧ��Value��ֵ��B
            foreach (var item in MessageSend.instance.Events)
            {
                if (NPC_ZhanBuShiEvents.ContainsKey(item.Key))
                {
                    NPC_ZhanBuShiEvents[item.Key] = item.Value;
                }
            }
            ((Action)NPC_ZhanBuShiEvents[ID_NPC_ZhanBuShi]["Action"])();
            isArrive = true;
        }
        else if (msg.Command == MyMessageType.Event_LeaveNPC_ZhanBuShi)
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
