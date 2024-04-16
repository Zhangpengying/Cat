using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TalkEvents : MonoBase
{
   
    public Player player;
    
    private Action tempAct;

    //该道具上绑定的所有事件
    public Dictionary<int, Hashtable> ToStreetEvents = new Dictionary<int, Hashtable>();

    // Use this for initialization
    void Start()
    {
        EventManager.instance.RegisterReceiver(this);
    }


    private void Update()
    {
      
    }
    
    public override void ReciveMessage(Message msg)
    {
        base.ReciveMessage(msg);
        if (msg.Command == MyMessageType.Event_Talk1)
        {
            player = msg.Content as Player;
            //遍历A，根据ID读取A里对应的Value赋值给B
            foreach (var item in MessageSend.instance.Events)
            {
                if (ToStreetEvents.ContainsKey(item.Key))
                {
                    ToStreetEvents[item.Key] = item.Value;
                }
            }
            //遍历B，筛选当前满足触发条件的事件添加到临时字典C
            Dictionary<int, Hashtable> ActiveEvents = new Dictionary<int, Hashtable>();
            foreach (var item in ToStreetEvents)
            {
                switch (item.Key)
                {
                    case 200:
                        if ((int)item.Value["DoNum"] ==0 )
                        {
                            //满足触发条件添加进新的字典
                            ActiveEvents.Add(item.Key,item.Value);
                        }
                        break;
                    case 210:
                        if (player.PlayerMoney>600)
                        {
                            //满足触发条件添加进新的字典
                            ActiveEvents.Add(item.Key, item.Value);
                        }
                        break;
                    case 220:
                        if (player.PlayerMoney > 900)
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
            if (ActiveEvents.Count!=0)
            {
                StaticVar.PreferenceEvent(ActiveEvents);
            }

        }
        else if (msg.Command == MyMessageType.Event_LeaveTalk1)
        {
            StaticVar.InteractiveProp = null;
        }
    }

   
}
