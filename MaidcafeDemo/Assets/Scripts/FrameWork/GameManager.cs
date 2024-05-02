using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
   
    void Awake()
    {

      
        DontDestroyOnLoad(this);
        //场景加载前先加载配置文件
        LoadConfig();
        ConfigManager.instance.Initialize(new ConfigParser());
        

        //创建存档文件
        CreateSaveFloder();
        //传送阵对应加载
        LoadGateways();
        //打开初始界面
        WindowManager.instance.Initialize();

        AssignDIYPropertys();
        AssignCurrentMenus();
        AssignCurrentHaveCom();
        AssignCurrentHavaSysPro();
        AssignWaiters();
        AssignUnlockCus();
    }

    private void Start() { }
    
    void Update()
    {
        float dt = Time.deltaTime;
        WindowManager.instance.Update(dt);
        TimerManager.instance.Update(dt);
        ActorManager.instance.Update();

        if (Input.GetKeyDown(KeyCode.K))
        {

        }
    }

    //加载配置文件
    public void LoadConfig()
    {
        ConfigManager.instance.Initialize(new ConfigParser());
        //场景加载前先加载配置文件
        MessageSend.instance.waiterCfgs.Clear();
        MessageSend.instance.customerCfgs.Clear();
        MessageSend.instance.playerCfgs.Clear();
        ConfigManager.instance.LoadAllConfigs();
        
        StaticVar.LoadWaiAndCus();
    }

    //传送阵对应加载
    private void LoadGateways()
    {
        if (MessageSend.instance.Gateways.Count == 0)
        {
            MessageSend.instance.Gateways.Add("HomeToStreet", "StreetToHome");
            MessageSend.instance.Gateways.Add("StreetToHome", "HomeToStreet");
            MessageSend.instance.Gateways.Add("StreetToCoffee", "CoffeeToStreet");
            MessageSend.instance.Gateways.Add("CoffeeToStreet", "StreetToCoffee");
        }
    }

    //存档文件创建
    private void CreateSaveFloder()
    {
        if (!Directory.Exists(StaticVar.SavePath))
        {
            Directory.CreateDirectory(StaticVar.SavePath);

            File.CreateText(StaticVar.SavePath + "/BlockName.txt").Dispose();
            File.CreateText(StaticVar.SavePath + "/PlayerInfor.txt").Dispose();
            File.CreateText(StaticVar.SavePath + "/HaveMenu.txt").Dispose();
            File.CreateText(StaticVar.SavePath + "/HaveWaiter.txt").Dispose();
            File.CreateText(StaticVar.SavePath + "/DIYBag.txt").Dispose();
            File.CreateText(StaticVar.SavePath + "/HaveCommodify.txt").Dispose();
            File.CreateText(StaticVar.SavePath + "/CurrentTime.txt").Dispose();
            File.CreateText(StaticVar.SavePath + "/Events.txt").Dispose();
            File.CreateText(StaticVar.SavePath + "/PropertyToID.txt").Dispose();
            File.CreateText(StaticVar.SavePath + "/CreatPropertysInfo.txt").Dispose();
            
        }

    }

    //当前触发的服务员
    private void AssignWaiters()
    {
        foreach (var item in MessageSend.instance.waiterCfgs)
        {
            if (!MessageSend.instance.currenthavewaiters.Contains(item.Value))
            {
                MessageSend.instance.currenthavewaiters.Add(item.Value);
            }
        }
    }
    //当前解锁的特殊客人
    private void AssignUnlockCus()
    {
        foreach (var item in MessageSend.instance.customerCfgs)
        {
            if (!MessageSend.instance.CurrUnLockCustomer.Contains(item.Value))
            {
                MessageSend.instance.CurrUnLockCustomer.Add(item.Value);
                
            }
            if (!MessageSend.instance.allcustomers.Contains(item.Value))
            {
                MessageSend.instance.allcustomers.Add(item.Value);

            }
        }
    }

    //当前拥有的DIY物品赋值
    private void AssignDIYPropertys()
    {
        foreach (var item in MessageSend.instance.propertyCfgs)
        {
            MessageSend.instance.CurrentHavePropertys.Add(item.Value);
        }
    }
    //当前拥有的菜品赋值
    private void AssignCurrentMenus()
    {
        HaveMenuIO.instance.Read();
        //第一次进游戏
        if (MessageSend.instance.HaveMenus.Count == 0)
        {
            MessageSend.instance.HaveMenus.Add(MessageSend.instance.menuCfgs[1000]);
            MessageSend.instance.HaveMenus.Add(MessageSend.instance.menuCfgs[1001]);
            MessageSend.instance.HaveMenus.Add(MessageSend.instance.menuCfgs[1002]);
            MessageSend.instance.HaveMenus.Add(MessageSend.instance.menuCfgs[1004]);
            MessageSend.instance.HaveMenus.Add(MessageSend.instance.menuCfgs[1005]);
            MessageSend.instance.HaveMenus.Add(MessageSend.instance.menuCfgs[1006]);
        }
        else if (MessageSend.instance.HaveMenus.Count == 2)
        {

        }
    }

    //周边物品赋值
    private void AssignCurrentHaveCom()
    {
        foreach (var item in MessageSend.instance.commodityCfg)
        {
            if (!MessageSend.instance.CurrentHaveCom.Contains(item.Value))
            {
                MessageSend.instance.CurrentHaveCom.Add(item.Value);
            }
        }
    }

    //当前系统背包物品赋值
    private void AssignCurrentHavaSysPro()
    {
        foreach (var item in MessageSend.instance.systemPropertyCfg)
        {
            if (!MessageSend.instance.CurrentHaveSysPro.Contains(item.Value))
            {
                MessageSend.instance.CurrentHaveSysPro.Add(item.Value);
            }
        }
    }

}
