using System;
using System.Text;
using ChessBurger.Board;

namespace ChessBurger.Board
{
    public class Board : GameObject
    {
        public Board() : base(100, 150, GameObjectID.BOARD)
        {
        }
    }
}
