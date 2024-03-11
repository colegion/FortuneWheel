using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

public class InputListener : MonoBehaviour
{
    [SerializeField] private LevelController levelController;
    private Button _spinButton;

    private void OnValidate()
    {
        _spinButton = GetComponent<Button>();

        if (_spinButton != null)
        {
            _spinButton.onClick.AddListener(OnSpinButtonClicked);
        }
    }

    private void OnEnable()
    {
        AddListeners();
    }

    private void AddListeners()
    {
        LevelController.OnLevelReady += EnableSpinButton;
    }

    private void RemoveListeners()
    {
        LevelController.OnLevelReady -= EnableSpinButton;
        _spinButton.onClick.RemoveListener(OnSpinButtonClicked);
    }

    private void OnDisable()
    {
        RemoveListeners();
    }

    private void EnableSpinButton(CommonFields.WheelType dummy)
    {
        _spinButton.transition = Selectable.Transition.ColorTint;
        _spinButton.interactable = true;
    }

    private void OnSpinButtonClicked()
    {
        _spinButton.transition = Selectable.Transition.None;
        _spinButton.interactable = false;
        levelController.DecideWheelOutcome();
    }
}
