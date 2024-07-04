using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoffeeMenuWnd : BaseWnd
{
    public void Initialize()
    {
        StaticVar.CurrentMenu = _transform.Find("Classify/Food");
        _transform.gameObject.AddComponent<CoffeeMenuWndCon>();
    }
	
}

public class CoffeeMenuWndCon:MonoBehaviour
{
    //按钮数组
    //当前拥有的全部菜品按钮
    public ArrayList _haveMenus = new ArrayList();
    
    //菜品类型按钮
    private ArrayList _classify = new ArrayList();
   

    //菜品分类
    private List<MenuCfg> _currentdayfood = new List<MenuCfg>();
    private List<MenuCfg> _currentdaydrink = new List<MenuCfg>();



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
        //foreach (var item in transform.Find("HaveMenus/Food/Viewport/Content").GetComponentsInChildren<ButtonStateAdjust>())
        //{
        //    if (!_haveMenus.Contains(item.transform))
        //    {
        //        _haveMenus.Add(item.transform);
        //    }
        //}
    }
    private void Update()
    {
        
        //当前选择在分类区域
        if (_classify.Contains(StaticVar.CurrentMenu))
        {
            StaticVar.InputControl1(_classify);
            
            //确认选中
            if (Input.GetKeyDown(KeyCode.Z))
            {
                //该按钮状态改变
                StaticVar.CurrentMenu.GetComponent<ButtonStateAdjust>().state = ButtonState.Interaction;
                //跳转到下一级

                RefreshCurrentHaveMenu();
                StaticVar.CurrentMenu = (Transform)_haveMenus[0];
            }
            //取消，返回咖啡店界面
            else if (Input.GetKeyDown(KeyCode.X))
            {
                StaticVar.CurrentMenu = null;
                WindowManager.instance.Close<CoffeeMenuWnd>();
                WindowManager.instance.Open<CoffeeWnd>();
                WindowManager.instance.Get<CoffeeWnd>().lastwndName = transform.name;
                WindowManager.instance.Get<CoffeeWnd>().Initialize();
            }
        }
       
        //当前选择在当前拥有的菜单区域
        else if (_haveMenus.Contains(StaticVar.CurrentMenu))
        {
            StaticVar.InputControl1(_haveMenus);
            //刷新菜品信息
            RefreshMenuInfo();
            transform.Find("HaveMenus/Slider").GetComponent<MyScrollBar>().ScrollList = _haveMenus;

           
            //撤销返回上一级
            if (Input.GetKeyDown(KeyCode.X))
            {
                foreach (var item in _classify)
                {
                    if (((Transform)item).GetComponent<ButtonStateAdjust>().state== ButtonState.Interaction)
                    {
                        StaticVar.CurrentMenu = item as Transform;
                        ((Transform)item).GetComponent<ButtonStateAdjust>().state = ButtonState.Select;
                    }
                }
                 
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
  

    
    //菜品详细信息区域的刷新（当前选择）
    public void RefreshMenuInfo()
    {
        ClassifyMenu();
        Transform infor = transform.Find("Infor");
     
        //当前选择在当前拥有菜品区域
        if (_haveMenus.Contains(StaticVar.CurrentMenu))
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
        //先清空
        _haveMenus.Clear();
        
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


        Transform foodcontent = transform.Find("HaveMenus/Food/Viewport/Content");
        Transform drinkcontent = transform.Find("HaveMenus/Drink/Viewport/Content");

        //动态激活按钮
        if (transform.Find("Classify/Food").GetComponent<ButtonStateAdjust>().state == ButtonState.Interaction || StaticVar.CurrentMenu == transform.Find("Classify/Food"))
        {
            transform.Find("HaveMenus/Food").gameObject.SetActive(true);
            transform.Find("HaveMenus/Drink").gameObject.SetActive(false);
            transform.Find("HaveMenus/Slider").GetComponent<MyScrollBar>().enabled = true;
            transform.Find("HaveMenus/Slider").GetComponent<MyScrollBar>().Content = transform.Find("HaveMenus/Food/Viewport/Content");
            //先清空
            foreach (var item in foodcontent.GetComponentsInChildren<ButtonStateAdjust>())
            {
                item.gameObject.SetActive(false);
            }

            for (int i = 0; i < MessageSend.instance.HaveMenus.Count; i++)
            {
                Transform temp = transform.Find("HaveMenus/Food/Viewport/Content/Button" + (i + 1));
                temp.Find("Image").GetComponent<Image>().sprite = LoadTexture.getInstance().LoadAtlasSprite("Atlas/UI/PropertyIcon/Foods/Foods", MessageSend.instance.HaveMenus[i].MenuName);
                //temp.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/UI切图/物品图标/" + leftFood[i].MenuName);
                temp.Find("Name").GetComponent<Text>().text = MessageSend.instance.HaveMenus[i].MenuName;
                temp.Find("Price").GetComponent<Text>().text = "$" + MessageSend.instance.HaveMenus[i].Price.ToString();
                temp.gameObject.SetActive(true);
                _haveMenus.Add(temp);
            }
            StaticVar.CurrentMenu = foodcontent.Find("Button1");
          
        }
        else if (transform.Find("Classify/Drink").GetComponent<ButtonStateAdjust>().state == ButtonState.Interaction || StaticVar.CurrentMenu == transform.Find("Classify/Food"))
        {
            transform.Find("HaveMenus/Drink").gameObject.SetActive(true);
            transform.Find("HaveMenus/Food").gameObject.SetActive(false);
            transform.Find("HaveMenus/Slider").GetComponent<MyScrollBar>().Content = transform.Find("HaveMenus/Drink/Viewport/Content");
            //先清空
            foreach (var item in drinkcontent.GetComponentsInChildren<ButtonStateAdjust>())
            {
                item.gameObject.SetActive(false);
            }


            for (int i = 0; i < MessageSend.instance.HaveMenus.Count; i++)
            {
                Transform temp = transform.Find("HaveMenus/Drink/Viewport/Content/Button" + (i + 1));
                temp.Find("Image").GetComponent<Image>().sprite = LoadTexture.getInstance().LoadAtlasSprite("Atlas/UI/PropertyIcon/Foods/Foods", MessageSend.instance.HaveMenus[i].MenuName);
                //temp.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/UI切图/物品图标/" + leftDrink[i].MenuName);
                temp.Find("Name").GetComponent<Text>().text = MessageSend.instance.HaveMenus[i].MenuName;
                temp.Find("Price").GetComponent<Text>().text = "$" + MessageSend.instance.HaveMenus[i].Price.ToString();
                temp.gameObject.SetActive(true);
                _haveMenus.Add(temp);

            }
            StaticVar.CurrentMenu = drinkcontent.Find("Button1");
            
            
        }
    }
}