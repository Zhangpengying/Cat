

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpyHatArea : MonoBehaviour
{
    private float _timerMax;
    private float _timer;

    private void Awake()
    {
        _timerMax = 0.2f;
        _timer = 0f;
    }

    private void Update()
    {
        _timer = Mathf.Clamp(_timer - Time.deltaTime, 0f, _timerMax);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.isTrigger)
        {
            return;
        }

        if (_timer > 0f)
        {
            return;
        }

        if (collision.CompareTag("PlayerLandArea"))
        {
            Debug.Log("二段跳");
            _timer = _timerMax;
            //collision.GetComponent<PlayerLandArea>().StepJump();
        }
    }
}

