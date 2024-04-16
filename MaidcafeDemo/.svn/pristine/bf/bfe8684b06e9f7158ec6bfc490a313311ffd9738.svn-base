using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicInforWnd : BaseWnd
{
     public void  Initialize()
    {
        _transform.gameObject.AddComponent<BasicInforWndCon>();
    }
}

public class BasicInforWndCon : MonoBehaviour
{
    private void Start()
    {
   
    }
    private void Update()
    {
        transform.Find("Day/Text").GetComponent<Text>().text = "第" + StaticVar.CurrentDay + "天";
        transform.Find("Week/Text").GetComponent<Text>().text =  StaticVar.CurrentWeek;
        transform.Find("TimeFrame/Text").GetComponent<Text>().text = StaticVar.CurrentTimeFrame;
        transform.Find("HaveMoney/Text").GetComponent<Text>().text ="$"+ StaticVar.player.PlayerMoney.ToString();
      
    }
}
