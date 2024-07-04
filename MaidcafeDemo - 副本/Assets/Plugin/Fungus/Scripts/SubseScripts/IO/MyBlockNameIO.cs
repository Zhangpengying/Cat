using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Fungus;

public class MyBlockNameIO : Singleton<MyBlockNameIO>
{
    string str = Directory.GetCurrentDirectory();

    public void Write()
    {
        StreamWriter sw = new StreamWriter(Path.Combine(str, @"Assets\MyData\SavePointName.txt"));


        for (int i = 0; i < SavePoint.SavepointName.Count; i++)
        {
            sw.Write("#" + SavePoint.SavepointName[i]);
        }
        sw.Close();
    }

    public void Read()
    {
        StreamReader sr = new StreamReader(Path.Combine(str, @"Assets\MyData\SavePointName.txt"));
        string B = sr.ReadToEnd();
        string[] C = B.Split('#');
        for (int i = 0; i < C.Length; i++)
        {
            if (C[i] != "")
            {
                string key = C[i];
                SavePoint.SavepointName.Add(key);
               
            }

        }
        sr.Close();
    }

}
