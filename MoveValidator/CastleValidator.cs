using ChessBurger.GameComponents;
using ChessBurger.GameComponents.Pieces;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;

namespace ChessBurger.MoveValidator
{
    public class CastleValidator : DefaultValidator
    {
        // first check if king and rook is moved?
        // if yes => remove king's castle moves
        // else => check if there is any pieces between the king and the rook?
        // if not => remove castle moves
        // else => check if there any opposite color possible move in the range between the king and rook?
        // if yes => remove castle moves
        // else => make the move
        // if the desX = curX + 2 => king's x += 2 and rook's x = king's x - 1
        // if the desX = curX - 2 => king's x -= 2 and rook's x = king's x + 1

        public override void ValidCheck(Piece currentPiece, List<Piece> activePieces)
        {
            // first move is checked
            if (currentPiece.IsID(GameComponents.GameObjectID.WHITE_KING) || currentPiece.IsID(GameComponents.GameObjectID.BLACK_KING))
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
                            //RemoveCastleMoveBlockedByAOpponentMove(king, activePieces);
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

        // remove castle move blocked by opponent's move
        private void RemoveCastleMoveBlockedByAOpponentMove(Piece king, List<Piece> activePieces)
        {
            foreach (Cell move in GenerateAllOpponentMove(king, activePieces))
            {
                if (move.Y == king.Y)
                {
                    if (move.X < king.X)
                    {
                        Console.WriteLine(king.IsWhite + "1");
                        Console.WriteLine(move.X);
                        RemoveCastleMove(king, true, false);
                    }
                    else if (move.X > king.X)
                    {
                        Console.WriteLine(king.IsWhite + "2");
                        Console.WriteLine(move.X);
                        RemoveCastleMove(king, false, true);
                    }
                    else
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
                if (activePieces[i].IsWhite != king.IsWhite)
                {
                    opponentPossibleMove = opponentPossibleMove.Concat(activePieces[i].PossibleMoves).ToList();
                }
            }
            return opponentPossibleMove;
        }

        // remove the castle move base on the input
        private void RemoveCastleMove(Piece king, bool removeQueenSide, bool removeKingSide)
        {
            for (int i = king.PossibleMoves.Count - 1; i >= 0; i--)
            {
                if (removeKingSide && removeQueenSide)
                {
                    if (king.PossibleMoves[i].X > king.X + 1 || king.PossibleMoves[i].X < king.X - 1)
                    {
                        king.PossibleMoves.Remove(king.PossibleMoves[i]);
                    }
                }
                else if (removeKingSide)
                {
                    if (king.PossibleMoves[i].X > king.X + 1)
                    {
                        king.PossibleMoves.Remove(king.PossibleMoves[i]);
                    }
                }
                else if (removeQueenSide)
                {
                    if (king.PossibleMoves[i].X < king.X - 1)
                    {
                        king.PossibleMoves.Remove(king.PossibleMoves[i]);
                    }
                }
            }
        }
    }
}
