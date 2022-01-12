using SpaceInvaders.Helpers;
using System;
using System.Drawing;

namespace SpaceInvaders.Screens
{
    public class CurrentLevelScreen : BaseScreen
    {
        FontHelper _helper = new FontHelper();
        PointF pos = new PointF(Common.GetPlayArea().Width / 2, 50);
        public override void Paint(Graphics g)
        {
            _helper.InitializeGraphics(g);
            _helper.WriteLargeTextCentered(pos, "LEVEL:" + Common.CurrentGameLevel);
        }
    }
}
