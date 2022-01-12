using System.Drawing;

namespace SpaceInvaders.GameItems
{
    public class BaseItem
    {
        public Point Position = new Point();

        protected Image BaseImage = null;
        protected Rectangle MovingBounds = new Rectangle();
        protected Rectangle ImageBounds = new Rectangle();

        public int Width
        {
            get
            {
                return ImageBounds.Width;
            }
        }

        public BaseItem()
        {
            BaseImage = null;
        }

        public BaseItem(Image fileName)
        {
            if (fileName == null)
            {
                return;
            }
            BaseImage = fileName;
            ImageBounds.Width = BaseImage.Width;
            ImageBounds.Height = BaseImage.Height;
        }

        public virtual Rectangle GetMovingBounds()
        {
            return MovingBounds;
        }

        public virtual void DrawImage(Graphics g)
        {
            UpdateBounds();
            g.DrawImage(BaseImage, MovingBounds, 0, 0, ImageBounds.Width, ImageBounds.Height, GraphicsUnit.Pixel);
        }

        public bool HitCheck(Rectangle rect)
        {
            bool hit = MovingBounds.IntersectsWith(rect);
            return hit;
        }

        public void UpdateBounds()
        {
            MovingBounds = ImageBounds;
            MovingBounds.Offset(Position);
        }
    }
}
