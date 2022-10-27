﻿namespace ChessBurger.Game.GameCommand
{
    public enum CommandStatus
    {
        SUCCESSFUL,
        NOT_SUCCESSFUL
    }

    public interface Command
    {
        public CommandStatus Execute();
    }
}
