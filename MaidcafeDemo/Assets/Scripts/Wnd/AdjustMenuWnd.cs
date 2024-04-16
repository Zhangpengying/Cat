﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AdjustMenuWnd : BaseWnd
{
    public void Initialize()
    {
        StaticVar.CurrentMenu = _transform.Find("Classify/Food");
       
       _transform.gameObject.AddComponent<AdjustMenuWndControl>();
       
    }
}
public class AdjustMenuWndControl :MonoBehaviour
{
    //按钮数组
    //当前拥有的全部菜品按钮
    public ArrayList _haveMenus = new ArrayList();
    //当天安排的菜谱按钮
    public ArrayList _currentDayMenus = new ArrayList();
    //菜品类型按钮
    private ArrayList _classify = new ArrayList();
    //删除选项按钮
    private ArrayList _delete = new ArrayList();

    //菜品分类
    private List<MenuCfg> _currentdayfood = new List<MenuCfg>();
    private List<MenuCfg> _currentdaydrink = new List<MenuCfg>();

    //当天售卖食物和位置的对应关系
    private Dictionary<string, MenuCfg> _foodToPosition = new Dictionary<string, MenuCfg>();
    //当天售卖的饮料和位置的对应关系
    private Dictionary<string, MenuCfg> _drinkToPosition = new Dictionary<string, MenuCfg>();

    private void Start()
    {
        //按钮列表
        foreach (var item in transform.Find("Classify").GetComponentsInChildren<ButtonStateAdjust>())
        {
            if (!_classify.Contains(item.transform))
            {
                _classify.Add(item.transform);
            }
        }
        foreach (var item in transform.Find("CurrentDayMenus/Scroll View/Viewport/Content").GetComponentsInChildren<ButtonStateAdjust>())
        {
            if (!_currentDayMenus.Contains(item.transform))
            {
                _currentDayMenus.Add(item.transform);
            }
        }

        foreach (var item in transform.Find("HaveMenus/Food/Viewport/Content").GetComponentsInChildren<ButtonStateAdjust>())
        {
            if (!_haveMenus.Contains(item.transform))
            {
                _haveMenus.Add(item.transform);
            }
        }

        foreach (var item in transform.Find("Delete").GetComponentsInChildren<ButtonStateAdjust>())
        {
            if (!_delete.Contains(item.transform))
            {
                _delete.Add(item.transform);
            }
        }

        //初始化当天菜谱
        IniCurrentDayMenu();
    }
    private void Update()
    {
        //当前选择在分类区域
        if (_classify.Contains(StaticVar.CurrentMenu))
        {
            StaticVar.InputControl1(_classify);
            RefreshCurrentDayMenu();
            //确认选中
            if (Input.GetKeyDown(KeyCode.Z))
            {
                //该按钮状态改变
                StaticVar.CurrentMenu.GetComponent<ButtonStateAdjust>().state = ButtonState.Interaction;
                //跳转到下一级
                StaticVar.CurrentMenu = (Transform)_currentDayMenus[0];
            }
            //取消，返回场景，结束互动
            else if (Input.GetKeyDown(KeyCode.X))
            {
                StaticVar.CurrentMenu = null;
               
                WindowManager.instance.Close<AdjustMenuWnd>();
                WindowManager.instance.Open<StartPreparationWnd>().Initialize();
            }
        }
        //当前选择在当天菜单区域 
        else if (_currentDayMenus.Contains(StaticVar.CurrentMenu))
        {
            StaticVar.InputControl1(_currentDayMenus);
            //菜品详细信息的刷新
            RefreshMenuInfo();
            //确认选中
            if (Input.GetKeyDown(KeyCode.Z))
            {
                //判定此位置是否为空
                StaticVar.CurrentMenu.GetComponent<ButtonStateAdjust>().state = ButtonState.Interaction;
                if (StaticVar.CurrentMenu.Find("Add").gameObject.activeSelf)
                {
                    
                    //跳转到所有菜品界面
                    transform.Find("HaveMenus").gameObject.SetActive(true);
                    RefreshCurrentHaveMenu();
                    StaticVar.CurrentMenu = (Transform)_haveMenus[0];
                    transform.Find("CurrentDayMenus").gameObject.SetActive(false);
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
                foreach (var item in _classify)
                {
                    if (((Transform)item).GetComponent<ButtonStateAdjust>().state == ButtonState.Interaction)
                    {
                        StaticVar.CurrentMenu = (Transform)item;
                        StaticVar.CurrentMenu.GetComponent<ButtonStateAdjust>().state = ButtonState.Select;
                    }
                }
            }
            
        }
        //当前选择在当前拥有的菜单区域
        else if(_haveMenus.Contains(StaticVar.CurrentMenu))
        {
            //刷新菜品信息
            RefreshMenuInfo();
            ArrayList tempArr = new ArrayList();
            foreach (var item in _haveMenus)
            {
                if (((Transform)item).GetComponent<CanvasGroup>().alpha == 1)
                {
                    tempArr.Add(item);
                }
            }
            StaticVar.InputControl1(tempArr);
           
            transform.Find("HaveMenus/Slider").GetComponent<MyScrollBar>().ScrollList = _haveMenus;
         
            //确定添加并返回上一级
            if (Input.GetKeyDown(KeyCode.Z))
            {
                MenuCfg cfg = new MenuCfg();
                foreach (var item in MessageSend.instance.HaveMenus)
                {
                    if (item.MenuName == StaticVar.CurrentMenu.Find("Name").GetComponent<Text>().text)
                    {
                        cfg = item;
                        
                    }
                }
                transform.Find("CurrentDayMenus").gameObject.SetActive(true);
                foreach (var item in _currentDayMenus)
                {
                    if (((Transform)item).GetComponent<ButtonStateAdjust>().state == ButtonState.Interaction)
                    {
                        StaticVar.CurrentMenu = (Transform)item;
                        StaticVar.CurrentMenu.GetComponent<ButtonStateAdjust>().state = ButtonState.Select;
                        
                        if (cfg.Type == MenuType.Food)
                        {
                            //更换菜品
                            if (_foodToPosition.ContainsKey(StaticVar.CurrentMenu.name))
                            {
                                int index = MessageSend.instance.CurrentDayMenus.IndexOf(_foodToPosition[StaticVar.CurrentMenu.name]);
                                MessageSend.instance.CurrentDayMenus[index] = cfg;
                                _foodToPosition[StaticVar.CurrentMenu.name] = cfg;
                            }
                            //添加菜品
                            else
                            {
                                MessageSend.instance.CurrentDayMenus.Add(cfg);
                                //对应菜品和位置的保存
                                _foodToPosition.Add(StaticVar.CurrentMenu.name, MessageSend.instance.CurrentDayMenus[MessageSend.instance.CurrentDayMenus.Count - 1]);
                            }
                        }
                        else
                        {
                            //更换菜品
                            if (_drinkToPosition.ContainsKey(StaticVar.CurrentMenu.name))
                            {
                                int index = MessageSend.instance.CurrentDayMenus.IndexOf(_drinkToPosition[StaticVar.CurrentMenu.name]);
                                MessageSend.instance.CurrentDayMenus[index] = cfg;
                                _drinkToPosition[StaticVar.CurrentMenu.name] = cfg;
                            }
                            //添加菜品
                            else
                            {
                                MessageSend.instance.CurrentDayMenus.Add(cfg);
                                //对应菜品和位置的保存
                                _drinkToPosition.Add(StaticVar.CurrentMenu.name, MessageSend.instance.CurrentDayMenus[MessageSend.instance.CurrentDayMenus.Count - 1]);
                            }
                        }
                    }
                }
                RefreshCurrentDayMenu();
                transform.Find("HaveMenus").gameObject.SetActive(false);
            }
            //撤销返回上一级
            else if (Input.GetKeyDown(KeyCode.X))
            {
                transform.Find("CurrentDayMenus").gameObject.SetActive(true);
                foreach (var item in _currentDayMenus)
                {
                    if (((Transform)item).GetComponent<ButtonStateAdjust>().state == ButtonState.Interaction)
                    {
                        ((Transform)item).GetComponent<ButtonStateAdjust>().state = ButtonState.Select;
                        StaticVar.CurrentMenu = (Transform)item;
                    }
                }
                transform.Find("HaveMenus").gameObject.SetActive(false);
            }

        }
        //当前选择在删除区域
        else if(_delete.Contains(StaticVar.CurrentMenu))
        {
            StaticVar.InputControl2(_delete);
            //修改字体颜色
            foreach (var item in _delete)
            {
                if (((Transform)item) == StaticVar.CurrentMenu)
                {
                    ((Transform)item).Find("Text").GetComponent<Text>().color = new Color(0, 0, 0);
                }
                else
                {
                    ((Transform)item).Find("Text").GetComponent<Text>().color = new Color(1, 1, 1);
                }
            }
            if (Input.GetKeyDown(KeyCode.Z))
            {
                foreach (var item in _currentDayMenus)
                {
                    if (((Transform)item).GetComponent<ButtonStateAdjust>().state == ButtonState.Interaction)
                    {
                        string key = ((Transform)item).name;
                        if (StaticVar.CurrentMenu == (Transform)_delete[0])
                        {
                            //切界面
                            transform.Find("HaveMenus").gameObject.SetActive(true);
                            RefreshCurrentHaveMenu();
                            transform.Find("CurrentDayMenus").gameObject.SetActive(false);
                        }
                        else
                        {
                            //刷新当天菜单
                            RefreshCurrentDayMenu();
                            StaticVar.CurrentMenu = (Transform)item;
                            StaticVar.CurrentMenu.GetComponent<ButtonStateAdjust>().state = ButtonState.Select;
                        }
                        
                        transform.Find("Delete").gameObject.SetActive(false);
                    }
                }
            }
        }
        //当前选择为背景
        else if (StaticVar.CurrentMenu == transform.Find("HaveMenus/Bc"))
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                //切界面
                transform.Find("CurrentDayMenus").gameObject.SetActive(true);
                foreach (var item in _currentDayMenus)
                {
                    if (((Transform)item).GetComponent<ButtonStateAdjust>().state == ButtonState.Interaction)
                    {
                        ((Transform)item).GetComponent<ButtonStateAdjust>().state = ButtonState.Select;
                        StaticVar.CurrentMenu = (Transform)item;
                    }
                }
                transform.Find("HaveMenus").gameObject.SetActive(false);
            }
        }
    }
    //分类当天菜谱
    public void ClassifyMenu()
    {
        _currentdayfood.Clear();
        _currentdaydrink.Clear();
        //分类当天菜谱
        foreach (var item in MessageSend.instance.CurrentDayMenus)
        {
            if (item.Type == MenuType.Food)
            {
                _currentdayfood.Add(item);
            }
            else if (item.Type == MenuType.Drink)
            {
                _currentdaydrink.Add(item);
            }
        }
    }
    //当天菜谱的初始化
    public void IniCurrentDayMenu()
    {
        ClassifyMenu();
        
        foreach (var item in _currentdayfood)
        {
            _foodToPosition.Add(((Transform)_currentDayMenus[_currentdayfood.IndexOf(item)]).name, item);       
        }
        foreach (var item in _currentdaydrink)
        {
            _drinkToPosition.Add(((Transform)_currentDayMenus[_currentdaydrink.IndexOf(item)]).name, item);
        }
    }

    //当天菜谱的刷新
    public void RefreshCurrentDayMenu()
    {
        ClassifyMenu();
        //给当天菜谱区域赋值
        foreach (var item in _currentDayMenus)
        {
            if (transform.Find("Classify/Food").GetComponent<ButtonStateAdjust>().state == ButtonState.Interaction || StaticVar.CurrentMenu == transform.Find("Classify/Food"))
            {
                if (_foodToPosition.ContainsKey(((Transform)item).name))
                {
                    Transform menu = ((Transform)item).Find("Have");
                    menu.Find("Image").GetComponent<Image>().sprite = LoadTexture.getInstance().LoadAtlasSprite("Atlas/UI/PropertyIcon/Foods/Foods", _foodToPosition[((Transform)item).name].MenuName);
                    //menu.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/UI切图/物品图标/" + _foodToPosition[((Transform)item).name].MenuName);
                    menu.Find("Name").GetComponent<Text>().text = _foodToPosition[((Transform)item).name].MenuName;
                    menu.Find("Price").GetComponent<Text>().text = "$" + _foodToPosition[((Transform)item).name].Price.ToString();
                    menu.gameObject.SetActive(true);
                    ((Transform)item).Find("Add").gameObject.SetActive(false);
                }
                else
                {
                    ((Transform)item).Find("Have").gameObject.SetActive(false);
                    ((Transform)item).Find("Add").gameObject.SetActive(true);
                }
            }
            else if (transform.Find("Classify/Drink").GetComponent<ButtonStateAdjust>().state == ButtonState.Interaction || StaticVar.CurrentMenu == transform.Find("Classify/Drink"))
            {
                if (_drinkToPosition.ContainsKey(((Transform)item).name))
                {
                    Transform menu = ((Transform)item).Find("Have");
                    menu.Find("Image").GetComponent<Image>().sprite = LoadTexture.getInstance().LoadAtlasSprite("Atlas/UI/PropertyIcon/Foods/Foods", _drinkToPosition[((Transform)item).name].MenuName);
                    menu.Find("Name").GetComponent<Text>().text = _drinkToPosition[((Transform)item).name].MenuName;
                    menu.Find("Price").GetComponent<Text>().text = "$" + _drinkToPosition[((Transform)item).name].Price.ToString();
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

    }
    //菜品详细信息区域的刷新（当前选择）
    public void RefreshMenuInfo()
    {
        ClassifyMenu();
        Transform infor = transform.Find("Infor");
        //当前选择在当天菜谱区域
        if (_currentDayMenus.Contains(StaticVar.CurrentMenu))
        {
            if (((Transform)_classify[0]).GetComponent<ButtonStateAdjust>().state == ButtonState.Interaction)
            {
                //判定该位置是否为空 
                if (StaticVar.CurrentMenu.Find("Have").gameObject.activeSelf)
                {
                    string key = StaticVar.CurrentMenu.name;
                    infor.Find("Name").GetComponent<Text>().text = _foodToPosition[key].MenuName;
                    infor.Find("Image").GetComponent<Image>().sprite = StaticVar.CurrentMenu.Find("Have/Image").GetComponent<Image>().sprite;
                    infor.Find("Price").GetComponent<Text>().text = "$" + _foodToPosition[key].Price.ToString();
                    int n = _foodToPosition[key].Level;
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
                    infor.Find("Introduce/Text").GetComponent<Text>().text = _foodToPosition[key].Introduce;
                    infor.gameObject.SetActive(true);
                }
                //该位置为空
                else
                {
                    infor.gameObject.SetActive(false);
                }
            }

            else
            {
                if (StaticVar.CurrentMenu.Find("Have").gameObject.activeSelf)
                {
                    string key = StaticVar.CurrentMenu.name;
                    infor.Find("Name").GetComponent<Text>().text = _drinkToPosition[key].MenuName;
                    infor.Find("Image").GetComponent<Image>().sprite = StaticVar.CurrentMenu.Find("Have/Image").GetComponent<Image>().sprite;
                    infor.Find("Price").GetComponent<Text>().text = "$" + _drinkToPosition[key].Price.ToString();
                    int n = _drinkToPosition[key].Level;
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
                    infor.Find("Introduce/Text").GetComponent<Text>().text = _drinkToPosition[key].Introduce;
                    infor.gameObject.SetActive(true);
                }
                //该位置为空
                else
                {
                    infor.gameObject.SetActive(false);
                }
            }
          
        }
        //当前选择在当前拥有菜品区域
        else if (_haveMenus.Contains(StaticVar.CurrentMenu))
        {
            foreach (var item in MessageSend.instance.HaveMenus)
            {
                if (StaticVar.CurrentMenu.Find("Name").GetComponent<Text>().text == item.MenuName)
                {
                    infor.Find("Name").GetComponent<Text>().text = item.MenuName;
                    infor.Find("Image").GetComponent<Image>().sprite = StaticVar.CurrentMenu.Find("Image").GetComponent<Image>().sprite;
                    infor.Find("Price").GetComponent<Text>().text = "$" + item.Price.ToString();
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
    //当前拥有的所有菜品的分类
    public void RefreshCurrentHaveMenu()
    {
        List<MenuCfg> _food = new List<MenuCfg>();
        List<MenuCfg> _drink = new List<MenuCfg>();
        //分类当前拥有的菜品
        foreach (var item in MessageSend.instance.HaveMenus)
        {
            if (item.Type == MenuType.Food)
            {
                _food.Add(item);
            }
            else if (item.Type == MenuType.Drink)
            {
                _drink.Add(item);
            }
        }
        foreach (var item in _haveMenus)
        {
            ((Transform)item).GetComponent<CanvasGroup>().alpha = 0;
        }

        Transform foodcontent = transform.Find("HaveMenus/Food/Viewport/Content");
        Transform drinkcontent = transform.Find("HaveMenus/Drink/Viewport/Content");
      
        //赋值显示
        if (transform.Find("Classify/Food").GetComponent<ButtonStateAdjust>().state == ButtonState.Interaction)
        {
            //临时列表接收挑选剩下的食物
            List<MenuCfg> leftFood = new List<MenuCfg>();
            ArrayList tempArr = new ArrayList();
            foreach (var item in _food)
            {
                if (!MessageSend.instance.CurrentDayMenus.Contains(item))
                {
                    leftFood.Add(item);
                    tempArr.Add(item);
                }
            }
            //判定是否还有剩下的食物
            if (leftFood.Count == 0)
            {
                transform.Find("HaveMenus/Bc").gameObject.SetActive(true);
                StaticVar.CurrentMenu = transform.Find("HaveMenus/Bc");
            }
            else
            {
                //判定滑动轴
                if (leftFood.Count > 5)
                {
                    Transform slider = transform.Find("HaveMenus/Slider");
                    slider.gameObject.SetActive(true);
                    slider.GetComponent<MyScrollBar>().ScrollList = tempArr;
                    slider.GetComponent<MyScrollBar>().Initialize();
                }

                for (int i = 0; i < leftFood.Count; i++)
                {
                    if (i<5)
                    {
                        Transform temp = transform.Find("HaveMenus/Food/Viewport/Content/Button" + (i + 1));
                        temp.Find("Image").GetComponent<Image>().sprite = LoadTexture.getInstance().LoadAtlasSprite("Atlas/UI/PropertyIcon/Foods/Foods", leftFood[i].MenuName);
                        temp.Find("Name").GetComponent<Text>().text = leftFood[i].MenuName;
                        temp.Find("Price").GetComponent<Text>().text = "$" + leftFood[i].Price.ToString();
                        temp.GetComponent<CanvasGroup>().alpha = 1;
                    }
                }
                StaticVar.CurrentMenu = _haveMenus[0] as Transform;
                transform.Find("HaveMenus/Bc").gameObject.SetActive(false);
            }
            
        }
        else if (transform.Find("Classify/Drink").GetComponent<ButtonStateAdjust>().state == ButtonState.Interaction)
        {
            //临时列表接收挑选剩下的饮品
            List<MenuCfg> leftDrink = new List<MenuCfg>();
            ArrayList tempArr = new ArrayList();
            foreach (var item in _drink)
            {
                if (!MessageSend.instance.CurrentDayMenus.Contains(item))
                {
                    leftDrink.Add(item);
                }
            }
            if (leftDrink.Count == 0)
            {
                transform.Find("HaveMenus/Bc").gameObject.SetActive(true);
                StaticVar.CurrentMenu = transform.Find("HaveMenus/Bc");
            }
            else
            {
                //判定滑动轴
                if (leftDrink.Count > 5)
                {
                    Transform slider = transform.Find("HaveMenus/Slider");
                    slider.gameObject.SetActive(true);
                    slider.GetComponent<MyScrollBar>().ScrollList = tempArr;
                    slider.GetComponent<MyScrollBar>().Initialize();
                }

                for (int i = 0; i < leftDrink.Count; i++)
                {
                    Transform temp = transform.Find("HaveMenus/Food/Viewport/Content/Button" + (i + 1));
                    temp.Find("Image").GetComponent<Image>().sprite = LoadTexture.getInstance().LoadAtlasSprite("Atlas/UI/PropertyIcon/Foods/Foods", leftDrink[i].MenuName);
                    temp.Find("Name").GetComponent<Text>().text = leftDrink[i].MenuName;
                    temp.Find("Price").GetComponent<Text>().text = "$" + leftDrink[i].Price.ToString();
                    temp.GetComponent<CanvasGroup>().alpha = 1;
                }
                StaticVar.CurrentMenu = _haveMenus[0] as Transform;
                transform.Find("HaveMenus/Bc").gameObject.SetActive(false);
            }
        }
    }
}