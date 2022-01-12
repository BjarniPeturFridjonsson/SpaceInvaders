using System.Drawing;

namespace SpaceInvaders.Helpers
{
    public static class PlayerHelper
    {
        public static void DrawPlayer(Graphics g, int playerNr, Point start)
        {
            int x = start.X;
            int y = start.Y;
            using (Brush brush = new SolidBrush(GetPlayerColor(playerNr)))
            {
                g.FillRectangle(brush, new Rectangle(x + 24, y + 00, 04, 4));
                g.FillRectangle(brush, new Rectangle(x + 20, y + 04, 12, 8));
                g.FillRectangle(brush, new Rectangle(x + 04, y + 12, 46, 4));
                g.FillRectangle(brush, new Rectangle(x + 00, y + 16, 52, 16));
            }
        }

        public static void DrawPlayerSmall(Graphics g, int playerNr, Point start)
        {
            int x = start.X;
            int y = start.Y + (playerNr * 2);
            using (Brush brush = new SolidBrush(GetPlayerColor(playerNr)))
            {
                g.FillRectangle(brush, new Rectangle(x + 12, y + 00, 02, 2));
                g.FillRectangle(brush, new Rectangle(x + 10, y + 02, 06, 4));
                g.FillRectangle(brush, new Rectangle(x + 02, y + 06, 23, 2));
                g.FillRectangle(brush, new Rectangle(x + 00, y + 08, 26, 8));
            }
        }

        public static Color GetPlayerColor(int playerNr)
        {
            switch (playerNr)
            {
                case 1: return Color.FromArgb(255, 0, 255, 0);
                case 2: return Color.FromArgb(255, 0, 255, 255);
                default: return Color.FromArgb(255, 255, 0, 255);
            }
        }
    }
}
