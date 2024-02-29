using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class InitializeSound : MonoBehaviour
{
    public AudioSource clickAudioSource;
    public void Initialize()
    {
        clickAudioSource = GameObject.Find("FungusManager").GetComponent<AudioSource>();
        clickAudioSource.volume = SetWnd.bgma;
        WriterAudio.Volume = SetWnd.effa;
        Writer.WritingSpeed = SetWnd.writea;
        Say.WaitForClick = true;
    }
}
