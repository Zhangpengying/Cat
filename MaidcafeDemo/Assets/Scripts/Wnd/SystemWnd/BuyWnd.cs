﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyWnd : BaseWnd
{
    public void Initialize()
    {
        _transform.gameObject.AddComponent<BuyWndCon>();
    }
	
}

public class BuyWndCon : MonoBehaviour
{
    public ArrayList menuList = new ArrayList();
    public ArrayList buyNums = new ArrayList();
    public ArrayList makesureBtns = new ArrayList();
    private void Start()
    {
        foreach (var item in transform.Find("Scroll View/Viewport/Content").GetComponentsInChildren<ButtonStateAdjust>())
        {
            if (!menuList.Contains(item.transform))
            {
                menuList.Add(item.transform);
                
            }
        }

        foreach (var item in transform.Find("BuyNum").GetComponentsInChildren<ButtonStateAdjust>())
        {
            if (!buyNums.Contains(item.transform))
            {
                buyNums.Add(item.transform);
            }
        }

        foreach (var item in transform.Find("MakeSure").GetComponentsInChildren<ButtonStateAdjust>())
        {
            if (!makesureBtns.Contains(item.transform))
            {
                makesureBtns.Add(item.transform);
            }
        }
        StaticVar.CurrentMenu = menuList[0] as Transform;
        
    }

    private void Update()
    {
        if (menuList.Contains(StaticVar.CurrentMenu))
        {
            StaticVar.InputControl1(menuList);
            //刷新物品列表
            //RefreshList();
            if (Input.GetKeyDown(KeyCode.Z))
            {
                StaticVar.CurrentMenu.GetComponent<ButtonStateAdjust>().state = ButtonState.Interaction;
                //跳转到下一级
                StaticVar.CurrentMenu = buyNums[0] as Transform;
            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
                //结束互动，返回场景
                WindowManager.instance.Close<BuyWnd>();
                StaticVar.EndInteraction();
            }
        }
        else if (buyNums.Contains(StaticVar.CurrentMenu))
        {
            StaticVar.InputControl2(buyNums);
            int n = int.Parse(StaticVar.CurrentMenu.Find("Text").GetComponent<Text>().text);
            //数字调整
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (n <9 )
                {
                    n += 1;
                }
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (n > 0)
                {
                    n -= 1;
                }
            }
            StaticVar.CurrentMenu.Find("Text").GetComponent<Text>().text = n.ToString();
            if (Input.GetKeyDown(KeyCode.Z))
            {
                StaticVar.CurrentMenu = makesureBtns[0] as Transform;
            }
            else if(Input.GetKeyDown(KeyCode.X))
            {
                foreach (var item in menuList)
                {
                    if (((Transform)item).GetComponent<ButtonStateAdjust>().state == ButtonState.Interaction)
                    {
                        StaticVar.CurrentMenu = item as Transform;
                        StaticVar.CurrentMenu.GetComponent<ButtonStateAdjust>().state = ButtonState.Select;
                    }
                }
            }
        }
        else if (makesureBtns.Contains(StaticVar.CurrentMenu))
        {
            StaticVar.CurrentMenu.parent.gameObject.SetActive(true);
            StaticVar.InputControl1(makesureBtns);
            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (StaticVar.CurrentMenu == makesureBtns[0] as Transform)
                {
                    StaticVar.CurrentMenu = null;
                    WindowManager.instance.Close<BuyWnd>();
                    StaticVar.EndInteraction();
                }
                else if (StaticVar.CurrentMenu == makesureBtns[1] as Transform)
                {
                    StaticVar.CurrentMenu = buyNums[0] as Transform;
                }
                transform.Find("MakeSure").gameObject.SetActive(false);
            }
        }
    }

    //刷新列表
    public void RefreshList()
    {
        foreach (var item in MessageSend.instance.CurrentHavePropertys)
        {
            int n = MessageSend.instance.CurrentHavePropertys.IndexOf(item);
            Transform temp = ((Transform)menuList[n]);
            temp.Find("Name").GetComponent<Text>().text = item.PropertyName;
            temp.Find("Icon").GetComponent<Image>().sprite = LoadTexture.getInstance().LoadAtlasSprite("", item.PropertyName);
            temp.Find("Price").GetComponent<Text>().text = item.Price.ToString();
        }
    }
    //刷新物品信息
    public void RefreshInfor()
    {
        int n = 0;
        foreach (var item in menuList)
        {
            if (((Transform)item).GetComponent<ButtonStateAdjust>().state == ButtonState.Interaction)
            {
                n = menuList.IndexOf(item);
            }
        }
        
    }
}
