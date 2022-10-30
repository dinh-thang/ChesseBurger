using ChessBurger.GameComponents.Pieces;
using System.Collections.Generic;
using ChessBurger.Ultilities;
using ChessBurger.lib;

namespace ChessBurger.GUI
{
    public class PieceDisplayer : IDisplayer
    {
        private List<Piece> _activePieces;

        public PieceDisplayer(List<Piece> activePieces)
        {
            _activePieces = activePieces;
        }

        // display all pieces
        public void Display()
        {
            for (int i = 0; i < _activePieces.Count; i++)
            {
                double locX = Extras.GetBoardPosX(_activePieces[i].X);
                double locY = Extras.GetBoardPosY(_activePieces[i].Y);

                SplashKit.DrawSprite(_activePieces[i].GetSprite, locX, locY);
            }
        }
    }
}
