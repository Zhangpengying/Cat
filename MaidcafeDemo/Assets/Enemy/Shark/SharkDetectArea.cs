/****************************************************
文件：SharkDetectArea.cs
作者：Alisk Xu
日期：2023/02/01 10:27:41
功能：Nothing
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkDetectArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.isTrigger)
        {
            return;
        }

        if (collision.CompareTag("Player"))
        {
            GetComponentInParent<SharkController>().EnemyEnter();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.isTrigger)
        {
            return;
        }

        if (collision.CompareTag("Player"))
        {
            GetComponentInParent<SharkController>().EnemyExit();
        }
    }
}
