using System;
using UnityEngine;
using UnityEngine.EventSystems; 

public class characterinteractcontroller : MonoBehaviour
{
    private charactercontroller2d charactercontroller;
    private Rigidbody2D rigidbody;
    [SerializeField] private float offsetdistance = 1f;
    [SerializeField] private float interactablearea = 2f;
    private character _character;
    [SerializeReference] highlightcontroller highlightcontroller;

    private void Awake()
    {
        charactercontroller = GetComponent<charactercontroller2d>();
        rigidbody = GetComponent<Rigidbody2D>();
        _character = GetComponent<character>();
    }

    private void interact()
    {
        Vector2 position = rigidbody.position + charactercontroller.lastmotionvector * offsetdistance;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, interactablearea);
        
        foreach (Collider2D c in colliders)
        {
            interactable hit = c.GetComponent<interactable>();
            if (hit is not null)
            {
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

        if (Input.GetMouseButtonDown(0) && Input.touchCount == 0) 
        {
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            {
                return; 
            }
            checktouchinteraction(Input.mousePosition);
        }

        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);
                
                if (touch.phase == TouchPhase.Began)
                {
                    if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                    {
                        continue; 
                    }

                    checktouchinteraction(touch.position);
                }
            }
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

    private void checktouchinteraction(Vector3 screenPosition)
    {
        Vector3 tapPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        Vector2 tapPosition2D = new Vector2(tapPosition.x, tapPosition.y);

        Collider2D[] hitColliders = Physics2D.OverlapPointAll(tapPosition2D);

        foreach (Collider2D hitCollider in hitColliders)
        {
            interactable tappedObject = hitCollider.GetComponent<interactable>();

            if (tappedObject != null)
            {
                Vector2 interactCenter = rigidbody.position + charactercontroller.lastmotionvector * offsetdistance;
                
                Collider2D[] colliders = Physics2D.OverlapCircleAll(interactCenter, interactablearea);
                bool inRange = false;
                
                foreach (Collider2D c in colliders)
                {
                    if (c == hitCollider) 
                    {
                        inRange = true;
                        break;
                    }
                }

                float absoluteDistance = Vector2.Distance(rigidbody.position, hitCollider.transform.position);

                if (inRange || absoluteDistance <= interactablearea * 1.5f)
                {
                    Debug.Log($"Touch interacted directly with: {hitCollider.gameObject.name}");
                    tappedObject.interact(_character);
                    return; 
                }
                else
                {
                    Debug.Log("Tapped an interactable, but it is too far away.");
                }
            }
        }
    }
}