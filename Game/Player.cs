using ChessBurger.Game.GameCommand;

namespace ChessBurger.Game
{
    public class Player
    {
        private bool _isWhite;
        private Command _command;

        public Player(bool isWhite)
        {
            _isWhite = isWhite;
        }

        // get player's color
        public bool IsWhite
        {
            get
            {
                return _isWhite; 
            }
        }

        // set the command for the player
        public void SetCommand(Command command)
        {
            _command = command;
        }


        // execute the command requested from game
        public CommandStatus ExecuteCommand()
        {
            return _command.Execute();
        }
    }
}
