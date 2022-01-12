using System;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Windows.Forms;

namespace SpaceInvaders.Helpers
{
    public class FontHelper
    {
        private Brush _fontBrush = Brushes.White;
        private const int _fontSizeMedium = 15;
        private const int _fontSizeLarge = 27;

        private int _fadeOutAlpha = 0;
        private int _fadeOutStep = 10;

        private static PrivateFontCollection fontCollection = null;
        private Graphics _g;

        public void InitializeGraphics(Graphics g)
        {
            _g = g;
        }

        public void WriteTextFadeOut(PointF startPoint, string text)
        {
            int blinkGuard = 20;//don't want it to blink by becomming tranparent.

            if (_fadeOutAlpha + _fadeOutStep < 0 || _fadeOutAlpha + _fadeOutStep > 255 - blinkGuard)
            {
                _fadeOutStep *= -1;
            }

            _fadeOutAlpha += _fadeOutStep;

            using (Brush brush = new SolidBrush(Color.FromArgb(_fadeOutAlpha + blinkGuard, Color.White)))
            {
                WriteText(brush, startPoint, text, _fontSizeMedium);
            }
        }

        public void WriteMediumText(PointF startPoint, string text)
        {
            WriteText(_fontBrush, startPoint, text, _fontSizeMedium);
        }

        public void WriteMediumText(PointF startPoint, string text, Color color)
        {
            WriteText(color, startPoint, text, _fontSizeMedium);
        }

        public void WriteMediumTextCentered(PointF midPoint, string text, Color color)
        {
            using (Brush brush = new SolidBrush(color))
            {
                WriteTextCentered(brush, midPoint, text, _fontSizeMedium);
            }
        }

        public void WriteLargeTextCentered(PointF midPoint, string text)
        {
            WriteTextCentered(_fontBrush, midPoint, text, _fontSizeLarge);
        }

        public int MesureTextLarge(string text)
        {
            using (Font f = GetFont(_fontSizeLarge))
            {
                return SingleLineTextWidth(text, f);
            }
        }

        public void WriteLargeText(PointF startPoint, string text)
        {
            WriteText(_fontBrush, startPoint, text, _fontSizeLarge);
        }

        public void WriteLargeText(PointF startPoint, string text, Color color)
        {
            WriteText(color, startPoint, text, _fontSizeLarge);
        }

        private void WriteText(Color color, PointF startPoint, string text, int size)
        {
            using (Brush brush = new SolidBrush(color))
            {
                WriteText(brush, startPoint, text, size);
            }

        }

        private void WriteText(Brush brush, PointF startPoint, string text, int size)
        {
            using (Font f = GetFont(size))
            {
                _g.DrawString(text, f, brush, startPoint);
            }
        }

        private void WriteTextCentered(Brush brush, PointF midPoint, string text, int size)
        {
            int width = 0;
            using (Font f = GetFont(size))
            {
                width = SingleLineTextWidth(text, f);
            }
            midPoint.X -= width / 2;
            WriteText(brush, midPoint, text, size);
        }

        private int SingleLineTextWidth(string text, Font font)
        {
            SizeF ret = _g.MeasureString(text, font);
            return (int)Math.Ceiling(ret.Width);
        }

        private Font GetFont(int size)
        {
            if (fontCollection == null)
            {
                LoadFont();
            }
            if (fontCollection == null)
            {
                return new Font("Arial", size, GraphicsUnit.Pixel);
            }
            return new Font(fontCollection.Families[0], size, GraphicsUnit.Pixel);
        }

        private void LoadFont()
        {
            fontCollection = new PrivateFontCollection();
            try
            {
                fontCollection.AddFontFile(Path.Combine(Application.StartupPath, "resources\\C64.TTF"));
            }
            catch (Exception ex)
            {
                fontCollection = null;
            }
        }

    }
}
