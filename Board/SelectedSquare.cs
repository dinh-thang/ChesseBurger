
namespace ChessBurger.Board
{
    public class SelectedSquare : GameObject
    {
        public SelectedSquare(int x, int y, GameObjectID imgPath) : base(x, y, imgPath)
        {
        }

        public SelectedSquare() : this(-1, -1, GameObjectID.SELECTED_SQUARE)
        {
        }
    }
}
