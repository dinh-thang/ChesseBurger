namespace ChessBurger.GameComponents
{
    public class Background : GameObject
    {
        public Background() : base(0, 0, GameObjectID.BACKGROUND)
        {
        }

        public override string CreateImagePath()
        {
            return "\\background.png";
        }
    }
}
