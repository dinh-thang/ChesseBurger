using ChessBurger.GameComponents;
using ChessBurger.GameComponents.Pieces;
using System.Collections.Generic;
using System.Linq;

namespace ChessBurger.MoveValidator
{
    public class KingCheckedValidator : DefaultValidator
    {
        // if move is in a opposite piece's possible move
        // => set the king is checked ( the next move will not be display but validated before display) 
        public override void ValidCheck(Piece currentPiece, List<Piece> activePieces)
        {
            // if the piece to be checked is not king => continue
            if (currentPiece.IsID(GameComponents.GameObjectID.WHITE_KING) || currentPiece.IsID(GameComponents.GameObjectID.BLACK_KING))
            {
                // if pieceChecking is not null => king is checked
                List<Cell> possibleMovesWhileChecked;
                Piece pieceChecking = PieceChecking(currentPiece, activePieces);
                King king;

                if (pieceChecking != null)
                {
                    king = currentPiece as King;
                    king.IsChecked = true;
                    possibleMovesWhileChecked = GetPossibeMoveWhileChecked(king, pieceChecking);
                    RemoveMovesIfChecked(king, possibleMovesWhileChecked, activePieces);
                }
                else
                {
                    // reset the state at the next validate turn
                    (currentPiece as King).IsChecked = false;
                }
            }

            if (_nextValidator != null)
            {
                _nextValidator.ValidCheck(currentPiece, activePieces);
            }
        }

        // return a list of possible moves for a side when their king is checked
        private List<Cell> GetPossibeMoveWhileChecked(King king, Piece pieceChecking)
        {
            if (pieceChecking.UseLinearValidator && !(pieceChecking.IsID(GameObjectID.WHITE_PAWN) || pieceChecking.IsID(GameObjectID.BLACK_PAWN)))
            {
                return PieceChekingLinearly(king, pieceChecking);
            }
            else if (pieceChecking.UseDiagonalValidator)
            {
                return PieceCheckingDiagonally(king, pieceChecking);
            }
            return null;
        }

        // calculate the possible moves for a side when their king is checked
        private List<Cell> PieceChekingLinearly(King king, Piece pieceChecking)
        {
            List<Cell> possibleMoveToBlockCheck = new List<Cell>();

            if (king.X == pieceChecking.X)
            {
                if (king.Y > pieceChecking.Y)
                {
                    for (int i = 0; i < pieceChecking.MoveManager.PossibleMoveCount; i++)
                    {
                        if (pieceChecking.MoveManager.PossibleMovesClone[i].Y >= pieceChecking.Y)
                        {
                            possibleMoveToBlockCheck.Add(pieceChecking.MoveManager.PossibleMovesClone[i]);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < pieceChecking.MoveManager.PossibleMoveCount; i++)
                    {
                        if (pieceChecking.MoveManager.PossibleMovesClone[i].Y <= pieceChecking.Y)
                        {
                            possibleMoveToBlockCheck.Add(pieceChecking.MoveManager.PossibleMovesClone[i]);
                        }
                    }
                }
            }
            else if (king.Y == pieceChecking.Y)
            {
                if (king.X > pieceChecking.X)
                {
                    for (int i = 0; i < pieceChecking.MoveManager.PossibleMoveCount; i++)
                    {
                        if (pieceChecking.MoveManager.PossibleMovesClone[i].X >= pieceChecking.X)
                        {
                            possibleMoveToBlockCheck.Add(pieceChecking.MoveManager.PossibleMovesClone[i]);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < pieceChecking.MoveManager.PossibleMoveCount; i++)
                    {
                        if (pieceChecking.MoveManager.PossibleMovesClone[i].X <= pieceChecking.X)
                        {
                            possibleMoveToBlockCheck.Add(pieceChecking.MoveManager.PossibleMovesClone[i]);
                        }
                    }
                }
            }
            return possibleMoveToBlockCheck;
        }

        // calculate the possible move for a side when their king is checked
        private List<Cell> PieceCheckingDiagonally(King king, Piece pieceChecking)
        {
            List<Cell> possibleMoveToBlockCheck = new List<Cell>();

            List<int> offset = new List<int>() { king.X - pieceChecking.X, king.Y - pieceChecking.Y };

            if (offset[0] < 0)
            {
                if (offset[1] < 0)
                {
                    // king is at top left of the piece
                    foreach (Cell move in pieceChecking.MoveManager.PossibleMovesClone)
                    {
                        if (pieceChecking.X > move.X && pieceChecking.Y > move.Y)
                        {
                            possibleMoveToBlockCheck.Add(move);
                        }
                    }
                }
                else
                {
                    // king is at bottom left of the piece
                    foreach (Cell move in pieceChecking.MoveManager.PossibleMovesClone)
                    {
                        if (pieceChecking.X > move.X && pieceChecking.Y > move.Y)
                        {
                            possibleMoveToBlockCheck.Add(move);
                        }
                    }
                }
            }
            else
            {
                if (offset[1] > 0)
                {
                    // king is at bottom right of the piece
                    foreach (Cell move in pieceChecking.MoveManager.PossibleMovesClone)
                    {
                        if (pieceChecking.X > move.X && pieceChecking.Y < move.Y)
                        {
                            possibleMoveToBlockCheck.Add(move);
                        }
                    }
                }
                else
                {
                    // king is at top right of the piece
                    foreach (Cell move in pieceChecking.MoveManager.PossibleMovesClone)
                    {
                        if (pieceChecking.X > move.X && pieceChecking.Y < move.Y)
                        {
                            possibleMoveToBlockCheck.Add(move);
                        }
                    }
                }
            }

            return possibleMoveToBlockCheck;
        }

        private void RemoveMovesIfChecked(King king, List<Cell> possibleMoves, List<Piece> activePieces)
        {
            king = (king as King);
            if (king.IsChecked)
            {
                foreach (Piece piece in activePieces)
                {
                    if (piece.IsWhite == king.IsWhite)
                    {
                        for (int i = piece.MoveManager.PossibleMoveCount - 1; i >= 0; i--)
                        {
                            if (!possibleMoves.Contains(piece.MoveManager.PossibleMovesClone[i]))
                            {
                                piece.MoveManager.RemovePossibleMove(piece.MoveManager.PossibleMovesClone[i]);
                            }
                        }
                    }
                }
            }
        }

        // return null if no piece is checking the king
        private Piece PieceChecking(Piece king, List<Piece> activePieces)
        {
            for (int i = 0; i < activePieces.Count; i++)
            {
                if (activePieces[i].IsWhite != king.IsWhite)
                {
                    if (activePieces[i].MoveManager.MoveExist(new GameComponents.Cell(king.X, king.Y)))
                    {
                        return activePieces[i];
                    }
                }
            }
            return null;
        }
    }
}
