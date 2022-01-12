using SpaceInvaders.Enums;
using System.Drawing;

namespace SpaceInvaders.Helpers
{
    public static class EnemyHelper
    {
        public static Image GetEnemyAnimationA(Characters enemy)
        {
            Image ret = null;
            switch (enemy)
            {
                case Characters.Octopus:
                    ret = Properties.Resources.invader_3_1;
                    break;
                case Characters.Crab:
                    ret = Properties.Resources.invader_2_1;
                    break;
                case Characters.Squid:
                    ret = Properties.Resources.invader_1_1;
                    break;
            }
            return ret;
        }

        public static Image GetEnemyAnimationB(Characters enemy)
        {
            Image ret = null;
            switch (enemy)
            {
                case Characters.Octopus:
                    ret = Properties.Resources.invader_3_2;
                    break;
                case Characters.Crab:
                    ret = Properties.Resources.invader_2_2;
                    break;
                case Characters.Squid:
                    ret = Properties.Resources.invader_1_2;
                    break;

            }
            return ret;
        }

        public static int GetMarginX(Characters enemy)
        {
            switch (enemy)
            {
                case Characters.Crab:
                    return 2;
                case Characters.Squid:
                    return 8;
                default:
                    return 0;
            }
        }
    }
}

