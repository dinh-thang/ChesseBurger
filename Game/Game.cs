using ChessBurger.GameComponents.Pieces;
using ChessBurger.Game.GameCommand;
using ChessBurger.GameComponents;
using ChessBurger.Ultilities;
using ChessBurger.lib;
using System.Collections.Generic;
using System;

namespace ChessBurger.Game
{
    public enum GameState
    {
        WHITE_WON,
        BLACK_WON,
        WHITE_TURN,
        BLACK_TURN,
        MENU
    }

    public class Game
    {
        // main window configurations
        private const string MAIN_WINDOW_TITLE = "Chesse Burger";
        private const int MAIN_WINDOW_WIDTH = 900;
        private const int MAIN_WINDOW_HEIGHT = 700;
        private readonly Window _window;
        // Game instance
        private static Game _gameInstance;
        // Game objects
        private List<Piece> _activePieces;
        // Turn manager
        private TurnManager _moveTurn;        
        // commands set up
        private CommandStatus _commandStat = CommandStatus.NOT_SUCCESSFUL;
        private ICommand _validateCommand;
        private ICommand _setPieces;
        private ICommand _choosePieceCommand;
        private ICommand _movePieceCommand;
        private ICommand _display;
        // current possition and desination
        private Cell _selectedPos;
        private Cell _selectedDes;

        private Game()
        {
            // init main window
            _window = new Window(MAIN_WINDOW_TITLE, MAIN_WINDOW_WIDTH, MAIN_WINDOW_HEIGHT);
            // init turn manager
            _moveTurn = new TurnManager();
            _activePieces = new List<Piece>();
        }

        // get the game instance
        public static Game GetInstance()
        {
            if (_gameInstance == null)
            {
                _gameInstance = new Game();
            }
            return _gameInstance;
        }

        public void MainLoop()
        {
            // init setPieces command and execute
            for (int i = 0; i < _moveTurn.GetPlayers.Count; i++)
            {
                _setPieces = new SetPiecesCommand(_moveTurn.GetPlayers[i].IsWhite, _activePieces);
                _setPieces.Execute();
            }

            _display = new DisplayCommand(_activePieces);
            _display.Execute();
            
            do
            {

                SplashKit.ProcessEvents();

                // save the current piece position
                if (SplashKit.MouseClicked(MouseButton.LeftButton) && _commandStat == CommandStatus.NOT_SUCCESSFUL)
                {
                    _validateCommand = new ValidateCommand(_activePieces);
                    _moveTurn.CurrentPlayer.SetCommand(_validateCommand);
                    _moveTurn.CurrentPlayer.ExecuteCommand();

                    _selectedPos = new Cell(Extras.WindowXPosToBoardXPos((int)SplashKit.MouseX()), Extras.WindowYPosToBoardYPos((int)SplashKit.MouseY()));

                    _choosePieceCommand = new ChoosePieceCommand(_selectedPos.X, _selectedPos.Y, _moveTurn.CurrentPlayer.IsWhite, _activePieces);

                    _moveTurn.CurrentPlayer.SetCommand(_choosePieceCommand);

                    _commandStat = _moveTurn.CurrentPlayer.ExecuteCommand();
                }
                else if (SplashKit.MouseClicked(MouseButton.LeftButton) && _commandStat == CommandStatus.SUCCESSFUL)
                {
                    _selectedDes = new Cell(Extras.WindowXPosToBoardXPos((int)SplashKit.MouseX()), Extras.WindowYPosToBoardYPos((int)SplashKit.MouseY()));

                    _movePieceCommand = new MakeMoveCommand(_selectedPos.X, _selectedPos.Y, _selectedDes.X, _selectedDes.Y, _activePieces);

                    _moveTurn.CurrentPlayer.SetCommand(_movePieceCommand);

                    _commandStat = _moveTurn.CurrentPlayer.ExecuteCommand();

                    if (_commandStat == CommandStatus.SUCCESSFUL)
                    {
                        _moveTurn.SetNextTurn();
                    }
                    _commandStat = CommandStatus.NOT_SUCCESSFUL;

                    _display = new DisplayCommand(_activePieces);
                    _display.Execute();

                    _validateCommand = new ValidateCommand(_activePieces);
                    _moveTurn.CurrentPlayer.SetCommand(_validateCommand);
                    _moveTurn.CurrentPlayer.ExecuteCommand();
                }


                // insert code here
                SplashKit.RefreshScreen();
            } while (!_window.CloseRequested);
        }
    }
}
