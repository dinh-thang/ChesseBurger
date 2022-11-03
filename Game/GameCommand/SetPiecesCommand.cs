using ChessBurger.GameComponents;
using System.Collections.Generic;
using ChessBurger.GameComponents.Pieces;

namespace ChessBurger.Game.GameCommand
{
    public class SetPiecesCommand : ICommand
    {
        private bool _isWhite;
        private List<Piece> _activePieces;

        public SetPiecesCommand(bool playerIsWhite, List<Piece> activePieces)
        {
            _activePieces = activePieces;
            _isWhite = playerIsWhite;
        }

        // set up the pieces and add them to the active pieces list
        public CommandStatus Execute()
        {
            if (_isWhite)
            {
                // add king
                _activePieces.Add(PieceFactory.CreatePiece(4, 0, _isWhite, GameObjectID.WHITE_KING));
                // add rooks
                _activePieces.Add(PieceFactory.CreatePiece(7, 0, _isWhite, GameObjectID.WHITE_ROOK));
                _activePieces.Add(PieceFactory.CreatePiece(0, 0, _isWhite, GameObjectID.WHITE_ROOK));
                // add knights
                _activePieces.Add(PieceFactory.CreatePiece(1, 0, _isWhite, GameObjectID.WHITE_KNIGHT));
                _activePieces.Add(PieceFactory.CreatePiece(6, 0, _isWhite, GameObjectID.WHITE_KNIGHT));
                // add bishops
                _activePieces.Add(PieceFactory.CreatePiece(5, 0, _isWhite, GameObjectID.WHITE_BISHOP));
                _activePieces.Add(PieceFactory.CreatePiece(2, 0, _isWhite, GameObjectID.WHITE_BISHOP));
                // add queen
                _activePieces.Add(PieceFactory.CreatePiece(3, 0, _isWhite, GameObjectID.WHITE_QUEEN));
                // add white pawns
                for (int i = 0; i < 8; i++)
                {
                    _activePieces.Add(PieceFactory.CreatePiece(i, 1, _isWhite, GameObjectID.WHITE_PAWN));
                }
            }
            else
            {
                for (int i = 0; i < 8; i++)
                {
                    // add black pawns
                    _activePieces.Add(PieceFactory.CreatePiece(i, 6, _isWhite, GameObjectID.BLACK_PAWN));
                }
                // add knights
                _activePieces.Add(PieceFactory.CreatePiece(6, 7, _isWhite, GameObjectID.BLACK_KNIGHT));
                _activePieces.Add(PieceFactory.CreatePiece(1, 7, _isWhite, GameObjectID.BLACK_KNIGHT));
                // add bishop
                _activePieces.Add(PieceFactory.CreatePiece(5, 7, _isWhite, GameObjectID.BLACK_BISHOP));
                _activePieces.Add(PieceFactory.CreatePiece(2, 7, _isWhite, GameObjectID.BLACK_BISHOP));
                //// add queen
                _activePieces.Add(PieceFactory.CreatePiece(3, 7, _isWhite, GameObjectID.BLACK_QUEEN));
                //// add king
                _activePieces.Add(PieceFactory.CreatePiece(4, 7, _isWhite, GameObjectID.BLACK_KING));
                // add rooks
                _activePieces.Add(PieceFactory.CreatePiece(0, 7, _isWhite, GameObjectID.BLACK_ROOK));
                _activePieces.Add(PieceFactory.CreatePiece(7, 7, _isWhite, GameObjectID.BLACK_ROOK));
            }
            return CommandStatus.SUCCESSFUL;
        }
    }
}
