using System.Collections.Generic;
using ChessBurger.Board;
using ChessBurger.MoveExplorer;

namespace ChessBurger.Pieces
{
    public class Bishop : Piece
    {
        private DiagonalMove _bishopMoveExplorer;

        public Bishop(int x, int y, bool isWhite, GameObjectID bmpPath) : base(x, y, isWhite, bmpPath)
        {
            _bishopMoveExplorer = new DiagonalMove();
            PossibleMoves = GenerateMoves();
        }

        public override List<Cell> GenerateMoves()
        {
            return _bishopMoveExplorer.FindAllPossibleMoves(X, Y);
        }
    }
}
