using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBase
{
    //是否到达触发位置
    public bool arriveEvent = false;
    private int chairID;
    Player player;
	// Use this for initialization
	void Start () {
        //EventManager.instance.RegisterReceiver(this);
        ////注册事件
        //StaticVar.AddEvents(gameObject);
        //chairID = transform.parent.GetComponent<Chair>().chairID;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (arriveEvent)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                if (chairID == StaticVar.InteractiveProp.GetComponent<Chair>().chairID)
                {
                    player.GetComponent<SpriteRenderer>().flipX = !transform.parent.Find("BG").GetComponent<SpriteRenderer>().flipX;
                    StaticVar.MessageSendToFungus("Hello", player);

                }
            }
        }
    }

    public override void ReciveMessage(Message msg)
    {
        base.ReciveMessage(msg);
        if (msg.Command == MyMessageType.Event_StartEvent)
        {
            arriveEvent = true;
            player = msg.Content as Player;
        }
        else if (msg.Command == MyMessageType.Event_LeaveEvent)
        {
            arriveEvent = false;
        }
    }

  
}
 