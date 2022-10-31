using System.Collections.Generic;
using ChessBurger.GameComponents;

namespace ChessBurger.MoveExplorer
{
    public class KnightMove : MoveGenerator
    {
        public KnightMove()
        {
            PossibleMoves = new List<Cell>();
        }

        public override List<Cell> FindAllPossibleMoves(int locX, int locY)
        {
            ClearPossibleMove();

            int[,] relativeDistanceFromPiece = new int[,] { { 1, -2 }, { 2, -1 }, {2, 1}, {1, 2}, {-1, 2}, {-2, 1}, {-2, -1}, {-1, -2} };

            // each value in the 2D array represent the distance from the cur position to a possible move.
            // Adding the cur pos with the relative distance = a possible pos
            for (int i = 0; i < relativeDistanceFromPiece.GetLength(0); i++)
            {
                int possibleX = locX + relativeDistanceFromPiece[i, 0];
                int possibleY = locY + relativeDistanceFromPiece[i, 1];

                // add if the position is inbound
                if (possibleX < 8 && possibleX >= 0 && possibleY < 8 && possibleY >= 0)
                {
                    PossibleMoves.Add(new Cell(possibleX, possibleY));
                } 
            }
            return PossibleMoves;
        }
    }
}
