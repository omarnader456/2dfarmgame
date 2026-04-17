using System;
using TMPro;
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
    [SerializeField] private TextMeshProUGUI text1;
    [SerializeField] private TextMeshProUGUI text2;
    [SerializeField] private TextMeshProUGUI text3;
    [SerializeField] private TextMeshProUGUI text4;



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

    public void toggleinventory()
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

    public void open()
    {
        scrollview.SetActive(true);
        panel.SetActive(true);
        statuspanel.SetActive(true);
        toolbarpanel.SetActive(false);
        storepanel.SetActive(false);
        parentstorepanel.SetActive(false);
        additionalpanel.SetActive(false);
        text1.gameObject.SetActive(false);
        text2.gameObject.SetActive(false);
        text3.gameObject.SetActive(false);
        text4.gameObject.SetActive(false);
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
        text1.gameObject.SetActive(true);
        text2.gameObject.SetActive(true);
        text3.gameObject.SetActive(true);
        text4.gameObject.SetActive(true);
    }
}
