using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConTest : MonoBehaviour
{
    public float _moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        PlayerControl();
    }
    public void PlayerControl()
    {
        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");

        if (Horizontal > 0)
        {
            //播放动画
            SetAni(new string[] { "RightIdleToWalk","ToRight" });
            //transform.Find("BG").GetComponent<Animator>().SetBool("ToRight", false);

            //面朝右
            transform.Find("BG").GetComponent<SpriteRenderer>().flipX = true;
            //朝右走
            transform.position += new Vector3(Horizontal, 0, 0) * Time.deltaTime * _moveSpeed;
        }
        else if (Horizontal < 0)
        {
            //播放动画
            SetAni(new string[] { "RightIdleToWalk", "ToRight" });
            //transform.Find("BG").GetComponent<Animator>().SetBool("ToRight", false);

            //面朝右
            transform.Find("BG").GetComponent<SpriteRenderer>().flipX = false;
            //朝右走
            transform.position += new Vector3(Horizontal, 0, 0) * Time.deltaTime * _moveSpeed;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                SetAni(new string[] { "RightIdleToAtt" });
            }
            else if (Input.GetKeyUp(KeyCode.X))
            {
                SetAni(new string[] { });
            }
            else
            {
                transform.Find("BG").GetComponent<Animator>().SetBool("RightIdleToWalk", false);
                transform.Find("BG").GetComponent<Animator>().SetBool("RightIdleToAtt", false);
            }
        }

        //垂直方向控制
        if (Vertical > 0)
        {
            //播放动画
            SetAni(new string[] { "BackIdleToWalk", "ToBack" });
            //transform.Find("BG").GetComponent<Animator>().SetBool("ToBack", false);

            transform.position += new Vector3(0, Vertical, 0) * Time.deltaTime * _moveSpeed;
        }
        else if (Vertical < 0)
        {
            //播放动画
            SetAni(new string[] { "FrontIdleToWalk", "ToFront" });
            //transform.Find("BG").GetComponent<Animator>().SetBool("ToFront", false);

            transform.position += new Vector3(0, Vertical, 0) * Time.deltaTime * _moveSpeed;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                if (transform.Find("BG").GetComponent<Animator>().GetBool("RightToBack"))
                {
                    SetAni(new string[] { "BackIdleToAtt", "RightToBack" });
                }
                else if (transform.Find("BG").GetComponent<Animator>().GetBool("RightToFront"))
                {
                    SetAni(new string[] { "FrontIdleToAtt", "RightToFront" });
                }
            }
            else if(Input.GetKeyUp(KeyCode.X))
            {
                if (transform.Find("BG").GetComponent<Animator>().GetBool("RightToBack"))
                {
                    SetAni(new string[] {  "RightToBack" });
                }
                else if (transform.Find("BG").GetComponent<Animator>().GetBool("RightToFront"))
                {
                    SetAni(new string[] {  "RightToFront" });
                }
            }
            else
            {
                //transform.Find("BG").GetComponent<Animator>().SetBool("RightIdleToWalk", false);
                //transform.Find("BG").GetComponent<Animator>().SetBool("RightIdleToAtt", false);
                transform.Find("BG").GetComponent<Animator>().SetBool("BackIdleToWalk", false);
                transform.Find("BG").GetComponent<Animator>().SetBool("BackIdleToAtt", false);
                transform.Find("BG").GetComponent<Animator>().SetBool("FrontIdleToWalk", false);
                transform.Find("BG").GetComponent<Animator>().SetBool("FrontIdleToAtt", false);

            }

        }

    }
    public void SetAni( string[] aniTrue)
    {
        //修正朝向
        transform.Find("BG").GetComponent<Animator>().SetBool("ToBack", false);
        transform.Find("BG").GetComponent<Animator>().SetBool("ToFront", false);
        transform.Find("BG").GetComponent<Animator>().SetBool("ToRight", false);

        transform.Find("BG").GetComponent<Animator>().SetBool("RightIdleToWalk", false);
        transform.Find("BG").GetComponent<Animator>().SetBool("RightIdleToAtt", false);
        transform.Find("BG").GetComponent<Animator>().SetBool("BackIdleToWalk", false);
        transform.Find("BG").GetComponent<Animator>().SetBool("BackIdleToAtt", false);
        transform.Find("BG").GetComponent<Animator>().SetBool("FrontIdleToWalk", false);
        transform.Find("BG").GetComponent<Animator>().SetBool("FrontIdleToAtt", false);

        foreach (var item in aniTrue)
        {
            transform.Find("BG").GetComponent<Animator>().SetBool(item, true);
        }

    }

}
