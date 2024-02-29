using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;

public class ReadSaveWnd : BaseWnd
{
    public static string saveDataKey;
    public void Initialize()
    {
        Button btn1 = _transform.Find("ButtonYes").GetComponent<Button>();
        btn1.onClick.AddListener(OnBtnYes);

        Button btn2 = _transform.Find("ButtonNo").GetComponent<Button>();
        btn2.onClick.AddListener(OnBtnNo);

    }
    //Yes按钮被点击
    private void OnBtnYes()
    {
        WindowManager.instance.Close<ReadSaveWnd>();

        //WindowManager.instance.Close<LoadWnd>();
        if (GameObject.Find("UI/Canvas/LoadWnd(Clone)") == null)
        {
            WindowManager.instance.Close<LoadWnd>();
            WindowManager.instance.Open<MenuWnd>().Initialize();
        }
        else
        {
            Object.Destroy(GameObject.Find("UI/Canvas/LoadWnd(Clone)"));
            WindowManager.instance.Open<MenuWnd>().Initialize();
        }

        //WindowManager.instance.Open<MenuWnd>().Initialize();
        var saveManager = FungusManager.Instance.SaveManager;
        WindowManager.instance.Close<MenuWnd>();
        if (saveManager.SaveDataExists(saveDataKey))
        {
            saveManager.Load(saveDataKey);
        }
       
    }
    //No按钮被点击
    private void OnBtnNo()
    {
        WindowManager.instance.Close<ReadSaveWnd>();
    }

}
