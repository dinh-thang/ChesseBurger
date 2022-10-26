using ChessBurger.Board;

namespace ChessBurger.Pieces.PieceCreator
{
    public class KnightCreator : PieceFactory
    {
        public override Piece CreatePiece(int x, int y, bool isWhite, GameObjectID objectID)
        {
            return new Knight(x, y, isWhite, objectID);
        }
    }
}
