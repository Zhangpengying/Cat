using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoEvent1 : MonoBehaviour {

    //测试事件1
    public int ID_TextEvent1 = 110;
    //该事件执行次数
    public int DoNum_TextEvent1 = 0;


    private Hashtable Infor_TextEvent1 = new Hashtable();

    private Action tempAct;
    // Use this for initialization
    void Start()
    {

        //事件信息记录
        tempAct = Jump;
        Infor_TextEvent1.Add("DoNum", DoNum_TextEvent1);
        Infor_TextEvent1.Add("Action", tempAct);

        //注册事件
        transform.GetComponent<AutoEvents>().TextEvents.Add(ID_TextEvent1, Infor_TextEvent1);

        StaticVar.AddEvents(ID_TextEvent1, Infor_TextEvent1);
        

    }

    void Jump()
    {
        TimerManager.instance.Invoke(0.5f, delegate { 
        if (StaticVar.player!= null)
        {
            StaticVar.player.transform.localPosition = StaticVar.player.transform.localPosition + new Vector3(0, 0.5f, 0);
        }
        });
    }

    // Update is called once per frame
    void Update () {
		
	}
}
