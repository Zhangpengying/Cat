using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class SpriteAnimation : MonoBehaviour
{
    public Sprite[] sprites; // 存储序列帧p画的所有帧
    public float framesPerSecond = 10.0f; // 每秒播放的帧数

    public SpriteAtlas m_spriteAtlas;

    // Start is called before the first frame update
    void Start()
    {
        
        //StartCoroutine(PlayAnimation(GetComponent<SpriteRenderer>,Resources));
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator PlayAnimation(SpriteRenderer sp, Sprite[] sprites)
    {
        while (true)
        {
            for (int i = 0; i < sprites.Length; i++)
            {
                sp.sprite = sprites[i];
                yield return new WaitForSeconds(1f / framesPerSecond);
            }
        }
    }



}

