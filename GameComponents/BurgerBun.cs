namespace ChessBurger.GameComponents
{
    public class BurgerBun : GameObject
    {
        public BurgerBun(int x, int y, GameObjectID objectID) : base(x, y, objectID)
        {
        }

        // create image path
        public override string CreateImagePath()
        {
            if (IsID(GameObjectID.UPPER_BUN))
            {
                return ASSETS_PATH + "\\bun\\UpperBun.png";
            }
            else
            {
                return ASSETS_PATH + "\\bun\\UnderBun.png";
            }
        }
    }
}
