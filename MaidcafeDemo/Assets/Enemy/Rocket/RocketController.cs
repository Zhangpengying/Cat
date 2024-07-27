

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    [SerializeField] private float _flySpeed;
    [SerializeField] private float _leftTime;

    private float _timmer;
    private Vector3 m_StartPos;

    private void Awake()
    {
        _timmer = _leftTime;
        m_StartPos = transform.position;
    }

    private void Update()
    {
        if (StaticVar.player.IsLockPlayer)
        {
            return;
        }
        _timmer -= Time.deltaTime;
        if (_timmer < 0)
        {
            //_timmer = _leftTime;
            //transform.position = m_StartPos;
            Destroy(gameObject);
        }

        transform.Translate(_flySpeed * Time.deltaTime * Vector3.left);
    }
}

