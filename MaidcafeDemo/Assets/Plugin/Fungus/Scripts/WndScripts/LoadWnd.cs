﻿using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LoadWnd : BaseWnd
{
    public Transform transtemp1;
    public Transform transtemp2;
    protected static SaveHistory saveHistory = new SaveHistory();
    public int[] Numbers = { 1, 2, 3, 4, 5, 6, 7, 8 };
    public List<GameObject> backgroundL = new List<GameObject>();
    public void Initialize()
    {
        Button btnReturn = _transform.Find("返回1").GetComponent<Button>();
        btnReturn.onClick.AddListener(OnReturn);

        //找到所有的背景底框
        for (int i = 0; i < Numbers.Length; i++)
        {
            backgroundL.Add(GameObject.Find("UI/Canvas/LoadWnd/Scroll View/Viewport/Content/Button" + (i + 1) + "s"));
        }
        
        Transform content = _transform.Find("Scroll View/Viewport/Content");
        Button btnLoad = _transform.Find("Scroll View/Viewport/BtnLoadPoint").GetComponent<Button>();

        transtemp1 = GameObject.Find("UI/Canvas").transform;
        transtemp2 = transtemp1.Find("SaveWnd/Scroll View/Viewport/Content");

        //保存点的IO读取
        MyMenu.CurrentsaveDataKeys().Clear();
        MySaveIO.instance.Read();

        //保存点父子关系的IO读取
        MessageSend.instance._savewnd.Clear();
        MyParentIO.instance.Read();

        for (int i = 0; i < MyMenu.CurrentsaveDataKeys().Count; i++)
        {
            //创建加载点
            Transform btnLoadPoint = (Object.Instantiate(btnLoad.gameObject) as GameObject).transform;
            btnLoadPoint.name = "Button" + (i + 1);

            //为加载点设置父物体
            for (int j = 0; j < backgroundL.Count; j++)
            {
                if (backgroundL[j].name + "Have" == MessageSend.instance._savewnd[btnLoadPoint.name])
                {
                    backgroundL[j].GetComponent<Button>();
                    btnLoadPoint.SetParent(backgroundL[j].transform);
                    for (int m = 0; m < Numbers.Length; m++)
                    {
                        //if ("Button" + Numbers[m] + "sL" == backgroundL[j].name)
                        //{
                        //    btnLoadPoint.Find("Text").GetComponent<Text>().text = Numbers[m].ToString();
                        //}
                        btnLoadPoint.Find("Text").GetComponent<Text>().text = Numbers[m].ToString();
                    }
                    backgroundL[j].transform.Find("Text").GetComponent<Text>().text = "";
                }
            }

            //btnLoadPoint.tag = "PointL";
            btnLoadPoint.gameObject.SetActive(true);
            btnLoadPoint.localScale = Vector3.one;
            btnLoadPoint.localPosition = Vector3.zero;

            // 存档信息
            Text num = btnLoadPoint.Find("Num").GetComponent<Text>();

            SavePoint.SavepointName.Clear();
            MyBlockNameIO.instance.Read();
            MessageSend.instance.temp.Clear();
            MySaveNumIO.instance.Read();
            MessageSend.instance.time.Clear();
            MySaveTimeIO.instance.Read();
            //保存点的文字信息
            num.text = SavePoint.SavepointName[MessageSend.instance.temp[i] - 1] + "\n" +
              MessageSend.instance.time[i];

            //保存点的图片信息
            MessageSend.instance._image.Clear();
            MyImageIO.instance.Read();
            ImageMessage(btnLoadPoint);

            btnLoadPoint.gameObject.AddComponent<ButtonClickListener>()._saveDataKey = MyMenu.CurrentsaveDataKeys()[i];     
        }
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


    private void OnReturn()
    {
        WindowManager.instance.Close<LoadWnd>();
        WindowManager.instance.Open<MenuWnd>().Initialize();
    }
}
public class ButtonClickListener : MonoBehaviour, IPointerClickHandler
{
    public  string _saveDataKey;
    public void OnPointerClick(PointerEventData eventData)
    {
        WindowManager.instance.Close<LoadWnd>();

       
        if (GameObject.Find("UI/Canvas/LoadWnd(Clone)") == null)
        {
            WindowManager.instance.Close<LoadWnd>();
            WindowManager.instance.Open<MenuWnd>().Initialize();
        }
        else
        {
            Object.Destroy(GameObject.Find("UI/Canvas/LoadWnd(Clone)"));
            WindowManager.instance.Open<MenuWnd>().Initialize();
        }

        var saveManager = FungusManager.Instance.SaveManager;
        if (saveManager.SaveDataExists(_saveDataKey))
        {
            saveManager.Load(_saveDataKey);
           
        }
       
    }
}
