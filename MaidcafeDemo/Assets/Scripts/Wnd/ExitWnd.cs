using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExitWnd : BaseWnd
{
    public void Initialize()
    {
        _transform.gameObject.AddComponent<ExitWndControl>();
    }
   
}

public class ExitWndControl:MonoBehaviour
{
    private void Start()
    {
       
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            ExitJingYing();
        }  
    }


    private void ExitJingYing()
    {
        MessageSend.instance.stayPointList.Clear();
        MessageSend.instance.waiter.Clear();
        MessageSend.instance.customer.Clear();
        MessageSend.instance.firstFloorWaiter.Clear();
        MessageSend.instance.secondFloorWaiter.Clear();
        MessageSend.instance.firstFloorSeats.Clear();
        MessageSend.instance.secondFloorSeats.Clear();
        MessageSend.instance.combine.Clear();
        MessageSend.instance.customerSeat.Clear();
        MessageSend.instance.seatToService.Clear();

        MessageSend.instance.CreatPropertys.Clear();

        //场景的清算
        StaticVar.ClearScene();


        WindowManager.instance.Close<ExitWnd>();
        WindowManager.instance.Close<JingYing>();

        StaticVar.ToNextScenes("Rest", GameObject.FindWithTag("Player").GetComponent<Player>());

        //开启特殊模式场景
        //if (StaticVar.ManageComData.Count > 3)
        //{
        //    StaticVar.ToNextSecens("InteractionScene1", GameObject.FindWithTag("Player").GetComponent<Player>());
        //}
        ////开启中场休息场景
        //else
        //{
        //    StaticVar.ToNextSecens("Rest", GameObject.FindWithTag("Player").GetComponent<Player>());
        //}
        //StaticVar.ToNextSecens("Home_Normal_01", GameObject.FindWithTag("Player").GetComponent<Player>());

        Time.timeScale = 1;


    }
}

