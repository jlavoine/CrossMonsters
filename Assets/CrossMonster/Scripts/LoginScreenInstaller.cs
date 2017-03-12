using Zenject;
using System.Collections.Generic;
using MyLibrary;

namespace CrossMonsters {
    public class LoginScreenInstaller : MonoInstaller {
        public override void InstallBindings() {
            Container.Bind<IAppUpgradeRequiredManager>().To<AppUpgradeRequiredManager>().AsSingle();
            Container.Bind<IAppUpgradeRequiredPM>().To<AppUpgradeRequiredPM>().AsSingle();
        }
    }
}