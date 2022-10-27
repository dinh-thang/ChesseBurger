using ChessBurger.MoveValidator.ValidatorCreator;
using ChessBurger.MoveValidator;
using System.Collections.Generic;
using ChessBurger.GameComponents.Pieces;

namespace ChessBurger.Game.GameCommand
{
    public class ValidateCommand : Command
    {
        private ValidatorFactory _validatorFactory;
        private IValidator _defaultValidator;
        private IValidator _diagonalBlockValidator;
        private IValidator _linearBlockValidator;
        private List<Piece> _activePieces;

        public ValidateCommand(List<Piece> activePieces)
        {
            _activePieces = activePieces;
            // validators
            _validatorFactory = new ValidatorFactory();
            _defaultValidator = _validatorFactory.CreateValidator(ValidatorID.DEFAULT);
            _diagonalBlockValidator = _validatorFactory.CreateValidator(ValidatorID.DIAGONAL);
            _linearBlockValidator = _validatorFactory.CreateValidator(ValidatorID.LINEAR);
            // set up validators chain
            _diagonalBlockValidator.SetNextValidator(_linearBlockValidator);
            _linearBlockValidator.SetNextValidator(_defaultValidator);
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
