using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaiterInforListsWnd : BaseWnd
{
    public WaiterCfg selectWaiter;
    public void Initialize()
    {
        _transform.gameObject.AddComponent<WaiterInLiCon>();
    }
}

public class WaiterInLiCon:MonoBehaviour
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
                if (menuList.Count < MessageSend.instance.currenthavewaiters.Count)
                {
                    menuList.Add(item.transform);
                }
                
            }
        }
        WaiterCfg cfg = WindowManager.instance.Get<WaiterInforListsWnd>().selectWaiter;
        if (cfg!=null)
        {
            int n = MessageSend.instance.currenthavewaiters.IndexOf(cfg);
            StaticVar.CurrentMenu = menuList[n] as Transform;

        }
        else
        {
            StaticVar.CurrentMenu = (Transform)menuList[0];
        }
        //初始化女仆列表
        RefreshWaiterList();
        
    }
    private void Update()
    {
        StaticVar.InputControl1(menuList);
        //查看女仆详细信息
        if (Input.GetKeyDown(KeyCode.Z))
        {
            //传递当前选择查看的女仆
            int n = menuList.IndexOf(StaticVar.CurrentMenu);
            
            //打开详细信息界面
            WindowManager.instance.Close<WaiterInforListsWnd>();
            WindowManager.instance.Open<WaiterInfor>();
            WindowManager.instance.Get<WaiterInfor>().SelectWaiter = MessageSend.instance.currenthavewaiters[n];
            WindowManager.instance.Get<WaiterInfor>().Initialize();

        }
        //返回咖啡店界面
        else if (Input.GetKeyDown(KeyCode.X))
        {
            WindowManager.instance.Close<WaiterInforListsWnd>();
            WindowManager.instance.Open<CoffeeWnd>();
            WindowManager.instance.Get<CoffeeWnd>().lastwndName = transform.name;
            WindowManager.instance.Get<CoffeeWnd>().Initialize();

        }
        RefreshProInfor();
    }
    //刷新显示
    private void RefreshWaiterList()
    {
        foreach (var item in MessageSend.instance.currenthavewaiters)
        {
            int n = MessageSend.instance.currenthavewaiters.IndexOf(item);
            Transform temp = menuList[n] as Transform;
            temp.Find("Name").GetComponent<Text>().text = item.Name;
            temp.Find("Icon").GetComponent<Image>().sprite = LoadTexture.getInstance().LoadAtlasSprite("Atlas/UI/PropertyIcon/Characters/Head/Head", item.Name);
        }
    }
    private void RefreshProInfor()
    {
        if (StaticVar.CurrentMenu!= null)
        {
            if (menuList.Contains(StaticVar.CurrentMenu))
            {
                int n = menuList.IndexOf(StaticVar.CurrentMenu);
                transform.Find("Infor/Name/Text2").GetComponent<Text>().text = MessageSend.instance.currenthavewaiters[n].Name;
                transform.Find("Infor/Age/Text2").GetComponent<Text>().text = MessageSend.instance.currenthavewaiters[n].Age.ToString();
                transform.Find("Infor/Job/Text2").GetComponent<Text>().text = MessageSend.instance.currenthavewaiters[n].Job.ToString();
                transform.Find("Infor/Interest/Text2").GetComponent<Text>().text = MessageSend.instance.currenthavewaiters[n].Interest;
                transform.Find("Infor/Special/Text2").GetComponent<Text>().text = MessageSend.instance.currenthavewaiters[n].Special;
                transform.Find("Infor/Intro/Text2").GetComponent<Text>().text = MessageSend.instance.currenthavewaiters[n].Introduce;
            }
        }
       
    }
}
