using System.Collections.Generic;
using ChessBurger.MoveExplorer;

namespace ChessBurger.GameComponents.Pieces
{
    public class Bishop : Piece
    {
        private MoveGenerator _bishopMoveExplorer;

        public Bishop(int x, int y, bool isWhite, GameObjectID objectId) : base(x, y, isWhite, objectId)
        {
            _bishopMoveExplorer = new DiagonalMove();
            PossibleMoves = GenerateMoves();
        }

        // create image path
        public override string CreateImagePath()
        {
            if (IsID(GameObjectID.WHITE_BISHOP))
            {
                return ASSETS_PATH + "\\pieces\\wb.png";
            }
            return ASSETS_PATH + "\\pieces\\bb.png";
        }

        public override List<Cell> GenerateMoves()
        {
            return _bishopMoveExplorer.FindAllPossibleMoves(X, Y);
        }
    }
}
