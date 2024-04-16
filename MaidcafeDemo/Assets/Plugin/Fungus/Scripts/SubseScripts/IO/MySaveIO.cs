using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Fungus;

public class MySaveIO : Singleton<MySaveIO>
{
    string str = Directory.GetCurrentDirectory();
    public void Write()
    {
        StreamWriter sw = new StreamWriter(Path.Combine(str, @"Assets\MyData\MyFile.txt"));

        for (int i = 0; i < MyMenu.CurrentsaveDataKeys().Count; i++)
        {
            sw.Write("#" + i + "." + MyMenu.CurrentsaveDataKeys()[i]);
        }
        sw.Close();
    }

    public void Read()
    {
        StreamReader sr = new StreamReader(Path.Combine(str, @"Assets\MyData\MyFile.txt"));
        string B = sr.ReadToEnd();
        string[] C = B.Split('#');
        for (int i = 0; i < C.Length; i++)
        {
            if (C[i] != "")
            {
                string[] D = C[i].Split('.');
                int c = int.Parse(D[0]);
                string key = D[1];
                MyMenu.CurrentsaveDataKeys().Add(key);
            }

        }
        sr.Close();
    }

}
