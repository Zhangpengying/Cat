﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
/// <summary>
/// 来回走动状态
/// </summary>
public class MoveLoopState : ActorState
{
    private Actor _actor;
    private Vector3 Goal = Vector3.back;
    public override ActorStateType StateType
    {
        get
        {
            return ActorStateType.LoopMove;
        }
    }

    public override void Enter(params object[] param)
    {
        //Debug.Log("IdleState Enter");
        _actor = param[0] as Actor;
        if (_actor != null)
        {
            //TimerManager.instance.Invoke(2f, FindNextPoint, true);
            FindNextPoint();
        }
    }
    public override void Update()
    {
        if (Goal != Vector3.back)
        {
            if (_actor != null)
            {
                if (_actor.transform.localPosition == Goal)
                {

                    _actor._ani.SetBool("IsWalk", false);
                    Goal = Vector3.back;
                    TimerManager.instance.Invoke(2f, delegate
                    {
                        if (_actor != null)
                        {
                            FindNextPoint();
                        }
                    });
                }
            }
          
        }
    }
    public void FindNextPoint()
    {
        Goal = StaticVar.VecTranslate(RandomNum());
        
        _actor.AITranslate(_actor.gameObject, Goal);
        _actor._ani.SetBool("IsWalk", true);
    }
    //选取随机位置
    public Transform RandomNum()
    {
        List<Transform> temp1 = new List<Transform>();
        List<Transform> temp2 = new List<Transform>();
        //在当前楼层寻找下一个移动点
        if (_actor.currentFloor == 1)
        {
            foreach (var item in GameObject.Find("FirstFloorItems").GetComponentsInChildren<BoxCollider2D>())
            {
                if (item.transform.localPosition != _actor.transform.localPosition && !temp1.Contains(item.transform))
                {
                    temp1.Add(item.transform);
                }
            }
            int n = Random.Range(0, temp1.Count);
            return temp1[n];
        }
        else
        {
            foreach (var item in GameObject.Find("SecondFloorItems").GetComponentsInChildren<BoxCollider2D>())
            {
                if (item.transform.localPosition != _actor.transform.localPosition && !temp2.Contains(item.transform))
                {
                    temp2.Add(item.transform);
                }
            }
            int n = Random.Range(0, temp2.Count);
            return temp2[n];
        }
       
    }

    public override void Exit()
    {
        _actor = null;
        
    }
}
/// <summary>
/// 招呼客人状态
/// </summary>
public class HospitalityState : ActorState
{
    private Actor _actor;

    public override ActorStateType StateType
    {
        get
        {
            return ActorStateType.Hospitality;
        }
    }

    public override void Enter(params object[] param)
    {
        _actor = param[0] as Actor;
        if (_actor != null)
        {
            //顾客的座位
            Transform temp1 = MessageSend.instance.customerSeat[MessageSend.instance.combine[(Waiter1)_actor]];
            //服务位置
            Transform temp2 = MessageSend.instance.seatToService[temp1];
            //判定女仆当前位置是否与对应的服务位置相同
            if (_actor.transform.localPosition == temp2.localPosition)
            {
                //调整朝向
                _actor._ani.GetComponent<SpriteRenderer>().flipX = temp1.localPosition.x - temp2.localPosition.x < 0 ? false : true;
                //_actor.transform.localScale = MessageSend.instance.combine[(Waiter1)_actor].transform.localPosition.x - _actor.transform.localPosition.x < 0 ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
                WindowManager.instance.Get<JingYing>().ActiveTalk(_actor, MessageSend.instance.combine[(Waiter1)_actor]);
            }
            else
            {
                //播放行走动画
                _actor.PlayAnim("IsWalk");
                //移动
                _actor.AITranslate(_actor.gameObject, StaticVar.VecTranslate(temp2));
            }
        }
    }

    public override void Update()
    {

    }

    public override void Exit()
    {
        _actor = null;

    }
}
/// <summary>
/// 取菜状态
/// </summary>
public class TakeFoodState : ActorState
{
    private Actor _actor;

    public override ActorStateType StateType
    {
        get
        {
            return ActorStateType.TakeFood;
        }
    }

    public override void Enter(params object[] param)
    {
        //Debug.Log("IdleState Enter");
        _actor = param[0] as Actor;
        if (_actor != null)
        {
            //播放走路动画
            _actor.PlayAnim("IsWalk");
            //移动
            //判定楼层
            if (_actor.currentFloor == 2)
            {
                _actor.AIMove(_actor, StaticVar.VecTranslate(GameObject.Find("FirstFloorItems/PayMoney").transform));
                //_actor.path = StaticVar.SetPath(_actor, StaticVar.VecTranslate(GameObject.Find("FirstFloorItems/FrontDesk/PayMoney").transform));
            }
            else
            {
                //设置目标点
                _actor.AITranslate(_actor.gameObject, StaticVar.VecTranslate(GameObject.Find("FirstFloorItems/PayMoney").transform));
            }
        }
    }

    public override void Update()
    {

    }

    public override void Exit()
    {
        _actor = null;

    }
}
/// <summary>
/// 等菜状态
/// </summary>
public class WaitFoodState : ActorState
{
    private Actor _actor;

    public override ActorStateType StateType
    {
        get
        {
            return ActorStateType.WaitFood;
        }
    }

    public override void Enter(params object[] param)
    {
        //Debug.Log("IdleState Enter");
        _actor = param[0] as Actor;
        if (_actor != null)
        {
            _actor._ani.SetBool("IsWalk", false);
            TimerManager.instance.Invoke(2f, delegate {_actor. TransState(_actor, ActorStateType.SendFood); });

        }
    }

    public override void Update()
    {

    }

    public override void Exit()
    {
        _actor = null;

    }
}
/// <summary>
/// 送菜状态
/// </summary>
public class SendFoodState : ActorState
{
    private Actor _actor;

    public override ActorStateType StateType
    {
        get
        {
            return ActorStateType.SendFood;
        }
    }

    public override void Enter(params object[] param)
    {
        //Debug.Log("IdleState Enter");
        _actor = param[0] as Actor;
        if (_actor != null)
        {
            //播放动画
            _actor._ani.SetBool("Bow", false);
            _actor.PlayAnim("TakeFood");
            //移动
            //判断目标楼层
            if (MessageSend.instance.combine[(Waiter1)_actor].currentFloor == 2)
            {
                _actor.AIMove(_actor, StaticVar.VecTranslate(MessageSend.instance.seatToService[MessageSend.instance.customerSeat[MessageSend.instance.combine[(Waiter1)_actor]]]));
                //_actor.path = StaticVar.SetPath(_actor, StaticVar.VecTranslate(MessageSend.instance.seatToService[MessageSend.instance.customerSeat[MessageSend.instance.combine[(Waiter1)_actor]]]));
            }
            else
            {
                //设置目标点
                _actor.AITranslate(_actor.gameObject, StaticVar.VecTranslate(MessageSend.instance.seatToService[MessageSend.instance.customerSeat[MessageSend.instance.combine[(Waiter1)_actor]]]));
            }
        }
    }

    public override void Update()
    {

    }

    public override void Exit()
    {
        _actor = null;

    }
}
/// <summary>
/// 吐槽状态
/// </summary>
public class TalkState : ActorState
{
    private Actor _actor;

    public override ActorStateType StateType
    {
        get
        {
            return ActorStateType.Talk;
        }
    }

    public override void Enter(params object[] param)
    {
        //Debug.Log("IdleState Enter");
        _actor = param[0] as Actor;
        if (_actor != null)
        {
            //TODO 播放动画相关
        }
    }

    public override void Update()
    {

    }

    public override void Exit()
    {
        _actor = null;

    }
}
/// <summary>
/// 表演状态
/// </summary>
public class DancingState : ActorState
{
    private Actor _actor;

    public override ActorStateType StateType
    {
        get
        {
            return ActorStateType.Dance;
        }
    }

    public override void Enter(params object[] param)
    {
        //Debug.Log("IdleState Enter");
        _actor = param[0] as Actor;
        if (_actor != null)
        {
            //TODO 播放动画相关
        }
    }

    public override void Update()
    {

    }

    public override void Exit()
    {
        _actor = null;

    }
}
/// <summary>
/// 训练状态
/// </summary>
public class TrainingState : ActorState
{
    private Actor _actor;

    public override ActorStateType StateType
    {
        get
        {
            return ActorStateType.Training;
        }
    }

    public override void Enter(params object[] param)
    {
        //Debug.Log("IdleState Enter");
        _actor = param[0] as Actor;
        if (_actor != null)
        {
            //TODO 播放动画相关
        }
    }

    public override void Update()
    {

    }

    public override void Exit()
    {
        _actor = null;

    }
}
/// <summary>
/// 顾客默认初始站立状态
/// </summary>
public class CustomerIdleState : ActorState
{
    private Actor _actor;

    public override ActorStateType StateType
    {
        get
        {
            return ActorStateType.CustomerIdle;
        }
    }

    public override void Enter(params object[] param)
    {
        //Debug.Log("IdleState Enter");
        _actor = param[0] as Actor;

        if (_actor != null)
        {

        }
    }

    public override void Update()
    {

    }

    public override void Exit()
    {
        _actor = null;

    }
}
/// <summary>
/// 进门入座状态
/// </summary>
public class InSeatState : ActorState
{
    private Actor _actor;
    Transform tempSeat;
    //楼层
    int floor;

    public override ActorStateType StateType
    {
        get
        {
            return ActorStateType.InSeat;
        }
    }

    public override void Enter(params object[] param)
    {
        //Debug.Log("IdleState Enter");
        _actor = param[0] as Actor;
        //一楼门口的女仆鞠躬
        foreach (var item in MessageSend.instance.firstFloorWaiter)
        {
            if (item._stateType == ActorStateType.Idle)
            {
                item._ani.SetTrigger("Bow");
            }
        }
      
        //设置出生点
        _actor.transform.localPosition = GameObject.Find("FirstFloorItems/CustomerBorn").transform.localPosition;
        //播放行走动画
        _actor.PlayAnim("IsWalk");

        //随机挑选一个空座位 
        if ((MessageSend.instance.firstFloorSeats.Count + MessageSend.instance.secondFloorSeats.Count) != 0)
        {
            //挑选楼层
            floor = SelectFloor();
            //挑座位
            //选了一层
            if (floor == 0)
            {
                tempSeat = FindSeat1();
                _actor.AITranslate(_actor.gameObject, StaticVar.VecTranslate(tempSeat));
            }
            //选了二层
            else
            {
                tempSeat = FindSeat2();
                //位移
                _actor.AIMove(_actor, StaticVar.VecTranslate(tempSeat));
                //_actor.path =  StaticVar.SetPath(_actor, StaticVar.VecTranslate(tempSeat));
               
              
            }
            //保存客人与座位的从属关系
            MessageSend.instance.customerSeat.Add((Customer1)_actor, tempSeat);
        }
    }
    public int SelectFloor()
    {
        int m = Random.Range(0, 2);
        return 0;
    }

    public Transform FindSeat1()
    {
        List<Transform> temp = new List<Transform>();
        foreach (var item in MessageSend.instance.firstFloorSeats)
        {
            if (!MessageSend.instance.customerSeat.ContainsValue(item))
            {
                temp.Add(item);
            }
        }
        int n = Random.Range(0, temp.Count);
        return temp[n];
    }
    public Transform FindSeat2()
    {
        List<Transform> temp = new List<Transform>();
        foreach (var item in MessageSend.instance.secondFloorSeats)
        {
            if (!MessageSend.instance.customerSeat.ContainsValue(item))
            {
                temp.Add(item);
            }
        }
        int n = Random.Range(0, temp.Count);
        return temp[n];
    }

    public override void Update()
    {
        
    }

    public override void Exit()
    {
        _actor = null;

    }
}
/// <summary>
/// 点菜状态
/// </summary>
public class TakerOrderState : ActorState
{
    private Actor _actor;

    public override ActorStateType StateType
    {
        get
        {
            return ActorStateType.TakeOrder;
        }
    }

    public override void Enter(params object[] param)
    {
        //Debug.Log("IdleState Enter");
        _actor = param[0] as Actor;

        if (_actor != null)
        {
            //播放点菜动画
            _actor._ani.SetBool("WaitFood", true);
            _actor.PlayAnim("TakeOrder");
            //点菜
            TakeOrder();
        }
    }

    public override void Update()
    {
       
    }

    //点菜
    public void TakeOrder()
    {
        List<MenuCfg> food = new List<MenuCfg>();
        List<MenuCfg> drink = new List<MenuCfg>();
        //菜品分类
        foreach (var item in MessageSend.instance.CurrentDayMenus)
        {
            if (item.Type == MenuType.Drink)
            {
                drink.Add(item);
            }
            else
            {
                food.Add(item);
            }
        }
        //是否有喜欢的菜
        if (((Customer1)_actor)._loveOrder.Count == 0)
        {
            //选吃的
            int n = food.Count;
            int m = Random.Range(0, n);
            if (((Customer1)_actor).expenditure.ContainsKey(food[m].ID))
            {
                int amount = ((Customer1)_actor).expenditure[food[m].ID];
                amount += 1;
                ((Customer1)_actor).expenditure[food[m].ID] = amount;
            }
            else
            {
                ((Customer1)_actor).expenditure.Add(food[m].ID, 1);
            }
          
            if (StaticVar.ManageMenuData.ContainsKey(food[m]))
            {
                int amount = StaticVar.ManageMenuData[food[m]];
                amount += 1;
                StaticVar.ManageMenuData[food[m]] = amount;

            }
            else
            {
                StaticVar.ManageMenuData.Add(food[m], 1);

            }
            //选喝的
            int x = drink.Count;
            int y = Random.Range(0, x);
            if (((Customer1)_actor).expenditure.ContainsKey(drink[y].ID))
            {
                int amount = ((Customer1)_actor).expenditure[drink[y].ID];
                amount += 1;
                ((Customer1)_actor).expenditure[drink[y].ID] = amount;
            }
            else
            {
                ((Customer1)_actor).expenditure.Add(drink[y].ID, 1);
            }
            if (StaticVar.ManageMenuData.ContainsKey(drink[y]))
            {
                int amount = StaticVar.ManageMenuData[drink[y]];
                amount += 1;
                StaticVar.ManageMenuData[drink[y]] = amount;

            }
            else
            {
                StaticVar.ManageMenuData.Add(drink[y], 1);

            }
        }
        else
        {
            foreach (var item in ((Customer1)_actor)._loveOrder)
            {
                ((Customer1)_actor).expenditure.Add(item.ID, item.Price);
                if (StaticVar.ManageMenuData.ContainsKey(item))
                {
                    int amount = StaticVar.ManageMenuData[item];
                    amount += 1;
                    StaticVar.ManageMenuData[item] = amount;

                }
                else
                {
                    StaticVar.ManageMenuData.Add(item, 1);
                }
            }
        }
    }
    public override void Exit()
    {
        _actor = null;

    }
}
/// <summary>
/// 等菜状态（玩电脑）
/// </summary>
public class PlayComputerState : ActorState
{
    private Actor _actor;

    public override ActorStateType StateType
    {
        get
        {
            return ActorStateType.PlayComputer;
        }
    }

   

    public override void Enter(params object[] param)
    {
        //Debug.Log("IdleState Enter");
        _actor = param[0] as Actor;

        if (_actor != null)
        {
            //播放等菜动画
            _actor._ani.SetBool("TakeOrder", false);
            _actor.PlayAnim("WaitFood");
            Waiter1 waiter = ReturnWaiter();
            if (waiter != null)
            {
                waiter.TransState(waiter, ActorStateType.Hospitality);
            }
        }
    }

    public Waiter1 ReturnWaiter()
    {
        List<Waiter1> temp = new List<Waiter1>(MessageSend.instance.combine.Keys);
        for (int i = 0; i < temp.Count; i++)
        {
            if (MessageSend.instance.combine[temp[i]] == (Customer1)_actor)
            {
                return temp[i];
            }
        }
        return null;
    }

    public override void Update()
    {

    }

    public override void Exit()
    {
        _actor = null;

    }
}
/// <summary>
/// 吃饭状态
/// </summary>
public class EatState : ActorState
{
    private Actor _actor;

    public override ActorStateType StateType
    {
        get
        {
            return ActorStateType.Eat;
        }
    }

    public override void Enter(params object[] param)
    {
        //Debug.Log("IdleState Enter");
        _actor = param[0] as Actor;

        if (_actor != null)
        {
            //播吃饭动画
            _actor._ani.SetBool("IsWalk", false);
            _actor.PlayAnim("Eat");
            //去付钱
            TimerManager.instance.Invoke(StaticVar.eatingTime, PayMoney);
        }
    }
    public void PayMoney()
    {
        _actor._ani.SetBool("Eat", false);
        GameObject obj = GameObject.Find("FirstFloorItems/" + ((Customer1)_actor)._loveOrder);
        Object.Destroy(obj);
        _actor.TransState(_actor, ActorStateType.PayMoney);
    }

    public override void Update()
    {
      
    }

    public override void Exit()
    {
        _actor = null;

    }
}
/// <summary>
/// 喝水状态
/// </summary>
public class DrinkState : ActorState
{
    private Actor _actor;

    public override ActorStateType StateType
    {
        get
        {
            return ActorStateType.Drink;
        }
    }

    public override void Enter(params object[] param)
    {
        //Debug.Log("IdleState Enter");
        _actor = param[0] as Actor;

        if (_actor != null)
        {
            //播喝水动画
            _actor._ani.SetBool("IsWalk", false);
            _actor.PlayAnim("Drink");
            //销毁水杯
            Object.Destroy(GameObject.Find("FirstFloorItems/" + ((Customer1)_actor)._loveOrder));
            //去付钱
            TimerManager.instance.Invoke(StaticVar.eatingTime, delegate {
                _actor._ani.SetBool("Drink", false);
                _actor.TransState(_actor, ActorStateType.PayMoney);
            });
           
        }
    }
    public override void Update() {}

    public override void Exit()
    {
        _actor = null;

    }
}
/// <summary>
/// 感谢状态
/// </summary>
public class ThanksState  : ActorState
{
    private Actor _actor;

    public override ActorStateType StateType
    {
        get
        {
            return ActorStateType.Thanks;
        }
    }

    public override void Enter(params object[] param)
    {
        //Debug.Log("IdleState Enter");
        _actor = param[0] as Actor;

        if (_actor != null)
        {
            //播感谢动画
            _actor.PlayAnim("Thanks");
            _actor._ani.SetBool("TakeOrder", false);
            //喝水
            //TimerManager.instance.Invoke(StaticVar.thanksTime, delegate { _actor.TransState(_actor, ActorStateType.Drink); });
            TimerManager.instance.Invoke(StaticVar.thanksTime, delegate { _actor.TransState(_actor, ActorStateType.Eat); });

        }
    }
    public override void Update() {}

    public override void Exit()
    {
        _actor = null;
    }
}
/// <summary>
/// 结账状态
/// </summary>
public class PayMoneyState : ActorState
{
    private Actor _actor;

    public override ActorStateType StateType
    {
        get
        {
            return ActorStateType.PayMoney;
        }
    }

    public override void Enter(params object[] param)
    {
        _actor = param[0] as Actor;
        if (_actor != null)
        {
            //播放动画
            _actor._ani.SetBool("TakeOrder", false);
            _actor._ani.SetBool("WaitFood", false);
            
            //调整顾客的层级
            _actor._ani.GetComponent<SpriteRenderer>().sortingLayerName = "Characters";
            _actor._ani.GetComponent<SpriteRenderer>().sortingOrder = 100 + MessageSend.instance.customer.IndexOf((Customer1)_actor);
           
            //起身后的缓冲
            TimerManager.instance.Invoke(StaticVar.situpBufferTime,delegate {
                _actor.PlayAnim("IsWalk");
                //移动
                //判定客人当前所在楼层
                if (_actor.currentFloor == 1)
                {
                    _actor.AITranslate(_actor.gameObject, StaticVar.VecTranslate(GameObject.Find("FirstFloorItems/PayMoney").transform));
                }
                else
                {
                    _actor.AIMove(_actor, StaticVar.VecTranslate(GameObject.Find("FirstFloorItems/PayMoney").transform));
                }
            });
        }
    }

    public override void Update()
    {
     
    }

    public override void Exit()
    {
        _actor = null;

    }
}
/// <summary>
/// 欢呼状态
/// </summary>
public class CheersState : ActorState
{
    private Actor _actor;

    public override ActorStateType StateType
    {
        get
        {
            return ActorStateType.Cheers;
        }
    }

    public override void Enter(params object[] param)
    {
        //Debug.Log("IdleState Enter");
        _actor = param[0] as Actor;

        if (_actor != null)
        {
            _actor.GetComponent<PolyNavAgent>().SetDestination(MessageSend.instance.stayPointList[4].localPosition);
        }
    }

    public override void Update()
    {
       
    }

    public override void Exit()
    {
        _actor = null;

    }
}

/// <summary>
/// 基本站立状态
/// </summary>
public class IdleState : ActorState
{
    private Actor _actor;

    public override ActorStateType StateType
    {
        get
        {
            return ActorStateType.Idle;
        }
    }

    public override void Enter(params object[] param)
    {
        //Debug.Log("IdleState Enter");
        _actor = param[0] as Actor;

        if (_actor != null)
        {
            //判定楼层
            if (_actor.currentFloor == 1)
            {
                //设置出生点 
                int n = MessageSend.instance.firstFloorWaiter.IndexOf((Waiter1)_actor);
                _actor.transform.localPosition = MessageSend.instance.bornCfgs[602+n].Position;
                _actor._ani.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                //设置出生点
                int n = MessageSend.instance.secondFloorWaiter.IndexOf((Waiter1)_actor);
                _actor.transform.localPosition = GameObject.Find("SecondFloorItems/WelcomePoint" + (n + 1)).transform.localPosition;
                _actor._ani.GetComponent<SpriteRenderer>().flipX = false;
            }
        }
    }

    public override void Update()
    {

    }

    public override void Exit()
    {
        _actor = null;

    }
}
/// <summary>
/// 离店状态
/// </summary>
public class LeaveState : ActorState
{
    private Actor _actor;

    public override ActorStateType StateType
    {
        get
        {
            return ActorStateType.Leave;
        }
    }

    public override void Enter(params object[] param)
    {
        //Debug.Log("IdleState Enter");
        _actor = param[0] as Actor;

        if (_actor != null)
        {
            _actor._ani.SetBool("IsWalk", true);

            //离店
            _actor.AITranslate(_actor.gameObject, StaticVar.VecTranslate(GameObject.Find("FirstFloorItems/CustomerBorn").transform));
            //店长切换收钱状态
            Player player = StaticVar.player;
            player.TransState(player, ActorStateType.Player_Idle) ;
        }
    }

    public override void Update()
    {

    }

    public override void Exit()
    {
        _actor = null;

    }
}
/// <summary>
/// 过渡状态（女仆服务结束后恢复到站立的过渡阶段）
/// </summary>
public class TransitState : ActorState
{
    private Actor _actor;

    public override ActorStateType StateType
    {
        get
        {
            return ActorStateType.Transit;
        }
    }

    public override void Enter(params object[] param)
    {
        //Debug.Log("IdleState Enter");
        _actor = param[0] as Actor;

        if (_actor != null)
        {
            //先恢复站立状态
            _actor._ani.SetBool("TakeFood", false);
            TimerManager.instance.Invoke(1f, delegate
            {
                //删除对应关系
                MessageSend.instance.combine.Remove((Waiter1)_actor);

                //检测当前楼层是否有客人在座位上等待
                List<Customer1> temp = new List<Customer1>();
                foreach (var item in MessageSend.instance.customer)
                {
                    if (!MessageSend.instance.combine.ContainsValue(item) && item._stateType == ActorStateType.PlayComputer)
                    {
                        if (item.currentFloor == _actor.currentFloor)
                        {
                            temp.Add(item);
                        }
                    }
               
                }
                if (temp.Count == 0)
                {
                    //恢复闲置状态
                    _actor.TransState(_actor, ActorStateType.LoopMove);
                }
                else
                {
                    MessageSend.instance.combine.Add((Waiter1)_actor, temp[0]);
                    _actor.TransState(_actor, ActorStateType.Hospitality);
                }
            });  

        }
    }
  

    public override void Update()
    {

    }

    public override void Exit()
    {
        _actor = null;

    }
}
/// <summary>
/// 店长的基本站立状态
/// </summary>
public class PlayerIdleState : ActorState
{
    private Actor _actor;

    public override ActorStateType StateType
    {
        get
        {
            return ActorStateType.Player_Idle;
        }
    }

    public override void Enter(params object[] param)
    {
        //Debug.Log("IdleState Enter");
        _actor = param[0] as Actor;

        if (_actor != null)
        {
            //_actor.transform.localPosition = new Vector3(4.8f, -4.52f, 0);
            //播放站立动画
            _actor._ani.SetBool("IsWalk", false);
            _actor._ani.SetBool("IsSit", false);
            _actor._ani.SetBool("IsSleep", false);
        }
    }

    public override void Update()
    {

    }

    public override void Exit()
    {
        _actor = null;

    }
}
/// <summary>
/// 工作状态（经营）
/// </summary>
public class PlayerWorkState : ActorState
{
    private Actor _actor;

    public override ActorStateType StateType
    {
        get
        {
            return ActorStateType.Player_Work;
        }
    }

    public override void Enter(params object[] param)
    {
        //Debug.Log("IdleState Enter");
        _actor = param[0] as Actor;

        if (_actor != null)
        {
            _actor.GetComponent<Player>().IsLockPlayer = true;
        }
    }

    public override void Update()
    {

    }

    public override void Exit()
    {
        _actor = null;

    }
}


/// <summary>
/// 玩家睡觉状态
/// </summary>
public class PlayerSleepState : ActorState
{
    private Actor _actor;

    public override ActorStateType StateType
    {
        get
        {
            return ActorStateType.Player_Sleep;
        }
    }

    public override void Enter(params object[] param)
    {
        //Debug.Log("IdleState Enter");
        _actor = param[0] as Actor;

        if (_actor != null)
        {


        }
    }

    public override void Update()
    {

    }

    public override void Exit()
    {
        _actor = null;

    }
}
/// <summary>
/// 玩家被追赶状态
/// </summary>
public class PlayerChiviedState : ActorState
{
    private Actor _actor;

    public override ActorStateType StateType
    {
        get
        {
            return ActorStateType.Player_Chivied;
        }
    }

    public override void Enter(params object[] param)
    {
        //Debug.Log("IdleState Enter");
        _actor = param[0] as Actor;

        if (_actor != null)
        {
            if (GameObject.Find("Environment/Events/StreetToHome")!=null)
            {
                GameObject.Find("Environment/Events/StreetToHome").GetComponent<ToHome>().openDoor = false;
            }
        }
    }

    public override void Update()
    {

    }

    public override void Exit()
    {
        _actor = null;

    }
}
/// <summary>
/// 玩家的约会状态
/// </summary>
public class PlayerDateState : ActorState
{
    private Actor _actor;

    public override ActorStateType StateType
    {
        get
        {
            return ActorStateType.Player_Date;
        }
    }

    public override void Enter(params object[] param)
    {
        //Debug.Log("IdleState Enter");
        _actor = param[0] as Actor;

        if (_actor != null)
        {

        }
    }

    public override void Update()
    {

    }

    public override void Exit()
    {
        _actor = null;

    }
}
/// <summary>
/// 等待约会状态
/// </summary>
public class WaiteDateState : ActorState
{
    private Actor _actor;

    public override ActorStateType StateType
    {
        get
        {
            return ActorStateType.WaiteDate;
        }
    }

    public override void Enter(params object[] param)
    {
        //Debug.Log("IdleState Enter");
        _actor = param[0] as Actor;

        if (_actor != null)
        {
            //设定NPC位置
            _actor._transform.localPosition = GameObject.Find("Events/TriggerDate").transform.localPosition;

        }
    }

    public override void Update()
    {

    }

    public override void Exit()
    {
        _actor = null;

    }
}
/// <summary>
/// 约会状态
/// </summary>
public class DateState : ActorState
{
    private Actor _actor;

    public override ActorStateType StateType
    {
        get
        {
            return ActorStateType.Date;
        }
    }

    public override void Enter(params object[] param)
    {
        //Debug.Log("IdleState Enter");
        _actor = param[0] as Actor;

        if (_actor != null)
        {
           
            //设置角色位置
            ((InteractiveNpc)_actor).player = GameObject.Find("Characters/Player").GetComponent<Player>();

            if (((InteractiveNpc)_actor).player.IsLockPlayer)
            {

            }
            else
            {
                _actor.transform.localPosition = StaticVar.LastGateway - new Vector3(0.5f, 0, 0);
            }
        }
    }

    public override void Update()
    {
        if (_actor != null)
        {
            NpcControl((InteractiveNpc)_actor);
        }
    }

    public override void Exit()
    {
        _actor = null;

    }
    public void NpcControl(InteractiveNpc npc)
    {
       
        if (npc.player != null)
        {
            if (!npc.player._ani.GetBool("IsSit"))
            {
                npc.dis = Vector3.Distance(npc.player._transform.localPosition, npc.transform.localPosition);
                //小于最小距离则停止移动
                if (npc.dis <= npc.minFollowDis)
                {
                    npc._isMoving = false;
                }
                //大于最大距离则追随移动
                else if (npc.dis >= npc.maxFollowDis)
                {
                    npc._isMoving = true;
                }

                if (npc._isMoving)
                {
                    npc._ani.SetBool("IsSit", false);
                    npc._ani.SetBool("IsWalk", true);
                    Vector3 horizontal = (npc.player._transform.localPosition - npc.transform.localPosition).normalized;
                    npc.GetComponent<SpriteRenderer>().flipX = (horizontal.x < 0) ? false : true;
                    npc._transform.position += horizontal * Time.deltaTime * npc._moveSpeed;
                }
                else
                {
                    npc._ani.SetBool("IsWalk", false);
                    npc._ani.SetBool("IsSit", false);
                }
            }
            //玩家坐下的情况
            else
            {
                //npc走到玩家对面椅子位置
                Transform emptyChair = null;
                foreach (var item in StaticVar.InteractiveProp.transform.parent.GetComponentsInChildren<Chair>())
                {
                    if (item.gameObject != StaticVar.InteractiveProp)
                    {
                        emptyChair = item.transform;
                    }
                }

                float dis1 = Vector3.Distance(emptyChair.Find("SitPoint").position, npc.transform.position);
                if (dis1 <= 0.2f)
                {
                    npc._isMoving = false;

                    npc.GetComponent<SpriteRenderer>().flipX = !npc.player.GetComponent<SpriteRenderer>().flipX;
                    npc._ani.SetBool("IsWalk", false);
                    npc._ani.SetBool("IsSit", true);
                }
                else
                {
                    npc._isMoving = true;
                    Vector3 horizontal = (emptyChair.Find("SitPoint").transform.position - npc.transform.position).normalized;
                    npc.GetComponent<SpriteRenderer>().flipX = (horizontal.x < 0) ? false : true;
                    npc._transform.position += horizontal * Time.deltaTime * npc._moveSpeed;
                }
            }
        }
    }
}
/// <summary>
/// 守护Rain状态
/// </summary>
public class ProtectRainState : ActorState
{
    private Actor _actor;

    public override ActorStateType StateType
    {
        get
        {
            return ActorStateType.ProtectRain;
        }
    }

    public override void Enter(params object[] param)
    {
        //Debug.Log("IdleState Enter");
        _actor = param[0] as Actor;

        if (_actor != null)
        {
            //设置出生点
            _actor._transform.localPosition = GameObject.Find("Environment/Events/RainFa/BornPoint").transform.localPosition;
            
        }
    }

    public override void Update()
    {

    }

    public override void Exit()
    {
        _actor = null;

    }
}
/// <summary>
/// 追赶玩家状态
/// </summary>
public class CatchPlayerState : ActorState
{
    private Actor _actor;

    public override ActorStateType StateType
    {
        get
        {
            return ActorStateType.CatchPlayer;
        }
    }

    public override void Enter(params object[] param)
    {
        //Debug.Log("IdleState Enter");
        _actor = param[0] as Actor;

        if (_actor != null)
        {
            //播放追赶动画
            _actor._ani.SetBool("IsWalk", true);
        }
    }

    public override void Update()
    {
        ControlRainFa((InteractiveNpc)_actor);
    }
    public override void Exit()
    {
        _actor = null;
    }
    public void ControlRainFa(InteractiveNpc RainFa)
    {
        if (RainFa._isMoving)
        {
            RainFa.dis = Vector3.Distance(RainFa.player._transform.localPosition, RainFa.transform.localPosition);
            Vector3 horizontal = (RainFa.player._transform.localPosition - RainFa.transform.localPosition).normalized;
            RainFa.GetComponent<SpriteRenderer>().flipX = (horizontal.x < 0) ? false : true;
            RainFa._transform.position += horizontal * Time.deltaTime * RainFa._moveSpeed;
            //抓住玩家
            if (RainFa.dis <= 1.5f)
            {
                RainFa._isMoving = false;
                RainFa._ani.SetBool("IsWalk", false);
                //删除约会任务
                StaticVar.MessageSendToFungus("DeleteDate", RainFa.player);
            } 
        }
       
    }
}
/// <summary>
/// 被锁定店里状态
/// </summary>
public class IsLockState : ActorState
{
    private Actor _actor;

    public override ActorStateType StateType
    {
        get
        {
            return ActorStateType.IsLock;
        }
    }

    public override void Enter(params object[] param)
    {
        //Debug.Log("IdleState Enter");
        _actor = param[0] as Actor;

        if (_actor != null)
        {
          
        }
    }

    public override void Update()
    {

    }

    public override void Exit()
    {
        _actor = null;

    }
}
