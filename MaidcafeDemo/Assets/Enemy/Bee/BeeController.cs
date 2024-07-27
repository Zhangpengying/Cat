using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BeeController : MonoBehaviour
{
    [SerializeField] private float _patrolRadius;
    [SerializeField] private float _searchRange;
    [SerializeField] private float _flySpeed;

    private Transform _horizontalPoint;

    private Vector3 _newPosition;
    private Vector3 _oldPosition;
    private Vector3 _oriPosition;

    private float _startTime;
    private float _journeyTime;

    private void Awake()
    {
        _newPosition = transform.position;
        _oldPosition = transform.position;
        _oriPosition = transform.position;

        _horizontalPoint = transform.Find("HorizontalPoint");
    }

    private void Update()
    {
        if (StaticVar.player.IsLockPlayer == true)
        {
            return;
        }
        // 如果抵达目的地
        if (TargetArrived())
        {
            BuildNewTarget();
        }
        else
        {
            // 中心点
            Vector3 __center = (_oldPosition + _newPosition) * 0.5F;

            // 添加弧度
            __center -= new Vector3(0, 1, 0);

            // 弧度起止
            Vector3 __riseRelCenter = _oldPosition - __center;
            Vector3 __setRelCenter = _newPosition - __center;

            // 计算当前弧度
            float __fracComplete = (Time.time - _startTime) / _journeyTime;

            transform.position = Vector3.Slerp(__riseRelCenter, __setRelCenter, __fracComplete);
            transform.position += __center;

            _horizontalPoint.rotation = Quaternion.Euler(0, _newPosition.x - transform.position.x > 0 ? 180 : 0, 0);
        }
    }

    private bool TargetArrived()
    {
        return Vector3.Distance(transform.position, _newPosition) < 0.3f;
    }

    private void BuildNewTarget()
    {
        float __alpha = Random.Range(0f, 360f);
        Vector3 __direction = new Vector3(Mathf.Cos(__alpha), Mathf.Sin(__alpha), 0) * Random.Range(1f, 1f + _searchRange);

        _oldPosition = transform.position;
        _newPosition = transform.position + __direction;

        if (Vector3.Distance(_oriPosition, _newPosition) > _patrolRadius)
        {
            _newPosition = _oriPosition;
        }

        _journeyTime = Vector3.Distance(transform.position, _newPosition) / _flySpeed;
        _startTime = Time.time;
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 __centerPosition = transform.position;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(__centerPosition, _patrolRadius);
    }
}
