using SpaceInvaders.Enums;
using SpaceInvaders.GameItems;
using SpaceInvaders.Helpers;
using System.Drawing;

namespace SpaceInvaders.Screens
{
    public class StartScreen : BaseScreen
    {
        private FontHelper _fontHelper = new FontHelper();

        private Enemy _squid = null;
        private Enemy _octopus = null;
        private Enemy _crab = null;
        private Alien _alien = null;

        private long _tickCount = 0;

        public StartScreen()
        {
            _squid = new Enemy(Characters.Squid);
            _crab = new Enemy(Characters.Crab);
            _octopus = new Enemy(Characters.Octopus);
            _alien = new Alien();

            _squid.Start();
            _octopus.Start();
            _crab.Start();
        }

        public override void Paint(Graphics g)
        {
            _tickCount++;
            StartSceen(g);
            _squid.DrawImage(g);
            _octopus.DrawImage(g);
            _crab.DrawImage(g);
            _alien.DrawImage(g);

            if (_tickCount % 40 != 0)
            {
                return;
            }
            _squid.AnimateOnly();
            _octopus.AnimateOnly();
            _crab.AnimateOnly();
            _alien.AnimateOnly();
        }

        private void StartSceen(Graphics g)
        {
            int startY = 50;
            int startX = 450;
            _fontHelper.InitializeGraphics(g);

            _fontHelper.WriteLargeTextCentered(new PointF(startX, startY += 80), "PLAY");
            _fontHelper.WriteLargeTextCentered(new PointF(startX, startY += 40), "SPACE INVADERS");
            _fontHelper.WriteLargeTextCentered(new PointF(startX, startY += 80), "* SCORE ADVANCE TABLE *");
            _fontHelper.WriteLargeTextCentered(new PointF(startX, startY += 50), "= ? MYSTERY");
            _fontHelper.WriteLargeTextCentered(new PointF(startX, startY += 50), "= 30 POINTS");
            _fontHelper.WriteLargeTextCentered(new PointF(startX, startY += 50), "= 20 POINTS");
            _fontHelper.WriteLargeTextCentered(new PointF(startX, startY += 50), "= 10 POINTS");

            _alien.Start();
            _alien.Position = new Point(startX - 200, startY - 155);
            _squid.Position = new Point(startX - 180 + EnemyHelper.GetMarginX(Characters.Squid), startY - 115);
            _octopus.Position = new Point(startX - 180, startY - 60);
            _crab.Position = new Point(startX - 180 + EnemyHelper.GetMarginX(Characters.Crab), startY - 10);
            
            _fontHelper.WriteTextFadeOut(new PointF(530, 650), "PRESS SHOOT TO START");


            startX = 50;
            startY = 30;
            _fontHelper.WriteMediumText(new PointF(startX - 30, startY), "PLAYER ONE KEYS");
            _fontHelper.WriteMediumText(new PointF(startX, startY += 20), " LEFT = A");
            _fontHelper.WriteMediumText(new PointF(startX, startY += 20), "RIGHT = B");
            _fontHelper.WriteMediumText(new PointF(startX, startY += 20), "SHOOT = W");

            startX = 640;
            startY = 30;
            _fontHelper.WriteMediumText(new PointF(startX - 30, startY), "PLAYER TWO KEYS");
            _fontHelper.WriteMediumText(new PointF(startX, startY += 20), " LEFT = <-");
            _fontHelper.WriteMediumText(new PointF(startX, startY += 20), "RIGHT = ->");
            _fontHelper.WriteMediumText(new PointF(startX, startY += 20), "SHOOT = ^");

            startX = 50;
            startY = 600;
            _fontHelper.WriteMediumText(new PointF(startX - 30, startY), "PLAYER THREE KEYS");
            _fontHelper.WriteMediumText(new PointF(startX, startY += 20), " LEFT = H");
            _fontHelper.WriteMediumText(new PointF(startX, startY += 20), "RIGHT = K");
            _fontHelper.WriteMediumText(new PointF(startX, startY += 20), "SHOOT = U");
        }
    }
}
