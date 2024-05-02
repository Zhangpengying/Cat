using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 初始化Home场景
/// </summary>
public class StartPointSceneInit : MonoBehaviour, SceneInitManager
{
    public void Start()
    {
        GameObject.Find("UI/Canvas").GetComponent<Canvas>().worldCamera = GameObject.Find("Environment/Camera/UICamera").GetComponent<Camera>();

        Initialize();
        //WindowManager.instance.Open<PersonInforWnd>().Initialize();
    }

    public void Initialize()
    {

        //初次进入场景
        if (StaticVar.PlayerAttribute.Count == 0)
        {
            ActorManager.instance.CreatPlayer(MessageSend.instance.playerCfgs[0]);

            StaticVar.player = (Player)ActorManager.instance.GetActor(0);

            //设置玩家进入场景的位置
            foreach (var item in MessageSend.instance.bornCfgs)
            {
                if (item.Value.LevelID == 7)
                {
                    StaticVar.player.transform.position = item.Value.Position;
                }
            }

            //游戏基本信息赋值
            StaticVar.CurrentDay = 1.ToString();
            StaticVar.CurrentWeek = "周一";
            StaticVar.CurrentTimeFrame = "上午";
        }
        //多次进入场景
        else
        {
            StaticVar.LastGateway = MessageSend.instance.doorsCfg[901].Position;
            ActorManager.instance.CreateActorCon();

        }


        //玩家信息记录
        if (PlayerPrefs.HasKey("HaveSave"))
        {
            StaticVar.HaveSave = PlayerPrefs.GetInt("HaveSave");
            //判定玩家是否有存档
            if (StaticVar.HaveSave == 0)
            {
                //无存档
                StaticVar.SavePlayerInfor(StaticVar.player);
            }
            //有存档
            else
            {

                //判定是否是周期循环
                if (StaticVar.PlayerAttribute.Count == 0)
                {
                    StaticVar.ReadSaveInfor();
                    StaticVar.player.PlayerMoney = (int)StaticVar.PlayerAttribute["Money"];
                    StaticVar.player.TransState(StaticVar.player, (ActorStateType)StaticVar.PlayerAttribute["Money"]);
                }

            }
        }
        else
        {
            //无存档
            StaticVar.SavePlayerInfor(StaticVar.player);
        }

        TimerManager.instance.Invoke(1f, delegate {
            StaticVar.player.transform.Find("NormalTrigger").GetComponent<PlayerTrigger>().AutoTrigge();
        });
    }

    public void Finalise()
    {
        //场景的清算
        StaticVar.ClearScene();
    }
    public void EndInteraction()
    {
        StaticVar.EndInteraction();

    }
    public void OpenSelectWnd()
    {
        WindowManager.instance.Open<SelectWnd1>().Initialize();
    }

}
