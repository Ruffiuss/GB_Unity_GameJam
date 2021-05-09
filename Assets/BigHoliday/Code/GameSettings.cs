using UnityEngine;


namespace BigHoliday
{
    public static class GameSettings
    {
        public const float PLAYER_WALK_SPEED = 5.0f;

        public const int RANDOM_EVENT_MINVALUE = 2;
        public const int RANDOM_EVENT_MAXVALUE = 10;
        public const int RANDOM_EVENT_REPEAT_RATE = 1;
        
        public const string TOOLS_TIP_TEXT = "Key= Z, Vantuz = X, Paper = V";

        public const KeyCode PLAYER_TOOL1 = KeyCode.Z;
        public const KeyCode PLAYER_TOOL2 = KeyCode.X;
        public const KeyCode PLAYER_TOOL3 = KeyCode.V;
    }
}