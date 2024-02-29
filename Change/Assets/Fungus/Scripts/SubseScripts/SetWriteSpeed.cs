using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Fungus
{
    public class SetWriteSpeed : MonoBehaviour
    {
        public Slider mySlider;
        
    
        public void ChangeWriteSpeed( )
        {
            Writer.WritingSpeed = mySlider.value;
            SetWnd.writea = mySlider.value;
            SSetWnd.writeA = mySlider.value;
        }
    }
}


