﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewDIYWmd : BaseWnd
{
  

    //初始化DIY界面
    public void Initialize()
    {
        //类型按钮添加监听
        foreach (var item in _transform.Find("DIYProTypes/MenuList").GetComponentsInChildren<Button>())
        {
            item.gameObject.AddComponent<DIYTypeContro>();
            _transform.GetComponent<NewBuildingCon>().propertyTypes.Add(item.transform);
        }
        //遍历背包,添加监听
        Button[] tempBags = _transform.Find("Store/Viewport/Content").GetComponentsInChildren<Button>();
       
        for (int i = 0; i < tempBags.Length; i++)
        {
            if (i < MessageSend.instance.CurrentHavePropertys.Count)
            {
                tempBags[i].gameObject.AddComponent<DIYBagContro>().property = MessageSend.instance.CurrentHavePropertys[i];
                tempBags[i].GetComponent<DIYBagContro>().defaultSprite = LoadTexture.getInstance().LoadAtlasSprite("Atlas/UI/BaseFrame/BaseFrame1", "底框-物品栏-未选中");
                tempBags[i].GetComponent<DIYBagContro>().selectSprite = LoadTexture.getInstance().LoadAtlasSprite("Atlas/UI/BaseFrame/BaseFrame1", "底框-物品栏-选中");
            }
            _transform.GetComponent<NewBuildingCon>().bagPropertys.Add(tempBags[i].transform);
        } 
      
        //修改组合键添加监听
        foreach (var item in _transform.Find("Revise").GetComponentsInChildren<Button>())
        {
            item.gameObject.AddComponent<DIYTypeContro>();
            _transform.GetComponent<NewBuildingCon>().reviseTypes.Add(item.transform);
        }
        //DIY区域
        
        NewBuildingCon bc = _transform.GetComponent<NewBuildingCon>();
        Transform content = _transform.Find("DIYRange/Viewport/Content");
        
        bc.normalizState.Clear();

      
       
        for (int i = 1; i < 14; i++)
        {
            for (int j = 1; j < 33; j++)
            {
               
                Transform btn = content.GetChild((i-1)*32 +j-1);
                if (btn.GetComponent<NormalizeBtn>()==null)
                {
                    btn.gameObject.AddComponent<NormalizeBtn>();
                    btn.name = "Button" + ((i - 1) * 32 + j - 1);
                    btn.GetComponent<NormalizeBtn>().normalizeID = new Vector2(i, j);
                    btn.GetComponent<NormalizeBtn>().state = true;
                    btn.GetComponent<NormalizeBtn>().normalizeSprite = LoadTexture.getInstance().LoadAtlasSprite("Atlas/UI/BaseFrame/BaseFrame1", "DIY区域-空置");
                    btn.GetComponent<NormalizeBtn>().canPutSprite = LoadTexture.getInstance().LoadAtlasSprite("Atlas/UI/BaseFrame/BaseFrame1", "DIY区域-可放置");
                    btn.GetComponent<NormalizeBtn>().noPutSprite = LoadTexture.getInstance().LoadAtlasSprite("Atlas/UI/BaseFrame/BaseFrame1", "DIY区域-不可放置");
                }
                bc.normalizState.Add(btn.GetComponent<NormalizeBtn>().normalizeID, btn.GetComponent<NormalizeBtn>().state);
            }
        }

        //修改一二层门的基准格状态
        int[] doors = new int[] { 43, 44, 45, 75, 76, 77, 107, 108, 109, 139, 140, 141, 171, 172, 173, 267, 268, 269, 299, 300, 301, 331, 332, 333, 363, 364, 365, 395, 396, 397 };
        foreach (var item in doors)
        {
            List<Vector2> temp = new List<Vector2>(bc.normalizState.Keys);
            bc.doorIDs.Add(temp[item ]);
            bc.normalizState[temp[item ]] = false;
            content.GetChild(item).GetComponent<NormalizeBtn>().state = false;
        }
    }

}
