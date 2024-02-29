using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;//注意引入
using UnityEngine.UI;
using UnityEditor;


public class MiniMapUtil : MonoBehaviour
{
    public string mapName = "";
    //public static  int  i = 0;
    
    //按钮点击触发该事件
    public void Open()
    {
        
        string path = Application.dataPath + "/Fungus/Resources/Sprites/";
        if (Directory.Exists(path))
        {
            //获取文件信息
            DirectoryInfo direction = new DirectoryInfo(path);

            FileInfo[] files = direction.GetFiles("*", SearchOption.AllDirectories);
            SaveWnd.q = files.Length/2;
        }
            mapName = "Image" + (SaveWnd.q+ 1);

        StartCoroutine(UploadPNG());
    }
    IEnumerator UploadPNG()
    {
        // 因为"WaitForEndOfFrame"在OnGUI之后执行    
        // 所以我们只在渲染完成之后才读取屏幕上的画面  
        
        yield return new WaitForEndOfFrame();
        SaveRenderTextureToPNG(mapName);
        //截完图之后打开窗口，把图片赋值给指定物体
        //yield return new WaitForSeconds(1);
        WindowManager.instance.Close<MenuWnd>();
        WindowManager.instance.Open<SaveWnd>().Initialize();
    }

    public void SaveRenderTextureToPNG(string pngName)
    {
        //string str = Directory.GetCurrentDirectory();
        string contents = Application.dataPath + "/Fungus/Resources/Sprites/";
        //string contents = Path.Combine(str, "Assets/Fungus/Resources/Sprites");

        Texture2D png = new Texture2D(Screen.width, Screen.height, TextureFormat.ARGB32, false);
        png.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);

      
        //截完的Texture转换成sprite传递给指定窗口中声明的变量
        //Sprite sprite = Sprite.Create(png, new Rect(0, 0, 64, 64), Vector2.zero);
        //SaveWnd.sp = sprite;

        byte[] bytes = png.EncodeToPNG();
        if (!Directory.Exists(contents))
            Directory.CreateDirectory(contents);
        FileStream file = File.Open(contents + "/" + pngName + ".png", FileMode.Create);
        BinaryWriter writer = new BinaryWriter(file);
        writer.Write(bytes);
        file.Close();
        AssetDatabase.Refresh();
        
    }
}
