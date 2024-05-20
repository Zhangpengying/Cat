using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : Singleton<ActorManager>
{

    /// <summary>
    /// 所有玩家的角色列表
    /// </summary>
    public Dictionary<int, Actor> _actorDic = new Dictionary<int, Actor>();

    // Update is called once per frame
    public void Update()
    {
        UpdateActor();
        
    }

    /// <summary>
    /// 更新角色状态
    /// </summary>
    private void UpdateActor()
    {
        var enumerator = _actorDic.GetEnumerator();
        while (enumerator.MoveNext())
        {
            enumerator.Current.Value.UpdateState();
        }
        enumerator.Dispose();
    }


    //创建女仆
    public void CreatWaiter(WaiterCfg cfg)
    {
        Actor actor = null;
        if (!_actorDic.TryGetValue(cfg.ID, out actor))
        {

            GameObject go = (Object.Instantiate(Resources.Load("Prefabs/Characters/" + cfg.Name))) as GameObject;
            go.transform.SetParent(GameObject.Find("Characters").transform);
            
            actor = go.GetComponent<Waiter1>();
            actor.enabled = true;
            MessageSend.instance.waiter.Add((Waiter1)actor);
            ////为楼层分配女仆
            //int temp = MessageSend.instance.currenthavewaiters.Count/2;
            ////固定二楼
            //int temp = MessageSend.instance.currenthavewaiters.Count;
            //固定一楼
            MessageSend.instance.firstFloorWaiter.Add((Waiter1)actor);
            actor.currentFloor = 1;

            //if (MessageSend.instance.secondFloorWaiter.Count<temp)
            //{
            //    MessageSend.instance.secondFloorWaiter.Add((Waiter1)actor);
            //    actor.currentFloor = 2;
            //}
            //else
            //{
            //    MessageSend.instance.firstFloorWaiter.Add((Waiter1)actor);
            //    actor.currentFloor = 1;
            //}
          
           
            actor._uid = cfg.ID;
            actor._name = cfg.Name;
            actor.name = actor._name;
            actor._moveSpeed = cfg.MoveSpeed;
            _actorDic[cfg.ID] = actor;

            actor._curState = actor._actorStateDic[cfg.Behavior];
            actor._stateType = actor._curState.StateType;
            actor._curState.Enter(actor);


        }
        else
        {
            Debug.Log(cfg.Name + "已经存在");
        }
    }

    //创建客人
    public void CreatCustomer(CustomerCfg cfg)
    {
        Actor actor = null;
        if (!_actorDic.TryGetValue(cfg.ID, out actor))
        {
            GameObject go = (Object.Instantiate(Resources.Load("Prefabs/Characters/" + cfg.Name))) as GameObject;
            go.transform.SetParent(GameObject.Find("Characters").transform);           
            actor = go.GetComponent<Customer1>();         
            MessageSend.instance.customer.Add((Customer1)actor);

            actor._uid = cfg.ID;
            actor._name = cfg.Name;
            actor.name = actor._name;
            actor._moveSpeed = cfg.MoveSpeed;
            //喜好菜品的添加
            if (cfg.LoveOrder1 != 0)
            {
                if (!((Customer1)actor)._loveOrder.Contains(MessageSend.instance.menuCfgs[cfg.LoveOrder1]))
                {
                    ((Customer1)actor)._loveOrder.Add(MessageSend.instance.menuCfgs[cfg.LoveOrder1]);
                }
                if (cfg.LoveOrder2 != 0)
                {
                    if (!((Customer1)actor)._loveOrder.Contains(MessageSend.instance.menuCfgs[cfg.LoveOrder2]))
                    {
                        ((Customer1)actor)._loveOrder.Add(MessageSend.instance.menuCfgs[cfg.LoveOrder2]);
                    }
                    if (cfg.LoveOrder3 != 0)
                    {
                        if (!((Customer1)actor)._loveOrder.Contains(MessageSend.instance.menuCfgs[cfg.LoveOrder3]))
                        {
                            ((Customer1)actor)._loveOrder.Add(MessageSend.instance.menuCfgs[cfg.LoveOrder3]);
                        }

                        if (cfg.LoveOrder4!= 0)
                        {
                            if (!((Customer1)actor)._loveOrder.Contains(MessageSend.instance.menuCfgs[cfg.LoveOrder4]))
                            {
                                ((Customer1)actor)._loveOrder.Add(MessageSend.instance.menuCfgs[cfg.LoveOrder4]);
                            }
                        }
                    }
                }

            }
          
            
            _actorDic[cfg.ID] = actor;

            actor._curState = actor._actorStateDic[cfg.Behavior];
            actor._stateType = actor._curState.StateType;
            actor._curState.Enter(actor);


        }
        else
        {
            Debug.Log(cfg.Name + "已经存在");
        }
    }

    //创建玩家
    public void CreatPlayer(PlayerCfg cfg)
    {
        Actor actor = null;
        if (!_actorDic.TryGetValue(cfg.ID, out actor))
        {
            GameObject go = (Object.Instantiate(Resources.Load("Prefabs/Characters/" + cfg.Name))) as GameObject;
            go.transform.SetParent(GameObject.Find("Characters").transform);

            actor = go.GetComponent<Player>();
            StaticVar.player = actor as Player;
            actor._uid = cfg.ID;
            actor._name = cfg.Name;
            actor.name = actor._name;
            actor._moveSpeed = cfg.MoveSpeed;
            _actorDic[cfg.ID] = actor;
            actor._curState = actor._actorStateDic[cfg.Behavior];
            actor._stateType = actor._curState.StateType;
            actor._curState.Enter(actor);
        }
        else
        {
            Debug.Log(cfg.Name + "已经存在");
        }
    }

  

    /// <summary>
    /// 创建角色2（转场景或者其他，角色不需要初始化）
    /// </summary>
    /// <param name="cfg"></param>
    public void CreateActorCon()
    {
        Actor actor;
        GameObject go = (Object.Instantiate(Resources.Load("Prefabs/Characters/" + StaticVar.PlayerAttribute["ActorName"]))) as GameObject;
        go.transform.SetParent(GameObject.Find("Characters").transform);
        actor = go.GetComponent<Actor>();
        //设置玩家进入场景的位置

        actor._transform.localPosition = StaticVar.LastGateway;
        //玩家属性
        StaticVar.player = (Player)actor;
        if (StaticVar.PlayerAttribute != null)
        {
            actor._uid = (int)StaticVar.PlayerAttribute["ActorID"];
            actor._name = (string)StaticVar.PlayerAttribute["ActorName"];

            actor.name = actor._name;
            actor._moveSpeed = (float)StaticVar.PlayerAttribute["ActorMoveSpeed"];
            ((Player)actor).PlayerMoney = (int)StaticVar.PlayerAttribute["Money"];
            ((Player)actor).ItemList.Clear();
            foreach (var item in (List<ItemInfo>)StaticVar.PlayerAttribute["ItemList"])
            {
                ((Player)actor).ItemList.Add(item);
            }
            _actorDic[actor._uid] = actor;
            actor._curState = actor._actorStateDic[(ActorStateType)StaticVar.PlayerAttribute["ActorStatetype"]];
        }
        actor._stateType = actor._curState.StateType;
        actor._curState.Enter(actor);
        if (actor._stateType == ActorStateType.Player_Date || actor._stateType == ActorStateType.Player_Chivied)
        {
            //创建对应跟随的NPC
            Actor npc;
            GameObject temp = (Object.Instantiate(Resources.Load("Prefabs/Characters/" + StaticVar.InteractiveNpc._name))) as GameObject;
            temp.transform.SetParent(GameObject.Find("Characters").transform);
            npc = temp.GetComponent<Actor>();

            //npc属性


            npc._uid = StaticVar.InteractiveNpc._uid;
            npc._name = StaticVar.InteractiveNpc._name;
            npc.name = npc._name;
            npc._moveSpeed = StaticVar.InteractiveNpc._moveSpeed;
            _actorDic[npc._uid] = npc;

            npc._curState = npc._actorStateDic[StaticVar.InteractiveNpc._stateType];
            npc._stateType = npc._curState.StateType;
            npc._curState.Enter(npc);

            StaticVar.InteractiveNpc = npc;
            npc.GetComponent<InteractiveNpc>().player = (Player)actor;
            npc._ani.SetBool("IsWalk", false);
        }
       
    }

    /// <summary>
    /// 删除角色
    /// </summary>
    /// <param name="uid">角色id</param>
    public void RemoveActor(int uid)
    {
        Actor actor = null;
        if (_actorDic.TryGetValue(uid, out actor))
        {
            Object.Destroy(actor.gameObject);
            _actorDic.Remove(uid);
        }
        else
        {
            Debug.Log("玩家" + uid + "不存在");
        }
    }

    /// <summary>
    /// 获取角色
    /// </summary>
    /// <param name="uid">角色id</param>
    /// <returns></returns>
    public Actor GetActor(int uid)
    {
        Actor actor = null;
        _actorDic.TryGetValue(uid, out actor);
        return actor;
    }
}
