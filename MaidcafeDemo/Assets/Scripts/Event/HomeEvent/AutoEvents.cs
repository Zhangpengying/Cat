﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AutoEvents : MonoBase
{
    public Player player;

    private Action tempAct;

    //该道具上绑定的所有事件
    public Dictionary<int, Hashtable> TextEvents = new Dictionary<int, Hashtable>();
  
    // Use this for initialization
    void Start()
    {
        EventManager.instance.RegisterReceiver(this);
    }
    public override void ReciveMessage(Message msg)
    {
        base.ReciveMessage(msg);
        if (msg.Command == MyMessageType.Event_Talk2)
        {
            player = msg.Content as Player;
            //遍历A，根据ID读取A里对应的Value赋值给B
            foreach (var item in MessageSend.instance.Events)
            {
                if (TextEvents.ContainsKey(item.Key))
                {
                    TextEvents[item.Key] = item.Value;
                }
            }
            //遍历B，筛选当前满足触发条件的事件添加到临时字典C
            Dictionary<int, Hashtable> ActiveEvents = new Dictionary<int, Hashtable>();
            foreach (var item in TextEvents)
            {
                switch (item.Key)
                {
                    case 110:
                        if (player.PlayerMoney>200)
                        {
                            //满足触发条件添加进新的字典
                            ActiveEvents.Add(item.Key, item.Value);
                        }
                        break;
                    case 120:
                        if (player.PlayerMoney > 500)
                        {
                            //满足触发条件添加进新的字典
                            ActiveEvents.Add(item.Key, item.Value);
                        }
                        break;
                 
                    default:
                        break;
                }

            }

            //遍历C，找ID最小的事件执行
            if (ActiveEvents.Count != 0)
            {
                StaticVar.PreferenceEvent(ActiveEvents);
            }

        }
        else 
        {
            StaticVar.InteractiveProp = null;
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
