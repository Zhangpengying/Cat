using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyScrollBar : MonoBehaviour {

    public ArrayList ScrollList = new ArrayList();
    public Vector3 StartPos;
    public Vector3 EndPos;
    private int Step;
    private Vector3 Space;
    private Transform Handle;
    private Transform ScrollBar;

    public Transform Content;
    
    //当前步数
    public int currentStep = 0;
    public void Start()
    {
       
    }

    public void Initialize()
    {
        ScrollBar = transform.Find("Scrollbar");
        Handle = transform.Find("Scrollbar/Handle");
        Handle.localPosition = StartPos;
        currentStep = 0;
    }

    // Update is called once per frame
    void Update ()
    {
        //当前列表的长度是否大于显示长度
        if (ScrollList.Count > 5 )
        {
            //界定滑动的步数和每一步的长度
            Step = ScrollList.Count;
            Space =(EndPos-StartPos)/Step ;
        }
        //当前列表长度小于显示长度
        else
        {
            gameObject.SetActive(false);
        }

        if (ScrollBar.gameObject.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (currentStep>0)
                {
                    currentStep -= 1;
                    Handle.localPosition = StartPos + Space * currentStep;

                }
            }
            else if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (currentStep< ScrollList.Count)
                {
                    currentStep += 1;
                    Handle.localPosition = StartPos + Space * currentStep;
                }
            }

        }
        
	}


}
