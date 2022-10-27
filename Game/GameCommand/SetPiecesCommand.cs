using ChessBurger.GameComponents;
using System.Collections.Generic;
using ChessBurger.GameComponents.Pieces;

namespace ChessBurger.Game.GameCommand
{
    public class SetPiecesCommand : Command
    {
        private bool _isWhite;
        private List<Piece> _activePieces;
        private PieceFactory _pieceCreator;

        public SetPiecesCommand(bool playerIsWhite, List<Piece> activePieces)
        {
            _activePieces = activePieces;
            _isWhite = playerIsWhite;
            _pieceCreator = new PieceFactory();
        }

        // set up the pieces and add them to the active pieces list
        public CommandStatus Execute()
        {
            if (_isWhite)
            {
                // add rooks
                _activePieces.Add(_pieceCreator.CreatePiece(7, 0, _isWhite, GameObjectID.WHITE_ROOK));
                _activePieces.Add(_pieceCreator.CreatePiece(0, 0, _isWhite, GameObjectID.WHITE_ROOK));
                // add knights
                _activePieces.Add(_pieceCreator.CreatePiece(1, 0, _isWhite, GameObjectID.WHITE_KNIGHT));
                _activePieces.Add(_pieceCreator.CreatePiece(6, 0, _isWhite, GameObjectID.WHITE_KNIGHT));
                // add bishops
                _activePieces.Add(_pieceCreator.CreatePiece(5, 0, _isWhite, GameObjectID.WHITE_BISHOP));
                _activePieces.Add(_pieceCreator.CreatePiece(2, 0, _isWhite, GameObjectID.WHITE_BISHOP));
                // add queen
                _activePieces.Add(_pieceCreator.CreatePiece(3, 0, _isWhite, GameObjectID.WHITE_QUEEN));
                // add king
                _activePieces.Add(_pieceCreator.CreatePiece(4, 0, _isWhite, GameObjectID.WHITE_KING));
                // add white pawns
                for (int i = 0; i < 8; i++)
                {
                    _activePieces.Add(_pieceCreator.CreatePiece(i, 1, _isWhite, GameObjectID.WHITE_PAWN));
                }
            }
            else
            {
                for (int i = 0; i < 8; i++)
                {
                    // add black pawns
                    _activePieces.Add(_pieceCreator.CreatePiece(i, 6, _isWhite, GameObjectID.BLACK_PAWN));
                }
                // add rooks
                _activePieces.Add(_pieceCreator.CreatePiece(0, 7, _isWhite, GameObjectID.BLACK_ROOK));
                _activePieces.Add(_pieceCreator.CreatePiece(7, 7, _isWhite, GameObjectID.BLACK_ROOK));
                // add knights
                _activePieces.Add(_pieceCreator.CreatePiece(6, 7, _isWhite, GameObjectID.BLACK_KNIGHT));
                _activePieces.Add(_pieceCreator.CreatePiece(1, 7, _isWhite, GameObjectID.BLACK_KNIGHT));
                // add bishop
                _activePieces.Add(_pieceCreator.CreatePiece(5, 7, _isWhite, GameObjectID.BLACK_BISHOP));
                _activePieces.Add(_pieceCreator.CreatePiece(2, 7, _isWhite, GameObjectID.BLACK_BISHOP));
                //// add queen
                _activePieces.Add(_pieceCreator.CreatePiece(3, 7, _isWhite, GameObjectID.BLACK_QUEEN));
                //// add king
                _activePieces.Add(_pieceCreator.CreatePiece(4, 7, _isWhite, GameObjectID.BLACK_KING));
            }
            return CommandStatus.SUCCESSFUL;
        }
    }
}
