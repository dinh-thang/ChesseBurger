using ChessBurger.Board;
namespace ChessBurger.Pieces.PieceCreator
{
    public class PawnCreator : PieceFactory
    {
        public override Piece CreatePiece(int x, int y, bool isWhite, GameObjectID objectID)
        {
            return new Pawn(x, y, isWhite, objectID);
        }
    }
}
