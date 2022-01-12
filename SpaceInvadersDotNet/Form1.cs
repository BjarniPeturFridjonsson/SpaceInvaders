using SpaceInvaders;
using System;
using System.Windows.Forms;

namespace SpaceInvadersDotNet
{
    public partial class Form1 : Form
    {
        private Game _game = null;
        private InputHandler _inputHandler = null;

        private Timer tmrGameTick;
        private bool _cursorVisible = true;

        public Form1()
        {
            InitializeComponent();

            //Set flicker guard.
            SetStyle(
                ControlStyles.DoubleBuffer |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint, true);

            UpdateStyles();
            _game = new Game();
            _game.Initialize();
            _inputHandler = new InputHandler(_game);
            tmrGameTick.Start();

        }
        private void frmMain_KeyUp(object sender, KeyEventArgs e)
        {
            _inputHandler.KeyUp(e.KeyData);
        }

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            _inputHandler.KeyDown(e.KeyData);
        }

        private void frmMain_Paint(object sender, PaintEventArgs e)
        {
            _game.Paint(e.Graphics);
        }


        private void frmMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_cursorVisible)
            {
                Cursor.Show();
                _cursorVisible = true;
            }
            //Text = e.Location.ToString();
        }

        int _speedCount = 0;
        long _counter = 0;

        private void tmrGameTick_Tick(object sender, EventArgs e)
        {
            _counter++;
            var start = DateTime.Now;
            tmrGameTick.Enabled = false;
            _inputHandler.HandleKeys();
            _game.Tick();
            Invalidate();//queue the redraw
            Update(); //force redraw of the whole form. 
            tmrGameTick.Enabled = true;

            if (_counter % 200 == 0)
            {
                _speedCount = 0;
            }

            if (_speedCount < (int)(DateTime.Now - start).TotalMilliseconds)
            {
                _speedCount = (int)(DateTime.Now - start).TotalMilliseconds;
            }

            // Text = "Ticks speed " + _speedCount + " mSec";

            if (_counter % 100 == 0 && _cursorVisible)
            {
                Cursor.Hide();
                _cursorVisible = false;
            }
        }
    }
}
