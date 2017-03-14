using Zenject;
using System.Collections.Generic;
using MyLibrary;

namespace CrossMonsters {
    public class LoginScreenInstaller : MonoInstaller {
        public override void InstallBindings() {
            Container.Bind<IAppUpdateRequiredManager>().To<AppUpdateRequirdManager>().AsSingle();
            Container.Bind<IAppUpdateRequiredPM>().To<AppUpdateRequiredPM>().AsSingle();
        }
    }
}