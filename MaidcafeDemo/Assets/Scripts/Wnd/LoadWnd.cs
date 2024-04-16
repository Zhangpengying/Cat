using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadWnd : BaseWnd
{
    public void Initialize()
    {
        _transform.gameObject.AddComponent<LoadWndControl>();
    }
     

}

public class LoadWndControl : MonoBehaviour
{
    ArrayList archiveDatas = new ArrayList();
    private void Start()
    {
        foreach (var item in GetComponentsInChildren<ButtonStateAdjust>())
        {
            if (!archiveDatas.Contains(item.transform))
            {
                archiveDatas.Add(item.transform);   
            }
        }
        StaticVar.CurrentMenu = archiveDatas[0] as Transform;
    }

    private void Update()
    {
        StaticVar.InputControl1(archiveDatas);
        //返回主界面
        if (Input.GetKeyDown(KeyCode.X))
        {
            WindowManager.instance.Close<LoadWnd>();
            WindowManager.instance.Open<MainMenuWnd>().Initialize();
        }
        //加载存档
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            
        }
    }
}

