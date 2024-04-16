using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

public class PropertyToIDIO : Singleton<PropertyToIDIO>
{
    public void Write()
    {
        StreamWriter sw = new StreamWriter(StaticVar.SavePath + "/PropertyToID.txt");

        foreach (KeyValuePair<Property,Vector2> kvp in MessageSend.instance.PropertyToID)
        {
            sw.Write("Position" + kvp.Value + "ID" + kvp.Key.ID);
        }
        sw.Close();
    }

    public void Read()
    {
        MessageSend.instance.PropertyToID.Clear();
       
        StreamReader sr = new StreamReader(StaticVar.SavePath + "/PropertyToID.txt");

        string B = sr.ReadToEnd();
        if (B != null)
        {
            string[] C = Regex.Split(B, "Position");
            for (int i = 0; i < C.Length; i++)
            {
                if (C[i] != "")
                {
                    string[] D = Regex.Split(C[i], "ID");
                    string Position = D[0];
                    int ID = int.Parse(D[1]);
                    Property property = MessageSend.instance.propertyCfgs[ID];
                    MessageSend.instance.PropertyToID.Add(property, StaticVar.ParseVector2(Position));
                }
            }
        }
        sr.Close();
    }
}
