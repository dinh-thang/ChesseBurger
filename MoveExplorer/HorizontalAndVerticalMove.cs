using System.Collections.Generic;
using ChessBurger.GameComponents;

namespace ChessBurger.MoveExplorer
{
    public class HorizontalAndVerticalMove : MoveGenerator
    {
        public override List<Cell> FindAllPossibleMoves(int locX, int locY)
        {
            ClearPossibleMove();

            int i = 1;

            // check if there any possible moves on two side of the piece horizontally
            while (locX + i < 8 || locX - i >= 0 || locY + i < 8 || locY - i >= 0 || i < 9)
            {
                if (locX + i < 8)
                {
                    Cell right = new Cell(locX + i, locY);
                    PossibleMoves.Add(right);
                }
                if (locX - i >= 0)
                {
                    Cell left = new Cell(locX - i, locY);
                    PossibleMoves.Add(left);
                }
                if (locY + i < 8)
                {
                    Cell bottom = new Cell(locX, locY + i);
                    PossibleMoves.Add(bottom);
                }
                if (locY - i >= 0)
                {
                    Cell top = new Cell(locX, locY - i);
                    PossibleMoves.Add(top);
                }
                i++;
            } 
            return PossibleMoves;
        }
    }
}
