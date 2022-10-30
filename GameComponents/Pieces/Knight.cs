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

        // create image path
        public override string CreateImagePath()
        {
            if (IsID(GameObjectID.WHITE_KNIGHT))
            {
                return "\\pieces\\wn.png";
            }
            return "\\pieces\\bn.png";
        }

        public override List<Cell> GenerateMoves()
        {
            return _knightMoveExplorer.FindAllPossibleMoves(X, Y);
        }
    }
}
