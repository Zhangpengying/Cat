using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BlockNameIO : Singleton<BlockNameIO>
{
    public void Write()
    {
        StreamWriter sw = new StreamWriter(StaticVar.SavePath + "/BlockName.txt");

        //foreach (KeyValuePair<string, bool> kvp in MessageSend.instance._divergence)
        //{
        //    sw.Write("#" + kvp.Key + "|" + kvp.Value);
        //}
        sw.Close();
    }

    public void Read()
    {
        StreamReader sr = new StreamReader(StaticVar.SavePath + "/BlockName.txt");
        string B = sr.ReadToEnd();
        string[] C = B.Split('#');
        for (int i = 0; i < C.Length; i++)
        {
            if (C[i] != "")
            {
                string[] D = C[i].Split('|');
                string divergence = D[0];
                bool bl = bool.Parse(D[1]);
                //MessageSend.instance._divergence.Add(divergence, bl);
            }

        }
        sr.Close();
    }
}
