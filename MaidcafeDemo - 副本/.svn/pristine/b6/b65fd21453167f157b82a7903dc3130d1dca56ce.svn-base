using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoBase : MonoBehaviour
{
    
    //发送消息
    public static void SendCustomerMessage(byte type, int command, object content)
    {
        MessageCenter.SendMessage(type, command, content);
    }
    public static void SendCustomerMessage(Message msg)
    { 
        MessageCenter.SendMessage(msg);
    }

    //接收消息
    public virtual void ReciveMessage(Message msg){}

   
}
