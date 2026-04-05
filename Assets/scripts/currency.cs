using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class currency : MonoBehaviour
{
   [SerializeField] private int amount;
   [SerializeField] private TextMeshProUGUI text;

   private void Start()
   {
      amount = 10000;
      updatetext();
   }

   private void updatetext()
   {
      text.text = amount.ToString();
   }

   public void add(int moneygained)
   {
      amount += moneygained;
      updatetext();
   }

   public bool check(int totalprice)
   {
      return amount >= totalprice;
   }

   public void decrease(int totalprice)
   {
      amount -= totalprice;
      if (amount < 0)
      {
         amount = 0;
      }
      updatetext();
   }
}
