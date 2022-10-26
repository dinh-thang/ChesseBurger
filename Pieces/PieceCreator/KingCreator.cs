using ChessBurger.Board;

namespace ChessBurger.Pieces.PieceCreator
{
    public class KingCreator : PieceFactory
    {
        public override Piece CreatePiece(int x, int y, bool isWhite, GameObjectID objectID)
        {
            return new King(x, y, isWhite, objectID);
        }
    }
}
