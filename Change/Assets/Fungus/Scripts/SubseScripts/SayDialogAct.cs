using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class SayDialogAct : MonoBehaviour
{
    public static bool sayAct  = false;
    public GameObject obj;
    private void Update()
    {
        sayAct = obj.activeSelf;
    }
}
