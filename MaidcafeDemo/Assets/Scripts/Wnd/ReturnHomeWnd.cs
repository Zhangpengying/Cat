using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReturnHomeWnd : BaseWnd
{
    public void Initialize()
    {
        StaticVar.LastGateway = MessageSend.instance.doorsCfg[901].Position;
        ActorManager.instance.CreateActorCon();
        StaticVar.player.GetComponent<Rigidbody2D>().gravityScale = 0;
        Button btnReturn = _transform.Find("ReturnBtn").GetComponent<Button>();
        btnReturn.onClick.AddListener(Return);
    }

    public void Return()
    {
        StaticVar.ToNextScenes("Home_Normal_01", StaticVar.player);
        WindowManager.instance.Close<ReturnHomeWnd>();
    }
}
