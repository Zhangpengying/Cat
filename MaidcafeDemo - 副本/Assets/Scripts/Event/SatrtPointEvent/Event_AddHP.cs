using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_AddHP : MonoBase
{

    public bool isArrive = false;
   

    // Use this for initialization
    void Start()
    {
        EventManager.instance.RegisterReceiver(this);

  
    }

    // Update is called once per frame
    void Update()
    {
        if (isArrive)
        {
            if (StaticVar.player.PlayerHP<3)
            {
                StaticVar.player.PlayerHP += 1;
                Destroy(transform.parent.gameObject);
            }
            isArrive = false;

        }
    }

    public override void ReciveMessage(Message msg)
    {
        base.ReciveMessage(msg);
        if (msg.Command == MyMessageType.Event_AddHP)
        {
            isArrive = true;
        }
       
    }

   
}
