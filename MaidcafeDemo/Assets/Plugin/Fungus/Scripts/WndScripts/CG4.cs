using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CG4 : BaseWnd
{
    public void Initialize()
    {
        _transform.Find("Image").gameObject.AddComponent<CG4ButtonClickListener>();
    }
}

public class CG4ButtonClickListener : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        WindowManager.instance.Close<CG4>();
        WindowManager.instance.Open<RecallWnd>().Initialize();

    }
}
