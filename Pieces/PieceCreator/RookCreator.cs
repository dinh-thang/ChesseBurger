using ChessBurger.Board;

namespace ChessBurger.Pieces.PieceCreator
{
    public class RookCreator : PieceFactory
    {
        public override Piece CreatePiece(int x, int y, bool isWhite, GameObjectID objectID)
        {
            return new Rook(x, y, isWhite, objectID);
        }
    }
}
