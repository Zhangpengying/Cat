﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBase{

    private Player player;
	// Use this for initialization
	void Awake () {
        //将当前类注册到管理类中，接收消息
        PlayerManager.instance.RegisterReceiver(this);
        player = transform.parent.GetComponent<Player>();
    }
	

	// Update is called once per frame
	void Update () {
      
    }

    public void AutoTrigge()
    {
        //触发自动触发事件
        SendCustomerMessage(MyMessageType.Type_Event, MyMessageType.Event_Talk2, player);
    }

    //接收消息
    public override void ReciveMessage(Message msg)
    {
        base.ReciveMessage(msg);
        //判断消息类型
        if (msg.Command == MyMessageType.UI_AddScore)
        {
          
        }
    }

    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!player.IsLockPlayer)
        {
            if (collision.name == "HomeToStreet")
            {
                StaticVar.InteractiveProp = collision.gameObject;
                SendCustomerMessage(MyMessageType.Type_Event, MyMessageType.Event_OpenHomeToStreet, player);
            }
            //触发测试事件
            if (collision.name == "Talk1")
            {
                StaticVar.InteractiveProp = collision.gameObject;
                SendCustomerMessage(MyMessageType.Type_Event, MyMessageType.Event_Talk1, player);
            }
            //触发经营前准备工作
            else if (collision.transform.parent.name == "Menu")
            {
                StaticVar.InteractiveProp = collision.transform.parent.gameObject;
               
                SendCustomerMessage(MyMessageType.Type_Event, MyMessageType.Event_AdjustAllMenus, player);
            }
            //触发结束中场休息
            else if (collision.name == "EndRest")
            {
                StaticVar.InteractiveProp = collision.gameObject;
                
                SendCustomerMessage(MyMessageType.Type_Event, MyMessageType.Event_EndRest, player);
            }
            else if (collision.name == "StreetToHome")
            {
                StaticVar.InteractiveProp = collision.gameObject;
                SendCustomerMessage(MyMessageType.Type_Event, MyMessageType.Event_OpenStreetToHome, player);
            }
            else if (collision.transform.parent.name == "Bed")
            {
                StaticVar.InteractiveProp = collision.transform.parent.gameObject;
               
                SendCustomerMessage(MyMessageType.Type_Event, MyMessageType.Event_Bed, player);
            }

            else if (collision.transform.parent.tag == "Chair" && collision.name == "SitPoint")
            {
                StaticVar.InteractiveProp = collision.transform.parent.gameObject;
                SendCustomerMessage(MyMessageType.Type_Event, MyMessageType.Event_EnterChair, player);
            }
            else if (collision.name == "WaiterStay")
            {
                StaticVar.InteractiveProp = collision.transform.parent.gameObject;
                SendCustomerMessage(MyMessageType.Type_Event, MyMessageType.Event_StartEvent, player);
            }
            //初始房间//////////////////////////////////////
            #region
            else if (collision.transform.parent.name == "道具1")
            {
                StaticVar.InteractiveProp = collision.transform.parent.gameObject;
                SendCustomerMessage(MyMessageType.Type_Event, MyMessageType.Event_TouchProperty1, player);
            }
            else if (collision.transform.parent.name == "道具2")
            {
                StaticVar.InteractiveProp = collision.transform.parent.gameObject;
                SendCustomerMessage(MyMessageType.Type_Event, MyMessageType.Event_TouchProperty2, player);
            }
            else if (collision.transform.parent.name == "道具3")
            {
                StaticVar.InteractiveProp = collision.transform.parent.gameObject;
                SendCustomerMessage(MyMessageType.Type_Event, MyMessageType.Event_TouchProperty3  , player);
            }
            else if (collision.transform.parent.name == "道具4")
            {
                StaticVar.InteractiveProp = collision.transform.parent.gameObject;
                SendCustomerMessage(MyMessageType.Type_Event, MyMessageType.Event_TouchProperty4, player);
            }
            else if (collision.transform.parent.name == "莉莉丝")
            {
                StaticVar.InteractiveProp = collision.transform.parent.gameObject;
                SendCustomerMessage(MyMessageType.Type_Event, MyMessageType.Event_TouchNPC1, player);
            }
            else if (collision.transform.parent.name == "莉莉安")
            {
                StaticVar.InteractiveProp = collision.transform.parent.gameObject;

                SendCustomerMessage(MyMessageType.Type_Event, MyMessageType.Event_TouchNPC2, player);
            }
            else if (collision.transform.parent.name == "出口")
            {
                StaticVar.InteractiveProp = collision.transform.parent.gameObject;
                SendCustomerMessage(MyMessageType.Type_Event, MyMessageType.Event_TouchGateWay1, player);
            }
            
            #endregion

            //村庄/////////////////////////////
            #region
            else if (collision.transform.parent.name == "村长")
            {
                StaticVar.InteractiveProp = collision.transform.parent.gameObject;
                SendCustomerMessage(MyMessageType.Type_Event, MyMessageType.Event_TouchNPC_CunZhang, player);
            }
            else if (collision.transform.parent.name == "铁匠")
            {
                StaticVar.InteractiveProp = collision.transform.parent.gameObject;
                SendCustomerMessage(MyMessageType.Type_Event, MyMessageType.Event_TouchNPC_TieJiang, player);
            }
            else if (collision.transform.parent.name == "占卜师")
            {
                StaticVar.InteractiveProp = collision.transform.parent.gameObject;
                SendCustomerMessage(MyMessageType.Type_Event, MyMessageType.Event_TouchNPC_ZhanBuShi, player);
            }
            else if (collision.transform.parent.name == "仓库")
            {
                StaticVar.InteractiveProp = collision.transform.parent.gameObject;
                SendCustomerMessage(MyMessageType.Type_Event, MyMessageType.Event_TouchProperty5, player);
            }
            else if (collision.transform.parent.name == "血包")
            {
              SendCustomerMessage(MyMessageType.Type_Event, MyMessageType.Event_AddHP, player);
            }
            else if (collision.transform.parent.name == "怪物")
            {
                SendCustomerMessage(MyMessageType.Type_Event, MyMessageType.Event_Monster, player);
            }
            else if (collision.transform.parent.name == "森林障碍1")
            {
                StaticVar.InteractiveProp = collision.transform.parent.gameObject;
                SendCustomerMessage(MyMessageType.Type_Event, MyMessageType.Event_TouchObstacles1, player);
            }
            else if (collision.transform.parent.name == "森林障碍2")
            {
                StaticVar.InteractiveProp = collision.transform.parent.gameObject;
                SendCustomerMessage(MyMessageType.Type_Event, MyMessageType.Event_TouchObstacles2, player);
            }
            else if (collision.transform.parent.name == "草药")
            {
                StaticVar.InteractiveProp = collision.transform.parent.gameObject;
                SendCustomerMessage(MyMessageType.Type_Event, MyMessageType.Event_TouchProperty8, player);
            }
            else if (collision.transform.parent.name == "拉尔夫")
            {
                StaticVar.InteractiveProp = collision.transform.parent.gameObject;
                SendCustomerMessage(MyMessageType.Type_Event, MyMessageType.Event_TouchNPC_LaErFu, player);
            }
            #endregion


        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
       
    }

    public void OnTriggerExit2D(Collider2D collision)
    { 
        if (collision.name == "HomeToStreet")
        {
            SendCustomerMessage(MyMessageType.Type_Event, MyMessageType.Event_CloseHomeToStreet,player);
            
        }
        if (collision.name == "Talk1")
        {
            SendCustomerMessage(MyMessageType.Type_Event, MyMessageType.Event_LeaveTalk1, player);

        }

        else if (collision.name == "StreetToHome")
        {
            SendCustomerMessage(MyMessageType.Type_Event, MyMessageType.Event_CloseStreetToHome,player);
        }
        else if (collision.name == "StreetToCoffee")
        {
            SendCustomerMessage(MyMessageType.Type_Event, MyMessageType.Event_CloseStreetToCoffee, player);
        }
        else if (collision.name == "CoffeeToStreet")
        {
            SendCustomerMessage(MyMessageType.Type_Event, MyMessageType.Event_CloseCoffeeToStreet, player);
        }
        else if (collision.name == "WaiterStay")
        {
            StaticVar.InteractiveProp =null;
            SendCustomerMessage(MyMessageType.Type_Event, MyMessageType.Event_LeaveEvent, player);
        }
        else if (collision.transform.parent.tag == "Chair" && collision.name == "SitPoint")
        {
            SendCustomerMessage(MyMessageType.Type_Event, MyMessageType.Event_LeaveChair, player);
        }
        else if (collision.transform.parent.name == "Menu")
        {
            SendCustomerMessage(MyMessageType.Type_Event, MyMessageType.Event_LeaveAllMenus, player);
            StaticVar.InteractiveProp = null;
        }
        //触发结束中场休息
        else if (collision.name == "EndRest")
        {
            EndRest.canEnd = false;
            StaticVar.InteractiveProp = null;
        }
        //离开床
        else if (collision.transform.parent.name == "Bed")
        {
            SendCustomerMessage(MyMessageType.Type_Event, MyMessageType.Event_LeaveBed, player);
            StaticVar.InteractiveProp = null;
        }

        //初始房间///////////////////////////
        #region
        else if (collision.transform.parent.name == "道具1")
        {
            SendCustomerMessage(MyMessageType.Type_Event, MyMessageType.Event_LeaveProperty1, player);
            StaticVar.InteractiveProp = null;
        }
        else if (collision.transform.parent.name == "道具2")
        {
            SendCustomerMessage(MyMessageType.Type_Event, MyMessageType.Event_LeaveProperty2, player);
            StaticVar.InteractiveProp = null;
        }
        else if (collision.transform.parent.name == "道具3")
        {
            SendCustomerMessage(MyMessageType.Type_Event, MyMessageType.Event_LeaveProperty3, player);
            StaticVar.InteractiveProp = null;
        }

        else if (collision.transform.parent.name == "道具4")
        {
            SendCustomerMessage(MyMessageType.Type_Event, MyMessageType.Event_LeaveProperty4, player);
            StaticVar.InteractiveProp = null;
        }
        else if (collision.transform.parent.name == "莉莉丝")
        {
            SendCustomerMessage(MyMessageType.Type_Event, MyMessageType.Event_LeaveNPC1, player);
            StaticVar.InteractiveProp = null;
        }
        else if (collision.transform.parent.name == "莉莉安")
        {
            SendCustomerMessage(MyMessageType.Type_Event, MyMessageType.Event_LeaveNPC2, player);
            StaticVar.InteractiveProp = null;
        }
        #endregion

        //村庄///////////////////////////
        #region
        else if (collision.transform.parent.name == "村长")
        {
            SendCustomerMessage(MyMessageType.Type_Event, MyMessageType.Event_LeaveNPC_CunZhang, player);
            StaticVar.InteractiveProp = null;
        }
        else if (collision.transform.parent.name == "铁匠")
        {
            SendCustomerMessage(MyMessageType.Type_Event, MyMessageType.Event_LeaveNPC_TieJiang, player);
            StaticVar.InteractiveProp = null;
        }
        else if (collision.transform.parent.name == "仓库")
        {
            SendCustomerMessage(MyMessageType.Type_Event, MyMessageType.Event_LeaveProperty5, player);
            StaticVar.InteractiveProp = null;
        }
        else if (collision.transform.parent.name == "占卜师")
        {
            SendCustomerMessage(MyMessageType.Type_Event, MyMessageType.Event_LeaveNPC_ZhanBuShi, player);
            StaticVar.InteractiveProp = null;
        }
        else if (collision.transform.parent.name == "草药")
        {
            SendCustomerMessage(MyMessageType.Type_Event, MyMessageType.Event_LeaveProperty8, player);
            StaticVar.InteractiveProp = null;
        }
        else if (collision.transform.parent.name == "拉尔夫")
        {
            SendCustomerMessage(MyMessageType.Type_Event, MyMessageType.Event_LeaveNPC_LaErFu, player);
            StaticVar.InteractiveProp = null;
        }
        #endregion
    }
}
