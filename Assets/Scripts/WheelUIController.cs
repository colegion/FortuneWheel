using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

public class WheelUIController : MonoBehaviour
{
    [SerializeField] private Image _wheelBase;
    [SerializeField] private Image _wheelIndicator;

    public void ConfigureWheelSprites(WheelConfig wheel)
    {
        _wheelBase.sprite = wheel.WheelBase;
        _wheelIndicator.sprite = wheel.WheelIndicator;
    }
}
