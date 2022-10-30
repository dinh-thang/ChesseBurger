using System;
using System.Collections.Generic;
using System.Linq;
using ChessBurger.MoveExplorer;

namespace ChessBurger.GameComponents.Pieces
{
    public class King : Piece
    {
        private CastleMove _castleMoveExplorer;
        private KingMove _kingMoveExplorer;
        private bool _isFirstMove;

        public King(int x, int y, bool isWhite, GameObjectID objectID) : base(x, y, isWhite, objectID)
        {
            _isFirstMove = true;
            _castleMoveExplorer = new CastleMove();
            _kingMoveExplorer = new KingMove();

            PossibleMoves = GenerateMoves();
        }

        // return true if piece haven't move 
        public bool FirstMove
        {
            get { return _isFirstMove; }
            set { _isFirstMove = value; }
        }

        // create image path
        public override string CreateImagePath()
        {
            if (IsID(GameObjectID.WHITE_KING))
            {
                return "\\pieces\\wk.png";
            }
            return "\\pieces\\bk.png";
        }

        public override List<Cell> GenerateMoves()
        {
            List<Cell> defaultMoves = _kingMoveExplorer.FindAllPossibleMoves(X, Y);
            List<Cell> castleMoves = _castleMoveExplorer.FindAllPossibleMoves(X, Y);
            PossibleMoves = defaultMoves.Concat(castleMoves).ToList();
            return PossibleMoves;
        }
    }
}
