using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowEvent : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        StaticVar.bowlength = stateInfo.length;
       
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        StaticVar.bowlength = 0;
        
    }
}
