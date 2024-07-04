using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

public class CreatPropertysInfoIO : Singleton<CreatPropertysInfoIO>
{
    public void Write()
    {
        StreamWriter sw = new StreamWriter(StaticVar.SavePath + "/CreatPropertysInfo.txt");

        foreach (KeyValuePair<Property, Vector3> kvp in MessageSend.instance.CreatPropertysInfo)
        {
            sw.Write("ID" + kvp.Key.ID + "Position" + kvp.Value);
        }
        sw.Close();
        
    }

    public void Read()
    {
        MessageSend.instance.CreatPropertysInfo.Clear();
        StreamReader sr = new StreamReader(StaticVar.SavePath + "/CreatPropertysInfo.txt");

        string B = sr.ReadToEnd();
        if (B != null)
        {
            string[] C = Regex.Split(B, "ID");
            for (int i = 0; i < C.Length; i++)
            {
                if (C[i] != "")
                {
                    string[] D = Regex.Split(C[i], "Position");
                    int ID = int.Parse(D[0]);
                    Vector3 Position = StaticVar.ParseVector3(D[1]);
                    foreach (var item in MessageSend.instance.CurrentHavePropertys)
                    {
                        if (item.ID == ID)
                        {
                            MessageSend.instance.CreatPropertysInfo.Add(item, Position);
                        }
                    }
                  }
            }
        }
        sr.Close();
    }
}
