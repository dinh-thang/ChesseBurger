using ChessBurger.MoveExplorer;

namespace ChessBurger.GameComponents.Pieces
{
    public class Queen : Piece
    {
        private MoveGenerator _queenDiagonalMoveExplorer;
        private MoveGenerator _queenHorizontalMoveExplorer;

        public Queen(int x, int y, bool isWhite, GameObjectID objectID) : base(x, y, isWhite, objectID)
        {
            _queenDiagonalMoveExplorer = new DiagonalMove();
            _queenHorizontalMoveExplorer = new HorizontalAndVerticalMove();
            MoveManager.GeneratePossibleMoves(X, Y, _queenHorizontalMoveExplorer, _queenDiagonalMoveExplorer);
        }

        // return true since it use linear validator
        public override bool UseLinearValidator
        {
            get { return true; }
        }
        public override bool UseDiagonalValidator
        {
            get { return true; }
        }

        // reset and re-generate moves
        public override void ResetPossibleMove()
        {
            MoveManager.ClearPossibleMoves();
            MoveManager.GeneratePossibleMoves(X, Y, _queenHorizontalMoveExplorer, _queenDiagonalMoveExplorer);
        }

        // create image path
        public override string CreateImagePath()
        {
            if (IsID(GameObjectID.WHITE_QUEEN))
            {
                return ASSETS_PATH + "\\pieces\\wq.png";
            }
            return ASSETS_PATH + "\\pieces\\bq.png";
        }
    }
}
