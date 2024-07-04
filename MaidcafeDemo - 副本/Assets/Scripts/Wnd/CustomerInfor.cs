using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerInfor : BaseWnd
{
    public CustomerCfg customerCfg;
    public void Initialize()
    {
        _transform.gameObject.AddComponent<CusInforContro>().customer = customerCfg;
    }
	
}

public class CusInforContro: MonoBehaviour
{
    public CustomerCfg customer;
    private void Start()
    {
        RefreshBasicInfor();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            WindowManager.instance.Close<CustomerInfor>();
            WindowManager.instance.Open<CustomerInforListsWnd>();
            WindowManager.instance.Get<CustomerInforListsWnd>().selectCus = customer;
            WindowManager.instance.Get<CustomerInforListsWnd>().Initialize();
        }
    }

    //刷新基本信息
    private void RefreshBasicInfor()
    {
        transform.Find("Name/Text1").GetComponent<Text>().text = customer.Name;
        transform.Find("Infor/Image").GetComponent<Image>().sprite = LoadTexture.getInstance().LoadAtlasSprite("", customer.Name);
        transform.Find("Infor/Name/Text2").GetComponent<Text>().text = customer.Name;

    }
}
