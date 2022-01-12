using SpaceInvaders.Enums;
using SpaceInvaders.Helpers;
using System.Drawing;

namespace SpaceInvaders.GameItems
{
    public class Enemy : BaseItem
    {
        private const int _speed = 10;

        private Image _imgAnim2 = null;
        private int _iExplodeCount = 0;
        private CharacterState _state = CharacterState.Undefined;
        private int _speedModifier = 1;
        private long _animCounter = 0;
        private Characters _enemyCharacter = 0;

        public bool Died = false;

        public Enemy(Characters character) : base(null)
        {
            _enemyCharacter = character;
            _imgAnim2 = EnemyHelper.GetEnemyAnimationB(_enemyCharacter);
            BaseImage = EnemyHelper.GetEnemyAnimationA(_enemyCharacter);

            ImageBounds.Width = BaseImage.Width;
            ImageBounds.Height = BaseImage.Height;

            Position.X = -50;
            Position.Y = -50;
            _state = CharacterState.Initalizing;
            UpdateBounds();
        }

        public void Start()
        {
            _state = CharacterState.Running;
        }

        public bool IsDead()
        {
            return _state == CharacterState.Dead;
        }
        public CharacterState GetState()
        {
            return _state;
        }

        public int GetCharacterScoreValue()
        {
            return 1 + (int)_enemyCharacter;
        }

        public void SetDirectionMultiplier(int multiplier)
        {
            _speedModifier = multiplier;
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

            if (_state != CharacterState.Running)
            {
                return;
            }

            if (_animCounter % 2 == 0)
                g.DrawImage(BaseImage, MovingBounds, 0, 0, ImageBounds.Width, ImageBounds.Height, GraphicsUnit.Pixel);
            else
                g.DrawImage(_imgAnim2, MovingBounds, 0, 0, ImageBounds.Width, ImageBounds.Height, GraphicsUnit.Pixel);
        }

        public void Play()
        {
            if (_state != CharacterState.Running)
            {
                return;
            }

            Position.X += _speed * _speedModifier;
            _animCounter++;
        }

        public void MoveDown()
        {
            Position.Y += Common.ENEMY_ROW_MOVE_DOWN;
            UpdateBounds();
        }

        public void Explode()
        {
            _state = CharacterState.Dead;
            _iExplodeCount = 2;
        }

        public void AnimateOnly()
        {
            _animCounter++;
        }
    }
}
