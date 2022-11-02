using System.Collections.Generic;
using ChessBurger.GameComponents;

namespace ChessBurger.MoveExplorer
{
    public class PawnMove : MoveGenerator
    {
        private bool _isFirstMove;
        private bool _isWhite;

        public PawnMove(bool isFirstMove, bool isWhite)
        {
            _isFirstMove = isFirstMove;
            _isWhite = isWhite;
        }

        // set firstMove value of the pawn
        public bool SetFirstMove
        {
            set
            {
                _isFirstMove = value;
            }
        }

        // generate a pawn's moves including 1 and 2 square, capture
        public override List<Cell> FindAllPossibleMoves(int locX, int locY)
        {
            int[] moveLength = { 1, 2 };
            
            // if it is the first move
            if (_isFirstMove)
            {
                if (_isWhite)
                {
                    PossibleMoves.Add(new Cell(locX, locY + moveLength[1]));
                }
                else 
                {
                    PossibleMoves.Add(new Cell(locX, locY - moveLength[1]));
                }
            }
            // this will run despite the first move or not. It allows the pawn to move 1 square ahead
            if (_isWhite)
            {
                if (locY <= 6)
                {
                    // move 2 squares ahead
                    PossibleMoves.Add(new Cell(locX, locY + moveLength[0]));
                    // capture diagonally 
                    if (locX + moveLength[0] < 8 && locX - moveLength[0] >= 0)
                    {
                        PossibleMoves.Add(new Cell(locX - moveLength[0], locY + moveLength[0]));
                        PossibleMoves.Add(new Cell(locX + moveLength[0], locY + moveLength[0]));
                    }
                }
            }

            else
            {
                if (locY >= 1)
                {
                    // move 2 squares ahead
                    PossibleMoves.Add(new Cell(locX, locY - moveLength[0]));
                    // capture diagonally
                    if (locX + moveLength[0] < 8 && locX - moveLength[0] >= 0)
                    {
                        PossibleMoves.Add(new Cell(locX - moveLength[0], locY - moveLength[0]));
                        PossibleMoves.Add(new Cell(locX + moveLength[0], locY - moveLength [0]));
                    }
                }
            }
            return PossibleMoves;
        }
    }
}
