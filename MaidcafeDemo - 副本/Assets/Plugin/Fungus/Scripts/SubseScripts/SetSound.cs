using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetSound : MonoBehaviour
{
    
    public   AudioSource myAutio;
    public Slider mySlider;
   
    public  void ChangeValue(Slider slider)
    {
        myAutio = GameObject.Find("FungusManager").GetComponent<AudioSource>();
        myAutio.volume = slider.value;
        mySlider = slider;
        SSetWnd.bgmA = slider.value;
        SetWnd.bgma = slider.value;
       
    }
}
