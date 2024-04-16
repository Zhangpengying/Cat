using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagePlanWnd : BaseWnd
{
    public void Initialize()
    {
        StaticVar.CurrentMenu = _transform.Find("Classify/Type1");
        _transform.gameObject.AddComponent<ManagePlanWndControl>();
    }
	
}

public class ManagePlanWndControl :MonoBehaviour
{
    //按钮数组
    //分类板块
    private ArrayList _activityType = new ArrayList();
    //计划板块
    private ArrayList _plan = new ArrayList();

    private void Start()
    {
        foreach (var item in transform.Find("Classify").GetComponentsInChildren<ButtonStateAdjust>())
        {
            if (!_activityType.Contains(item.transform))
            {
                _activityType.Add(item.transform);
            }
        }
        foreach (var item in transform.Find(""))
        {

        }
    }

    private void Update()
    {
       
    }
}
