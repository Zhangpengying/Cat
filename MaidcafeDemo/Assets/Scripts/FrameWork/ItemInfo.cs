using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class ItemInfo
{
    //����ID
    public int itemID;
    //��������
    public ItemType itemType;
    //��������
    public int itemNum;
    //��������
    public string itemName;
    //��������
    public string itemDesc;

}
public enum ItemType
{
    //��������
     SingleItem =0,                   //��һ����
     MoreItem = 1                    //��������
}
