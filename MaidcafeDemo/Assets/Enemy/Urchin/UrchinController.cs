

using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class UrchinController : MonoBehaviour
{
    [SerializeField] private float _topRange;
    [SerializeField] private float _downRange;
    [SerializeField] private float _leftRange;
    [SerializeField] private float _rightRange;
    [SerializeField] private float _swimUpSpeed;
    [SerializeField] private float _swimDownSpeed;
    [SerializeField] private float _idleTimeMax;
    [SerializeField] private float _rushTimeMax;

    private Transform _horizontalPoint;

    private Vector3 _oriPosition;
    private Vector3 _targetDirection;

    private float _topBorder;
    private float _downBorder;
    private float _leftBorder;
    private float _rightBorder;
    private float _idleTimer;
    private float _rushTimer;

    private void Awake()
    {
        _oriPosition = transform.position;
        _targetDirection = Vector3.down;
        _horizontalPoint = transform.Find("HorizontalPoint");

        // 误差修正
        float __deviationFix = 0f;

        _topBorder = _oriPosition.y + _topRange - __deviationFix;
        _downBorder = _oriPosition.y - _downRange + __deviationFix;
        _leftBorder = _oriPosition.x - _leftRange + __deviationFix;
        _rightBorder = _oriPosition.x + _rightRange - __deviationFix;
        _idleTimer = 0f;
        _rushTimer = 0f;
    }

    private void Update()
    {
        if (StaticVar.player.IsLockPlayer)
        {
            return;
        }
        if (TargetArrived())
        {
            BuildNewTarget();
        }
        else
        {
            Swim();
        }
    }

    private bool TargetArrived()
    {
        bool __reachTop = transform.position.y > _topBorder;
        bool __reachDown = transform.position.y < _downBorder;
        bool __reachLeft = transform.position.x < _leftBorder;
        bool __reachRight = transform.position.x > _rightBorder;

        return __reachTop || __reachDown || __reachLeft || __reachRight;
    }

    private void BuildNewTarget()
    {
        if (transform.position.y > _topBorder)
        {
            // 如果抵达水面
            _targetDirection = Vector3.down;
            transform.Translate(Vector3.down * Time.deltaTime);
            _rushTimer = 0f;
            _idleTimer = 0f;
        }
        else if (transform.position.y < _downBorder)
        {
            // 如果在水底
            bool __leftRight = Random.Range(-1f, 1f) > 0;
            _targetDirection = new Vector3(__leftRight ? 1 : -1, 1, 0).normalized;
            transform.Translate(Vector3.up * Time.deltaTime);
        }
        else if (transform.position.x < _leftBorder)
        {
            // 如果在左侧
            _targetDirection = new Vector3(1, 1, 0).normalized;
            transform.Translate(Vector3.right * Time.deltaTime);
        }
        else if (transform.position.x > _rightBorder)
        {
            // 如果在右侧
            _targetDirection = new Vector3(-1, 1, 0).normalized;
            transform.Translate(Vector3.left * Time.deltaTime);
        }
        else
        {
            Debug.Log("报错");
        }
    }

    private void Swim()
    {
        if (_targetDirection.y > 0)
        {
            if (_rushTimer > 0)
            {
                // 先向上冲刺;
                transform.Translate(Time.deltaTime * _swimUpSpeed * _targetDirection);
                _rushTimer -= Time.deltaTime;
            }
            else if (_rushTimer <= 0 && _idleTimer > 0)
            {
                // 原地休息
                _idleTimer -= Time.deltaTime;
            }
            else
            {
                // 重置所用动作
                _idleTimer = _idleTimeMax;
                _rushTimer = _rushTimeMax;
            }
        }
        else
        {
            // 如果向下
            transform.Translate(Time.deltaTime * _swimDownSpeed * _targetDirection);
        }

        _horizontalPoint.rotation = Quaternion.Euler(0, _targetDirection.x > 0 ? 180 : 0, 0);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(transform.position.x - _leftRange, transform.position.y + _topRange, 0), new Vector3(transform.position.x + _rightRange, transform.position.y + _topRange, 0));
        Gizmos.DrawLine(new Vector3(transform.position.x - _leftRange, transform.position.y + _topRange, 0), new Vector3(transform.position.x - _leftRange, transform.position.y - _downRange, 0));
        Gizmos.DrawLine(new Vector3(transform.position.x - _leftRange, transform.position.y - _downRange, 0), new Vector3(transform.position.x + _rightRange, transform.position.y - _downRange, 0));
        Gizmos.DrawLine(new Vector3(transform.position.x + _rightRange, transform.position.y + _topRange, 0), new Vector3(transform.position.x + _rightRange, transform.position.y - _downRange, 0));
    }
}

