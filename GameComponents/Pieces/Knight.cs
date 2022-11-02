using ChessBurger.MoveExplorer;

namespace ChessBurger.GameComponents.Pieces
{
    public class Knight : Piece
    {
        private MoveGenerator _knightMoveExplorer;

        public Knight(int x, int y, bool isWhite, GameObjectID bmpPath) : base(x, y, isWhite, bmpPath)
        {
            _knightMoveExplorer = new KnightMove();
            MoveManager.GeneratePossibleMoves(X, Y, _knightMoveExplorer);
        }

        // reset possible move 
        public override void ResetPossibleMove()
        {
            MoveManager.ClearPossibleMoves();
            MoveManager.GeneratePossibleMoves(X, Y, _knightMoveExplorer);
        }

        // create image path
        public override string CreateImagePath()
        {
            if (IsID(GameObjectID.WHITE_KNIGHT))
            {
                return ASSETS_PATH + "\\pieces\\wn.png";
            }
            return ASSETS_PATH + "\\pieces\\bn.png";
        }
    }
}
