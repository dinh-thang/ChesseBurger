using ChessBurger.lib;

namespace ChessBurger.Board
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
        SELECTED_SQUARE = 13,
        NULL_PIECE = 14
    }

    public class GameObject
    {
        private const string ASSETS_PATH = "F:\\Swinburne CS\\COS20007 OOP\\Portfolio\\ChessBurger\\assets";
        private string _fullImgPath;
        private GameObjectID _objectID;
        private Bitmap _bmp;
        private int _x;
        private int _y;

        public GameObject(int x, int y, GameObjectID imgPath)
        {
            _objectID = imgPath;
            _x = x;
            _y = y;
            _fullImgPath = ASSETS_PATH + CreateImagePath(_objectID);
            _bmp = CreateBitmap();
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

        // return obj's ID
        public GameObjectID ID
        {
            get { return _objectID; }
        }

        public Bitmap GetBitmap
        {
            get { return _bmp; }
        } 

        // return full path to image
        public string CreateImagePath(GameObjectID imgPath)
        {
            switch (imgPath)
            {
                default:
                    return "";
                case GameObjectID.BLACK_KNIGHT:
                    return "\\pieces\\bn.png";
                case GameObjectID.BLACK_PAWN:
                    return "\\pieces\\bp.png";
                case GameObjectID.BLACK_BISHOP:
                    return "\\pieces\\bb.png";
                case GameObjectID.BLACK_KING:
                    return "\\pieces\\bk.png";
                case GameObjectID.BLACK_ROOK:
                    return "\\pieces\\br.png";
                case GameObjectID.BLACK_QUEEN:
                    return "\\pieces\\bq.png";
                case GameObjectID.WHITE_KNIGHT:
                    return "\\pieces\\wn.png";
                case GameObjectID.WHITE_BISHOP:
                    return "\\pieces\\wb.png";
                case GameObjectID.WHITE_QUEEN:
                    return "\\pieces\\wq.png";
                case GameObjectID.WHITE_KING:
                    return "\\pieces\\wk.png";
                case GameObjectID.WHITE_PAWN:
                    return "\\pieces\\wp.png";
                case GameObjectID.WHITE_ROOK:
                    return "\\pieces\\wr.png";
                case GameObjectID.BOARD:
                    return "\\board\\rsz_150.bmp";
                case GameObjectID.SELECTED_SQUARE:
                    return "\\selectedSquare\\mac_rsz.png";
            }
        }

        // return bitmap created with the full path
        public Bitmap CreateBitmap()
        {
            return SplashKit.LoadBitmap(((int) _objectID).ToString(), _fullImgPath);
        }

        // check gameobject id
        public bool IsID(GameObjectID id)
        {
            if (id == this.ID)
            {
                return true;
            }
            return false;
        }
    }
}
