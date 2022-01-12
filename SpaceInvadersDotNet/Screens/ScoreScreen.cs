using SpaceInvaders.Helpers;
using SpaceInvaders.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;

namespace SpaceInvaders.Screens
{
    public class ScoreScreen : BaseScreen
    {
        private Dictionary<int, int> _scores = new Dictionary<int, int>();
        FontHelper _helper = new FontHelper();
        NumberFormatInfo _nf = null;

        public ScoreScreen()
        {
            _nf = new CultureInfo("en-US", false).NumberFormat;
            _nf.NumberGroupSeparator = ".";
        }

        public void AddScore(int score, int playerId)
        {
            _scores[playerId] = _scores[playerId] + score;
        }

        public override void Paint(Graphics g)
        {
            _helper.InitializeGraphics(g);
            Text(g);
        }

        private void Text(Graphics g)
        {
            int x = 20;
            for (int i = 0; i < 3; i++)
            {
                if (_scores.ContainsKey(i + 1))
                {
                    _helper.WriteMediumText(new PointF(x + (i * 310), 20), "SCORE:" + FormatScore(_scores[i + 1]), PlayerHelper.GetPlayerColor(i + 1));
                }
            }
        }

        private string FormatScore(int i)
        {
            return i.ToString("N0", _nf);
        }

        public void ResetScore(int playerId)
        {
            if (!_scores.ContainsKey(playerId))
            {
                _scores.Add(playerId, 0);
            }
            else
            {
                _scores[playerId] = 0;
            }
        }

        public List<ScoreValueDto> GetAllScores()
        {
            var ret = new List<ScoreValueDto>();
            foreach (var item in _scores)
            {
                ret.Add(new ScoreValueDto
                {
                    PlayerId = item.Key,
                    score = item.Value
                });
            }
            return ret;
        }
    }
}
