using System.Windows.Forms;

namespace SpaceInvaders
{
    public class InputHandler
    {
        private Game _game;

        private string _currentKeyP1 = "";
        private string _previousKeyP1 = "";

        private string _currentKeyP2 = "";
        private string _previousKeyP2 = "";

        private string _currentKeyP3 = "";
        private string _previousKeyP3 = "";
        private bool shoot1, shoot2, shoot3;

        public InputHandler(Game game)
        {
            _game = game;
        }

        public void HandleKeys()
        {
            HandleKeys(_currentKeyP1, _previousKeyP1, shoot1, 1);
            HandleKeys(_currentKeyP2, _previousKeyP2, shoot2, 2);
            HandleKeys(_currentKeyP3, _previousKeyP3, shoot3, 3);
            shoot1 = false;
            shoot2 = false;
            shoot3 = false;
        }

        private void HandleKeys(string currentKey, string previousKey, bool shoot, int player)
        {
            if (shoot)
            {
                _game.ShootAndInitialize(player);
            }

            switch (currentKey)
            {
                case "R":
                    _game.MoveR(player);
                    break;
                case "L":
                    _game.MoveL(player);
                    break;
                default:
                    break;
            }
        }

        public void KeyUp(Keys key)
        {
            
            if (key == Keys.A && _currentKeyP1 != "R" || key == Keys.D && _currentKeyP1 != "L")
            {
                _currentKeyP1 = "";
            }

            if (key == Keys.Left && _currentKeyP2 != "R" || key == Keys.Right && _currentKeyP2 != "L")
            {
                _currentKeyP2 = "";
            }

            if (key == Keys.H && _currentKeyP3 != "R" || key == Keys.K && _currentKeyP3 != "L")
            {
                _currentKeyP3 = "";
            }

            if (key == Keys.Escape || key == Keys.Pause || key == Keys.F1 || key == Keys.F2 || key == Keys.F3 || key == Keys.F4 || key == Keys.F5 || key == Keys.F6 || key == Keys.F7 || key == Keys.F8 || key == Keys.F9 || key == Keys.F10 || key == Keys.F11 || key == Keys.F12)
            {
                _currentKeyP1 = "";
                _currentKeyP2 = "";
                _currentKeyP3 = "";
                _game.PauseGame();

            }
            if (key == Keys.Y)
            {
                _game.Restart();
            }
        }

        public void KeyDown(Keys key)
        {
            if (key == Keys.A)
            {
                _previousKeyP1 = "L";
                _currentKeyP1 = _previousKeyP1;
            }

            if (key == Keys.D)
            {
                _previousKeyP1 = "R";
                _currentKeyP1 = _previousKeyP1;
            }

            if (key == Keys.W)
            {
                shoot1 = true;
            }

            if (key == Keys.Left)
            {
                _previousKeyP2 = "L";
                _currentKeyP2 = _previousKeyP2;
            }
            if (key == Keys.Right)
            {
                _previousKeyP2 = "R";
                _currentKeyP2 = _previousKeyP2;
            }

            if (key == Keys.Up)
            {
                shoot2 = true;
            }

            if (key == Keys.H)
            {
                _previousKeyP3 = "L";
                _currentKeyP3 = _previousKeyP3;
            }

            if (key == Keys.K)
            {
                _previousKeyP3 = "R";
                _currentKeyP3 = _previousKeyP3;
            }
            if (key == Keys.U)
            {
                shoot3 = true;
            }
        }
    }
}
