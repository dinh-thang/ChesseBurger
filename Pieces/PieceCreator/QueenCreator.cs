using ChessBurger.Board;

namespace ChessBurger.Pieces.PieceCreator
{
    public class QueenCreator : PieceFactory
    {
        public override Piece CreatePiece(int x, int y, bool isWhite, GameObjectID objectID)
        {
            return new Queen(x, y, isWhite, objectID);
        }
    }
}
