using ChessBurger.GameComponents;
using ChessBurger.GameComponents.Pieces;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace ChessBurger.Game.GameCommand
{
    public class ChoosePieceCommand : ICommand
    {
        private int _curX;
        private int _curY;
        private bool _isWhiteTurn;
        private List<Piece> _activePieces;

        public ChoosePieceCommand(int curX, int curY, bool isWhiteTurn, List<Piece> activePieces)
        {
            _curX = curX;
            _curY = curY;
            _isWhiteTurn = isWhiteTurn;
            _activePieces = activePieces;
        }

        private Piece GetSelectedPiece()
        {
            foreach (Piece piece in _activePieces)
            {
                if (piece.X == _curX && piece.Y == _curY)
                {
                    return piece;
                }
            }
            return null;
        }

        public CommandStatus Execute()
        {
            // set the selected piece base on the input coordinate
            Piece currentPiece = GetSelectedPiece();

            // if the selected piece is not the null piece => return piece_selected, else remain the same (destination_selected is the default)
            if (currentPiece != null && currentPiece.IsWhite == _isWhiteTurn)
            {
                return CommandStatus.SUCCESSFUL;
            }

            return CommandStatus.NOT_SUCCESSFUL;
        }
    }
}
