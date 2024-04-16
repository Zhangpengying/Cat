using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : ManageBase<UIManager>
{
    protected override byte SetMessageType()
    {
        return MyMessageType.Type_UI;
    }
}
