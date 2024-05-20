using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindupWnd : BaseWnd
{
    public void Initialize()
    {
        StaticVar.CurrentMenu = _transform.Find("Classify/FoodInfo");
        _transform.gameObject.AddComponent<WindupWndControl>();
    }
	
}

public class WindupWndControl:MonoBehaviour
{
    //按钮数组
    //分类界面
    private ArrayList _infoType = new ArrayList();

    //点餐的菜品
    private ArrayList _sellMenuBtnlist = new ArrayList();
    //售出周边界面
    private ArrayList _sellComBtnList = new ArrayList();

    private void Start()
    {
        foreach (var item in transform.Find("Classify").GetComponentsInChildren<ButtonStateAdjust>())
        {
            if (!_infoType.Contains(item.transform))
            {
                _infoType.Add(item.transform);
            }
        }

        foreach (var item in transform.Find("Food/Scroll View/Viewport/Content").GetComponentsInChildren<ButtonStateAdjust>())
        {
            if (!_sellMenuBtnlist.Contains(item.transform))
            {
                _sellMenuBtnlist.Add(item.transform);
            }
        }
        foreach (var item in transform.Find("Commodity").GetComponentsInChildren<ButtonStateAdjust>())
        {
            if (!_sellComBtnList.Contains(item.transform))
            {
                _sellComBtnList.Add(item.transform);
            }
        }


    }

    private void Update()
    {
        //当前选择在分类界面
        if (_infoType.Contains(StaticVar.CurrentMenu))
        {
            StaticVar.InputControl1(_infoType);
            //刷新界面信息
            RefreshInfo();
            //返回上一级
            if (Input.GetKeyDown(KeyCode.X))
            {
                StaticVar.CurrentMenu = null;
                StaticVar.EndInteraction();
                WindowManager.instance.Close<WindupWnd>();
            }
        }

    }

    //信息刷新
    public void RefreshInfo()
    {
        //显示该轮经营点餐的具体信息
        if (StaticVar.CurrentMenu ==(Transform)_infoType[0])
        {
            transform.Find("Food").gameObject.SetActive(true);
            transform.Find("Commodity").gameObject.SetActive(false);
            int n = 0;
            foreach (KeyValuePair<MenuCfg, int> kvp in StaticVar.ManageMenuData)
            {
                Transform infor = ((Transform)_sellMenuBtnlist[n]).Find("Infor");
                infor.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/UI切图/物品图标/" + kvp.Key.MenuName);
                infor.Find("Name").GetComponent<Text>().text = kvp.Key.MenuName;
                infor.Find("Price").GetComponent<Text>().text = "$" + kvp.Key.Price.ToString();
                infor.Find("Num").GetComponent<Text>().text = kvp.Value.ToString();
                infor.Find("Pay").GetComponent<Text>().text = "$" + (kvp.Key.Price * kvp.Value).ToString();
                infor.gameObject.SetActive(true);
                n += 1;
            }
        }
        //该轮经营周边的具体信息
        else if (StaticVar.CurrentMenu == (Transform)_infoType[1])
        {
            transform.Find("Food").gameObject.SetActive(false);
            transform.Find("Commodity").gameObject.SetActive(true);
            int n = 0;
            foreach (KeyValuePair<CommodityCfg, int> kvp in StaticVar.ManageComData)
            {
                Transform infor = ((Transform)_sellComBtnList[n]).Find("Infor");
                infor.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/UI切图/物品图标/" + kvp.Key.Name);
                infor.Find("Name").GetComponent<Text>().text = kvp.Key.Name;
                infor.Find("Price").GetComponent<Text>().text = "$" + kvp.Key.Price.ToString();
                infor.Find("Num").GetComponent<Text>().text = kvp.Value.ToString();
                infor.Find("Pay").GetComponent<Text>().text = "$" + (kvp.Key.Price * kvp.Value).ToString();
                infor.gameObject.SetActive(true);
                n += 1;
            }
        }
        //结束中场休息
        else if (StaticVar.CurrentMenu == (Transform)_infoType[2])
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                StaticVar.ToNextScenes("Home_Normal_01", StaticVar.player);
                WindowManager.instance.Close<WindupWnd>();
            }
        }
        //总体收益界面
        int Sum = 0;
        foreach (KeyValuePair<MenuCfg, int> kvp in StaticVar.ManageMenuData)
        {
            Sum+= kvp.Key.Price * kvp.Value;
        }
        foreach (KeyValuePair<CommodityCfg, int> kvp in StaticVar.ManageComData)
        {
            Sum += kvp.Key.Price * kvp.Value;
        }
        transform.Find("Infor/Introduce/Text").GetComponent<Text>().text = "今天一共来了3位客人，知名度增加了好几百，总收入为$" + Sum.ToString() ;
    }
}
