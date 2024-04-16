using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutDownEvent : StateMachineBehaviour {

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       

    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.GetComponent<WaiterAni>().Active2();
    }
}
