using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum ButtonState
{
    //正常状态
    Normal = 0,
    //选择状态
    Select = 1,
    //按键互动状态
    Interaction = 2
}

public class ButtonStateAdjust : MonoBehaviour
{
    public ButtonState state;
    //按钮默认状态图片
    public Sprite defaultSprite;
    //按钮选中状态图片
    public Sprite selectSprite;
    private void Update()
    {
        if (StaticVar.CurrentMenu!= null)
        {
            if (state!= ButtonState.Interaction)
            {
                if (StaticVar.CurrentMenu == transform)
                {
                    state = ButtonState.Select;
                }
                else
                {
                    state = ButtonState.Normal;
                }
            }
           
        }
        CheckState();
    }

    public void CheckState()
    {
        switch (state)
        {
            case ButtonState.Normal:
                transform.GetComponent<Image>().sprite = defaultSprite;
                break;
            case ButtonState.Select:
                transform.GetComponent<Image>().sprite = selectSprite;
                StaticVar.CurrentMenu = transform;
                break;
            case ButtonState.Interaction:
                transform.GetComponent<Image>().sprite = selectSprite;
                break;
            default:
                break;
        }
    }
}
