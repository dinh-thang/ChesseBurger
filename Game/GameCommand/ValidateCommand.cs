using ChessBurger.MoveValidator.ValidatorCreator;
using ChessBurger.MoveValidator;
using System.Collections.Generic;
using ChessBurger.GameComponents.Pieces;

namespace ChessBurger.Game.GameCommand
{
    public class ValidateCommand : ICommand
    {
        private IValidator _defaultValidator;
        private IValidator _diagonalBlockValidator;
        private IValidator _linearBlockValidator;
        private IValidator _castleMoveValidator;
        // active pieces
        private List<Piece> _activePieces;

        public ValidateCommand(List<Piece> activePieces)
        {
            _activePieces = activePieces;
            // validators
            _defaultValidator = ValidatorFactory.CreateValidator(ValidatorID.DEFAULT);
            _diagonalBlockValidator = ValidatorFactory.CreateValidator(ValidatorID.DIAGONAL);
            _linearBlockValidator = ValidatorFactory.CreateValidator(ValidatorID.LINEAR);
            _castleMoveValidator = ValidatorFactory.CreateValidator(ValidatorID.CASTLE);

            // set up validators chain
            _diagonalBlockValidator.SetNextValidator(_linearBlockValidator);
            _linearBlockValidator.SetNextValidator(_castleMoveValidator);
            _castleMoveValidator.SetNextValidator(_defaultValidator);
        } 
        public CommandStatus Execute()
        {
            for (int i = 0; i < _activePieces.Count; i++)
            {
                _diagonalBlockValidator.ValidCheck(_activePieces[i], _activePieces);
            }
            return CommandStatus.SUCCESSFUL;
        }
    }
}
