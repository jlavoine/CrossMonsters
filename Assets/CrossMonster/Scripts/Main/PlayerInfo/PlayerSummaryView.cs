﻿using MyLibrary;
using Zenject;

namespace MonsterMatch {
    public class PlayerSummaryView : GroupView {

        [Inject]
        IPlayerSummaryPM PM;

        void Start() {
            SetModel( PM.ViewModel );
        }

        protected override void OnDestroy() {
            base.OnDestroy();

            PM.Dispose();
        }
    }
}
