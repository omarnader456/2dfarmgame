using UnityEngine;

public class highlightcontroller : MonoBehaviour
{
    [SerializeField]  GameObject highlighter;
    GameObject currenttarget;

    public void highlight(GameObject target)
    {
        if (currenttarget == target && highlighter.activeSelf)
        {
            return;
        }
        currenttarget = target;
        Vector3 position = target.transform.position + Vector3.up * 0.5f;
        highlight(position);
    }

    public void highlight(Vector3 position)
    {
        highlighter.SetActive(true);
        highlighter.transform.position = position;
    }

    public void hide()
    {
        currenttarget = null;
        highlighter.SetActive(false);
    }
}
