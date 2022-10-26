using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessBurger.MoveExplorer
{
    public class CastleMove : MoveExplorer
    {
        public CastleMove()
        {
            PossibleMoves = new List<Cell>();
        }

        public override List<Cell> FindAllPossibleMoves(int locX, int locY)
        {
            // create castle move for king
            Cell right = new Cell(locX + 2, locY);
            Cell left = new Cell(locX - 2, locY);

            PossibleMoves.Add(right);
            PossibleMoves.Add(left);
            
            return PossibleMoves;
        }

        public List<Cell> FindAllPossibleMoves(int locX, int locY, bool isFirstMove)
        {
            ClearPossibleMove();

            // if king havent move yet => castle is valid.
            // Rook is checked in a validator 
            if (isFirstMove)
            {
                FindAllPossibleMoves(locX, locY);
            }
            return PossibleMoves;
        }
    }
}
