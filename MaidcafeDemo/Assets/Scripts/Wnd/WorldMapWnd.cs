using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldMapWnd : BaseWnd
{
    public void Initialize()
    {

        _transform.gameObject.AddComponent<WorldMapWndCon>();
    }
}

public class WorldMapWndCon : MonoBehaviour
{
    private void Start()
    {
        Player player = GameObject.FindWithTag("Player").GetComponent<Player>();

      

        
        transform.Find("BackBtn").GetComponent<Button>().onClick.AddListener(() => {
            WindowManager.instance.Close<WorldMapWnd>();
        });

        
    }


    


}
