using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MessageManager : Singleton<MessageManager>
{
    public Dictionary<Actor, Transform> whoSay = new Dictionary<Actor, Transform>();

    /// <summary>
    /// 根据说话角色创建消息弹框
    /// </summary>
    /// <param name="act"></param>说话的角色
    public void CreatMessageBox(Actor act)
    {
        GameObject go = GameObject.Instantiate(Resources.Load("Prefabs/UIWnd/Say")) as GameObject;
        go.name = act.name;
        go.transform.SetParent(GameObject.Find("UI/Canvas/JingYing/SayList").transform);
        //位置改变
        go.GetComponent<SayFollow>().target = act.transform;
        go.transform.Find("Text").GetComponent<Text>().text = StaticVar.say4;
        go.transform.localScale = Vector3.one;
        go.SetActive(true);
        go.GetComponent<SayFollow>().UpdatePosition();
        whoSay.Add(act, go.transform);

    }
	/// <summary>
    /// 销毁某个角色对应的对话框
    /// </summary>
    public void DestoryMessageBox(Actor act)
    {
        Object.Destroy(whoSay[act].gameObject);
        whoSay.Remove(act);
    }


}
