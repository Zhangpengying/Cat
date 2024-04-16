using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPreparationWnd : BaseWnd
{
    public void Initialize()
    {
        _transform.gameObject.AddComponent<StartPreparationWndCon>();
        StaticVar.CurrentMenu = _transform.GetChild(0);
    }

}

public class StartPreparationWndCon :MonoBehaviour
{
    private ArrayList BtnList = new ArrayList();
    private void Start()
    {
        foreach (var item in transform.GetComponentsInChildren<ButtonStateAdjust>())
        {
            if (!BtnList.Contains(item.transform))
            {
                BtnList.Add(item.transform);
            }
        }
    }
    private void Update()
    {
        if (BtnList.Contains(StaticVar.CurrentMenu))
        {
            StaticVar.InputControl1(BtnList);
            if (Input.GetKeyDown(KeyCode.Z))
            {
                switch (BtnList.IndexOf(StaticVar.CurrentMenu))
                {
                    case 0:
                        WindowManager.instance.Close<StartPreparationWnd>();
                        WindowManager.instance.Open<AdjustMenuWnd>().Initialize();
                        break;
                    case 1:
                        WindowManager.instance.Close<StartPreparationWnd>();
                        WindowManager.instance.Open<AdjustCommodityWnd>().Initialize();
                       
                        break;
                    case 2:
                        WindowManager.instance.Close<StartPreparationWnd>();
                        WindowManager.instance.Open<ChangeWaiterWnd>().Initialize();
                       
                        break;
                    case 3:
                        WindowManager.instance.Close<StartPreparationWnd>();
                        WindowManager.instance.Open<NewDIYWmd>().Initialize();
                        
                        break;
                    default:
                        //判定是否达到经营的条件
                        if (MessageSend.instance.currentDayWaiters.Count != 0 && MessageSend.instance.CurrentDayMenus.Count != 0 && MessageSend.instance.CurrentDayCom.Count != 0)
                        {
                            MessageSend.instance.propertyIDs.Clear();
                            StaticVar.ToNextSecens("Manage", GameObject.FindWithTag("Player").GetComponent<Player>());
                            WindowManager.instance.Close<StartPreparationWnd>();
                        }
                        break;
                       
                }
            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
                WindowManager.instance.Close<StartPreparationWnd>();
                StaticVar.EndInteraction();
            }
        }
    }
}
