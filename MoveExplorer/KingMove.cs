using System.Collections.Generic;
using ChessBurger.GameComponents;

namespace ChessBurger.MoveExplorer
{
    public class KingMove : MoveExplorer
    {
        public KingMove()
        {
            PossibleMoves = new List<Cell>();
        }

        public override List<Cell> FindAllPossibleMoves(int x, int y)
        {
            ClearPossibleMove();

            int[,] relativeDistanceFromPiece = new int[,] { 
                { 1, 1 },
                { -1, 1 },
                { 1, -1 },
                { -1, -1 },
                { 1, 0 },
                { -1, 0 },
                { 0, 1 },
                { 0, -1 },
            };

            for (int i = 0; i < relativeDistanceFromPiece.GetLength(0); i++)
            {
                int desX = x + relativeDistanceFromPiece[i, 0];
                int desY = y + relativeDistanceFromPiece[i, 1];

                if (desX >= 0 && desX < 8 && desY >=0 && desY < 8)
                {
                    PossibleMoves.Add(new Cell(desX, desY));
                }
            }
            return PossibleMoves;
        }
    }
}
