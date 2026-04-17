using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class touchjoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private Image joystickbackground;
    private Image joystickhandle;
    private Vector2 inputvector;

    public Vector2 Direction => inputvector;

    private void Start()
    {
        joystickbackground = GetComponent<Image>();
        joystickhandle = transform.GetChild(0).GetComponent<Image>(); 
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickbackground.rectTransform, eventData.position, eventData.pressEventCamera, out position))
        {
            position.x = (position.x / joystickbackground.rectTransform.sizeDelta.x);
            position.y = (position.y / joystickbackground.rectTransform.sizeDelta.y);

            inputvector = new Vector2(position.x * 2 - 1, position.y * 2 - 1);
            inputvector = (inputvector.magnitude > 1.0f) ? inputvector.normalized : inputvector;

            joystickhandle.rectTransform.anchoredPosition = new Vector2(
                inputvector.x * (joystickbackground.rectTransform.sizeDelta.x / 2),
                inputvector.y * (joystickbackground.rectTransform.sizeDelta.y / 2));
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputvector = Vector2.zero;
        joystickhandle.rectTransform.anchoredPosition = Vector2.zero;
    }
}