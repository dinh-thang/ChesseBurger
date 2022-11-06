using ChessBurger.Game.GameCommand;
using ChessBurger.GameComponents;
using ChessBurger.GameComponents.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChessBurger.MoveValidator
{
    public class KingCheckedValidator : DefaultValidator
    {
        // these moves are legal to make when a side is under check
        private List<Cell> _legalMoves;
        // these are moves that a piece could actually make
        // the piece when its side is checked, it can only make a move in its _possibleMoves if that move is also in _legalMoves
        private List<Cell> _possibleMoves;
        private bool _pieceCheckingIsUncapturable;
        private King _king;

        public KingCheckedValidator()
        {
            _possibleMoves = new List<Cell>();
            _legalMoves = new List<Cell>();

        }

        public override CommandStatus ValidCheck(Piece currentPiece, List<Piece> activePieces)
        {
            // if the piece to be checked is not king => continue
            if (currentPiece.IsID(GameComponents.GameObjectID.WHITE_KING) || currentPiece.IsID(GameComponents.GameObjectID.BLACK_KING))
            {
                _pieceCheckingIsUncapturable = true;
                _king = currentPiece as King;
                
                // if pieceChecking is not null => king is checked
                Piece pieceChecking = FindPieceChecking(_king, activePieces);
                
                if (pieceChecking != null)
                {
                    _king.IsChecked = true;

                    if (pieceChecking.IsID(GameObjectID.WHITE_QUEEN) || pieceChecking.IsID(GameObjectID.BLACK_QUEEN))
                    {
                        if (pieceChecking.X == _king.X || pieceChecking.Y == _king.Y)
                        {
                            _legalMoves = PossibleMovesIfKingIsCheckedLinearly(_king, pieceChecking);
                        }
                        else
                        {
                            _legalMoves = PossibleMovesIfKingIsCheckedDiagonally(_king, pieceChecking);
                        }
                    }
                    else if (pieceChecking.UseLinearValidator && !(pieceChecking.IsID(GameObjectID.WHITE_PAWN) || pieceChecking.IsID(GameObjectID.BLACK_PAWN)))
                    {
                        _legalMoves = PossibleMovesIfKingIsCheckedLinearly(_king, pieceChecking);
                    }
                    else if (pieceChecking.UseDiagonalValidator)
                    {
                        _legalMoves = PossibleMovesIfKingIsCheckedDiagonally(_king, pieceChecking);
                    }
                    else
                    {
                        // validate possible move if the piece checking is not blockable (e.x: bishop, queen, rook)
                        _legalMoves = new List<Cell>();
                    }
                    foreach (Cell cell in _legalMoves)
                    {
                        Console.WriteLine(cell.X + ", " + cell.Y);
                    }
                    
                    RemoveIllegalKingMoves(_king, pieceChecking, activePieces);
                    RemoveIllegalMoves(_king, pieceChecking, activePieces);
                    
                    if (_possibleMoves.Count == 0)
                    {
                        if (_pieceCheckingIsUncapturable is true)
                        {
                            if (_king.IsWhite)
                            {
                                return CommandStatus.WHITE_CHECKMATED;
                            }
                                
                            {
                                return CommandStatus.BLACK_CHECKMATED;
                            }
                        }
                    }
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
            return CommandStatus.SUCCESSFUL;
        }

        // remove possible move of a king when it is checked
        private void RemoveIllegalKingMoves(King king, Piece pieceChecking, List<Piece> activePieces)
        {
            Cell pieceCheckingCell = new Cell(pieceChecking.X, pieceChecking.Y);
            List<Cell> opponentMoves = GenerateAllOpponentMove(king, activePieces);

            if (king.MoveManager.MoveExist(pieceCheckingCell))
            {
                _pieceCheckingIsUncapturable = false;
            }
            // only allow move that capture the piece checking or escaping 
            foreach (Cell move in opponentMoves)
            {
                if (move.X == pieceChecking.X && move.Y == pieceChecking.Y)
                {
                    continue;
                }
                if (king.MoveManager.MoveExist(move))
                {
                    king.MoveManager.RemovePossibleMove(move);
                }
            }
        }

        // calculate the possible moves for a side when their king is checked by a rook or queen (linearly here mean horizontally and vertically)
        private List<Cell> PossibleMovesIfKingIsCheckedLinearly(King king, Piece pieceChecking)
        {
            List<Cell> legalMoves = new List<Cell>();

            if (king.X == pieceChecking.X)
            {
                // king is under the piece checking
                if (king.Y > pieceChecking.Y)
                {
                    for (int i = 0; i < pieceChecking.MoveManager.PossibleMoveCount; i++)
                    {
                        if (pieceChecking.MoveManager.PossibleMovesClone[i].X != pieceChecking.X)
                        {
                            continue;
                        }
                        if (pieceChecking.MoveManager.PossibleMovesClone[i].Y >= pieceChecking.Y)
                        {
                            legalMoves.Add(pieceChecking.MoveManager.PossibleMovesClone[i]);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < pieceChecking.MoveManager.PossibleMoveCount; i++)
                    {
                        if (pieceChecking.MoveManager.PossibleMovesClone[i].X != pieceChecking.X)
                        {
                            continue;
                        }
                        if (pieceChecking.MoveManager.PossibleMovesClone[i].Y <= pieceChecking.Y)
                        {
                            legalMoves.Add(pieceChecking.MoveManager.PossibleMovesClone[i]);
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
                        if (pieceChecking.MoveManager.PossibleMovesClone[i].Y != pieceChecking.Y)
                        {
                            continue;
                        }
                        if (pieceChecking.MoveManager.PossibleMovesClone[i].X >= pieceChecking.X)
                        {
                            legalMoves.Add(pieceChecking.MoveManager.PossibleMovesClone[i]);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < pieceChecking.MoveManager.PossibleMoveCount; i++)
                    {
                        if (pieceChecking.MoveManager.PossibleMovesClone[i].Y != pieceChecking.Y)
                        {
                            continue;
                        }
                        if (pieceChecking.MoveManager.PossibleMovesClone[i].X <= pieceChecking.X)
                        {
                            legalMoves.Add(pieceChecking.MoveManager.PossibleMovesClone[i]);
                        }
                    }
                }
            }
            return legalMoves;
        }

        // calculate the possible move for a side when their king is checked by a bishop or queen (diagonally)
        private List<Cell> PossibleMovesIfKingIsCheckedDiagonally(King king, Piece pieceChecking)
        {
            List<Cell> legalMoves = new List<Cell>();

            List<int> offset = new List<int>() { king.X - pieceChecking.X, king.Y - pieceChecking.Y };

            if (offset[0] < 0)
            {
                if (offset[1] < 0)
                {
                    // king is at top left of the piece
                    foreach (Cell move in pieceChecking.MoveManager.PossibleMovesClone)
                    {
                        if (pieceChecking.X == move.X && pieceChecking.Y == move.Y)
                        {
                            continue;
                        }
                        if (pieceChecking.X > move.X && pieceChecking.Y > move.Y)
                        {
                            legalMoves.Add(move);
                        }
                    }
                }
                else
                {
                    // king is at bottom left of the piece
                    foreach (Cell move in pieceChecking.MoveManager.PossibleMovesClone)
                    {
                        if (pieceChecking.X == move.X && pieceChecking.Y == move.Y)
                        {
                            continue;
                        }
                        if (pieceChecking.X > move.X && pieceChecking.Y < move.Y)
                        {
                            legalMoves.Add(move);
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
                        if (pieceChecking.X == move.X && pieceChecking.Y == move.Y)
                        {
                            continue;
                        }

                        if (pieceChecking.X > move.X && pieceChecking.Y < move.Y)
                        {
                            legalMoves.Add(move);
                        }
                    }
                }
                else
                {
                    // king is at top right of the piece
                    foreach (Cell move in pieceChecking.MoveManager.PossibleMovesClone)
                    {
                        if (pieceChecking.X == move.X && pieceChecking.Y == move.Y)
                        {
                            continue;
                        }

                        if (pieceChecking.X > move.X && pieceChecking.Y < move.Y)
                        {
                            legalMoves.Add(move);
                        }
                    }
                }
            }

            return legalMoves;
        }

        // if a piece move is not in possibleMovesWhileChecked list then remove it
        private void RemoveIllegalMoves(King king, Piece pieceChecking, List<Piece> activePieces)
        {
            if (!king.IsChecked)
            {
                return;
            }

            foreach (Piece piece in activePieces)
            {
                if (piece.UseCastleValidator)
                {
                    continue;
                }

                if (piece.IsWhite == king.IsWhite)
                {
                    if (piece.MoveManager.MoveExist(new Cell(pieceChecking.X, pieceChecking.Y)))
                    {
                        _pieceCheckingIsUncapturable = false;
                    }
                    for (int i = piece.MoveManager.PossibleMoveCount - 1; i >= 0; i--)
                    {
                        if (piece.MoveManager.PossibleMovesClone[i].X == pieceChecking.X && piece.MoveManager.PossibleMovesClone[i].Y == pieceChecking.Y)
                        {
                            continue;
                        }
                        if (!_legalMoves.Contains(piece.MoveManager.PossibleMovesClone[i]))
                        {
                            piece.MoveManager.RemovePossibleMove(piece.MoveManager.PossibleMovesClone[i]);
                        }
                        else
                        {
                            _possibleMoves.Add(piece.MoveManager.PossibleMovesClone[i]);
                        }
                    }
                }
            }
        }

        private List<Cell> GenerateAllOpponentMove(King king, List<Piece> activePieces)
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

        // return null if no piece is checking the king
        private Piece FindPieceChecking(King king, List<Piece> activePieces)
        {
            for (int i = 0; i < activePieces.Count; i++)
            {
                if (activePieces[i].IsWhite != king.IsWhite)
                {
                    if (activePieces[i].MoveManager.MoveExist(new Cell(king.X, king.Y)))
                    {
                        Console.WriteLine(activePieces[i].GetType());
                        return activePieces[i];
                    }
                }
            }
            return null;
        }

    }
}
