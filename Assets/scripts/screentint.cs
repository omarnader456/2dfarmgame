using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class screentint : MonoBehaviour
{

     Image image;
     private float f;
     public float speed = 0.5f;

     private void Awake()
     {
         image = GetComponent<Image>();
         image.color = new Color(0,0,0,0);
     }

     
    public void tint()
    {
        StopAllCoroutines();
        f = 0f;
        StartCoroutine(tintscreen());
    }

    public void untint()
    {
        StopAllCoroutines();
        f = 1f;
        StartCoroutine(untintscreen());
    }

    private IEnumerator tintscreen()
    {
        while (f < 1f)
        {
            f += Time.deltaTime * speed;
            f = Mathf.Clamp(f, 0, 1f);
            image.color = new Color(0,0,0,f);
            yield return new WaitForEndOfFrame();
        }
    }
    
    private IEnumerator untintscreen()
    {
        while (f > 0)
        {
            f -= Time.deltaTime * speed;
            f = Mathf.Clamp(f, 0, 1f);
            image.color = new Color(0,0,0,f);
            yield return new WaitForEndOfFrame();
        }
    }
}
