using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageStateAdjust : MonoBehaviour {

    public ButtonState state;
    //按钮默认状态图片
    public Sprite defaultSprite;
    //按钮选中状态图片
    public Sprite selectSprite;
    private void Update()
    {
        //if (StaticVar.CurrentMenu != null)
        //{
        //    if (state != ButtonState.Interaction)
        //    {
        //        if (StaticVar.CurrentMenu == transform)
        //        {
        //            state = ButtonState.Select;
        //        }
        //        else
        //        {
        //            state = ButtonState.Normal;
        //        }
        //    }

        //}
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
                break;
            case ButtonState.Interaction:
                transform.GetComponent<Image>().sprite = selectSprite;
                break;
            default:
                break;
        }
    }
}
