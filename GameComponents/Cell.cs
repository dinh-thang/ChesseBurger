namespace ChessBurger.GameComponents
{
    public struct Cell
    {
        private int _x;
        private int _y;

        public Cell(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public int X { get { return _x; } }
        public int Y { get { return _y; } }
    }
}
