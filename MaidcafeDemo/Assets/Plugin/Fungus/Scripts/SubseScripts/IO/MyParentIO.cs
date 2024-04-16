using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Fungus;

public class MyParentIO : Singleton<MyParentIO>
{
    string str = Directory.GetCurrentDirectory();
    public void Write()
    {
        StreamWriter sw = new StreamWriter(Path.Combine(str, @"Assets\MyData\Parent.txt"));

        foreach (KeyValuePair<string, string> kvp in MessageSend.instance._savewnd)
        {
            sw.Write("#" + kvp.Key + "|" + kvp.Value);
        }
        sw.Close();
    }

    public void Read()
    {
        StreamReader sr = new StreamReader(Path.Combine(str, @"Assets\MyData\Parent.txt"));
        string B = sr.ReadToEnd();
        string[] C = B.Split('#');
        for (int i = 0; i < C.Length; i++)
        {
            if (C[i] != "")
            {
                string[] D = C[i].Split('|');
                string son = D[0];
                string parent = D[1];
                MessageSend.instance.SaveMessage(son, parent);
            }

        }
        sr.Close();
    }

}
