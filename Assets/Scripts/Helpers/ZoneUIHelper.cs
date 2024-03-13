using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Controllers;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace Helpers
{
    public class ZoneUIHelper : MonoBehaviour
    {
        [SerializeField] private HorizontalLayoutGroup zoneLayoutGroup;
        [SerializeField] private Image zoneFrame;
        [SerializeField] private GameObject zoneItem;

        private List<GameObject> _spawnedZones = new List<GameObject>();
        private int _currentZoneIndex = 0;

        private void OnEnable()
        {
            AddListeners();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }

        private void Start()
        {
            for (int i = 0; i < 50; i++)
            {
                var tempZone = Instantiate(zoneItem, zoneLayoutGroup.transform);
                var zoneTextField = tempZone.GetComponentInChildren<TextMeshProUGUI>();
                zoneTextField.text = $"{i}";
                zoneTextField.color = DecideZoneColor(i);
                _spawnedZones.Add(tempZone);
            }
            zoneLayoutGroup.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
        }

        private void SpawnNewZone(CommonFields.WheelType type)
        {
            if (_currentZoneIndex == 0) return;
            var zone = Instantiate(zoneItem, zoneLayoutGroup.transform);
            var zoneField = zone.GetComponentInChildren<TextMeshProUGUI>();
            zoneField.text = $"{_spawnedZones.Count}";
            zoneField.color = DecideZoneColor(_spawnedZones.Count);
            _spawnedZones.Add(zone);
            _currentZoneIndex++;
            PositionZoneFrame();
        }

        private void PositionZoneFrame()
        {
            zoneLayoutGroup.GetComponent<RectTransform>().localPosition =
                new Vector2(zoneLayoutGroup.GetComponent<RectTransform>().localPosition.x - 100, 0);
            //zoneLayoutGroup.transform.DOMoveX(zoneLayoutGroup.transform.position.x - 100, .5f)
              //  .SetEase(Ease.Linear);
        }

        private Color DecideZoneColor(int zoneIndex)
        {
            if (zoneIndex % 30 == 0) return CommonFields.GOLDEN_ZONE_COLOR;
            if (zoneIndex % 5 == 0) return CommonFields.SILVER_ZONE_COLOR;
            return CommonFields.BRONZE_ZONE_COLOR;
        }

        private void AddListeners()
        {
            LevelController.OnLevelReady += SpawnNewZone;
        }

        private void RemoveListeners()
        {
            LevelController.OnLevelReady -= SpawnNewZone;
        }
    }
}
