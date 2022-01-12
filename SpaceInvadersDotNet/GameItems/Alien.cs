using SpaceInvaders.Helpers;
using System.Drawing;

namespace SpaceInvaders.GameItems
{
    public class Alien : BaseItem
    {
        private int _speed = 0;
        private long _animCounter = 0;
        private Image[] _anime = null;
        private int _iExplodeCount = 0;
        private bool _running = false;
        private long _drawCounter = 0;

        public Alien() : base(Properties.Resources.alien_1)
        {
            _anime = new Image[5];
            _anime[0] = Properties.Resources.alien_1;
            _anime[1] = Properties.Resources.alien_2;
            _anime[2] = Properties.Resources.alien_3;
            _anime[3] = Properties.Resources.alien_4;
            _anime[4] = Properties.Resources.alien_5;

            BaseImage = _anime[0];

            ImageBounds.Width = BaseImage.Width;
            ImageBounds.Height = BaseImage.Height;

            Position.X = -50;
            Position.Y = 85;
            UpdateBounds();
        }

        public override void DrawImage(Graphics g)
        {
            UpdateBounds();
            if (_iExplodeCount > 0)
            {
                Bitmap exp = Properties.Resources.explosion;
                g.DrawImage(exp, MovingBounds, 0, 0, exp.Width, exp.Height, GraphicsUnit.Pixel);
                _iExplodeCount--;
                return;
            }

            if (!_running)
            {
                return;
            }

            if (_animCounter > 4)
            {
                _animCounter = 0;
            }
            g.DrawImage(_anime[_animCounter], MovingBounds, 0, 0, ImageBounds.Width, ImageBounds.Height, GraphicsUnit.Pixel);
        }

        public void AnimateOnly()
        {
            _speed = 4 + (Common.CurrentGameLevel / 4);
            _drawCounter++;
            if (_drawCounter % 6 != 0)
            {
                return;
            }
            _animCounter++;
        }

        public void Start()
        {
            Position.X = 0 - ImageBounds.Width;
            _running = true;
        }

        public void MaybeStart()
        {
            if(_running == false && Common.GetRandomNumberFromZero(10000) < 2)
            {
                Start();
            }
        }

        public void Play()
        {
            MaybeStart();
            if (_running == false)
            {
                return;
            }
            
            Position.X += _speed;
            UpdateBounds();
            AnimateOnly();

            if (MovingBounds.X > Common.GetPlayArea().Width)
            {
                _running = false;
                return;
            }
        }

        public int GetCharacterScoreValue()
        {
            return 2 + Common.GetRandomNumberFromZero(4);
        }

        public void Explode()
        {
            _running = false;
            _iExplodeCount = 2;
        }
    }
}
