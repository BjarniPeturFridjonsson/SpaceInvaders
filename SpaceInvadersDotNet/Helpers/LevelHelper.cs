namespace SpaceInvaders.Helpers
{
    public class LevelHelper
    {
        public string GetLevelName(int level)
        {
            if (level > 18)
            {
                return "Palladin level " + (level - 18);
            }

            switch (level)
            {
                case 0: return "Ensign";
                case 1: return "Watchdog";
                case 2: return "Watchman";
                case 3: return "Bodyguard";
                case 4: return "Warrior";
                case 5: return "Veteran";
                case 6: return "Keeper";
                case 7: return "Protector";
                case 8: return "Defender";
                case 9: return "Preserver";
                case 10: return "Warden";
                case 11: return "Guardian";
                case 12: return "Guardian Angel";
                case 13: return "Champion";
                case 14: return "Crusader";
                case 15: return "Knight";
                case 16: return "Chevalier";
                case 17: return "Justiciar";
                case 18: return "Hero";
            }

            return "Wretched fool";
        }
    }
}
