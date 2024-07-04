using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animation>().Play("Player_RightWalk");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
