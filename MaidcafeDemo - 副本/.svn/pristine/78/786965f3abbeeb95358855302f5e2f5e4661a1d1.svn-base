using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageBox1 : BaseWnd
{
    public void Initialize()
    {
        StaticVar.StartInteraction();
        _transform.gameObject.AddComponent<MessageBox1Control>();
    }

    public void Receive(string msg)
    {
        _transform.Find("Button/Text").GetComponent<Text>().text = msg;
        TimerManager.instance.Invoke(0.5f, delegate { WindowManager.instance.Open<SelectWnd1>().Initialize(); });
        
    }
}

public class MessageBox1Control:MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            WindowManager.instance.Close<MessageBox1>();
        }
    }
}
    

