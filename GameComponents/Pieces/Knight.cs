using System.Collections.Generic;
using ChessBurger.MoveExplorer;

namespace ChessBurger.GameComponents.Pieces
{
    public class Knight : Piece
    {
        private MoveGenerator _knightMoveExplorer;

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
                return ASSETS_PATH + "\\pieces\\wn.png";
            }
            return ASSETS_PATH + "\\pieces\\bn.png";
        }

        public override List<Cell> GenerateMoves()
        {
            return _knightMoveExplorer.FindAllPossibleMoves(X, Y);
        }
    }
}
