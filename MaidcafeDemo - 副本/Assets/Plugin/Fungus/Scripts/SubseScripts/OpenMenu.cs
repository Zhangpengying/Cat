using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMenu : MonoBehaviour
{
   
    public void Change()
    {
        WindowManager.instance.Close<GameOverWnd>();
        WindowManager.instance.Open<MenuWnd>().Initialize();
    }

}
