using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SGameOver: MonoBehaviour
{
    public Transform _trans;

    public void Create()
    {
        Transform _canvas = GameObject.Find("UI/Canvas").transform;
        _trans = (GameObject.Instantiate(Resources.Load("Prefabs/Wnd/主退出窗口")) as GameObject).transform;
        _trans.SetParent(_canvas);
        _trans.localPosition = Vector3.zero;
        _trans.localScale = Vector3.one;
        Initialize();
    }
    public void Initialize()
    {
        Button btnYes = _trans.Find("ButtonNo").GetComponent<Button>();
        btnYes.onClick.AddListener(OnNo);

        Button btnNo = _trans.Find("ButtonYes").GetComponent<Button>();
        btnNo.onClick.AddListener(OnYes);
    }

    public void OnYes()
    {
        Application.Quit();
        //Destroy(GameObject.Find("UI/Canvas/主退出窗口(Clone)"));
    }

    public  void OnNo()
    {
        Destroy(GameObject.Find("UI/Canvas/主退出窗口(Clone)"));
    }
}
