using System.Collections.Generic;
using ChessBurger.lib;

namespace ChessBurger.GameComponents.Pieces
{
    public abstract class Piece : GameObject
    {
        private bool _isSelected;
        private bool _isPin;
        private List<Cell> _possibleMoves;
        private bool _isWhite;
        private PossibleMoveManager _moveManager;

        public Piece(int x, int y, bool isWhite, GameObjectID objectID) : base(x, y, objectID)
        {
            _moveManager = new PossibleMoveManager();
            _possibleMoves = new List<Cell>();
            _isWhite = isWhite;
        }

        // get possible move manager
        public PossibleMoveManager MoveManager
        {
            get { return _moveManager; }
        }

        // return true if piece use the validator
        public virtual bool UseLinearValidator
        {
            get { return false; }
        }
        public virtual bool UseDiagonalValidator
        {
            get { return false; }
        }
        public virtual bool UseCastleValidator
        {
            get { return false; }
        }

        public bool Selected
        {
            set
            {
                _isSelected = value;
            }
            get
            {
                return _isSelected;
            }
        }

        public bool Pin
        {
            get { return _isPin; }
            set { _isPin = value; }
        }

        // readonly get piece's color
        public bool IsWhite
        {
            get { return _isWhite; }
        }

        // clear and re-generate possible moves
        public abstract void ResetPossibleMove();

        // return piece's sprite using its bitmap
        public override Sprite GenerateSprite()
        {
            Sprite sprite = SplashKit.CreateSprite(GetBitmap);
            SplashKit.SpriteSetScale(sprite, 1 / 3f);
            return sprite;
        }
    }
}
