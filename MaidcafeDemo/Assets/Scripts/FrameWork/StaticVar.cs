﻿using Fungus;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public static class StaticVar
{
    //玩家
    public static Player player;
    //顾客梦幻度
    public static int dreamDegrees = 0;
    //店里的气氛
    public static int mood = 0;
    //女仆的魅力
    public static int charm = 0;
    //菜单的新鲜度
    public static int freshness = 0;
    //女仆的宅力
    public static int residence = 0;
    //女仆的体力
    public static int physicalStrength = 0;
    //女仆的接待等级
    public static int receptionLevel = 0;
    //女仆的歌唱水平
    public static int singLevel = 0;
    //女仆的舞蹈水平
    public static int danceLevel = 0;
    //女仆的移动速度
    public static int characterspeed = 5;
    //角色与对话框之间偏移值
    //public static Vector3 offest = new Vector3(-214.6f,-36.2f,0);
    //女仆服务位置与椅子之间的距离
    public static Vector3 dis1 = new Vector3(0, 0, 0);
    //女仆鞠躬的时间
    public static float bowlength;
    //女仆放下菜后施法前的等待时间
    public static float waiteConjure = 1.2f;
    //女仆闲置状态下每次到达目标点后等待的时间
    public static float waiteFindNextpoint = 2f;
    //顾客期待的时间
    public static float expectTime = 2.5f;
    //顾客感谢的时间
    public static float thanksTime = 1f;
    //顾客吃饭或者喝水的时间
    public static float eatingTime = 25f;
    //点单的时间(女仆到达位置后开始计时)
    public static float takeOrderTime = 5f;
    //点单开始到去取菜的间隔时间
    public static float takeFoodTime = 6f;
    //两个相邻客人的创建间隔时间
    public static float betweenCustomers = 8f;
    //经营总时间
    public static float totalOperatingtime = 20f;
    //判定当前咖啡店是否有空位
    public static bool haveEmptySeat = false;
    //店满情况下离开一个客人后后续创建客人的间隔时间
    public static float waitEmptyTime = 2f;
    //当前的经营时间
    public static float currentManageTime = 0f;
    //客人坐下前的缓冲时间
    public static float sitdownBufferTime = 1f;
    //客人起身后的缓冲时间
    public static float situpBufferTime = 1f;
    //楼层分界坐标
    public static Vector3 boundaries = Vector3.zero;
    //sprite透明度渐变时间
    public static float gradualChangeTime = 0.5f;
    //上下楼层的时间
    public static float upStairTime = 1f;
    //目标FlowChart
    public static Flowchart TipsFlowChart;
    //当前日期
    public static int Week;
    //当前时段0白天 1下午 2夜里 3深夜
    public static int TimeFrame = 0;
    //下个场景
    public static string NextScene = "";
    //当前交互道具
    public static GameObject InteractiveProp = null;
    //发呆次数（触发喵咪）
    public static int FaidaiCatNumber = 0;

    //是否触发过发呆的要切场景的故事
    public static bool CatEvent = false;

    //Rin和玩家的友好度
    public static int RinFriendliness = 5;

    //Rin事件触发开关 0不可触发（尚未满足触发条件） 1可触发（待触发）  2已触发（已经触发一次，不可再触发）
    public static int RinEventTrigger = 0;

    //约会状态：0 不在约会，1和Rin约会
    public static int Datestate = 0;

    //正在执行的，会锁住门的事件的
    public static string PlayerLockDoorEvent;

    //转场景时的玩家属性
    public static Hashtable PlayerAttribute = new Hashtable();
    //游戏保存路径

    public static string SavePath = @"C:\\Maidcafe";
    //玩家在当前场景的初始位置
    public static Vector3 LastGateway;
    //当前已触发的互动NPC
    public static Actor InteractiveNpc;

    //当前金币数
    public static int money = 500;
    //地图宽度
    public static int mapWidth = 100;
    //地图长度
    public static int mapLength = 100;
    //格子大小
    public static float gridSize = 1f;
    //是否回收
    public static bool IsRemove = false;
    //当前选项
    public static Transform CurrentMenu;
    //上个界面确定选项的路径
    public static string LastCurrPath;
    //当天经营的菜品数据详细
    public static Dictionary<MenuCfg, int> ManageMenuData = new Dictionary<MenuCfg, int>();
    //当天经营的周边数据详细
    public static Dictionary<CommodityCfg, int> ManageComData = new Dictionary<CommodityCfg, int>();

    //玩家是否有存档(0为无存档，1为有存档)
    public static int HaveSave = 0;
    //键盘按键持续按下的检测时间
    public static float GetKeyTime = 1f;
    //键盘持续按下后的移动速度
    public static float Velocity = 0.02f;
    //游戏基本信息
    public static string CurrentDay = "";
    //当前星期几
    public static string CurrentWeek = "";
    //当前时段
    public static string CurrentTimeFrame = "";

    //对话
    public static string say1 = "请问要点些什么？";
    public static string say2 = "酸菜鱼";
    public static string say3 = "好的，你稍等";
    public static string say4 = "主人，欢迎回家！";


    //座位及服务位置坐标转化为可移动坐标
    public static Vector3 VecTranslate(Transform temp)
    {
        if (temp!= null)
        {
            if (temp.parent.name != "FirstFloorItems")
            {
                //该物体相对父物体A的世界坐标
                Vector3 homePosInWorld = temp.parent.TransformPoint(temp.localPosition);
                //转换为相对父物体B的本地坐标
                Vector3 homePosInHomeMgrLocal = GameObject.Find("FirstFloorItems").transform.InverseTransformPoint(homePosInWorld);
                return homePosInHomeMgrLocal;
                //return new Vector3(temp.parent.localPosition.x - temp.localPosition.x, temp.parent.localPosition.y + temp.localPosition.y, 0);
            }
            else
            {
                return temp.localPosition;

            }
        }
        else
        {
            return new Vector3();
        }
    }

    //分离女仆库和客人库
    public static void LoadWaiAndCus()
    {
        //foreach (var cfg in MessageSend.instance.roleCfgs)
        //{
        //    if (cfg.Value.RoleType == RoleType.Waiter)
        //    {
        //        MessageSend.instance.allwaiters.Add(cfg.Value);
        //        MessageSend.instance.currenthavewaiters.Add(cfg.Value);
        //    }
        //    else if (cfg.Value.RoleType == RoleType.Customer)
        //    {
        //        MessageSend.instance.allcustomers.Add(cfg.Value);

        //    }

        //}
    }

    /// <summary>
    /// sprite的透明度渐变
    /// </summary>
    /// <param name="spite"></param>需要渐变处理的物体
    /// <param name="target"></param>渐变的目标透明度
    /// /// <param name="time"></param>渐变的时间
    public static void GradualChange(GameObject sprite, float target, float time)
    {
        Hashtable argms = new Hashtable();
        argms.Add("alpha", target);
        argms.Add("time", time);
        iTween.FadeTo(sprite, argms);

    }

    /// <summary>
    /// 前往下个场景
    /// </summary>
    /// <param name="SceneNmae"> 目标场景</param>
    public static void ToNextScenes(string SceneName, Actor act)
    {
        ClearScene();

        if (act != null)
        {
            PlayerAttribute.Add("ActorID", act._uid);
            PlayerAttribute.Add("ActorName", act._name);
            PlayerAttribute.Add("ActorMoveSpeed", act._moveSpeed);
            PlayerAttribute.Add("ActorStatetype", act._stateType);
            int money = ((Player)act).PlayerMoney;
            PlayerAttribute.Add("Money", money);
            PlayerAttribute.Add("ItemList", ((Player)act).ItemList);
            PlayerAttribute.Add("PlayerHP", ((Player)act).PlayerHP);

        }
        SceneManager.LoadScene(SceneName);

    }

    //清理场景存储
    public static void ClearScene()
    {
        PlayerAttribute.Clear();
        //当前场景事件清空
        MessageCenter.Managers.Clear();
        //切场景人物管理列表清空
        List<int> temp = new List<int>(ActorManager.instance._actorDic.Keys);
        MessageSend.instance.triggerEventID.Clear();
        for (int i = 0; i < temp.Count; i++)
        {
            ActorManager.instance.RemoveActor(temp[i]);
        }
    }

    //执行Block
    public static void MessageSendToFungus(string blockname, Player player)
    {
        player.IsLockPlayer = true;
        player._ani.SetBool("IsWalk", false);
        Flowchart[] aa = UnityEngine.Object.FindObjectsOfType<Flowchart>();
        foreach (var item in aa)
        {
            if (item.FindBlock(blockname) != null)
            {
                item.ExecuteBlock(blockname);
            }
        }
    }

    //设置Camera追随目标
    public static void SetCamera()
    {
        Camera.main.transform.parent.Find("CM vcam1").GetComponent<Cinemachine.CinemachineVirtualCamera>().Follow = GameObject.FindGameObjectWithTag("Player").transform;
    }

    //开始交互
    public static void StartInteraction()
    {
        GameObject obj = GameObject.FindWithTag("Player");
        if (obj != null)
        {
            obj.GetComponent<Player>().IsLockPlayer = true;
            obj.GetComponent<Player>()._ani.SetBool("IsWalk", false);

        }
    }

    //结束交互
    public static void EndInteraction()
    {
        GameObject obj = GameObject.FindWithTag("Player");
        if (obj != null)
        {
            obj.GetComponent<Player>().IsLockPlayer = false;
        }
        GameObject say = GameObject.Find("Fungus/SayDialog");
        if (say != null)
        {
            say.SetActive(false);
        }
        
    }

    //事件添加
    public static void AddEvents(int _eventID,Hashtable infor)
    {
        if (!MessageSend.instance.Events.ContainsKey(_eventID))
        {
            MessageSend.instance.Events.Add(_eventID, infor);
        }

    }

    //事件监测
    public static void CheckEvents()
    {
        //GameObject[] eventobj = GameObject.FindGameObjectsWithTag("Event");
        //List<string> events = new List<string>();
        //foreach (var item in eventobj)
        //{
        //    events.Add(item.name);
        //    item.SetActive(false);
        //}
        //foreach (var item in MessageSend.instance.Events.Values)
        //{
        //    if (events.Contains((item["Name"] as string)))
        //    {
        //        eventobj[0].transform.parent.Find((item["Name"] as string)).gameObject.SetActive(true);
        //    }
           
        //}
       
    }
   

    //通过世界坐标点获取相对的格子位置
    public static Vector3 GetLocalPos(Vector3 globalPos)
    {
        return new Vector3((int)(globalPos.x + mapWidth / 2), 0f, (int)(globalPos.z + mapLength / 2));
    }

    //通过相对位置获取世界坐标
    public static Vector3 GetGlobalPos(Vector3 localPos)
    {
        return new Vector3(localPos.x - mapWidth / 2 + 0.5f, 0f, localPos.z - mapLength / 2 + 0.5f);
    }
    //按键控制(上下键)
    public static void InputControl1(ArrayList temp)
    {
        int n = temp.IndexOf(CurrentMenu);
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (n > 0)
            {
                CurrentMenu = (Transform)temp[n - 1];
            }

        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (n < temp.Count - 1)
            {
                CurrentMenu = (Transform)temp[n + 1];
            }

        }

    }
    //按键控制(左右键)
    public static void InputControl2(ArrayList temp)
    {
        int n = temp.IndexOf(CurrentMenu);
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (n > 0)
            {
                CurrentMenu = (Transform)temp[n - 1];
            }

        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (n < temp.Count - 1)
            {
                CurrentMenu = (Transform)temp[n + 1];
            }

        }

    }
    //门
    public static void Door(int ID, Player player, int eventID)
    {
        
        switch (ID)
        {
            case 903:
                //上楼
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    player.transform.position = LastGateway;
                }
                break;
            case 904:
                //下楼
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    player.transform.position = LastGateway;
                }
                break;

            default:
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    MessageSend.instance.triggerEventID.Remove(eventID);
                    ToNextScenes(NextScene, player);
                }
                break;
        }

    }
    //传送阵
    public static void GateWay(int ID, Player player)
    {
        string gatewayName = MessageSend.instance.gatewaysCfg[ID].Name;
        switch (ID)
        {
            case 1101:
                //街上场景返回家里
                ToNextScenes(NextScene, player);

                break;
            case 1102:
                //场景的清算
                ClearScene();
                ToNextScenes("Village", player);
                break;
            default:
                break;
        }
    }
    //字符串转化为Vector3
    public static Vector3 ParseVector3(string str)
    {
        str = str.Replace("(", "").Replace(")", "");
        string[] s = str.Split(',');
        Vector3 temp = new Vector3(float.Parse(s[0]), float.Parse(s[1]), float.Parse(s[2]));
        return temp;
    }
    //字符串转化为Vector2
    public static Vector2 ParseVector2(string str)
    {
        str = str.Replace("(", "").Replace(")", "");
        string[] s = str.Split(',');
        Vector3 temp = new Vector3(float.Parse(s[0]), float.Parse(s[1]));
        return temp;
    }

    //玩家信息记录
    public static void SavePlayerInfor(Player player)
    {
        PlayerAttribute.Clear();
        PlayerAttribute.Add("ActorID", player._uid);
        PlayerAttribute.Add("ActorName", player._name);
        PlayerAttribute.Add("ActorMoveSpeed", player._moveSpeed);
        PlayerAttribute.Add("ActorStatetype", player._stateType);
        PlayerAttribute.Add("Money", player.PlayerMoney);

    }

    //提交信息保存
    public static void WriteSaveInfor()
    {
        PlayerInforIO.instance.Write();
        HaveMenuIO.instance.Write();
        BlockNameIO.instance.Write();
        CurrentTimeIO.instance.Write();
        DIYBagIO.instance.Write();
        HaveCommodifyIO.instance.Write();
        HaveWaiterIO.instance.Write();
        CreatPropertysInfoIO.instance.Write();
        PropertyToIDIO.instance.Write();
    }
    //读取存档信息
    public static void ReadSaveInfor()
    {
        PlayerInforIO.instance.Read();
        HaveMenuIO.instance.Read();
        BlockNameIO.instance.Read();
        CurrentTimeIO.instance.Read();
        DIYBagIO.instance.Read();
        HaveCommodifyIO.instance.Read();
        HaveWaiterIO.instance.Read();
        CreatPropertysInfoIO.instance.Read();
        PropertyToIDIO.instance.Read();
    }
    //删除存档
    public static void DeleteData()
    {
        if (Directory.Exists(SavePath))
        {
            Directory.Delete(SavePath, true);
            PlayerPrefs.DeleteKey("HaveSave");
        }

    }

    //UI坐标转化为世界坐标
    public static Vector3 UIToScreenPos(Canvas canvas, GameObject obj1)
    {
        Vector3 scr = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, obj1.transform.position);
        scr.z = 0;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(scr);
        return new Vector3(worldPos.x, worldPos.y, 0);
    }
    //世界坐标转化为UI坐标
    public static Vector3 WorldPosToUI(Canvas canvas, GameObject UI, GameObject obj)
    {
        //先把物体的世界坐标转化为屏幕坐标
        Vector3 scr = RectTransformUtility.WorldToScreenPoint(Camera.main, obj.transform.position);
        //把这个屏幕坐标转化为UI坐标
        Vector2 uiPos2D;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(UI.GetComponent<RectTransform>(), scr, canvas.worldCamera, out uiPos2D);
        Vector3 uiPos3D = new Vector3(uiPos2D.x, uiPos2D.y, 0);
        return uiPos3D;
    }
   //判定事件优先级
   public static void PreferenceEvent(Dictionary<int, Hashtable> events)
    {
        List<int> temp = new List<int>(events.Keys);
        int minID = temp[0];
        foreach (var item in events)
        {
            if (item.Key < minID)
            {
                minID = item.Key;
            }
        }
        ((Action)events[minID]["Action"])();
    }
    //判定物品是否存在
    public static bool GetItem(int itemID)
    {
        bool isHave = false;
        foreach (var item in StaticVar.player.ItemList)
        {
            if (item.itemID == itemID)
            {
                isHave = true;
            }
        }
        return isHave;

    }
    

}
