using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CG2 : BaseWnd
{
    public void Initialize()
    {
        _transform.Find("Image").gameObject.AddComponent<CG2ButtonClickListener>();
    }
}

public class CG2ButtonClickListener : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)             
    {
        WindowManager.instance.Close<CG2>();
        WindowManager.instance.Open<RecallWnd>().Initialize();           

    }
}
