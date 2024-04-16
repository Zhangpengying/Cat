using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;
using System;

public class PlayerInforIO : Singleton<PlayerInforIO>
{
    public void Write()
    {
        StreamWriter sw = new StreamWriter(StaticVar.SavePath + "/PlayerInfor.txt");
        sw.Write(StaticVar.PlayerAttribute["Money"] + "#" +  StaticVar.PlayerAttribute["ActorStatetype"].ToString());
        sw.Close();
    }

    public void Read()
    {
        
        StreamReader sr = new StreamReader(StaticVar.SavePath + "/PlayerInfor.txt");
        string B = sr.ReadToEnd();
        if (B!=null)
        {
            string[] C = Regex.Split(B, "#");
            StaticVar.PlayerAttribute["Money"] = int.Parse(C[0]);
            StaticVar.PlayerAttribute["ActorStatetype"] = (ActorStateType)Enum.Parse(typeof(ActorStateType), C[1]);

        }
        sr.Close();
    }
}
