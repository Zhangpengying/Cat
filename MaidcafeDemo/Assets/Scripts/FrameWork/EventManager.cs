using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : ManageBase<EventManager>
{
   
    protected override byte SetMessageType()
    {
        return MyMessageType.Type_Event;
    }
   
}
