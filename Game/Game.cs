using System.Collections.Generic;
using ChessBurger.lib;
using ChessBurger.GUI;
using ChessBurger.Ultilities;
using ChessBurger.MoveValidator;
using System.Linq;
using ChessBurger.MoveValidator.ValidatorCreator;

namespace ChessBurger.Game
{
    public enum GameState
    {
        WHITE_WON,
        BLACK_WON,
        WHITE_TURN,
        BLACK_TURN,
    }

    public enum ClickState
    {
        FIRST_CLICK,
        SECOND_CLICK
    }

    public class Game
    {
        private ClickState _clickState = ClickState.SECOND_CLICK;
        private const string MAIN_WINDOW_TITLE = "Chesse Burger";
        private const int MAIN_WINDOW_WIDTH = 900;
        private const int MAIN_WINDOW_HEIGHT = 700;
        private Displayer _displayer;
        private Board.Board _board;
        private readonly Window _window;
        private Turn _moveTurn; 
        private List<int> _selectedPos;
        private List<int> _selectedDes;
        private ValidatorFactory _validatorFactory;
        private IValidator _defaultValidator;
        private IValidator _diagonalBlockValidator;
        private IValidator _linearBlockValidator;

        public Game()
        {
            // create main window
            _window = new Window(MAIN_WINDOW_TITLE, MAIN_WINDOW_WIDTH, MAIN_WINDOW_HEIGHT);

            // initiate instances
            _selectedDes = new List<int>();
            _selectedPos = new List<int>(); 
            _displayer = new Displayer();
            _moveTurn = new Turn();
            _board = new Board.Board();
            // validators
            _validatorFactory = new ValidatorFactory();
            _defaultValidator = _validatorFactory.CreateValidator(ValidatorID.DEFAULT);
            _diagonalBlockValidator = _validatorFactory.CreateValidator(ValidatorID.DIAGONAL);
            _linearBlockValidator = _validatorFactory.CreateValidator(ValidatorID.LINEAR);

            // set up validator chain 
            _diagonalBlockValidator.SetNextValidator(_linearBlockValidator);
            _linearBlockValidator.SetNextValidator(_defaultValidator);

            // validate the moves of all pieces (initialize)
            ValidateMove();
        }

        // validate pseudo-legal moves
        public void ValidateMove()
        {
            for (int i = 0; i < _moveTurn.GetPlayers[0].GetActivePieces.Count; i++)
            {
                _diagonalBlockValidator.ValidCheck(_moveTurn.GetPlayers[0].GetActivePieces[i], Enumerable.Concat(_moveTurn.GetPlayers[0].GetActivePieces, _moveTurn.GetPlayers[1].GetActivePieces).ToList());
            }

            for (int i = 0; i < _moveTurn.GetPlayers[1].GetActivePieces.Count; i++)
            {
                _diagonalBlockValidator.ValidCheck(_moveTurn.GetPlayers[1].GetActivePieces[i], Enumerable.Concat(_moveTurn.GetPlayers[0].GetActivePieces, _moveTurn.GetPlayers[1].GetActivePieces).ToList());
            }
        }

        public void MainLoop()
        {
            do
            {
                // displays board and pieces
                _displayer.DisplayBoard(_board);
                _displayer.DisplayBoardIndex();
                _displayer.DisplayPiece(_moveTurn.GetPlayers[0].GetActivePieces);
                _displayer.DisplayPiece(_moveTurn.GetPlayers[1].GetActivePieces);


                SplashKit.ProcessEvents();

                // save the current piece position
                if (SplashKit.MouseClicked(MouseButton.LeftButton) && _clickState == ClickState.SECOND_CLICK)
                {
                    // convert the coordinate on the window to board's coordinate
                    _selectedPos = new List<int> { Extras.WindowXPosToBoardXPos((int) SplashKit.MouseX()), Extras.WindowYPosToBoardYPos((int) SplashKit.MouseY()) };

                    // return PIECE_SELECTED if the selected pos hold a valid piece, else DESTINATION_SELECTED.
                    _clickState = _moveTurn.CurrentPlayer.UpdateClickStateOfFirstClick(_selectedPos[0], _selectedPos[1]);
                }
                // save the destination postion
                else if (SplashKit.MouseClicked(MouseButton.LeftButton) && _clickState == ClickState.FIRST_CLICK)
                {
                    // convert the coordinate on the window to board's coordinate
                    _selectedDes = new List<int> { Extras.WindowXPosToBoardXPos((int)SplashKit.MouseX()), Extras.WindowYPosToBoardYPos((int)SplashKit.MouseY()) };

                    // check if the selected pos contains any opposite color piece

                    // make the move
                    bool concretePieceMoved = _moveTurn.CurrentPlayer.Move(_selectedDes[0], _selectedDes[1]);

                    // make sure a concrete piece is moved to change the turn.
                    if (concretePieceMoved)
                    { 
                        if (_moveTurn.NextPLayer.IsOccupied(_selectedDes[0], _selectedDes[1]))
                        {
                            _moveTurn.NextPLayer.RemovePiece(_selectedDes[0], _selectedDes[1]);
                        }
                        _moveTurn.SetNextTurn();
                    }
                    
                    // reset the click state to default
                    _clickState = ClickState.SECOND_CLICK; 
                    _moveTurn.CurrentPlayer.UpdatePiecesPosition();
                    ValidateMove();
                }


                // insert code here
                SplashKit.RefreshScreen();
            } while (!_window.CloseRequested);
        }
    }
}
