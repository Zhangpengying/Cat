using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersonInforWnd : BaseWnd
{
    public void Initialize()
    {

        _transform.gameObject.AddComponent<PersonInforWndCon>();
    }
}

public class PersonInforWndCon : MonoBehaviour
{
    private void Start()
    {
        Player player = GameObject.FindWithTag("Player").GetComponent<Player>();

        player.IsLockPlayer = true;

        transform.Find("Title/MapBtn").GetComponent<Button>().onClick.AddListener(() => {
            WindowManager.instance.Open<WorldMapWnd>();
        });
        transform.Find("BackBtn").GetComponent<Button>().onClick.AddListener(() => {
            WindowManager.instance.Close<PersonInforWnd>();
            StaticVar.EndInteraction();
        });

        Button[] btnList1 = transform.Find("LeftBg/SingleItemList/Viewport/Content").GetComponentsInChildren<Button>();
        List<ItemInfo> singleItemList = new List<ItemInfo>();
        for (int i = 0; i < btnList1.Length; i++)
        {
            if (i<player.ItemList.Count && player.ItemList[i].itemType== ItemType.SingleItem)
            {
                singleItemList.Add(player.ItemList[i]);
                ItemInfo itemInfo = player.ItemList[i];
                btnList1[i].transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Atlas/" + itemInfo.itemName);
                btnList1[i].onClick.AddListener(() => ClickItemInfo(itemInfo)); 
            }
        }
        Button[] btnList2 = transform.Find("LeftBg/MoreItemList/Viewport/Content").GetComponentsInChildren<Button>();
        List<ItemInfo> moreItemList = new List<ItemInfo>();
        for (int i = 0; i < btnList2.Length; i++)
        {
            if (i < player.ItemList.Count && player.ItemList[i].itemType == ItemType.MoreItem)
            {
                moreItemList.Add(player.ItemList[i]);
                ItemInfo itemInfo = player.ItemList[i];

                btnList2[i].transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Atlas/" + itemInfo.itemName);
                btnList2[i].onClick.AddListener(() => ClickItemInfo(itemInfo));

            }
        }
    }

   
    private void ClickItemInfo(ItemInfo itemInfo)
    {
        transform.Find("Name").GetComponent<Text>().text = itemInfo.itemName;
        transform.Find("Desc").GetComponent<Text>().text = itemInfo.itemDesc;

    }


}
