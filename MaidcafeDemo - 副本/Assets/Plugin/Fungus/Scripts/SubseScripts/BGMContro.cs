using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMContro : MonoBehaviour
{
    public AudioSource myaudio;
    public AudioClip[] _clips;
    public static BGMContro  _instance;
    private void Awake()
    {
        _instance = this;

    }

    public void PlayMusic(int i)
    {
        myaudio = GameObject.Find("FungusManager").GetComponent<AudioSource>();
        myaudio.clip = _clips[i];
        myaudio.Play();
    }
    public void StopBGM()
    {
        myaudio = GameObject.Find("FungusManager").GetComponent<AudioSource>();
        myaudio.Stop();
    }
}
