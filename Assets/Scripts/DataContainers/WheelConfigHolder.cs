using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Utilities;
using static Utilities.CommonFields;

namespace DataContainers
{
   [CreateAssetMenu(fileName = "WheelConfigContainer", menuName = "Wheel/WheelContainer")]
   public class WheelConfigHolder : SerializedScriptableObject
   {
      public Dictionary<WheelType, WheelConfig> ConfigContainer;
   
      public WheelConfig DecideLevelWheelType(int levelIndex)
      {
         if (levelIndex % GOLDEN_WHEEL_COEFFICIENT == 0) return ConfigContainer[WheelType.Gold];
         if (levelIndex % SILVER_WHEEL_COEFFICIENT == 0) return ConfigContainer[WheelType.Silver];
         return ConfigContainer[WheelType.Bronze];
      }
   }
}
