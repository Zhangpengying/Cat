using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Fungus;

public class SaveWnd : BaseWnd
{
  
    public List<GameObject> backgrounds = new List<GameObject>();
    public static Sprite sp;
    public static Transform trans;
    public static int q = 0;
    public int[] Numbers = { 1, 2, 3, 4, 5, 6, 7, 8 };
    //Create执行的次数( 静态变量，一旦改变全局改变 )
    public static  int num = 0;

    public Transform temp;
    public void Initialize()
    {       
        Button btnReturn = _transform.Find("返回4").GetComponent<Button>();
        btnReturn.onClick.AddListener(OnReturn);
        trans = GameObject.Find("UI/Canvas/SaveWnd").transform;
        //默认把预制体的图片设定为当时截图
        temp =trans.Find("Scroll View/Viewport/BtnSavePoint/Im/Image");
        temp.GetComponent<Image>().sprite
         = Resources.Load<Sprite>("Sprites/Image" + (q + 1));

        //temp.GetComponent<Image>().sprite
        //= sp;

       // Debug.Log(trans.Find("Scroll View/Viewport/BtnSavePoint/Image").GetComponent<Image>().sprite.name);
        CreateAndSet();
    }

    private void OnReturn()
    {
        //GameObject.Find("UI/Canvas/SaveWnd").SetActive(false);
        WindowManager.instance.Close<SaveWnd>();
        WindowManager.instance.Open<MenuWnd>().Initialize();
    }

    //建立父子关系
    public void SetParent(Transform son, Transform parent)
    {
        son.SetParent(parent);
        parent.name = parent.name + "Have";
        son.gameObject.SetActive(true);
        //son.tag = "Point";
        son.localScale = Vector3.one ;
        son.localPosition = Vector3.zero;
    }

    //总体整合
    public void CreateAndSet()
    {
       
        MessageSend.instance._savewnd.Clear();
        MyParentIO.instance.Read();

        //背景框

        for (int i = 0; i < Numbers.Length; i++)
        {
            if (GameObject.Find("UI/Canvas/SaveWnd/Scroll View/Viewport/Content/Button" + (i + 1) + "s") != null)
            {
                string a = (GameObject.Find("UI/Canvas/SaveWnd/Scroll View/Viewport/Content/Button" + (i + 1) + "s").name);
                backgrounds.Add(GameObject.Find("UI/Canvas/SaveWnd/Scroll View/Viewport/Content/Button" + (i + 1) + "s"));
            }
            if (GameObject.Find("UI/Canvas/SaveWnd/Scroll View/Viewport/Content/Button" + (i + 1) + "sHave") != null)
            {
                backgrounds.Add(GameObject.Find("UI/Canvas/SaveWnd/Scroll View/Viewport/Content/Button" + (i + 1) + "sHave"));
            }
            //backgrounds.Add(GameObject.Find("UI/Canvas/SaveWnd/Scroll View/Viewport/Content/Button" + (i+1) + "s"));
            
        }
        //backgrounds = GameObject.FindGameObjectsWithTag("EmptySaveWndBtn");
        //不为空即为有保存历史，先复原保存历史，剩下的添加按钮监听事件
        if (MessageSend.instance._savewnd.Count != 0)
        {
            //字典的key值
            List<string> test = new List<string>(MessageSend.instance._savewnd.Keys);

            for (int i = 0; i < MessageSend.instance._savewnd.Count; i++)
            {
                //创建保存点
                Button btnsave = trans.Find("Scroll View/Viewport/BtnSavePoint").GetComponent<Button>();
                Transform btnSavePoint = (Object.Instantiate(btnsave.gameObject) as GameObject).transform;
                
              
                if (num ==0)
                {
                    num += MessageSend.instance._savewnd.Count;
                }
                //保存点名字
                btnSavePoint.name = test[i];
            
                //历史保存点的图片信息恢复
                MessageSend.instance._image.Clear();
                MyImageIO.instance.Read();
                //ImageMessage(btnSavePoint);

                //复原保存历史
                for (int j = 0; j < backgrounds.Count; j++)
                {
                    if (backgrounds[j].name + "Have"== MessageSend.instance._savewnd[test[i]])
                    {
                        SetParent(btnSavePoint, backgrounds[j].transform);
                        backgrounds[j].transform.Find("Text").GetComponent<Text>().text = "";
                        for (int m = 0; m < Numbers.Length; m++)
                        {
                            if ("Button"+ Numbers[m] + "sHave" == backgrounds[j].name)
                            {
                                //历史保存里的文字信息恢复
                                TextMessage(btnSavePoint, i, Numbers[m]);
                            }
                        }
                    }
                }
            }
            backgrounds.Clear();
            for (int i = 0; i < Numbers.Length; i++)
            {
                if (GameObject.Find("UI/Canvas/SaveWnd/Scroll View/Viewport/Content/Button" + (i + 1) + "s") != null)
                {
                    backgrounds.Add(GameObject.Find("UI/Canvas/SaveWnd/Scroll View/Viewport/Content/Button" + (i + 1) + "s"));
                }
            }
          
            //backgrounds = GameObject.FindGameObjectsWithTag("EmptySaveWndBtn");
            //剩下的添加监听
            for (int k = 0; k < backgrounds.Count; k++)
            {

                backgrounds[k].GetComponent<Button>();
                backgrounds[k].AddComponent<SButtonClickListener>().A = trans;
                //backgrounds[k].GetComponent<SButtonClickListener>().m = num;

            }
        }
        //为空则无保存历史，全部按钮添加监听
        else
        {
            //backgrounds = GameObject.FindGameObjectsWithTag("EmptySaveWndBtn");
            for (int j = 0; j < backgrounds.Count; j++)
            {

                //点击设置父节点
                backgrounds[j].GetComponent<Button>();
                backgrounds[j].AddComponent<SButtonClickListener>().A = trans;
                //backgrounds[j].GetComponent<SButtonClickListener>().m = num;
            }
        }
    }

    //存档信息
    public static void TextMessage(Transform btnSavePoint,int n,int b)
    {
        SavePoint.SavepointName.Clear();
        MyBlockNameIO.instance.Read();

        MessageSend.instance.temp.Clear();
        MySaveNumIO.instance.Read();

        MyMenu.CurrentsaveDataKeys().Clear();
        MySaveIO.instance.Read();

        MessageSend.instance.time.Clear();
        MySaveTimeIO.instance.Read();

        Text t = btnSavePoint.Find("Num").GetComponent<Text>();
        //保存点的文字信息
        t.text = SavePoint.SavepointName[MessageSend.instance.temp[n] - 1] +"\n"
            + MessageSend.instance.time[n];
        //保存点的编号
        btnSavePoint.Find("Text").GetComponent<Text>().text =b.ToString();

    }
    //存档图片信息
    public static void ImageMessage(Transform btnsavepoint)
    {
        List<string> keyPoi = new List<string>(MessageSend.instance._image.Keys);
        //将已经赋值过的变量传递给指定物体
        for (int i = 0; i < keyPoi.Count; i++)
        {
            if (btnsavepoint.name == keyPoi[i])
            {
                string con = "Sprites/" + MessageSend.instance._image[keyPoi[i]];
                btnsavepoint.Find("Im/Image").gameObject.GetComponent<Image>().sprite
                    = Resources.Load<Sprite>(con);
            }
        }
    }
}

public class SButtonClickListener : MonoBehaviour, IPointerClickHandler
{
    public  Transform A;
    public static string _saveDataKey;
    public int[] Numbers = { 1, 2, 3, 4, 5, 6, 7, 8 };
    public static void AddSave(string saveDataKey)
    {
        _saveDataKey = saveDataKey;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        //添加进列表并存入本地
        MyMenu. CurrentsaveDataKeys().Add(_saveDataKey);
        MySaveIO.instance.Write();

        //截图存入本地文件

        //将图片的编号和与之对应的保存点之间的对应关系保存进本地

        //创建并设置父节点
        Button C = A.Find("Scroll View/Viewport/BtnSavePoint").GetComponent<Button>();
        Transform D = (Object.Instantiate(C.gameObject) as GameObject).transform;
    
        SaveWnd.num++;

       
            D.name= "Button"+ SaveWnd.num;
           
            D.SetParent(transform);
            transform.Find("Text").GetComponent<Text>().text = "";
            transform.name = transform.name + "Have";
            //将父子关系添加到字典中
            MessageSend.instance.SaveMessage(D.name, transform.name);
            //将字典内的信息存入本地
            MyParentIO.instance.Write();

            //将当前的保存点个数存入列表
            MessageSend.instance.temp.Add(FungusManager.Instance.SaveManager.NumSavePoints);
            //将列表信息存入本地
            MySaveNumIO.instance.Write();

            //把当前时间写入本地
            System.DateTime dt = System.DateTime.Now;
            MessageSend.instance.time.Add(dt.AddMonths(1).ToString());
            //System.DateTime.Now.ToString("HH:mm dd MMMM, yyyy")
            //2005-12-5 13:47:04
            MySaveTimeIO.instance.Write();

            D.gameObject.SetActive(true);
            //D.tag = "Point";
            D.localScale = Vector3.one;
            D.localPosition = Vector3.zero;

            //保存点的时间和名字信息
            for (int m = 0; m < Numbers.Length; m++)
            {
                if ("Button" + Numbers[m] + "sHave" == transform.name)
                {
                    SaveWnd.TextMessage(D, MyMenu.CurrentsaveDataKeys().Count - 1, Numbers[m]);
                }
            }
            //图片信息写入IO
            MessageSend.instance.ImageMessage(D.name, D.Find("Im/Image").GetComponent<Image>().sprite.name);
            MyImageIO.instance.Write();
        
    }
}

