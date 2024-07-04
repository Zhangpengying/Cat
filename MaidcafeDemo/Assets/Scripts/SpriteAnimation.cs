using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class SpriteAnimation : MonoBehaviour
{
    public Sprite[] sprites; // �洢����֡p��������֡
    public float framesPerSecond = 10.0f; // ÿ�벥�ŵ�֡��

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

