
namespace CrossMonsters {
    public interface IChainManager {
        void StartChain( IGamePiece i_piece );
        void ContinueChain( IGamePiece i_piece );
        void EndChain();
        void CancelChain();

        bool IsNoChain();
        bool IsActiveChain();
    }
}
