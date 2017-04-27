using UnityEngine;
using Zenject;
using MyLibrary;
using MonsterMatch;

public class ProjectInstaller : MonoInstaller {
    public override void InstallBindings() {
        Container.Bind<IMessageService>().To<MyMessenger>().AsSingle();
        Container.Bind<MyLogger>().AsSingle();
        Container.Bind<ITreasureDataManager>().To<TreasureDataManager>().AsSingle();
        Container.Bind<ITimedChestDataManager>().To<TimedChestDataManager>().AsSingle();
        Container.Bind<IMonsterDataManager>().To<MonsterDataManager>().AsSingle();
        Container.Bind<IPlayerDataManager>().To<PlayerDataManager>().AsSingle();        
        Container.Bind<IBackendManager>().To<BackendManager>().AsSingle();
        Container.Bind<ISceneManager>().To<MySceneManager>().AsSingle();
        Container.Bind<IStringTableManager>().To<StringTableManager>().AsSingle();
        Container.Bind<IUpcomingMaintenanceManager>().To<UpcomingMaintenanceManager>().AsSingle();
        Container.Bind<INewsManager>().To<NewsManager>().AsSingle();
        Container.Bind<IPreferredLoginMethod>().To<PreferredLoginMethod>().AsSingle();

        Container.Bind<ICurrentDungeonGameManager>().To<CurrentDungeonGameManager>().AsSingle();
        Container.BindFactory<IDungeonRewardData, DungeonReward, DungeonReward.Factory>();
        Container.Bind<IDungeonRewardSpawner>().To<DungeonRewardSpawner>().AsSingle();        
    }
}