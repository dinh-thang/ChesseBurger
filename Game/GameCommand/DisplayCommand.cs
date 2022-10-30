using ChessBurger.GUI;
using System.Collections.Generic;
using ChessBurger.GameComponents.Pieces;

namespace ChessBurger.Game.GameCommand
{
    public class DisplayCommand : ICommand
    {
        private PieceDisplayer _pieceDisplayer;
        private GameObjectDisplayer _boardDisplayer;
        private GameObjectDisplayer _upperBunDisplayer;
        private GameObjectDisplayer _underBunDisplayer;
        private GameObjectDisplayer _backgroundDisplayer;

        public DisplayCommand(List<Piece> activePieces)
        {
            _pieceDisplayer = new PieceDisplayer(activePieces);
            _boardDisplayer = new GameObjectDisplayer(GameComponents.GameObjectID.BOARD);
            _upperBunDisplayer = new GameObjectDisplayer(GameComponents.GameObjectID.UPPER_BUN);
            _underBunDisplayer = new GameObjectDisplayer(GameComponents.GameObjectID.UNDER_BUN);
            _backgroundDisplayer = new GameObjectDisplayer(GameComponents.GameObjectID.BACKGROUND);
        }

        public CommandStatus Execute()
        {
            _boardDisplayer.Display();
            _underBunDisplayer.Display();
            _upperBunDisplayer.Display();
            _pieceDisplayer.Display();
            return CommandStatus.SUCCESSFUL;
        }
    }
}
