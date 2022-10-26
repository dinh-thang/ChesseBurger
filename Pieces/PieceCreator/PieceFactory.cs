using ChessBurger.Board;

namespace ChessBurger.Pieces.PieceCreator
{
    public abstract class PieceFactory
    {
        abstract public Piece CreatePiece(int x, int y, bool isWhite, GameObjectID objectID);
    }
}
