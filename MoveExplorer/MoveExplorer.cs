using System.Collections.Generic;
using System.Data;

namespace ChessBurger.MoveExplorer
{
    public abstract class MoveExplorer
    {
        // List of all possible moves (not yet validated) of a piece
        private List<Cell> _possibleMoves;
        // return all possible moves (not yet validated) of a piece
        public abstract List<Cell> FindAllPossibleMoves(int x, int y);

        // clear all previous move
        public void ClearPossibleMove()
        {
            _possibleMoves.Clear();
        }

        public List<Cell> PossibleMoves
        {
            set { _possibleMoves = value; }
            get { return _possibleMoves; }
        }
    }
}
