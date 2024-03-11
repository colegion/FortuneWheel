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
   
      public WheelConfig DecideLevelWheelType(WheelType type)
      {
         return ConfigContainer[type];
      }
   }
}
