using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waiter1 : Actor
{
    /// <summary>
    /// 初始化状态机
    /// </summary>
    protected override void InitState()
    {
        _actorStateDic[ActorStateType.LoopMove] = new MoveLoopState();
        _actorStateDic[ActorStateType.Hospitality] = new HospitalityState();
        _actorStateDic[ActorStateType.TakeFood] = new TakeFoodState();
        _actorStateDic[ActorStateType.WaitFood] = new WaitFoodState();
        _actorStateDic[ActorStateType.SendFood] = new SendFoodState();
        _actorStateDic[ActorStateType.Talk] = new TalkState();
        _actorStateDic[ActorStateType.Dance] = new DancingState();
        _actorStateDic[ActorStateType.Training] = new TrainingState();
        _actorStateDic[ActorStateType.Idle] = new IdleState();
        _actorStateDic[ActorStateType.Transit] = new TransitState();
    }

    /// <summary>
    /// 初始化当前状态
    /// </summary>
    protected override void InitCurState()
    {
       
    }
    
    void Start()
    {
       //transform.parent.Find("Manager").gameObject.SetActive(true);
    }

  
   
    //public void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Waiter1 waiter = transform.GetComponent<Waiter1>();
        
    //    //触发事件
    //    if ( MessageSend.instance.customerSeat.Count !=0 && MessageSend.instance.combine.ContainsKey(waiter))
    //    {
    //        Customer1 customer = MessageSend.instance.combine[waiter];

    //        //顾客点菜
    //        if (collision.transform == MessageSend.instance.seatToService[MessageSend.instance.customerSeat[customer]] && _curState == _actorStateDic[ActorStateType.Hospitality])
    //        {
    //            //调整朝向
    //            _ani.GetComponent<SpriteRenderer>().flipX = customer.transform.localPosition.x - StaticVar.VecTranslate(MessageSend.instance.seatToService[MessageSend.instance.customerSeat[customer]]) .x < 0 ? false : true;
    //            WindowManager.instance.Get<JingYing>().ActiveTalk(waiter, customer);
    //        }
    //        // 
    //        if (collision.transform == MessageSend.instance.seatToService[MessageSend.instance.customerSeat[customer]] && _curState == _actorStateDic[ActorStateType.SendFood])
    //        {
    //            //调整朝向
    //            _ani.SetBool("TakeFood", false);
    //            //上菜
    //            _ani.SetTrigger("PutDown");
    //        }
    //        //女仆等菜
    //        else if (collision.gameObject.name == "PayMoney" && waiter._stateType == ActorStateType.TakeFood)
    //        {
    //            //切换状态
    //            TransState(this, ActorStateType.WaitFood);
                
    //        }
    //    }

    //}
    
}
