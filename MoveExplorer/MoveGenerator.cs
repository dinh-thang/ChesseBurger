using System.Collections.Generic;
using System.Data;
using ChessBurger.GameComponents;

namespace ChessBurger.MoveExplorer
{
    public abstract class MoveGenerator
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

        protected List<Cell> PossibleMoves
        {
            set { _possibleMoves = value; }
            get { return _possibleMoves; }
        }
    }
}
