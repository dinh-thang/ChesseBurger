using System.Collections.Generic;
using ChessBurger.GameComponents;

namespace ChessBurger.MoveExplorer
{
    public abstract class MoveGenerator
    {
        // List of all possible moves (not yet validated) of a piece
        private List<Cell> _possibleMoves;

        public MoveGenerator()
        {
            _possibleMoves = new List<Cell>();
        }

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
