using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagWnd : BaseWnd
{
    public void Initialize()
    {
        _transform.gameObject.AddComponent<BagWndControl>();
        
    }
}

public class BagWndControl : MonoBehaviour
{
    private ArrayList menuList = new ArrayList();
    private Transform content;
    private void Start()
    {
        
        content = transform.Find("Scroll View/Viewport/Content");
        foreach (var item in content.GetComponentsInChildren<ButtonStateAdjust>())
        {
            if (!menuList.Contains(item.transform))
            {
                menuList.Add(item.transform);
            }
        }
        StaticVar.CurrentMenu = (Transform)menuList[0];
        //初始化背包显示内容
        RefreshBag();
    }

    private void Update()
    {
        StaticVar.InputControl1(menuList);
        //使用道具
        if (Input.GetKeyDown(KeyCode.Z))
        {

        }
        //返回系统界面
        else if (Input.GetKeyDown(KeyCode.X))
        {
            WindowManager.instance.Close<BagWnd>();
            WindowManager.instance.Open<SystemMenuWnd>();
            WindowManager.instance.Get<SystemMenuWnd>().temp = transform.name;
            WindowManager.instance.Get<SystemMenuWnd>().Initialize();
        }
        RefreshProInfor();
    }

    private void RefreshBag()
    {
        foreach (var item in MessageSend.instance.CurrentHaveSysPro)
        {
            int n = MessageSend.instance.CurrentHaveSysPro.IndexOf(item);
            if (n<menuList.Count)
            {
                Transform temp = (Transform)menuList[n];
                temp.Find("Name").GetComponent<Text>().text = item.PropertyName;
                temp.Find("Icon").GetComponent<Image>().sprite = LoadTexture.getInstance().LoadAtlasSprite("Atlas/UI", item.PropertyName);
                temp.Find("HaveNum").GetComponent<Text>().text = item.HaveNum.ToString();
                if (item.Type == "菜谱")
                {
                    //已掌握
                    if (item.Exp == 0)
                    {
                        temp.Find("Exp").GetComponent<Text>().text = "未掌握";
                        temp.Find("Exp").gameObject.SetActive(true);
                    }
                    //未掌握
                    else
                    {
                        temp.Find("Exp").GetComponent<Text>().text = "已掌握";
                        temp.Find("Exp").gameObject.SetActive(true);
                    }
                }
                else if (item.Type == "普通物品")
                {
                    temp.Find("Exp").gameObject.SetActive(false);
                }
            }
        }
    }

    //物品信息刷新
    private void RefreshProInfor()
    {
        if (StaticVar.CurrentMenu != null)
        {
            if (menuList.Contains(StaticVar.CurrentMenu))
            {
                int n = menuList.IndexOf(StaticVar.CurrentMenu);
                transform.Find("PropertyMessage/Name").GetComponent<Text>().text = MessageSend.instance.CurrentHaveSysPro[n].PropertyName;
                transform.Find("PropertyMessage/Num/Text").GetComponent<Text>().text = MessageSend.instance.CurrentHaveSysPro[n].HaveNum.ToString();
                transform.Find("PropertyMessage/Message/Text").GetComponent<Text>().text = MessageSend.instance.CurrentHaveSysPro[n].Intro;
                transform.Find("PropertyMessage/Icon").GetComponent<Image>().sprite = LoadTexture.getInstance().LoadAtlasSprite("Atlas/UI", MessageSend.instance.CurrentHaveSysPro[n].PropertyName);

            }

        }

    }
}