using Zenject;
using System.Collections.Generic;
using MyLibrary;

namespace CrossMonsters {
    public class MainMenuInstaller : MonoInstaller {
        public override void InstallBindings() {
            Container.Bind<IInitializable>().To<AllTreasurePM>().AsSingle();
            Container.Bind<AllTreasurePM>().AsSingle();

            Container.Bind<ITreasureSetPM_Spawner>().To<TreasureSetPM_Spawner>().AsSingle();
            Container.Bind<ITreasurePM_Spawner>().To<TreasurePM_Spawner>().AsSingle();

            Container.BindFactory<ITreasureSetData, TreasureSetPM, TreasureSetPM.Factory>();
            Container.BindFactory<string, TreasurePM, TreasurePM.Factory>();
            Container.BindFactory<DungeonLoader, DungeonLoader.Factory>();

            Container.Bind<ILoadingScreenPM>().To<LoadingScreenPM>().AsSingle();

            Container.Bind<MainMenuFlow>().AsSingle();
            Container.Bind<IUpcomingMaintenancePM>().To<UpcomingMaintenancePM>().AsSingle();

            Container.Bind<IAllNewsPM>().To<AllNewsPM>().AsSingle();
            Container.BindFactory<IBasicNewsData, SingleNewsPM, SingleNewsPM.Factory>();
        }
    }
}