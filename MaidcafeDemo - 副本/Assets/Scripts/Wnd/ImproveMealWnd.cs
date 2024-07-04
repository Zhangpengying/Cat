using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImproveMealWnd : BaseWnd
{

    public void Initialize()
    {
        
       
        StaticVar.CurrentMenu = _transform.Find("Menu/Scroll View/Viewport/Content/Button1");
    }
}
public class ImproveMealWndControl :MonoBehaviour
{
    //按钮数组
    //菜品选项
    private ArrayList _menus = new ArrayList();
    //改良选项
    private ArrayList _improve = new ArrayList();
    //每页最多菜品数量
    private int menuNum = 4;
    //当前页码
    private int currentPage = 1;
    //总页数
    private int sumPage;
    private void Start()
    {
        foreach (var item in transform.Find("Menu/Scroll View/Viewport/Content").GetComponentsInChildren<ButtonStateAdjust>())
        {
            _menus.Add(item.transform);
        }
        foreach (var item in transform.Find("SelectWnd").GetComponentsInChildren<ButtonStateAdjust>())
        {
            _improve.Add(item.transform);
        }
        
        //页码设置
        if (MessageSend.instance.HaveMenus.Count % 4 == 0)
        {
            sumPage = MessageSend.instance.HaveMenus.Count / 4 ;
        }
        else
        {
            sumPage = MessageSend.instance.HaveMenus.Count / 4 + 1;
        }
        

        //刷新菜品
        RefreshMenus();
        //刷新玩家金钱
        RefreshPlayer();
    }

    private void Update()
    {
        //当前选择在菜品区域
        if (_menus.Contains(StaticVar.CurrentMenu))
        {
            int n = _menus.IndexOf(StaticVar.CurrentMenu);
            RefreshMenuInfor();
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (_menus.IndexOf(StaticVar.CurrentMenu) == 0)
                {
                    if (currentPage>1)
                    {
                        StaticVar.CurrentMenu = (Transform)_menus[menuNum - 1];
                        currentPage -= 1;
                        RefreshMenus();
                    }
                }
                else
                {
                    StaticVar.CurrentMenu = (Transform)_menus[n - 1];
                }
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (_menus.IndexOf(StaticVar.CurrentMenu) == menuNum - 1)
                {
                    if (currentPage<sumPage)
                    {
                        StaticVar.CurrentMenu = (Transform)_menus[0];
                        currentPage += 1;
                        RefreshMenus();
                    }
                   
                }
                else
                {
                    if ((currentPage-1) * menuNum + n < MessageSend.instance.HaveMenus.Count-1)
                    {
                        StaticVar.CurrentMenu = (Transform)_menus[n + 1];
                    }
                   
                }

            }
            else if (Input.GetKeyDown(KeyCode.Z))
            {
                StaticVar.CurrentMenu.GetComponent<ButtonStateAdjust>().state = ButtonState.Interaction;
                transform.Find("SelectWnd").gameObject.SetActive(true);
                StaticVar.CurrentMenu = (Transform)_improve[0];
            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
                WindowManager.instance.Close<ImproveMealWnd>();
                StaticVar.EndInteraction();
            }
        }
        //当前选择在改良选项区域
        else if (_improve.Contains(StaticVar.CurrentMenu))
        {
            StaticVar.InputControl1(_improve);
            if (Input.GetKeyDown(KeyCode.Z))
            {
                //改良
                if (StaticVar.CurrentMenu == (Transform)_improve[0])
                {
                    //等级提升
                    foreach (var item in _menus)
                    {
                        if (((Transform)item).GetComponent<ButtonStateAdjust>().state == ButtonState.Interaction)
                        {
                            int btnIndex = _menus.IndexOf(item);
                            int menuindex = btnIndex + (currentPage - 1) * menuNum;
                            MessageSend.instance.HaveMenus[menuindex].Level += 1;
                            //玩家个人金币减少
                            int money = (int)StaticVar.PlayerAttribute["Money"];
                            money -= MessageSend.instance.HaveMenus[menuindex].LevelCoefficient;
                            StaticVar.PlayerAttribute["Money"] = money;
                            StaticVar.player.PlayerMoney = money;

                        }
                    }
                    //修改显示
                    RefreshMenus();
                    RefreshMenuInfor();
                    RefreshPlayer();

                    WindowManager.instance.Close<ImproveMealWnd>();
                    StaticVar.EndInteraction();
                  
                }
                //取消
                else
                {
                    foreach (var item in _menus)
                    {
                        if (((Transform)item).GetComponent<ButtonStateAdjust>().state == ButtonState.Interaction)
                        {
                            ((Transform)item).GetComponent<ButtonStateAdjust>().state = ButtonState.Select;
                            StaticVar.CurrentMenu = (Transform)item;
                        }
                    }
                    transform.Find("SelectWnd").gameObject.SetActive(false);
                }
            }
        }
    }
    
    //菜品区域的刷新
    public void RefreshMenus()
    {
      

        for (int i = 0; i < _menus.Count; i++)
        {
            int menuindex = i + (currentPage - 1) * menuNum;
            Transform temp = (Transform)_menus[i];
            if (menuindex < MessageSend.instance.HaveMenus.Count)
            {
                temp.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/UI切图/物品图标/" + MessageSend.instance.HaveMenus[menuindex].MenuName);
                temp.Find("Image").gameObject.SetActive(true);
                temp.Find("Name").GetComponent<Text>().text = MessageSend.instance.HaveMenus[menuindex].MenuName;
                temp.Find("Name").gameObject.SetActive(true);
                temp.Find("Price").GetComponent<Text>().text = "$" + MessageSend.instance.HaveMenus[menuindex].Price.ToString();
                temp.Find("Price").gameObject.SetActive(true);
                int n = MessageSend.instance.HaveMenus[menuindex].Level;
                for (int j = 1; j < 6; j++)
                {
                    if (j <= n)
                    {
                        temp.Find("Quality/Star" + j).GetComponent<StarState>().highQuality = true;
                    }
                    else
                    {
                        temp.Find("Quality/Star" + j).GetComponent<StarState>().highQuality = false;
                    }
                }
                temp.Find("Quality").gameObject.SetActive(true);
                temp.Find("Expend").GetComponent<Text>().text = MessageSend.instance.HaveMenus[menuindex].LevelCoefficient.ToString();
                temp.Find("Expend").gameObject.SetActive(true);
            }
            else
            {
                temp.Find("Image").gameObject.SetActive(false);
                temp.Find("Name").gameObject.SetActive(false);
                temp.Find("Price").gameObject.SetActive(false);
                temp.Find("Quality").gameObject.SetActive(false);
                temp.Find("Expend").gameObject.SetActive(false);
            }
        }
        //刷新页码
        transform.Find("Menu/PageNum/Text").GetComponent<Text>().text = currentPage.ToString() + "/" + sumPage.ToString();
    }
    //菜品信息区域的刷新
    public void RefreshMenuInfor()
    {
        Transform temp = transform.Find("Infor");
        int btnIndex = 0;
        int menuindex = 0;
        //查看菜品
        if (_menus.Contains(StaticVar.CurrentMenu))
        {
           
            btnIndex = _menus.IndexOf(StaticVar.CurrentMenu);
            menuindex = btnIndex + (currentPage - 1) * menuNum;
            
        }
        //改良
        else
        {
            foreach (var item in _menus)
            {
                if (((Transform)item).GetComponent<ButtonStateAdjust>().state == ButtonState.Interaction)
                {
                    btnIndex = _menus.IndexOf(item);
                    menuindex = btnIndex + (currentPage - 1) * menuNum;
                }
            }
        }
        temp.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/UI切图/物品图标/" + MessageSend.instance.HaveMenus[menuindex].MenuName);
        temp.Find("Name").GetComponent<Text>().text = MessageSend.instance.HaveMenus[menuindex].MenuName;
        temp.Find("Price").GetComponent<Text>().text = "$" + MessageSend.instance.HaveMenus[menuindex].Price.ToString();
        int n = MessageSend.instance.HaveMenus[menuindex].Level;
        for (int j = 1; j < 6; j++)
        {
            if (j <= n)
            {
                temp.Find("Quality/Star" + j).GetComponent<StarState>().highQuality = true;
            }
            else
            {
                temp.Find("Quality/Star" + j).GetComponent<StarState>().highQuality = false;
            }
        }
        temp.Find("Introduce/Text").GetComponent<Text>().text = MessageSend.instance.HaveMenus[menuindex].Introduce;
    }

    //刷新玩家个人信息
    public void RefreshPlayer()
    {
        transform.Find("PlayerMoney/Text2").GetComponent<Text>().text = "$" + StaticVar.PlayerAttribute["Money"].ToString();
    }
}
