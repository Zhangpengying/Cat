using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Message
{
    //消息类型
    public byte Type;
    //消息ID
    public int Command;
    //消息内容
    public object Content;

    public Message(){}

    public Message(byte type,int command, object obj)
    {
        Type = type;
        Command = command;
        Content = obj;
    }
}

//消息类型
public class MyMessageType
{
    public static byte Type_UI = 1;
    public static byte Type_Audio = 2;
    public static byte Type_CharAttribute = 3;
    public static byte Type_Event = 4;

    public static int Audio_PlaySound = 100;
    public static int Audio_PlayEffect = 101;
    public static int Audio_StopSound = 102;
    public static int Audio_StopEffect = 103;

    public static int UI_ShowTalk1 = 200;
    public static int UI_AddScore = 201;

    //改变人物状态
    public static int Attribute_State = 300;

    //触发测试事件
    public static int Event_Talk1 = 400;
    //家里传送到商业街上
    public static int Event_ToStreet = 401; 
    //NPC
    public static int Event_NPC = 402;
    //睡觉
    public static int Event_Bed = 403;
    //HomeToStreet打开
    public static int Event_OpenHomeToStreet = 404;
    //HomeToStreet关闭
    public static int Event_CloseHomeToStreet = 405;
    //StreetToHome打开
    public static int Event_OpenStreetToHome = 406;
    //StreetToHome关闭
    public static int Event_CloseStreetToHome = 407;
    //StreetToCoffee打开
    public static int Event_OpenStreetToCoffee = 408;
    //StreetToCoffee关闭
    public static int Event_CloseStreetToCoffee = 409;
    //CoffeeToStreet打开
    public static int Event_OpenCoffeeToStreet = 410;
    //CoffeeToStreet关闭
    public static int Event_CloseCoffeeToStreet = 411;
    //玩家进入椅子
    public static int Event_EnterChair = 412;
    //玩家离开椅子
    public static int Event_LeaveChair = 413;
    //进入约会触发
    public static int Event_Date = 414;
    //离开约会触发
    public static int Event_LeaveDate = 415;
    //触发老猫
    public static int Event_Cat = 416;
    //触发RainFa
    public static int Event_RainFa = 417;
    //约会回家
    public static int Event_Talk2 = 418;
    //改良菜品
    public static int Event_OperatingFloor = 419;
    //调整当天菜谱
    public static int Event_AdjustMenus = 420;
    //开始经营
    public static int Event_StartManage = 421;
    //DIY
    public static int Event_DIY = 422;
    //到达菜单
    public static int Event_AdjustAllMenus = 423;
    //调整当天售卖的周边物品
    public static int Event_AdjustCommodity = 424;
    //咖啡店一层门打开
    public static int Event_OpenCfFirFl = 425;
    //咖啡店一层门关闭
    public static int Event_CloseCfFirFl = 427;
    //咖啡店二层门打开
    public static int Event_OpenCfSecFl = 428;
    //咖啡店二层门关闭
    public static int Event_CloseCfSecFl = 429;
    //触发结束中场休息
    public static int Event_EndRest = 430;
    //触发中场休息的事件
    public static int Event_StartEvent = 431;
    //离开中场休息事件触发区域
    public static int Event_LeaveEvent = 432;
    //保存游戏
    public static int Event_SaveGame = 433;
    //删除游戏存档
    public static int Event_DeleteData = 434;
    //结束约会
    public static int Event_EndDate = 435;
    //CoffeeToHome打开
    public static int Event_OpenCoffeeToHome = 436;
    //CoffeeToHome关闭
    public static int Event_CloseCoffeeToHome = 437;
    //离开菜单
    public static int Event_LeaveAllMenus = 438;
    //触发商店NPC
    public static int Event_OpenStore = 439;
    //离开商店NPC
    public static int Event_LeaveStore = 440;
    //离开床
    public static int Event_LeaveBed = 441;
    //离开测试事件
    public static int Event_LeaveTalk1 = 442;

    //触发道具
    public static int Event_TouchProperty1 = 500;
    public static int Event_LeaveProperty1 = 501;

    public static int Event_TouchProperty2 = 502;
    public static int Event_LeaveProperty2 = 503;

    public static int Event_TouchProperty3 = 504;
    public static int Event_LeaveProperty3 = 505;

    public static int Event_TouchProperty4 = 506;
    public static int Event_LeaveProperty4 = 507;

    public static int Event_TouchProperty5 = 508;
    public static int Event_LeaveProperty5 = 509;

    public static int Event_TouchProperty8 = 510;
    public static int Event_LeaveProperty8 = 511;

    //触发传送门
    public static int Event_TouchGateWay1 = 550;
    //public static int Event_LeaveGateWay1 = 551;
    //解锁传送门
    public static int Event_UnlockGateWay1 = 552;

    //触发NPC
    public static int Event_TouchNPC1 = 600;
    public static int Event_LeaveNPC1 = 601;

    public static int Event_TouchNPC2 = 602;
    public static int Event_LeaveNPC2 = 603;

    public static int Event_TouchNPC_CunZhang = 604;
    public static int Event_LeaveNPC_CunZhang = 605;

    public static int Event_TouchNPC_TieJiang = 606;
    public static int Event_LeaveNPC_TieJiang = 607;

    public static int Event_TouchNPC_ZhanBuShi = 608;
    public static int Event_LeaveNPC_ZhanBuShi = 609;

    public static int Event_TouchNPC_LaErFu = 610;
    public static int Event_LeaveNPC_LaErFu = 611;

    public static int Event_TouchMovePoint = 612;
    public static int Event_LeaveMovePoint = 613;



    public static int Event_AddHP = 700;
    public static int Event_Monster = 701;

    //触发障碍1
    public static int Event_TouchObstacles1 = 750;
    //解除障碍1
    public static int Event_UnlockObstacles1 = 751;
    //触发障碍2
    public static int Event_TouchObstacles2 = 752;
    //解除障碍2
    public static int Event_UnlockObstacles2 = 753;

}


