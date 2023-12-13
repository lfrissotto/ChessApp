
namespace ChessChallenge.API
{
    public interface IChessBot
    {
        Move Think(Board board, Timer timer, bool flag);
        void ConnectionHandler();
    }
}
