using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace SpaceInvaders.Helpers
{
    public class ImageHelpers
    {
        public Rectangle MakeRectangleSafe(Rectangle intersecting, Rectangle bounds)
        {
            Rectangle rect = MakeRectangleSafe(intersecting);

            if (rect.X + rect.Width > bounds.Width)
            {
                rect.Width = bounds.Width - rect.X;
            }
            if (rect.Y + rect.Height > bounds.Height)
            {
                rect.Height = bounds.Height - rect.Y;
            }
            return rect;
        }

        public Rectangle MakeRectangleSafe(Rectangle rect)
        {
            rect.Height = rect.Height.ReturnZeroIfNegative();
            rect.Width = rect.Width.ReturnZeroIfNegative();
            if (rect.Location.X < 0 || rect.Location.Y < 0)
            {
                rect.Location = new Point(rect.Location.X.ReturnZeroIfNegative(), rect.Location.Y.ReturnZeroIfNegative());
            }

            return rect;
        }

        public void RotateRectangle(Graphics g, Rectangle rect, float angle)
        {
            using (Matrix m = new Matrix())
            {
                m.RotateAt(angle, new PointF(rect.Left + rect.Width / 2, rect.Top + rect.Height / 2));
                g.Transform = m;
                g.FillRectangle(Brushes.Black, rect);
                g.ResetTransform();
            }
        }
        public Bitmap CopyImageToBitmap(Image orginal)
        {
            Bitmap ret = new Bitmap(orginal.Width, orginal.Height);
            using (Graphics memG = Graphics.FromImage(ret))
            {
                memG.DrawImage(orginal, new Rectangle(0, 0, orginal.Width, orginal.Height), 0, 0, orginal.Width, orginal.Height, GraphicsUnit.Pixel);
            }

            return ret;
        }

        public int PixelHitDownOnly(Bitmap bmp, Rectangle rect, int hitWidth)
        {
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);
            IntPtr ptr = bmpData.Scan0;

            int bytes = bmpData.Stride * rect.Height;
            byte[] rgbVal = new byte[bytes];
            int stride = bmpData.Stride;
            int bpp = Image.GetPixelFormatSize(bmp.PixelFormat) / 8;//will only work on 16 or 32 bit 

            Marshal.Copy(ptr, rgbVal, 0, bytes);
            bmp.UnlockBits(bmpData);

            int foundAtLevel = 0;
            for (int y = 0; y < rect.Height; y++)
            {
                for (int x = 0; x < rect.Width; x++)
                {
                    if ((rgbVal[(y * stride) + (x * bpp) + 1]) > 200) // close to green
                    {
                        return y + 1;//to cover for hit on first line.
                    }
                }
            }
            return foundAtLevel;
        }

        public void CreatePixel(Graphics g, int x, int y)
        {
            g.FillRectangle(Brushes.Black, new Rectangle(x, y, 4, 4));
        }

        public void CreatedPixelJittered(Graphics g, Point pos, int jitterX, int jitterY)
        {
            int x = pos.X += Common.GetRandomNumber(jitterX * -1, jitterX);
            int y = pos.Y += Common.GetRandomNumber(jitterY * -1, jitterY);
            CreatePixel(g, x, y);
        }
    }
}
