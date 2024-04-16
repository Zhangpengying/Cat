using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;

public class DownLoad : Singleton<DownLoad>
{
    /// <summary>
    /// 加载AB包
    /// </summary>
    /// <param name="bundlePath"></param>资源包路径
    /// <param name="relyonPath"></param>共享依赖资源包路径
    /// <param name="name"></param>要加载的资源的名字
    public void  LoadAB(string bundlePath,string relyonPath,string name )
    {
        //string path1 = "AssetBundles/cubewall.unity3d";
        //string path2 = "AssetBundles/share.unity3d";

        //方法二：同步加载(无需用协程)
        //加载资源
        AssetBundle ab3 = AssetBundle.LoadFromMemory(File.ReadAllBytes(bundlePath));
        //加载共同依赖资源，如贴图、材质
        AssetBundle ab4 = AssetBundle.LoadFromMemory(File.ReadAllBytes(relyonPath));

      
        AssetBundle ab5 = AssetBundle.LoadFromFile(bundlePath);
        AssetBundle ab6 = AssetBundle.LoadFromFile(relyonPath);

        //"CubeWall"要加载的资源的名字
        Object wallPrefab3 = ab5.LoadAsset(name);
        Object.Instantiate(wallPrefab3);


    }
}