using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConTest1 : MonoBehaviour
{
    public float _moveSpeed;
    public Transform _ani;
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
            if (!_ani.GetComponent<Animator>().GetBool("RightIdleToWalk"))
            {
                SetAni(new string[] { "RightIdleToWalk", "BackToRight","FrontToRight" });
                //面朝右
                _ani.GetComponent<SpriteRenderer>().flipX = true;
            }
            //朝右走
            transform.position += new Vector3(Horizontal, 0, 0) * Time.deltaTime * _moveSpeed;
        }
        else if (Horizontal < 0)
        {
            //播放动画
            if (!_ani.GetComponent<Animator>().GetBool("RightIdleToWalk"))
            {
                SetAni(new string[] { "RightIdleToWalk", "BackToRight", "FrontToRight" });
                //面朝右
                _ani.GetComponent<SpriteRenderer>().flipX = false;
            }
              
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
                _ani.GetComponent<Animator>().SetBool("RightIdleToWalk", false);
                _ani.GetComponent<Animator>().SetBool("RightIdleToAtt", false);
            }
        }

        //垂直方向控制
        if (Vertical > 0)
        {
            //播放动画
            if (!_ani.GetComponent<Animator>().GetBool("BackIdleToWalk"))
            {
                SetAni(new string[] { "BackIdleToWalk",  "FrontToBack", "RightToBack" });
            }

            transform.position += new Vector3(0, Vertical, 0) * Time.deltaTime * _moveSpeed;
        }
        else if (Vertical < 0)
        {
            //播放动画
            if (!_ani.GetComponent<Animator>().GetBool("FrontIdleToWalk"))
            {
                SetAni(new string[] { "FrontIdleToWalk", "BackToFront", "RightToFront" });
            }

            transform.position += new Vector3(0, Vertical, 0) * Time.deltaTime * _moveSpeed;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                if (_ani.GetComponent<Animator>().GetBool("RightToBack"))
                {
                    SetAni(new string[] { "BackIdleToAtt", "RightToBack" });
                }
                else if (_ani.GetComponent<Animator>().GetBool("RightToFront"))
                {
                    SetAni(new string[] { "FrontIdleToAtt", "RightToFront" });
                }
            }
            else if (Input.GetKeyUp(KeyCode.X))
            {
                if (_ani.GetComponent<Animator>().GetBool("RightToBack"))
                {
                    SetAni(new string[] { "RightToBack" });
                }
                else if (_ani.GetComponent<Animator>().GetBool("RightToFront"))
                {
                    SetAni(new string[] { "RightToFront" });
                }
            }
            else
            {
                //_ani.GetComponent<Animator>().SetBool("RightIdleToWalk", false);
                //_ani.GetComponent<Animator>().SetBool("RightIdleToAtt", false);
                _ani.GetComponent<Animator>().SetBool("BackIdleToWalk", false);
                _ani.GetComponent<Animator>().SetBool("BackIdleToAtt", false);
                _ani.GetComponent<Animator>().SetBool("FrontIdleToWalk", false);
                _ani.GetComponent<Animator>().SetBool("FrontIdleToAtt", false);

            }

        }

    }
    public void SetAni(string[] aniTrue)
    {
        //修正朝向
        _ani.GetComponent<Animator>().SetBool("RightToBack", false);
        _ani.GetComponent<Animator>().SetBool("RightToFront", false);
        _ani.GetComponent<Animator>().SetBool("BackToRight", false);
        _ani.GetComponent<Animator>().SetBool("FrontToBack", false);
        _ani.GetComponent<Animator>().SetBool("BackToFront", false);
        _ani.GetComponent<Animator>().SetBool("FrontToRight", false);

        _ani.GetComponent<Animator>().SetBool("RightIdleToWalk", false);
        _ani.GetComponent<Animator>().SetBool("RightIdleToAtt", false);
        _ani.GetComponent<Animator>().SetBool("BackIdleToWalk", false);
        _ani.GetComponent<Animator>().SetBool("BackIdleToAtt", false);
        _ani.GetComponent<Animator>().SetBool("FrontIdleToWalk", false);
        _ani.GetComponent<Animator>().SetBool("FrontIdleToAtt", false);

        foreach (var item in aniTrue)
        {
            _ani.GetComponent<Animator>().SetBool(item, true);
        }

    }

}
