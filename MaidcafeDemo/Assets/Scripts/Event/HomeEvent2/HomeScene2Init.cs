using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeScene2Init : MonoBehaviour ,SceneInitManager
{

    public void Start()
    {
        GameObject.Find("UI/Canvas").GetComponent<Canvas>().worldCamera = GameObject.Find("Environment/Camera/UICamera").GetComponent<Camera>();

        Initialize();
        WindowManager.instance.Open<BasicInforWnd>().Initialize();
    }

    public void Initialize()
    {
        StaticVar.LastGateway = MessageSend.instance.doorsCfg[901].Position;
        ActorManager.instance.CreateActorCon();
        
        //事件监测
        StaticVar.CheckEvents();

     



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
    public void Sleep()
    {
        TimerManager.instance.Invoke(2, delegate { StaticVar.ClearScene(); StaticVar.ToNextSecens("Home_Normal_01", StaticVar.player); AdjustBasicInfor(); });
    }

    private void AdjustBasicInfor()
    {
        //游戏基本信息赋值
        int n = int.Parse(StaticVar.CurrentDay);
        StaticVar.CurrentDay = (n + 1).ToString();
        switch ((n + 1) % 7)
        {
            case 0:
                StaticVar.CurrentWeek = "周日";
                break;
            case 1:
                StaticVar.CurrentWeek = "周一";
                break;
            case 2:
                StaticVar.CurrentWeek = "周二";
                break;
            case 3:
                StaticVar.CurrentWeek = "周三";
                break;
            case 4:
                StaticVar.CurrentWeek = "周四";
                break;
            case 5:
                StaticVar.CurrentWeek = "周五";
                break;
            default:
                StaticVar.CurrentWeek = "周六";
                break;
        }

        StaticVar.CurrentTimeFrame = "上午";
    }
}
