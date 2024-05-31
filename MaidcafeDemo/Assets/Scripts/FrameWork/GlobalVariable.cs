
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 全局方法和变量的传递监听
/// </summary>
public class GlobalVariable : MonoBehaviour
{
    
    private void Start()
    {
            
    }
    private void Update()
    {
        if (StaticVar.player!= null)
        {
            if (!StaticVar.player.IsLockPlayer)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                   
                    WindowManager.instance.Open<SystemMenuWnd>().Initialize();
                }
            }
        }
    }

    public void ActiveTips()
    {
        GameObject obj = (Object.Instantiate(Resources.Load("Prefabs/Items/Tips"))) as GameObject;
        obj.transform.SetParent(GameObject.Find("Environment/Events").transform);
        obj.transform.position = StaticVar.InteractiveProp.transform.Find("BG-Counter").position + new Vector3(0, 0.7f* StaticVar.InteractiveProp.transform.parent.localScale.x, 0);
        obj.transform.localScale = Vector3.one;

        obj.name = "Tips";
        obj.SetActive(true);
    }
}
