using ChessBurger.GameComponents;
using ChessBurger.GameComponents.Pieces;
using System.Collections.Generic;

namespace ChessBurger.Game.GameCommand
{
    public class MakeMoveCommand : Command
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

            foreach (Cell position in currentPiece.PossibleMoves)
            {
                if (position.X == _desX && position.Y == _desY)
                {
                    currentPiece.X = _desX;
                    currentPiece.Y = _desY;

                    if (currentPiece.IsID(GameObjectID.WHITE_PAWN) || currentPiece.IsID(GameObjectID.BLACK_PAWN))
                    {
                        Pawn pawn = (currentPiece as Pawn);
                        pawn.IsFirstMove = false;
                    }

                    if (capturePiece != null && capturePiece.IsWhite != currentPiece.IsWhite)
                    {
                        Capture(capturePiece);
                    }
                    UpdatePieces();

                    return CommandStatus.SUCCESSFUL;
                }
            }
            return CommandStatus.NOT_SUCCESSFUL;
        }

        // reset and re-generate moves
        public void UpdatePieces()
        {
            foreach (Piece piece in _activePieces)
            {
                piece.ResetPossibleMove();
                piece.GenerateMoves();
            }
        }
    }
}
