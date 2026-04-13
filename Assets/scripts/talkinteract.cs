using UnityEngine;

public class talkinteract : interactable
{
    private npcdefinition _npcdefinition;
    private npccharacter _npccharacter;
    private npcmove _npcmove;

    private void Awake()
    {
        _npccharacter = GetComponent<npccharacter>();
        _npcdefinition = _npccharacter.character; 
        _npcmove = GetComponent<npcmove>(); 
    }

    public override void interact(character character)
    {
        dialoguecontainer _dialoguecontainer = _npcdefinition.generaldialogue[Random.Range(0,
            _npcdefinition.generaldialogue.Count)];
            
        _npccharacter.increaserelationship(0.1f);

        if (_npcmove != null)
        {
            _npcmove.stopfordialogue(character.transform);
        }

        gamemanager.instance._dialoguesystem.initialize(_dialoguecontainer);
    }
}