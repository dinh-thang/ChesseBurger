using ChessBurger.Game;

namespace ChessBurger
{
    public class Program
    {
        static void Main(string[] args)
        {
            Game.Game newGame = new Game.Game();
            newGame.MainLoop();
        }
    }
}
