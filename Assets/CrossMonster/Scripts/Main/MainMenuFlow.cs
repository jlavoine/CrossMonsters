using MyLibrary;
using System.Collections.Generic;

namespace MonsterMatch {
    public class MainMenuFlow : ISceneStartFlowManager {
        readonly IUpcomingMaintenanceFlowStepSpawner mMaintenanceStepSpawner;
        readonly IShowNewsStepSpawner mNewStepSpawner;
        readonly IShowLoginPromosStepSpawner mLoginPromosStepSpawner;
        readonly IShowGauntletStepSpawner mShowGauntletStepSpawner;

        private Queue<ISceneStartFlowStep> mSteps = new Queue<ISceneStartFlowStep>();

        public MainMenuFlow( IShowGauntletStepSpawner i_gauntletStepSpawner, IUpcomingMaintenanceFlowStepSpawner i_maintenanceStepSpawner, IShowNewsStepSpawner i_newsStepSpawner, IShowLoginPromosStepSpawner i_loginPromoStepSpawner ) {
            mMaintenanceStepSpawner = i_maintenanceStepSpawner;
            mNewStepSpawner = i_newsStepSpawner;
            mLoginPromosStepSpawner = i_loginPromoStepSpawner;
            mShowGauntletStepSpawner = i_gauntletStepSpawner;
        }

        public void StepFinished() {            
            ProcessNextStep();
        }

        public void Start() {
            AddStepsToQueue();            
            ProcessNextStep();
        }

        private void AddStepsToQueue() {
            mSteps.Enqueue( mMaintenanceStepSpawner.Create( this ) );
            mSteps.Enqueue( mNewStepSpawner.Create( this ) );
            mSteps.Enqueue( mLoginPromosStepSpawner.Create( this ) );
            mSteps.Enqueue( mShowGauntletStepSpawner.Create( this ) );
        }

        private void ProcessNextStep() {
            if ( mSteps.Count > 0 ) {
                ISceneStartFlowStep step = mSteps.Dequeue();
                step.Start();
            } else {
                UnityEngine.Debug.LogError( "Done with main menu flow steps" );
            }
        }
    }
}