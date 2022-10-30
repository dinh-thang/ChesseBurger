using System.Collections.Generic;
using ChessBurger.lib;

namespace ChessBurger.GameComponents.Pieces
{
    public abstract class Piece : GameObject
    {
        private List<Cell> _possibleMoves;
        private bool _isWhite;

        public Piece(int x, int y, bool isWhite, GameObjectID objectID) : base(x, y, objectID)
        {
            _possibleMoves = new List<Cell>();
            _isWhite = isWhite;
        }

        // reset move list
        public void ResetPossibleMove()
        {
            _possibleMoves.Clear();
        }

        // return piece's sprite using its bitmap
        public override Sprite GenerateSprite()
        {
            Sprite sprite = SplashKit.CreateSprite(GetBitmap);
            SplashKit.SpriteSetScale(sprite, 1 / 3f);
            return sprite;
        }

        // return list of pseudo legal moves
        public List<Cell> PossibleMoves
        {
            get { return _possibleMoves; }
            set { _possibleMoves = value; }
        }

        // readonly get piece's color
        public bool IsWhite
        {
            get { return _isWhite; }
        }

        // return a list of type cell, representing the pseudo legal moves
        public abstract List<Cell> GenerateMoves();
    }
}
