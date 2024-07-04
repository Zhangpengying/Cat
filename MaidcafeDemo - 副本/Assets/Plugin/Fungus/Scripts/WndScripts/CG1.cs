using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CG1 : BaseWnd
{
    public List<GameObject> cg1 = new List<GameObject>();
    public void Initialize()
    {
        MessageSend.instance._cg1content.Clear();
        MyCG1ContentIO.instance.Read();
        Act();

        for (int i = 1; i < 3; i++)
        {
            if (GameObject.Find("UI/Canvas/CG1/Image" + i)!= null)
            {
                cg1.Add(GameObject.Find("UI/Canvas/CG1/Image" + i));
            }
        }
       
        //var cg1 = GameObject.FindGameObjectsWithTag("CG1");
        switch (cg1.Count)
        {
            case 1:
                _transform.Find("Image2").gameObject.AddComponent<CG1ButtonClickListener>();
                //Add(cg1);
                break;
            case 2:
                _transform.Find("Image1").gameObject.AddComponent<CG1ButtonClickListener>();
                //Add(cg1);
                break;
            default:
                break;
        }
    }
    //分差CG的底层激活
    public void Act()
    {
        for (int i = 0; i < MessageSend.instance._cg1content.Count; i++)
        {
            for (int j = 0; j < MessageSend.instance._cg1content[i]; j++)
            {
                _transform.Find("Image" + (2 - j)).gameObject.SetActive(true);
            }
        }
      
    }
    

}

public class CG1ButtonClickListener : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        WindowManager.instance.Close<CG1>();
        WindowManager.instance.Open<RecallWnd>().Initialize();
    }
}
