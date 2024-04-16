using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaiterInfor : BaseWnd
{
    public WaiterCfg SelectWaiter;
    public void Initialize()
    {
        _transform.gameObject.AddComponent<WaiterInforCon>().selectWaiter = SelectWaiter;

    }
}

public class WaiterInforCon : MonoBehaviour
{
    public WaiterCfg selectWaiter;
    private void Start()
    {
        //刷新基本信息
        RefreshBasicInfor();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.Find("Page1").gameObject.SetActive(true);
            transform.Find("Page2").gameObject.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.Find("Page2").gameObject.SetActive(true);
            transform.Find("Page1").gameObject.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            WindowManager.instance.Close<WaiterInfor>();
            WindowManager.instance.Open<WaiterInforListsWnd>();
            WindowManager.instance.Get<WaiterInforListsWnd>().selectWaiter = selectWaiter;
            WindowManager.instance.Get<WaiterInforListsWnd>().Initialize();
        }
    }

    private void RefreshBasicInfor()
    {
        transform.Find("Page1/Name/Text1").GetComponent<Text>().text = selectWaiter.Name;
        transform.Find("Page1/Bg/Image").GetComponent<Image>().sprite = LoadTexture.getInstance().LoadAtlasSprite("Atlas/UI/PropertyIcon/Characters/Head/Head", selectWaiter.Name);
        transform.Find("Page1/Infor/Name/Text2").GetComponent<Text>().text = selectWaiter.Name;
        transform.Find("Page1/Infor/Age/Text2").GetComponent<Text>().text = selectWaiter.Age.ToString();
        transform.Find("Page1/Infor/Job/Text2").GetComponent<Text>().text = selectWaiter.Job;
        transform.Find("Page1/Infor/Interest/Text2").GetComponent<Text>().text = selectWaiter.Interest;
        transform.Find("Page1/Infor/Special/Text2").GetComponent<Text>().text = selectWaiter.Interest;
        transform.Find("Page1/Infor/Intro/Text2").GetComponent<Text>().text = selectWaiter.Introduce;
    }
}