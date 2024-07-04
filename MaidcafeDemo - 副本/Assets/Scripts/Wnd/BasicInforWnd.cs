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
        transform.Find("Menu").GetComponent<Button>().onClick.AddListener(()=> {
            WindowManager.instance.Open<PersonInforWnd>().Initialize();
        });

    }
    private void Update()
    {
        RefreshHP();
    }
    private void RefreshHP()
    {
        Button[] HPArr = transform.Find("HP").GetComponentsInChildren<Button>();
        for (int i = 0; i < HPArr.Length; i++)
        {
            HPArr[i].interactable = i < StaticVar.player.PlayerHP;
        }
    }
}
