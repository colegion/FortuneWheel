using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Helpers
{
    public class ItemAnimationHelper : MonoBehaviour
    {
        [SerializeField] private GameObject itemObject;

        private List<GameObject> _spawnedObjects;

        private void OnEnable()
        {
            AddListeners();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }

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
            var xOffset = Random.Range(0, 150);
            var yOffset = Random.Range(0, 150);
            var position = transform.position;
            return new Vector2(position.x + xOffset, position.y + yOffset);
        }

        private void MakeParticlesMove(RectTransform target)
        {
            DOVirtual.DelayedCall(.1f, ()=>
            {
                StartCoroutine(MoveParticles(target));
            });
        }

        private IEnumerator MoveParticles(RectTransform target)
        {
            foreach (var itemObj in _spawnedObjects)
            {
                itemObj.transform.DOMove(target.position, 1.2f).SetEase(Ease.OutCubic).OnComplete(() =>
                {
                    Destroy(itemObj.gameObject);
                });

                yield return new WaitForSeconds(0.2f);
            }
        }

        private void AddListeners()
        {
            InventoryItemUIHelper.OnItemParticleMovementNeeded += MakeParticlesMove;
        }

        private void RemoveListeners()
        {
            InventoryItemUIHelper.OnItemParticleMovementNeeded -= MakeParticlesMove;
        }
    }
}
