using ChessBurger.GameComponents;
using ChessBurger.lib;

namespace ChessBurger.GUI
{
    public class GameObjectDisplayer : IDisplayer
    {
        private GameObject _object;

        public GameObjectDisplayer(GameObjectID objectID)
        {
            _object = GameObjectFactory.CreateObject(objectID);
        }

        public void Display()
        {
            if (_object.IsID(GameObjectID.BOARD))
            {
                DisplayBoardIndex();
            }
            SplashKit.DrawSprite(_object.GetSprite, _object.X, _object.Y);
        }

        public void DisplayBoardIndex()
        {
            for (int i = 0; i < 8; i++)
            {
                SplashKit.DrawText($"{i}", SplashKit.ColorBlack(), 125 + (i * 50), 125);
                SplashKit.DrawText($"{i}", SplashKit.ColorBlack(), 75, 175 + (i * 50));
            }
        }
    }
}
