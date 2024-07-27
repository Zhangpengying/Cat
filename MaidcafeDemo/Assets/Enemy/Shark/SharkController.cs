

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SharkController : MonoBehaviour
{
    public enum EnemyState { IDLE, RUSH, WAIT };
    [SerializeField] private float _leftRange;
    [SerializeField] private float _rightRange;
    [SerializeField] private float _topRange;
    [SerializeField] private float _bottomRange;

    [SerializeField] private float _swimSpeed;
    [SerializeField] private float _rushSpeed;

    private Vector3 _oriPosition;
    private Vector3 _targetPosition;
    private Transform _horizontalPoint;
    public EnemyState curEnemyState;
    private bool isFinishMove = true;


    private void Awake()
    {
        _oriPosition = transform.position;
        _targetPosition = transform.position;
        curEnemyState = EnemyState.IDLE;
        _horizontalPoint = transform.Find("HorizontalPoint");
    }

    private void Update()
    {
        //if (GameDataModule.Instance.isGamePause == true)
        //{
        //    return;
        //}
        //if (curEnemyState == EnemyState.RUSH)
        //{
        //    GameObject __player = GameObjectManager.Instance.GetGameObject(GameObjectType.Player);
        //    if (__player == null)
        //    {
        //        return;
        //    }
        //    RoleStateType roleStateType = __player.GetComponent<PlayerController>()._stateType;
        //    if (roleStateType == RoleStateType.IsDead)
        //    {
        //        return;
        //    }
        //    else if (roleStateType == RoleStateType.Idle)
        //    {
        //        curEnemyState = EnemyState.WAIT;
        //        // 如果抵达, 则直接构建新目标
        //        Invoke("DelayChangeState", 1.0f);
        //    }
        //    if (isFinishMove == true)
        //    {
        //        FindPlayer();
        //    }
        //    else
        //    {
        //        if (TargetArrived())
        //        {
        //            isFinishMove = true;
        //            return;
        //        }
        //        RushSwim();
        //    }

        //    return;
        //}


        //if (TargetArrived())
        //{
        //    if (curEnemyState == EnemyState.WAIT)
        //    {
        //        return;
        //    }
        //    // 如果抵达, 则直接构建新目标
        //    BuildNewTarget();
        //}
        //else
        //{
        //    // 如果未抵达则继续游
        //    Swim();
        //}

    }
    private void DelayChangeState()
    {
        Debug.Log("DelayChangeState");
        curEnemyState = EnemyState.IDLE;
        BuildNewTarget();
    }
    private void FindPlayer()
    {
        //GameObject __player = GameObjectManager.Instance.GetGameObject(GameObjectType.Player);
        //if (__player == null)
        //{
        //    return;
        //}
        //RoleStateType roleStateType = __player.GetComponent<PlayerController>()._stateType;
        //if (roleStateType == RoleStateType.Swim)
        //{
        //    _targetPosition = __player.transform.position;
        //    isFinishMove = false;
        //}
    }

    private void RushSwim()
    {
        Vector3 __direction = (_targetPosition - transform.position).normalized;
        _horizontalPoint.rotation = Quaternion.Euler(0, __direction.x > 0 ? 180 : 0, 0);
        transform.Translate(__direction * _rushSpeed * Time.deltaTime);

    }
    private void Swim()
    {
        Vector3 __direction = (_targetPosition - transform.position).normalized;
        //if (curEnemyState == EnemyState.RUSH)
        //{
        //    transform.Translate(__direction * _rushSpeed * Time.deltaTime);
        //}
        //else
        //{
        transform.Translate(__direction * _swimSpeed * Time.deltaTime);
        //}

        _horizontalPoint.rotation = Quaternion.Euler(0, __direction.x > 0 ? 180 : 0, 0);
    }

    private bool TargetArrived()
    {
        return Vector3.Distance(transform.position, _targetPosition) < 0.3f;
    }

    private void BuildNewTarget()
    {
        _targetPosition = new Vector3(Random.Range(_oriPosition.x - _leftRange, _oriPosition.x + _rightRange), Random.Range(_oriPosition.y - _bottomRange, _oriPosition.y + _topRange), 0);
    }

    public void EnemyEnter()
    {
        Debug.Log("SharkController EnemyEnter _rushMode");
        curEnemyState = EnemyState.RUSH;
        FindPlayer();
    }

    public void EnemyExit()
    {
        // Debug.Log("SharkController EnemyExit _rushMode");
        // curEnemyState = EnemyState.IDLE;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        // 上
        Gizmos.DrawLine(new Vector3(transform.position.x - _leftRange, transform.position.y + _topRange, 0), new Vector3(transform.position.x + _rightRange, transform.position.y + _topRange, 0));
        // 下
        Gizmos.DrawLine(new Vector3(transform.position.x - _leftRange, transform.position.y - _bottomRange, 0), new Vector3(transform.position.x + _rightRange, transform.position.y - _bottomRange, 0));
        // 左
        Gizmos.DrawLine(new Vector3(transform.position.x - _leftRange, transform.position.y - _bottomRange, 0), new Vector3(transform.position.x - _leftRange, transform.position.y + _topRange, 0));
        // 右
        Gizmos.DrawLine(new Vector3(transform.position.x + _rightRange, transform.position.y - _bottomRange, 0), new Vector3(transform.position.x + _rightRange, transform.position.y + _topRange, 0));
    }
}
