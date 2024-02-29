using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Fungus;

public class MyImageIO : Singleton<MyImageIO>
{
    string str = Directory.GetCurrentDirectory();
    public void Write()
    {
        StreamWriter sw = new StreamWriter(Path.Combine(str, @"Assets\MyData\Image.txt"));

        foreach (KeyValuePair<string, string> kvp in MessageSend.instance._image)
        {
            sw.Write("#" + kvp.Key + "|" + kvp.Value);
        }
        sw.Close();
    }

    public void Read()
    {
        StreamReader sr = new StreamReader(Path.Combine(str, @"Assets\MyData\Image.txt"));
        string B = sr.ReadToEnd();
        string[] C = B.Split('#');
        for (int i = 0; i < C.Length; i++)
        {
            if (C[i] != "")
            {
                string[] D = C[i].Split('|');
                string poi = D[0];
                string img = D[1];
                MessageSend.instance.ImageMessage(poi, img);
            }

        }
        sr.Close();
    }

}
