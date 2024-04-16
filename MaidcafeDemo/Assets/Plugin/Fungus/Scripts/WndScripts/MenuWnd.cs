using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using Fungus;


public class MenuWnd : BaseWnd 
{
    public void Initialize()
    {
        //StartCoroutine(CountSeconds());
//        WindowManager.instance.Close<GameOverWnd>();

        //保存按钮
        Button saveBtn = _transform.Find("BackGround/MyPanel/存档").GetComponent<Button>();
        //saveBtn.onClick.AddListener(OnSaveBtnClicked);

        //读档按钮
        Button loadBtn = _transform.Find("BackGround/MyPanel/读档").GetComponent<Button>();
        loadBtn.onClick.AddListener(OnLoadBtnClicked);

        // 自动按钮
        Button reviewBtn = _transform.Find("BackGround/MyPanel/自动").GetComponent<Button>();
        reviewBtn.onClick.AddListener(OnReviewBtnClicked);

        //游戏设置按钮
        Button setBtn = _transform.Find("BackGround/MyPanel/游戏设置").GetComponent<Button>();
        setBtn.onClick.AddListener(OnSetBtnClicked);

        //退出按钮
        Button recallBtn = _transform.Find("BackGround/MyPanel/退出").GetComponent<Button>();
        recallBtn .onClick.AddListener(OnRecallBtnClicked );
    }

    public Transform trans;
    private void OnSaveBtnClicked()
    {
        WindowManager.instance.Close<MenuWnd>();
        WindowManager.instance.Open<SaveWnd>().Initialize();
    }
    private void OnLoadBtnClicked()
    {
        WindowManager.instance.Close<MenuWnd>();
        WindowManager.instance.Open<LoadWnd>().Initialize();
       
    }
    //自动按钮被点击
    private void OnReviewBtnClicked()
    {
        Say.WaitForClick = !Say.WaitForClick;
        Debug.Log(Say.WaitForClick);
    }
    //设置按钮被点击
    private void OnSetBtnClicked()
    {
        WindowManager.instance.Open<SetWnd>().Initialize();
        WindowManager.instance.Close<MenuWnd>();
    }
    //退出按钮被点击
    private void OnRecallBtnClicked()
    {
        WindowManager.instance.Open<ExitWnd>().Initialize();
        WindowManager.instance.Close<MenuWnd>();
    }

}