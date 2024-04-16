using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeWnd : BaseWnd
{
    public string lastwndName;
    public void Initialize()
    {
        _transform.gameObject.AddComponent<CoffeeWndControl>().lastWnd = lastwndName;
        
    }
}

public class CoffeeWndControl:MonoBehaviour
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
        SetCurrBtn(lastWnd);
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
            WindowManager.instance.Close<CoffeeWnd>();
            WindowManager.instance.Open<SystemMenuWnd>();
            WindowManager.instance.Get<SystemMenuWnd>().temp = transform.name;
            WindowManager.instance.Get<SystemMenuWnd>().Initialize();
        }
    }

    void ClickMenu()
    {
        if (StaticVar.CurrentMenu != null && menulist.Contains(StaticVar.CurrentMenu))
        {
            //当前选择咖啡店-女仆
            if (StaticVar.CurrentMenu == (Transform)menulist[0])
            {
                WindowManager.instance.Close<CoffeeWnd>();
                WindowManager.instance.Open<WaiterInforListsWnd>().Initialize();
            }
            //当前选择咖啡店-客人
            else if (StaticVar.CurrentMenu == (Transform)menulist[1])
            {
                WindowManager.instance.Close<CoffeeWnd>();
                WindowManager.instance.Open<CustomerInforListsWnd>().Initialize();
            }
            //当前选择咖啡店-菜谱
            else if (StaticVar.CurrentMenu == (Transform)menulist[2])
            {
                WindowManager.instance.Close<CoffeeWnd>();
                WindowManager.instance.Open<CoffeeMenuWnd>().Initialize();
            }
            //当前选择咖啡店-DIY物品
            else if (StaticVar.CurrentMenu == (Transform)menulist[3])
            {
                WindowManager.instance.Close<CoffeeWnd>();
                WindowManager.instance.Open<CoffeeDecWnd>().Initialize();
            }
            //当前选择咖啡店-周边物品
            else if (StaticVar.CurrentMenu == (Transform)menulist[4])
            {
                WindowManager.instance.Close<CoffeeWnd>();
                WindowManager.instance.Open<CoffeeComWnd>().Initialize();
            }
        }
    }

    public void SetCurrBtn(string wndName)
    {
        switch (wndName)
        {
            case "WaiterInforListsWnd":
                StaticVar.CurrentMenu = menulist[0] as Transform;
                break;
            case "CustomerInforListsWnd":
                StaticVar.CurrentMenu = menulist[1] as Transform;
                break;
            case "CoffeeMenuWnd":
                StaticVar.CurrentMenu = menulist[2] as Transform;
                break;
            case "CoffeeDecWnd":
                StaticVar.CurrentMenu = menulist[3] as Transform;
                break;
            case "CoffeeComWnd":
                StaticVar.CurrentMenu = menulist[4] as Transform;
                break;
            default:
                StaticVar.CurrentMenu = menulist[0] as Transform;
                break;
        }
    }
}
