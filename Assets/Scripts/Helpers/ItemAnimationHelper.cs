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
            if (_spawnedObjects.Count == 0) return;
            DOVirtual.DelayedCall(.1f, ()=>
            {
                MoveParticles(target);
            });
        }

        private void MoveParticles(RectTransform target)
        {
            for (int i = 0; i < _spawnedObjects.Count; i++)
            {
                AnimateObject(_spawnedObjects[i], target, i);
            }
        }

        private void AnimateObject(GameObject objectToAnimate, RectTransform target, int delayIndex)
        {
            Sequence animSequence = DOTween.Sequence();
            animSequence.AppendInterval(0.1f * delayIndex);
            animSequence.Append(objectToAnimate.transform.DORotate(new Vector3(0, 0,0), .7f));
            animSequence.Join(objectToAnimate.transform.DOJump(target.position, 100, 1, .8f).SetEase(Ease.OutQuad));
            animSequence.AppendCallback(() =>
            {
                Destroy(objectToAnimate.gameObject);
            });
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
