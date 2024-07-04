using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEventTrig : MonoBase {

    private Player player;
    // Use this for initialization
    void Awake()
    {
        //将当前类注册到管理类中，接收消息
        PlayerManager.instance.RegisterReceiver(this);
        player = transform.parent.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {


        //触发售卖NPC
        if (collision.transform.parent.name == "SellNPC1")
        {
            StaticVar.InteractiveProp = collision.transform.parent.gameObject;
         
            SendCustomerMessage(MyMessageType.Type_Event, MyMessageType.Event_Bed, player);
        }
        //触发售卖NPC
        else if (collision.transform.parent.name == "SellNPC")
        {
            StaticVar.InteractiveProp = collision.transform.parent.gameObject;
          
            SendCustomerMessage(MyMessageType.Type_Event, MyMessageType.Event_OpenStore, player);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //触发售卖NPC
        if (collision.transform.parent.name == "SellNPC")
        {
            SendCustomerMessage(MyMessageType.Type_Event, MyMessageType.Event_LeaveStore, player);
            StaticVar.InteractiveProp = null;
        }
    }
}
