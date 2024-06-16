using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����
/// </summary>
public class Event_Property7 : MonoBase
{
    //�Ƿ�ɴ���
    public static bool canGateWay = false;
    public Player player;
    private Action tempAct;
    //�¼�ID
    public int ID_GateWay = 400;
    //���¼�ִ�д���
    public int DoNum_GateWay = 0;

    private Hashtable Infor_GateWay = new Hashtable();

    //�õ����ϰ󶨵������¼�
    public Dictionary<int, Hashtable> DoorEvents = new Dictionary<int, Hashtable>();

    // Use this for initialization
    void Start()
    {
        EventManager.instance.RegisterReceiver(this);

        //�¼���Ϣ��¼
        tempAct = PropertyEvent;
        Infor_GateWay.Add("DoNum", DoNum_GateWay);
        Infor_GateWay.Add("Action", tempAct);

        //ע���¼�
        DoorEvents.Add(ID_GateWay, Infor_GateWay);

        StaticVar.AddEvents(ID_GateWay, Infor_GateWay);
    }

    public void PropertyEvent()
    {

    }

    private void Update()
    {


    }

    public override void ReciveMessage(Message msg)
    {
        base.ReciveMessage(msg);


        if (msg.Command == MyMessageType.Event_TouchObstacles2)
        {
            player = msg.Content as Player;
            //����A������ID��ȡA���Ӧ��Value��ֵ��B
            foreach (var item in MessageSend.instance.Events)
            {
                if (DoorEvents.ContainsKey(item.Key))
                {
                    DoorEvents[item.Key] = item.Value;
                }
            }
            if (canGateWay)
            {
                Destroy(transform.parent.gameObject);
            }
            else
            {
                //���뻥��
                if (!player.IsLockPlayer)
                {
                    StaticVar.MessageSendToFungus(transform.parent.name, player);
                }
            }
            ((Action)DoorEvents[ID_GateWay]["Action"])();


        }
        else if (msg.Command == MyMessageType.Event_UnlockObstacles2)
        {
            canGateWay = true;
        }

    }
}
