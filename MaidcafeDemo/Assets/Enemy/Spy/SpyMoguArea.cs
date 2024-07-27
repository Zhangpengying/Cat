/****************************************************
文件：SpyHatArea.cs
作者：Alisk Xu
日期：2023/02/01 11:01:33
功能：Nothing
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpyMoguArea : MonoBehaviour
{
    [SerializeField] private float m_Radius;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.isTrigger)
        {
            return;

        }
        if (collision.CompareTag("PlayerLandArea") && collision.transform.position.y - transform.position.y > 0)
        {
            Debug.Log("Mogu");
            GameObject mogu = this.transform.parent.gameObject; // 获取父结点的gameObject
            //mogu.GetComponent<Mogu>().StartDown(collision);
        }
    }
}

