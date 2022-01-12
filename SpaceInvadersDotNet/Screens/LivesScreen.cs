using SpaceInvaders.Helpers;
using System.Collections.Generic;
using System.Drawing;

namespace SpaceInvaders.Screens
{
    public class LivesScreen : BaseScreen
    {
        private Dictionary<int, int> _lives = new Dictionary<int, int>();
        private FontHelper _fontHelper = new FontHelper();

        public void Died(int playerId)
        {
            _lives[playerId] = _lives[playerId] - 1;
            if (_lives[playerId] < 0)
            {
                _lives[playerId] = 0;
            }
        }

        public override void Paint(Graphics g)
        {
            int x = 0;
            int y = Common.GetPlayArea().Height + 20;
            string text = "LIVES:";
            _fontHelper.InitializeGraphics(g);

            _fontHelper.WriteLargeText(new Point(10, y), text);
            x = _fontHelper.MesureTextLarge(text) + 10;

            PaintLives(g, 1, new Point(x + 10, y));
            PaintLives(g, 2, new Point(Common.GetPlayArea().Width/2 -45, y));
            PaintLives(g, 3, new Point(Common.GetPlayArea().Width - 100, y));
        }

        private int GetLivesSafe(int playerId)
        {
            if (_lives.ContainsKey(playerId))
            {
                return _lives[playerId];
            }
            return -1;
        }

        private void PaintLives(Graphics g, int playerId, Point start)
        {
            int lives = GetLivesSafe(playerId);
            if (lives < 0)
            {
                start.Y += 4;
                _fontHelper.InitializeGraphics(g);
                _fontHelper.WriteMediumText(start, "CREDITS:1", PlayerHelper.GetPlayerColor(playerId));
                return;
            }

            if(lives == 0)
            {
                start.Y += 4;    
                _fontHelper.InitializeGraphics(g);
                _fontHelper.WriteMediumText(start, "GAME OVER", PlayerHelper.GetPlayerColor(playerId));
                return;
            }

            for (int ii = 0; ii < lives; ii++)
            {
                PlayerHelper.DrawPlayerSmall(g, playerId, new Point(start.X + (30 * ii), start.Y ));
            }
        }

        public int GetLives(int playerId)
        {
            if (!_lives.ContainsKey(playerId))
            {
                _lives.Add(playerId, Common.MAX_LIVES);
            }

            return _lives[playerId];
        }
    
        public int GetLives()
        {
            int ret = 0;
            foreach (var id in _lives.Keys)
            {
                ret += _lives[id];
            }
            return ret;
        }

        public void ResetLives(int playerId)
        {
            if (!_lives.ContainsKey(playerId))
            {
                _lives.Add(playerId, Common.MAX_LIVES);
            }
            else
            {
                _lives[playerId] = Common.MAX_LIVES;
            }
        }

        public void ResetAll()
        {
            _lives = new Dictionary<int, int>();
        }
    }
}