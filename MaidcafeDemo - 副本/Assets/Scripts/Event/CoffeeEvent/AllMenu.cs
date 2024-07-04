using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllMenu : MonoBase
{
    public Player player;
   
    private Hashtable infor = new Hashtable();
    public bool isArrive = false;
    private Action tempAct;
    //事件ID
    public int ID_AllMenu = 300;
    //该事件执行次数
    public int DoNum_AllMenu = 0;
    private Hashtable Infor_AllMenu = new Hashtable();

    //该道具上绑定的所有事件
    public Dictionary<int, Hashtable> MenuEvents = new Dictionary<int, Hashtable>();

    // Use this for initialization
    void Start()
    {
        EventManager.instance.RegisterReceiver(this);

        //事件信息记录
        tempAct = AllMenuEvent;
        Infor_AllMenu.Add("DoNum", DoNum_AllMenu);
        Infor_AllMenu.Add("Action", tempAct);

        //注册事件
        MenuEvents.Add(ID_AllMenu, Infor_AllMenu);

        StaticVar.AddEvents(ID_AllMenu, Infor_AllMenu);
    }

    // Update is called once per frame
    void Update () {
        if (isArrive)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (GameObject.Find("Environment/Events/Tips") != null)
                {
                    Destroy(GameObject.Find("Environment/Events/Tips"));
                }
                //进入互动
                if (!player.IsLockPlayer)
                {
                    StaticVar.MessageSendToFungus(transform.parent.name, player);
                }
            }
        }
	}

    public override void ReciveMessage(Message msg)
    {
        base.ReciveMessage(msg);
        player = msg.Content as Player;
        if (msg.Command == MyMessageType.Event_AdjustAllMenus)
        {
            player = msg.Content as Player;
            //遍历A，根据ID读取A里对应的Value赋值给B
            foreach (var item in MessageSend.instance.Events)
            {
                if (MenuEvents.ContainsKey(item.Key))
                {
                    MenuEvents[item.Key] = item.Value;
                }
            }
            ((Action)MenuEvents[ID_AllMenu]["Action"])();
            isArrive = true;
        }
        else if(msg.Command == MyMessageType.Event_LeaveAllMenus)
        {
            if (GameObject.Find("Environment/Events/Tips") != null)
            {
                Destroy(GameObject.Find("Environment/Events/Tips"));
            }
            isArrive = false;
        }
    }

    public void AllMenuEvent()
    {
        
        GameObject.Find("UI").GetComponent<GlobalVariable>().ActiveTips();
    }
}
