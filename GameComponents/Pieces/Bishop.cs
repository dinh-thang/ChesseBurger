using ChessBurger.MoveExplorer;

namespace ChessBurger.GameComponents.Pieces
{
    public class Bishop : Piece
    {
        private MoveGenerator _bishopMoveExplorer;

        public Bishop(int x, int y, bool isWhite, GameObjectID objectId) : base(x, y, isWhite, objectId)
        {
            _bishopMoveExplorer = new DiagonalMove();
            MoveManager.GeneratePossibleMoves(X, Y, _bishopMoveExplorer);
        }

        public override bool UseDiagonalValidator
        {
            get { return true; }
        }
        public override void ResetPossibleMove()
        {
            MoveManager.ClearPossibleMoves();
            MoveManager.GeneratePossibleMoves(X, Y, _bishopMoveExplorer);
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
    }
}
