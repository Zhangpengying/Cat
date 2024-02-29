using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Fungus;

public class MyCG1ContentIO : Singleton<MyCG1ContentIO>
{
    string str = Directory.GetCurrentDirectory();
    public void Write()
    {
        StreamWriter sw = new StreamWriter(Path.Combine(str, @"Assets\MyData\CGContent.txt"));

        sw.Write("#" +VariablesSend.num );
       
        sw.Close();
    }

    public void Read()
    {
        StreamReader sr = new StreamReader(Path.Combine(str, @"Assets\MyData\CGContent.txt"));

        string B = sr.ReadToEnd();
        if (B == "")
        {
            sr.Close();
        }
        else
        {
            string[] C = B.Split('#');
            int D = int.Parse(C[1]);

            MessageSend.instance._cg1content.Add(D);
            sr.Close();
        }
    }
 }


