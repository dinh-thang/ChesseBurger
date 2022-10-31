namespace ChessBurger.GameComponents
{
    public class GameObjectFactory 
    {
        public static GameObject CreateObject(GameObjectID objectID)
        {
            switch (objectID)
            {
                default:
                    return new Board();
                case GameObjectID.UPPER_BUN:
                    return new BurgerBun(100, 0, objectID);
                case GameObjectID.UNDER_BUN:
                    return new BurgerBun(100, 560, objectID);
            }
        }
    }
}
