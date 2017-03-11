using UnityEngine;
using Zenject;
using MyLibrary;
using CrossMonsters;

public class ProjectInstaller : MonoInstaller {
    public override void InstallBindings() {
        Container.Bind<IMessageService>().To<MyMessenger>().AsSingle();
        Container.Bind<ITreasureDataManager>().To<TreasureDataManager>().AsSingle();
        Container.Bind<IMonsterDataManager>().To<MonsterDataManager>().AsSingle();
    }
}