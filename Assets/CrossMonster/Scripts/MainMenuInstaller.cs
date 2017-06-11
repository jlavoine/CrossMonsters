using Zenject;
using UnityEngine;
using MyLibrary;

namespace MonsterMatch {
    public class MainMenuInstaller : MonoInstaller {
        public GameObject AudioManager;

        public override void InstallBindings() {
            Container.Bind<IAudioManager>().To<MyLibrary.AudioManager>().FromComponentInNewPrefab( AudioManager ).AsSingle();

            Container.Bind<IInitializable>().To<AllTreasurePM>().AsSingle();
            Container.Bind<AllTreasurePM>().AsSingle();

            Container.Bind<IPlayerSummaryPM>().To<PlayerSummaryPM>().AsSingle();
            Container.Bind<IPlayerStatInfoPM>().To<PlayerStatInfoPM>().AsSingle();

            Container.Bind<ITreasureSetPM_Spawner>().To<TreasureSetPM_Spawner>().AsSingle();
            Container.Bind<ITreasurePM_Spawner>().To<TreasurePM_Spawner>().AsSingle();

            Container.BindFactory<ITreasureSetData, TreasureSetPM, TreasureSetPM.Factory>();
            Container.BindFactory<string, TreasurePM, TreasurePM.Factory>();            

            Container.Bind<ILoadingScreenPM>().To<LoadingScreenPM>().AsSingle();

            Container.Bind<IDungeonLoader>().To<DungeonLoader>().AsSingle();
            Container.Bind<IEnterDungeonPM>().To<EnterDungeonPM>().AsSingle();

            Container.Bind<IGauntletInventoryHelper>().To<GauntletInventoryHelper>().AsSingle();
            Container.Bind<IEnterGauntletPM>().To<EnterGauntletPM>().AsSingle();

            Container.Bind<MainMenuFlow>().AsSingle();

            Container.BindFactory<ISceneStartFlowManager, ShowGauntletStep, ShowGauntletStep.Factory>();
            Container.Bind<IShowGauntletStepSpawner>().To<ShowGauntletStepSpawner>().AsSingle();

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

            Container.Bind<ITimedChestsMainPM>().To<TimedChestsMainPM>().AsSingle();
            Container.BindFactory<ITimedChestData, TimedChestPM, TimedChestPM.Factory>();
            Container.Bind<ITimedChestPM_Spawner>().To<TimedChestPM_Spawner>().AsSingle();

            Container.BindFactory<IDungeonReward, IAllRewardsPM, SingleRewardPM, SingleRewardPM.Factory>();
            Container.Bind<ISingleRewardPM_Spawner>().To<SingleRewardPM_Spawner>().AsSingle();

            Container.Bind<IAppBusyPM>().To<AppBusyPM>().AsSingle();

            Container.Bind<ILoginPromoDisplaysPM>().To<LoginPromoDisplaysPM>().AsSingle();
            Container.BindFactory<ILoginPromotionData, SingleLoginPromoDisplayPM, SingleLoginPromoDisplayPM.Factory>();
            Container.Bind<ISingleLoginPromoPM_Spawner>().To<SingleLoginPromoPM_Spawner>().AsSingle();
            Container.BindFactory<int, IGameRewardData, SingleLoginPromoRewardPM, SingleLoginPromoRewardPM.Factory>();
            Container.Bind<ISingleLoginPromoRewardPM_Spawner>().To<SingleLoginPromoRewardPM_Spawner>().AsSingle();
            Container.Bind<IActiveLoginPromoPM>().To<ActiveLoginPromoPM>().AsSingle();
            Container.BindFactory<ILoginPromotionData, ActiveLoginPromoButtonPM, ActiveLoginPromoButtonPM.Factory>();
            Container.Bind<IActiveLoginPromoButtonPM_Spawner>().To<ActiveLoginPromoButtonPM_Spawner>().AsSingle();
            Container.BindFactory<ISceneStartFlowManager, ShowLoginPromosStep, ShowLoginPromosStep.Factory>();
            Container.Bind<IShowLoginPromosStepSpawner>().To<ShowLoginPromosStepSpawner>().AsSingle();
            Container.Bind<ILoginPromoPopupHelper>().To<LoginPromoPopupHelper>().AsSingle();
        }
    }
}