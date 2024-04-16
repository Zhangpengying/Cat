using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class JingYing : BaseWnd
{
    public Button WaiterSay;
    public Button CustomerSay;
    
    public void Initialize()
    {
        WindowManager.instance.Close<BasicInforWnd>();
        //地图物品加载
        MapInitialize();
        //创建玩家
        ActorManager.instance.CreateActorCon();
        StaticVar.player.TransState(StaticVar.player, ActorStateType.Player_Work);
        //设置出生点
        StaticVar.player = (Player)ActorManager.instance.GetActor(0);
        StaticVar.player.transform.position = MessageSend.instance.bornCfgs[601].Position;
        StaticVar.player.transform.Find("BG").GetComponent<SpriteRenderer>().sortingLayerName = "MidView";
        //创建女仆
        CreatWaiters();
        //清空纠正 
        StaticVar.currentManageTime = 0;
        MessageSend.instance.customer.Clear();
        StaticVar.ManageMenuData.Clear();
        StaticVar.ManageComData.Clear();

        _transform.gameObject.AddComponent<ManageControl>();
        //创建客人
        TimerManager.instance.Invoke(2f,  delegate { StaticVar.haveEmptySeat = true; GameObject.Find("Characters/Player").GetComponent<Player>().CreatCustomers(); });
    }

    //创建女仆
    public void CreatWaiters()
    {
        for (int i = 0; i < MessageSend.instance.currentDayWaiters.Count; i++)
        {
            ActorManager.instance.CreatWaiter(MessageSend.instance.currentDayWaiters[i]);
            //
        }
    }

    //地图初始化
    void MapInitialize()
    {
        //保存特殊位置
        //一楼
        foreach (var item in GameObject.Find("FirstFloorItems").GetComponentsInChildren<BoxCollider2D>())
        {
            MessageSend.instance.stayPointList.Add(item.transform);
            if (item.tag == "Seat")
            {
                if (!MessageSend.instance.firstFloorSeats.Contains(item.transform))
                {
                    MessageSend.instance.firstFloorSeats.Add(item.transform);
                }
                
            }
        }
        //二楼
        foreach (var item in GameObject.Find("SecondFloorItems").GetComponentsInChildren<BoxCollider2D>())
        {
            MessageSend.instance.stayPointList.Add(item.transform);
            if (item.tag == "Seat")
            {
                if (!MessageSend.instance.secondFloorSeats.Contains(item.transform))
                {
                    MessageSend.instance.secondFloorSeats.Add(item.transform);
                }
            }
        }
        //保存座位与服务位置关系
        for (int i = 0; i < MessageSend.instance.firstFloorSeats.Count; i++)
        {
            MessageSend.instance.seatToService.Add(MessageSend.instance.firstFloorSeats[i], MessageSend.instance.firstFloorSeats[i].parent.Find("WaiterStay"));
        }
        for (int i = 0; i < MessageSend.instance.secondFloorSeats.Count; i++)
        {
            MessageSend.instance.seatToService.Add(MessageSend.instance.secondFloorSeats[i], MessageSend.instance.secondFloorSeats[i].parent.Find("WaiterStay"));
        }
        
    }

    //激活吐槽
    public void ActiveTalk(Actor act1, Actor act2)
    {
        //激活对话
        MessageManager.instance.CreatMessageBox(act1);

        //先鞠躬
        act1._ani.SetBool("IsWalk", false);
        act1._ani.SetTrigger("Bow");
        //两人开始点菜
        TimerManager.instance.Invoke(1.7f, delegate
        {
            MessageManager.instance.DestoryMessageBox(act1);
            act1.PlayAnim("TakeOrder");
            act2.TransState(act2, ActorStateType.TakeOrder);
        });

        //点完菜后鞠躬
        TimerManager.instance.Invoke(StaticVar.takeOrderTime, delegate
        {
            act1._ani.SetBool("TakeOrder", false);
            act1._ani.SetTrigger("Bow");
            act2.TransState(act2, ActorStateType.PlayComputer);
        });

        TimerManager.instance.Invoke(StaticVar.takeFoodTime, delegate
        {
            act1.TransState(act1, ActorStateType.TakeFood);
        });

    }
   


}

public class ManageControl : MonoBehaviour
{
    public float temptime = 1f;
    private void Start()
    {
        CreateDIY();
        transform.Find("AllMoney/Text").GetComponent<Text>().text = StaticVar.player.PlayerMoney.ToString();
    }
    private void Update()
    {
        ManageTimeCon();
        
    }

    //创建之前创建的DIY物品
    void CreateDIY()
    {
        foreach (KeyValuePair<Property, Vector3> kvp in MessageSend.instance.CreatPropertysInfo)
        {
            GameObject property = Instantiate(Resources.Load("Prefabs/Items/DIYItems/" + kvp.Key.PropertyName) as GameObject);
            property.name = kvp.Key.PropertyName;
            property.transform.SetParent(GameObject.Find("DIYCreatPropertys").transform);
            Building b = property.GetComponent<Building>();
            b.propertyID = kvp.Key.ID;
            b.state = BuildingStates.Build;
            b.transform.position = kvp.Value;
        }
    }
    //经营时间控制
    void ManageTimeCon()
    {
       
        temptime -= Time.deltaTime;
        if (temptime <= 0 )
        {
            StaticVar.currentManageTime += 1;
            temptime = 1f;
        }
    
        transform.Find("ManageTime/Text1").GetComponent<Text>().text = (StaticVar.totalOperatingtime- StaticVar.currentManageTime).ToString();
        if ( StaticVar.currentManageTime >= StaticVar.totalOperatingtime)
        {
            transform.Find("ManageTime/Text2").GetComponent<Text>().text = "经营结束";
            temptime = 100000000000000;
        }
    }
}
