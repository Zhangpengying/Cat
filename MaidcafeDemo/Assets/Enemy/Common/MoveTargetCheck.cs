using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTargetCheck : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.isTrigger)
        {
            return;
        }

        if (collision.CompareTag("Player"))
        {
            //etComponentInParent<MoveEnemy>().EnemyEnter();
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
            //GetComponentInParent<MoveEnemy>().EnemyExit();
        }
    }
}
