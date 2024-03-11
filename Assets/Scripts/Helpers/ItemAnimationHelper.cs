using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Helpers
{
    public class ItemAnimationHelper : MonoBehaviour
    {
        [SerializeField] private GameObject itemObject;

        private List<GameObject> _spawnedObjects;
        
        
        [ContextMenu("Test spawning")]
        public void InstantiateItemObjects(int amount, Sprite itemSprite)
        {
            _spawnedObjects = new List<GameObject>();
            for (int i = 0; i < amount; i++)
            {
                var tempObj = Instantiate(itemObject, transform);
                tempObj.GetComponent<Image>().sprite = itemSprite;
                tempObj.transform.rotation = Quaternion.Euler(0, 0, GetRandomRotation());
                _spawnedObjects.Add(tempObj);
            }
        }

        private float GetRandomRotation()
        {
            return Random.Range(0, 360);
        }

        private Vector2 GetRandomTransform()
        {
            var xOffset = Random.Range(0, 15);
            var yOffset = Random.Range(0, 15);
            var position = transform.position;
            return new Vector2(position.x + xOffset, position.y + yOffset);
        }
    }
}
