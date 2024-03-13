using System;
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
      public static event Action<RectTransform> OnItemParticleMovementNeeded;
      public void ConfigureItemUI(Sprite itemSprite, int amount, bool gameEnded = false)
      {
         if(!gameEnded) RaiseTrailEvent();
         itemImage.sprite = itemSprite;
         itemAmount.text = $"x{amount}";
         _currentAmount = amount;
      }

      public void IncreaseItemAmount(int amount)
      {
         RaiseTrailEvent();
         _currentAmount += amount;
         itemAmount.text = $"x{_currentAmount}";
      }

      private void RaiseTrailEvent()
      {
         OnItemParticleMovementNeeded?.Invoke(gameObject.GetComponent<RectTransform>());
      }
   }
}
