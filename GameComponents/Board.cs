namespace ChessBurger.GameComponents
{
    public class Board : GameObject
    {
        private string _boardImageFile = "rsz_150.png";
        public Board() : base(100, 150, GameObjectID.BOARD)
        {
        }

        public override string CreateImagePath()
        {
            return ASSETS_PATH + $"\\board\\{_boardImageFile}";
        }
    }
}
