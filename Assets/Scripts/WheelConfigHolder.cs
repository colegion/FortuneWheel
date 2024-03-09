using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
using static Utilities.CommonFields;

[CreateAssetMenu(fileName = "WheelConfigContainer", menuName = "WheelContainer")]
public class WheelConfigHolder : ScriptableObject
{
   public Dictionary<WheelType, WheelConfig> ConfigContainer;
}
