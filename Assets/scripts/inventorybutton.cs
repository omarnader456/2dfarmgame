using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class inventorybutton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image highlight;
    itempanel _itempanel;

    private int myindex;

    public void setindex(int index)
    {
        myindex = index;
    }

    public void setitempanel(itempanel source)
    {
        _itempanel = source;
    }
    public void set(itemslot slot)
    {
        icon.sprite = slot.itm.icon;
        icon.gameObject.SetActive(true);

        if (slot.itm.stackable == true)
        {
            text.gameObject.SetActive(true);
            text.text = slot.count.ToString();
        }
        else
        {
            text.gameObject.SetActive(false);
        }
    }

    public void clean()
    {
        icon.sprite = null;
        icon.gameObject.SetActive(false);
        text.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _itempanel.onclick(myindex);
    }

    public void highlighter(bool b)
    {
        highlight.gameObject.SetActive(b);
    }
}
