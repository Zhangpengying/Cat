using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 单次触发事件，只在执行次数为0时执行
/// </summary>
public class EventText1 : MonoBehaviour {

    //测试事件1
    public int ID_TextEvent1 = 200;
    //该事件执行次数
    public int DoNum_TextEvent1 = 0;
    
   
    private Hashtable Infor_TextEvent1 = new Hashtable();

    private Action tempAct;
    // Use this for initialization
    void Start () {

        //事件信息记录
        tempAct = TextEvent1;
        Infor_TextEvent1.Add("DoNum", DoNum_TextEvent1);
        Infor_TextEvent1.Add("Action", tempAct);

        //注册事件
        transform.GetComponent<TalkEvents>().ToStreetEvents.Add(ID_TextEvent1, Infor_TextEvent1);

        StaticVar.AddEvents(ID_TextEvent1, Infor_TextEvent1);

        
    }
	
    public void TextEvent1()
    {
        DoNum_TextEvent1 += 1;
        MessageSend.instance.Events[ID_TextEvent1]["DoNum"] = DoNum_TextEvent1;
        StaticVar.MessageSendToFungus("TextEvent1", StaticVar.player);
    }

	// Update is called once per frame
	void Update () {
		
	}
}
