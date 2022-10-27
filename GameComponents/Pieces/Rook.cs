using System.Collections.Generic;
using ChessBurger.MoveExplorer;

namespace ChessBurger.GameComponents.Pieces
{
    public class Rook : Piece
    {
        private HorizontalAndVerticalMove _rookMoveExplorer;

        public Rook(int x, int y, bool isWhite, GameObjectID bmpPath) : base(x, y, isWhite, bmpPath)
        {
            _rookMoveExplorer = new HorizontalAndVerticalMove();
            PossibleMoves = GenerateMoves();
        }

        public override List<Cell> GenerateMoves()
        {
            return _rookMoveExplorer.FindAllPossibleMoves(X, Y);
        }
    }
}
