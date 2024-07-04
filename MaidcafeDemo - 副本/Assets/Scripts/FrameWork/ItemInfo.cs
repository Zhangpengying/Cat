using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class ItemInfo
{
    //道具ID
    public int itemID;
    //道具类型
    public ItemType itemType;
    //道具数量
    public int itemNum;
    //道具名字
    public string itemName;
    //道具描述
    public string itemDesc;

}
public enum ItemType
{
    //道具类型
     SingleItem =0,                   //单一道具
     MoreItem = 1                    //复数道具
}
