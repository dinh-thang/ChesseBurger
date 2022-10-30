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
            SplashKit.DrawSprite(_object.GetSprite, _object.X, _object.Y);
        }
    }
}
