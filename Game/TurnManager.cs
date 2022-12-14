using System.Collections.Generic;

namespace ChessBurger.Game
{
    public class TurnManager
    {
        private GameState _curTurn;
        private Player _whitePlayer;
        private Player _blackPlayer;

        public TurnManager()
        {
            _whitePlayer = new Player(true); 
            _blackPlayer = new Player(false);
            _curTurn = GameState.WHITE_TURN;
        }

        public List<Player> GetPlayers
        {
            get
            {
                return new List<Player> { _blackPlayer, _whitePlayer };
            }
        }

        public Player CurrentPlayer
        {
            get 
            { 
                if (_curTurn == GameState.WHITE_TURN)
                {
                    return _whitePlayer;
                }
                return _blackPlayer; 
            }
        }

        public void SetNextTurn()
        {
            if (_curTurn == GameState.WHITE_TURN)
            {
                _curTurn = GameState.BLACK_TURN;
            }
            else
            {
                _curTurn = GameState.WHITE_TURN;
            }
        }

    }
}
