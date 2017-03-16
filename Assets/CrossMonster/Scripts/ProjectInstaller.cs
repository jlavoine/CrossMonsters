using UnityEngine;
using Zenject;
using MyLibrary;
using CrossMonsters;

public class ProjectInstaller : MonoInstaller {
    public override void InstallBindings() {
        Container.Bind<IMessageService>().To<MyMessenger>().AsSingle();
        Container.Bind<ITreasureDataManager>().To<TreasureDataManager>().AsSingle();
        Container.Bind<IMonsterDataManager>().To<MonsterDataManager>().AsSingle();
        Container.Bind<ICurrentDungeonGameManager>().To<CurrentDungeonGameManager>().AsSingle();
        Container.Bind<IBackendManager>().To<BackendManager>().AsSingle();
        Container.Bind<ISceneManager>().To<MySceneManager>().AsSingle();
        Container.Bind<IStringTableManager>().To<StringTableManager>().AsSingle();
        Container.Bind<IUpcomingMaintenanceManager>().To<UpcomingMaintenanceManager>().AsSingle();
    }
}