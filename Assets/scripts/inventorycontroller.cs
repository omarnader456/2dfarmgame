using System;
using UnityEngine;

public class inventorycontroller : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject toolbarpanel;
    [SerializeField] private GameObject statuspanel;
    [SerializeField] private GameObject additionalpanel;
    [SerializeField] private GameObject storepanel;
    [SerializeField] private GameObject parentstorepanel;
    [SerializeField] private GameObject scrollview;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (panel.activeInHierarchy == false)
            {
                open();
            }
            else
            {
                close();
            }
        }
    }

    public void open()
    {
        scrollview.SetActive(true);
        panel.SetActive(true);
        statuspanel.SetActive(true);
        toolbarpanel.SetActive(false);
        storepanel.SetActive(false);
        parentstorepanel.SetActive(false);
        additionalpanel.SetActive(false);
    }

    public void close()
    {
        scrollview.SetActive(false);
        panel.SetActive(false);
        statuspanel.SetActive(false);
        toolbarpanel.SetActive(true); 
        additionalpanel.SetActive(false);
        storepanel.SetActive(false);
        parentstorepanel.SetActive(false);
    }
}
