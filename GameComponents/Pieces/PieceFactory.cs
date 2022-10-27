namespace ChessBurger.GameComponents.Pieces
{
    public class PieceFactory 
    {
        public Piece CreatePiece(int x, int y, bool isWhite, GameObjectID objectID)
        {
            switch (objectID)
            {
                case GameObjectID.BLACK_BISHOP:
                case GameObjectID.WHITE_BISHOP:
                    return new Bishop(x, y, isWhite, objectID);
                case GameObjectID.WHITE_KING:
                case GameObjectID.BLACK_KING:
                    return new King(x, y, isWhite, objectID);
                case GameObjectID.BLACK_KNIGHT:
                case GameObjectID.WHITE_KNIGHT:
                    return new Knight(x, y, isWhite, objectID);
                case GameObjectID.BLACK_QUEEN:
                case GameObjectID.WHITE_QUEEN:
                    return new Queen(x, y, isWhite, objectID);
                case GameObjectID.BLACK_PAWN:
                case GameObjectID.WHITE_PAWN:
                    return new Pawn(x, y, isWhite, objectID);
                case GameObjectID.BLACK_ROOK:
                case GameObjectID.WHITE_ROOK:
                    return new Rook(x, y, isWhite, objectID);
            }
            return null;
        }
    }
}
