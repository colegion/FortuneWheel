namespace Utilities
{
    public static class CommonFields
    {
        public const int SILVER_WHEEL_COEFFICIENT = 5;
        public const int GOLDEN_WHEEL_COEFFICIENT = 30;
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
            Special,
            Cash,
            Bomb
        }
    }
}
