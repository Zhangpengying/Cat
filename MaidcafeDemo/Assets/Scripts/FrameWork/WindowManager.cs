using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 所有界面类的基类
/// </summary>
public abstract class BaseWnd 
{
    protected Transform _transform;
    /// <summary>
    /// 打开窗口
    /// </summary>
    /// <param name="wndName"></param>
    public void Open(Transform canvas, string wndName)
    {
        _transform = (GameObject.Instantiate(Resources.Load("Prefabs/UIWnd/" + wndName)) as GameObject).transform;
        _transform.SetParent(canvas);
        _transform.name = wndName;

        //_transform.localPosition = Vector3.zero;
        ////获取设备宽高  
        //float device_width = Screen.width;
        //float device_height = Screen.height;

        //float device_aspect = device_width / device_height;
        //var n = _transform.GetComponent<RectTransform>();
        //float m = device_aspect / (16 * 1.0f / 9);
        //if (device_aspect != 16 * 1.0f / 9)
        //{
        //    n.localScale = new Vector3(1.0f / m, 1.0f / m, 1);
        //}
        //else
        //{
        //    n.localScale = Vector3.one;
        //}
        _transform.GetComponent<RectTransform>().offsetMin = Vector2.zero;
        _transform.GetComponent<RectTransform>().offsetMax = Vector2.zero;
        _transform.localPosition = new Vector3(_transform.localPosition.x, _transform.localPosition.y, 0);
        _transform.localScale = Vector3.one;
    }

    /// <summary>
    /// 关闭窗口
    /// </summary>
    public virtual void Close()
    {
        if (_transform != null && _transform.gameObject != null)
        {
            GameObject.Destroy(_transform.gameObject);
        }
    }
    public virtual void Update(float dt){ }
   
}



public class WindowManager : Singleton<WindowManager>
{
    private Transform _canvas;

    // 保存所有的打开的窗口
    public  Dictionary<string, BaseWnd> _windows = new Dictionary<string, BaseWnd>();

    /// <summary>
    /// 初始化
    /// </summary>
    public void Initialize()
    {
        _canvas = GameObject.Find("UI/Canvas").transform;
        Open<StartMenuWnd>().Initialize();
        
    }
     
    /// <summary>
    /// 打开界面
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T Open<T>() where T : BaseWnd, new()
    {
        string wndName = typeof(T).Name;
        if(_windows.ContainsKey(wndName))
        {
            return _windows[wndName] as T;
        }
        else
        {
            T wnd = new T();
            wnd.Open(_canvas, wndName);
            _windows.Add(wndName, wnd);
            return wnd;
        }
    }

    /// <summary>
    /// 关闭窗口 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void Close<T>() where T : BaseWnd
    {
        string wndName = typeof(T).Name;
        if (_windows.ContainsKey(wndName))
        {
            _windows[wndName].Close();
            _windows.Remove(wndName);
            
        }
    }

    public T Get<T>() where T : BaseWnd
    {
        string wndName = typeof(T).Name;
        if (_windows.ContainsKey(wndName))
        {
            return _windows[wndName] as T;
        }
        else
        {
            return null;
        }
    }

    public  void Update(float dt)
    {

        foreach (BaseWnd wnd in _windows.Values)
        {
            wnd.Update(dt);
        }
    }

    public void Clear()
    {
        foreach (string wnd in _windows.Keys)
        {
            _windows[wnd].Close();
        }
        _windows.Clear();
    }
   
}
