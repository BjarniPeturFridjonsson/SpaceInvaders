using SpaceInvaders.Helpers;
using System.Collections;
using System.Drawing;

namespace SpaceInvaders.GameItems
{
    public  class ShoreLine : BaseItem
    {
        Point _linePosStart = new Point();
        Point _linePosEnd = new Point();
        ArrayList list = new ArrayList();

        public ShoreLine() : base(null)
        {
            Rectangle area = Common.GetPlayArea();
            _linePosStart = new Point(area.X, area.Height);
            _linePosEnd = new Point(area.Width, area.Height);
            list = new ArrayList();
        }

        public void SetHole(int x)
        {
            if (!list.Contains(x))
            {
                list.Add(x);
            }           
        }

        public override void DrawImage(Graphics g)
        {
            using (var pen = new Pen(Common.GetGreen(),4)) 
            {
                g.DrawLine(pen, _linePosStart, _linePosEnd);
                foreach (var item in list)
                {
                    int x = (int)item;
                    g.FillRectangle(Brushes.Black, new Rectangle(x, _linePosStart.Y - 2, 4, 4));
                }
            }
        }
    }
}
