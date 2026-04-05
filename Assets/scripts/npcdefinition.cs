using System;
using System.Collections.Generic;
using UnityEngine;

public enum gender
{
    Male,
    Female
}

[Serializable]
public class portraitcollection
{
    public Texture2D normal;
    public Texture2D suprised;
    public Texture2D confused;
    public Texture2D angry;
}

[CreateAssetMenu( menuName = "Data/npccharacter")]
public class npcdefinition : ScriptableObject
{
    public string name = "name";
    public gender _gender = gender.Male;
    public portraitcollection portrait;
    public GameObject characterprefab;

    [Header("interaction")]
    public List<item> likeditems;
    public List<item> dislikeditems;

    [Header("schedule")] 
    public string schedule;

    [Header("dialogue")] 
    public List<dialoguecontainer> generaldialogue;
    
}
