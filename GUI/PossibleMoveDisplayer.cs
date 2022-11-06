using ChessBurger.GameComponents.Pieces;
using ChessBurger.GameComponents;
using System.Collections.Generic;
using ChessBurger.lib;
using ChessBurger.Ultilities;
using System;

namespace ChessBurger.GUI
{
    public class PossibleMoveDisplayer : IDisplayer
    {
        private List<Piece> _activePieces;
        private GameObject _selectedSquare;
        private GameObjectID _gameObjectID = GameObjectID.SELECTED_SQUARE;
        
        public PossibleMoveDisplayer(List<Piece> activePieces)
        {
            _activePieces = activePieces;
            _selectedSquare = GameObjectFactory.CreateObject(_gameObjectID);
        }

        public void Display()
        {
            Piece selectedPiece = GetSelectedPiece();

            if (selectedPiece != null)
            {
                MoveDisplayer(selectedPiece);
            }
        }

        private Piece GetSelectedPiece()
        {
            foreach (Piece piece in _activePieces)
            {
                if (piece.Selected)
                {
                    return piece;
                }
            }
            return null;
        }

        private void MoveDisplayer(Piece piece)
        {
            foreach (Cell move in piece.MoveManager.PossibleMovesClone)
            {
                int x = Extras.SelectedSquareXPosition(move.X);
                int y = Extras.SelectedSquareYPosition(move.Y);
                SplashKit.SpriteBringLayerToFront(_selectedSquare.GetSprite, 1);
                SplashKit.DrawSprite(_selectedSquare.GetSprite, x, y);
            }
        }
    }
}
