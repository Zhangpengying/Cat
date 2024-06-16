using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_NPC_TieJiang : MonoBase
{
    public Player player;

    public bool isArrive = false;
    private Action tempAct;
    //�¼�ID
    public int ID_NPC_TieJiang = 606;
    //���¼�ִ�д���
    public int DoNum_NPC_TieJiang = 0;
    private Hashtable Infor_NPC_TieJiang = new Hashtable();

    //�õ����ϰ󶨵������¼�
    public Dictionary<int, Hashtable> NPC_TieJiangEvents = new Dictionary<int, Hashtable>();

    // Use this for initialization
    void Start()
    {
        EventManager.instance.RegisterReceiver(this);

        //�¼���Ϣ��¼
        tempAct = AllMenuEvent;
        Infor_NPC_TieJiang.Add("DoNum", DoNum_NPC_TieJiang);
        Infor_NPC_TieJiang.Add("Action", tempAct);

        //ע���¼�
        NPC_TieJiangEvents.Add(ID_NPC_TieJiang, Infor_NPC_TieJiang);

        StaticVar.AddEvents(ID_NPC_TieJiang, Infor_NPC_TieJiang);
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
                    //if (StaticVar.player.PlayerHP>0)
                    //{
                    //    StaticVar.player.PlayerHP -= 1;
                    //}
                }
            }
        }
    }

    public override void ReciveMessage(Message msg)
    {
        base.ReciveMessage(msg);
        player = msg.Content as Player;
        if (msg.Command == MyMessageType.Event_TouchNPC_TieJiang)
        {
            player = msg.Content as Player;
            //����A������ID��ȡA���Ӧ��Value��ֵ��B
            foreach (var item in MessageSend.instance.Events)
            {
                if (NPC_TieJiangEvents.ContainsKey(item.Key))
                {
                    NPC_TieJiangEvents[item.Key] = item.Value;
                }
            }
            ((Action)NPC_TieJiangEvents[ID_NPC_TieJiang]["Action"])();
            isArrive = true;
        }
        else if (msg.Command == MyMessageType.Event_LeaveNPC_TieJiang)
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
