
namespace ChessBurger.MoveValidator.ValidatorCreator
{
    public class ValidatorFactory
    {
        public static IValidator CreateValidator(ValidatorID id)
        {
            switch (id)
            {
                case ValidatorID.DEFAULT:
                    return new DefaultValidator();
                case ValidatorID.LINEAR:
                    return new LinearBlock();
                case ValidatorID.DIAGONAL:
                    return new DiagonalBlock();
                case ValidatorID.CASTLE:
                    return new CastleValidator();
                default:
                    return new DiagonalBlock();
            }
        }
    }
}
