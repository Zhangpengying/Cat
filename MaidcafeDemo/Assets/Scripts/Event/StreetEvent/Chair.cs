﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBase
{
    //玩家是否在椅子位置
    public bool isArrive ;
    //椅子是否空置
    public bool isEmpty;
    //椅子的ID
    public int chairID;
    
	// Use this for initialization
	void Start () {
        EventManager.instance.RegisterReceiver(this);
        //注册事件
        //StaticVar.AddEvents(gameObject);
        isArrive = false;
        isEmpty = true;
    }

    // Update is called once per frame
    void Update () {
        if (isArrive && isEmpty)
        {
            //玩家坐下
            Player player = GameObject.Find("Characters/Player").GetComponent<Player>();
            if (Input.GetKeyDown(KeyCode.DownArrow) )
            {
                if (gameObject == StaticVar.InteractiveProp)
                {
                    player._transform.position = transform.Find("SitPoint").position;
                    player.GetComponentInChildren<SpriteRenderer>().flipX = transform.Find("BG").GetComponent<SpriteRenderer>().flipX;
                    player._ani.SetBool("IsSit", true);
                  
                }
            }
        }
	}

    public override void ReciveMessage(Message msg)
    {
        base.ReciveMessage(msg);

        if (msg.Command == MyMessageType.Event_EnterChair)
        {
            
            isArrive = true;
           

        }
        else if (msg.Command == MyMessageType.Event_LeaveChair)
        {
            isArrive = false;
            StaticVar.InteractiveProp = null;
        }
      
    }
}
