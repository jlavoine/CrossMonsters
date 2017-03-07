﻿using Zenject;
using System.Collections.Generic;

namespace CrossMonsters {
    public class MainMenuInstaller : MonoInstaller {
        public override void InstallBindings() {
            Container.Bind<IInitializable>().To<AllTreasurePM>().AsSingle();
            Container.Bind<AllTreasurePM>().AsSingle();
        }
    }
}