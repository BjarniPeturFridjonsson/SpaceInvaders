using SpaceInvaders.Helpers;
using System.Drawing;

namespace SpaceInvaders.Screens
{
    public class GamePausedScreen : BaseScreen
    {
        public override void Paint(Graphics g)
        {
            int x = Common.GetPlayArea().Width / 2 - 50;
            int y = 250;
            int width = 4 * 6;
            int height = 4 * 20;
            
            g.FillRectangle(new SolidBrush(Color.Black), new Rectangle(x-2, y-2, width+4, height+4));
            g.FillRectangle(new SolidBrush(Common.GetGreen()), new Rectangle(x, y, width, height));

            g.FillRectangle(new SolidBrush(Color.Black), new Rectangle(x - 2+40, y - 2, width + 4, height + 4));
            g.FillRectangle(new SolidBrush(Common.GetGreen()), new Rectangle(x +40, y, width, height));
        }
    }
}
