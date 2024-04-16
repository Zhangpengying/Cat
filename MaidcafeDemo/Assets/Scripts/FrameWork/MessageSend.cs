﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MessageSend : Singleton<MessageSend>  
{
    
    //所有可停留位置
    public List<Transform> stayPointList = new List<Transform>();

    //玩家
    public Dictionary<int, PlayerCfg> playerCfgs = new Dictionary<int, PlayerCfg>();

    //所有的女仆
    public Dictionary<int, WaiterCfg> waiterCfgs = new Dictionary<int, WaiterCfg>();

    //所有的客人
    public Dictionary<int, CustomerCfg> customerCfgs = new Dictionary<int, CustomerCfg>();

    //关卡出生点
    public Dictionary<int, BornPointCfg> bornCfgs = new Dictionary<int, BornPointCfg>();

    //所有可以DIY的物品
    public Dictionary<int, Property> propertyCfgs = new Dictionary<int, Property>();

    //已创建的物品和对应占据的基准格ID
    public Dictionary<Building, ArrayList> propertyIDs = new Dictionary<Building, ArrayList>();

    //已创建物品和对应占据的首个基准格
    public Dictionary<Property, Vector2> PropertyToID = new Dictionary<Property, Vector2>();

    //背包
    public List<Property> CurrentHavePropertys = new List<Property>();

    //DIY创建的物品
    public List<Building> CreatPropertys = new List<Building>();

    //DIY创建物品信息
    public Dictionary<Property, Vector3> CreatPropertysInfo = new Dictionary<Property, Vector3>();

    //场景内所有的女仆
    public List<Waiter1> waiter = new List<Waiter1>();

    //场景内所有的客人
    public List<Customer1> customer = new List<Customer1>();

    //一楼分配的女仆
    public List<Waiter1> firstFloorWaiter = new List<Waiter1>();

    //二楼分配女仆
    public List<Waiter1> secondFloorWaiter = new List<Waiter1>();

    //玩家当前拥有的女仆
    public List<WaiterCfg> currenthavewaiters = new List<WaiterCfg>();

    //当天排班的女仆
    public List<WaiterCfg> currentDayWaiters = new List<WaiterCfg>();

    //女仆库
    public List<WaiterCfg> allwaiters = new List<WaiterCfg>();

    //NPC库
    public List<CustomerCfg> allcustomers = new List<CustomerCfg>();

    //一楼所有的座位
    public List<Transform> firstFloorSeats = new List<Transform>();

    //二楼所有的座位
    public List<Transform> secondFloorSeats = new List<Transform>();

    //工作中的女仆和顾客的对应关系
    public Dictionary<Waiter1, Customer1> combine = new Dictionary<Waiter1, Customer1>();

    //某个客人当前选择的座位
    public Dictionary<Customer1, Transform> customerSeat = new Dictionary<Customer1, Transform>();

    //座位与对应的服务位置
    public Dictionary<Transform, Transform> seatToService = new Dictionary<Transform, Transform>();

    //所有的可触发事件
    public Dictionary<int, Hashtable> Events = new Dictionary<int, Hashtable>();

    //当前同时触发的事件ID
    public List<int> triggerEventID = new List<int>();

    //出入口的一一对应
    public Dictionary<string, string> Gateways = new Dictionary<string, string>();

    //传送阵
    public Dictionary<int, GateWaysCfg> gatewaysCfg = new Dictionary<int, GateWaysCfg>();

    //门
    public Dictionary<int, DoorsCfg> doorsCfg = new Dictionary<int, DoorsCfg>();
    
    //所有菜品
    public Dictionary<int, MenuCfg> menuCfgs = new Dictionary<int, MenuCfg>();

    //当前拥有菜品
    public List<MenuCfg> HaveMenus = new List<MenuCfg>();

    //当天安排的售菜
    public List<MenuCfg> CurrentDayMenus = new List<MenuCfg>();

    //所有的提示消息
    public Dictionary<int,MessageCfg> messageInforCfgs = new Dictionary<int,MessageCfg>();

    //所有的周边商品
    public Dictionary<int, CommodityCfg> commodityCfg = new Dictionary<int, CommodityCfg>();

    //当前拥有的周边商品
    public List<CommodityCfg> CurrentHaveCom = new List<CommodityCfg>();

    //当天售卖的周边商品
    public List<CommodityCfg> CurrentDayCom = new List<CommodityCfg>();

    //系统背包物品
    public Dictionary<int, SystemPropertyCfg> systemPropertyCfg = new Dictionary<int, SystemPropertyCfg>();

    //当前拥有的系统背包物品
    public List<SystemPropertyCfg> CurrentHaveSysPro = new List<SystemPropertyCfg>();

    //中场休息时会出现的特殊客人
    public List<CustomerCfg> RestCustomers = new List<CustomerCfg>();

    //中场休息时的事件
    public Dictionary<int, RestEventCfg> restEventCfg = new Dictionary<int, RestEventCfg>();

    //已经解锁的特殊客人
    public List<CustomerCfg> CurrUnLockCustomer = new List<CustomerCfg>();

    //游戏基本时间信息
    public Hashtable BasicInfor = new Hashtable();
}
