using ChessBurger.MoveExplorer;

namespace ChessBurger.GameComponents.Pieces
{
    public class Rook : Piece
    {
        private bool _isFirstMove;
        private MoveGenerator _rookMoveExplorer;

        public Rook(int x, int y, bool isWhite, GameObjectID objectID) : base(x, y, isWhite, objectID)
        {
            _isFirstMove = true;
            _rookMoveExplorer = new HorizontalAndVerticalMove();
            MoveManager.GeneratePossibleMoves(X, Y, _rookMoveExplorer);
        }

        // return true since it use linear validator
        public override bool UseLinearValidator
        {
            get { return true; }
        }

        public bool FirstMove
        {
            get { return _isFirstMove; }
            set { _isFirstMove = value; }
        }

        public override void ResetPossibleMove()
        {
            MoveManager.ClearPossibleMoves();
            MoveManager.GeneratePossibleMoves(X, Y, _rookMoveExplorer);
        }

        // return image path
        public override string CreateImagePath()
        {
            if (IsID(GameObjectID.WHITE_ROOK))
            {
                return ASSETS_PATH + "\\pieces\\wr.png";
            }
            return ASSETS_PATH + "\\pieces\\br.png";
        }
    }
}
