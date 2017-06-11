using MyLibrary;
using Zenject;

namespace MonsterMatch {
    public class ShowGauntletStep : BaseSceneStartFlowStep, IShowGauntletStep {
        readonly ICurrentGauntletManager mGauntletManager;
        readonly IEnterGauntletPM mEnterGauntlet;

        public ShowGauntletStep( ICurrentGauntletManager i_gauntletManager, IEnterGauntletPM i_enterGauntlet, ISceneStartFlowManager i_sceneManager ) : base( i_sceneManager ) {
            mGauntletManager = i_gauntletManager;
            mEnterGauntlet = i_enterGauntlet;
        }

        public override void Start() {
            if ( mGauntletManager.ComingFromGauntletVictory ) {
                mGauntletManager.ComingFromGauntletVictory = false;
                mEnterGauntlet.SetIndex( mGauntletManager.CurrentGauntletIndex );
                mEnterGauntlet.Show();
            }
        }

        protected override void OnDone() {}

        public class Factory : Factory<ISceneStartFlowManager, ShowGauntletStep> { }
    }
}
