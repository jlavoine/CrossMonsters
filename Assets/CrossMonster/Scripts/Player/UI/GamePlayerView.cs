﻿using MyLibrary;
using System.Collections.Generic;
using Zenject;

namespace MonsterMatch {
    public class GamePlayerView : GroupView {
        [Inject]
        IGamePlayerPM PM;

        void Start() {
            SetModel( PM.ViewModel );
        }

        protected override void OnDestroy() {
            base.OnDestroy();

            PM.Dispose();
        }
    }
}
