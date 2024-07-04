using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariablesSend : MonoBehaviour
{
 

    public static bool CG1Active;
    //CG1里的图片激活状态
    public static bool _CG1Active;
    //CG1ContentAct方法执行的次数
    public static int num = 0;

    public static bool CG2Active;


    public void CG1Act()
    {
        CG1Active = true;
    }

    public void CG1ContentAct()
    {

        MyCG1ContentIO.instance.Read();
        if (MessageSend.instance._cg1content.Count != 0)
        {
            if (MessageSend.instance._cg1content[0] == 2)
            {
                num = 2;
            }
        }
        num++;
        if (num >= 3)
        {
            Debug.Log(num);
            num = 3;
        }
        if (num < 3)
        {
            MessageSend.instance.CG1Content(num);
            MyCG1ContentIO.instance.Write();
            _CG1Active = true;
        }
    }

    public void CG2Act()
    {
        CG2Active = true;
    }

}
