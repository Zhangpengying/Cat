using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CG3 : BaseWnd
{
  
    public void Initialize()
    {
        _transform.Find("Image").gameObject.AddComponent<CG3ButtonClickListener>();
    }

}
public class CG3ButtonClickListener : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        WindowManager.instance.Close<CG3>();
        WindowManager.instance.Open<RecallWnd>().Initialize();

    }
}
