﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeComWnd:BaseWnd
{
     public void Initialize()
    {
        _transform.gameObject.AddComponent<CoffeeComWndCon>();
    }
}

public class CoffeeComWndCon : MonoBehaviour
{
    private ArrayList menuList = new ArrayList();
    private Transform content;
    private void Start()
    {
        content = transform.Find("HaveList/Viewport/Content");
        foreach (var item in content.GetComponentsInChildren<ButtonStateAdjust>())
        {
            if (!menuList.Contains(item.transform))
            {
                menuList.Add(item.transform);
            }
        }
        StaticVar.CurrentMenu = menuList[0] as Transform;
    }

    private void Update()
    {
        StaticVar.InputControl1(menuList);
        if (Input.GetKeyDown(KeyCode.X))
        {
            WindowManager.instance.Close<CoffeeComWnd>();
            WindowManager.instance.Open<CoffeeWnd>();
            WindowManager.instance.Get<CoffeeWnd>().lastwndName = transform.name;
            WindowManager.instance.Get<CoffeeWnd>().Initialize();
        }
    }

    //初始化当前拥有的周边物品
    public void RefreshComList()
    {

    }

    //刷新右侧物品具体信息显示
    public void RefreshComInfor()
    {

    }
}
