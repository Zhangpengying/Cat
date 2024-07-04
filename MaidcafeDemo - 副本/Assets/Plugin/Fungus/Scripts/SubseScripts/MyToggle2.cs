using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;

public class MyToggle2 : MonoBehaviour
{
    public void SetToggle(Image onImage)
    {
        var aaa = FungusManager.Instance;

        if (aaa.GetComponent<AudioSource>().clip == BGMContro._instance._clips[2] && BGMContro._instance.myaudio.isPlaying)
        {
            if (GetComponent<Toggle>().isOn == false)
            {
                onImage.enabled = true;
                BGMContro._instance.StopBGM();

            }
            else
            {
                onImage.enabled = false;
            }
        }
        else
        {
            if (GetComponent<Toggle>().isOn == false)
            {
                onImage.enabled = true;
                BGMContro._instance.StopBGM();

            }
            else
            {
                onImage.enabled = false;
                BGMContro._instance.StopBGM();
                BGMContro._instance.PlayMusic(2);
            }
        }
    }
}
