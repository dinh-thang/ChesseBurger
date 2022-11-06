using ChessBurger.GameComponents.Pieces;
using System.Collections.Generic;

namespace ChessBurger.Game
{
    public class PiecesManager
    {
        private List<Piece> _activePieces;

        public PiecesManager()
        {
            _activePieces = new List<Piece>();
        }

        public List<Piece> ActivePieces
        {
            get 
            { 
                List<Piece> temp = _activePieces;
                return temp; 
            }
        }

        public void RemovePiece(Piece piece)
        {
            _activePieces.Remove(piece);
        }

    }
}
