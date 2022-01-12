using SpaceInvaders.Enums;
using SpaceInvaders.GameItems;
using SpaceInvaders.Helpers;
using System.Collections;
using System.Drawing;

namespace SpaceInvaders
{
    public class DirectorEnemies
    {
        private const int _rowCount = 5;
        private Enemy[] _allInvaders;
        private Bomb[] _bombs = null;
        private Alien _alien;
        private ShoreLine _floorline = null;

        public void Initialize()
        {
            int startHeight = 125;
            int rowHeight = 50;
            int _enemyWidth = 57;

            _allInvaders = new Enemy[Common.ENEMY_PER_ROW_COUNT * _rowCount];
            _bombs = new Bomb[_allInvaders.Length];
            _floorline = new ShoreLine();
            _alien = new Alien();

            int counter = 0;
            for (int i = 0; i < _rowCount; i++)
            {
                Characters character = (Characters)((i + 1) / 2);
                for (int ii = 0; ii < Common.ENEMY_PER_ROW_COUNT; ii++)
                {
                    Enemy invader = new Enemy(character);
                    invader.Position.X = ii * _enemyWidth + 150 + EnemyHelper.GetMarginX(character);
                    invader.Position.Y = (i * rowHeight) + startHeight;

                    _allInvaders[counter] = invader;
                    _bombs[counter] = new Bomb();
                    counter++;
                }
            }
        }

        public void Paint(Graphics g)
        {
            _alien.DrawImage(g);
            for (int i = 0; i < _allInvaders.Length; i++)
            {
                _allInvaders[i].DrawImage(g);
                _bombs[i].DrawImage(g);
            }
        }

        public void PaintFloorline(Graphics g)
        {
            _floorline.DrawImage(g);
        }

        public int TotalNumberOfInvaders()
        {
            int count = 0;
            for (int i = 0; i < _allInvaders.Length; i++)
            {
                if (!_allInvaders[i].IsDead())
                {
                    count++;
                }
            }
            return count;
        }

        public ArrayList GetLowInvaders()
        {
            ArrayList ret = new ArrayList();
            for (int i = 0; i < _allInvaders.Length; i++)
            {
                if (!_allInvaders[i].IsDead() && _allInvaders[i].GetMovingBounds().Y > 495)
                {
                    ret.Add(_allInvaders[i]);
                }
            }
            return ret;
        }

        public bool Starting()
        {
            for (int i = 0; i < _allInvaders.Length; i++)
            {
                if (_allInvaders[i].GetState() == CharacterState.Initalizing)
                {
                    _allInvaders[i].Start();
                    return true;
                }
            }
            return false;
        }

        private void ChangeDirection(int turn)
        {
            for (int i = 0; i < _allInvaders.Length; i++)
            {
                _allInvaders[i].SetDirectionMultiplier(turn);
            }
        }

        public void InvaderPlay()
        {
            for (int i = 0; i < _allInvaders.Length; i++)
            {
                _allInvaders[i].Play();
                int bombOdds = (200 - Common.CurrentGameLevel * Common.CurrentGameLevel).MinimumIf(150);
                if (!_allInvaders[i].IsDead() && Common.GetRandomNumberFromZero(bombOdds) < 2)
                {
                    Rectangle bounds = _allInvaders[i].GetMovingBounds();
                    DropBomb(_allInvaders[i].Position.X + bounds.Width / 2, _allInvaders[i].Position.Y + bounds.Height);
                }
            }

            int turn = ShouldTurnAround();
            if (turn != 0)
            {
                ChangeDirection(turn);
            }

            if (turn == 1)
            {
                for (int i = 0; i < _allInvaders.Length; i++)
                {
                    _allInvaders[i].MoveDown();
                }
            }

        }

        public void InvaderAnimateOnly()
        {
            for (int i = 0; i < _allInvaders.Length; i++)
            {
                _allInvaders[i].AnimateOnly();
            }
        }

        private void DropBomb(int x, int y)
        {
            for (int i = 0; i < _bombs.Length; i++)
            {
                if (!_bombs[i].IsFalling())
                {
                    _bombs[i].DropBomb(x, y);
                    return;
                }
            }
        }

        private int ShouldTurnAround()
        {
            int margin = 10;

            for (int i = 0; i < _allInvaders.Length; i++)
            {
                if (!_allInvaders[i].IsDead())
                {
                    if (_allInvaders[i].Position.X + margin > Common.GetPlayArea().Width - _allInvaders[i].Width)
                    {
                        return -1;
                    }
                    if (_allInvaders[i].Position.X < 20)
                    {
                        return 1;
                    }
                }
            }
            return 0;
        }

        public int TorpedoHitCheck(Rectangle torpedo)
        {
            for (int i = 0; i < _allInvaders.Length; i++)
            {
                if (!_allInvaders[i].IsDead() && _allInvaders[i].HitCheck(torpedo))
                {
                    _allInvaders[i].Explode();
                    return _allInvaders[i].GetCharacterScoreValue();
                }
                if (_alien.HitCheck(torpedo))
                {
                    _alien.Explode();
                    return _alien.GetCharacterScoreValue();
                }
            }

            return 0;
        }

        public bool HitCheckEnemies(Rectangle playItem)
        {

            for (int i = 0; i < _bombs.Length; i++)
            {
                if (_bombs[i].IsFalling() && _bombs[i].HitCheck(playItem))
                {
                    return true;
                }
            }

            for (int i = 0; i < _allInvaders.Length; i++)
            {
                if (!_allInvaders[i].IsDead() && _allInvaders[i].GetMovingBounds().Y > (Common.GetPlayArea().Height - 100))
                {
                    if (_allInvaders[i].HitCheck(playItem))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public ArrayList GetBombs()
        {
            ArrayList activeBombs = new ArrayList(0);

            for (int i = 0; i < _bombs.Length; i++)
            {
                if (_bombs[i].IsFalling())
                {
                    activeBombs.Add(_bombs[i]);
                }
            }
            return activeBombs;
        }

        public void PlayBomnbsAndAlien()
        {
            _alien.Play();

            for (int i = 0; i < _bombs.Length; i++)
            {
                var missLocation = _bombs[i].PlayAndMissLocation();
                if (missLocation > 0)
                {
                    _floorline.SetHole(missLocation);
                }
            }
        }

        public void ResetBombs()
        {
            for (int i = 0; i < _bombs.Length; i++)
            {
                _bombs[i].Reset();
            }
        }
    }
}
