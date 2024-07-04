using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageBase<T> : MonoBase where T  : MonoBase
{
    public static T instance;
    //管理的消息接收者
    public List<MonoBase> ReceiveList = new List<MonoBase>();
    //当前管理类接受的消息类型
    protected byte messageType;

    //返回消息类型
    protected void  Awake()
    {
        instance = this as T;
        //设置消息类型
       
         messageType = SetMessageType();

        //将当前的管理类添加到消息中心列表中
        MessageCenter.Managers.Add(this);

    }
    //必须实现的返回消息类型
    protected virtual byte SetMessageType()
    {
        return MyMessageType.Type_UI;
    }


    //注册消息监听
    public void RegisterReceiver(MonoBase mb)
    {
        if (!ReceiveList.Contains(mb))
        {
            ReceiveList.Add(mb);
        }
    }

    public override void ReciveMessage(Message msg)
    {
        base.ReciveMessage(msg);
        //判断接收到的消息类型是否匹配
        if (msg.Type != messageType)
        {
            return;
        }
        foreach (MonoBase item in ReceiveList)
        {
            item.ReciveMessage(msg);
        }
    }


    
}
