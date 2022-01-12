using System;
using System.Drawing;
using SpaceInvaders.Helpers;

namespace SpaceInvaders.GameItems
{
    public class Shield : BaseItem
    {
        Bitmap _imgMemory = null;
        private bool _initialized = false;
        private ImageHelpers _imgHelper = new ImageHelpers();
        private int _speed = 8;

        public Shield(int nr) : base(Properties.Resources.shield)
        {
            InitializeBitmap();
            UpdateBounds();
            Position.X = 120 + (164 * nr);
            Position.Y = Common.GetPlayArea().Height - BaseImage.Height - 80;
        }

        public override void DrawImage(Graphics g)
        {
            UpdateBounds();
            g.DrawImage(_imgMemory, MovingBounds, 0, 0, ImageBounds.Width, ImageBounds.Height, GraphicsUnit.Pixel);
        }

        public new bool HitCheck(Rectangle rect)
        {
            return InnerHitCheckAndPaint(rect, 0);
        }


        public bool HitCheckTorpedo(Rectangle rect)
        {
            return InnerHitCheckAndPaint(rect, 1);
        }

        public bool HitCheckInvader(Rectangle rect)
        {
            var hit = Rectangle.Intersect(rect, GetMovingBounds());
            if (!hit.IsEmpty)
            {
                PaintInvaderHit(hit);
            }
            return false;
        }
        
        private bool InnerHitCheckAndPaint(Rectangle rect, int type)
        {
            if (base.HitCheck(rect))
            {
                Rectangle hitCheckPart = _imgHelper.MakeRectangleSafe(new Rectangle(rect.X - MovingBounds.Location.X, rect.Y - MovingBounds.Location.Y - _speed, rect.Width, rect.Height + 20 + _speed), MovingBounds);
                int depth = _imgHelper.PixelHitDownOnly(_imgMemory, hitCheckPart, 4);
                if (depth > 0)
                {
                    int x = (rect.X - MovingBounds.X - (rect.Width / 2) - 2).ReturnZeroIfNegative();
                    switch (type)
                    {
                        case 0:
                            PaintAnotherExplosion(new Point(x, depth - 2));
                            break;
                        case 1:
                            PaintLazerHit(new Point(x, hitCheckPart.Y));
                            break;
                        default:
                            throw new NotImplementedException("Tsk, tsk, tsk. What do you think you're doing?");
                    }
                    return true;
                }
            }
            return false;
        }

        private void InitializeBitmap()
        {
            if (_initialized)
            {
                return;
            }
            _imgMemory = _imgHelper.CopyImageToBitmap(BaseImage);
            _initialized = true;
        }

        private void PaintInvaderHit(Rectangle rect)
        {
            var bounds = GetMovingBounds();
            rect.X -= bounds.X;
            rect.Y -= bounds.Y;
            using (Graphics g = Graphics.FromImage(_imgMemory))
            {
                g.FillRectangle(new SolidBrush(Color.Black), rect);
            }
        }

        private void PaintLazerHit(Point pos)
        {
            using (Graphics g = Graphics.FromImage(_imgMemory))
            {
                g.FillRectangle(new SolidBrush(Color.Black), new Rectangle(pos.X, pos.Y, 8, 36));
                for (int i = 0; i < 6; i++)
                {
                    _imgHelper.CreatedPixelJittered(g, new Point(pos.X, pos.Y + 18), 10, 20);
                }
            }
        }

        private void PaintAnotherExplosion(Point pos)
        {
            InitializeBitmap();

            using (Graphics g = Graphics.FromImage(_imgMemory))
            {
                PaintExplosionPixels(g, pos);
            }
        }

        private void PaintExplosionPixels(Graphics g, Point pos)
        {
            g.FillRectangle(new SolidBrush(Color.Black), new Rectangle(pos.X, pos.Y, 8, 20));
            _imgHelper.RotateRectangle(g, new Rectangle(pos.X - 2, pos.Y + 18, 13, 13), 45);

            _imgHelper.CreatePixel(g, pos.X - 4 * 2, pos.Y + 4 * 1);
            _imgHelper.CreatePixel(g, pos.X - 4 * 1, pos.Y + 4 * 3);
            _imgHelper.CreatePixel(g, pos.X - 4 * 2, pos.Y + 4 * 4);
            _imgHelper.CreatePixel(g, pos.X - 4 * 2, pos.Y + 4 * 6);
            _imgHelper.CreatePixel(g, pos.X - 4 * 1, pos.Y + 4 * 7);

            _imgHelper.CreatePixel(g, pos.X + 4 * 2, pos.Y + 4 * 1);
            _imgHelper.CreatePixel(g, pos.X + 4 * 3, pos.Y + 4 * 2);
            _imgHelper.CreatePixel(g, pos.X + 4 * 3, pos.Y + 4 * 7);
        }
    }
}
