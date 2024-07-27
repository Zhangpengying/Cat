using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTag : MonoBehaviour
{
    private List<GameObject> m_panel = new List<GameObject>();
    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            m_panel.Add(transform.GetChild(i).gameObject);
            transform.GetChild(i).gameObject.SetActive(false);
        }
        
      
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("CameraView"))
        {
            foreach (var item in m_panel)
            {
                item.SetActive(true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("CameraView"))
        {
            foreach (var item in m_panel)
            {
                item.SetActive(false);
            }
        }
    }
}
