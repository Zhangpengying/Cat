using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageInit : MonoBehaviour, SceneInitManager
{
    private void Start()
    {
        GameObject.Find("UI/Canvas").GetComponent<Canvas>().worldCamera = GameObject.Find("Environment/Camera/UICamera").GetComponent<Camera>();
        Initialize();
    }

    public void Initialize()
    {
        //事件监测
        StaticVar.CheckEvents();

        //创建玩家
        ActorManager.instance.CreateActorCon();
        StaticVar.player.GetComponent<Transform>().localPosition = new Vector3(-2.4f, -3.8f, 0);
        StaticVar.player.GetComponent<Transform>().localScale = new Vector3(2f, 2f, 2f);
        StaticVar.player.GetComponent<Player>()._moveSpeed = 5;

        StaticVar.CurrentTimeFrame = "晚上";
        WindowManager.instance.Open<BasicInforWnd>().Initialize();




        //创建互动NPC

        //创建Rain
        //if (MessageSend.instance.Events.ContainsKey("TriggerDate") && MessageSend.instance.Events["TriggerDate"])
        //{
        //    ActorManager.instance.CreateActor(MessageSend.instance.roleCfgs[713]);
        //}
        ////创建RainFa
        //if (MessageSend.instance.Events.ContainsKey("RainFaTriggerPoint") && MessageSend.instance.Events["RainFaTriggerPoint"])
        //{
        //    ActorManager.instance.CreateActor(MessageSend.instance.roleCfgs[714]);
        //}
        //回家的传送开关
        //if (player._stateType != ActorStateType.Player_Chivied)
        //{
        //    if (GameObject.Find("Environment/Events/StreetToHome") != null)
        //    {
        //        GameObject.Find("Environment/Events/StreetToHome").GetComponent<ToHome>().openDoor = true;
        //    }
        //}

        //设置相机追随
        StaticVar.SetCamera();
    }
    public void Finalise()
    {
        StaticVar.ClearScene();
    }
    public void EndInteraction()
    {
        StaticVar.EndInteraction();
    }
    //触发追赶玩家
    public void CatchPlayer()
    {
        StaticVar.InteractiveNpc.TransState(StaticVar.InteractiveNpc, ActorStateType.CatchPlayer);
    }



    //结束约会
    public void EndDate()
    {
        StaticVar.player.TransState(StaticVar.player, ActorStateType.Player_Idle);
        ActorManager.instance.RemoveActor(StaticVar.player.InteractNPC._uid);

        StaticVar.EndInteraction();
    }
    public void OpenSelectWnd()
    {
        WindowManager.instance.Open<SelectWnd1>().Initialize();
    }

}
