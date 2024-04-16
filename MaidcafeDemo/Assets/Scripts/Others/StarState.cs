using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarState : MonoBehaviour {

    public Sprite defaultSprite;
    public Sprite highlightedSprite;
    //是否高亮
    public bool highQuality = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (highQuality)
        {
            GetComponent<Image>().sprite = highlightedSprite;
        }
        else
        {
            GetComponent<Image>().sprite = defaultSprite;
        }
	}
}
