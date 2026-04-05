using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;
using Slider = UnityEngine.UI.Slider;

public class statusbar : MonoBehaviour
{
   [SerializeField] TextMeshProUGUI text;
   [SerializeField]  Slider bar;

   public void set(int curr, int max)
   {
      bar.maxValue = max;
      bar.value = curr;
      text.text = curr.ToString() + "/" + max.ToString();
   }
}
