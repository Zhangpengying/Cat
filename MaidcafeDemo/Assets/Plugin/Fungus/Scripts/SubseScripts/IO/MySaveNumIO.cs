using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Fungus;

public class MySaveNumIO : Singleton<MySaveNumIO>
{
    string str = Directory.GetCurrentDirectory();
    public void Write()
    {
        StreamWriter sw = new StreamWriter(Path.Combine(str, @"Assets\MyData\SaveNum.txt"));

        for (int i = 0; i < MessageSend.instance.temp.Count; i++)
        {
            sw.Write("#" + MessageSend.instance.temp[i]);
        }
        sw.Close();
    }

    public void Read()
    {
        StreamReader sr = new StreamReader(Path.Combine(str, @"Assets\MyData\SaveNum.txt"));
        string B = sr.ReadToEnd();
        string[] C = B.Split('#');
        for (int i = 0; i < C.Length; i++)
        {
            if (C[i] != "")
            {
                int d = int.Parse(C[i]);
                MessageSend.instance.temp.Add(d);
            }
        }
        sr.Close();
    }

}
