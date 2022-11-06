using ChessBurger.Game.GameCommand;

namespace ChessBurger.Game
{
    public class Player
    {
        private bool _isWhite;
        private ICommand _command;

        public Player(bool isWhite)
        {
            _isWhite = isWhite;
        }

        public bool IsWhite
        {
            get
            {
                return _isWhite; 
            }
        }

        public void SetCommand(ICommand command)
        {
            _command = command;
        }

        public CommandStatus ExecuteCommand()
        {
            return _command.Execute();
        }
    }
}
