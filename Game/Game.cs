using ChessBurger.GameComponents.Pieces;
using ChessBurger.Game.GameCommand;
using ChessBurger.GameComponents;
using ChessBurger.Ultilities;
using ChessBurger.GUI;
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
        private Board _board;
        private List<Piece> _activePieces;
        // GUI
        private Displayer _displayer;
        // Turn manager
        private TurnManager _moveTurn;        
        // commands set up
        private CommandStatus _commandStat = CommandStatus.NOT_SUCCESSFUL;
        private Command _validateCommand;
        private Command _setBlackPieces;
        private Command _setWhitePieces;
        private Command _choosePieceCommand;
        private Command _movePieceCommand;
        // current possition and desination
        private Cell _selectedPos;
        private Cell _selectedDes;

        private Game()
        {
            // init main window
            _window = new Window(MAIN_WINDOW_TITLE, MAIN_WINDOW_WIDTH, MAIN_WINDOW_HEIGHT);
            // init displayer
            _displayer = new Displayer();
            // init turn manager
            _moveTurn = new TurnManager();
            // init gameobjects
            _board = new Board();
            _activePieces = new List<Piece>();
            // init setPieces command and execute
            _setBlackPieces = new SetPiecesCommand(_moveTurn.GetPlayers[0].IsWhite, _activePieces);
            _setWhitePieces = new SetPiecesCommand(_moveTurn.GetPlayers[1].IsWhite, _activePieces);
            _setBlackPieces.Execute();
            _setWhitePieces.Execute();
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
            do
            {
                // displays board and pieces
                _displayer.DisplayBoard(_board);
                _displayer.DisplayPiece(_activePieces);


                SplashKit.ProcessEvents();

                // save the current piece position
                if (SplashKit.MouseClicked(MouseButton.LeftButton) && _commandStat == CommandStatus.NOT_SUCCESSFUL)
                {
                    // convert the coordinate on the window to board's coordinate
                    _selectedPos = new Cell(Extras.WindowXPosToBoardXPos((int)SplashKit.MouseX()), Extras.WindowYPosToBoardYPos((int)SplashKit.MouseY()));

                    _choosePieceCommand = new ChoosePieceCommand(_selectedPos.X, _selectedPos.Y, _moveTurn.CurrentPlayer.IsWhite, _activePieces);

                    // set the current command to choose piece
                    _moveTurn.CurrentPlayer.SetCommand(_choosePieceCommand);

                    // return SUCCESSFUL if the selected pos hold a valid piece, else NOT_SUCCESSFUL.
                    _commandStat = _moveTurn.CurrentPlayer.ExecuteCommand();
                }
                // save the destination postion
                else if (SplashKit.MouseClicked(MouseButton.LeftButton) && _commandStat == CommandStatus.SUCCESSFUL)
                {
                    // convert the coordinate on the window to board's coordinate
                    _selectedDes = new Cell(Extras.WindowXPosToBoardXPos((int)SplashKit.MouseX()), Extras.WindowYPosToBoardYPos((int)SplashKit.MouseY()));

                    _movePieceCommand = new MakeMoveCommand(_selectedPos.X, _selectedPos.Y, _selectedDes.X, _selectedDes.Y, _activePieces);

                    // set current command to move
                    _moveTurn.CurrentPlayer.SetCommand(_movePieceCommand);

                    // make the move
                    _commandStat = _moveTurn.CurrentPlayer.ExecuteCommand();

                    // make sure a concrete piece is moved to change the turn.
                    if (_commandStat == CommandStatus.SUCCESSFUL)
                    {
                        Console.WriteLine(_moveTurn._curTurn);
                        _moveTurn.SetNextTurn();
                    }
                    // reset the click state to default
                    _commandStat = CommandStatus.NOT_SUCCESSFUL;

                    // validate the moves
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
