using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 发送消息
/// </summary>
public class MessageCenter
{
    //管理类集合
    public static List<MonoBase> Managers = new List<MonoBase>();

    //发送消息
    public static void SendMessage(byte type, int command, object content)
    {
        Message msg = new Message(type,command,content);
        SendMessage(msg);
    }
    public static void SendMessage(Message msg)
    {
        if (Managers.Count!=0)
        {
            foreach (var item in Managers)
            {
                item.ReciveMessage(msg);
            }
        }
        
    }
}
