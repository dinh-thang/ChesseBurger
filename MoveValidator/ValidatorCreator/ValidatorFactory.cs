
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
                    return new LinearBlockValidator();
                case ValidatorID.DIAGONAL:
                    return new DiagonalBlockValidator();
                case ValidatorID.CASTLE:
                    return new CastleValidator();
                default:
                    return new DiagonalBlockValidator();
            }
        }
    }
}
