using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;

public class HaveMenuIO : Singleton<HaveMenuIO>
{
    public void Write()
    {
        StreamWriter sw = new StreamWriter(StaticVar.SavePath + "/HaveMenu.txt");

        foreach (var item in MessageSend.instance.HaveMenus)
        {
            sw.Write("ID" + item.ID + "Level" + item.Level);
        }
        sw.Close();
    }

    public void Read()
    {
        MessageSend.instance.HaveMenus.Clear();
        StreamReader sr = new StreamReader(StaticVar.SavePath + "/HaveMenu.txt");
      
        string B = sr.ReadToEnd();
        if (B!=null)
        {
            string[] C = Regex.Split(B, "ID");
            for (int i = 0; i < C.Length; i++)
            {
                if (C[i] != "")
                {
                    string[] D = Regex.Split(C[i], "Level");
                    int ID = int.Parse(D[0]);
                    int Level = int.Parse(D[1]);
                    MenuCfg cfg = MessageSend.instance.menuCfgs[ID];
                    cfg.Level = Level;
                    MessageSend.instance.HaveMenus.Add(cfg);
                }
            }
        }
        sr.Close();
    }
}
