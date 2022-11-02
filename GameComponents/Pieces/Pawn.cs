using ChessBurger.MoveExplorer;

namespace ChessBurger.GameComponents.Pieces
{
    public class Pawn : Piece
    {
        private MoveGenerator _pawnMoveExplorer;
        private bool _isFirstMove;
        private bool _isWhite;

        public Pawn(int x, int y, bool isWhite, GameObjectID objectID) : base(x, y, isWhite, objectID)
        {
            _isFirstMove = true;
            _isWhite = isWhite;
            _pawnMoveExplorer = new PawnMove(_isFirstMove, _isWhite);
            MoveManager.GeneratePossibleMoves(X, Y, _pawnMoveExplorer);
        }

        // return true since it use linear validator
        public override bool UseLinearValidator
        {
            get { return true; }
        }

        // return true if piece haven't move 
        public bool FirstMove
        {
            set { _isFirstMove = value; }
            get { return _isFirstMove; }
        }

        // create image path
        public override string CreateImagePath()
        {
            if (IsID(GameObjectID.WHITE_PAWN))
            {
                return ASSETS_PATH + "\\pieces\\wp.png";
            }
            else
            {
                return ASSETS_PATH + "\\pieces\\bp.png";
            }
        }


        public override void ResetPossibleMove()
        {
            MoveManager.ClearPossibleMoves();
            (_pawnMoveExplorer as PawnMove).SetFirstMove = FirstMove;
            MoveManager.GeneratePossibleMoves(X, Y, _pawnMoveExplorer);
        }
    }
}
