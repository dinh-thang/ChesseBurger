using System.Collections.Generic;
using ChessBurger.GameComponents.Pieces;
using ChessBurger.GameComponents;

namespace ChessBurger.MoveValidator
{
    public class DefaultValidator : IValidator
    {
        protected IValidator _nextValidator;
        private List<Cell> _illegalMove;

        public void SetNextValidator(IValidator validator)
        {
            _nextValidator = validator;
        }

        // remove any move that contain a same color piece
        public virtual void ValidCheck(Piece currentPiece, List<Piece> activePieces)
        {
            // adding illegal moves to a list
            _illegalMove = new List<Cell>();

            foreach (Piece sameColorPiece in activePieces)
            {
                if (sameColorPiece.IsWhite == currentPiece.IsWhite)
                {
                    _illegalMove.Add(new Cell(sameColorPiece.X, sameColorPiece.Y));
                }
            }
            // if a move in possible moves is in the illegal list => remove it
            for (int i = 0; i < _illegalMove.Count - 1; i++)
            {
                if (currentPiece.MoveManager.MoveExist(_illegalMove[i]))
                {
                    currentPiece.MoveManager.RemovePossibleMove(_illegalMove[i]);
                }
            }

            // pass the data to the next validator
            if (_nextValidator != null)
            {
                _nextValidator.ValidCheck(currentPiece, activePieces);
            }
        }
    }
}
