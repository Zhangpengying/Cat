using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenuWnd : BaseWnd
{
    public void Initialize()
    {
        //判定是否有存档
        StaticVar.HaveSave = PlayerPrefs.GetInt("HaveSave");
        if (StaticVar.HaveSave == 0)
        {
            _transform.Find("MenuList/Continue").gameObject.SetActive(false);
        }
        else
        {
            _transform.Find("MenuList/Continue").gameObject.SetActive(true);
        }
        _transform.gameObject.AddComponent<StartMenuWndContro>();
        //Button startGame = _transform.Find("MenuList/StartGame").GetComponent<Button>();
        //StaticVar.CurrentMenu = startGame.transform;

    }


}

public class StartMenuWndContro : MonoBehaviour
{
    //动画是否已经播放过了
    private bool havePlayAni = false;

    //选项列表
    private ArrayList menuList = new ArrayList();
    private void Start()
    {
        foreach (var item in GetComponentsInChildren<ButtonStateAdjust>())
        {
            if (!menuList.Contains(item.transform))
            {
                menuList.Add(item.transform);
            }
        }
        StaticVar.CurrentMenu = menuList[0] as Transform;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (StaticVar.CurrentMenu == transform.Find("MenuList/StartGame"))
            {
                StaticVar.CurrentMenu.GetComponent<Animation>().Play("StartGame");
                havePlayAni = true;
            }
            else if (StaticVar.CurrentMenu == transform.Find("MenuList/Continue"))
            {
                WindowManager.instance.Close<MainMenuWnd>();
                WindowManager.instance.Open<LoadWnd>().Initialize();
            }
            else if (StaticVar.CurrentMenu == transform.Find("MenuList/Exit"))
            {

            }
        }

        if (StaticVar.CurrentMenu != null)
        {
            if (havePlayAni)
            {
                TimerManager.instance.Invoke(1.5f, LoadScene);
                havePlayAni = false;
            }
            else
            {
                StaticVar.InputControl1(menuList);
            }
        }
    }

    void LoadScene()
    {
        //场景的清算
        WindowManager.instance.Close<StartMenuWnd>();
        StaticVar.ClearScene();
        SceneManager.LoadScene("StartVillage");
        //BusinessStreet_001_Normal_Day_01
        //StartVillage
        Time.timeScale = 1;
    }
}

