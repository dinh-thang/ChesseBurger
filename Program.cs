using ChessBurger.Game;

namespace ChessBurger
{
    public class Program
    {
        static void Main(string[] args)
        {
            Game.Game newGame = Game.Game.GetInstance();
            newGame.MainLoop();
        }
    }
}
