using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Helpers
{
   public class InventoryItemUIHelper : MonoBehaviour
   {
      [SerializeField] private Image itemImage;
      [SerializeField] private TextMeshProUGUI itemAmount;

      private int _currentAmount;
      
      public void ConfigureItemUI(Sprite itemSprite, int amount)
      {
         itemImage.sprite = itemSprite;
         itemAmount.text = $"x{amount}";
         _currentAmount = amount;
      }

      public void IncreaseItemAmount(int amount)
      {
         _currentAmount += amount;
         itemAmount.text = $"x{_currentAmount}";
      }
   }
}
