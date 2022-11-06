using ChessBurger.Game.GameCommand;
using ChessBurger.GameComponents.Pieces;
using System.Collections.Generic;
using System.ComponentModel.Design;

namespace ChessBurger.MoveValidator
{
    public enum ValidatorID
    {
        DEFAULT,
        LINEAR,
        DIAGONAL,
        CASTLE, 
        PAWN_CAPTURE,
        CHECK
    }

    public interface IValidator
    {
        public void SetNextValidator(IValidator validator);
        public CommandStatus ValidCheck(Piece piece, List<Piece> activePieces);
    }
}
