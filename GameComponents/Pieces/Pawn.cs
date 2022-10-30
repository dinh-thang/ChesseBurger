using System.Collections.Generic;
using ChessBurger.MoveExplorer;

namespace ChessBurger.GameComponents.Pieces
{
    public class Pawn : Piece
    {
        private PawnMove _pawnMoveExplorer;
        private bool _isFirstMove;
        private bool _isWhite;

        public Pawn(int x, int y, bool isWhite, GameObjectID objectID) : base(x, y, isWhite, objectID)
        {
            _isFirstMove = true;
            _isWhite = isWhite;
            _pawnMoveExplorer = new PawnMove(_isFirstMove, _isWhite);
            PossibleMoves = GenerateMoves();
        }

        // create image path
        public override string CreateImagePath()
        {
            if (IsID(GameObjectID.WHITE_PAWN))
            {
                return "\\pieces\\wp.png";
            }
            else
            {
                return "\\pieces\\bp.png";
            }
        }

        // return true if piece haven't move 
        public bool FirstMove
        {
            set { _isFirstMove = value; }
            get { return _isFirstMove; }
        }

        public override List<Cell> GenerateMoves()
        {
            return _pawnMoveExplorer.FindAllPossibleMoves(X, Y, _isFirstMove);
        }
    }
}
