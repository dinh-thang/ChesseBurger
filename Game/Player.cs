using System.Collections.Generic;
using ChessBurger.MoveExplorer;
using System;
using ChessBurger.GameComponents;
using ChessBurger.GameComponents.Pieces;
using ChessBurger.Game.GameCommand;

namespace ChessBurger.Game
{
    public class Player
    {
        private List<Piece> _activePieces;
        private Piece _selectedPiece;
        private bool _isWhite;
        private PieceFactory _pieceCreator;
        private Command _command;

        public Player(bool isWhite)
        {
            _activePieces = new List<Piece>();
            _isWhite = isWhite;

            // set active same color pieces for a player
            _command = new SetPiecesCommand(_isWhite, _activePieces);
            _command.Execute();
        }

        // set the command for the player
        public void SetCommand(Command command)
        {
            _command = command;
        }

        // return click state based on the selected piece
        public ClickState UpdateClickStateOfFirstClick(int selectedX, int selectedY)
        {   
            // set the selected piece base on the input coordinate
            _selectedPiece = GetSelectedPiece(selectedX, selectedY);

            if (_selectedPiece != null)
            {
                Console.WriteLine(_selectedPiece.PossibleMoves.Count);

                foreach (Cell move in _selectedPiece.PossibleMoves)
                {
                    System.Diagnostics.Debug.WriteLine(move.X + " " + move.Y);
                }
            }

            // if the selected piece is not the null piece => return piece_selected, else remain the same (destination_selected is the default)
            if (_selectedPiece != null)
            {
                return ClickState.FIRST_CLICK;
            }
            
            return ClickState.SECOND_CLICK;
        }

        // remove the piece
        public void RemovePiece(int x, int y)
        {
            _activePieces.Remove(GetSelectedPiece(x, y));
        }

        // return list of piece available of A player
        public List<Piece> GetActivePieces
        {
            get { return _activePieces; }
        }

        // return true if there is a piece in the given position
        public bool IsOccupied(int x, int y)
        {
            foreach (Piece piece in _activePieces)
            {
                if (piece.X == x && piece.Y == y)
                {
                    return true;
                }
            }
            return false;
        }

        // make the move. Return true if a piece other than null moved
        public bool Move(int desX, int desY)
        {
            foreach (Cell position in _selectedPiece.PossibleMoves)
            {
                if (position.X == desX && position.Y == desY)
                {
                    _selectedPiece.X = desX;
                    _selectedPiece.Y = desY;

                    if (_selectedPiece.ID == GameObjectID.WHITE_PAWN || _selectedPiece.ID == GameObjectID.BLACK_PAWN)
                    {
                        Pawn pawn = (_selectedPiece as Pawn);
                        pawn.IsFirstMove = false;
                    }
                    return true;
                }
            }
            return false;
        }

        // return a piece at the given position. If there is no piece there, nullPiece will be returned
        public Piece GetSelectedPiece(int selectedX, int selectedY)
        {
            foreach (Piece piece in _activePieces)
            {
                if (piece.X == selectedX && piece.Y == selectedY)
                {
                    return piece;
                }
            }
            return null;
        }

        // set pieces for black and white
        //private void SetActivePieces()
        //{
        //    if (_isWhite)
        //    {
        //        // add rooks
        //        _pieceCreator = new RookCreator();
        //        _activePieces.Add(_pieceCreator.CreatePiece(7, 0, _isWhite, GameObjectID.WHITE_ROOK));
        //        _activePieces.Add(_pieceCreator.CreatePiece(0, 0, _isWhite, GameObjectID.WHITE_ROOK));
        //        // add knights
        //        _pieceCreator = new KnightCreator(); 
        //        _activePieces.Add(_pieceCreator.CreatePiece(1, 0, _isWhite, GameObjectID.WHITE_KNIGHT));
        //        _activePieces.Add(_pieceCreator.CreatePiece(6, 0, _isWhite, GameObjectID.WHITE_KNIGHT));
        //        // add bishops
        //        _pieceCreator = new BishopCreator(); 
        //        _activePieces.Add(_pieceCreator.CreatePiece(5, 0, _isWhite, GameObjectID.WHITE_BISHOP));
        //        _activePieces.Add(_pieceCreator.CreatePiece(2, 0, _isWhite, GameObjectID.WHITE_BISHOP));
        //        // add queen
        //        _pieceCreator = new QueenCreator();
        //        _activePieces.Add(_pieceCreator.CreatePiece(3, 0, _isWhite, GameObjectID.WHITE_QUEEN));
        //        // add king
        //        _pieceCreator = new KingCreator();
        //        _activePieces.Add(_pieceCreator.CreatePiece(4, 0, _isWhite, GameObjectID.WHITE_KING));
        //        for (int i = 0; i < 8; i++)
        //        {
        //            // add white pawns
        //            _pieceCreator = new PawnCreator();
        //            _activePieces.Add(_pieceCreator.CreatePiece(i, 1, _isWhite, GameObjectID.WHITE_PAWN));
        //        }
        //    }
        //    else
        //    {
        //        for (int i = 0; i < 8; i++)
        //        {
        //            // add black pawns
        //            _pieceCreator = new PawnCreator();
        //            _activePieces.Add(_pieceCreator.CreatePiece(i, 6, _isWhite, GameObjectID.BLACK_PAWN));
        //        }
        //        // add rooks
        //        _pieceCreator = new RookCreator();
        //        _activePieces.Add(_pieceCreator.CreatePiece(0, 7, _isWhite, GameObjectID.BLACK_ROOK));
        //        _activePieces.Add(_pieceCreator.CreatePiece(7, 7, _isWhite, GameObjectID.BLACK_ROOK));
        //        // add knights
        //        _pieceCreator = new KnightCreator();
        //        _activePieces.Add(_pieceCreator.CreatePiece(6, 7, _isWhite, GameObjectID.BLACK_KNIGHT));
        //        _activePieces.Add(_pieceCreator.CreatePiece(1, 7, _isWhite, GameObjectID.BLACK_KNIGHT));
        //        // add bishop
        //        _pieceCreator = new BishopCreator();
        //        _activePieces.Add(_pieceCreator.CreatePiece(5, 7, _isWhite, GameObjectID.BLACK_BISHOP));
        //        _activePieces.Add(_pieceCreator.CreatePiece(2, 7, _isWhite, GameObjectID.BLACK_BISHOP));
        //        //// add queen
        //        _pieceCreator = new QueenCreator();
        //        _activePieces.Add(_pieceCreator.CreatePiece(3, 7, _isWhite, GameObjectID.BLACK_QUEEN));
        //        //// add king
        //        _pieceCreator = new KingCreator();
        //        _activePieces.Add(_pieceCreator.CreatePiece(4, 7, _isWhite, GameObjectID.BLACK_KING));
        //    }
        //}

        // clear the current move list of every pieces and re-generate their move list
        public void UpdatePiecesPosition()
        {
            for (int i = 0; i < _activePieces.Count; i++)
            {
                _activePieces[i].ResetPossibleMove();
                _activePieces[i].GenerateMoves();
            }
        }
    }
}
