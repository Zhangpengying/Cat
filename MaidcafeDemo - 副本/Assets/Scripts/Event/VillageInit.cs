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
        //�¼����
        StaticVar.CheckEvents();

        //�������
        ActorManager.instance.CreateActorCon();
        StaticVar.player.GetComponent<Transform>().localPosition = new Vector3(-2.4f, -3.8f, 0);
        StaticVar.player.GetComponent<Transform>().localScale = new Vector3(2f, 2f, 2f);
        StaticVar.player.GetComponent<Player>()._moveSpeed = 5;

        StaticVar.CurrentTimeFrame = "����";
        WindowManager.instance.Open<BasicInforWnd>().Initialize();




        //��������NPC

        //����Rain
        //if (MessageSend.instance.Events.ContainsKey("TriggerDate") && MessageSend.instance.Events["TriggerDate"])
        //{
        //    ActorManager.instance.CreateActor(MessageSend.instance.roleCfgs[713]);
        //}
        ////����RainFa
        //if (MessageSend.instance.Events.ContainsKey("RainFaTriggerPoint") && MessageSend.instance.Events["RainFaTriggerPoint"])
        //{
        //    ActorManager.instance.CreateActor(MessageSend.instance.roleCfgs[714]);
        //}
        //�ؼҵĴ��Ϳ���
        //if (player._stateType != ActorStateType.Player_Chivied)
        //{
        //    if (GameObject.Find("Environment/Events/StreetToHome") != null)
        //    {
        //        GameObject.Find("Environment/Events/StreetToHome").GetComponent<ToHome>().openDoor = true;
        //    }
        //}

        //�������׷��
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
    //����׷�����
    public void CatchPlayer()
    {
        StaticVar.InteractiveNpc.TransState(StaticVar.InteractiveNpc, ActorStateType.CatchPlayer);
    }



    //����Լ��
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
