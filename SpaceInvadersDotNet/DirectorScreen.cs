using SpaceInvaders.Enums;
using SpaceInvaders.Helpers;
using SpaceInvaders.Screens;
using System;
using System.Drawing;

namespace SpaceInvaders
{
    public class DirectorScreen
    {
        private ScoreScreen _gameScoreScreen = new ScoreScreen();
        private LivesScreen _gameLivesScreen = new LivesScreen();
        private CurrentLevelScreen _gameCurrentLevel = new CurrentLevelScreen();
        private GamePausedScreen _gamePauseScreen = new GamePausedScreen();
        private GameOverScreen _gameOverScreen = new GameOverScreen();

        public void DrawStaticScreens(Graphics g)
        {
            _gameLivesScreen.Paint(g);
            _gameScoreScreen.Paint(g);
            _gameCurrentLevel.Paint(g);
        }

        public void DrawLastOverlay(Graphics g)
        {
            if (Common.CurrentRunState == GameState.Starting)
            {
                return;
            }

            if (Common.CurrentRunState == GameState.Paused)
            {
                _gamePauseScreen.Paint(g);
            }
        }

        public int GetLives(int playerNr)
        {
            return _gameLivesScreen.GetLives(playerNr);
        }

        public int GetLives()
        {
            return _gameLivesScreen.GetLives();
        }

        public void Initalize()
        {
            _gameScoreScreen.ResetScore(1);
            _gameScoreScreen.ResetScore(2);
            _gameScoreScreen.ResetScore(3);
            _gameLivesScreen.ResetAll();
            _gameOverScreen.Reset();
        }

        public void AddScore(int score, int playerNr)
        {
            _gameScoreScreen.AddScore(score, playerNr);
        }

        public void GameOver(Graphics g)
        {
            _gameOverScreen.SetScore(_gameScoreScreen.GetAllScores());
            _gameOverScreen.Paint(g);
        }

        public void InitializePlayer(int player)
        {
            _gameLivesScreen.ResetLives(player);
        }

        public bool CanIplay(int playerNr, CharacterState state)
        {
            if (state == CharacterState.Dead)
            {
                _gameLivesScreen.Died(playerNr);

                if (GetLives() == 0)
                {
                    Common.CurrentRunState = GameState.GameOver;
                }
                return false;
            }
            return true;
        }
    }
}
