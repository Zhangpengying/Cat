using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitWnd : BaseWnd
{

    public void Initialize()
    {
        Button btn1 = _transform.Find("Button1").GetComponent<Button>();
        btn1.onClick.AddListener(OnBtn1);

        Button btn2 = _transform.Find("Button2").GetComponent<Button>();
        btn2.onClick.AddListener(OnBtn2);

        Button btn3 = _transform.Find("Button3").GetComponent<Button>();
        btn3.onClick.AddListener(OnBtn3);
    }
    //退出游戏按钮被点击
    private void OnBtn1()
    {
        Application.Quit();
    }
    //回到主页按钮被点击
    private void OnBtn2()
    {
        WindowManager.instance.Close<ExitWnd>();
        WindowManager.instance.Open<ReturnMainWnd>().Initialize();
    }
    //取消按钮被点击
    private void OnBtn3()
    {
        WindowManager.instance.Close<ExitWnd>();
        WindowManager.instance.Open<MenuWnd>().Initialize();
    }

}
