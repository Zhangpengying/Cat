﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;
using System;

public class EventsIO : Singleton<EventsIO>
{
    public void Write()
    {
        StreamWriter sw = new StreamWriter(StaticVar.SavePath + "/Events.txt");
        //foreach (KeyValuePair<string,bool> kvp in MessageSend.instance.Events)
        //{
        //    sw.Write("#" + kvp.Key + "&&" + kvp.Value);
        //}
        sw.Close();
    }

    public void Read()
    {
        
        StreamReader sr = new StreamReader(StaticVar.SavePath + "/Events.txt");
        string B = sr.ReadToEnd();
        //if (B!=null)
        //{
        //    string[] C = Regex.Split(B, "#");
        //    for (int i = 0; i < C.Length; i++)
        //    {
        //        string[] D = Regex.Split(C[i], "&&");
        //        //添加进字典
        //        MessageSend.instance.Events.Add(D[0], bool.Parse(D[1]));
        //    }
        //}
        sr.Close();
    }
}
