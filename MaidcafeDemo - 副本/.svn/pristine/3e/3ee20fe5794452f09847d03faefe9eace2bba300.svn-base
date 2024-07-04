using Fungus;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public abstract class Actor :MonoBehaviour
{
    /// <summary>
    /// 角色的ID
    /// </summary>
    public int _uid;

    /// <summary>
    /// 角色名字
    /// </summary>
    public string _name;

    /// <summary>
    /// 角色移动速度
    /// </summary>
    public float _moveSpeed;

    /// <summary>
    /// 是否正在移动
    /// </summary>
    public bool _isMoving;

    /// <summary>
    /// 坐标
    /// </summary>
    public Vector3 _pos;

    /// <summary>
    /// 当前状态
    /// </summary>
    public ActorState _curState { set; get; }
    /// <summary>
    /// 当前状态类型
    /// </summary>
    public ActorStateType _stateType;
    /// <summary>
    /// 当前的目标点
    /// </summary>
    public Vector3 _currentGoal;
    /// <summary>
    /// 状态机集合
    /// </summary>
    public Dictionary<ActorStateType, ActorState> _actorStateDic = new Dictionary<ActorStateType, ActorState>();

    /// <summary>
    /// 动画控制器
    /// </summary>
    [HideInInspector]
    public Animator _ani;

    public  AnimatorStateInfo animatorInfo;

    //移动路线
    public List<Vector3> path = new List<Vector3>();

    //角色移动到路线中点的索引
    private int index = 0;

    public Transform _transform;

    //角色当前所在的楼层
    public int currentFloor;

    void Awake()
    {
        _transform = this.transform;
        _ani = transform.Find("BG").GetComponent<Animator>();
        InitState();
        InitCurState();
    }

    public void Update()
    {
        _pos = _transform.localPosition;
        
        //判定角色所在楼层
        if (_pos.y>StaticVar.boundaries.y)
        {
            currentFloor = 2;
        }
        else
        {
            currentFloor = 1;
        }
        //if (path.Count != 0)
        //{
        //    AIMove();
        //}

      
    }
    /// <summary>
    /// AI单段移动
    /// </summary>
    /// <param name="goal"></param>要移动到的目标点
    public void AITranslate(GameObject target, Vector3 goal)
    {
        //调整朝向
        target.transform.Find("BG").GetComponent<SpriteRenderer>().flipX = goal.x - target.transform.localPosition.x < 0 ? false : true;
        //target.transform.localScale = goal.x - target.transform.localPosition.x < 0 ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
        _currentGoal = goal;
        float time = Vector3.Distance(goal, target.transform.localPosition) / _moveSpeed;
        iTween.MoveTo(target, iTween.Hash("Position", _currentGoal, "Time", time, "EaseType", iTween.EaseType.linear));
    }
    /// <summary>
    /// 角色按照路线移动
    /// </summary>
    public void AIMove(Actor target, Vector3 goal)
    {
        #region
        //if (index > path.Count - 1)
        //{
        //    return;
        //}
        //transform.Translate((path[index] - transform.localPosition).normalized * Time.deltaTime * _moveSpeed);

        //if (Vector3.Distance(path[index], transform.localPosition) < 0.02f)
        //{

        //    index++;
        //    //调整朝向
        //    if (index < path.Count)
        //    {
        //        transform.GetComponent<SpriteRenderer>().flipX = path[index].x - transform.localPosition.x < 0 ? false : true;
        //        //transform.localScale = path[index].x - transform.localPosition.x < 0 ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
        //    }
        //    if (index == path.Count)
        //    {
        //        transform.localPosition = path[index - 1];
        //        path.Clear();
        //        index = 0;
        //    }
        //}
        #endregion
        Vector3 firstFloorDoor = StaticVar.VecTranslate(GameObject.Find("FirstFloorItems/Door/EnterPoint").transform);
        Vector3 secondFloorDoor = StaticVar.VecTranslate(GameObject.Find("SecondFloorItems/Door/EnterPoint").transform);
        //判定上楼还是下楼
        if (target.transform.localPosition.y - goal.y > 0)
        {
            //下楼
            AITranslate(target.gameObject, secondFloorDoor);
            float time1 = Vector3.Distance(secondFloorDoor, target.transform.localPosition) / _moveSpeed;
            TimerManager.instance.Invoke(time1, delegate { StaticVar.GradualChange(target.gameObject, 0, StaticVar.gradualChangeTime); });
             

            TimerManager.instance.Invoke(time1 + StaticVar.gradualChangeTime  + StaticVar.upStairTime, delegate {
                target.transform.localPosition = firstFloorDoor;
                StaticVar.GradualChange(target.gameObject, 1, StaticVar.gradualChangeTime);
                TimerManager.instance.Invoke(StaticVar.gradualChangeTime, delegate { AITranslate(target.gameObject, goal); });
            });
        }
        else
        {
            //上楼
            AITranslate(target.gameObject, firstFloorDoor); 
            float time1 = Vector3.Distance(firstFloorDoor, target.transform.localPosition) / _moveSpeed;
            TimerManager.instance.Invoke(time1, delegate { StaticVar.GradualChange(target.gameObject, 0, StaticVar.gradualChangeTime);});
            TimerManager.instance.Invoke(time1 + StaticVar.gradualChangeTime + StaticVar.upStairTime, delegate {
                target.transform.localPosition = secondFloorDoor;
                StaticVar.GradualChange(target.gameObject, 1, StaticVar.gradualChangeTime);
                TimerManager.instance.Invoke(StaticVar.gradualChangeTime, delegate { AITranslate(target.gameObject, goal); });
            });
        }
    }
    

    /// <summary>
    /// 初始化状态机
    /// </summary>
    protected abstract void InitState();

    /// <summary>
    ///  初始化当前状态
    /// </summary>
    protected abstract void InitCurState();


    /// <summary>
    /// 改变状态机
    /// </summary>
    /// <param name="stateType"></param>
    /// <param name="param"></param>
    public void TransState(Actor act, ActorStateType stateType)
    {
        if (act._curState == null)
        {
            return;
        }
        if (act._curState.StateType == stateType)
        {
            return;
        }
        else
        {
            ActorState _state;
            if (act._actorStateDic.TryGetValue(stateType, out _state))
            {
                act._curState.Exit();
                act._curState = _state;
                act._curState.Enter(this);
                _stateType = _curState.StateType;
            }
        }
    }

    /// <summary>
    /// 更新状态机
    /// </summary>
    public void UpdateState()
    {
        if (_curState != null)
        {
            _curState.Update();
        }
       
    }

    /// <summary>
    /// 播放动画
    /// </summary>
    /// <param name="name"></param>
    /// <param name="dir"></param>
    public void PlayAnim(string name)
    {
        _ani.SetBool(name, true);
        
    }
}
