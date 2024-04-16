using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PropertyType
{
    All,                       //全部
    Furniture,                //家具
    Wiring,                   //电器
    Decorite,                 //装饰
}

public class Property 
{
    public int ID;
    public string PropertyName;
    public int OrderLayer;
    public PropertyType Type;
    public int Price;

}
