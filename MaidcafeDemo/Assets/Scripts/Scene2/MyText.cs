 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Anima2D;

public class MyText : MonoBehaviour
{

    private void Start()
    {
        SpriteRenderer a = transform.Find("img").GetComponentInChildren<SpriteRenderer>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Character")
        {
            int n = int.Parse(transform.Find("img").GetComponentInChildren<SpriteMeshInstance>().sortingLayerName);
            //判定谁的位置靠下
            if (transform.localPosition.y-collision.transform.localPosition.y>0)
            {
                foreach (var item in collision.transform.Find("img").GetComponentsInChildren<SpriteMeshInstance>())
                {
                    item.sortingLayerName = (n + 1).ToString();
                    
                }
            }
            else
            {
                foreach (var item in collision.transform.Find("img").GetComponentsInChildren<SpriteMeshInstance>())
                {
                    item.sortingLayerName = (n - 1).ToString();
                }
            }
        }
    }

}


