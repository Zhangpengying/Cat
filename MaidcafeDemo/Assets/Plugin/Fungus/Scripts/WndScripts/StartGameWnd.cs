using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGameWnd : MonoBehaviour
{
    public Button m_SkipButton;
    // Start is called before the first frame update
    void Start()
    {
        MyBlockNameIO.instance.Read();
        m_SkipButton.gameObject.SetActive(SavePoint.SavepointName.Count > 0);
        m_SkipButton.onClick.AddListener(()=> { SceneManager.LoadScene(1); });
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene(1);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
