using System;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class characterinteractcontroller : MonoBehaviour
{
    private charactercontroller2d charactercontroller;
    private Rigidbody2D rigidbody;
    [SerializeField] private float offsetdistance = 1f;
    [SerializeField] private float interactablearea = 2f;
    private character _character;
    [SerializeReference]  highlightcontroller highlightcontroller;

    private void Awake()
    {
        charactercontroller = GetComponent<charactercontroller2d>();
        rigidbody = GetComponent<Rigidbody2D>();
        _character = GetComponent<character>();
    }
    private void interact()
    {
        Debug.Log("1.  Right-Click  interact pressed.");
        
        Vector2 position = rigidbody.position + charactercontroller.lastmotionvector * offsetdistance;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, interactablearea);
        
        Debug.Log($"2. Interaction circle found {colliders.Length} colliders.");

        foreach (Collider2D c in colliders)
        {
            Debug.Log($"3. Checking collider on GameObject: {c.gameObject.name}");
            
            interactable hit = c.GetComponent<interactable>();
            if (hit is not null)
            {
                Debug.Log($"4. interactable script found on: {c.gameObject.name}  interact signal sent.");
                hit.interact(_character);
                break;
            }
        }
    }

    private void Update()
    {
        check();
        if (Input.GetMouseButtonDown(1))
        {
            interact();
        }
    } 
    private void check()
    {
        Vector2 position = rigidbody.position + charactercontroller.lastmotionvector * offsetdistance;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, interactablearea);
    
        
        bool foundSomething = false;

        foreach (Collider2D c in colliders)
        {
            interactable hit = c.GetComponent<interactable>();
            if (hit is not null)
            {
                highlightcontroller.highlight(hit.gameObject);
                foundSomething = true;
                break; 
            }
        } 
        if (!foundSomething)
        {
            highlightcontroller.hide();
        }
    }
}

