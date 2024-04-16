﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdjustCommodityWnd : BaseWnd
{
    public void Initialize()
    {
        StaticVar.CurrentMenu = _transform.Find("CurrentList/Viewport/Content/Button1");
        _transform.gameObject.AddComponent<AdjustCommodityWndControl>();

    }
}

public class AdjustCommodityWndControl : MonoBehaviour
{
    //按钮数组
    //当前拥有的全部周边
    private ArrayList _haveCommodity = new ArrayList();
    //当天放置的周边商品
    private ArrayList _currentDayCommodity = new ArrayList();
    //删除选项
    private ArrayList _delete = new ArrayList();
    //当天选择的周边商品和位置对应
    private Dictionary<string, CommodityCfg> _commodityToPosition = new Dictionary<string, CommodityCfg>();

    private void Start()
    {
        foreach (var item in transform.Find("CurrentList/Viewport/Content").GetComponentsInChildren<ButtonStateAdjust>())
        {
            if (!_currentDayCommodity.Contains(item.transform))
            {
                _currentDayCommodity.Add(item.transform);
            }
        }
        foreach (var item in transform.Find("HaveList/Viewport/Content").GetComponentsInChildren<ButtonStateAdjust>())
        {
            if (!_haveCommodity.Contains(item.transform))
            {
                _haveCommodity.Add(item.transform);
            }
        }
        foreach (var item in transform.Find("Delete").GetComponentsInChildren<ButtonStateAdjust>())
        {
            if (!_delete.Contains(item.transform))
            {
                _delete.Add(item.transform);
            }
        }
        IniCurrentDayCom();
    }

    private void Update()
    {
        //当前选择在当天售卖周边界面
        if (_currentDayCommodity.Contains(StaticVar.CurrentMenu))
        {
            StaticVar.InputControl1(_currentDayCommodity);
            RefreshCurrentDayCom();
            //周边物品信息刷新
            RefreshComInfor();
            //确认选中
            if (Input.GetKeyDown(KeyCode.Z))
            {
                //改变该选择按钮的状态
                StaticVar.CurrentMenu.GetComponent<ButtonStateAdjust>().state = ButtonState.Interaction;
                //判定此位置是否为空
                if (StaticVar.CurrentMenu.Find("Add").gameObject.activeSelf)
                {

                    //跳转到所有菜品界面
                    transform.Find("HaveList").gameObject.SetActive(true);
                    IniCurrentHaveCom();
                    StaticVar.CurrentMenu = (Transform)_haveCommodity[0];
                    transform.Find("CurrentList").gameObject.SetActive(false);
                }
                else
                {
                    //跳转到删除界面
                    transform.Find("Delete").gameObject.SetActive(true);
                    StaticVar.CurrentMenu = (Transform)_delete[0];
                }
            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
                WindowManager.instance.Close<AdjustCommodityWnd>();
                WindowManager.instance.Open<StartPreparationWnd>().Initialize();
            }
        }
        //当前选择在当前拥有周边界面
        else if (_haveCommodity.Contains(StaticVar.CurrentMenu))
        {
           
            CurrentHaveComCon();
            //刷新物品信息
            RefreshComInfor();
           
        }
        //当前选择在删除界面
        else if (_delete.Contains(StaticVar.CurrentMenu))
        {
            StaticVar.InputControl2(_delete);
            
            if (Input.GetKeyDown(KeyCode.Z))
            {
                foreach (var item in _currentDayCommodity)
                {
                    if (((Transform)item).GetComponent<ButtonStateAdjust>().state == ButtonState.Interaction)
                    {
                        string key = ((Transform)item).name; ;
                        if (StaticVar.CurrentMenu == (Transform)_delete[0])
                        {
                            //切界面
                            transform.Find("HaveList").gameObject.SetActive(true);
                            transform.Find("CurrentList").gameObject.SetActive(false);
                            IniCurrentHaveCom();
                            StaticVar.CurrentMenu = (Transform)_haveCommodity[0];
                        }
                        else
                        {
                            //刷新当天菜单
                            RefreshCurrentDayCom();
                            StaticVar.CurrentMenu = (Transform)item;
                            StaticVar.CurrentMenu.GetComponent<ButtonStateAdjust>().state = ButtonState.Select;
                        }
                        transform.Find("Delete").gameObject.SetActive(false);
                    }
                }
            }
        }
    }
    //当天周边商品的初始化
    public void IniCurrentDayCom()
    {
        foreach (var item in MessageSend.instance.CurrentDayCom)
        {
            _commodityToPosition.Add(((Transform)_currentDayCommodity[MessageSend.instance.CurrentDayCom.IndexOf(item)]).name, item);
        }
    }
    //初始化当前拥有商品界面
    public void IniCurrentHaveCom()
    {
        //临时列表接收挑选剩下的商品
        List<CommodityCfg> leftCommodity = new List<CommodityCfg>();
        ArrayList tempArr = new ArrayList();
        foreach (var item in MessageSend.instance.CurrentHaveCom)
        {
            if (!MessageSend.instance.CurrentDayCom.Contains(item))
            {
                leftCommodity.Add(item);
                tempArr.Add(item);
            }
        }
        //判定滑动轴
        if (leftCommodity.Count>5)
        {
            Transform slider = transform.Find("HaveList/Slider");
            slider.gameObject.SetActive(true);
            slider.GetComponent<MyScrollBar>().ScrollList = tempArr;
            slider.GetComponent<MyScrollBar>().Initialize();
        }

        //内容赋值
        for (int i = 0; i < leftCommodity.Count; i++)
        {
            if (i < 5)
            {
                Transform temp = transform.Find("HaveList/Viewport/Content/Button" + (i + 1) + "/Have");
                temp.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/UI切图/ChangeWaiter/角色像/头像/" + leftCommodity[i].Name);
                temp.Find("Name").GetComponent<Text>().text = leftCommodity[i].Name;
                temp.Find("Price").GetComponent<Text>().text = "$" + leftCommodity[i].Price.ToString();
                temp.Find("Num").GetComponent<Text>().text = leftCommodity[i].Num.ToString();
                temp.Find("ID").GetComponent<Text>().text = leftCommodity[i].ID.ToString();
               
            }
        }
    }

    //刷新当天售卖商品界面
    public void RefreshCurrentDayCom()
    {
     
        foreach (var item in _currentDayCommodity)
        {
            if (_commodityToPosition.ContainsKey(((Transform)item).name))
            {
                Transform menu = ((Transform)item).Find("Have");
               
                menu.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/UI切图/ChangeWaiter/角色像/头像/" + _commodityToPosition[((Transform)item).name].Name);
                menu.Find("Name").GetComponent<Text>().text = _commodityToPosition[((Transform)item).name].Name;
                menu.Find("Price").GetComponent<Text>().text = "$" + _commodityToPosition[((Transform)item).name].Price.ToString();
                menu.Find("Num").GetComponent<Text>().text = _commodityToPosition[((Transform)item).name].Num.ToString();
                menu.gameObject.SetActive(true);
                ((Transform)item).Find("Add").gameObject.SetActive(false);
            }
           
            else
            {
                ((Transform)item).Find("Have").gameObject.SetActive(false);
                ((Transform)item).Find("Add").gameObject.SetActive(true);
            }
        }
    }
    

    //控制当前拥有的商品界面
    public void CurrentHaveComCon()
    {
        //临时列表接收挑选剩下的商品
        List<CommodityCfg> leftCommodity = new List<CommodityCfg>();
        foreach (var item in MessageSend.instance.CurrentHaveCom)
        {
            if (!MessageSend.instance.CurrentDayCom.Contains(item))
            {
                leftCommodity.Add(item);
            }
        }
        int n = _haveCommodity.IndexOf(StaticVar.CurrentMenu);
        CommodityCfg tempcfg = MessageSend.instance.commodityCfg[int.Parse(StaticVar.CurrentMenu.Find("Have/ID").GetComponent<Text>().text)];
        int m = leftCommodity.IndexOf(tempcfg);
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (n > 0)
            {
                StaticVar.CurrentMenu = (Transform)_haveCommodity[n - 1];
            }
            else
            {
                
                if (m>0)
                {
                    for (int i = 0; i < _haveCommodity.Count; i++)
                    {
                        Transform btn = ((Transform)_haveCommodity[i]).Find("Have");
                        btn.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/UI切图/ChangeWaiter/角色像/头像/" + leftCommodity[m+i-1].Name);
                        btn.Find("Name").GetComponent<Text>().text = leftCommodity[m+i-1].Name;
                        btn.Find("Price").GetComponent<Text>().text = "$" + leftCommodity[m + i - 1].Price.ToString();
                        btn.Find("Num").GetComponent<Text>().text = leftCommodity[m + i - 1].Num.ToString();
                        btn.Find("ID").GetComponent<Text>().text = leftCommodity[m + i - 1].ID.ToString();
                        btn.parent.gameObject.SetActive(true);
                    }
                }
            }

        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (n<4)
            {
                if (leftCommodity.Count < _haveCommodity.Count)
                {
                    if (n< leftCommodity.Count-1)
                    {
                        StaticVar.CurrentMenu = (Transform)_haveCommodity[n + 1];
                    }
                }
                else
                {
                    StaticVar.CurrentMenu = (Transform)_haveCommodity[n + 1];
                }
            }
            else
            {
                if (m < leftCommodity.Count-1)
                {
                    for (int i = 0; i < _haveCommodity.Count; i++)
                    {
                        Transform btn = ((Transform)_haveCommodity[i]).Find("Have");
                        btn.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/UI切图/ChangeWaiter/角色像/头像/" + leftCommodity[m + i -3].Name);
                        btn.Find("Name").GetComponent<Text>().text = leftCommodity[m + i -3].Name;
                        btn.Find("Price").GetComponent<Text>().text = "$" + leftCommodity[m + i -3].Price.ToString();
                        btn.Find("Num").GetComponent<Text>().text = leftCommodity[m + i -3].Num.ToString();
                        btn.Find("ID").GetComponent<Text>().text = leftCommodity[m + i - 3].ID.ToString();
                        btn.parent.gameObject.SetActive(true);
                    }
                }
            }
        }


        if (Input.GetKeyDown(KeyCode.Z))
        {
            CommodityCfg cfg = new CommodityCfg();
            foreach (var item in MessageSend.instance.CurrentHaveCom)
            {
                if (item.Name == StaticVar.CurrentMenu.Find("Have/Name").GetComponent<Text>().text)
                {
                    cfg = item;
                }
            }
            transform.Find("CurrentList").gameObject.SetActive(true);
            foreach (var item in _currentDayCommodity)
            {
                if (((Transform)item).GetComponent<ButtonStateAdjust>().state == ButtonState.Interaction)
                {
                    StaticVar.CurrentMenu = (Transform)item;
                    StaticVar.CurrentMenu.GetComponent<ButtonStateAdjust>().state = ButtonState.Select;
                    //更换菜品
                    if (_commodityToPosition.ContainsKey(StaticVar.CurrentMenu.name))
                    {
                        int index = MessageSend.instance.CurrentDayCom.IndexOf(_commodityToPosition[StaticVar.CurrentMenu.name]);
                        MessageSend.instance.CurrentDayCom[index] = cfg;
                        _commodityToPosition[StaticVar.CurrentMenu.name] = cfg;
                    }
                    //添加菜品
                    else
                    {
                        MessageSend.instance.CurrentDayCom.Add(cfg);
                        //对应菜品和位置的保存
                        _commodityToPosition.Add(StaticVar.CurrentMenu.name, MessageSend.instance.CurrentDayCom[MessageSend.instance.CurrentDayCom.Count - 1]);
                    }
                }
            }


            //刷新
            RefreshCurrentDayCom();
            RefreshComInfor();
            transform.Find("HaveList").gameObject.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            transform.Find("CurrentList").gameObject.SetActive(true);
            foreach (var item in _currentDayCommodity)
            {
                if (((Transform)item).GetComponent<ButtonStateAdjust>().state == ButtonState.Interaction)
                {
                    ((Transform)item).GetComponent<ButtonStateAdjust>().state = ButtonState.Select;
                    StaticVar.CurrentMenu = (Transform)item;
                }
            }
            transform.Find("HaveList").gameObject.SetActive(false);
        }
    }
    //刷新周边商品信息界面 
    public void RefreshComInfor()
    {
        Transform infor = transform.Find("Infor");
        //当前选择在当天售卖商品区域
        if (_currentDayCommodity.Contains(StaticVar.CurrentMenu))
        {
            //判定该位置是否为空
            if (_commodityToPosition.ContainsKey(StaticVar.CurrentMenu.name))
            {
                string key = StaticVar.CurrentMenu.name;
                infor.Find("Name").GetComponent<Text>().text = _commodityToPosition[key].Name;
                infor.Find("Image").GetComponent<Image>().sprite = StaticVar.CurrentMenu.Find("Have/Image").GetComponent<Image>().sprite;
                infor.Find("Introduce/Text").GetComponent<Text>().text = _commodityToPosition[key].Introduce;
                infor.gameObject.SetActive(true);
            }
        
            //该位置为空
            else
            {
                infor.gameObject.SetActive(false);
            }
        }
        //当前选择在当前拥有商品区域
        else if (_haveCommodity.Contains(StaticVar.CurrentMenu))
        {
            foreach (var item in MessageSend.instance.CurrentHaveCom)
            {
                if (StaticVar.CurrentMenu.Find("Have/Name").GetComponent<Text>().text == item.Name)
                {
                    infor.Find("Name").GetComponent<Text>().text = item.Name;
                    infor.Find("Image").GetComponent<Image>().sprite = StaticVar.CurrentMenu.Find("Have/Image").GetComponent<Image>().sprite;
                    infor.Find("Introduce/Text").GetComponent<Text>().text = item.Introduce;
                    infor.gameObject.SetActive(true);
                }
            }

        }

    }
}
