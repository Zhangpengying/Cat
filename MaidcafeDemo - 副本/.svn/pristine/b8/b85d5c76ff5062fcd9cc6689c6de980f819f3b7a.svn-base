using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Choice1Wnd : BaseWnd
{
    public Transform defaultBtn;
    public void Initialize()
    {
        defaultBtn = _transform.Find("ImproveBtn");
        StaticVar.CurrentMenu = defaultBtn;
        defaultBtn.GetComponent<ButtonStateAdjust>().state = ButtonState.Select;
        _transform.gameObject.AddComponent<ChoiceControl>();
    }

}

public class ChoiceControl:MonoBehaviour
{
    private ArrayList SelectButton = new ArrayList();  
    private void Start()
    {
        foreach (var item in Object.FindObjectsOfType<ButtonStateAdjust>())
        {
            if (!SelectButton.Contains(item))
            {
                SelectButton.Add(item);
            }
           
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (StaticVar.CurrentMenu != null)
            {
                int n = SelectButton.IndexOf(StaticVar.CurrentMenu);
                StaticVar.CurrentMenu.GetComponent<ButtonStateAdjust>().state = ButtonState.Normal;
                if (n > 0)
                {
                    StaticVar.CurrentMenu = (Transform)SelectButton[n - 1];
                } 
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (StaticVar.CurrentMenu != null)
            {
                int n = SelectButton.IndexOf(StaticVar.CurrentMenu);
                StaticVar.CurrentMenu.GetComponent<ButtonStateAdjust>().state = ButtonState.Normal;
                if (n < SelectButton.Count-1)
                {
                    StaticVar.CurrentMenu = (Transform)SelectButton[n + 1];
                }
            }
        }
        //选中按钮
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            //改良
            if (StaticVar.CurrentMenu ==(Transform)SelectButton[0])
            {
                WindowManager.instance.Open<ImproveMealWnd>().Initialize();
            }
            //取消
            else
            {
                StaticVar.EndInteraction();
            }
            WindowManager.instance.Close<Choice1Wnd>();
        }
    }
}
    