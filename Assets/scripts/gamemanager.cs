using System;
using UnityEditorInternal;
using UnityEngine;

public class gamemanager : MonoBehaviour
{
    public static gamemanager instance;

    private void Awake()
    {
        instance = this;
    }

    public GameObject player;
    public itemcontainer inventorycontainer;
    public itemdraganddropcontroller draganddropcontroller;
    public daytimecontroller timecontroller;
    public dialoguesystem _dialoguesystem;
    public placeableobjectsreferencemanager _placeableobjects;
    public itemlist itemdb;
    public onscreenmessagesystem messagesystem;
    public screentint _screentint;
}
