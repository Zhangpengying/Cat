using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


namespace Fungus
{
    public class MyMenu : MonoBehaviour
    {
        //用于在播放器前存储保存游戏数据的字符串键。如果在同一个Unity项目中定义了多个游戏，那么就为每个保存点使用一个唯一的键。
        [SerializeField] protected string saveDataKey = FungusConstants.DefaultSaveDataKey;

        //包含保存菜单按钮的画布
        [SerializeField] protected CanvasGroup saveMenuGroup;

        //保存按钮
        [SerializeField] protected Button saveButton;

        //加载按钮
        [SerializeField] protected Button loadButton;

        //剧情回顾按钮
        [SerializeField] protected Button reviewButton;

        //游戏设置按钮
        [SerializeField] protected Button setButton;

        //回想按钮
        [SerializeField] protected Button recallButton;

        //用于调试保存数据的可滚动文本字段。文本字段应该在正常使用时禁用
        //[SerializeField] protected ScrollRect clickSavePoint;

        protected static bool MenuActive;

        protected AudioSource clickAudioSource;

        protected LTDescr fadeTween;

        protected static MyMenu instance;

        private static List<string> currentsaveDataKeys;


 

        public static List<string> CurrentsaveDataKeys()
        {
            if (currentsaveDataKeys == null)
            {
                currentsaveDataKeys = new List<string>();
                return currentsaveDataKeys;
            }
            return currentsaveDataKeys;
            
        }


        protected virtual void Awake()
        {
            // Only one instance of SaveMenu may exist
            // 单例
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;

            GameObject.DontDestroyOnLoad(this);

            clickAudioSource = GetComponent<AudioSource>();
        }

        protected virtual void Start()
        {
          
            var saveManager = FungusManager.Instance.SaveManager;

            // Make a note of the current scene. This will be used when restarting the game.
            //记下当前的场景。这将在重新启动游戏时使用。
            if (string.IsNullOrEmpty(saveManager.StartScene))
            {
                saveManager.StartScene = SceneManager.GetActiveScene().name;
            }
         
        }


        protected virtual void Update()
        {
            var saveManager = FungusManager.Instance.SaveManager;
            var menuDialog = new MenuDialog();

            MenuActive = GameObject.Find("MenuWnd").activeSelf;
            bool showSaveAndLoad = MenuActive;


            if (showSaveAndLoad)
            {
                if (saveButton != null)
                {
                    // Don't allow saving unless there's at least one save point in the history,
                    // This avoids the case where you could try to load a save data with 0 save points.
                    //设置保存和加载按钮是否可以被点击
                    saveButton.interactable = saveManager.NumSavePoints > 0 && MenuActive;
                }
                if (loadButton != null)
                {
                    loadButton.interactable = MenuActive;//&& saveManager.SaveDataExists(saveDataKey);
                }
            }
            //设置按钮是否可以被点击
            if (reviewButton != null)
            {
                reviewButton.interactable = MenuActive;
            }
            if (setButton != null)
            {
                setButton.interactable =  MenuActive;
            }
            if (recallButton != null)
            {
                recallButton.interactable = MenuActive;
            }
        }

        protected virtual void OnEnable()
        {
            SaveManagerSignals.OnSavePointAdded += OnSavePointAdded;
        }

        protected virtual void OnDisable()
        {
            SaveManagerSignals.OnSavePointAdded -= OnSavePointAdded;
        }

        protected virtual void OnSavePointAdded(string savePointKey, string savePointDescription)
        {
            var saveManager = FungusManager.Instance.SaveManager;

            if (saveManager.NumSavePoints > 0)
            {
                saveManager.Save(saveDataKey);
            }
        }

        protected void PlayClickSound()
        {
            if (clickAudioSource != null)
            {
                clickAudioSource.Play();
            }
        }
        #region Public methods
        /// <summary>
        /// Gets the string key used to store save game data in Player Prefs. 
        /// 获取用于在播放器Prefs中存储保存游戏数据的字符串键。
        /// </summary>
        public  virtual  string SaveDataKey { get { return saveDataKey ; } }

     



        /// <summary>
        /// Handler function called when the Save button is pressed.
        /// 当保存按钮被按下时执行的方法
        /// </summary>
        public virtual void Save()
        {
            var saveManager = FungusManager.Instance.SaveManager;
           

            if (saveManager.NumSavePoints > 0)
            {
                PlayClickSound();
                
                saveManager.Save(saveDataKey+=FungusManager.Instance.SaveManager.NumSavePoints.ToString());
                SButtonClickListener.AddSave(saveDataKey);

            }
        }

        /// <summary>
        /// Handler function called when the Load button is pressed.
        /// 加载按钮被按下时方法执行
        /// </summary>
        public void Load()
        {
            var saveManager = FungusManager.Instance.SaveManager;

            if (saveManager.SaveDataExists(saveDataKey))
            {
                PlayClickSound();
            }
        }


        /// <summary>
        /// 剧情回顾按钮被按下
        /// </summary>
        public virtual void Review()
        {
            PlayClickSound();

        }

        /// <summary>
        /// 游戏设置按钮被按下
        /// </summary>
        public virtual void Set()
        {
            PlayClickSound();
        }

        /// <summary>
        /// 退出按钮被按下
        /// </summary>
        public virtual void Recall()
        {
            
            PlayClickSound();
           
            #endregion
        }
    }
}
