using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MenuType
{
    Food,
    Drink,

}

public class MenuCfg
{
    public int ID;
    public string MenuName;
    public int Price;
    public int Level;
    //升级系数
    public int LevelCoefficient;
    public MenuType Type;
    public string Introduce;
}
