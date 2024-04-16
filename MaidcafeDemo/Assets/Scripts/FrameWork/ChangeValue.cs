using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChangeValue : MonoBehaviour
{
    //当前结账金额
    int value = 0;
    //增加金额
    public int damage = 0;
    //起始金钱
    public int startValue = 0;
    //结束金钱
    int endValue = 0;

    //金钱TEXT
    Text damageShow;
    //初始位置        
    Vector3 pos_Start;
    //结束位置
    Vector3 pos_End;
    //是否收款
    public bool collectMoney = false;
    //数字增加的总时间
    public float durition = 2f;
    //初始时间
    float startTime = 0;

    void Awake()
    {
        damageShow = GetComponent<Text>();
        pos_Start = transform.localPosition;
        pos_End = pos_Start + new Vector3(0, 20, 0);

    }
    // Use this for initialization
    void Start()
    {
        collectMoney = false;
    }

    private void Update()
    {
        if (collectMoney)
        {
            startTime += Time.deltaTime;
            endValue = startValue + damage;
            if (startTime <= durition)
            {
                int value = (int)Mathf.Lerp(startValue, endValue, startTime / durition);
                damageShow.text = value.ToString();
            }
            else
            {
                damageShow.text = endValue.ToString();
                StaticVar.player.PlayerMoney = endValue;
                startTime = 0;
                collectMoney = false;
            }

        }

    }


}

