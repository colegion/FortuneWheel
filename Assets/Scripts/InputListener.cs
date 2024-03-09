using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputListener : MonoBehaviour
{
    [SerializeField] private LevelController _levelController;
    private Button _spinButton;

    private void OnValidate()
    {
        _spinButton = GetComponent<Button>();

        if (_spinButton != null)
        {
            _spinButton.onClick.AddListener(OnSpinButtonClicked);
        }
    }

    private void OnDisable()
    {
        _spinButton.onClick.RemoveListener(OnSpinButtonClicked);
    }

    private void OnSpinButtonClicked()
    {
        
    }
}
