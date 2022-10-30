using System;
using System.Text;

namespace ChessBurger.GameComponents
{
    public class Board : GameObject
    {
        public Board() : base(100, 150, GameObjectID.BOARD)
        {
        }

        public override string CreateImagePath()
        {
            return "\\board\\rsz_150.bmp";
        }
    }
}
