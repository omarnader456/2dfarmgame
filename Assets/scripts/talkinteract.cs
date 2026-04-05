using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class talkinteract : interactable
{
    private npcdefinition _npcdefinition;
    npccharacter _npccharacter;

    private void Awake()
    {
        _npccharacter = GetComponent<npccharacter>();
        _npcdefinition = GetComponent<npccharacter>().character;
    }

    public override void interact(character character)
    {
        dialoguecontainer _dialoguecontainer = _npcdefinition.generaldialogue[Random.Range(0,
            _npcdefinition.generaldialogue.Count)];
        _npccharacter.increaserelationship(0.1f);
        gamemanager.instance._dialoguesystem.initialize(_dialoguecontainer);
    }
}
