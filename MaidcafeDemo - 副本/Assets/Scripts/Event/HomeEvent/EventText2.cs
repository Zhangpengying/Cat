using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 循环触发事件，在玩家当前的金钱超过1000时执行
/// </summary>
public class EventText2 : MonoBehaviour {

    //测试事件2
    public int ID_TextEvent2 = 210;
    //该事件执行次数
    public int DoNum_TextEvent2 = 0;

    private Hashtable Infor_TextEvent2 = new Hashtable();

    private Action tempAct;
    // Use this for initialization
    void Start()
    {

        //事件信息记录
        tempAct = TextEvent2;
        Infor_TextEvent2.Add("DoNum", DoNum_TextEvent2);
        Infor_TextEvent2.Add("Action", tempAct);

        //注册事件
        transform.GetComponent<TalkEvents>().ToStreetEvents.Add(ID_TextEvent2, Infor_TextEvent2);

        StaticVar.AddEvents(ID_TextEvent2, Infor_TextEvent2);


    }

    public void TextEvent2()
    {
        StaticVar.MessageSendToFungus("TextEvent2", StaticVar.player);
        DoNum_TextEvent2 += 1;
        MessageSend.instance.Events[ID_TextEvent2]["DoNum"] = DoNum_TextEvent2;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
