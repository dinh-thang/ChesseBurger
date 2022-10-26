using ChessBurger.Board;

namespace ChessBurger.Pieces.PieceCreator
{
    public class BishopCreator : PieceFactory
    {
        public override Piece CreatePiece(int x, int y, bool isWhite, GameObjectID objectID)
        {
            return new Bishop(x, y, isWhite, objectID);
        }
    }
}
