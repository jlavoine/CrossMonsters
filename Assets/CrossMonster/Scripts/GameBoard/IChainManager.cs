
namespace CrossMonsters {
    public interface IChainManager {
        void StartChain( IGamePiece i_piece );
        void ContinueChain( IGamePiece i_piece );

        bool IsNoChain();
        bool IsActiveChain();
    }
}
