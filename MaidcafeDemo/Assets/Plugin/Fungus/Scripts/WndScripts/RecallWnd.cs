using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Fungus;

public class RecallWnd :BaseWnd
{
    //public List<GameObject> CGs;
    public void Initialize()
    {
        //CG界面
        Button btnReturn = _transform.Find("返回2").GetComponent<Button>();
        btnReturn.onClick.AddListener(OnReturn);

        Transform content = _transform.Find("Scroll View/Viewport/Content");
        //寻找之前先读取IO
        MessageSend.instance._recallwnd.Clear();
        MyVariablesIO.instance.Read();
        //如果字典内有内容则复原字典内保存的CG的状态
        if (MessageSend.instance._recallwnd.Count != 0)
        {
            //字典内的key值
            List<string> test = new List<string>(MessageSend.instance._recallwnd.Keys);
            for (int i = 0; i < MessageSend.instance._recallwnd.Count; i++)
            {
                var temp = content.Find("CG" + (i + 1));
                temp.Find(test[i]).gameObject.SetActive(MessageSend.instance._recallwnd[test[i]]);
                //temp.tag = "ActCG";
                temp.localPosition = Vector3.zero;
                temp.localScale = Vector3.one;
                temp.Find(test[i]).gameObject.AddComponent<RButtonClickListener>().name = temp.name;
            }
        }
        //GameObject[] CG = GameObject.FindGameObjectsWithTag("CG");
        //while (CGs.Count != CG.Length)
        //{
        //    for (int i = 0; i < CG.Length; i++)
        //    {
        //        if (CG[i].name == "CG" + (i + 1))
        //        {
        //            CGs.Add(CG[i]);
        //        }
        //    }
        //}


        //for (int i = 0; i < CGs.Count; i++)
        //{
        //    CGs[i].transform.localScale = Vector3.one;
        //    CGs[i].transform.localPosition = Vector3.zero;

        //    if (CGs[i].name == "CG" + 1)
        //    {
        //        var cg1 = content.Find("CG1/ActiveCG1");
        //        cg1.gameObject.SetActive(VariablesSend.CG1Active);
        //        //激活后CG的标签改变
        //        CGs[i].tag = "ActCG";
        //        //将CG的状态存入IO
        //        if (cg1.gameObject.activeSelf)
        //        {
        //            Add(cg1);
        //            MyVariablesIO.instance.Write();
        //            if (VariablesSend.CG1Active)
        //            {
        //                content.Find("CG1/ActiveCG1").GetComponent<Button>().onClick.AddListener(OnOpenCG1);
        //            }
        //        }
        //    }
        //    if (CGs[i].name == "CG" + 2)
        //    {
        //        var cg2 = content.Find("CG2/ActiveCG2");
        //        cg2.gameObject.SetActive(VariablesSend.CG2Active);
        //        //激活后CG的标签改变
        //        CGs[i].tag = "ActCG";
        //        //将CG的状态存入IO
        //        if (cg2.gameObject.activeSelf)
        //        {
        //            Add(cg2);
        //            MyVariablesIO.instance.Write();
        //            if (VariablesSend.CG2Active)
        //            {
        //                content.Find("CG2/ActiveCG2").GetComponent<Button>().onClick.AddListener(OnOpenCG2);
        //            }
        //        }
        //    }
        //    //if (CG[i].name == "CG" + 3)
        //    //{
        //    //    CG[i].GetComponent<Button>().onClick.AddListener(OnOpenCG3);
        //    //}
        //    //if (CG[i].name == "CG" + 4)
        //    //{
        //    //    CG[i].GetComponent<Button>().onClick.AddListener(OnOpenCG4);
        //    //}
        //    //if (CG[i].name == "CG" + 5)
        //    //{
        //    //    CG[i].GetComponent<Button>().onClick.AddListener(OnOpenCG5);
        //    //}
        //}

        //音乐界面
        Button btnBGM1 = _transform.Find("BGMEnjoy/BGM1").GetComponent<Button>();
        var aaa = FungusManager.Instance;
        if (BGMContro._instance.myaudio.isPlaying)
        {
            if (aaa.GetComponent<AudioSource>().clip == BGMContro._instance._clips[0])
            {
                _transform.Find("BGMEnjoy/BGM1/Toggle").GetComponent<Toggle>().isOn = true;
            }
            if (aaa.GetComponent<AudioSource>().clip == BGMContro._instance._clips[1])
            {
                _transform.Find("BGMEnjoy/BGM2/Toggle").GetComponent<Toggle>().isOn = true;
            }
            if (aaa.GetComponent<AudioSource>().clip == BGMContro._instance._clips[2])
            {
                _transform.Find("BGMEnjoy/BGM3/Toggle").GetComponent<Toggle>().isOn = true;
            }
        }
    }
    
    private void OnReturn()
    {
        WindowManager.instance.Close<RecallWnd>();
        WindowManager.instance.Open<GameOverWnd>();
        WindowManager.instance.obj1.GetComponent<CanvasGroup>().alpha = 1;
        //WindowManager.instance.Open<MenuWnd>().Initialize();
    }
    private void OnOpenCG1()
    {
        WindowManager.instance.Close<RecallWnd>();
        WindowManager.instance.Open<CG1>().Initialize();
    }
    private void OnOpenCG2()
    {
        WindowManager.instance.Close<RecallWnd>();
        WindowManager.instance.Open<CG2>().Initialize();
    }
    private void OnOpenCG3()
    {
        WindowManager.instance.Close<RecallWnd>();
        WindowManager.instance.Open<CG3>().Initialize();
    }
    private void OnOpenCG4()
    {
        WindowManager.instance.Close<RecallWnd>();
        WindowManager.instance.Open<CG4>().Initialize();
    }
    private void OnOpenCG5()
    {
        WindowManager.instance.Close<RecallWnd>();
        WindowManager.instance.Open<CG5>().Initialize();
    }

    public static void Add(Transform cg)
    {
        MessageSend.instance.CGMessage(cg.name, true);
    }
  
}



