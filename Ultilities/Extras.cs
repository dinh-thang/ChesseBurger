using ChessBurger.MoveExplorer;
using System.Collections.Generic;

namespace ChessBurger.Ultilities
{
    public class Extras
    {
        // return the correspond x value of the board from the window's x
        public static double GetBoardPosX(double x)
        {
            // for some reason, the cordinate of sprites is 50 unit larger than normal cordinate? so both x and y are decreased 50 unit
            return x * 50 + 50;
        }
        // return the correspond y value of the board from the window's y
        public static double GetBoardPosY(double y)
        {
            return y * 50 + 100;
        }

        // convert window's coordinate to board's coordinate 
        // e.x: (100, 456) => (1, 4)
        public static int WindowXPosToBoardXPos(int WindowXPos)
        {
            return (WindowXPos - 100) / 50;

        }

        public static int WindowYPosToBoardYPos(int WindowYPos)
        {
            return (WindowYPos - 150) / 50;
        }

        public static int SelectedSquareXPosition(int windowXPos)
        {
            double x = GetBoardPosX(windowXPos);
            return (int) x + 60;
        }

        public static int SelectedSquareYPosition(int windowYPos)
        {
            double y = GetBoardPosY(windowYPos);
            return (int) y + 60;
        }
    }
}
