using System.Collections.Generic;
using ChessBurger.MoveExplorer;

namespace ChessBurger.GameComponents.Pieces
{
    public class Knight : Piece
    {
        private KnightMove _knightMoveExplorer;

        public Knight(int x, int y, bool isWhite, GameObjectID bmpPath) : base(x, y, isWhite, bmpPath)
        {
            _knightMoveExplorer = new KnightMove();
            PossibleMoves = GenerateMoves();
        }

        public override List<Cell> GenerateMoves()
        {
            return _knightMoveExplorer.FindAllPossibleMoves(X, Y);
        }
    }
}
