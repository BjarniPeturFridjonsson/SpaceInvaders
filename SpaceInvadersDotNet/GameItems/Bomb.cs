using SpaceInvaders.Helpers;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SpaceInvaders.GameItems
{
    public class Bomb : BaseItem
    {
        private const int speed = 5;
        private bool _flipper = false;
        private bool _running = false;

        GraphicsPath bombA;
        GraphicsPath bombB;
        GraphicsPath animation1;
        GraphicsPath animation2;

        public bool IsFalling()
        {
            return _running;
        }

        public void DropBomb(int x, int y)
        {
            Position.X = x;
            Position.Y = y;
            ImageBounds.Width = 4;
            ImageBounds.Height = 12;

            bombA = CreateBomb(0, ImageBounds.Width);
            bombB = CreateBomb(ImageBounds.Width, 0);

            _running = true;
        }

        private GraphicsPath CreateBomb(int start, int next)
        { 
            GraphicsPath bomb = new GraphicsPath();
            float height = ImageBounds.Height / 3;

            bomb.AddLine(new PointF(start, 0), new PointF(next, height));
            bomb.AddLine(new PointF(next, height), new PointF(start, height * 2));
            bomb.AddLine(new PointF(start, height * 2), new PointF(next, height * 3));
            return bomb;
        }

        public int PlayAndMissLocation()
        {
            if (!_running)
            {
                return 0;
            }

            Position.Y += speed;
            _flipper = !_flipper;
            if (Position.Y > Common.GetPlayArea().Height)
            {
                int x = Position.X;
                Reset();
                return x;
            }
            return 0;
        }

        public override void DrawImage(Graphics g)
        {
            if (!_running)
            {
                return;
            }

            UpdateBounds();
            Matrix matrix = new Matrix();

            matrix.Translate(MovingBounds.Left, MovingBounds.Top);
            using (var pen = new Pen(Color.White, 2))
            {
                if (_flipper)
                {
                    using (animation1 = (GraphicsPath)bombA.Clone())
                    {
                        animation1.Transform(matrix);
                        g.DrawPath(pen, animation1);
                    }
                }
                else
                {
                    using (animation2 = (GraphicsPath)bombB.Clone())
                    {
                        animation2.Transform(matrix);
                        g.DrawPath(pen, animation2);
                    }
                }
            }
        }

        public void Reset()
        {
            _running = false;
            Position = new Point(0, 0);
            UpdateBounds();
        }
    }
}
