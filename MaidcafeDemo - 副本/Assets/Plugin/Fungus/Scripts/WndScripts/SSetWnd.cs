using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Fungus;

public class SSetWnd : MonoBehaviour
{
    public Transform _trans;
    public static float bgmA = 0.5f;
    public static float writeA = 200;
    public static float effA = 200;
    public GameObject obj;
    public void Create()
    {
        Transform _canvas = GameObject.Find("UI/Canvas").transform;
        _trans = (GameObject.Instantiate(Resources.Load("Prefabs/Wnd/SetWnd")) as GameObject).transform;
        _trans.SetParent(_canvas);
        _trans.localPosition = Vector3.zero;
        _trans.localScale = Vector3.one;
        Initialize();
    }

    public void Initialize()
    {
        Slider bgmslider = _trans.Find("背景音乐").GetComponent<Slider>();
        bgmslider.value = bgmA;

        Slider writeslider = _trans.Find("字速").GetComponent<Slider>();
        writeslider.value = writeA;

        Slider soundeff = _trans.Find("音效").GetComponent<Slider>();
        soundeff.value = effA;

        Button btnReturn = _trans.Find("返回3").GetComponent<Button>();
        btnReturn.onClick.AddListener(OnReturn);

        WindowManager.instance.Close<GameOverWnd>();
    }

    private void OnReturn()
    {
        Destroy(GameObject.Find("UI/Canvas/SetWnd(Clone)"));
        WindowManager.instance.Open<GameOverWnd>();
        // Fungus.SendMessage.
        obj.GetComponent<CanvasGroup>().alpha = 1;
    }
}
