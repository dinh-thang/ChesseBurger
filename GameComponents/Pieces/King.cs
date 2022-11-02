using ChessBurger.MoveExplorer;

namespace ChessBurger.GameComponents.Pieces
{
    public class King : Piece
    {
        private MoveGenerator _castleMoveExplorer;
        private KingMove _kingMoveExplorer;
        private bool _isChecked;
        private bool _isFirstMove;

        public King(int x, int y, bool isWhite, GameObjectID objectID) : base(x, y, isWhite, objectID)
        {
            _isFirstMove = true;
            _castleMoveExplorer = new CastleMove();
            _kingMoveExplorer = new KingMove();
            MoveManager.GeneratePossibleMoves(X, Y, _kingMoveExplorer, _castleMoveExplorer);
        }

        // return true if piece haven't move 
        public bool FirstMove
        {
            get { return _isFirstMove; }
            set { _isFirstMove = value; }
        }

        public bool IsChecked
        {
            get { return _isChecked; }
            set { _isChecked = value; }
        }

        public override bool UseCastleValidator
        {
            get
            {
                return true;
            }
        }

        public override void ResetPossibleMove()
        {
            MoveManager.ClearPossibleMoves();
            MoveManager.GeneratePossibleMoves(X, Y, _kingMoveExplorer, _castleMoveExplorer);
        }

        // create image path
        public override string CreateImagePath()
        {
            if (IsID(GameObjectID.WHITE_KING))
            {
                return ASSETS_PATH + "\\pieces\\wk.png";
            }
            return ASSETS_PATH + "\\pieces\\bk.png";
        }
    }
}
