using System;
using System.Collections.Generic;
using UnityEngine;
using Fungus;




public class GameManager : MonoBehaviour
{

    public  GameObject obj;
    void Awake()
    {
        var aa = FungusManager.Instance.SaveManager;
        //DontDestroyOnLoad(this);
        WindowManager.instance.Initialize();
        Initializesou();
        //WindowManager.instance.obj1 = obj;
    }

    private void Start()
    {
       
        BGMContro._instance.PlayMusic(0);
    }


    void Update()
    {
        
        float dt = Time.deltaTime;
        WindowManager.instance.Update(dt);
    }

    public AudioSource myAutio;
    public void Initializesou()
    {
        myAutio = GameObject.Find("FungusManager").GetComponent<AudioSource>();
        myAutio.volume = 0.5f;
    }
}
