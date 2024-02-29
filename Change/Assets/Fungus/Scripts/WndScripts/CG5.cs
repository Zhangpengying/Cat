using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CG5 :BaseWnd
{
    public void Initialize()
    {
        _transform.Find("Image").gameObject.AddComponent<CG5ButtonClickListener>();
    }

}

public class CG5ButtonClickListener : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        WindowManager.instance.Close<CG5>();
        WindowManager.instance.Open<RecallWnd>().Initialize();

    }
}

