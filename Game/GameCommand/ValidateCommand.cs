using ChessBurger.MoveValidator.ValidatorCreator;
using ChessBurger.MoveValidator;
using System.Collections.Generic;
using System;
using ChessBurger.GameComponents.Pieces;

namespace ChessBurger.Game.GameCommand
{
    public class ValidateCommand : ICommand
    {
        private IValidator _defaultValidator;
        private IValidator _diagonalBlockValidator;
        private IValidator _linearBlockValidator;
        private IValidator _castleMoveValidator;
        private IValidator _pawnCaptureValidator;
        private IValidator _checkValidator;
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
            _pawnCaptureValidator = ValidatorFactory.CreateValidator(ValidatorID.PAWN_CAPTURE);
            _checkValidator = ValidatorFactory.CreateValidator(ValidatorID.CHECK);
            // set up validators chain
            _diagonalBlockValidator.SetNextValidator(_linearBlockValidator);
            _linearBlockValidator.SetNextValidator(_defaultValidator);
            _defaultValidator.SetNextValidator(_pawnCaptureValidator);
        } 
        public CommandStatus Execute()
        {
            CommandStatus status = CommandStatus.SUCCESSFUL;

            for (int i = 0; i < _activePieces.Count; i++)
            {
                _diagonalBlockValidator.ValidCheck(_activePieces[i], _activePieces);
            }
            for (int i = 0; i < _activePieces.Count; i++)
            {
                _castleMoveValidator.ValidCheck(_activePieces[i], _activePieces);
            }
            for (int i = 0; i < _activePieces.Count; i++)
            {
                status = _checkValidator.ValidCheck(_activePieces[i], _activePieces);
                if (status != CommandStatus.SUCCESSFUL)
                {
                    return status;
                }
            }
            return status;
        }
    }
}
