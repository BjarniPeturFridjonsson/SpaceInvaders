using SpaceInvaders.Enums;
using SpaceInvaders.Helpers;
using System.Drawing;

namespace SpaceInvaders.GameItems
{
    public class Player : BaseItem
    {
        private CharacterState _state = CharacterState.Undefined;
        private int speed = 5;
        private int _playerNr = 0;
        int CountExplosion = 0;

        public Player(int nr) : base(Properties.Resources.player)
        {
            _playerNr = nr;
            Position.X = 50 + (nr * 150);
            Position.Y = Common.GetPlayArea().Height - 44 +((_playerNr-1)*nr);
            _state = CharacterState.Running;
        }

        public Point GetLaunchStart()
        {
            Point ret = new Point(MovingBounds.Left + MovingBounds.Width / 2, MovingBounds.Top - 4);
            return ret;
        }

        public CharacterState GetState()
        {
            return _state;
        }

        public void GameOver()
        {
            _state = CharacterState.GameOver;
        }

        public void Die()
        {
            _state = CharacterState.Dying;
        }

        public void MoveLeft()
        {
            Move(-1);
        }

        public void MoveRight()
        {
            Move(1);
        }

        private void Move(int multiplyer)
        {
            int maxWidth = Common.GetPlayArea().Width - Width - 8;
            if (_state != CharacterState.Running || Common.CurrentRunState != GameState.Running)
            {
                return;
            }

            Position.X += (speed * multiplyer);

            if (Position.X > maxWidth)
            {
                Position.X = maxWidth;
                return;
            }

            if (Position.X < 4)
            {
                Position.X = 4;
            }
        }

        public override void DrawImage(Graphics g)
        {
            if (_state == CharacterState.Dead)
                return;

            if (_state == CharacterState.Running)
            {
                UpdateBounds();
                PlayerHelper.DrawPlayer(g, _playerNr, new Point(MovingBounds.X, MovingBounds.Y));
            }
            else
            {
                if (CountExplosion < 20)
                {
                    DieAnim(g);
                }
                else
                {
                    _state = CharacterState.Dead;
                }
            }
        }

        public void Reset()
        {
            _state = CharacterState.Running;
            CountExplosion = 0;
        }

        private void DieAnim(Graphics g)
        {
            CountExplosion++;
            using (Pen pen = new Pen(PlayerHelper.GetPlayerColor(_playerNr), 3))
            {
                int lines = Common.GetRandomNumberFromZero(15) + 10;
                for (int i = 0; i < lines; i++)
                {
                    int x = Common.GetRandomNumberFromZero(MovingBounds.Width);
                    int y = Common.GetRandomNumberFromZero(MovingBounds.Height + 10);
                    x += Position.X;
                    y += Position.Y;
                    g.DrawLine(pen, Position.X + (MovingBounds.Width / 2), Position.Y + MovingBounds.Height, x, y);
                }
            }
        }
    }
}
