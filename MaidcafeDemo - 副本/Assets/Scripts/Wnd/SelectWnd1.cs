using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectWnd1 : BaseWnd
{
    public void Initialize()
    {
        StaticVar.CurrentMenu = _transform.Find("Adjust");
        _transform.gameObject.AddComponent<SelectAdjustMenuWndControl>();
    }
	
}

public class SelectAdjustMenuWndControl : MonoBehaviour
{
    private ArrayList menu = new ArrayList();
    private void Start()
    {
        foreach (var item in transform.GetComponentsInChildren<ButtonStateAdjust>())
        {
            if (!menu.Contains(item.transform))
            {
                menu.Add(item.transform);
            }
        }
        //调整文字显示
        if (StaticVar.InteractiveProp!=null)
        {
            if (StaticVar.InteractiveProp.name == "AdjustWaiter")
            {
                transform.Find("Adjust/Text").GetComponent<Text>().text = "调整女仆";
            }
            else if (StaticVar.InteractiveProp.name == "AdjustMenu")
            {
                transform.Find("Adjust/Text").GetComponent<Text>().text = "调整菜单";
            }
           
            else if (StaticVar.InteractiveProp.name == "OperatingFloor" )
            {
                transform.Find("Adjust/Text").GetComponent<Text>().text = "查看";
            }
            else if (StaticVar.InteractiveProp.name == "AdjustCommodity")
            {
                transform.Find("Adjust/Text").GetComponent<Text>().text = "查看";
            }
            
            else
            {
                transform.Find("Adjust/Text").GetComponent<Text>().text = "确定";
            }
        }
    }

    private void Update()
    {
        StaticVar.InputControl1(menu);
        //修改透明度
        foreach (var item in menu)
        {
            if (((Transform)item) == StaticVar.CurrentMenu)
            {
                ((Transform)item).Find("Text").GetComponent<Text>().color = new Color(1,1,1,1);
                ((Transform)item).Find("Image").GetComponent<Image>().color = new Color(1, 1, 1, 1);            }
            else
            {
                ((Transform)item).Find("Text").GetComponent<Text>().color = new Color(1, 1, 1, 0.4f);
                ((Transform)item).Find("Image").GetComponent<Image>().color = new Color(1, 1, 1, 0.4f);
            }
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (StaticVar.CurrentMenu == transform.Find("Adjust"))
            {
                //调整后续打开界面
                if (StaticVar.InteractiveProp != null)
                {
                    if (StaticVar.InteractiveProp.name == "AdjustWaiter")
                    {
                        WindowManager.instance.Open<ChangeWaiterWnd>().Initialize();
                    }
                    else if (StaticVar.InteractiveProp.name == "AdjustMenu")
                    {
                        WindowManager.instance.Open<AdjustMenuWnd>().Initialize();
                    }
                    else if (StaticVar.InteractiveProp.name == "StartManage")
                    {
                        //判定是否达到经营的条件
                        if (MessageSend.instance.currentDayWaiters.Count!=0 && MessageSend.instance.CurrentDayMenus.Count!=0 && MessageSend.instance.CurrentDayCom.Count!=0)
                        {
                            MessageSend.instance.propertyIDs.Clear();
                            StaticVar.ToNextScenes("Manage", GameObject.FindWithTag("Player").GetComponent<Player>());
                        }
                       
                    }
                    else if (StaticVar.InteractiveProp.name == "OperatingFloor")
                    {
                        WindowManager.instance.Open<ImproveMealWnd>().Initialize();
                    }
                    else if (StaticVar.InteractiveProp.name == "AdjustCommodity")
                    {
                        WindowManager.instance.Open<AdjustCommodityWnd>().Initialize();
                    }
                    else if (StaticVar.InteractiveProp.name == "DIY")
                    {
                        WindowManager.instance.Open<NewDIYWmd>().Initialize();
                    }
                    else if (StaticVar.InteractiveProp.name == "EndRest")
                    {
                        WindowManager.instance.Open<WindupWnd>().Initialize();
                    }
                    else if (StaticVar.InteractiveProp.name == "SavePoint")
                    {
                        PlayerPrefs.SetInt("HaveSave",1);
                        StaticVar.EndInteraction();
                        StaticVar.WriteSaveInfor();
                    }
                    else if (StaticVar.InteractiveProp.name == "DeletePoint")
                    {
                        StaticVar.DeleteData();
                        StaticVar.EndInteraction();
                    }
                    else if (StaticVar.InteractiveProp.name == "Menu")
                    {
                        WindowManager.instance.Open<StartPreparationWnd>().Initialize();
                    }
                }
               
            }
            else
            {
                StaticVar.EndInteraction();
            }
            GameObject.Find("Fungus/SayDialog").SetActive(false);
            WindowManager.instance.Close<SelectWnd1>();

        }
    }
}
