using System.Collections.Generic;
using ChessBurger.lib;
using ChessBurger.Ultilities;
using ChessBurger.GameComponents;
using ChessBurger.GameComponents.Pieces;

namespace ChessBurger.GUI
{
    public interface IDisplayer
    {
        public void Display();

        // display board starting from x, y
        public void DisplayBoard(GameObject board)
        {
            SplashKit.DrawSprite(board.GetSprite, board.X, board.Y);
            DisplayBoardIndex();
        }

        public void DisplayBoardIndex()
        {
            for (int i = 0; i < 8; i++)
            {
                SplashKit.DrawText($"{i}", SplashKit.ColorBlack(), 125 + (i * 50), 125);
                SplashKit.DrawText($"{i}", SplashKit.ColorBlack(), 75, 175 + (i * 50));
            }
        }

        public void DisplayBun(GameObject bun)
        {
            SplashKit.DrawBitmap(bun.GetBitmap, bun.X, bun.Y);
        }
    }
}
