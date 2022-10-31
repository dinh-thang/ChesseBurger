using System;
using System.Collections.Generic;
using System.Linq;
using ChessBurger.MoveExplorer;

namespace ChessBurger.GameComponents.Pieces
{
    public class Queen : Piece
    {
        private MoveGenerator _queenDiagonalMoveExplorer;
        private MoveGenerator _queenHorizontalMoveExplorer;

        public Queen(int x, int y, bool isWhite, GameObjectID objectID) : base(x, y, isWhite, objectID)
        {
            PossibleMoves = new List<Cell>();
            _queenDiagonalMoveExplorer = new DiagonalMove();
            _queenHorizontalMoveExplorer = new HorizontalAndVerticalMove();
            PossibleMoves = GenerateMoves();
        }

        // return true since it use linear validator
        public override bool UseLinearValidator
        {
            get { return true; }
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

        public override List<Cell> GenerateMoves()
        {
            List<Cell> diagonalMoves = _queenDiagonalMoveExplorer.FindAllPossibleMoves(X, Y);
            List<Cell> horizontalMoves = _queenHorizontalMoveExplorer.FindAllPossibleMoves(X, Y);
            PossibleMoves = diagonalMoves.Concat(horizontalMoves).ToList();
            return PossibleMoves;
        }
    }
}
