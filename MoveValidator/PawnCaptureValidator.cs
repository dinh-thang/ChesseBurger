using ChessBurger.GameComponents;
using ChessBurger.GameComponents.Pieces;
using System;
using System.Collections.Generic;


namespace ChessBurger.MoveValidator
{
    public class PawnCaptureValidator : DefaultValidator
    {
        // check for en passant and diagonal capture
        public override void ValidCheck(Piece currentPiece, List<Piece> activePieces)
        {
            if (currentPiece.IsID(GameComponents.GameObjectID.WHITE_PAWN) || currentPiece.IsID(GameComponents.GameObjectID.BLACK_PAWN))
            {
                Pawn pawn = currentPiece as Pawn;
                List<Cell> diagonalCaptureMoves = new List<Cell>();

                // get the diagonal move
                foreach (Cell move in pawn.MoveManager.PossibleMovesClone)
                {
                    if (move.X != pawn.X)
                    {
                        diagonalCaptureMoves.Add(move);
                    }
                }

                // capture check 
                // if there is no piece => remove capture move
                foreach (Piece piece in activePieces)
                {
                    // checking for a piece that is in the diagonal capture move of the pawn
                    if (piece.X != pawn.X && piece.IsWhite != pawn.IsWhite)
                    {
                        Cell piecePosition = new Cell(piece.X, piece.Y);
                        
                        // if a piece is in the diagonal capture moves list => remove that move from the list
                        // the leftover move will be remove from the pawn's possible move
                        if (diagonalCaptureMoves.Contains(piecePosition))
                        {
                            diagonalCaptureMoves.Remove(piecePosition);
                            
                            
                        }

                    }
                }
                // remove the leftover move in the list
                foreach (Cell move in diagonalCaptureMoves)
                {
                    pawn.MoveManager.RemovePossibleMove(move);
                }

                // en passant check
                // if 2 side of the pawn have another pawn, check its en passant value
                // if true => capture
            }
            if (_nextValidator != null)
            {
                _nextValidator.ValidCheck(currentPiece, activePieces);
            }
        }
    }
}
