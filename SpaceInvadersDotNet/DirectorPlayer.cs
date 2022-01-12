using SpaceInvaders.Enums;
using SpaceInvaders.GameItems;
using System.Collections.Generic;
using System.Drawing;

namespace SpaceInvaders.Helpers
{
    public class DirectorPlayer
    {
        private Dictionary<int, Player> _players = null;
        private Dictionary<int, Torpedo> _torpedoes = null;

        public DirectorPlayer()
        {
            Initialize();
        }

        public void Initialize()
        {
            _players = new Dictionary<int, Player>();
            _torpedoes = new Dictionary<int, Torpedo>();
        }

        public void PaintPlayers(Graphics g)
        {
            foreach (int id in _players.Keys)
            {
                _players[id].DrawImage(g);
                _torpedoes[id].DrawImage(g);
            }
        }

        public void PlayTorpedo(int playerId)
        {
            if (_players.ContainsKey(playerId))
            {
                _torpedoes[playerId].Play();
            }
        }

        public void MoveRight(int playerId)
        {
            if (_players.ContainsKey(playerId))
            {
                _players[playerId].MoveRight();
            }

        }

        public void MoveLeft(int playerId)
        {
            if (_players.ContainsKey(playerId))
            {
                _players[playerId].MoveLeft();
            }
        }

        public void Launch(int playerId)
        {
            _torpedoes[playerId].Launch(_players[playerId].GetLaunchStart());
        }

        public bool CreatePlayerIfNotCreated(int playerId)
        {
            if (Common.CurrentRunState == GameState.StartupScreen || Common.CurrentRunState == GameState.Paused)
            {
                Common.CurrentRunState = GameState.Starting;
            }
            if (!_players.ContainsKey(playerId))
            {
                _players.Add(playerId, new Player(playerId));
                _torpedoes.Add(playerId, new Torpedo());
                return true;
            }
            return false;
        }

        public CharacterState GetState(int playerId)
        {
            if (!_players.ContainsKey(playerId))
            {
                return CharacterState.Undefined;
            }
            return _players[playerId].GetState();
        }

        public void Reset(int playerId)
        {
            _players[playerId].Reset();
        }

        public Rectangle GetMovingBounds(int playerId)
        {
            return _players[playerId].GetMovingBounds();
        }

        public void Die(int playerId)
        {
            _players[playerId].Die();
        }

        public Rectangle GetTorpedoMovingBounds(int playerId)
        {
            return _torpedoes[playerId].GetMovingBounds();
        }

        public bool TorpedoIsRunning(int playerId)
        {
            if (!_players.ContainsKey(playerId))
            {
                return false;
            }
            return _torpedoes[playerId].IsRunning();
        }

        public void TorpedoReset(int playerId)
        {
            _torpedoes[playerId].Reset();
        }

        public void TorpedoResetAll()
        {
            foreach (int id in _torpedoes.Keys)
            {
                _torpedoes[id].Reset();
            }
        }
    }
}
