using ChessBurger.lib;

namespace ChessBurger.GameComponents
{
    public enum GameObjectID
    {
        WHITE_PAWN = 0,
        WHITE_BISHOP = 1,
        WHITE_KNIGHT = 2,
        WHITE_KING = 3,
        WHITE_ROOK = 4,
        WHITE_QUEEN = 5,
        BLACK_PAWN = 6,
        BLACK_BISHOP = 7,
        BLACK_KNIGHT = 8,
        BLACK_KING = 9,
        BLACK_ROOK = 10,
        BLACK_QUEEN = 11,
        BOARD = 12,
        UPPER_BUN = 13,
        UNDER_BUN = 14,
    }

    public abstract class GameObject
    {
        protected const string ASSETS_PATH = "F:\\Swinburne CS\\COS20007 OOP\\Portfolio\\ChessBurger\\assets";
        private GameObjectID _objectID;
        private string _fullImgPath;
        private Sprite _sprite;
        private Bitmap _bmp;
        private int _x;
        private int _y;

        public GameObject(int x, int y, GameObjectID objectID)
        {
            _x = x;
            _y = y;
            _objectID = objectID;
            _fullImgPath = CreateImagePath();
            _bmp = CreateBitmap();
            _sprite = GenerateSprite();
        }

        public int X
        {
            get { return _x; }
            set { _x = value; }
        }

        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }

        // return object's bitmap image
        public Bitmap GetBitmap
        {
            get { return _bmp; }
        }

        // return bitmap created with the full path
        private Bitmap CreateBitmap()
        {
            return SplashKit.LoadBitmap(((int)_objectID).ToString(), _fullImgPath);
        }

        // return full path to image
        abstract public string CreateImagePath();

        // create sprite
        public virtual Sprite GenerateSprite()
        {
            return SplashKit.CreateSprite(GetBitmap);
        }

        // return sprite
        public Sprite GetSprite
        {
            get
            {
                return _sprite;
            }
        }

        // check gameobject id
        public bool IsID(GameObjectID id)
        {
            if (id == _objectID)
            {
                return true;
            }
            return false;
        }
    }
}
