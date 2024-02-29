using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;

public class SetSoundEff : MonoBehaviour
{
    public void ChangeValue(Slider slider)
    {
        WriterAudio.Volume = slider.value;
        SetWnd.effa = slider.value;
        SSetWnd.effA = slider.value;
    }
}
