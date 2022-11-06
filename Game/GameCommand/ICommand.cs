namespace ChessBurger.Game.GameCommand
{
    public enum CommandStatus
    {
        SUCCESSFUL,
        NOT_SUCCESSFUL,
        WHITE_CHECKMATED,
        BLACK_CHECKMATED
    }

    public interface ICommand
    {
        public CommandStatus Execute();
    }
}
