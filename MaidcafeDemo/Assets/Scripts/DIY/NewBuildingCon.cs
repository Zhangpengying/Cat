﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBuildingCon : MonoBehaviour {

    //各个网格的ID和状态
    public Dictionary<Vector2, bool> normalizState = new Dictionary<Vector2, bool>();
    //第一级别默认选项
    public Transform firstDefault;
    //第二级别默认选项
    public Transform secondDefault;
    //第三级别默认选项(DIY区域默认创建基准格)
    public Transform thirdDefault;
    //第四级别默认基准格（删除和再次移动）
    public Transform fouthDefault;
    //基准格的UI宽度
    public float WidthUI;
   //基准格的UI高度
    public float HeightUI;
    //基准格的场景宽度
    public float WidthScene;
    //基准格的场景高度
    public float HeightScene;
    //默认基准格位置坐标
    public Vector3 defaultCreatPosition;
    //背包
    public ArrayList bagPropertys = new ArrayList();
    //DIY物品类型
    public ArrayList propertyTypes = new ArrayList();
    //修改功能键位组合
    public ArrayList reviseTypes = new ArrayList(); 
    //默认格
    public Transform defaultPosition;
    //当前选择类型
    public PropertyType currentType;
    //当前创建物体覆盖的基准格
    private ArrayList currentBuildID = new ArrayList();
    //当前修改物体
    private Building reviseBuild;
    //创建DIY物品的UI映射图片
    private Transform BuildToUIImage; 
    //修改状态下物体是否可以移动
    private bool canMove = false;
    //键盘按键持续按下的检测时间
    public float getKeyTime;
    //键盘持续按下后的移动速度
    public float velocity;
    //是否左键长按
    private bool isGetLeftKey = false;
    //是否右键长按
    private bool isGetRightKey = false;
    //是否上键长按
    private bool isGetUpKey = false;
    
    //是否下键长按
    private bool isGetDownKey = false;
    //门的位置坐标
    public List<Vector2> doorIDs = new List<Vector2>();
    
    // Use this for initialization
    void Start () {
        getKeyTime = StaticVar.GetKeyTime;
        velocity = StaticVar.Velocity;
        //配置基准格尺寸
        WidthUI = 40;
        HeightUI = 40f;
        Canvas canvas = transform.parent.GetComponent<Canvas>();
        Vector3 top = StaticVar.UIToScreenPos(canvas, transform.Find("DIYRange/Viewport/Content/Button0").gameObject);
        Vector3 bottom = StaticVar.UIToScreenPos(canvas, transform.Find("DIYRange/Viewport/Content/Button384").gameObject);
        Vector3 right = StaticVar.UIToScreenPos(canvas, transform.Find("DIYRange/Viewport/Content/Button31").gameObject);
        WidthScene = (top.y - bottom.y) / 12;
        HeightScene = (right.x - top.x) / 31;
       
       
        //设定各级别默认选项
        firstDefault = transform.Find("DIYProTypes/MenuList/Button1");
        secondDefault = transform.Find("Store/Viewport/Content/Button1");
        thirdDefault = transform.Find("DIYRange/Viewport/Content/Button400");
        fouthDefault = transform.Find("Revise/MoveBtn");
        BuildToUIImage = transform.Find("DIYRange/CurrentCreatProperty");

        //设置默认创建位置
        defaultCreatPosition = StaticVar.UIToScreenPos(transform.parent.GetComponent<Canvas>(), thirdDefault.gameObject);
        //设置默认选项
        defaultPosition = transform.gameObject.GetComponent<NewBuildingCon>().secondDefault;
        StaticVar.CurrentMenu = defaultPosition;
        defaultPosition.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        //加载历史记录
        LoadDIYData();
        //刷新背包显示
        currentType = PropertyType.All;
        NeatBag();
    }
	
	// Update is called once per frame
	void Update () {
        if (StaticVar.CurrentMenu != null)
        { 
              //当前选择在分类总按钮上
            if (StaticVar.CurrentMenu == transform.Find("DIYProTypes/MenuButton"))
            {
                //打开分类
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    //消除总按钮上的选中状态
                    StaticVar.CurrentMenu.GetComponent<Image>().color = new Color(1, 1, 1, 0);
                    StaticVar.CurrentMenu = firstDefault;
                    firstDefault.parent.GetComponent<CanvasGroup>().alpha = 1;

                }
                //返回游戏
                else if (Input.GetKeyDown(KeyCode.X))
                {
                    WindowManager.instance.Close<NewDIYWmd>();
                    WindowManager.instance.Open<StartPreparationWnd>().Initialize();

                }
                //修改DIY
                else if (Input.GetKeyDown(KeyCode.N))
                {
                    //消除总按钮上的选中状态
                    StaticVar.CurrentMenu.GetComponent<Image>().color = new Color(1, 1, 1, 0);
                    ClickRevise();
                }
            }
            //当前选择在类型按钮组上
            else if (propertyTypes.Contains(StaticVar.CurrentMenu))
            {
                TypeController();
             
            }
            //当前选择在背包按钮组上
            else if (bagPropertys.Contains(StaticVar.CurrentMenu))
            {
                MenuController();
            }
            //当前选择是修改总按钮
            else if (StaticVar.CurrentMenu == fouthDefault.parent)
            {
                if (canMove)
                {
                    DIYController();
                    //确认放置
                    if (Input.GetKeyDown(KeyCode.Z))
                    {
                        //删除该物品的已放置信息
                        MessageSend.instance.propertyIDs.Remove(reviseBuild);
                        Property cfg = MessageSend.instance.propertyCfgs[reviseBuild.propertyID];
                        MessageSend.instance.CreatPropertysInfo.Remove(cfg);
                        MessageSend.instance.PropertyToID.Remove(cfg);
                        CheckState2();
                    }
                }
                else
                { 
                    //选择当前修改物
                    SelectRevisePro();
                    
                }
            }
            //当前选择在修改按钮组上
            else if (reviseTypes.Contains(StaticVar.CurrentMenu))
            { 
                StaticVar.InputControl1(reviseTypes);
                //修改对应字体和图标
                Transform tempIcon = fouthDefault.parent.Find("DeleteBtn");
                if (StaticVar.CurrentMenu == fouthDefault)
                {
                    fouthDefault.Find("Text").GetComponent<Text>().color = new Color(0, 0, 0, 1);
                    fouthDefault.Find("Image").GetComponent<Image>().sprite =  fouthDefault.Find("Image").GetComponent<ButtonBasic>().selectSprite;
                    tempIcon.Find("Text").GetComponent<Text>().color = new Color(1, 1, 1, 1);
                    tempIcon.Find("Image").GetComponent<Image>().sprite = tempIcon.Find("Image").GetComponent<ButtonBasic>().defaultSprite;
                }
                else
                {
                    tempIcon.Find("Text").GetComponent<Text>().color = new Color(0, 0, 0, 1);
                    tempIcon.Find("Image").GetComponent<Image>().sprite = tempIcon.Find("Image").GetComponent<ButtonBasic>().selectSprite;
                    fouthDefault.Find("Text").GetComponent<Text>().color = new Color(1, 1, 1, 1);
                    fouthDefault.Find("Image").GetComponent<Image>().sprite = fouthDefault.Find("Image").GetComponent<ButtonBasic>().defaultSprite;
                }
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    //判定移动或者删除
                    //改变物体的状态
                    reviseBuild.state = BuildingStates.Build;
                    currentBuildID.Clear();
                    
                    foreach (var item in MessageSend.instance.propertyIDs[reviseBuild])
                    {
                        currentBuildID.Add(item);
                        
                    }
                   
                    if (StaticVar.CurrentMenu == fouthDefault)
                    {
                        //失活按钮组
                        fouthDefault.parent.GetComponent<CanvasGroup>().alpha= 0;
                        //激活移动
                        StaticVar.CurrentMenu = fouthDefault.parent;
                       
                        canMove = true;
                    }
                    else
                    {
                        //删除该物体
                        StaticVar.CurrentMenu = secondDefault;
                      
                        fouthDefault.parent.GetComponent<CanvasGroup>().alpha = 0;
                       
                        //添加进背包
                       
                        if (reviseBuild != null)
                        {
                            Property tempProperty = MessageSend.instance.propertyCfgs[reviseBuild.propertyID];
                            MessageSend.instance.CurrentHavePropertys.Insert(0, tempProperty);
                            //删除该物体
                            MessageSend.instance.CreatPropertys.Remove(reviseBuild);
                            MessageSend.instance.propertyIDs.Remove(reviseBuild);
                            MessageSend.instance.PropertyToID.Remove(tempProperty);
                            MessageSend.instance.CreatPropertysInfo.Remove(tempProperty);
                            Destroy(reviseBuild.gameObject);
                        }

                        //修改这几个基准格的颜色
                        foreach (var item in currentBuildID)
                        {
                            Vector2 tempVec = (Vector2)item;
                            Transform temp = thirdDefault.parent.Find("Button" + ((tempVec.x - 1) * 32 + tempVec.y - 1));
                            UnActiveNormalizeBtn(temp, tempVec);
                        }
                        //还原UI映射
                        BuildToUIImage.localPosition = new Vector3(20, -240, 0);
                        BuildToUIImage.gameObject.SetActive(false);

                        currentBuildID.Clear();
                        reviseBuild = null;
                        //整理背包
                        NeatBag();
                    }
                }
                else if (Input.GetKeyDown(KeyCode.X))
                {
                    StaticVar.CurrentMenu = fouthDefault.parent;
                    fouthDefault.parent.GetComponent<CanvasGroup>().alpha = 0;
                }
            }
           
        }
        //控制DIY
        else
        {
            DIYController();
            if (Input.GetKeyDown(KeyCode.Z))
            {
                //定位（把物品放置在该位置）
                CheckState2();

            }
            //X键取消放置
            else if (Input.GetKeyDown(KeyCode.X))
            {

                StaticVar.CurrentMenu = secondDefault;
                Building cancelBuild = new Building();
                //添加进背包
                foreach (var item in MessageSend.instance.CreatPropertys)
                {
                    if (item.state == BuildingStates.Build)
                    {
                        cancelBuild = item;
                    }
                }
                if (cancelBuild != null)
                {
                    MessageSend.instance.CurrentHavePropertys.Insert(0, MessageSend.instance.propertyCfgs[cancelBuild.propertyID]);
                    //删除该物体
                    MessageSend.instance.CreatPropertys.Remove(cancelBuild);
                    Destroy(cancelBuild.gameObject);
                }

                //修改这几个基准格的颜色
                foreach (var item in currentBuildID)
                {
                    Vector2 tempVec = (Vector2)item;
                    Transform temp = thirdDefault.parent.Find("Button" + ((tempVec.x - 1) * 32 + tempVec.y - 1));
                    UnActiveNormalizeBtn(temp, tempVec);
                }
                //还原UI映射
                BuildToUIImage.localPosition = new Vector3(20, -240, 0);
                BuildToUIImage.gameObject.SetActive(false);

                currentBuildID.Clear();
                //整理背包
                NeatBag();
                //
            }
        }
    }
    //控制类型选择
    public void TypeController()
    {
        StaticVar.InputControl1(propertyTypes);
        if (Input.GetKeyDown(KeyCode.Z))
        {
            //关闭分类列表
            ((Transform)propertyTypes[0]).parent.GetComponent<CanvasGroup>().alpha = 0;
            //刷新背包列表显示
            switch (propertyTypes.IndexOf(StaticVar.CurrentMenu))
            {
                case 0:
                    currentType = PropertyType.All;
                    break;
                case 1:
                    currentType = PropertyType.Furniture;
                    break;
                case 2:
                    currentType = PropertyType.Wiring;
                    break;
                case 3:
                    currentType = PropertyType.Decorite;
                    break;
                default:
                    break;
            }
          
            transform.Find("DIYProTypes/MenuButton/Image").GetComponent<Image>().sprite = StaticVar.CurrentMenu.Find("Image").GetComponent<Image>().sprite;
            //默认选项跳转到下一级别选项

            //置空检测
            StaticVar.CurrentMenu = secondDefault;
            NeatBag();
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            //关闭分类列表
            ((Transform)propertyTypes[0]).parent.GetComponent<CanvasGroup>().alpha = 0;
            //设置当前默认按钮
            StaticVar.CurrentMenu = transform.Find("DIYProTypes/MenuButton");
            StaticVar.CurrentMenu.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
        else if (Input.GetKeyDown(KeyCode.N))
        {
            firstDefault.parent.GetComponent<CanvasGroup>().alpha = 0;
            ClickRevise();
        }
    }

    //背包选择
    public void MenuController()
    {
        int n = bagPropertys.IndexOf(StaticVar.CurrentMenu);
       
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //获取当前显示类型物品
            List<Property> currentShowPros = new List<Property>();
            foreach (var item in MessageSend.instance.CurrentHavePropertys)
            {
                if (currentType == PropertyType.All)
                {
                    currentShowPros.Add(item);
                }
                else
                {
                    if (item.Type == currentType)
                    {
                        currentShowPros.Add(item);
                    }
                }
            }
            if (n > 0)
            {
                StaticVar.CurrentMenu = (Transform)bagPropertys[n - 1];
            }
            else
            {
                //判定第一个按钮显示的物品是否是currentShowPros的第一个
                int m = currentShowPros.IndexOf(StaticVar.CurrentMenu.GetComponent<DIYBagContro>().property);
                if (m > 0)
                {
                    for (int i = 0; i < bagPropertys.Count; i++)
                    {
                        Transform btn = ((Transform)bagPropertys[i]);
                        btn.GetComponent<DIYBagContro>().property = currentShowPros[i + (m- 1)];
                        //加载图片
                        btn.Find("Image").GetComponent<Image>().sprite = LoadTexture.getInstance().LoadAtlasSprite("Atlas/UI/PropertyIcon/DIYPropertys/DIYPropertys", currentShowPros[m  + i - 1].PropertyName);

                        btn.Find("Image").GetComponent<Image>().raycastTarget = false;
                        btn.Find("Image").GetComponent<Image>().preserveAspect = true;
                        btn.Find("Image").GetComponent<Image>().SetNativeSize();
                        btn.Find("Image").gameObject.SetActive(true);
                    }
                }
              
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //当前显示类型物品
            List<Property> currentPros = new List<Property>();
            foreach (var item in MessageSend.instance.CurrentHavePropertys)
            {
                if (currentType == PropertyType.All)
                {
                    currentPros.Add(item);
                }
                else
                {
                    if (item.Type == currentType)
                    {
                        currentPros.Add(item);
                    }
                }
               
            }

            if (currentPros.Count > 10)
            {
                if (n == 9)
                {
                    int m = currentPros.IndexOf(StaticVar.CurrentMenu.GetComponent<DIYBagContro>().property);
                    if (m<(currentPros.Count-1))
                    {
                        for (int i = 0; i < bagPropertys.Count; i++)
                        {
                            Transform btn = ((Transform)bagPropertys[i]);
                            btn.GetComponent<DIYBagContro>().property = currentPros[m - 9 + i + 1];
                            //加载图片
                            btn.Find("Image").GetComponent<Image>().sprite = LoadTexture.getInstance().LoadAtlasSprite("Atlas/UI/PropertyIcon/DIYPropertys/DIYPropertys", currentPros[m - 9 + i + 1].PropertyName);
                             
                            btn.Find("Image").GetComponent<Image>().raycastTarget = false;
                            btn.Find("Image").GetComponent<Image>().preserveAspect = true;
                            btn.Find("Image").GetComponent<Image>().SetNativeSize();
                            btn.Find("Image").gameObject.SetActive(true);
                        }
                    }
                    
                }
                else
                {
                    StaticVar.CurrentMenu = (Transform)bagPropertys[n + 1];
                }

            }
            else
            {
                if (n < (currentPros.Count - 1))
                {
                    StaticVar.CurrentMenu = (Transform)bagPropertys[n + 1];
                }
                
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (StaticVar.CurrentMenu.GetComponent<DIYBagContro>()!= null)
            {
                //创建DIY物品
                string name = "Prefabs/Items/DIYItems/" + StaticVar.CurrentMenu.GetComponent<DIYBagContro>().property.PropertyName;
                GameObject property = Instantiate(Resources.Load(name) as GameObject);
                property.name = StaticVar.CurrentMenu.GetComponent<DIYBagContro>().property.PropertyName;
                SpriteRenderer a = property.GetComponentInChildren<SpriteRenderer>();
                a.sortingOrder = StaticVar.CurrentMenu.GetComponent<DIYBagContro>().property.OrderLayer;
                property.transform.SetParent(GameObject.Find("DIYCreatPropertys").transform);
                Building b = property.GetComponent<Building>();
                b.bc = transform.GetComponent<NewBuildingCon>();
                b.propertyID = StaticVar.CurrentMenu.GetComponent<DIYBagContro>().property.ID;
                b.state = BuildingStates.Build;
                MessageSend.instance.CreatPropertys.Add(b);
                NewBuildingCon bc = gameObject.GetComponent<NewBuildingCon>();

                //修改DIY物品的UI映射
                BuildToUIImage.GetComponent<Image>().sprite = a.sprite;
                BuildToUIImage.GetComponent<Image>().SetNativeSize();
                //修正临时图片位置
                BuildToUIImage.localPosition = (new Vector3((BuildToUIImage.localPosition.x + WidthUI * (b.width - 1) / 2), (BuildToUIImage.localPosition.y + HeightUI * (b.height - 1) / 2), 0) + b.offsetVec);

                //修正物品的位置
                property.transform.position = StaticVar.UIToScreenPos(transform.parent.GetComponent<Canvas>(), BuildToUIImage.gameObject);
                property.SetActive(true);
                //当前物品覆盖基准格

                for (int i = 0; i < b.height; i++)
                {
                    for (int j = 0; j < b.width; j++)
                    {
                        bc.currentBuildID.Add(new Vector2(thirdDefault.GetComponent<NormalizeBtn>().normalizeID.x - i, thirdDefault.GetComponent<NormalizeBtn>().normalizeID.y + j));
                    }
                }
                //激活这些基准格
                foreach (var item in currentBuildID)
                {
                    Vector2 tempVec = (Vector2)item;
                    Transform temp = thirdDefault.parent.Find("Button" + ((tempVec.x - 1) * 32 + tempVec.y - 1));
                    ActiveNormalizeBtn(temp, tempVec,b);
                }
                //贴地物品检测
                CheckIsGround(b, currentBuildID);

                //背包删除该物品
                MessageSend.instance.CurrentHavePropertys.Remove(StaticVar.CurrentMenu.GetComponent<DIYBagContro>().property);

                //整理背包
                StaticVar.CurrentMenu = null;
                NeatBag();
               
            }
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            StaticVar.CurrentMenu = transform.Find("DIYProTypes/MenuButton");
            StaticVar.CurrentMenu.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
        else if (Input.GetKeyDown(KeyCode.N))
        {
            ClickRevise();
        }
    }

    //DIY放置
    public void DIYController()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            GetKeyDownUp();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            GetKeyDownLeft();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            GetKeyDownDown();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            GetKeyDownRight();
        }

        //左键检测长按
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //长按检测
            getKeyTime -= Time.deltaTime;
            if (getKeyTime<=0)
            {
                isGetLeftKey = true;
            }
            if (isGetLeftKey)
            {
                velocity -= Time.deltaTime;
                if (velocity<= 0)
                {
                    GetKeyDownLeft();
                    velocity = StaticVar.Velocity;
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            isGetLeftKey = false;
            getKeyTime = StaticVar.GetKeyTime;
        }
        //右键长按检测
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //长按检测
            getKeyTime -= Time.deltaTime;
            if (getKeyTime <= 0)
            {
                isGetRightKey = true;
            }
            if (isGetRightKey)
            {
                velocity -= Time.deltaTime;
                if (velocity <= 0)
                {
                    GetKeyDownRight();
                    velocity = StaticVar.Velocity;
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            isGetRightKey = false;
            getKeyTime = StaticVar.GetKeyTime;
        }
        //上键长按检测
        if (Input.GetKey(KeyCode.UpArrow ))
        {
            //长按检测
            getKeyTime -= Time.deltaTime;
            if (getKeyTime <= 0)
            {
                isGetUpKey = true;
            }
            if (isGetUpKey)
            {
                velocity -= Time.deltaTime;
                if (velocity <= 0)
                {
                    GetKeyDownUp();
                    velocity = StaticVar.Velocity;
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            isGetUpKey = false;
            getKeyTime = StaticVar.GetKeyTime;
        }
        //下键长按检测
        if (Input.GetKey(KeyCode.DownArrow))
        {
            //长按检测
            getKeyTime -= Time.deltaTime;
            if (getKeyTime <= 0)
            {
                isGetDownKey = true;
            }
            if (isGetDownKey)
            {
                velocity -= Time.deltaTime;
                if (velocity <= 0)
                {
                    GetKeyDownDown();
                    velocity = StaticVar.Velocity;
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            isGetDownKey = false;
            getKeyTime = StaticVar.GetKeyTime;
        }
    }
    //DIY区域按下左键
    private void GetKeyDownLeft()
    {
        //判断物体是否在在DIY左边缘
        bool left = false;
        for (int i = 0; i < currentBuildID.Count; i++)
        {
            Vector2 tempID = (Vector2)currentBuildID[i];
            if (tempID.y == 1)
            {
                left = true;
            }
        }
        if (!left)
        {
            //修改物品当前覆盖区域
            for (int i = 0; i < currentBuildID.Count; i++)
            {
                Vector2 tempID = (Vector2)currentBuildID[i];
                Transform temp = thirdDefault.parent.Find("Button" + ((tempID.x - 1) * 32 + tempID.y - 1));
                UnActiveNormalizeBtn(temp, tempID);
                tempID.y -= 1;
                currentBuildID[i] = tempID;

            }
          
            //修改物品映射UI位置
            BuildToUIImage.localPosition -= new Vector3(WidthUI, 0, 0);
            Building currbuild = new Building();
            foreach (var item in MessageSend.instance.CreatPropertys)
            {
                if (item.state == BuildingStates.Build)
                {
                    currbuild = item;
                    item.transform.position = StaticVar.UIToScreenPos(transform.parent .GetComponent<Canvas>(), BuildToUIImage.gameObject);
                }
            }
            for (int i = 0; i < currentBuildID.Count; i++)
            {
                Vector2 tempID1 = (Vector2)currentBuildID[i];
                Transform temp1 = thirdDefault.parent.Find("Button" + ((tempID1.x - 1) * 32 + tempID1.y - 1));
                ActiveNormalizeBtn(temp1, tempID1, currbuild);
            }
            //贴地物品检测
            CheckIsGround(currbuild, currentBuildID);
        }
    }
    //DIY区域按下右键
    private void GetKeyDownRight()
    {
        //判断物体是否在在DIY边缘
        bool right = false;
        for (int i = 0; i < currentBuildID.Count; i++)
        {
            Vector2 tempID = (Vector2)currentBuildID[i];
            if (tempID.y == transform.Find("DIYRange/Viewport/Content").GetComponent<GridLayoutGroup>().constraintCount)
            {
                right = true;
            }
        }
        if (!right)
        {
            //修改物品当前覆盖区域
            for (int i = 0; i < currentBuildID.Count; i++)
            {
                Vector2 tempID = (Vector2)currentBuildID[i];
                Transform temp = thirdDefault.parent.Find("Button" + ((tempID.x - 1) * 32 + tempID.y - 1));
                UnActiveNormalizeBtn(temp, tempID);
                tempID.y += 1;
                currentBuildID[i] = tempID;

            }
          
            //修改物品映射UI位置
            BuildToUIImage.localPosition += new Vector3(WidthUI, 0, 0);
            Building currbuild = new Building();
            foreach (var item in MessageSend.instance.CreatPropertys)
            {
                if (item.state == BuildingStates.Build)
                {
                    currbuild = item;
                    item.transform.position = StaticVar.UIToScreenPos(transform.parent.GetComponent<Canvas>(), BuildToUIImage.gameObject);
                }
            }
            for (int i = 0; i < currentBuildID.Count; i++)
            {
                Vector2 tempID1 = (Vector2)currentBuildID[i];
                Transform temp1 = thirdDefault.parent.Find("Button" + ((tempID1.x - 1) * 32 + tempID1.y - 1));
                ActiveNormalizeBtn(temp1, tempID1,currbuild);
            }
            CheckIsGround(currbuild, currentBuildID);
        }
    }
    //DIY区域按下上键
    private void GetKeyDownUp()
    {
        //判断物体是否在在DIY上边缘
        bool top = false;
        for (int i = 0; i < currentBuildID.Count; i++)
        {
            Vector2 tempID = (Vector2)currentBuildID[i];
            if (tempID.x == 1)
            {
                top = true;
            }
        }

        //判定是否在一楼顶部
        bool floorTop = false;
        for (int i = 0; i < currentBuildID.Count; i++)
        {
            Vector2 tempID = (Vector2)currentBuildID[i];
            if (tempID.x == 8)
            {
                floorTop = true;
            }
        }
        if (!top)
        {
            if (floorTop)
            {
                Building currentBuild = new Building();
                //修改UI映射的位置
                foreach (var item in MessageSend.instance.CreatPropertys)
                {
                    if (item.state == BuildingStates.Build)
                    {
                        BuildToUIImage.localPosition += new Vector3(0, HeightUI * (1 + item.height), 0);
                        currentBuild = item;
                        currentBuild.transform.position += new Vector3(0, HeightScene * (1 + item.height), 0);
                    }
                }

                //修改物品当前覆盖区域
                for (int i = 0; i < currentBuildID.Count; i++)
                {
                    Vector2 tempID = (Vector2)currentBuildID[i];
                    Transform temp = thirdDefault.parent.Find("Button" + ((tempID.x - 1) * 32 + tempID.y - 1));
                    UnActiveNormalizeBtn(temp, tempID);
                    tempID.x -= (1 + currentBuild.height);
                    currentBuildID[i] = tempID;

                }
                for (int i = 0; i < currentBuildID.Count; i++)
                {
                    Vector2 tempID1 = (Vector2)currentBuildID[i];
                    Transform temp1 = thirdDefault.parent.Find("Button" + ((tempID1.x - 1) * 32 + tempID1.y - 1));
                    ActiveNormalizeBtn(temp1, tempID1,currentBuild);
                }
                CheckIsGround(currentBuild, currentBuildID);
            }
            else
            {
                //修改物品当前覆盖区域
                for (int i = 0; i < currentBuildID.Count; i++)
                {
                    //之前的按钮先失活
                    Vector2 tempID = (Vector2)currentBuildID[i];
                    Transform temp = thirdDefault.parent.Find("Button" + ((tempID.x - 1) * 32 + tempID.y - 1));
                    UnActiveNormalizeBtn(temp, tempID);
                    tempID.x -= 1;
                    currentBuildID[i] = tempID;

                }
             
                //修改映射UI位置和物品位置
                BuildToUIImage.localPosition += new Vector3(0, HeightUI, 0);
                Building currbuild = new Building();
                foreach (var item in MessageSend.instance.CreatPropertys)
                {
                    if (item.state == BuildingStates.Build)
                    {
                        currbuild = item;
                        item.transform.position = StaticVar.UIToScreenPos(transform.parent.GetComponent<Canvas>(), BuildToUIImage.gameObject);
                    }
                }
                for (int i = 0; i < currentBuildID.Count; i++)
                {
                    Vector2 tempID1 = (Vector2)currentBuildID[i];
                    Transform temp1 = thirdDefault.parent.Find("Button" + ((tempID1.x - 1) * 32 + tempID1.y - 1));
                    ActiveNormalizeBtn(temp1, tempID1,currbuild);
                }
                CheckIsGround(currbuild, currentBuildID);
            }
        }
    }
    //DIY区域按下下键
    private void GetKeyDownDown()
    {
        //判断物体是否在在DIY下边缘
        bool bottom = false;
        for (int i = 0; i < currentBuildID.Count; i++)
        {
            Vector2 tempID = (Vector2)currentBuildID[i];
            if (tempID.x == 13)
            {
                bottom = true;
            }
        }
        //判断物体是否在二楼下边缘
        bool floorBottom = false;
        for (int i = 0; i < currentBuildID.Count; i++)
        {
            Vector2 tempID = (Vector2)currentBuildID[i];
            if (tempID.x == 6)
            {
                floorBottom = true;
            }
        }
        if (!bottom)
        {
            if (floorBottom)
            {
                //修改物品所在实际位置
                Building currentBuild = new Building();
                foreach (var item in MessageSend.instance.CreatPropertys)
                {
                    if (item.state == BuildingStates.Build)
                    {
                        BuildToUIImage.localPosition -= new Vector3(0, HeightUI * (1 + item.height), 0);
                        currentBuild = item;
                        currentBuild.transform.position = StaticVar.UIToScreenPos(transform.parent.GetComponent<Canvas>(), BuildToUIImage.gameObject);
                    }
                }
                //修改物品当前覆盖区域
                for (int i = 0; i < currentBuildID.Count; i++)
                {
                    Vector2 tempID = (Vector2)currentBuildID[i];
                    Transform temp = thirdDefault.parent.Find("Button" + ((tempID.x - 1) * 32 + tempID.y - 1));
                    UnActiveNormalizeBtn(temp, tempID);
                    tempID.x += (1 + currentBuild.height);
                    currentBuildID[i] = tempID;

                }
                for (int i = 0; i < currentBuildID.Count; i++)
                {
                    Vector2 tempID1 = (Vector2)currentBuildID[i];
                    Transform temp1 = thirdDefault.parent.Find("Button" + ((tempID1.x - 1) * 32 + tempID1.y - 1));
                    ActiveNormalizeBtn(temp1, tempID1, currentBuild);
                }
                CheckIsGround(currentBuild, currentBuildID);
            }
            else
            {
                //修改物品当前覆盖区域
                for (int i = 0; i < currentBuildID.Count; i++)
                {
                    Vector2 tempID = (Vector2)currentBuildID[i];
                    Transform temp = thirdDefault.parent.Find("Button" + ((tempID.x - 1) * 32 + tempID.y - 1));
                    UnActiveNormalizeBtn(temp, tempID);
                    tempID.x += 1;
                    currentBuildID[i] = tempID;

                }
                
                //修改物品映射UI位置
                BuildToUIImage.localPosition -= new Vector3(0, HeightUI, 0);
                Building currbuild = new Building();
                foreach (var item in MessageSend.instance.CreatPropertys)
                {
                    if (item.state == BuildingStates.Build)
                    {
                        currbuild = item;
                        item.transform.position = StaticVar.UIToScreenPos(transform.parent.GetComponent<Canvas>(), BuildToUIImage.gameObject);
                    }
                }
                for (int i = 0; i < currentBuildID.Count; i++)
                {
                    Vector2 tempID1 = (Vector2)currentBuildID[i];
                    Transform temp1 = thirdDefault.parent.Find("Button" + ((tempID1.x - 1) * 32 + tempID1.y - 1));
                    ActiveNormalizeBtn(temp1, tempID1,currbuild);
                }
                CheckIsGround(currbuild, currentBuildID);
            }

        }
    }

    //根据传递的类型刷新背包物体
    public void NeatBag()
    {
        Transform content = transform.Find("Store/Viewport/Content");
        
        //清空按钮附加状态脚本
        foreach (var item in bagPropertys)
        {
            Transform temp = (Transform)item;
            if (temp.GetComponent<DIYBagContro>()!= null)
            {

                temp.GetComponent<Image>().sprite = temp.GetComponent<DIYBagContro>().defaultSprite;
                Destroy(temp.GetComponent<DIYBagContro>());
            }
            GameObject obj = ((Transform)item).Find("Image").gameObject;
            obj.SetActive(false);
            
        }
     
        int n = 1;
        foreach (var item in MessageSend.instance.CurrentHavePropertys)
        {
            if (n<=bagPropertys.Count)
            {
                if (currentType == PropertyType.All)
                {
                    Transform temp = transform.Find("Store/Viewport/Content/Button" + n);
                    temp.gameObject.AddComponent<DIYBagContro>().property = item;
                    //加载图片
                    temp.Find("Image").GetComponent<Image>().sprite = LoadTexture.getInstance().LoadAtlasSprite("Atlas/UI/PropertyIcon/DIYPropertys", item.PropertyName);
                    temp.Find("Image").GetComponent<Image>().raycastTarget = false;
                    temp.Find("Image").GetComponent<Image>().preserveAspect = true;
                    temp.Find("Image").GetComponent<Image>().SetNativeSize();
                    temp.Find("Image").gameObject.SetActive(true);

                    n++;
                }
                else
                {
                    if (item.Type == currentType)
                    {
                        Transform temp = transform.Find("Store/Viewport/Content/Button" + n);
                        temp.gameObject.AddComponent<DIYBagContro>().property = item;
                        //加载图片
                        temp.Find("Image").GetComponent<Image>().sprite = LoadTexture.getInstance().LoadAtlasSprite("Atlas/UI/PropertyIcon/DIYPropertys", item.PropertyName);
                        temp.Find("Image").GetComponent<Image>().raycastTarget = false;
                        temp.Find("Image").GetComponent<Image>().preserveAspect = true;
                        temp.Find("Image").GetComponent<Image>().SetNativeSize();
                        temp.Find("Image").gameObject.SetActive(true);

                        n++;
                    }
                }
            }
        }

        if (n == 1)
        {
            secondDefault.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        }
        else
        {
            secondDefault.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }

    }

    //检测该位置是否可以放置
    public void CheckState2()
    {
        bool CanPut = true;
        //检测该坐标对应的按钮的图片是否是可以放置的
        foreach (var item in currentBuildID)
        {
            Vector2 tempVec = (Vector2)item;
            Transform temp = thirdDefault.parent.Find("Button" + ((tempVec.x - 1) * 32 + tempVec.y - 1));
            if (temp.GetComponent<Image>().sprite == temp.GetComponent<NormalizeBtn>().noPutSprite)
            {
                CanPut = false;
                break;
            }
        }
        if (CanPut)
        {
            canMove = false;
            PutBuild();
        }
    }

    //历史记录读取
    public void LoadDIYData()
    {
        //修改已创建物品对应的基准格属性
        if (MessageSend.instance.CreatPropertysInfo.Count != 0)
        {
            ArrayList builds = new ArrayList();
            foreach (var item in GameObject.Find("DIYCreatPropertys").GetComponentsInChildren<Building>())
            {
                builds.Add(item);
                if (!MessageSend.instance.CreatPropertys.Contains(item))
                {
                    MessageSend.instance.CreatPropertys.Add(item);
                }

            }
            foreach (var item in builds)
            {
                Building b = (Building)item;
                ArrayList IDs = new ArrayList();
                Property property = MessageSend.instance.propertyCfgs[b.propertyID];
                Vector2 temp = MessageSend.instance.PropertyToID[property];
                for (int i = 0; i < b.height; i++)
                {
                    for (int j = 0; j < b.width; j++)
                    {
                        IDs.Add(new Vector2(temp.x + i, temp.y + j));
                    }
                }
                if (!MessageSend.instance.propertyIDs.ContainsKey(b))
                {
                    MessageSend.instance.propertyIDs.Add(b, IDs);
                }
            }
        }
        if (MessageSend.instance.propertyIDs.Count != 0)
        {
            ArrayList IDs = new ArrayList();
            foreach (KeyValuePair<Building, ArrayList> kvp in MessageSend.instance.propertyIDs)
            {
                foreach (var item in kvp.Value)
                {
                    IDs.Add(item);
                }
            }

            foreach (var item in IDs)
            {
                normalizState[(Vector2)item] = false;
            }
            
        }
    }

    //检测已创建的物品当中是否有和当前删除物品重合的物品
    public Building CheckSamePosi()
    {
        foreach (var item in MessageSend.instance.CreatPropertys)
        {
            if (item != reviseBuild)
            {
                if (Mathf.Abs(Vector3.Distance(item.transform.position, reviseBuild.transform.position)) < 0.5f)
                {
                    return item;

                }
            }

        }
        return null;
    }

    //检测贴地物品
    public void CheckIsGround(Building build,ArrayList buildsID)
    {
        bool temp1 = false;
        bool temp2 = false;
        if (MessageSend.instance.propertyCfgs[build.propertyID].Type == PropertyType.Furniture || MessageSend.instance.propertyCfgs[build.propertyID].Type == PropertyType.Wiring)
        {
            foreach (var item in buildsID)
            {
                if (((Vector2)item).x == 6 || ((Vector2)item).x == 13)
                {
                    temp2 = true;
                    break;
                }
            }           
        }
        else
        {
            temp2 = true;
        }
        if (!temp2)
        {
            temp1 = true;
        }
        if (temp1)
        {
            foreach (var item in buildsID)
            {
                Vector2 tempVec = (Vector2)item;
                Transform btn = thirdDefault.parent.Find("Button" + ((tempVec.x - 1) * 32 + tempVec.y - 1));
                btn.GetComponent<Image>().sprite = btn.GetComponent<NormalizeBtn>().noPutSprite;
            }

        }
        
       
    }
    
    //放置物品
    public void PutBuild()
    {
        //修改这几个基准格的状态
        ArrayList temp = new ArrayList();
        foreach (var item in currentBuildID)
        {
            Vector2 tempVec = (Vector2)item;
            Transform tran = thirdDefault.parent.Find("Button" + ((tempVec.x - 1) * 32 + tempVec.y - 1));
            tran.GetComponent<NormalizeBtn>().state = false;
            tran.GetComponent<Image>().sprite = tran.GetComponent<NormalizeBtn>().normalizeSprite;
            normalizState[tempVec] = false;
            tran.gameObject.SetActive(false);
            temp.Add(tempVec);
        }
        //切换物体状态
        foreach (var item in MessageSend.instance.CreatPropertys)
        {
            if (item.state == BuildingStates.Build)
            {
                //已创建物品和覆盖基准格ID对应关系 
                if (MessageSend.instance.propertyIDs.ContainsKey(item))
                {
                    MessageSend.instance.propertyIDs.Remove(item);
                }
                MessageSend.instance.propertyIDs.Add(item, temp);
                item.state = BuildingStates.Normal;
                //保存当前创建物体的位置信息
                Property cfg = MessageSend.instance.propertyCfgs[item.propertyID];
                if (MessageSend.instance.CreatPropertysInfo.ContainsKey(cfg))
                {
                    MessageSend.instance.CreatPropertysInfo.Remove(cfg);
                }
                MessageSend.instance.CreatPropertysInfo.Add(cfg, item.transform.position);
                //保存当前创建物体和对应的基准格
                if (MessageSend.instance.PropertyToID.ContainsKey(cfg))
                {
                    MessageSend.instance.PropertyToID.Remove(cfg);
                }
                MessageSend.instance.PropertyToID.Add(cfg, new Vector2(((Vector2)currentBuildID[0]).x-item.height+1,((Vector2)currentBuildID[0]).y) );
                //修改位置，激活物体
                Vector3 temp1 = StaticVar.UIToScreenPos(transform.parent.GetComponent<Canvas>(), BuildToUIImage.gameObject);
                item.transform.position = new Vector3(temp1.x, temp1.y, 0);
                item.gameObject.SetActive(true);
                //失活UI映射
                BuildToUIImage.localPosition = new Vector3(20, -240, 0);
                BuildToUIImage.gameObject.SetActive(false);
            }
        }
        
        //清空当前占据基准格ID
        currentBuildID.Clear();
        //跳转到上一级默认选项
        StaticVar.CurrentMenu = secondDefault;

    }
    
    /// <summary>
    /// 激活基准格
    /// </summary>
    /// <param name="temp"> 当前基准格</param>
    /// <param name="tempVec"> 当前基准格ID</param>
    /// <param name="currBuild"> 当前基准格所代表的物品</param>
    public void ActiveNormalizeBtn(Transform temp, Vector2 tempVec,Building currBuild)
    {
        //判定是否在门位置
        if (doorIDs.Contains(tempVec))
        {
            temp.GetComponent<Image>().sprite = temp.GetComponent<NormalizeBtn>().noPutSprite;
        }
        else
        {
            //包含该基准格的除当前物品之外的其他物品
            ArrayList havePropertys = new ArrayList();
            foreach (KeyValuePair<Building, ArrayList> kvp in MessageSend.instance.propertyIDs)
            {
                if (kvp.Value.Contains(tempVec))
                {
                    if (kvp.Key != currBuild)
                    {
                        havePropertys.Add(kvp.Key.transform);
                    }

                }
            }
            //判定之前是否有创建物品
            if (MessageSend.instance.propertyIDs.Count == 0)
            {
                temp.GetComponent<Image>().sprite = temp.GetComponent<NormalizeBtn>().canPutSprite;

            }
            else
            {
                //检测该基准格是否被占据
                if (temp.GetComponent<NormalizeBtn>().state)
                {
                    temp.GetComponent<Image>().sprite = temp.GetComponent<NormalizeBtn>().canPutSprite;
                }
                else
                {
                    switch (havePropertys.Count)
                    {
                        case 0:
                            temp.GetComponent<Image>().sprite = temp.GetComponent<NormalizeBtn>().canPutSprite;
                            break;
                        case 1:
                            if (((Transform)havePropertys[0]).GetChild(0).GetComponentInChildren<SpriteRenderer>().sortingOrder == currBuild.transform.GetChild(0).GetComponentInChildren<SpriteRenderer>().sortingOrder)
                            {
                                temp.GetComponent<Image>().sprite = temp.GetComponent<NormalizeBtn>().noPutSprite;
                            }
                            else
                            {

                                temp.GetComponent<Image>().sprite = temp.GetComponent<NormalizeBtn>().canPutSprite;
                            }
                            break;

                        default:
                            temp.GetComponent<Image>().sprite = temp.GetComponent<NormalizeBtn>().noPutSprite;
                            break;
                    }
                }
            }
        }

      

        temp.GetComponent<NormalizeBtn>().state = false;
        normalizState[tempVec] = false;
        temp.gameObject.SetActive(true);
    }


    //失活基准格
    public void UnActiveNormalizeBtn(Transform temp, Vector2 tempVec)
    {
        if (doorIDs.Contains(tempVec))
        {

        }
        else
        {
            //判定是否需要改变状态
            if (MessageSend.instance.propertyIDs.Count == 0)
            {
                temp.GetComponent<NormalizeBtn>().state = true;
                temp.GetComponent<Image>().sprite = temp.GetComponent<NormalizeBtn>().normalizeSprite;
                normalizState[tempVec] = true;
            }
            else
            {
                //失活的时候要看该基准格除了当前物体是否还有其他物体占据
                //有三种情况会失活 ：撤回   删除  移动，但都需要判断该基准格上除了当前控制物体歪是否有其他物体占据。没有则改变状态。有则不需要改变状态

                //创建过程中
                if (reviseBuild == null)
                {
                    ArrayList havePropertys = new ArrayList();
                    foreach (KeyValuePair<Building, ArrayList> kvp in MessageSend.instance.propertyIDs)
                    {
                        if (kvp.Value.Contains(tempVec))
                        {
                            havePropertys.Add(kvp.Key.transform);
                        }
                    }
                    if (havePropertys.Count == 0)
                    {
                        temp.GetComponent<NormalizeBtn>().state = true;
                        normalizState[tempVec] = true;
                    }

                }
                //修改过程中
                else
                {
                    ArrayList havePropertys = new ArrayList();
                    foreach (KeyValuePair<Building, ArrayList> kvp in MessageSend.instance.propertyIDs)
                    {
                        if (kvp.Value.Contains(tempVec))
                        {

                            havePropertys.Add(kvp.Key.transform);
                        }
                    }
                    havePropertys.Add(reviseBuild.transform);
                    if (havePropertys.Count == 1)
                    {
                        temp.GetComponent<NormalizeBtn>().state = true;
                        normalizState[tempVec] = true;
                    }
                }
            }
        }
        
        temp.GetComponent<Image>().sprite = temp.GetComponent<NormalizeBtn>().normalizeSprite;
        temp.gameObject.SetActive(false);
    }
    //开始修改
    public void ClickRevise()
    {
        //修改键(当前选择不在DIY区域的时候可以修改)
        if (!reviseTypes.Contains(StaticVar.CurrentMenu) && StaticVar.CurrentMenu != transform.Find("Revise"))
        {
            if (MessageSend.instance.CreatPropertys.Count != 0)
            {
                StaticVar.CurrentMenu = fouthDefault.parent;
                SetDefault();
            }
        }
    }
    //设置默认选择修改物
    public void SetDefault()
    {
        if (MessageSend.instance.propertyIDs.Count != 0)
        {
            //为默认删除物赋值(选中从上到下第一个已创建的物品)
            Vector2 temp1 = new Vector2();
            foreach (KeyValuePair<Vector2, bool> kvp in normalizState)
            {
                if (!doorIDs.Contains(kvp.Key))
                {
                    if (kvp.Value == false)
                    {
                        temp1 = kvp.Key;
                        break;
                    }
                }
               
            }
             Property tempProperty = new Property();

            foreach (var item in MessageSend.instance.PropertyToID)
            {
                if (item.Value == temp1)
                {
                    tempProperty = item.Key;
                    break;
                }
            }
            foreach (var item in MessageSend.instance.CreatPropertys)
            {
                if (item.propertyID == tempProperty.ID)
                {
                    reviseBuild = item;
                }
            }
        }

        if (reviseBuild == null)
        {
            Debug.LogError("当前修改物为空");
        }

        //修改DIY物品的UI映射
        BuildToUIImage.GetComponent<Image>().sprite = reviseBuild.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
        BuildToUIImage.GetComponent<Image>().SetNativeSize();
        //修正UI映射的位置
        BuildToUIImage.localPosition = Vector3.zero;
        BuildToUIImage.localPosition = StaticVar.WorldPosToUI(transform.parent.GetComponent<Canvas>(), BuildToUIImage.gameObject, reviseBuild.gameObject);
        BuildToUIImage.gameObject.SetActive(true);
        //激活标准格
        foreach (var item in MessageSend.instance.propertyIDs[reviseBuild])
        {
            Transform temp1 = thirdDefault.parent.Find("Button" + ((((Vector2)item).x - 1) * 32 + ((Vector2)item).y - 1));
          
            ActiveNormalizeBtn(temp1,(Vector2)item,reviseBuild);
        }
        CheckIsGround(reviseBuild, MessageSend.instance.propertyIDs[reviseBuild]);
    }
    //挑选修改物体
    public void SelectRevisePro()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //当前选中物品
            List<Building> directionBuild = new List<Building>();
            foreach (var item in MessageSend.instance.CreatPropertys)
            {

                if ((item.transform.position - reviseBuild.transform.position).normalized == Vector3.left)
                {
                    directionBuild.Add(item);
                }
            }
            //挑选一个备用的下一个选中物品
            Building nextPro = new Building();
            //规定方向上有可选择物品
            if (directionBuild.Count != 0)
            {
                nextPro = directionBuild[0];
                foreach (var item in directionBuild)
                {
                    if (Vector3.Distance(item.transform.position, reviseBuild.transform.position) < Vector3.Distance(nextPro.transform.position, reviseBuild.transform.position))
                    {
                        if (Vector3.Distance(item.transform.position, reviseBuild.transform.position) > 0.5f)
                        {
                            nextPro = item;
                        }

                    }
                }
            }

            //规定方向上没有可选择物品
            else
            {
                //找寻当前删除物品左侧最靠近的物品
                foreach (var item in MessageSend.instance.CreatPropertys)
                {
                    if (item.transform.position.x - reviseBuild.transform.position.x < 0)
                    {
                        if (nextPro == null)
                        {
                            nextPro = item;
                        }
                        else
                        {
                            if (Vector3.Distance(item.transform.position, reviseBuild.transform.position) < Vector3.Distance(nextPro.transform.position, reviseBuild.transform.position))
                            {
                                if (Vector3.Distance(item.transform.position, reviseBuild.transform.position) > 0.5f)
                                {
                                    nextPro = item;
                                }
                            }
                        }

                    }

                }

            }
            if (nextPro != null)
            {
                reviseBuild = nextPro;
            }
            //最边缘，切换到相反边缘的物体
            else
            {
                for (int i = 0; i < MessageSend.instance.CreatPropertys.Count; i++)
                {
                    if (nextPro == null)
                    {
                        nextPro = MessageSend.instance.CreatPropertys[i];
                    }
                    else
                    {
                        if (MessageSend.instance.CreatPropertys[i].transform.position.x > nextPro.transform.position.x)
                        {
                            nextPro = MessageSend.instance.CreatPropertys[i];
                        }
                    }
                }
                reviseBuild = nextPro;
            }
            ReviseBuild();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //当前选中物品
            List<Building> directionBuild = new List<Building>();
            foreach (var item in MessageSend.instance.CreatPropertys)
            {

                if ((item.transform.position - reviseBuild.transform.position).normalized == Vector3.right)
                {
                    directionBuild.Add(item);
                }
            }

            //挑选一个备用的下一个选中物品
            Building nextPro = new Building();
            //规定方向上有可选择物品
            if (directionBuild.Count != 0)
            {
                nextPro = directionBuild[0];
                foreach (var item in directionBuild)
                {
                    if (Vector3.Distance(item.transform.position, reviseBuild.transform.position) < Vector3.Distance(nextPro.transform.position, reviseBuild.transform.position))
                    {
                        if (Vector3.Distance(item.transform.position, reviseBuild.transform.position) > 0.5f)
                        {
                            nextPro = item;
                        }
                    }
                }
            }
            //规定方向上没有可选择物品
            else
            {

                //找寻当前删除物品左侧最靠近的物品
                foreach (var item in MessageSend.instance.CreatPropertys)
                {
                    if (item.transform.position.x - reviseBuild.transform.position.x > 0)
                    {
                        if (nextPro == null)
                        {
                            nextPro = item;
                        }
                        else
                        {
                            if (Vector3.Distance(item.transform.position, reviseBuild.transform.position) < Vector3.Distance(nextPro.transform.position, reviseBuild.transform.position))
                            {
                                if (Vector3.Distance(item.transform.position, reviseBuild.transform.position) > 0.5f)
                                {
                                    nextPro = item;
                                }
                            }
                        }

                    }
                }
            }
            if (nextPro != null)
            {
                reviseBuild = nextPro;
            }
            //最边缘，切换到相反边缘的物体
            else
            {
                for (int i = 0; i < MessageSend.instance.CreatPropertys.Count; i++)
                {
                    if (nextPro == null)
                    {
                        nextPro = MessageSend.instance.CreatPropertys[i];
                    }
                    else
                    {
                        if (MessageSend.instance.CreatPropertys[i].transform.position.x < nextPro.transform.position.x)
                        {
                            nextPro = MessageSend.instance.CreatPropertys[i];
                        }
                    }
                }
                reviseBuild = nextPro;
            }
            ReviseBuild();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //当前选中物品
            List<Building> directionBuild = new List<Building>();
            foreach (var item in MessageSend.instance.CreatPropertys)
            {
                if ((item.transform.position - reviseBuild.transform.position).normalized == Vector3.up)
                {
                    directionBuild.Add(item);
                }
            }
            //下一个选中物品
            Building nextPro = new Building();
            //规定方向上有可选择物品
            if (directionBuild.Count != 0)
            {
                nextPro = directionBuild[0];
                foreach (var item in directionBuild)
                {
                    if (Vector3.Distance(item.transform.position, reviseBuild.transform.position) < Vector3.Distance(nextPro.transform.position, reviseBuild.transform.position))
                    {
                        if (Vector3.Distance(item.transform.position, reviseBuild.transform.position) > 0.5f)
                        {
                            nextPro = item;
                        }
                    }
                }
            }
            //规定方向上没有可选择物品
            else
            {

                //找寻当前删除物品左侧最靠近的物品
                foreach (var item in MessageSend.instance.CreatPropertys)
                {
                    if (item.transform.position.y - reviseBuild.transform.position.y > 0)
                    {
                        if (nextPro == null)
                        {
                            nextPro = item;
                        }
                        else
                        {
                            if (Vector3.Distance(item.transform.position, reviseBuild.transform.position) < Vector3.Distance(nextPro.transform.position, reviseBuild.transform.position))
                            {
                                if (Vector3.Distance(item.transform.position, reviseBuild.transform.position) > 0.5f)
                                {
                                    nextPro = item;
                                }
                            }
                        }

                    }
                }
            }
            if (nextPro != null)
            {
                reviseBuild = nextPro;
            }
            //最边缘，切换到相反边缘的物体
            else
            {
                for (int i = 0; i < MessageSend.instance.CreatPropertys.Count; i++)
                {
                    if (nextPro == null)
                    {
                        nextPro = MessageSend.instance.CreatPropertys[i];
                    }
                    else
                    {
                        if (MessageSend.instance.CreatPropertys[i].transform.position.y < nextPro.transform.position.y)
                        {
                            nextPro = MessageSend.instance.CreatPropertys[i];
                        }
                    }
                }
                reviseBuild = nextPro;
            }
            ReviseBuild();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            //当前选中物品
            List<Building> directionBuild = new List<Building>();
            foreach (var item in MessageSend.instance.CreatPropertys)
            {
                if ((item.transform.position - reviseBuild.transform.position).normalized == Vector3.down)
                {
                    directionBuild.Add(item);
                }
            }
            //下一个选中物品
            Building nextPro = new Building();
            //规定方向上有可选择物品
            if (directionBuild.Count != 0)
            {
                nextPro = directionBuild[0];
                foreach (var item in directionBuild)
                {
                    if (Vector3.Distance(item.transform.position, reviseBuild.transform.position) < Vector3.Distance(nextPro.transform.position, reviseBuild.transform.position))
                    {
                        if (Vector3.Distance(item.transform.position, reviseBuild.transform.position) > 0.5f)
                        {
                            nextPro = item;
                        }
                    }
                }
            }
            //规定方向上没有可选择物品
            else
            {

                //找寻当前删除物品左侧最靠近的物品
                foreach (var item in MessageSend.instance.CreatPropertys)
                {
                    if (item.transform.position.y - reviseBuild.transform.position.y < 0)
                    {
                        if (nextPro == null)
                        {
                            nextPro = item;
                        }
                        else
                        {
                            if (Vector3.Distance(item.transform.position, reviseBuild.transform.position) < Vector3.Distance(nextPro.transform.position, reviseBuild.transform.position))
                            {
                                if (Vector3.Distance(item.transform.position, reviseBuild.transform.position) > 0.5f)
                                {
                                    nextPro = item;
                                }
                            }
                        }

                    }
                }
            }
            if (nextPro != null)
            {
                reviseBuild = nextPro;
            }
            //最边缘，切换到相反边缘的物体
            else
            {
                for (int i = 0; i < MessageSend.instance.CreatPropertys.Count; i++)
                {
                    if (nextPro == null)
                    {
                        nextPro = MessageSend.instance.CreatPropertys[i];
                    }
                    else
                    {
                        if (MessageSend.instance.CreatPropertys[i].transform.position.y > nextPro.transform.position.y)
                        {
                            nextPro = MessageSend.instance.CreatPropertys[i];
                        }
                    }
                }
                reviseBuild = nextPro;
            }
            ReviseBuild();
        }
        //确认修改
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            //激活修改按钮组
            fouthDefault.parent.GetComponent<CanvasGroup>().alpha = 1;
            //修正按钮组的位置
            fouthDefault.parent.localPosition = Vector3.zero;
            //判定按钮组的位置是否靠边缘
            bool IsLeft = false;
            foreach (var item in MessageSend.instance.propertyIDs[reviseBuild])
            {
                if (((Vector2)item).y < 4)
                {
                    IsLeft = true;
                    break;
                }
            }
            //靠近左边缘则放在物体右边
            if (IsLeft)
            {
                fouthDefault.parent.localPosition = BuildToUIImage.localPosition - reviseBuild.offsetVec + BuildToUIImage.parent.localPosition + new Vector3((WidthUI * reviseBuild.width / 2 + 60), (HeightUI * reviseBuild.height / 2 - 40), 0);
            }
            else
            {
                fouthDefault.parent.localPosition = BuildToUIImage.localPosition - reviseBuild.offsetVec + BuildToUIImage.parent.localPosition + new Vector3(-(WidthUI * reviseBuild.width / 2 + 60), (HeightUI * reviseBuild.height / 2-40), 0);
            }

            //设置默认选项
            StaticVar.CurrentMenu = fouthDefault;
        }
        //取消修改
        else if (Input.GetKeyDown(KeyCode.X))
        {
            StaticVar.CurrentMenu = secondDefault;
            //恢复UI映射
            BuildToUIImage.localPosition = new Vector3(20, -240, 0);
            BuildToUIImage.gameObject.SetActive(false);
            //激活物体
            reviseBuild.gameObject.SetActive(true);
            //失活基准格
            foreach (var item in normalizState.Keys)
            {
                Transform temp1 = thirdDefault.parent.Find("Button" + ((((Vector2)item).x - 1) * 32 + ((Vector2)item).y - 1));
                temp1.gameObject.SetActive(false);
            }
        }
        //切层
        if (Input.GetKeyDown(KeyCode.B))
        {
            //判定当前修改物所在位置中心是否有不同层物体
            if (CheckSamePosi() != null)
            {
                reviseBuild = CheckSamePosi();
                ReviseBuild();
            }
        }
    }
    //修正修改物体
    public void ReviseBuild()
    {
        //失活基准格
        foreach (var item in normalizState.Keys)
        {
            Transform temp1 = thirdDefault.parent.Find("Button" + ((((Vector2)item).x - 1) * 32 + ((Vector2)item).y - 1));
            temp1.gameObject.SetActive(false);
        }
        //激活当前修改物体的基准格
        foreach (var item in MessageSend.instance.propertyIDs[reviseBuild])
        {
            Transform temp1 = thirdDefault.parent.Find("Button" + ((((Vector2)item).x - 1) * 32 + ((Vector2)item).y - 1));
            ActiveNormalizeBtn(temp1, (Vector2)item,reviseBuild);
        }
        CheckIsGround(reviseBuild, MessageSend.instance.propertyIDs[reviseBuild]);
        BuildToUIImage.localPosition = Vector3.zero;
        BuildToUIImage.localPosition = StaticVar.WorldPosToUI(transform.parent.GetComponent<Canvas>(), BuildToUIImage.gameObject, reviseBuild.gameObject);
        BuildToUIImage.GetComponent<Image>().sprite = reviseBuild.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
        BuildToUIImage.GetComponent<Image>().SetNativeSize();
    }
    
}
