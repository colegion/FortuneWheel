using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public static class CommonFields
    {
        public const int SILVER_WHEEL_COEFFICIENT = 5;
        public const int GOLDEN_WHEEL_COEFFICIENT = 30;
        public const int WHEEL_SLICE_COUNT = 8;
        public static readonly Color BRONZE_ZONE_COLOR = new (1, 1, 1, 1);
        public static readonly Color SILVER_ZONE_COLOR = new (0, 1, 0, 1);
        public static readonly Color GOLDEN_ZONE_COLOR = new(255, 215, 0, 1);

        public static Dictionary<WheelType, int> RewardAmountFactors = new Dictionary<WheelType, int>()
        {
            { WheelType.Bronze, 5 },
            { WheelType.Silver, 20 },
            { WheelType.Gold, 50 }
        };

        public enum WheelType
        {
            Bronze = 0,
            Silver,
            Gold
        }

        public enum ItemClass
        {
            Knife = 0,
            Shotgun,
            Smg,
            Rifle,
            Sniper,
            Wearable,
            Cash,
            Special,
            Bomb
        }
    }
}
