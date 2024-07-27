using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : ManageBase<PlayerManager>
{
    private Player player ;
    public float _moveSpeed;

    private void Start()
    {
        player = GetComponent<Player>();
       
    }
    private void Update()
    {
        if (!player.IsLockPlayer)
        {
            PlayerControl();
        }
        else
        {
            StopAni();
        }
        
    }
    protected override byte SetMessageType()
    {
        return MyMessageType.Type_CharAttribute;
        
    }


    //人物移动控制
    public void PlayerControl()
    {
        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");

        if (Horizontal > 0)
        {
            //播放动画
            if (!player._ani.GetBool("RightIdleToWalk"))
            {
                SetAni(new string[] { "RightIdleToWalk", "BackToRight", "FrontToRight" });
                //面朝右
                player._ani.GetComponent<SpriteRenderer>().flipX = true;
            }
            //朝右走
            transform.position += new Vector3(Horizontal, 0, 0) * Time.deltaTime * _moveSpeed;
        }
        else if (Horizontal < 0)
        {
            //播放动画
            if (!player._ani.GetBool("RightIdleToWalk"))
            {
                SetAni(new string[] { "RightIdleToWalk", "BackToRight", "FrontToRight" });
                //面朝右
                player._ani.GetComponent<SpriteRenderer>().flipX = false;
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
                player._ani.SetBool("RightIdleToWalk", false);
                player._ani.SetBool("RightIdleToAtt", false);
            }
        }

        //垂直方向控制
        if (Vertical > 0)
        {
            //播放动画
            if (!player._ani.GetBool("BackIdleToWalk"))
            {
                SetAni(new string[] { "BackIdleToWalk", "FrontToBack", "RightToBack" });
            }

            transform.position += new Vector3(0, Vertical, 0) * Time.deltaTime * _moveSpeed;
        }
        else if (Vertical < 0)
        {
            //播放动画
            if (!player._ani.GetBool("FrontIdleToWalk"))
            {
                SetAni(new string[] { "FrontIdleToWalk", "BackToFront", "RightToFront" });
            }

            transform.position += new Vector3(0, Vertical, 0) * Time.deltaTime * _moveSpeed;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                if (player._ani.GetBool("RightToBack"))
                {
                    SetAni(new string[] { "BackIdleToAtt", "RightToBack" });
                }
                else if (player._ani.GetBool("RightToFront"))
                {
                    SetAni(new string[] { "FrontIdleToAtt", "RightToFront" });
                }
            }
            else if (Input.GetKeyUp(KeyCode.X))
            {
                if (player._ani.GetBool("RightToBack"))
                {
                    SetAni(new string[] { "RightToBack" });
                }
                else if (player._ani.GetBool("RightToFront"))
                {
                    SetAni(new string[] { "RightToFront" });
                }
            }
            else
            {
                player._ani.SetBool("BackIdleToWalk", false);
                player._ani.SetBool("BackIdleToAtt", false);
                player._ani.SetBool("FrontIdleToWalk", false);
                player._ani.SetBool("FrontIdleToAtt", false);
            }
        }

    }
    public void SetAni(string[] aniTrue)
    {
        //修正朝向
        player._ani.SetBool("RightToBack", false);
        player._ani.SetBool("RightToFront", false);
        player._ani.SetBool("BackToRight", false);
        player._ani.SetBool("FrontToBack", false);
        player._ani.SetBool("BackToFront", false);
        player._ani.SetBool("FrontToRight", false);

        player._ani.SetBool("RightIdleToWalk", false);
        player._ani.SetBool("RightIdleToAtt", false);
        player._ani.SetBool("BackIdleToWalk", false);
        player._ani.SetBool("BackIdleToAtt", false);
        player._ani.SetBool("FrontIdleToWalk", false);
        player._ani.SetBool("FrontIdleToAtt", false);

        foreach (var item in aniTrue)
        {
            player._ani.SetBool(item, true);
        }
        

    }
    public void StopAni()
    {
        player._ani.SetBool("RightIdleToWalk", false);
        player._ani.SetBool("RightIdleToAtt", false);
        player._ani.SetBool("BackIdleToWalk", false);
        player._ani.SetBool("BackIdleToAtt", false);
        player._ani.SetBool("FrontIdleToWalk", false);
        player._ani.SetBool("FrontIdleToAtt", false);
    }



}
