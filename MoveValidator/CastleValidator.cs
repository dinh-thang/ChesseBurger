using ChessBurger.GameComponents;
using ChessBurger.GameComponents.Pieces;
using System.Linq;
using System;
using System.Collections.Generic;

namespace ChessBurger.MoveValidator
{
    public class CastleValidator : DefaultValidator
    {
        public override void ValidCheck(Piece currentPiece, List<Piece> activePieces)
        {
            // first move is checked
            if (currentPiece.UseCastleValidator)
            {
                List<Rook> activeRooks = new List<Rook>();
                King king = currentPiece as King;

                if (king.FirstMove)
                {
                    // check if the rook moved
                    foreach (Piece piece in activePieces)
                    {
                        if (piece.IsWhite == king.IsWhite && (piece.IsID(GameComponents.GameObjectID.WHITE_ROOK) || piece.IsID(GameComponents.GameObjectID.BLACK_ROOK)))
                        {
                            activeRooks.Add((Rook)piece);
                        }
                    }
                    foreach (Rook rook in activeRooks)
                    {
                        if (!rook.FirstMove)
                        {
                            // check which rook moved => remove that rook
                            if (rook.X < king.X)
                            {
                                RemoveCastleMove(king, true, false);
                            }
                            if (rook.X > king.X)
                            {
                                RemoveCastleMove(king, false, true);
                            }
                        }
                        else
                        {
                            // if the rook havent move => check the range from king to rook for blocking piece
                            RemoveCastleMoveBlockedByAPiece(king, activePieces);
                            RemoveCastleMoveBlockedByAOpponentMove(king, activePieces);
                        }
                    }
                }
                else
                {
                    // remove both castle moves
                    RemoveCastleMove(king, true, true);
                }
            }

            if (_nextValidator != null)
            {
                _nextValidator.ValidCheck(currentPiece, activePieces);
            }
        }

        // check for piece blocking in the castle range
        private void RemoveCastleMoveBlockedByAPiece(Piece king, List<Piece> activePieces)
        {
            for (int i = 0; i < activePieces.Count; i++)
            {
                if (!(activePieces[i].IsID(GameObjectID.WHITE_ROOK) || activePieces[i].IsID(GameObjectID.BLACK_ROOK)))
                {

                    if (activePieces[i].Y == king.Y && activePieces[i].X < king.X)
                    {
                        RemoveCastleMove(king, true, false);
                    }
                    else if (activePieces[i].Y == king.Y && activePieces[i].X > king.X)
                    {
                        RemoveCastleMove(king, false, true);
                    }
                }
            }
        }

        // remove castle move blocked by opponent's movev
        // PROBLEM: linear move is blocking castle move
        private void RemoveCastleMoveBlockedByAOpponentMove(Piece king, List<Piece> activePieces)
        {
            List<Cell> opponentPossibleMove = GenerateAllOpponentMove(king, activePieces);

            foreach (Cell move in opponentPossibleMove)
            {
                if (move.Y == king.Y)
                {
                    Console.WriteLine(king.IsWhite);
                    Console.WriteLine(move.X + ", " + move.Y);

                    if (move.X < king.X)
                    {
                        RemoveCastleMove(king, true, false);
                    }
                    else if (move.X > king.X)
                    {
                        RemoveCastleMove(king, false, true);
                    }
                    else if ((king as King).IsChecked)
                    {
                        RemoveCastleMove(king, true, true);
                    }
                }
            }
        }

        // get all possible moves of the opponent
        private List<Cell> GenerateAllOpponentMove(Piece king, List<Piece> activePieces)
        {
            List<Cell> opponentPossibleMove = new List<Cell>();

            for (int i = 0; i < activePieces.Count; i++)
            {
                if (king.IsWhite != activePieces[i].IsWhite)
                {
                    opponentPossibleMove = opponentPossibleMove.Concat(activePieces[i].MoveManager.PossibleMovesClone).ToList();    
                }
            }
            return opponentPossibleMove;
        }

        // remove the castle move base on the input
        private void RemoveCastleMove(Piece king, bool removeQueenSide, bool removeKingSide)
        {
            for (int i = 0; i < king.MoveManager.PossibleMoveCount - 1; i++)
            {
                if (removeKingSide && removeQueenSide)
                {
                    if (king.MoveManager.PossibleMovesClone[i].X > king.X + 1 || king.MoveManager.PossibleMovesClone[i].X < king.X - 1)
                    {
                        king.MoveManager.RemovePossibleMove(king.MoveManager.PossibleMovesClone[i]);
                    }
                }
                else if (removeKingSide)
                {
                    if (king.MoveManager.PossibleMovesClone[i].X > king.X + 1)
                    {
                        king.MoveManager.RemovePossibleMove(king.MoveManager.PossibleMovesClone[i]);
                    }
                }
                else if (removeQueenSide)
                {
                    if (king.MoveManager.PossibleMovesClone[i].X < king.X - 1)
                    {
                        king.MoveManager.RemovePossibleMove(king.MoveManager.PossibleMovesClone[i]);
                    }
                }
            }
        }
    }
}
