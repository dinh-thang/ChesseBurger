

namespace ChessBurger.MoveValidator
{
    public class CastleValidator : DefaultValidator
    {
        // else => check if there is any pieces between the king and the rook?
        // if not => remove castle moves
        // else => check if there any opposite color possible move in the range between the king and rook?
        // if yes => remove castle moves
        // else => make the move
            // if the desX = curX + 2 => king's x += 2 and rook's x = king's x - 1
            // if the desX = curX - 2 => king's x -= 2 and rook's x = king's x + 1

        // first check if king and rook is moved?
        // if not => remove king's castle moves
        
    }
}
