using ChessBurger.GameComponents;
using ChessBurger.GameComponents.Pieces;
using System;
using System.Collections.Generic;

namespace ChessBurger.Game.GameCommand
{
    public class MakeMoveCommand : ICommand
    {
        private int _curX;
        private int _curY;
        private int _desX;
        private int _desY;
        private List<Piece> _activePieces;

        public MakeMoveCommand(int curX, int curY, int desX, int desY, List<Piece> activePieces)
        {
            _curX = curX;
            _curY = curY;
            _desX = desX;
            _desY = desY;
            _activePieces = activePieces;
        }

        // return the piece at curX and curY. Return null if not found
        private Piece GetSelectedPiece(int x, int y)
        {
            foreach (Piece piece in _activePieces)
            {
                if (piece.X == x && piece.Y == y)
                {
                    return piece;
                }
            }
            return null;
        }

        // remove the piece
        private void Capture(Piece piece)
        {
            _activePieces.Remove(piece);
        }

        // get the current piece, check if the destination is in possible moves list
        // if yes => modify the piece's location.
        // if the piece moved is pawn => set the pawn's IsFirstMove to false
        // return the command status
        public CommandStatus Execute()
        {
            Piece currentPiece = GetSelectedPiece(_curX, _curY);
            Piece capturePiece = GetSelectedPiece(_desX, _desY);

            if (currentPiece.MoveManager.MoveExist(new Cell(_desX, _desY)))
            {
                Cell initValue = new Cell(currentPiece.X, currentPiece.Y);

                currentPiece.X = _desX;
                currentPiece.Y = _desY;
                currentPiece.Selected = false;

                if (currentPiece.IsID(GameObjectID.WHITE_PAWN) || currentPiece.IsID(GameObjectID.BLACK_PAWN))
                {
                    Pawn pawn = (currentPiece as Pawn);
                    pawn.FirstMove = false;
                    
                    // if the pawn move 1 move foward, it cant be en passant by other
                    if (initValue.Y == currentPiece.Y + 1 || initValue.Y == currentPiece.Y - 1)
                    {
                        pawn.EnPassant = false;
                    }
                }
                else if (currentPiece.IsID(GameObjectID.BLACK_KING) || currentPiece.IsID(GameObjectID.WHITE_KING))
                {
                    King king = (currentPiece as King);

                    Castle(king, initValue);

                    king.FirstMove = false;
                }
                else if (currentPiece.IsID(GameObjectID.BLACK_ROOK) || currentPiece.IsID(GameObjectID.WHITE_ROOK))
                {
                    Rook rook = (currentPiece as Rook);
                    rook.FirstMove = false;
                }

                if (capturePiece != null && capturePiece.IsWhite != currentPiece.IsWhite)
                {
                    Capture(capturePiece);
                }
                UpdatePieces();

                return CommandStatus.SUCCESSFUL;
            }
            currentPiece.Selected = false; 
            return CommandStatus.NOT_SUCCESSFUL;
        }

        private void Castle(King king, Cell initValue)
        {
            // check if the destination is a castle move
            if (initValue.Y == king.Y)
            {
                // if the move is castle and not the king's normal move
                if (initValue.X + 1 < king.X || initValue.X - 1 > king.X)
                {
                    // search for rook and set rook new position
                    for (int i = 0; i < _activePieces.Count; i++)
                    {
                        if (_activePieces[i].IsID(GameObjectID.WHITE_ROOK) || _activePieces[i].IsID(GameObjectID.BLACK_ROOK))
                        { 
                            if (_activePieces[i].IsWhite == king.IsWhite && _activePieces[i].Y == king.Y)
                            {
                                if (initValue.X + 1 < king.X)
                                {
                                    if (_activePieces[i].X > king.X)
                                    {
                                        _activePieces[i].X = king.X - 1;
                                        return;
                                    }
                                }
                                else if (initValue.X - 1 > king.X)
                                {
                                    if (_activePieces[i].X < king.X)
                                    {
                                        _activePieces[i].X = king.X + 1;
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        // reset and re-generate moves
        private void UpdatePieces()
        {
            foreach (Piece piece in _activePieces)
            {
                piece.ResetPossibleMove(); 
            }
        }
    }
}
