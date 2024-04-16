﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeDecWnd : BaseWnd
{
    public void Initialize()
    {
        _transform.gameObject.AddComponent<CoffeeDecWndCon>();
    }
	
}

public class CoffeeDecWndCon : MonoBehaviour
{
    //分类按钮
    private ArrayList classify = new ArrayList();
    //当前选择类型按钮列表
    private ArrayList menuList = new ArrayList();
    private void Start()
    {
        foreach (var item in transform.Find("Classify").GetComponentsInChildren<ButtonStateAdjust>())
        {
            if (!classify.Contains(item.transform))
            {
                classify.Add(item.transform);
            }
        }
        foreach (var item in transform.Find("HaveDec/Viewport/Content").GetComponentsInChildren<ButtonStateAdjust>())
        {
            if (!menuList.Contains(item.transform))
            {
                menuList.Add(item.transform);
            }
        }

        StaticVar.CurrentMenu = classify[0] as Transform;
    }
    
    private void Update()
    {
        //当前选项在分类
        if (classify.Contains(StaticVar.CurrentMenu))
        {
            StaticVar.InputControl1(classify);
            if (Input.GetKeyDown(KeyCode.Z))
            {
                StaticVar.CurrentMenu = menuList[0] as Transform;
            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
                WindowManager.instance.Close<CoffeeDecWnd>();
                WindowManager.instance.Open<CoffeeWnd>();
                WindowManager.instance.Get<CoffeeWnd>().lastwndName = transform.name;
                WindowManager.instance.Get<CoffeeWnd>().Initialize();
            }
        }
        //当前选项在物品列表
        else if (menuList.Contains(StaticVar.CurrentMenu))
        {
            StaticVar.InputControl1(menuList);
            if (Input.GetKeyDown(KeyCode.X))
            {
                StaticVar.CurrentMenu = classify[0] as Transform;
            }
        }
    }
}