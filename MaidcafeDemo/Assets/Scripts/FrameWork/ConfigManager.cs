﻿//using UnityEngine;
using System.Collections.Generic;
using System;
using System.Xml;
using System.Reflection;
using UnityEngine;

public abstract class IConfigParser
{
    public abstract Dictionary<int, T> LoadConfig<T>(string tablename);

    protected T GreateAndSetValue<T>(XmlElement node)
    {
        // 通过类型创建一个对象实例
        T obj = Activator.CreateInstance<T>();

        // 获取一个类的所有字段
        FieldInfo[] fields = typeof(T).GetFields();

        for (int i = 0; i < fields.Length; i++)
        {
            string name = fields[i].Name;
            if (string.IsNullOrEmpty(name)) continue;

            string fieldValue = node.GetAttribute(name);
            if (string.IsNullOrEmpty(fieldValue)) continue;

            try
            {
                ParsePropertyValue<T>(obj, fields[i], fieldValue);
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("XML读取错误：对象类型({2}) => 属性名({0}) => 属性类型({3}) => 属性值({1})",
                    fields[i].Name, fieldValue, typeof(T).ToString(), fields[i].FieldType.ToString()));
            }
        }
        return obj;
    }


    private void ParsePropertyValue<T>(T obj, FieldInfo fieldInfo, string valueStr)
    {
        System.Object value = valueStr;

        // 将字符串解析为类中定义的类型
        if (fieldInfo.FieldType.IsEnum)
            value = Enum.Parse(fieldInfo.FieldType, valueStr);
        else
        {
            if (fieldInfo.FieldType == typeof(int))
                value = int.Parse(valueStr);
            else if (fieldInfo.FieldType == typeof(byte))
                value = byte.Parse(valueStr);
            else if (fieldInfo.FieldType == typeof(bool))
                value = bool.Parse(valueStr);
            else if (fieldInfo.FieldType == typeof(float))
                value = float.Parse(valueStr);
            else if (fieldInfo.FieldType == typeof(double))
                value = double.Parse(valueStr);
            else if (fieldInfo.FieldType == typeof(uint))
                value = uint.Parse(valueStr);
            else if (fieldInfo.FieldType == typeof(ulong))
                value = ulong.Parse(valueStr);
            else if (fieldInfo.FieldType == typeof(Vector3))
            {
                value = StaticVar.ParseVector3(valueStr);
            }
        }

        if (value == null)
            return;

        fieldInfo.SetValue(obj, value);
    }
}


/// <summary>
/// 游戏配置管理器
/// </summary>
public class ConfigManager : Singleton<ConfigManager>
{
    private IConfigParser _configParser;

    
  

    public void Initialize(IConfigParser configParser)
    {
        _configParser = configParser;
        LoadAllConfigs();
    }

    /// <summary>
    /// 载入所有游戏配置
    /// </summary>
    public void LoadAllConfigs()
    {
        MessageSend.instance.waiterCfgs = _configParser.LoadConfig<WaiterCfg>("Waiter");
        MessageSend.instance.customerCfgs = _configParser.LoadConfig<CustomerCfg>("Customer");
        MessageSend.instance.playerCfgs = _configParser.LoadConfig<PlayerCfg>("Player");
        MessageSend.instance.bornCfgs = _configParser.LoadConfig<BornPointCfg>("BornPoints");
        MessageSend.instance.propertyCfgs = _configParser.LoadConfig<Property>("DIYProperty");
        MessageSend.instance.menuCfgs = _configParser.LoadConfig<MenuCfg>("Menus");
        MessageSend.instance.messageInforCfgs = _configParser.LoadConfig<MessageCfg>("MessageInfor");
        MessageSend.instance.commodityCfg = _configParser.LoadConfig<CommodityCfg>("Commodity");
        MessageSend.instance.gatewaysCfg = _configParser.LoadConfig<GateWaysCfg>("GateWays");
        MessageSend.instance.doorsCfg = _configParser.LoadConfig<DoorsCfg>("Doors");
        MessageSend.instance.restEventCfg = _configParser.LoadConfig<RestEventCfg>("RestEvents");
        MessageSend.instance.systemPropertyCfg = _configParser.LoadConfig<SystemPropertyCfg>("SystemProperty");
    }

    /// <summary>
    /// 获取某一类型的角色
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    //public Dictionary<int, RoleCfg> GetTypeRoleCfgs(RoleType type)
    //{
    //    Dictionary<int, RoleCfg> cfgs = new Dictionary<int, RoleCfg>();
    //    foreach (RoleCfg cfg in MessageSend.instance.roleCfgs.Values)
    //    {
    //        if (cfg.RoleType == type)
    //            cfgs.Add(cfg.ID, cfg);
    //    }

    //    return cfgs;
    //}

}
