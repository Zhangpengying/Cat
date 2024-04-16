﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Customer1 : Actor
{
    //顾客喜欢的食物
    public List<MenuCfg> _loveOrder = new List<MenuCfg>();
    //顾客消费情况
    public Dictionary<int,int> expenditure = new Dictionary<int, int>() ;
    //消费信息
    public Dictionary<Customer1, Transform> expendData = new Dictionary<Customer1, Transform>();
    /// <summary>
    /// 初始化状态机
    /// </summary>
    protected override void InitState()
    {
        _actorStateDic[ActorStateType.CustomerIdle] = new CustomerIdleState();
        _actorStateDic[ActorStateType.InSeat] = new InSeatState();
        _actorStateDic[ActorStateType.TakeOrder] = new TakerOrderState();
        _actorStateDic[ActorStateType.Eat] = new EatState();
        _actorStateDic[ActorStateType.PayMoney] = new PayMoneyState();
        _actorStateDic[ActorStateType.Cheers] = new CheersState();
        _actorStateDic[ActorStateType.Leave] = new LeaveState();
        _actorStateDic[ActorStateType.PlayComputer] = new PlayComputerState();
        _actorStateDic[ActorStateType.Drink] = new DrinkState();
        _actorStateDic[ActorStateType.Thanks] = new ThanksState();

    }

    /// <summary>
    /// 初始化当前状态
    /// </summary>
    protected override void InitCurState()
    {
    }
  
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (MessageSend.instance.customerSeat.Count != 0)
        {
            if (collision.transform == MessageSend.instance.customerSeat[this] && _stateType == ActorStateType.InSeat)
            {
                //坐下前的缓冲
                _ani.SetBool("IsWalk", false);
                TimerManager.instance.Invoke(StaticVar.sitdownBufferTime, delegate { 
                //选择一个女仆服务
                if (HaveRelaxWaiter())
                {
                    Waiter1 waiter = ChoiceWaiter();
                    if (waiter != null)
                    {
                        MessageSend.instance.combine.Add(waiter, this);
                    }
                }
                //调整朝向
                Transform tempseat = MessageSend.instance.customerSeat[this];
                tempseat.parent.GetComponent<Chair>().isEmpty = false;
                _ani.GetComponent<SpriteRenderer>().flipX = tempseat.localPosition.x - MessageSend.instance.seatToService[tempseat].localPosition.x > 0 ? false : true;
                //transform.localScale = tempseat.localPosition.x - MessageSend.instance.seatToService[tempseat].localPosition.x < 0 ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
                TransState(this, ActorStateType.PlayComputer);
                //调整客人的层级在桌子之下
                transform.Find("BG").GetComponent<SpriteRenderer>().sortingLayerName = "NearView";
                    transform.Find("BG").GetComponent<SpriteRenderer>().sortingOrder  = 20;
                });
            }
            else if (collision.gameObject.name == "PayMoney" && _stateType == ActorStateType.PayMoney)
            {
                //顾客到达结账点结账，购买周边，两秒后离开店里
                int n = MessageSend.instance.CurrentDayCom.Count;
                //判定选了几件
                int o = Random.Range(0, n+1);
                //判定选哪几件
                for (int i = 0; i < o; i++)
                {
                    int m = Random.Range(0, n);
                    if (expenditure.ContainsKey(MessageSend.instance.CurrentDayCom[m].ID))
                    {
                        int amount = expenditure[MessageSend.instance.CurrentDayCom[m].ID];
                        amount += 1;
                        expenditure[MessageSend.instance.CurrentDayCom[m].ID] = amount;
                    }
                    else
                    {
                        expenditure.Add(MessageSend.instance.CurrentDayCom[m].ID,1);
                    }
                    if (StaticVar.ManageComData.ContainsKey(MessageSend.instance.CurrentDayCom[m]))
                    {
                        int amount = StaticVar.ManageComData[MessageSend.instance.CurrentDayCom[m]];
                        amount += 1;
                        StaticVar.ManageComData[MessageSend.instance.CurrentDayCom[m]] = amount;
                    }
                    else
                    {
                        StaticVar.ManageComData.Add(MessageSend.instance.CurrentDayCom[m], 1);

                    }
                }

                //显示结账信息
                ShowExpendData(this);

                
                _ani.SetBool("IsWalk", false);
                ////店长收钱
                //GameObject obj = GameObject.Find("UI/Canvas/JingYing/Money/Text");
                //if (obj != null)
                //{
                //    obj.GetComponent<ChangeValue>().collectMoney = true;
                //}
            }
        }
        //顾客上楼二楼门口女仆鞠躬
        if (collision.name == "CollisionPoint" && _stateType == ActorStateType.InSeat)
        {
            foreach (var item in MessageSend.instance.secondFloorWaiter)
            {
                if (item._stateType == ActorStateType.Idle)
                {
                    item._ani.SetTrigger("Bow");
                }
            }
        }

    }
    //判定当前楼层是否有闲置女仆(有则返回true)
    public bool HaveRelaxWaiter()
    {
        int m = 0;
        //判断楼层
        if (currentFloor == 1)
        {
            foreach (var item in MessageSend.instance.firstFloorWaiter)
            {

                //女仆处于忙碌状态
                if (!(item._curState == item._actorStateDic[ActorStateType.Idle] || item._curState == item._actorStateDic[ActorStateType.LoopMove]))
                {
                    m++;
                }
            }
            //所有女仆都处于忙碌状态
            if (m == MessageSend.instance.firstFloorWaiter.Count)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            foreach (var item in MessageSend.instance.secondFloorWaiter)
            {

                //女仆处于忙碌状态
                if (!(item._curState == item._actorStateDic[ActorStateType.Idle] || item._curState == item._actorStateDic[ActorStateType.LoopMove]))
                {
                    m++;
                }
            }
            //所有女仆都处于忙碌状态
            if (m == MessageSend.instance.secondFloorWaiter.Count)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
       
        
    }
    //挑选女仆来服务
    public Waiter1 ChoiceWaiter()
    {
        int m = Random.Range(0, MessageSend.instance.waiter.Count);

        Waiter1 item = MessageSend.instance.waiter[m];

        if (item._curState == item._actorStateDic[ActorStateType.Idle] || 
            item._curState == item._actorStateDic[ActorStateType.LoopMove] )
        {
            if (item.currentFloor == currentFloor)
            {
                return item;
            }
            else
            {
                return ChoiceWaiter();
            }
        }
        else
        {
            return ChoiceWaiter();
        }

    }
    //结账离店
    public void Leave()  
    {
       
        TransState(this, ActorStateType.Leave);
        TimerManager.instance.Invoke(2f, delegate {
            if (gameObject!=null)
            
            //传递有空位
            TimerManager.instance.Invoke(StaticVar.waitEmptyTime, delegate { StaticVar.haveEmptySeat = true; });
            //销毁该客人
            MessageSend.instance.customer.Remove(this);
            MessageSend.instance.customerSeat[this].parent.GetComponent<Chair>().isEmpty = true;
            MessageSend.instance.customerSeat.Remove(this);
            ActorManager.instance.RemoveActor(_uid);
            //检测是否全部客人离店以及经营时间是否到达
            if (StaticVar.currentManageTime>= StaticVar.totalOperatingtime)
            {
                if (MessageSend.instance.customer.Count == 0)
                {
                    WindowManager.instance.Open<ExitWnd>().Initialize();
                    Time.timeScale = 0;
                }
            }
        });
    }
    //结账信息
    private void ShowExpendData(Customer1 cus)
    {
        Transform data = CreatExpenditureData(cus).Find("Data");
        
        List<int> dataKeys = new List<int>(expenditure.Keys);
        foreach (var item in dataKeys)
        {
            int n = dataKeys.IndexOf(item);
            float m = data.GetComponent<Animation>().GetClip("ExpenditureData").length * n ;
            TimerManager.instance.Invoke(m+0.01f*n, delegate {
                data.GetComponent<Animation>().Play("ExpenditureData");
                
                if (n <= 1)
                {
                    data.GetComponent<Text>().text = MessageSend.instance.menuCfgs[item].MenuName + "*" + expenditure[item];
                    data.parent.Find("Money/Text").GetComponent<Text>().text  = MessageSend.instance.menuCfgs[item].Price.ToString();
                }
                else
                {
                    data.GetComponent<Text>().text = MessageSend.instance.commodityCfg[item].Name + "*" + expenditure[item];
                    data.parent.Find("Money/Text").GetComponent<Text>().text = (MessageSend.instance.commodityCfg[item].Price * expenditure[item]).ToString();
                    
                }

            });
        }
        int Sum = 0;
        foreach (var item in dataKeys)
        {
            int n = dataKeys.IndexOf(item);
            if (n<=1)
            {
                Sum += MessageSend.instance.menuCfgs[item].Price;
            }
            else
            {
                Sum += MessageSend.instance.commodityCfg[item].Price * expenditure[item];
            }
        }
        GameObject.Find("UI/Canvas/JingYing/AllMoney/Text").GetComponent<ChangeValue>().damage = Sum;
        GameObject.Find("UI/Canvas/JingYing/AllMoney/Text").GetComponent<ChangeValue>().collectMoney = true;
        GameObject.Find("UI/Canvas/JingYing/AllMoney/Text").GetComponent<ChangeValue>().startValue = StaticVar.player.PlayerMoney;
        TimerManager.instance.Invoke(expenditure.Count * data.GetComponent<Animation>().GetClip("ExpenditureData").length + 1, delegate { Leave();DestroyExpenditureData(this); } );
    }
    //创建结账信息框
    private Transform CreatExpenditureData(Customer1 cus)
    {
        GameObject go = GameObject.Instantiate(Resources.Load("Prefabs/UIWnd/ExpenditureData")) as GameObject;
        go.name = cus.name;
        go.transform.SetParent(GameObject.Find("UI/Canvas/JingYing/DataList").transform);
        //位置改变
        //go.GetComponent<SayFollow>().target = cus.transform;
        //go.GetComponent<SayFollow>().UpdatePosition();
        go.transform.localPosition = new Vector3(495, -55, 0);
        go.transform.localScale = Vector3.one;
        go.SetActive(true);
        expendData.Add(cus, go.transform);
        return go.transform;
    }

    //销毁结账信息框
    private void DestroyExpenditureData(Customer1 cus)
    {
        Destroy(expendData[cus].gameObject);
        expendData.Remove(cus);
    }
}
