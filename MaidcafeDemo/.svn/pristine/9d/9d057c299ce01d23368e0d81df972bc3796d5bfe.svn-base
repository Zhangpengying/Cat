using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 角色状态
/// </summary>
public abstract class ActorState
{

    /// <summary>
    /// 状态机类型
    /// </summary>
    public abstract ActorStateType StateType { get; }

    /// <summary>
    /// 进入状态
    /// </summary>
    /// <param name="param"></param>
    public abstract void Enter(params object[] param);

    /// <summary>
    /// 更新状态
    /// </summary>
    public abstract void Update();

    /// <summary>
    /// 退出状态
    /// </summary>
    public abstract void Exit();

}


/// <summary>
/// 角色状态类型
/// </summary>
public enum ActorStateType
{
    //NPC的共用状态
    Idle,                   //待机站立状态

   //女仆的状态类型

    LoopMove,               //来回闲逛
    Hospitality,            //去招呼客人
    TakeFood,               //取菜
    WaitFood,               //等菜
    SendFood,               //送菜
    Talk,                   //吐槽
    Dance,                  //表演
    Training,               //训练
    Transit,                //过渡

    //客人的状态类型
    CustomerIdle,           //默认初始状态
    InSeat,                 //进门入座
    TakeOrder,              //点菜
    PlayComputer,           //等菜（玩电脑）
    Eat,                    //吃饭
    Drink,                  //喝饮料
    Thanks,                 //感谢
    PayMoney,               //结账
    Leave,                  //离店
    Cheers,                 //欢呼

    //玩家状态类型
    Player_Idle,            //闲置状态（当前没有互动的道具和NPC）
    Player_Work,            //经营状态
    Player_Date,            //约会
    Player_Sleep,           //睡觉
    Player_Chivied,         //被RainFa追赶            

    //NPC状态类型
    WaiteDate,             //等待约会
    Date,                  //约会状态
    ProtectRain,           //守护Rain
    CatchPlayer,           //追赶玩家
    IsLock,                //被锁进店里

}
