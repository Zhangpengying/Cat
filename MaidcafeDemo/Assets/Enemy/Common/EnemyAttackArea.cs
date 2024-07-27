

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyAttackArea : MonoBehaviour
{
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.isTrigger)
        {
            return;
        }

        if (collision.CompareTag("Player"))
        {
            if (StaticVar.player.PlayerHP > 0)
            {
                StaticVar.player.PlayerHP -= 1;
            }            
            //Transform player = StaticVar.player.transform;
            //TankMoveData tankTrackMoveData = new TankMoveData();
            //player.GetComponent<PlayerController>().SetPlayerTankTrack(tankTrackMoveData);

            //collision.GetComponent<PlayerHitArea>().TakeDamage();
        }
    }
}

