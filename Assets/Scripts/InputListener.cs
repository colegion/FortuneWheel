using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using Helpers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Utilities;

public class InputListener : MonoBehaviour
{
    [SerializeField] private LevelController levelController;
    [SerializeField]private Button spinButton;
    [SerializeField]private Button restartButton;

    private void OnValidate()
    {
        if (spinButton != null)
        {
            spinButton.onClick.AddListener(OnSpinButtonClicked);
        }
        
        restartButton.onClick.AddListener(RestartGame);
    }

    private void OnEnable()
    {
        AddListeners();
    }
    
    
    private void RestartGame()
    {
        restartButton.gameObject.SetActive(false);
        SceneManager.LoadScene("SampleScene");
    }

    private void EnableRestartButton()
    {
        restartButton.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        RemoveListeners();
    }

    private void EnableSpinButton(CommonFields.WheelType dummy)
    {
        spinButton.transition = Selectable.Transition.ColorTint;
        spinButton.interactable = true;
    }

    private void OnSpinButtonClicked()
    {
        spinButton.transition = Selectable.Transition.None;
        spinButton.interactable = false;
        levelController.DecideWheelOutcome();
    }
    
    private void AddListeners()
    {
        LevelController.OnLevelReady += EnableSpinButton;
        ItemCardUIHelper.OnRestartButtonNeeded += EnableRestartButton;
    }

    private void RemoveListeners()
    {
        LevelController.OnLevelReady -= EnableSpinButton;
        ItemCardUIHelper.OnRestartButtonNeeded -= EnableRestartButton;
        spinButton.onClick.RemoveListener(OnSpinButtonClicked);
    }
}
