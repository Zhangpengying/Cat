﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeWaiterWnd : BaseWnd
{
    public void Initialize()
    {
        StaticVar.CurrentMenu = _transform.Find("CurrWaiterList/Viewport/Content/Button1");
        _transform.gameObject.AddComponent<ChangeWaiterWndControl>();
    }
}

public class ChangeWaiterWndControl : MonoBehaviour
{
    //按钮数组
    //当前拥有的全部女仆
    public ArrayList _haveWaiters = new ArrayList();
    //当天安排的女仆
    public ArrayList _currentDayWaiters = new ArrayList();
    //删除选项
    private ArrayList _delete = new ArrayList();

    //当天选择的女仆和位置对应
    private Dictionary<string, WaiterCfg> _waiterToPosition = new Dictionary<string, WaiterCfg>();

    private void Start()
    {
        //按钮列表赋值
        foreach (var item in transform.Find("CurrWaiterList/Viewport/Content").GetComponentsInChildren<ButtonStateAdjust>())
        {
            if (!_currentDayWaiters.Contains(item.transform))
            {
                _currentDayWaiters.Add(item.transform);
            }
        }
        foreach (var item in transform.Find("Delete").GetComponentsInChildren<ButtonStateAdjust>())
        {
            if (!_delete.Contains(item.transform))
            {
                _delete.Add(item.transform);
            }
        }
        foreach (var item in transform.Find("HaveWaiterList/Viewport/Content").GetComponentsInChildren<ButtonStateAdjust>())
        {
            if (!_haveWaiters.Contains(item.transform))
            {
                _haveWaiters.Add(item.transform);
            }
        }
        IniCurrentDayWaiter();
    }
    private void Update()
    {
        //当前选择在当天排班女仆区域
        if (_currentDayWaiters.Contains(StaticVar.CurrentMenu))
        {
            StaticVar.InputControl1(_currentDayWaiters);
            //按钮状态刷新
            RefreshBtn1(_currentDayWaiters);

            //女仆信息的刷新
            RefreshWaiterInfor();

            //当天排班女仆区域的刷新
            RefreshCurrentDayWaiter();
            //确认选中
            if (Input.GetKeyDown(KeyCode.Z))
            {
                StaticVar.CurrentMenu.GetComponent<ButtonStateAdjust>().state = ButtonState.Interaction;
                //判定此位置是否为空
                if (StaticVar.CurrentMenu.Find("Add").gameObject.activeSelf)
                {
                    //跳转到所有女仆界面
                    transform.Find("HaveWaiterList").gameObject.SetActive(true);
                    RefreshHaveWaiter();

                    transform.Find("CurrWaiterList").gameObject.SetActive(false);
                }
                else
                {
                    //跳转到删除界面
                    transform.Find("Delete").gameObject.SetActive(true);
                    StaticVar.CurrentMenu = (Transform)_delete[0];
                }
            }
            //取消，返回上一级
            else if (Input.GetKeyDown(KeyCode.X))
            {
                WindowManager.instance.Close<ChangeWaiterWnd>();
                WindowManager.instance.Open<StartPreparationWnd>().Initialize();
            }
        }
        //当前选择在当前拥有的女仆区域
        else if (_haveWaiters.Contains(StaticVar.CurrentMenu))
        {
            StaticVar.InputControl1(_haveWaiters);
            //按钮状态刷新
            RefreshBtn2(_haveWaiters);
            //刷新女仆信息
            RefreshWaiterInfor();
         
            //确定添加并返回上一级
            if (Input.GetKeyDown(KeyCode.Z))
            {
                WaiterCfg cfg = new WaiterCfg();
                foreach (var item in MessageSend.instance.currenthavewaiters)
                {
                    if (item.Name == StaticVar.CurrentMenu.Find("Have/Name").GetComponent<Text>().text)
                    {
                        cfg = item;
                    }
                }
                transform.Find("CurrWaiterList").gameObject.SetActive(true);
                foreach (var item in _currentDayWaiters)
                {
                    if (((Transform)item).GetComponent<ButtonStateAdjust>().state == ButtonState.Interaction)
                    {
                        StaticVar.CurrentMenu = (Transform)item;
                        StaticVar.CurrentMenu.GetComponent<ButtonStateAdjust>().state = ButtonState.Select;
                        //更换女仆
                        if (_waiterToPosition.ContainsKey(StaticVar.CurrentMenu.name))
                        {
                            int index = MessageSend.instance.currentDayWaiters.IndexOf(_waiterToPosition[StaticVar.CurrentMenu.name]);
                            MessageSend.instance.currentDayWaiters[index] = cfg;
                            _waiterToPosition[StaticVar.CurrentMenu.name] = cfg;
                        }
                        //添加女仆
                        else
                        {
                            MessageSend.instance.currentDayWaiters.Add(cfg);
                            //对应女仆和位置的保存
                            _waiterToPosition.Add(StaticVar.CurrentMenu.name, MessageSend.instance.currentDayWaiters[MessageSend.instance.currentDayWaiters.Count - 1]);
                        }
                        
                    }
                }
                //刷新
                RefreshCurrentDayWaiter();
                RefreshWaiterInfor();
                transform.Find("HaveWaiterList").gameObject.SetActive(false);
            }
            //撤销返回上一级
            else if (Input.GetKeyDown(KeyCode.X))
            {
                transform.Find("CurrWaiterList").gameObject.SetActive(true);
                foreach (var item in _currentDayWaiters)
                {
                    if (((Transform)item).GetComponent<ButtonStateAdjust>().state == ButtonState.Interaction)
                    {
                        ((Transform)item).GetComponent<ButtonStateAdjust>().state = ButtonState.Select;
                        StaticVar.CurrentMenu = (Transform)item;
                    }
                }
                transform.Find("HaveWaiterList").gameObject.SetActive(false);
            }
        }
        //当前选择在删除区域
        else if (_delete.Contains(StaticVar.CurrentMenu))
        {
            StaticVar.InputControl2(_delete);
            
            if (Input.GetKeyDown(KeyCode.Z))
            {
                foreach (var item in _currentDayWaiters)
                {
                    if (((Transform)item).GetComponent<ButtonStateAdjust>().state == ButtonState.Interaction)
                    {
                        string key = ((Transform)item).name;
                        if (StaticVar.CurrentMenu == (Transform)_delete[0])
                        {
                            //切界面
                            transform.Find("HaveWaiterList").gameObject.SetActive(true);
                            RefreshHaveWaiter();
                            transform.Find("CurrWaiterList").gameObject.SetActive(false);
                        }
                        else
                        {
                            //刷新当天菜单
                            RefreshCurrentDayWaiter();
                            StaticVar.CurrentMenu = (Transform)item;
                            StaticVar.CurrentMenu.GetComponent<ButtonStateAdjust>().state = ButtonState.Select;
                        }
                        transform.Find("Delete").gameObject.SetActive(false);
                    }
                }
            }
        }
        //当前选择为背景
        else if (StaticVar.CurrentMenu == transform.Find("HaveWaiterList/Bc"))
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                //切界面
                transform.Find("CurrWaiterList").gameObject.SetActive(true);
                foreach (var item in _currentDayWaiters)
                {
                    if (((Transform)item).GetComponent<ButtonStateAdjust>().state == ButtonState.Interaction)
                    {
                        ((Transform)item).GetComponent<ButtonStateAdjust>().state = ButtonState.Select;
                        StaticVar.CurrentMenu = (Transform)item;
                    }
                }
                transform.Find("HaveWaiterList").gameObject.SetActive(false);
            }
        }
    }

    //当天值班女仆的初始化
    public void IniCurrentDayWaiter()
    {
        foreach (var item in MessageSend.instance.currentDayWaiters)
        {
            _waiterToPosition.Add(((Transform)_currentDayWaiters[MessageSend.instance.currentDayWaiters.IndexOf(item)]).name, item);
        }
    }
    //刷新当天排班女仆
    public void RefreshCurrentDayWaiter()
    {
        //CurrentDayWaitersIO.instance.Read();
        foreach (var item in _currentDayWaiters)
        {
            if (_waiterToPosition.ContainsKey(((Transform)item).name))
            {
                Transform menu = ((Transform)item).Find("Have");
                menu.Find("HeadProtrait").GetComponent<Image>().sprite = LoadTexture.getInstance().LoadAtlasSprite("Atlas/UI/PropertyIcon/Characters/Head/Head", _waiterToPosition[((Transform)item).name].Name);
                menu.Find("Name").GetComponent<Text>().text = _waiterToPosition[((Transform)item).name].Name;
                menu.Find("State").GetComponent<Image>().sprite = LoadTexture.getInstance().LoadAtlasSprite("Atlas/UI/FunctionIcon/FunctionIcon", _waiterToPosition[((Transform)item).name].Health.ToString());
                menu.gameObject.SetActive(true);
                ((Transform)item).Find("Add").gameObject.SetActive(false);
            }
            else
            {
                ((Transform)item).Find("Have").gameObject.SetActive(false);
                ((Transform)item).Find("Add").gameObject.SetActive(true);
            }
        }
        RefreshBtn1(_currentDayWaiters);
    }
    //刷新当前拥有的女仆
    public void RefreshHaveWaiter()
    {
        transform.Find("HaveWaiterList/Slider").GetComponent<MyScrollBar>().ScrollList = _haveWaiters;
        transform.Find("HaveWaiterList/Slider").GetComponent<MyScrollBar>().Content = transform.Find("HaveWaiterList/Viewport/Content");
        //先清空
        _haveWaiters.Clear();
        //动态激活按钮
        Transform _waitercontent = transform.Find("HaveWaiterList/Viewport/Content");
      
        //先清空
        foreach (var item in _waitercontent.GetComponentsInChildren<ButtonStateAdjust>())
        {
            item.gameObject.SetActive(false);
        }
        //临时列表接收挑选剩下的女仆
        List<WaiterCfg> leftWaiter = new List<WaiterCfg>();

        //修正列表元素不统一问题
        for (int i = 0; i < MessageSend.instance.currentDayWaiters.Count; i++)
        {
            if (MessageSend.instance.waiterCfgs.ContainsKey(MessageSend.instance.currentDayWaiters[i].ID))
            {
                MessageSend.instance.currentDayWaiters[i] = MessageSend.instance.waiterCfgs[MessageSend.instance.currentDayWaiters[i].ID];
            }
        }
        for (int i = 0; i < MessageSend.instance.currenthavewaiters.Count; i++)
        {
            if (MessageSend.instance.waiterCfgs.ContainsKey(MessageSend.instance.currenthavewaiters[i].ID))
            {
                MessageSend.instance.currenthavewaiters[i] = MessageSend.instance.waiterCfgs[MessageSend.instance.currenthavewaiters[i].ID];
            }
        }

        foreach (var item in MessageSend.instance.currenthavewaiters)
        {
            if (!MessageSend.instance.currentDayWaiters.Contains(item))
            {
                leftWaiter.Add(item);
            }
        }

        if (leftWaiter.Count == 0)
        {
            transform.Find("HaveWaiterList/Bc").gameObject.SetActive(true);
            StaticVar.CurrentMenu = transform.Find("HaveWaiterList/Bc");
        }
        else
        {
            for (int i = 0; i < leftWaiter.Count; i++)
            {
                Transform temp = transform.Find("HaveWaiterList/Viewport/Content/Button" + (i + 1) + "/Have");
                
                temp.Find("HeadProtrait").GetComponent<Image>().sprite = LoadTexture.getInstance().LoadAtlasSprite("Atlas/UI/PropertyIcon/Characters/Head/Head", leftWaiter[i].Name);
                temp.Find("Name").GetComponent<Text>().text = leftWaiter[i].Name;
                temp.Find("State").GetComponent<Image>().sprite = LoadTexture.getInstance().LoadAtlasSprite("Atlas/UI/FunctionIcon/FunctionIcon", leftWaiter[i].Health.ToString());
                temp.parent.gameObject.SetActive(true);
                _haveWaiters.Add(temp.parent);

            }
            StaticVar.CurrentMenu = _waitercontent.Find("Button1");
        }
        
        
        
    }
    //刷新女仆信息
    public void RefreshWaiterInfor()
    {
        Transform infor = transform.Find("Infor");
        //当前选择在当天排班女仆区域
        if (_currentDayWaiters.Contains(StaticVar.CurrentMenu))
        {
            //判定该位置是否为空
            if (_waiterToPosition.ContainsKey(StaticVar.CurrentMenu.name))
            {
                infor.Find("Name").GetComponent<Text>().text = _waiterToPosition[StaticVar.CurrentMenu.name].Name;
                infor.Find("Image").GetComponent<Image>().sprite = StaticVar.CurrentMenu.Find("Have/HeadProtrait").GetComponent<Image>().sprite;
                infor.Find("Birthday").GetComponent<Text>().text = "生日：" + _waiterToPosition[StaticVar.CurrentMenu.name].BirthDay;
                infor.Find("Weight").GetComponent<Text>().text = "体重：" + _waiterToPosition[StaticVar.CurrentMenu.name].Weight.ToString() + "kg";
                infor.Find("Height").GetComponent<Text>().text = "身高：" + _waiterToPosition[StaticVar.CurrentMenu.name].Height.ToString() + "cm";
                infor.Find("BloodType").GetComponent<Text>().text = "血型：" + _waiterToPosition[StaticVar.CurrentMenu.name].BloodType;
                int n = _waiterToPosition[StaticVar.CurrentMenu.name].Level;
                for (int i = 1; i < 6; i++)
                {
                    if (i <= n)
                    {
                        infor.Find("Quality/Star" + i).GetComponent<StarState>().highQuality = true;
                    }
                    else
                    {
                        infor.Find("Quality/Star" + i).GetComponent<StarState>().highQuality = false;
                    }

                }
                infor.Find("Introduce/Text").GetComponent<Text>().text = _waiterToPosition[StaticVar.CurrentMenu.name].Introduce;
                infor.gameObject.SetActive(true);
            }
            //该位置为空
            else
            {
                infor.gameObject.SetActive(false);
            }
        }

     
        //当前选择在当前拥有菜品区域
        else if (_haveWaiters.Contains(StaticVar.CurrentMenu))
        {
            foreach (var item in MessageSend.instance.currenthavewaiters)
            {
                if (StaticVar.CurrentMenu.Find("Have/Name").GetComponent<Text>().text == item.Name)
                {
                    infor.Find("Name").GetComponent<Text>().text = item.Name;
                    infor.Find("Image").GetComponent<Image>().sprite = StaticVar.CurrentMenu.Find("Have/HeadProtrait").GetComponent<Image>().sprite;
                    infor.Find("Birthday").GetComponent<Text>().text = "生日：" + item.BirthDay;
                    infor.Find("Weight").GetComponent<Text>().text = "体重：" + item.Weight.ToString() + "kg";
                    infor.Find("Height").GetComponent<Text>().text = "身高：" + item.Height.ToString() + "cm";
                    infor.Find("BloodType").GetComponent<Text>().text = "血型：" + item.BloodType;
                    int n = item.Level;
                    for (int i = 1; i < 6; i++)
                    {
                        if (i <= n)
                        {
                            infor.Find("Quality/Star" + i).GetComponent<StarState>().highQuality = true;
                        }
                        else
                        {
                            infor.Find("Quality/Star" + i).GetComponent<StarState>().highQuality = false;
                        }

                    }
                    infor.Find("Introduce/Text").GetComponent<Text>().text = item.Introduce;
                    infor.gameObject.SetActive(true);
                }
            }

        }
    }
    //当天排班女仆界面按钮状态的刷新
    public void RefreshBtn1(ArrayList BtnList)
    {
        foreach (var item in BtnList)
        {
            Transform temp = (Transform)item;
            if (temp == StaticVar.CurrentMenu)
            {
                //判定此位置是否为空
                if (StaticVar.CurrentMenu.Find("Add").gameObject.activeSelf)
                {
                    StaticVar.CurrentMenu.Find("Add").GetComponent<ImageStateAdjust>().state = ButtonState.Select;
                    StaticVar.CurrentMenu.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                }
                else
                {
                    StaticVar.CurrentMenu.GetComponent<Image>().color = new Color(1, 1, 1, 0);
                    StaticVar.CurrentMenu.Find("Have/HeadProtrait").GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    StaticVar.CurrentMenu.Find("Have/Image").GetComponent<ImageStateAdjust>().state = ButtonState.Select;
                    StaticVar.CurrentMenu.Find("Have/State").GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    StaticVar.CurrentMenu.Find("Have/Name").GetComponent<Text>().color = new Color(0, 0, 0, 1);
                }
            }
            else
            {
                //判定此位置是否为空
                if (temp.Find("Add").gameObject.activeSelf)
                {
                    temp.Find("Add").GetComponent<ImageStateAdjust>().state = ButtonState.Normal;
                    temp.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                }
                else
                {
                    temp.GetComponent<Image>().color = new Color(1, 1, 1, 0);
                    temp.Find("Have/HeadProtrait").GetComponent<Image>().color = new Color(1, 1, 1, 0.4f);
                    temp.Find("Have/Image").GetComponent<ImageStateAdjust>().state = ButtonState.Normal;
                    temp.Find("Have/State").GetComponent<Image>().color = new Color(1, 1, 1, 0.4f);
                    temp.Find("Have/Name").GetComponent<Text>().color = new Color(0, 0, 0, 0.4f);
                }
            }
        }
        
    }
    //当前拥有女仆界面按钮状态的刷新
    public void RefreshBtn2(ArrayList BtnList)
    {
        foreach (var item in BtnList)
        {
            Transform temp = (Transform)item;
            if (temp == StaticVar.CurrentMenu)
            {
                StaticVar.CurrentMenu.GetComponent<Image>().color = new Color(1, 1, 1, 0);
                StaticVar.CurrentMenu.Find("Have/HeadProtrait").GetComponent<Image>().color = new Color(1, 1, 1, 1);
                StaticVar.CurrentMenu.Find("Have/Image").GetComponent<ImageStateAdjust>().state = ButtonState.Select;
                StaticVar.CurrentMenu.Find("Have/State").GetComponent<Image>().color = new Color(1, 1, 1, 1);
                StaticVar.CurrentMenu.Find("Have/Name").GetComponent<Text>().color = new Color(0, 0, 0, 1);
            }
            else
            {
                temp.GetComponent<Image>().color = new Color(1, 1, 1, 0);
                temp.Find("Have/HeadProtrait").GetComponent<Image>().color = new Color(1, 1, 1, 0.4f);
                temp.Find("Have/Image").GetComponent<ImageStateAdjust>().state = ButtonState.Normal;
                temp.Find("Have/State").GetComponent<Image>().color = new Color(1, 1, 1, 0.4f);
                temp.Find("Have/Name").GetComponent<Text>().color = new Color(0, 0, 0, 0.4f);
            }
        }

    }
}
