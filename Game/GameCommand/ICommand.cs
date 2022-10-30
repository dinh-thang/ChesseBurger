namespace ChessBurger.Game.GameCommand
{
    public enum CommandStatus
    {
        SUCCESSFUL,
        NOT_SUCCESSFUL
    }

    public interface ICommand
    {
        public CommandStatus Execute();
    }
}
