using SpaceInvaders.Enums;
using SpaceInvaders.Helpers;
using System.Drawing;

namespace SpaceInvaders.GameItems
{
    public class Torpedo : BaseItem
    {
        private int _speed = 20;
        private bool _isRunning = false;
        public int iCounter = 0;

        public Torpedo()
        {
            ImageBounds.Width = 4;
            ImageBounds.Height = 16;
        }

        public bool IsRunning()
        {
            return _isRunning;
        }

        public void Launch(Point startPos)
        {
            if (_isRunning || Common.CurrentRunState != GameState.Running)
            {
                return;
            }
            Position = startPos;
            _isRunning = true;
        }

        public void Reset()
        {
            _isRunning = false;
            Position = new Point(0, 0);
            UpdateBounds();
        }

        public void Play()
        {
            Position.Y -= _speed;

            if (Position.Y < 80)
            {
                Reset();
            }
        }

        public override void DrawImage(Graphics g)
        {
            if (!_isRunning)
            {
                return;
            }

            UpdateBounds();
            g.FillRectangle(Brushes.White, MovingBounds);

        }
    }
}
