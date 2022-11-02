using ChessBurger.GameComponents;
using ChessBurger.GameComponents.Pieces;
using System;
using System.Collections.Generic;

namespace ChessBurger.MoveValidator
{
    public class DiagonalBlockValidator : DefaultValidator
    {
        override public void ValidCheck(Piece currentPiece, List<Piece> activePieces)
        {
            // this list contain positions that contain a piece in the current piece's possible moves list
            List<Cell> blockingMovesInPossibleMoves = new List<Cell>();

            for (int i = 0; i < activePieces.Count; i++)
            {
                if (!activePieces[i].UseDiagonalValidator)
                {
                    continue;
                }
                else
                {
                    // add cell that have piece
                    for (int i = 0; i < activePieces.Count; i++)
                    {
                        Cell cellContainPieceInPossibleMove = new Cell(activePieces[i].X, activePieces[i].Y);
                    
                        if (currentPiece.MoveManager.MoveExist(cellContainPieceInPossibleMove) && activePieces[i] != currentPiece)
                        {
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
            for (int i = currentPiece.MoveManager.PossibleMoveCount - 1; i >= 0; i--)
            {
                if (currentPiece.MoveManager.PossibleMovesClone[i].X > blockingPieceX && currentPiece.MoveManager.PossibleMovesClone[i].Y < blockingPieceY)
                {
                    currentPiece.MoveManager.RemovePossibleMove(currentPiece.MoveManager.PossibleMovesClone[i]);
                }
            }
        }

        private void RemoveTopLeftMoves(int blockingPieceX, int blockingPieceY, Piece currentPiece)
        {
            for (int i = currentPiece.MoveManager.PossibleMoveCount - 1; i >= 0; i--)
            {
                if (currentPiece.MoveManager.PossibleMovesClone[i].X < blockingPieceX && currentPiece.MoveManager.PossibleMovesClone[i].Y < blockingPieceY)
                {
                    currentPiece.MoveManager.RemovePossibleMove(currentPiece.MoveManager.PossibleMovesClone[i]);
                }
            }
        }

        private void RemoveBottomRightMoves(int blockingPieceX, int blockingPieceY, Piece currentPiece)
        {
            for (int i = currentPiece.MoveManager.PossibleMoveCount - 1; i >= 0; i--)
            {
                if (currentPiece.MoveManager.PossibleMovesClone[i].X > blockingPieceX && currentPiece.MoveManager.PossibleMovesClone[i].Y > blockingPieceY)
                {
                    currentPiece.MoveManager.RemovePossibleMove(currentPiece.MoveManager.PossibleMovesClone[i]);
                }
            }
        }

        private void RemoveBottomLeftMoves(int blockingPieceX, int blockingPieceY, Piece currentPiece)
        {
            for (int i = currentPiece.MoveManager.PossibleMoveCount - 1; i >= 0; i--)
            {
                if (currentPiece.MoveManager.PossibleMovesClone[i].X < blockingPieceX && currentPiece.MoveManager.PossibleMovesClone[i].Y > blockingPieceY)
                {
                    currentPiece.MoveManager.RemovePossibleMove(currentPiece.MoveManager.PossibleMovesClone[i]);
                }
            }
        }
    }
}
