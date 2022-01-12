using SpaceInvaders.Helpers;
using SpaceInvaders.Models;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace SpaceInvaders.Screens
{
    public class GameOverScreen : BaseScreen
    {
        private List<ScoreValueDto> _scores = new List<ScoreValueDto>();
        FontHelper _helper = new FontHelper();
        private DateTime _gameOverTimer;

        public void SetScore(List<ScoreValueDto> dto)
        {
            _scores = dto;
        }

        public override void Paint(Graphics g)
        {
            _helper.InitializeGraphics(g);
            GameOverText(g);
            if (_gameOverTimer == DateTime.MinValue)
            {
                _gameOverTimer = DateTime.Now;
            }
            if (_gameOverTimer.AddSeconds(5) < DateTime.Now)
            {
                _helper.WriteTextFadeOut(new PointF(530, 650), "PRESS Y TO CONTINUE");
            }
        }

        private void GameOverText(Graphics g)
        {
            int x = Common.GetPlayArea().Width / 2 - 100;

            _helper.WriteLargeText(new PointF(x, 280), "GAME OVER");
            int textSize = _helper.MesureTextLarge("GAME OVER");
            int y = 330;

            _scores.ForEach(p =>
            {
                if (p.score > 0)
                {
                    _helper.WriteMediumTextCentered(new PointF(x + textSize / 2, y), $"PLAYER {NumberToName(p.PlayerId)} SCORE : {p.score}", PlayerHelper.GetPlayerColor(p.PlayerId));
                    y += 16;
                }
            });

        }

        private string NumberToName(int i)
        {
            switch (i)
            {
                case 1:
                    return "ONE";
                case 2:
                    return "TWO";
                case 3:
                    return "THREE";

                default:
                    return i.ToString();
            }
        }

        internal void Reset()
        {
            _gameOverTimer = DateTime.MinValue;
        }
    }
}
