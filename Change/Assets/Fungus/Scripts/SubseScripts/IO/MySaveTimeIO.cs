using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Fungus;

public class MySaveTimeIO : Singleton<MySaveTimeIO> 
{
    string str = Directory.GetCurrentDirectory();
    public void Write()
    {
        StreamWriter sw = new StreamWriter(Path.Combine(str, @"Assets\MyData\SaveTime.txt"));

        for (int i = 0; i < MessageSend.instance.time.Count; i++)
        {
            sw.Write("#" + MessageSend.instance.time[i]);
        }
        sw.Close();
    }

    public void Read()
    {
        StreamReader sr = new StreamReader(Path.Combine(str, @"Assets\MyData\SaveTime.txt"));
        string B = sr.ReadToEnd();
        string[] C = B.Split('#');
        for (int i = 0; i < C.Length; i++)
        {
            if (C[i] != "")
            {
                MessageSend.instance.time.Add(C[i]);
            }

        }
        sr.Close();
    }

}
