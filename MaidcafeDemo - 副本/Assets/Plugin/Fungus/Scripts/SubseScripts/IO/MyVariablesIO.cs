using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Fungus;

public class MyVariablesIO : Singleton<MyVariablesIO>
{
    string str = Directory.GetCurrentDirectory();
    public void Write()
    {
        StreamWriter sw = new StreamWriter(Path.Combine(str, @"Assets\MyData\CG.txt"));

        foreach (KeyValuePair<string, bool> kvp in MessageSend.instance._recallwnd)
        {
            sw.Write("#" + kvp.Key + "|" + kvp.Value);
        }
        sw.Close();
    }

    public void Read()
    {
        StreamReader sr = new StreamReader(Path.Combine(str, @"Assets\MyData\CG.txt"));
        string B = sr.ReadToEnd();
        string[] C = B.Split('#');
        for (int i = 0; i < C.Length; i++)
        {
            if (C[i] != "")
            {
                string[] D = C[i].Split('|');
                string CGName = D[0];
                bool bl = bool.Parse(D[1]);
                MessageSend.instance.CGMessage(CGName,bl);
            }

        }
        sr.Close();
    }

}
