using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;
using UnityEngine.SceneManagement;

public class ReturnMainWnd : BaseWnd
{
    protected AudioSource clickAudioSource;
    protected bool restartDeletesSave = false;
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
        
        WindowManager.instance.Close<ReturnMainWnd>();
        Restart();
       
    }
    //No按钮被点击
    private void OnBtnNo()
    {
        WindowManager.instance.Close<ReturnMainWnd >();
        WindowManager.instance.Open<ExitWnd>().Initialize();
        
    }
    //返回主页
    public virtual void Restart()
    {
        var saveManager = FungusManager.Instance.SaveManager;

        if (string.IsNullOrEmpty(saveManager.StartScene))
        {
            Debug.LogError("No start scene specified");
            return;
        }

        // Reset the Save History for a new game
        saveManager.ClearHistory();
        SceneManager.LoadScene(saveManager.StartScene);
    }
}
