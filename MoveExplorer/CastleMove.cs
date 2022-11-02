using System.Collections.Generic;
using ChessBurger.GameComponents;

namespace ChessBurger.MoveExplorer
{
    public class CastleMove : MoveGenerator
    {
        public override List<Cell> FindAllPossibleMoves(int locX, int locY)
        {
            // create castle move for king
            Cell right = new Cell(locX + 2, locY);
            Cell left = new Cell(locX - 2, locY);

            PossibleMoves.Add(right);
            PossibleMoves.Add(left);
            
            return PossibleMoves;
        }
    }
}
