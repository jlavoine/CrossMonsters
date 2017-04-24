using Zenject;
using System.Collections.Generic;
using MyLibrary;

namespace MonsterMatch {
    public class MainMenuInstaller : MonoInstaller {
        public override void InstallBindings() {
            Container.Bind<IInitializable>().To<AllTreasurePM>().AsSingle();
            Container.Bind<AllTreasurePM>().AsSingle();

            Container.Bind<IPlayerSummaryPM>().To<PlayerSummaryPM>().AsSingle();
            Container.Bind<IPlayerStatInfoPM>().To<PlayerStatInfoPM>().AsSingle();

            Container.Bind<ITreasureSetPM_Spawner>().To<TreasureSetPM_Spawner>().AsSingle();
            Container.Bind<ITreasurePM_Spawner>().To<TreasurePM_Spawner>().AsSingle();

            Container.BindFactory<ITreasureSetData, TreasureSetPM, TreasureSetPM.Factory>();
            Container.BindFactory<string, TreasurePM, TreasurePM.Factory>();
            Container.BindFactory<DungeonLoader, DungeonLoader.Factory>();

            Container.Bind<ILoadingScreenPM>().To<LoadingScreenPM>().AsSingle();

            Container.Bind<MainMenuFlow>().AsSingle();

            Container.Bind<IUpcomingMaintenancePM>().To<UpcomingMaintenancePM>().AsSingle();
            Container.BindFactory<ISceneStartFlowManager, UpcomingMaintenanceFlowStep, UpcomingMaintenanceFlowStep.Factory>();
            Container.Bind<IUpcomingMaintenanceFlowStepSpawner>().To<UpcomingMaintenanceFlowStepSpawner>().AsSingle();

            Container.Bind<IAllNewsPM>().To<AllNewsPM>().AsSingle();
            Container.BindFactory<IBasicNewsData, SingleNewsPM, SingleNewsPM.Factory>();
            Container.BindFactory<ISceneStartFlowManager, ShowNewsFlowStep, ShowNewsFlowStep.Factory>();
            Container.Bind<IShowNewsStepSpawner>().To<ShowNewsStepSpawner>().AsSingle();

            Container.Bind<ILinkAccountButton>().To<LinkAccountButton>().AsTransient();
            Container.Bind<IAccountLinker>().To<AccountLinker>().AsSingle();
            Container.Bind<IGoogleLinker>().To<GoogleLinker>().AsSingle();
            Container.Bind<ILinkAccountPM>().To<LinkAccountPM>().AsSingle();
            Container.Bind<IAccountAlreadyLinkedPM>().To<AccountAlreadyLinkedPM>().AsSingle();
            Container.Bind<IAccountLinkDonePM>().To<AccountLinkDonePM>().AsSingle();
            Container.Bind<IRemoveDeviceLinkPM>().To<RemoveDeviceLinkPM>().AsSingle();
        }
    }
}