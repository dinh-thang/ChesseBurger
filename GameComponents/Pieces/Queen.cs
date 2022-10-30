using System;
using System.Collections.Generic;
using System.Linq;
using ChessBurger.MoveExplorer;

namespace ChessBurger.GameComponents.Pieces
{
    public class Queen : Piece
    {
        private DiagonalMove _queenDiagonalMoveExplorer;
        private HorizontalAndVerticalMove _queenHorizontalMoveExplorer;

        public Queen(int x, int y, bool isWhite, GameObjectID objectID) : base(x, y, isWhite, objectID)
        {
            PossibleMoves = new List<Cell>();
            _queenDiagonalMoveExplorer = new DiagonalMove();
            _queenHorizontalMoveExplorer = new HorizontalAndVerticalMove();
            PossibleMoves = GenerateMoves();
        }

        // create image path
        public override string CreateImagePath()
        {
            if (IsID(GameObjectID.WHITE_QUEEN))
            {
                return "\\pieces\\wq.png";
            }
            return "\\pieces\\bq.png";
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
