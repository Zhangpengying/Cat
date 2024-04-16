using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class MessageSend : Singleton<MessageSend>  
{
    //save窗口的消息传递
    //保存点与背景框之间的父子关系
    public Dictionary<string, string> _savewnd = new Dictionary<string, string>();
    
    public void  SaveMessage(string son,string parent)
    {
        _savewnd.Add(son, parent);
    }

    //截图信息传递
    public Dictionary<string, string> _image = new Dictionary<string, string>();

    public void ImageMessage(string poi, string img)
    {
        _image.Add(poi, img);
    }

    //保存点在savepoints里的编号
    public List<int> temp = new List<int>();

    //保存时的时间
    public List<string> time = new List<string>();

    //Load窗口的消息传递
    public Dictionary<string, Transform> _loadwnd = new Dictionary<string, Transform>();


    //鉴赏窗口的消息传递

    //全体CG的状态
    public Dictionary<string, bool> _recallwnd = new Dictionary<string, bool>();
    public void CGMessage(string CGName, bool bl)
    {
        _recallwnd.Add(CGName, bl);
    }
    public List<int> CGActNum = new List<int>();
    //CG1内容的激活状态
    public List<int> _cg1content = new List<int>();
    public void CG1Content(int num)
    {
        _cg1content.Add(num);
    }


}
