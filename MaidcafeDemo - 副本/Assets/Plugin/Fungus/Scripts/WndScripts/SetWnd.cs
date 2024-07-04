using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SetWnd : BaseWnd 
{
    public static float bgma = 0.5f;
    public static float writea = 200;
    public static float effa = 200;
    public void Initialize()
    {
        Slider bgmslider = _transform.Find("背景音乐").GetComponent<Slider>();
        bgmslider.value = bgma ;
     

        Slider writeslider = _transform.Find("字速").GetComponent<Slider>();
        writeslider.value = writea;

        Slider soundeff = _transform.Find("音效").GetComponent<Slider>();
        soundeff.value = effa;

        Button btnReturn = _transform.Find("返回3").GetComponent<Button>();
        btnReturn.onClick.AddListener(OnReturn);
    }

    private void OnReturn()
    {
        WindowManager.instance.Close<SetWnd>();
        WindowManager.instance.Open<MenuWnd>().Initialize();
    }
}
