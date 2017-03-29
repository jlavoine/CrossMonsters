using Zenject;
using System.Collections.Generic;
using MyLibrary;

namespace CrossMonsters {
    public class LoginScreenInstaller : MonoInstaller {
        public override void InstallBindings() {
            Container.Bind<IAppUpdateRequiredManager>().To<AppUpdateRequirdManager>().AsSingle();
            Container.Bind<IAppUpdateRequiredPM>().To<AppUpdateRequiredPM>().AsSingle();
            Container.Bind<IUpcomingMaintenancePM>().To<UpcomingMaintenancePM>().AsSingle();

            Container.Bind<ILoginMethodManager>().To<LoginMethodManager>().AsSingle();
            Container.Bind<ILoginMethodValidator>().To<LoginMethodValidator>().AsSingle();
            Container.Bind<ILoginMethod_DeviceId>().To<LoginMethod_DeviceId>().AsSingle();
            Container.Bind<ILoginMethod_GameCenter>().To<LoginMethod_GameCenter>().AsSingle();
        }
    }
}