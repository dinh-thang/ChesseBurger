
using ChessBurger.GameComponents;
using ChessBurger.GameComponents.Pieces;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace ChessBurger.MoveValidator
{
    public class KingCheckedValidator : DefaultValidator
    {
        // if move is in a opposite piece's possible move
        // => set the king is checked ( the next move will not be display but validated before display) 
        //  
        public KingCheckedValidator()
        {

        }

        public override void ValidCheck(Piece currentPiece, List<Piece> activePieces)
        {
            // if the piece to be checked is not king => continue
            if (!currentPiece.IsID(GameComponents.GameObjectID.WHITE_KING) || !currentPiece.IsID(GameComponents.GameObjectID.BLACK_KING))
            {
                return;
            }
            // if pieceChecking is not null => king is checked
            Piece pieceChecking = PieceChecking(currentPiece, activePieces);
            
            if (pieceChecking != null)
            {

            }

            if (_nextValidator != null)
            {
                _nextValidator.ValidCheck(currentPiece, activePieces);
            }

        } 

        // check for 

        public Piece PieceChecking(Piece king, List<Piece> activePieces)
        {
            for (int i = 0; i < activePieces.Count; i++)
            {
                if (activePieces[i].IsWhite != king.IsWhite)
                {
                    if (activePieces[i].MoveManager.MoveExist(new GameComponents.Cell(king.X, king.Y)))
                    {
                        return activePieces[i];
                    }
                }
            }
            return null;
        }

        // return null if not found
        public Piece FindPiece(Cell position, List<Piece> activePieces)
        {
            foreach (Piece piece in activePieces)
            {
                if (piece.X == position.X && piece.Y == position.Y)
                {
                    return piece;
                }
            }
            return null;
        }

        public Piece CheckPieceBlocking(Piece king, List<Piece> activePieces)
        {
            return null;
        }
    }
}
