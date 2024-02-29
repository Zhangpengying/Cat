
using System;
using System.Collections;
using System.Reflection;

using UnityEngine;

public class AddMyTag : MonoBehaviour
{
    //private static string[] myTags = { "EmptySaveWndBtn", "CG", "Point", "BackGround" , "SaveWndBtn" , "prop" , "removebtn", "empty", "PointL", "ActCG" };
    //static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    //{
    //    foreach (string s in importedAssets)
    //    {
    //        if (s.Equals("Assets/AddMyTag.cs"))
    //        {
    //            //增加一个叫momo的tag
    //            foreach (string tag in myTags)
    //            {
    //                AddTag(tag);
    //            }
    //            //AddTag("EmptySaveWndBtn");
    //            //AddTag("CG");
    //            //AddTag("LoadWndBtn");
    //            //AddTag("CG1");
    //            //AddTag("Point");
    //            //AddTag("BackGround");
    //            //AddTag("SaveWndBtn");
    //            //AddTag("prop");
    //            //AddTag("removebtn");
    //            //AddTag("empty");
    //            //AddTag("PointL");
    //            //AddTag("ActCG");
    //            return;
    //        }
    //    }
    //}

    //static void AddTag(string tag)
    //{
    //    if (!isHasTag(tag))
    //    {
    //        SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
    //        SerializedProperty it = tagManager.GetIterator();
    //        while (it.NextVisible(true))
    //        {
    //            if (it.name == "tags")
    //            {
    //                for (int i = 0; i < it.arraySize; i++)
    //                {
    //                    SerializedProperty dataPoint = it.GetArrayElementAtIndex(i);
    //                    if (string.IsNullOrEmpty(dataPoint.stringValue))
    //                    {
    //                        dataPoint.stringValue = tag;
    //                        tagManager.ApplyModifiedProperties();
    //                        return;
    //                    }
    //                }
    //            }
    //        }
    //    }
    //}

    //static bool isHasTag(string tag)
    //{
    //    for (int i = 0; i < UnityEditorInternal.InternalEditorUtility.tags.Length; i++)
    //    {
    //        if (UnityEditorInternal.InternalEditorUtility.tags[i].Contains(tag))
    //            return true;
    //    }
    //    return false;
    //}

  
}
