using ChessBurger.GameComponents;
using ChessBurger.GameComponents.Pieces;
using ChessBurger.MoveExplorer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;

namespace ChessBurger.MoveValidator
{
    public class DiagonalBlock : DefaultValidator
    {
        override public void ValidCheck(Piece currentPiece, List<Piece> activePieces)
        {
            if (currentPiece.ID == GameObjectID.WHITE_BISHOP || currentPiece.ID == GameObjectID.BLACK_BISHOP || currentPiece.ID == GameObjectID.WHITE_QUEEN || currentPiece.ID == GameObjectID.BLACK_QUEEN)
            {
                // this list contain positions that contain a piece in the current piece's possible moves list
                List<Cell> blockingMovesInPossibleMoves = new List<Cell>();

                // add cell that have piece
                for (int i = 0; i < activePieces.Count; i++)
                {
                    if (currentPiece.PossibleMoves.Exists(cell => cell.X == activePieces[i].X && cell.Y == activePieces[i].Y) && activePieces[i] != currentPiece)
                    {
                        Cell cellContainPieceInPossibleMove = currentPiece.PossibleMoves.Find(cell => (cell.X == activePieces[i].X && cell.Y == activePieces[i].Y));
                        blockingMovesInPossibleMoves.Add(cellContainPieceInPossibleMove);
                    }
                }

                //using the position of a blocking piece => remove moves behind that piece relative to the input piece
                for (int i = 0; i < blockingMovesInPossibleMoves.Count; i++)
                {
                    // check if the blocking piece is in the top right direction
                    if (blockingMovesInPossibleMoves[i].X > currentPiece.X && blockingMovesInPossibleMoves[i].Y < currentPiece.Y)
                    {
                        RemoveTopRightMoves(blockingMovesInPossibleMoves[i].X, blockingMovesInPossibleMoves[i].Y, currentPiece);
                    }
                    // check if the blocking piece is in the top left direction
                    else if (blockingMovesInPossibleMoves[i].X < currentPiece.X && blockingMovesInPossibleMoves[i].Y < currentPiece.Y)
                    {
                        RemoveTopLeftMoves(blockingMovesInPossibleMoves[i].X, blockingMovesInPossibleMoves[i].Y, currentPiece);
                    }
                    // check if the blocking piece is in the bottom left direction
                    else if (blockingMovesInPossibleMoves[i].X < currentPiece.X && blockingMovesInPossibleMoves[i].Y > currentPiece.Y)
                    {
                        RemoveBottomLeftMoves(blockingMovesInPossibleMoves[i].X, blockingMovesInPossibleMoves[i].Y, currentPiece);
                    }
                    // check if the blocking piece is in the bottom right direction
                    else if (blockingMovesInPossibleMoves[i].X > currentPiece.X && blockingMovesInPossibleMoves[i].Y > currentPiece.Y)
                    {
                        RemoveBottomRightMoves(blockingMovesInPossibleMoves[i].X, blockingMovesInPossibleMoves[i].Y, currentPiece);
                    }
                }
            }

            // pass the data to the next validator
            if (_nextValidator != null)
            {
                _nextValidator.ValidCheck(currentPiece, activePieces);
            }

        }

        /* ILLEGAL MOVES REMOVERS */
        /*
            Each remover will check if there is any illegal move in their range
            For example:
                - Lets say there is a piece in the top right direction of a bishop
                - the top right remover will check if there any move beyond that piece => remove those moves from the possibleMoves list of the bishop
        */

        private void RemoveTopRightMoves(int blockingPieceX, int blockingPieceY, Piece currentPiece)
        {
            for (int i = currentPiece.PossibleMoves.Count - 1; i >= 0; i--)
            {
                if (currentPiece.PossibleMoves[i].X > blockingPieceX && currentPiece.PossibleMoves[i].Y < blockingPieceY)
                {
                    currentPiece.PossibleMoves.Remove(currentPiece.PossibleMoves[i]);
                }
            }
        }

        private void RemoveTopLeftMoves(int blockingPieceX, int blockingPieceY, Piece currentPiece)
        {
            for (int i = currentPiece.PossibleMoves.Count - 1; i >= 0; i--)
            {
                if (currentPiece.PossibleMoves[i].X < blockingPieceX && currentPiece.PossibleMoves[i].Y < blockingPieceY)
                {
                    currentPiece.PossibleMoves.Remove(currentPiece.PossibleMoves[i]);
                }
            }
        }

        private void RemoveBottomRightMoves(int blockingPieceX, int blockingPieceY, Piece currentPiece)
        {
            for (int i = currentPiece.PossibleMoves.Count - 1; i >= 0; i--)
            {
                if (currentPiece.PossibleMoves[i].X > blockingPieceX && currentPiece.PossibleMoves[i].Y > blockingPieceY)
                {
                    currentPiece.PossibleMoves.Remove(currentPiece.PossibleMoves[i]);
                }
            }
        }

        private void RemoveBottomLeftMoves(int blockingPieceX, int blockingPieceY, Piece currentPiece)
        {
            for (int i = currentPiece.PossibleMoves.Count - 1; i >= 0; i--)
            {
                if (currentPiece.PossibleMoves[i].X < blockingPieceX && currentPiece.PossibleMoves[i].Y > blockingPieceY)
                {
                    currentPiece.PossibleMoves.Remove(currentPiece.PossibleMoves[i]);
                }
            }
        }
    }
}
