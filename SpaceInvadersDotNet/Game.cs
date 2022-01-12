using SpaceInvaders.Enums;
using SpaceInvaders.GameItems;
using SpaceInvaders.Helpers;
using SpaceInvaders.Screens;
using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;

namespace SpaceInvaders
{
    public class Game
    {
        private long _tickCounter = 0;
        private int _gameSpeed = 0;

        private DirectorEnemies _enemiesDirector = new DirectorEnemies();
        private DirectorScreen _screenDirector = new DirectorScreen();
        private DirectorPlayer _playerDirector = new DirectorPlayer();

        private Shield[] _shields = null;

        //draw areas
        private StartScreen _startSceen = new StartScreen();
        private LevelPopupScreen _gameInitalizingScreen = new LevelPopupScreen();

        public void Initialize()
        {
            Common.CurrentRunState = GameState.StartupScreen;
            _playerDirector.Initialize();
            _screenDirector.Initalize();
            InitializeLevel();
        }

        public void Tick()
        {
            _tickCounter++;
            if (Common.CurrentRunState != GameState.Running)
            {
                if (_tickCounter % 16 == 0)
                {
                    _enemiesDirector.InvaderAnimateOnly();
                }
                return;
            }

            if (_tickCounter % _gameSpeed == 0)
            {
                _enemiesDirector.InvaderPlay();

                int totalInvaders = _enemiesDirector.TotalNumberOfInvaders();
                _gameSpeed = ((int)Math.Ceiling(Math.Sqrt(totalInvaders)) * 3) - Common.CurrentGameLevel;

                if (_gameSpeed < 1)
                {
                    _gameSpeed = 1;
                }

                if (totalInvaders == 0)
                {
                    InitializeLevel();
                    Common.CurrentRunState = GameState.Starting;
                    Common.CurrentGameLevel++;
                }
            }

            PlayTorpedoes();
            _enemiesDirector.PlayBomnbsAndAlien();
            PlayAllPlayers();
            HitCheckStaticItems();
        }

        public void Paint(Graphics g)
        {
            if (Common.CurrentRunState == GameState.Starting)
            {
                _gameInitalizingScreen.Paint(g);
                if (!_enemiesDirector.Starting())
                {
                    Common.CurrentRunState = GameState.Running;
                }
            }

            if (Common.CurrentRunState == GameState.StartupScreen)
            {
                _startSceen.Paint(g);
                return;
            }

            _screenDirector.DrawStaticScreens(g);
            PaintShields(g);
            _enemiesDirector.PaintFloorline(g);

            if (Common.CurrentRunState == GameState.GameOver)
            {
                _screenDirector.GameOver(g);
                return;
            }

            _playerDirector.PaintPlayers(g);

            _enemiesDirector.Paint(g);
            _screenDirector.DrawLastOverlay(g);
        }

        public void Restart()
        {
           if(Common.CurrentRunState != GameState.GameOver)
            {
                return;
            }
            Initialize();
        }

        private void PlayTorpedoes()
        {
            for (int i = 1; i < 4; i++)
            {
                if (_playerDirector.TorpedoIsRunning(i))
                {
                    _playerDirector.PlayTorpedo(i);
                    int level = _enemiesDirector.TorpedoHitCheck(_playerDirector.GetTorpedoMovingBounds(i));
                    if (level > 0)
                    {
                        _screenDirector.AddScore(level * 10, i);
                        _playerDirector.TorpedoReset(i);
                        return;
                    }
                }
            }
        }

        private void HitCheckStaticItems()
        {
            ArrayList bombs = _enemiesDirector.GetBombs();
            ArrayList invaders = _enemiesDirector.GetLowInvaders();


            for (int i = 0; i < Common.SHIELD_COUNT; i++)
            {
                for (int ii = 0; ii < bombs.Count; ii++)
                {
                    Bomb bomb = (Bomb)bombs[ii];
                    if (_shields[i].HitCheck(bomb.GetMovingBounds()))
                    {
                        bomb.Reset();
                    }
                }
                for (int playerId = 1; playerId < 4; playerId++)
                {
                    if (_playerDirector.TorpedoIsRunning(playerId) && _shields[i].HitCheckTorpedo(_playerDirector.GetTorpedoMovingBounds(playerId)))
                    {
                        _playerDirector.TorpedoReset(playerId);
                    }
                }

                for (int inv = 0; inv < invaders.Count; inv++)
                {
                    _shields[i].HitCheckInvader(((Enemy)invaders[inv]).GetMovingBounds());
                }

            }
        }

        private void PlayAllPlayers()
        {
            if (_screenDirector.GetLives() == 0)
            {
                Common.CurrentRunState = GameState.GameOver;
                return;
            }

            for (int i = 1; i < 4; i++)
            {
                CharacterState state = _playerDirector.GetState(i);
                if (state == CharacterState.Undefined || state == CharacterState.GameOver)
                {
                    continue;
                }
                PlayerPlay(i, state);
            }
        }

        private void PlayerPlay(int playerNr, CharacterState state)
        {
            if (!_screenDirector.CanIplay(playerNr, state))
            {
                if (_screenDirector.GetLives(playerNr) > 0)
                {
                    _playerDirector.Reset(playerNr);
                    _enemiesDirector.ResetBombs();
                }
                return;
            }

            if (state == CharacterState.Dying)
                return;

            if (_enemiesDirector.HitCheckEnemies(_playerDirector.GetMovingBounds(playerNr)))
            {
                _playerDirector.Die(playerNr);
            }
        }

        private void PaintShields(Graphics g)
        {
            for (int i = 0; i < Common.SHIELD_COUNT; i++)
            {
                _shields[i].DrawImage(g);
            }
        }

        private void InitializeLevel()
        {
            _enemiesDirector.Initialize();
            _playerDirector.TorpedoResetAll();
            InitializeShields();
            _gameSpeed = Common.GAME_START_SPEED;
        }

        private void InitializeShields()
        {
            _shields = new Shield[Common.SHIELD_COUNT];
            for (int i = 0; i < Common.SHIELD_COUNT; i++)
            {
                _shields[i] = new Shield(i);
            }
        }

        public void ShootAndInitialize(int player)
        {
            if (Common.CurrentRunState == GameState.GameOver)
            {
                return;
            }

            if (_playerDirector.CreatePlayerIfNotCreated(player))
            {
                _screenDirector.InitializePlayer(player);
            }
            _playerDirector.Launch(player);
        }

        public void MoveL(int player)
        {
            _playerDirector.MoveLeft(player);
        }

        public void MoveR(int player)
        {
            _playerDirector.MoveRight(player);
        }

        public void PauseGame()
        {
            Common.CurrentRunState = GameState.Paused;
        }
    }
}
