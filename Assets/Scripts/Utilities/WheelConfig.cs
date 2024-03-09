using UnityEngine;
using static Utilities.CommonFields;

namespace Utilities
{
    [CreateAssetMenu(fileName = "Wheel", menuName = "WheelConfig")]
    public class WheelConfig : ScriptableObject
    {
        public Sprite WheelBase;
        public Sprite WheelIndicator;
    }
}
