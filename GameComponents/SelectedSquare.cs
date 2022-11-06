namespace ChessBurger.GameComponents
{
    public class SelectedSquare : GameObject
    {
        private string _imageFile = "selected.png";

        public SelectedSquare(int x, int y, GameObjectID objectID) : base(x, y, objectID)
        {
        }

        public override string CreateImagePath()
        {
            return ASSETS_PATH + $"\\selectedSquare\\{_imageFile}";
        }
    }
}
