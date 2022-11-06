using ChessBurger.Game.GameCommand;
using ChessBurger.GameComponents;
using ChessBurger.GameComponents.Pieces;
using System.Collections.Generic;
using System.ComponentModel.Design;

namespace ChessBurger.MoveValidator
{
    public class LinearBlockValidator : DefaultValidator
    {
        public override CommandStatus ValidCheck(Piece currentPiece, List<Piece> activePieces)
        {
            if (currentPiece.UseLinearValidator)
            {               
                // this list contain positions that contain a piece in the current piece's possible moves list
                List<Cell> blockingMovesInPossibleMoves = new List<Cell>();

                for (int i = 0; i < activePieces.Count; i++)
                {
                    Cell cellContainPieceInPossibleMove = new Cell(activePieces[i].X, activePieces[i].Y);
                    
                    if (currentPiece.MoveManager.MoveExist(cellContainPieceInPossibleMove) && activePieces[i] != currentPiece)
                    {
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
            }
            if (_nextValidator != null)
            {
                _nextValidator.ValidCheck(currentPiece, activePieces);
            }
            return CommandStatus.SUCCESSFUL;
        }
        private void RemoveRightMoves(int blockPieceX, int blockPieceY, Piece currentPiece)
        {
            for (int i = currentPiece.MoveManager.PossibleMoveCount - 1; i >= 0; i--)
            {
                if (currentPiece.MoveManager.PossibleMovesClone[i].X > blockPieceX && currentPiece.MoveManager.PossibleMovesClone[i].Y == blockPieceY)
                {
                    currentPiece.MoveManager.RemovePossibleMove(currentPiece.MoveManager.PossibleMovesClone[i]);
                }
            }
        }

        private void RemoveLeftMoves(int blockPieceX, int blockPieceY, Piece currentPiece)
        {
            for (int i = currentPiece.MoveManager.PossibleMoveCount - 1; i >= 0; i--)
            {
                if (currentPiece.MoveManager.PossibleMovesClone[i].X < blockPieceX && currentPiece.MoveManager.PossibleMovesClone[i].Y == blockPieceY)
                {
                    currentPiece.MoveManager.RemovePossibleMove(currentPiece.MoveManager.PossibleMovesClone[i]);
                }
            }
        }

        private void RemoveTopMoves(int blockPieceX, int blockPieceY, Piece currentPiece)
        {
            for (int i = currentPiece.MoveManager.PossibleMoveCount - 1; i >= 0; i--)
            {
                if (currentPiece.MoveManager.PossibleMovesClone[i].X == blockPieceX && currentPiece.MoveManager.PossibleMovesClone[i].Y < blockPieceY)
                {
                    currentPiece.MoveManager.RemovePossibleMove(currentPiece.MoveManager.PossibleMovesClone[i]);
                } 
                else if (currentPiece.IsID(GameObjectID.WHITE_PAWN) || currentPiece.IsID(GameObjectID.BLACK_PAWN))
                {
                    // prevent pawn capture piece directly
                    if (currentPiece.MoveManager.PossibleMovesClone[i].X == blockPieceX && currentPiece.MoveManager.PossibleMovesClone[i].Y <= blockPieceY)
                    {
                        currentPiece.MoveManager.RemovePossibleMove(currentPiece.MoveManager.PossibleMovesClone[i]);
                    }
                }
            }
        }

        private void RemoveBottomMoves(int blockPieceX, int blockPieceY, Piece currentPiece)
        {
            for (int i = currentPiece.MoveManager.PossibleMoveCount - 1; i >= 0; i--)
            {
                if (currentPiece.MoveManager.PossibleMovesClone[i].X == blockPieceX && currentPiece.MoveManager.PossibleMovesClone[i].Y >= blockPieceY)
                {
                    currentPiece.MoveManager.RemovePossibleMove(currentPiece.MoveManager.PossibleMovesClone[i]);
                }
                else if (currentPiece.IsID(GameObjectID.WHITE_PAWN) || currentPiece.IsID(GameObjectID.BLACK_PAWN))
                {
                    // prevent pawn capture piece directly
                    if (currentPiece.MoveManager.PossibleMovesClone[i].X == blockPieceX && currentPiece.MoveManager.PossibleMovesClone[i].Y >= blockPieceY)
                    {
                        currentPiece.MoveManager.RemovePossibleMove(currentPiece.MoveManager.PossibleMovesClone[i]);
                    }
                }
            }
        }
    }
}
