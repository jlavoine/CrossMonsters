using Zenject;
using System.Collections.Generic;

namespace CrossMonsters {
    public class GameInstaller : MonoInstaller {
        public override void InstallBindings() {
            Container.Bind<IChainBuilder>().To<ChainBuilder>().AsSingle();
            Container.Bind<IChainProcessor>().To<ChainProcessor>().AsSingle();

            Container.Bind<IInitializable>().To<GameManager>().AsSingle();
            Container.Bind<IGameManager>().To<GameManager>().AsSingle();
            Container.Bind<IValidBoardChecker>().To<ValidBoardChecker>().AsSingle();

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

            Container.BindFactory<IDungeonReward, IAllRewardsPM, SingleRewardPM, SingleRewardPM.Factory>();
            Container.Bind<ISingleRewardPM_Spawner>().To<SingleRewardPM_Spawner>().AsSingle();
        }

        // TODO this is just temp testing
        public PlayerData GetPlayerData() {
            PlayerData data = new PlayerData();
            data.HP = 100;
            data.Defenses = new Dictionary<int, int>() { { 0, 5 }, { 1, 5 } };

            return data;
        }
    }
}