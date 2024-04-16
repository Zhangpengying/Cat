using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class BGMContro : MonoBehaviour
{
    public AudioSource mybgmaudio;
    public AudioSource mysoundeffaudio;
    
    //背景音乐
    public AudioClip[] _bgmclips;
    //音效
    public AudioClip[] _soundeffclips;
    //配音
    public AudioClip[] _dubbing;
    public static BGMContro  _instance;
    

    private void Awake()
    {
        _instance = this;
        mybgmaudio = GameObject.Find("FungusManager").GetComponent<AudioSource>();
        mysoundeffaudio = GameObject.Find("GameManager").GetComponent<AudioSource>();

    }
   
    public void PlayMusic(int i)
    {
        mybgmaudio.clip = _bgmclips[i-1];
        mybgmaudio.Play();
    
    }
    public void StopBGM()
    {
        mybgmaudio.Stop();
      
    }
    public void StopSoundEff()
    {
        mysoundeffaudio.Stop();
      
    }

    public void PlaySoundEff(int n)
    {
        //mysoundeffaudio.PlayOneShot(_soundeffclips[n], StaticVar.soundeff);
    }
    //背景音乐音量数值判定
    public void BGMGiveValue(int n, AudioSource audio)
    {
        if (n / 2 == 0)
        {
            audio.volume = 0;
        }
        else if (n / 2 == 1)
        {
            audio.volume = 0.1f;
        }
        else if (n / 2 == 2)
        {
            audio.volume = 0.3f;
        }
        else if (n / 2 == 3)
        {
            audio.volume = 0.5f;
        }
        else if (n / 2 == 4)
        {
            audio.volume = 0.7f;
        }
        else if (n / 2 == 5)
        {
            audio.volume = 0.9f;
        }
        else
        {
            audio.volume = 1;
        }

    }
    //音效音量数值判定
    public void SoundEffGiveValue(int n, AudioSource audio)
    {
        if (n / 2 == 0)
        {
            audio.volume = 0;
        }
        else if (n / 2 == 1)
        {
            audio.volume = 0.1f;
        }
        else if (n / 2 == 2)
        {
            audio.volume = 0.2f;
        }
        else if (n / 2 == 3)
        {
            audio.volume = 0.3f;
        }
        else if (n / 2 == 4)
        {
            audio.volume = 0.4f;
        }
        else if (n / 2 == 5)
        {
            audio.volume = 0.5f;
        }
        else
        {
            audio.volume = 0.6f;
        }

    }

}
