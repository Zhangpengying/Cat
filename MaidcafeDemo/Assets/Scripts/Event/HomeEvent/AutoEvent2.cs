using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoEvent2 : MonoBehaviour {

    //事件ID
    public int ID_TextEvent2 = 120;
    //该事件执行次数
    public int DoNum_TextEvent2 = 0;


    private Hashtable Infor_TextEvent2 = new Hashtable();

    private Action tempAct;
    // Use this for initialization
    void Start()
    {
        //事件信息记录
        tempAct = Move;
        Infor_TextEvent2.Add("DoNum", DoNum_TextEvent2);
        Infor_TextEvent2.Add("Action", tempAct);

        //注册事件
        transform.GetComponent<AutoEvents>().TextEvents.Add(ID_TextEvent2, Infor_TextEvent2);

        StaticVar.AddEvents(ID_TextEvent2, Infor_TextEvent2);


    }

    void Move()
    {
        StaticVar.player.AITranslate(StaticVar.player.gameObject, StaticVar.player.transform.localPosition + new Vector3(-0.5f, 0, 0));
    }

    // Update is called once per frame
    void Update () {
		
	}
}
