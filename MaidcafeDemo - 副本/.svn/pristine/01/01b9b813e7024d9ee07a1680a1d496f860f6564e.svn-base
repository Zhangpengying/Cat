using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemMenuWnd : BaseWnd
{
    public string temp;
    public void Initialize()
    {
       ;
        if (_transform.GetComponent<SystemMenuWndControl>() == null)
        {
            _transform.gameObject.AddComponent<SystemMenuWndControl>().lastWnd = temp;
        }
   
        StaticVar.player.IsLockPlayer = true;
    }
}

public class SystemMenuWndControl : MonoBehaviour
{
    ArrayList menulist = new ArrayList();
    public string lastWnd;
    private void Start()
    {
        foreach (var item in GetComponentsInChildren<ButtonStateAdjust>())
        {
            if (!menulist.Contains(item.transform))
            {
                menulist.Add(item.transform);
            }
        }

        ChangeCurrBtn(lastWnd);

       
    }

    private void Update()
    {
        StaticVar.InputControl1(menulist);
        if (Input.GetKeyDown(KeyCode.Z))
        {
            ClickMenu();
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            WindowManager.instance.Close<SystemMenuWnd>();
            if (StaticVar.player!= null)
            {
                StaticVar.player.IsLockPlayer = false;
            }
        }
    }

    //当前选项的后续操作控制
    private void ClickMenu()
    {
        if (StaticVar.CurrentMenu!= null && menulist.Contains(StaticVar.CurrentMenu))
        {
            //当前选择系统—手机
            if (StaticVar.CurrentMenu == (Transform)menulist[0])
            {

            }
            //当前选择系统-背包
            else if (StaticVar.CurrentMenu == (Transform)menulist[1])
            {
                WindowManager.instance.Close<SystemMenuWnd>();
                WindowManager.instance.Open<BagWnd>().Initialize();
            }
            //当前选择系统-咖啡店
            else if (StaticVar.CurrentMenu == (Transform)menulist[2])
            {
                WindowManager.instance.Close<SystemMenuWnd>();
                WindowManager.instance.Open<CoffeeWnd>().Initialize();
            }
            //当前选择系统-相册
            else if (StaticVar.CurrentMenu == (Transform)menulist[3])
            {

            }
            //当前选择系统-返回标题
            else if (StaticVar.CurrentMenu == (Transform)menulist[4])
            {
                WindowManager.instance.Close<SystemMenuWnd>();
                StaticVar.ToNextSecens("Start", null);
            }
        }
    }

    public void ChangeCurrBtn(string wndName)
    {
        switch (wndName)
        {
            case "BagWnd":
                StaticVar.CurrentMenu = menulist[1] as Transform;
                break;
            case "CoffeeWnd":
                StaticVar.CurrentMenu = menulist[2] as Transform;
                break;
            
            default:
                StaticVar.CurrentMenu = menulist[0] as Transform;
                break;
        }
    }
}
