using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersonInforWnd : BaseWnd
{
    public void Initialize()
    {
        _transform.gameObject.AddComponent<PersonInforWndCon>();
    }
}

public class PersonInforWndCon : MonoBehaviour
{
    private void Start()
    {

    }
    private void Update()
    {
        //transform.Find("Day/Text").GetComponent<Text>().text = "µÚ" + StaticVar.CurrentDay + "Ìì";
        //transform.Find("Week/Text").GetComponent<Text>().text = StaticVar.CurrentWeek;
        //transform.Find("TimeFrame/Text").GetComponent<Text>().text = StaticVar.CurrentTimeFrame;
        //transform.Find("HaveMoney/Text").GetComponent<Text>().text = "$" + StaticVar.player.PlayerMoney.ToString();

    }
}
