﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : ManageBase<PlayerManager>
{
    private Player player ;
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
            ////调整状态
            //player.TransState(player, ActorStateType.Player_Walk);
            //播放动画
            player._ani.SetBool("IsSit", false);
            player._ani.SetBool("IsWalk", true);

            //面朝右
            transform.Find("BG").GetComponent<SpriteRenderer>().flipX = true;
            //朝右走
            
            transform.position += new Vector3(Horizontal, 0, 0) * Time.deltaTime * player._moveSpeed;
            //把和NPC对话的触发框，移到右边去
            transform.GetChild(2).localPosition = new Vector3(0.8f, 0, 0);
            player._isMoving = true;
        }
        else if (Horizontal < 0)
        {
            //播放动画
            player._ani.SetBool("IsSit", false);
            player._ani.SetBool("IsWalk", true);
            //面朝左
            transform.Find("BG").GetComponent<SpriteRenderer>().flipX = false;
            //朝左走
            transform.position += new Vector3(Horizontal, 0, 0) * Time.deltaTime * player._moveSpeed;
            //把和NPC对话的触发框，移到左边去
            transform.GetChild(2).localPosition = new Vector3(-0.8f, 0, 0);
            player._isMoving = true;
        }
        else
        {
            //播放站立动画
            player._ani.SetBool("IsWalk", false);
            player._isMoving = false;
        }

        //垂直方向控制
        if (Vertical > 0)
        {
            ////调整状态
            //player.TransState(player, ActorStateType.Player_Walk);
            //播放动画
            player._ani.SetBool("IsSit", false);
            player._ani.SetBool("IsWalk", true);

            //面朝右
            transform.Find("BG").GetComponent<SpriteRenderer>().flipX = true;
            //朝右走

            transform.position += new Vector3(0, Vertical, 0) * Time.deltaTime * player._moveSpeed;
            //把和NPC对话的触发框，移到右边去
            transform.GetChild(2).localPosition = new Vector3(0, 0.8f, 0);
            player._isMoving = true;
        }
        else if (Vertical < 0)
        {
            //播放动画
            player._ani.SetBool("IsSit", false);
            player._ani.SetBool("IsWalk", true);
            //面朝左
            transform.Find("BG").GetComponent<SpriteRenderer>().flipX = false;
            //朝左走
            transform.position += new Vector3(0, Vertical, 0) * Time.deltaTime * player._moveSpeed;
            //把和NPC对话的触发框，移到左边去
            transform.GetChild(2).localPosition = new Vector3(0, -0.8f, 0);
            player._isMoving = true;
        }
        else
        {
            //播放站立动画
            player._ani.SetBool("IsWalk", false);
            player._isMoving = false;
        }
    }


}
