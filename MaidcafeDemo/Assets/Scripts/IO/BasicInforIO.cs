using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;

public class BasicInforIO : MonoBehaviour {

    public void Write()
    {
        StreamWriter sw = new StreamWriter(StaticVar.SavePath + "/BasicInfor.txt");
        sw.Write(MessageSend.instance.BasicInfor["CurrentDay"] + "#" + MessageSend.instance.BasicInfor["CurrentWeek"] + "#" + MessageSend.instance.BasicInfor["CurrentTimeFrame"]);
        sw.Close();
    }

    public void Read()
    {

        StreamReader sr = new StreamReader(StaticVar.SavePath + "/BasicInfor.txt");
        string B = sr.ReadToEnd();
        if (B != null)
        {
            string[] C = Regex.Split(B, "#");
            //添加进字典
            MessageSend.instance.BasicInfor.Add("CurrentDay", C[0]);
            MessageSend.instance.BasicInfor.Add("CurrentWeek", C[1]);
            MessageSend.instance.BasicInfor.Add("CurrentTimeFrame", C[2]);
        }
        sr.Close();
    }
}
