using Zenject;
using System.Collections.Generic;

namespace CrossMonsters {
    public class GameInstaller : MonoInstaller {
        public override void InstallBindings() {
            Container.Bind<IChainManager>().To<ChainManager>().AsSingle();
            Container.Bind<IChainProcessor>().To<ChainProcessor>().AsSingle();

            Container.Bind<IInitializable>().To<GameManager>().AsSingle();
            Container.Bind<IGameManager>().To<GameManager>().AsSingle();

            Container.Bind<IPlayerData>().To<PlayerData>().FromInstance( GetPlayerData() );
            Container.Bind<IInitializable>().To<GamePlayer>().AsSingle();
            Container.Bind<IGamePlayer>().To<GamePlayer>().AsSingle();
            Container.Bind<IGamePlayerPM>().To<GamePlayerPM>().AsTransient();

            Container.Bind<GameBackgroundPM>().AsSingle();

            Container.Bind<IInitializable>().To<GameBoard>().AsSingle();
            Container.Bind<IGameBoard>().To<GameBoard>().AsSingle();

            Container.Bind<IInitializable>().To<GameBoardPM>().AsSingle();
            Container.Bind<GameBoardPM>().AsSingle();                  

            Container.Bind<IGameRules>().To<GameRules>().AsSingle();
            Container.Bind<IMonsterManager>().To<MonsterManager>().AsSingle();

            Container.BindFactory<int, GamePiece, GamePiece.Factory>();

            Container.Bind<IDamageCalculator>().To<DamageCalculator>().AsSingle();
        }

        // TODO this is just temp testing
        public PlayerData GetPlayerData() {
            PlayerData data = new PlayerData();
            data.HP = 10;
            data.Defenses = new Dictionary<int, int>() { { 0, 5 }, { 1, 5 } };

            return data;
        }
    }
}