using System.Collections.Generic;
using ChessBurger.GameComponents;

namespace ChessBurger.MoveExplorer
{
    public class DiagonalMove : MoveGenerator
    {
        public DiagonalMove()
        {
            PossibleMoves = new List<Cell>();
        }

        public override List<Cell> FindAllPossibleMoves(int locX, int locY)
        {
            ClearPossibleMove();

            int i = 1;

            // check for 4 directions, if there is any available direction then enter the loop
            // each iteration will look for moves in 4 directions
            while (locX + i < 8 || locY + i < 8 || locX - i > 0 || locY - i > 0)
            {
                // check if there any possible moves in the top right
                if (locX + i < 8 && locY - i >= 0)
                {
                    Cell topRight = new Cell(locX + i, locY - i);
                    PossibleMoves.Add(topRight);
                }
                // check if there any possible moves in the top left
                if (locX - i >= 0 && locY - i >= 0)
                {
                    Cell topLeft = new Cell(locX - i, locY - i);
                    PossibleMoves.Add(topLeft);
                }
                // check if there any possible moves in the bottom left
                if (locX - i >= 0 && locY + i < 8)
                {
                    Cell bottomLeft = new Cell(locX - i, locY + i);
                    PossibleMoves.Add(bottomLeft);
                } 
                // check if there any possible moves in the bottom right
                if (locX + i < 8 && locY + i < 8)
                {
                    Cell bottomRight = new Cell(locX + i, locY + i);
                    PossibleMoves.Add(bottomRight);
                }
                i++;
            }
            return PossibleMoves;
        }
    }
}
