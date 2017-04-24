using MyLibrary;
using System.Collections.Generic;

namespace MonsterMatch {
    public class MainMenuFlow : ISceneStartFlowManager {
        readonly IUpcomingMaintenanceFlowStepSpawner mMaintenanceStepSpawner;
        readonly IShowNewsStepSpawner mNewStepSpawner;

        private Queue<ISceneStartFlowStep> mSteps = new Queue<ISceneStartFlowStep>();

        public MainMenuFlow( IUpcomingMaintenanceFlowStepSpawner i_maintenanceStepSpawner, IShowNewsStepSpawner i_newsStepSpawner ) {
            mMaintenanceStepSpawner = i_maintenanceStepSpawner;
            mNewStepSpawner = i_newsStepSpawner;
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