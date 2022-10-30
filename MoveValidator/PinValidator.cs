

using ChessBurger.GameComponents.Pieces;
using System.Collections.Generic;

namespace ChessBurger.MoveValidator
{
    public class PinValidator : DefaultValidator
    {
        private Piece _king;
        // remove all possible move of a piece if they are pinned
        // loop all opponent pieces => get possible move.
        // If the coordinate of the king is in their possible move
        // check if there A piece in the range => if yes remove all the move of that piece
        // if there are many piece => discard
        // if there is no piece => remove all move execept for king move out of the danger zone 
        public override void ValidCheck(Piece currentPiece, List<Piece> activePieces)
        {
            _king = GetSameColorKing(currentPiece, activePieces);
        }

         // get king same color as piece
        private Piece GetSameColorKing(Piece currentPiece, List<Piece> activePieces)
        {
            foreach (Piece piece in activePieces)
            {
                if (currentPiece.IsWhite == piece.IsWhite)
                {
                    if (piece.IsID(GameComponents.GameObjectID.WHITE_KING) || piece.IsID(GameComponents.GameObjectID.BLACK_KING))
                    {
                        return piece;
                    }
                }
            }
            return null;
        }

        private void KingChecked(King king, List<Piece> activePieces)
        {

        }

        private void RemovePinPieceMove(King king, List<Piece> activePieces)
        {

        }

        
    }
}
