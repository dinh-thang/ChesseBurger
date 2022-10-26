using System.Collections.Generic;
using ChessBurger.lib;
using ChessBurger.MoveExplorer;
using ChessBurger.Board;

namespace ChessBurger.Pieces 
{
    public abstract class Piece : GameObject
    {
        private List<Cell> _possibleMoves;
        private Sprite _sprite;
        private bool _isWhite;

        public Piece(int x, int y, bool isWhite, GameObjectID imgPath) : base(x, y, imgPath)
        {
            _possibleMoves = new List<Cell>();
            _sprite = GenerateSprite();
            _isWhite = isWhite;
        }
        // get cell
        public int[] GetPosition
        {
            get
            {
                return new int[] { X, Y};
            }
        }

        // reset move list
        public void ResetPossibleMove()
        {
            _possibleMoves.Clear();
        }

        // return piece's sprite using its bitmap
        public Sprite GenerateSprite()
        {
            return SplashKit.CreateSprite(GetBitmap);
        }

        // return scaled sprite
        public Sprite GetSprite
        {
            get 
            {
                SplashKit.SpriteSetScale(_sprite, 1/3f);
                return _sprite; 
            }
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
