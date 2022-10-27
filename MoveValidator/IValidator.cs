using ChessBurger.GameComponents.Pieces;
using System.Collections.Generic;

namespace ChessBurger.MoveValidator
{
    public enum ValidatorID
    {
        DEFAULT,
        LINEAR,
        DIAGONAL
    }

    public interface IValidator
    {
        public void SetNextValidator(IValidator validator);
        public void ValidCheck(Piece piece, List<Piece> activePieces);
    }
}
