

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    [SerializeField] private float _leftRange;
    [SerializeField] private float _rightRange;
    [SerializeField] private float _runSpeed;

    private Transform _horizontalPoint;
    private Vector3 _targetPosition;
    private Vector3 _oriPosition;

    private void Awake()
    {
        _targetPosition = transform.position;
        _oriPosition = transform.position;
        _horizontalPoint = transform.Find("HorizontalPoint");
    }

    private void Update()
    {
        //if (StaticVar.player.IsLockPlayer == true)
        //{
        //    return;
        //}
        if (ArriveTarget())
        {
            BuildNewTarget();
        }
        else
        {
            Run();
        }
    }

    private void Run()
    {
        Vector3 __direction = (_targetPosition - transform.position).normalized;
        transform.Translate(Time.deltaTime * _runSpeed * __direction);

        _horizontalPoint.rotation = Quaternion.Euler(0f, __direction.x > 0f ? 180f : 0f, 0f);
    }

    private bool ArriveTarget()
    {
        return Vector3.Distance(_targetPosition, transform.position) < 0.3f;
    }

    private void BuildNewTarget()
    {
        Vector3 __leftPosition = new Vector3(_oriPosition.x - _leftRange, _oriPosition.y, 0);
        Vector3 __rightPosition = new Vector3(_oriPosition.x + _rightRange, _oriPosition.y, 0);

        _targetPosition = Vector3.Distance(__leftPosition, transform.position) > Vector3.Distance(__rightPosition, transform.position) ? __leftPosition : __rightPosition;
    }

    private void OnDrawGizmosSelected()
    {
        float __yTop = 1.5f;
        float __yDown = 0f;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(transform.position.x - _leftRange, transform.position.y + __yDown, 0), new Vector3(transform.position.x + _rightRange, transform.position.y + __yDown, 0));
        Gizmos.DrawLine(new Vector3(transform.position.x - _leftRange, transform.position.y + __yTop, 0), new Vector3(transform.position.x + _rightRange, transform.position.y + __yTop, 0));
        Gizmos.DrawLine(new Vector3(transform.position.x - _leftRange, transform.position.y + __yDown, 0), new Vector3(transform.position.x - _leftRange, transform.position.y + __yTop, 0));
        Gizmos.DrawLine(new Vector3(transform.position.x + _rightRange, transform.position.y + __yDown, 0), new Vector3(transform.position.x + _rightRange, transform.position.y + __yTop, 0));
    }
}

