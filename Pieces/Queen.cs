    using System;
using System.Collections.Generic;
using System.Linq;
using ChessBurger.Board;
using ChessBurger.MoveExplorer;

namespace ChessBurger.Pieces
{
    public class Queen : Piece
    {
        private DiagonalMove _queenDiagonalMoveExplorer;
        private HorizontalAndVerticalMove _queenHorizontalMoveExplorer;

        public Queen(int x, int y, bool isWhite, GameObjectID bmpPath) : base(x, y, isWhite, bmpPath)
        {
            PossibleMoves = new List<Cell>();
            _queenDiagonalMoveExplorer = new DiagonalMove();
            _queenHorizontalMoveExplorer = new HorizontalAndVerticalMove();
            PossibleMoves = GenerateMoves();
        }
        public override List<Cell> GenerateMoves()
        {
            List<Cell> diagonalMoves = _queenDiagonalMoveExplorer.FindAllPossibleMoves(X, Y);
            List<Cell> horizontalMoves = _queenHorizontalMoveExplorer.FindAllPossibleMoves(X, Y);
            PossibleMoves = Enumerable.Concat(diagonalMoves, horizontalMoves).ToList();
            return PossibleMoves;
        }
    }
}
