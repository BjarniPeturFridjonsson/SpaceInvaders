using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Helpers
{
    public static class NumberHelper
    {
        public static int ReturnZeroIfNegative(this int i)
        {
            if (i < 0)
            {
                return 0;
            }

            return i;
        }

        public static int MinimumIf(this int orgVal, int minimumValue)
        {
            return orgVal < minimumValue ? minimumValue : orgVal;
        }
    }
}
