using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Fungus;

public class SRecallWnd :MonoBehaviour
{
    public Transform _trans;
    public bool switch1 = false;
    public int num = 1;
    public List<GameObject> CGs = new List<GameObject>();
    public GameObject obj;

    public  void Create()
    {
        Transform _canvas = GameObject.Find("UI/Canvas").transform;
        _trans = (GameObject.Instantiate(Resources.Load("Prefabs/Wnd/RecallWnd")) as GameObject).transform;
        _trans.SetParent(_canvas);
        _trans.name = "RecallWnd";
        _trans.localPosition = Vector3.zero;
        _trans.localScale = Vector3.one;
        Initialize();
    }

    public void Initialize()
    {
        //CG界面
        Button btnReturn = _trans.Find("返回2").GetComponent<Button>();
        btnReturn.onClick.AddListener(OnReturn);

        Transform content = _trans.Find("Scroll View/Viewport/Content");
        #region
        //for (int j = 1; j < 9; j++)
        //{
        //    CG.Add(content.Find("CG" + j));
        //}
        //for (int k = 0; k < CG.Count; k++)
        //{
        //    if (CG[k].tag != "CG")
        //    {
        //        CG.Remove(CG[k]);
        //    }
        //}
        //MessageSend.instance.CGActNum.Clear();
        //MyActCGNumIO.instance.Read();
        //if (MessageSend.instance.CGActNum.Count != 0)
        //{
        //    for (int i = 0; i < MessageSend.instance.CGActNum[0]; i++)
        //    {

        //        CG[i].transform.localScale = Vector3.one;
        //        CG[i].transform.localPosition = Vector3.zero;


        //        var cg = CG[i].Find("ActiveCG" + (i + 1));
        //        cg.gameObject.SetActive(true);
        //        //激活后CG的标签改变
        //        CG[i].tag = "ActCG";
        //        //将CG的状态存入IO

        //        RecallWnd.Add(cg);
        //        MyVariablesIO.instance.Write();
        //        CG[i].Find("ActiveCG" + (i + 1)).gameObject.AddComponent<RButtonClickListener>().name = CG[i].name;

        //    }
        //}

        #endregion
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
        else
        {
            for (int k = 1; k < 3; k++)
            {
                CGs.Add(GameObject.Find("UI/Canvas/RecallWnd/Scroll View/Viewport/Content/CG" + k));
            }
            for (int i = 0; i < CGs.Count; i++)
            {

                CGs[i].transform.localScale = Vector3.one;
                CGs[i].transform.localPosition = Vector3.zero;

                if (CGs[i].name == "CG" + 1)
                {
                    var cg1 = content.Find("CG1/ActiveCG1");
                    cg1.gameObject.SetActive(VariablesSend.CG1Active);
                    //将CG的状态存入IO
                    if (cg1.gameObject.activeSelf)
                    {
                        //激活后CG的标签改变
                        //CGs[i].tag = "ActCG";
                        RecallWnd.Add(cg1);
                        MyVariablesIO.instance.Write();
                        if (VariablesSend.CG1Active)
                        {
                            content.Find("CG1/ActiveCG1").gameObject.AddComponent<RButtonClickListener>().name = CGs[i].name;
                        }
                    }


                }
                if (CGs[i].name == "CG" + 2)
                {
                    var cg2 = content.Find("CG2/ActiveCG2");
                    cg2.gameObject.SetActive(VariablesSend.CG2Active);
                    if (cg2.gameObject.activeSelf)
                    {
                        //激活后CG的标签改变
                        //CGs[i].tag = "ActCG";
                        //将CG的状态存入IO
                        RecallWnd.Add(cg2);
                        MyVariablesIO.instance.Write();

                        if (VariablesSend.CG2Active)
                        {
                            content.Find("CG2/ActiveCG2").GetComponent<Button>().onClick.AddListener(OnOpenCG2);
                        }
                    }
                }
            }
        
            //if (CG[i].name == "CG" + 3)
            //{
            //    CG[i].GetComponent<Button>().onClick.AddListener(OnOpenCG3);
            //}
            //if (CG[i].name == "CG" + 4)
            //{
            //    CG[i].GetComponent<Button>().onClick.AddListener(OnOpenCG4);
            //}
            //if (CG[i].name == "CG" + 5)
            //{
            //    CG[i].GetComponent<Button>().onClick.AddListener(OnOpenCG5);
            //}
        }

        //音乐界面
        Button btnBGM1 = _trans.Find("BGMEnjoy/BGM1").GetComponent<Button>();
        var aaa = FungusManager.Instance;
        if (BGMContro._instance.myaudio.isPlaying)
        {
            if (aaa.GetComponent<AudioSource>().clip == BGMContro._instance._clips[0])
            {
                _trans.Find("BGMEnjoy/BGM1/Toggle").GetComponent<Toggle>().isOn = true;
            }
            if (aaa.GetComponent<AudioSource>().clip == BGMContro._instance._clips[1])
            {
                _trans.Find("BGMEnjoy/BGM2/Toggle").GetComponent<Toggle>().isOn = true;
            }
            if (aaa.GetComponent<AudioSource>().clip == BGMContro._instance._clips[2])
            {
                _trans.Find("BGMEnjoy/BGM3/Toggle").GetComponent<Toggle>().isOn = true;
            }
        }

        WindowManager.instance.Close<GameOverWnd>();
    }
    
    #region
    private void OnReturn()
    {
        Destroy(GameObject.Find("UI/Canvas/RecallWnd"));
        WindowManager.instance.Open<GameOverWnd>();
        obj.GetComponent<CanvasGroup>().alpha = 1;
    }

    private void OnOpenCG1()
    {
        Destroy(GameObject.Find("UI/Canvas/RecallWnd"));
        WindowManager.instance.Open<CG1>().Initialize();
    }

    private void OnOpenCG2()
    {
        Destroy(GameObject.Find("UI/Canvas/RecallWnd(Clone)"));
        WindowManager.instance.Open<CG2>().Initialize();
    }

    private void OnOpenCG3()
    {
        Destroy(GameObject.Find("UI/Canvas/RecallWnd"));
        WindowManager.instance.Open<CG3>().Initialize();
    }
    private void OnOpenCG4()
    {
        Destroy(GameObject.Find("UI/Canvas/RecallWnd"));
        WindowManager.instance.Open<CG4>().Initialize();
    }
    private void OnOpenCG5()
    {
        Destroy(GameObject.Find("UI/Canvas/RecallWnd"));
        WindowManager.instance.Open<CG5>().Initialize();
    }
    #endregion
    //开关
    private void OnBGM1()
    {
        //SendMessage()
    }

    private void OnButton2()
    {
        switch1 = !switch1;
    }


}

public class RButtonClickListener : MonoBehaviour, IPointerClickHandler
{

    public new string  name;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (name== "CG1")
        {

            Destroy(GameObject.Find("UI/Canvas/RecallWnd"));
            WindowManager.instance.Close<RecallWnd>();
            WindowManager.instance.Open<CG1>().Initialize();
        }
        else
        {
            Destroy(GameObject.Find("UI/Canvas/RecallWnd"));
            WindowManager.instance.Close<RecallWnd>();
            WindowManager.instance.Open<CG2>().Initialize();
        }
    }
}

