using SpaceInvaders.Enums;
using System;
using System.Drawing;

namespace SpaceInvaders.Helpers
{
    public static class Common
    {
        public static GameState CurrentRunState = GameState.Unknown;

        public const int ENEMY_PER_ROW_COUNT = 11;
        public const int ENEMY_ROW_MOVE_DOWN = 20;
        public const int SHIELD_COUNT = 4;
        public const int MAX_LIVES = 3;
        public const int GAME_START_SPEED = 6;

        public static int CurrentGameLevel = 0;

        private static Random _rndGenerator = null;
        private static Rectangle _playArea = new Rectangle(1, 1, 816, 675);//orginal size is 224 x 256.

        public static Rectangle GetPlayArea()
        {
            return _playArea;
        }

        public static Color GetGreen()
        {
            return Color.FromArgb(255, 0, 255, 0);
        }

        public static int GetRandomNumberFromZero(int max)
        {
            if (_rndGenerator == null)
            {
                _rndGenerator = new Random();
            }

            if(max < 0)
            {
                max = 0;
            }

            return _rndGenerator.Next(max);
        }
        public static int GetRandomNumber(int min, int max)
        {
            if (_rndGenerator == null)
            {
                _rndGenerator = new Random();
            }

            return _rndGenerator.Next(min,max);
        }
    }
}
