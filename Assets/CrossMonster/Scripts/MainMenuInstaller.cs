using Zenject;
using System.Collections.Generic;

namespace CrossMonsters {
    public class MainMenuInstaller : MonoInstaller {
        public override void InstallBindings() {
            Container.Bind<IInitializable>().To<AllTreasurePM>().AsSingle();
            Container.Bind<AllTreasurePM>().AsSingle();

            Container.Bind<ITreasureSetPM_Spawner>().To<TreasureSetPM_Spawner>().AsSingle();
            Container.Bind<ITreasurePM_Spawner>().To<TreasurePM_Spawner>().AsSingle();

            Container.BindFactory<ITreasureSetData, TreasureSetPM, TreasureSetPM.Factory>();
            Container.BindFactory<string, TreasurePM, TreasurePM.Factory>();
        }
    }
}