using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorText : MonoBehaviour {

	// Use this for initialization
	void Start () {
        AnimationClip[] clips = GetComponent<Animator>().runtimeAnimatorController.animationClips;
        foreach (var item in clips)
        {
            Debug.Log(item.name+ item.length);
        }

        print(GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.length);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
