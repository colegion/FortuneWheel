using UnityEngine;
using UnityEngine.Serialization;
using static Utilities.CommonFields;
namespace Utilities
{
    [CreateAssetMenu(fileName = "Item", menuName = "Item/ItemConfig")]
    public class ItemConfig : ScriptableObject
    {
        public ItemClass ItemClass;
        public Sprite[] ClassSprites;
        public Sprite ClassPointSprite;
        
        public int CurrentSpriteIndex;

        public Sprite GetRandomItemSprite()
        {
            CurrentSpriteIndex = Random.Range(0, ClassSprites.Length);
            return ClassSprites[CurrentSpriteIndex];
        }

        public Sprite GetSelectedClassSprite()
        {
            return ClassSprites[CurrentSpriteIndex];
        }
    }
}
