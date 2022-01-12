using SpaceInvaders.Helpers;
using System.Drawing;

namespace SpaceInvaders.Screens
{
    public class LevelPopupScreen : BaseScreen
    {
        FontHelper _helper = new FontHelper();
        LevelHelper _levelHelper = new LevelHelper();
       
        public override void Paint(Graphics g)
        {
            _helper.InitializeGraphics(g);
            Text(g);
        }

        private void Text(Graphics g)
        {
            int x = Common.GetPlayArea().Width / 2 ;
            PointF pos = new PointF(x, 420);

            string name = _levelHelper.GetLevelName(Common.CurrentGameLevel);
            _helper.WriteLargeTextCentered(pos, "GET READY "+ name.ToUpper());

        }
    }
}
