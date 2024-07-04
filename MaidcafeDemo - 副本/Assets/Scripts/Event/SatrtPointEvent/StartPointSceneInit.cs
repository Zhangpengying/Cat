using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ��ʼ��Home����
/// </summary>
public class StartPointSceneInit : MonoBehaviour, SceneInitManager
{
    public void Start()
    {
        GameObject.Find("UI/Canvas").GetComponent<Canvas>().worldCamera = GameObject.Find("Environment/Camera/UICamera").GetComponent<Camera>();

        Initialize();
        WindowManager.instance.Open<BasicInforWnd>().Initialize();
    }

    public void Initialize()
    {

        //���ν��볡��
        if (StaticVar.PlayerAttribute.Count == 0)
        {
            ActorManager.instance.CreatPlayer(MessageSend.instance.playerCfgs[0]);

            StaticVar.player = (Player)ActorManager.instance.GetActor(0);

            //������ҽ��볡����λ��
            foreach (var item in MessageSend.instance.bornCfgs)
            {
                if (item.Value.LevelID == 7)
                {
                    StaticVar.player.transform.position = item.Value.Position;
                }
            }

            //��Ϸ������Ϣ��ֵ
            StaticVar.CurrentDay = 1.ToString();
            StaticVar.CurrentWeek = "��һ";
            StaticVar.CurrentTimeFrame = "����";
        }
        //��ν��볡��
        else
        {
            StaticVar.LastGateway = MessageSend.instance.doorsCfg[901].Position;
            ActorManager.instance.CreateActorCon();

        }


        //�����Ϣ��¼
        if (PlayerPrefs.HasKey("HaveSave"))
        {
            StaticVar.HaveSave = PlayerPrefs.GetInt("HaveSave");
            //�ж�����Ƿ��д浵
            if (StaticVar.HaveSave == 0)
            {
                //�޴浵
                StaticVar.SavePlayerInfor(StaticVar.player);
            }
            //�д浵
            else
            {

                //�ж��Ƿ�������ѭ��
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
            //�޴浵
            StaticVar.SavePlayerInfor(StaticVar.player);
        }

        TimerManager.instance.Invoke(1f, delegate {
            StaticVar.player.transform.Find("NormalTrigger").GetComponent<PlayerTrigger>().AutoTrigge();
        });
    }

    public void Finalise()
    {
        //����������
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
