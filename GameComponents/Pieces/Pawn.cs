using System.Collections.Generic;
using ChessBurger.MoveExplorer;

namespace ChessBurger.GameComponents.Pieces
{
    public class Pawn : Piece
    {
        private PawnMove _pawnMoveExplorer;
        private bool _isFirstMove;
        private bool _isWhite;

        public Pawn(int x, int y, bool isWhite, GameObjectID bmpPath) : base(x, y, isWhite, bmpPath)
        {
            _isFirstMove = true;
            _isWhite = isWhite;
            _pawnMoveExplorer = new PawnMove(_isFirstMove, _isWhite);
            PossibleMoves = GenerateMoves();
        }

        public bool IsFirstMove
        {
            set { _isFirstMove = value; }
        }

        public override List<Cell> GenerateMoves()
        {
            return _pawnMoveExplorer.FindAllPossibleMoves(X, Y, _isFirstMove);
        }
    }
}
