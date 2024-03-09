using UnityEngine;
using static Utilities.CommonFields;
namespace Utilities
{
    [CreateAssetMenu(fileName = "Item", menuName = "Item/ItemConfig")]
    public class ItemConfig : ScriptableObject
    {
        public ItemClass ItemClass;
        public Sprite[] ClassSprites;
        public Sprite ClassPointSprite;
    }
}