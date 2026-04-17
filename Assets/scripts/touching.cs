using UnityEngine;
using UnityEngine.EventSystems;

public class touchbutton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool ispressed { get; private set; }

    public void OnPointerDown(PointerEventData eventData)
    {
        ispressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ispressed = false;
    }
}