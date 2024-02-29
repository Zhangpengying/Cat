using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MyActCGNumIO : Singleton<MyActCGNumIO>
{
    string str = Directory.GetCurrentDirectory();
    public void Write()
    {
        StreamWriter sw = new StreamWriter(Path.Combine(str, @"Assets\MyData\ActCGNum.txt"));

        for (int i = 0; i < MessageSend.instance.CGActNum.Count; i++)
        {
            sw.Write("#" + MessageSend.instance.CGActNum[i]);
        }
        sw.Close();
    }

    public void Read()
    {
        StreamReader sr = new StreamReader(Path.Combine(str, @"Assets\MyData\ActCGNum.txt"));
        string B = sr.ReadToEnd();
        string[] C = B.Split('#');
        for (int i = 0; i < C.Length; i++)
        {
            if (C[i] != "")
            {
                int d = int.Parse(C[i]);
                MessageSend.instance.CGActNum.Add(d);
            }
        }
        sr.Close();
    }

}
