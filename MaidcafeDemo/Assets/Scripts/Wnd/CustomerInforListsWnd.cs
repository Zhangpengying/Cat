using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerInforListsWnd : BaseWnd
{
    public CustomerCfg selectCus;
    public void Initialize()
    {
        _transform.gameObject.AddComponent<CusInforLiCon>();
    }
	
}

public class CusInforLiCon:MonoBehaviour
{
    private ArrayList menuList = new ArrayList();
    private Transform content;
    private void Start()
    {
        content = transform.Find("Scroll View/Viewport/Content");
        foreach (var item in content.GetComponentsInChildren<ButtonStateAdjust>())
        {
            if (!menuList.Contains(item.transform))
            {
                menuList.Add(item.transform);
            }
        }
        CustomerCfg cfg = WindowManager.instance.Get<CustomerInforListsWnd>().selectCus;
        if (cfg != null)
        {
            int n = MessageSend.instance.CurrUnLockCustomer.IndexOf(cfg);
            StaticVar.CurrentMenu = menuList[n] as Transform;

        }
        else
        {
            StaticVar.CurrentMenu = (Transform)menuList[0];
        }
        //初始化顾客列表
        RefreshCustomerList();
    }

    private void Update()
    {
        StaticVar.InputControl1(menuList);
       
        RefreshProInfor();
        if (Input.GetKeyDown(KeyCode.Z))
        {
            WindowManager.instance.Close<CustomerInforListsWnd>();
            WindowManager.instance.Open<CustomerInfor>();
            //赋值当前选择客人
            int temp = menuList.IndexOf(StaticVar.CurrentMenu);
            WindowManager.instance.Get<CustomerInfor>().customerCfg = MessageSend.instance.CurrUnLockCustomer[temp];
            WindowManager.instance.Get<CustomerInfor>().Initialize();
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            WindowManager.instance.Close<CustomerInforListsWnd>();
            WindowManager.instance.Open<CoffeeWnd>();
            WindowManager.instance.Get<CoffeeWnd>().lastwndName = transform.name;
            WindowManager.instance.Get<CoffeeWnd>().Initialize();
        }
    }

    //刷新显示
    private void RefreshCustomerList()
    {
        foreach (var item in MessageSend.instance.CurrUnLockCustomer)
        {
            int n = MessageSend.instance.CurrUnLockCustomer.IndexOf(item);
            if (n<menuList.Count)
            {
                Transform temp = menuList[n] as Transform;
                temp.Find("Name").GetComponent<Text>().text = item.Name;
                temp.Find("Icon").GetComponent<Image>().sprite = LoadTexture.getInstance().LoadAtlasSprite("Atlas/UI/PropertyIcon/Characters/Head/Head", item.Name);
            }
        }
    }
    private void RefreshProInfor()
    {
        if (StaticVar.CurrentMenu != null)
        {
            if (menuList.Contains(StaticVar.CurrentMenu))
            {
                int n = menuList.IndexOf(StaticVar.CurrentMenu);
                transform.Find("Infor/Name/Text2").GetComponent<Text>().text = MessageSend.instance.CurrUnLockCustomer[n].Name;
                transform.Find("Infor/Intro/Text2").GetComponent<Text>().text = MessageSend.instance.CurrUnLockCustomer[n].Introduce;
            }
        }

    }
}
