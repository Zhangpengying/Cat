using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 循环触发事件，在玩家当前的金钱超过1500时执行
/// </summary>
public class EventText3 : MonoBehaviour {

    //测试事件3
    public int ID_TextEvent3 = 220;
    //该事件执行次数
    public int DoNum_TextEvent3 = 0;

    private Hashtable Infor_TextEvent3 = new Hashtable();

    private Action tempAct;
    // Use this for initialization
    void Start()
    {

        //事件信息记录
        tempAct = TextEvent3;
        Infor_TextEvent3.Add("DoNum", DoNum_TextEvent3);
        Infor_TextEvent3.Add("Action", tempAct);

        //注册事件
        transform.GetComponent<TalkEvents>().ToStreetEvents.Add(ID_TextEvent3, Infor_TextEvent3);

        StaticVar.AddEvents(ID_TextEvent3, Infor_TextEvent3);


    }

    public void TextEvent3()
    {
        DoNum_TextEvent3 += 1;
        MessageSend.instance.Events[ID_TextEvent3]["DoNum"] = DoNum_TextEvent3;
        StaticVar.MessageSendToFungus("TextEvent3", StaticVar.player);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
