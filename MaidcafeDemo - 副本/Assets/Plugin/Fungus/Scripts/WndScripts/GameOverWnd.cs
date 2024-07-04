using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameOverWnd : BaseWnd
{

    public void Initialize()
    {
        Button btn1 = _transform.Find("Button").GetComponent<Button>();
        btn1.onClick.AddListener(OnBtnYes);
        if (WindowManager.instance._windows.ContainsKey("MenuWnd"))
        {
            WindowManager.instance.Close<GameOverWnd>();
        }
    }
    //Yes按钮被点击
    private void OnBtnYes()
    {
        Application.Quit();
    }
}
