
namespace ChessBurger.MoveValidator.ValidatorCreator
{
    public class ValidatorFactory
    {
        public IValidator CreateValidator(ValidatorID id)
        {
            switch (id)
            {
                case ValidatorID.DEFAULT:
                    return new DefaultValidator();
                case ValidatorID.LINEAR:
                    return new LinearBlock();
                case ValidatorID.DIAGONAL:
                    return new DiagonalBlock();
                default:
                    return new DiagonalBlock();
            }
        }
    }
}
