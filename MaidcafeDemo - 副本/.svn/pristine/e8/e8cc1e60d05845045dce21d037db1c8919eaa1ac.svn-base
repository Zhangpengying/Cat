using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DIYTypeContro : MonoBehaviour {

    public Sprite defaultSprite;
    public Sprite selectSprite;
    // Use this for initialization
    void Start()
    {
        
        defaultSprite = LoadTexture.getInstance().LoadAtlasSprite("Atlas/UI/BaseFrame/BaseFrame1", "底框-通用-未选中");
        selectSprite = LoadTexture.getInstance().LoadAtlasSprite("Atlas/UI/BaseFrame/BaseFrame1", "底框-通用-选中");
    }

    // Update is called once per frame
    void Update()
    {
        if (StaticVar.CurrentMenu != null)
        {
            if (StaticVar.CurrentMenu == transform)
            {
                transform.GetComponent<Image>().sprite = selectSprite;
            }
            else
            {
                transform.GetComponent<Image>().sprite = defaultSprite;

            }
        }
        else
        {
            transform.GetComponent<Image>().sprite = defaultSprite;

        }
    }
}
