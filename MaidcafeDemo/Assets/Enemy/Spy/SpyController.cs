

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpyController : MonoBehaviour
{
    [SerializeField] private float _leftRange;
    [SerializeField] private float _rightRange;
    [SerializeField] private float _timerMax;
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;

    private Animator _anim;
    private Transform _horizontalPoint;

    private float _timer;
    private bool _canAttack;
    private Vector3 _targetDirection;
    private Vector3 _oriPosition;

    private void Awake()
    {
        //_stateType = SpyStateType.闲逛;
        _timer = Random.Range(1f, _timerMax);
        _canAttack = false;
        _oriPosition = transform.position;

        _horizontalPoint = transform.Find("HorizontalPoint");
        _anim = transform.Find("HorizontalPoint/Sprite").GetComponent<Animator>();
    }

    private void Update()
    {
        if (StaticVar.player.IsLockPlayer == true)
        {
            return;
        }
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            BuildNewState();
        }
        else
        {
            DoMove();
        }
    }

    private void BuildNewState()
    {
        //string[] __stateArray = System.Enum.GetNames(typeof(SpyStateType));
        //string __stateName = __stateArray[Random.Range(0, __stateArray.Length)];

        //Debug.Log("进入新状态: " + __stateName);
        //_stateType = System.Enum.Parse<SpyStateType>(__stateName);

        _canAttack = true;
        _timer = Random.Range(1f, _timerMax);
        _targetDirection = Random.Range(-1f, 1f) > 0 ? Vector3.left : Vector3.right;

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, _oriPosition.x - _leftRange, _oriPosition.x + _rightRange), transform.position.y, 0);
    }

    private bool TargetArrived()
    {
        return transform.position.x < _oriPosition.x - _leftRange || transform.position.x > _oriPosition.x + _rightRange;
    }

    private void DoMove()
    {
        _horizontalPoint.rotation = Quaternion.Euler(0, _targetDirection.x > 0 ? 180f : 0f, 0);

        //switch (_stateType)
        //{
        //    case SpyStateType.闲逛:
        //        _anim.SetFloat("Speed", 0);
        //        break;
        //    case SpyStateType.走路:
        //        if (TargetArrived())
        //        {
        //            BuildNewState();
        //        }
        //        else
        //        {
        //            transform.Translate(Time.deltaTime * _walkSpeed * _targetDirection);
        //            _anim.SetFloat("Speed", _walkSpeed);
        //        }
        //        break;
        //    case SpyStateType.冲刺:
        //        if (TargetArrived())
        //        {
        //            BuildNewState();
        //        }
        //        else
        //        {
        //            transform.Translate(Time.deltaTime * _runSpeed * _targetDirection);
        //            _anim.SetFloat("Speed", _runSpeed);
        //        }
        //        break;
        //    case SpyStateType.攻击:
        //        if (_canAttack)
        //        {
        //            StartCoroutine(AttackEnumerator());
        //        }
        //        break;
        //    default:
        //        Debug.LogError("未定义类:" + _stateType);
        //        break;
        //}
    }

    private IEnumerator AttackEnumerator()
    {
        _anim.SetFloat("Speed", 0);
        _anim.SetTrigger("Attack");
        _canAttack = false;
        //Debug.Log("进行攻击");
        // TODO:待完成
        yield return null;
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

