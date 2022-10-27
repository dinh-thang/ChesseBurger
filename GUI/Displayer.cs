using System.Collections.Generic;
using ChessBurger.lib;
using ChessBurger.Ultilities;
using ChessBurger.GameComponents.Pieces;
using ChessBurger.GameComponents;

namespace ChessBurger.GUI
{
    public class Displayer
    {
        public Displayer()
        {
        }

        // display pieces
        public void DisplayPiece(List<Piece> activePieces)
        {
            for (int i = 0; i < activePieces.Count; i++)
            {
                double locX = Extras.GetBoardPosX(activePieces[i].X);
                double locY = Extras.GetBoardPosY(activePieces[i].Y);

                SplashKit.DrawSprite(activePieces[i].GetSprite, locX, locY);
            }
        }

        // display clicked square
        public void DisplaySelectedSquare(SelectedSquare square)
        {
            int boardX = Extras.WindowXPosToBoardXPos(square.X);
            int boardY = Extras.WindowYPosToBoardYPos(square.Y);

            SplashKit.DrawBitmap(square.GetBitmap, boardX, boardY);
        }

        // display board starting from x, y
        public void DisplayBoard(Board board)
        {
            SplashKit.DrawBitmap(board.GetBitmap, board.X, board.Y);
        }

        public void DisplayBoardIndex()
        {
            for (int i = 0; i < 8; i++)
            {
                SplashKit.DrawText($"{i}", SplashKit.ColorBlack(), 125 + (i * 50), 125);
                SplashKit.DrawText($"{i}", SplashKit.ColorBlack(), 75, 175 + (i * 50));
            }
        }
    }
}
