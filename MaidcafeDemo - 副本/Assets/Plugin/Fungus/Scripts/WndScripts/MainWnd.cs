using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Fungus;

public class MainWnd :BaseWnd
{
    public void Initialize()
    {
        //菜单按钮
        Button menuBtn = _transform.Find("MenuButton").GetComponent<Button>();
        menuBtn.onClick.AddListener(OnMenuBtnClicked);
        // 返回按钮
        Button returnBtn = _transform.Find("返回").GetComponent<Button>();
        returnBtn.onClick.AddListener(OnReturnBtnClicked);

        //自动播放按钮
        Button autoBtn = _transform.Find("AutoPlay").GetComponent<Button>();
        autoBtn.onClick.AddListener(OnAutoPlayClicked);
    }

    private void OnMenuBtnClicked()
    {
        if (GameObject.Find("MenuWnd")!=null)
        {
          
            WindowManager.instance.Open<MenuWnd>();
        }
        else
        {
            WindowManager.instance.Open<MenuWnd>().Initialize();
        }
    }

    private void OnReturnBtnClicked()
    {
        WindowManager.instance.Close<MenuWnd>();
    }

    private void OnAutoPlayClicked()
    {
       
        Say.WaitForClick = !Say.WaitForClick;
        Debug.Log(Say.WaitForClick);
    }
}
