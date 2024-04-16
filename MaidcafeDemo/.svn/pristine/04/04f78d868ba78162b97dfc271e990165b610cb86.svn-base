using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveNpc : Actor
{
    //最小跟随距离
    public float minFollowDis = 1.5f;
    //最大跟随距离
    public float maxFollowDis = 3f;
    //互动玩家
    public Player player;
    //NPC与玩家之间的距离
    public float dis;
   


    public void Start()
    {
      
    }
    


    protected override void InitState()
    {
        _actorStateDic[ActorStateType.WaiteDate] = new WaiteDateState();
        _actorStateDic[ActorStateType.Date] = new DateState();
        _actorStateDic[ActorStateType.ProtectRain] = new ProtectRainState();
        _actorStateDic[ActorStateType.CatchPlayer] = new CatchPlayerState();
        _actorStateDic[ActorStateType.IsLock] = new IsLockState();
    }

    protected override void InitCurState()
    {
        //_curState = _actorStateDic[ActorStateType.WaiteDate];
        //_stateType = ActorStateType.WaiteDate;
        //_curState.Enter(this);
    }

   
    
}
