using ChessBurger.GameComponents;
using ChessBurger.GameComponents.Pieces;
using ChessBurger.MoveExplorer;
using System;
using System.Collections.Generic;


namespace ChessBurger.MoveValidator
{
    public class LinearBlock : DefaultValidator
    {
        public override void ValidCheck(Piece currentPiece, List<Piece> activePieces)
        {
            if (currentPiece.IsID(GameObjectID.WHITE_ROOK) || currentPiece.IsID(GameObjectID.WHITE_QUEEN) || currentPiece.IsID(GameObjectID.BLACK_ROOK) || currentPiece.IsID(GameObjectID.BLACK_ROOK))
            {

                Console.WriteLine("cur:" + currentPiece.ID);
               
                // this list contain positions that contain a piece in the current piece's possible moves list
                List<Cell> blockingMovesInPossibleMoves = new List<Cell>();

                for (int i = 0; i < activePieces.Count; i++)
                {
                    if (currentPiece.PossibleMoves.Exists(cell => cell.X == activePieces[i].X && cell.Y == activePieces[i].Y) && activePieces[i] != currentPiece)
                    {
                        Cell cellContainPieceInPossibleMove = currentPiece.PossibleMoves.Find(cell => (cell.X == activePieces[i].X && cell.Y == activePieces[i].Y));
                        //Console.WriteLine("blocked: " + cellContainPieceInPossibleMove.X + ", " + cellContainPieceInPossibleMove.Y);
                        blockingMovesInPossibleMoves.Add(cellContainPieceInPossibleMove);
                    }
                }

                // using the position of a blocking piece => remove moves behind that piece relative to the input piece
                for (int i = 0; i < blockingMovesInPossibleMoves.Count; i++)
                {
                    // check if the blocking piece is in the top direction
                    if (blockingMovesInPossibleMoves[i].X == currentPiece.X && blockingMovesInPossibleMoves[i].Y < currentPiece.Y)
                    {
                        RemoveTopMoves(blockingMovesInPossibleMoves[i].X, blockingMovesInPossibleMoves[i].Y, currentPiece);
                    }
                    // check if the blocking piece is in the left direction
                    else if (blockingMovesInPossibleMoves[i].X < currentPiece.X && blockingMovesInPossibleMoves[i].Y == currentPiece.Y)
                    {
                        RemoveLeftMoves(blockingMovesInPossibleMoves[i].X, blockingMovesInPossibleMoves[i].Y, currentPiece);
                    }
                    // check if the blocking piece is in the bottom direction
                    else if (blockingMovesInPossibleMoves[i].X == currentPiece.X && blockingMovesInPossibleMoves[i].Y > currentPiece.Y)
                    {
                        RemoveBottomMoves(blockingMovesInPossibleMoves[i].X, blockingMovesInPossibleMoves[i].Y, currentPiece);
                    }
                    // check if the blocking piece is in the right direction
                    else if (blockingMovesInPossibleMoves[i].X > currentPiece.X && blockingMovesInPossibleMoves[i].Y == currentPiece.Y)
                    {
                        RemoveRightMoves(blockingMovesInPossibleMoves[i].X, blockingMovesInPossibleMoves[i].Y, currentPiece);
                    }
                }
                foreach (Cell cell in currentPiece.PossibleMoves)
                {
                    Console.WriteLine("possible: " + cell.X + ", " + cell.Y);
                }
            }
            

            if (_nextValidator != null)
            {
                _nextValidator.ValidCheck(currentPiece, activePieces);
            }
        }
        private void RemoveRightMoves(int blockPieceX, int blockPieceY, Piece currentPiece)
        {
            for (int i = currentPiece.PossibleMoves.Count - 1; i >= 0; i--)
            {
                if (currentPiece.PossibleMoves[i].X > blockPieceX && currentPiece.PossibleMoves[i].Y == blockPieceY)
                {
                    currentPiece.PossibleMoves.Remove(currentPiece.PossibleMoves[i]);
                }
            }
        }

        private void RemoveLeftMoves(int blockPieceX, int blockPieceY, Piece currentPiece)
        {
            for (int i = currentPiece.PossibleMoves.Count - 1; i >= 0; i--)
            {
                if (currentPiece.PossibleMoves[i].X < blockPieceX && currentPiece.PossibleMoves[i].Y == blockPieceY)
                {
                    currentPiece.PossibleMoves.Remove(currentPiece.PossibleMoves[i]);
                }
            }
        }

        private void RemoveTopMoves(int blockPieceX, int blockPieceY, Piece currentPiece)
        {
            for (int i = currentPiece.PossibleMoves.Count - 1; i >= 0; i--)
            {
                if (currentPiece.PossibleMoves[i].X == blockPieceX && currentPiece.PossibleMoves[i].Y < blockPieceY)
                {
                    currentPiece.PossibleMoves.Remove(currentPiece.PossibleMoves[i]);
                }
            }
        }

        private void RemoveBottomMoves(int blockPieceX, int blockPieceY, Piece currentPiece)
        {
            for (int i = currentPiece.PossibleMoves.Count - 1; i >= 0; i--)
            {
                if (currentPiece.PossibleMoves[i].X == blockPieceX && currentPiece.PossibleMoves[i].Y > blockPieceY)
                {
                    currentPiece.PossibleMoves.Remove(currentPiece.PossibleMoves[i]);
                }
            }
        }
    }
}
