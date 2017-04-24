using System.Collections.Generic;
using Zenject;
using MyLibrary;

namespace MonsterMatch {
    public class MonsterManager : IMonsterManager {       
        [Inject]
        IGameManager GameManager;

        [Inject]
        IGameBoard GameBoard;

        [Inject]
        IGameRules Rules;

        [Inject]
        IMessageService MessageService;

        private IMonsterWave mCurrentWave;
        public IMonsterWave CurrentWave { get { return mCurrentWave; } set { mCurrentWave = value; } }

        private List<IMonsterWave> mRemainingWaves;
        public List<IMonsterWave> RemainingWaves { get { return mRemainingWaves; } set { mRemainingWaves = value; } }

        public void SetMonsters( List<IMonsterWaveData> i_allMonsters ) {
            CreateWaves( i_allMonsters );
            PrepareNextWave();            
            RandomizeGameBoardIfNoMonsterCombosAvailable();
        }       

        private void CreateWaves( List<IMonsterWaveData> i_data ) {
            RemainingWaves = new List<IMonsterWave>();

            foreach ( IMonsterWaveData data in i_data ) {
                RemainingWaves.Add( new MonsterWave( Rules, data ) );
            }
        }

        public void Tick( long i_time ) {
            CurrentWave.Tick( i_time );
        }

        public void ProcessPlayerMove( IGamePlayer i_player, List<IGamePiece> i_move ) {
            ProcessPlayerMoveOnCurrentWave( i_player, i_move );             
            
            if ( CurrentWave.IsCleared() ) {
                if ( AreAllWavesCleared() ) {
                    SendAllMonstersDeadMessage();
                } else {
                    PrepareNextWave();
                    GameManager.PrepareForNextWave();
                    SendNewWaveEvent();
                }
            }                      
        }

        public bool DoesMoveMatchAnyCurrentMonsters( List<IGamePiece> i_move ) {
            return CurrentWave.DoesMoveMatchAnyCurrentMonsters( i_move );
        }

        public List<List<int>> GetCurrentMonsterCombos() {
            return CurrentWave.GetCurrentMonsterCombos();
        }

        public void ProcessPlayerMoveOnCurrentWave( IGamePlayer i_player, List<IGamePiece> i_move ) {
            CurrentWave.ProcessPlayerMove( i_player, i_move );
        }        

        public void SendAllMonstersDeadMessage() {
            GameManager.OnAllMonstersDead();
        }

        private void PrepareNextWave() {
            CurrentWave = PopCurrentMonsterWave();

            if ( CurrentWave != null ) {
                CurrentWave.Prepare();
            }
        }

        private bool AreAllWavesCleared() {
            return mRemainingWaves.Count == 0;
        }

        private void SendNewWaveEvent() {
            MessageService.Send( GameMessages.NEW_MONSTER_WAVE_EVENT );
        }

        private IMonsterWave PopCurrentMonsterWave() {
            if ( RemainingWaves.Count > 0 ) {
                IMonsterWave currentWave = RemainingWaves[0];
                mRemainingWaves.RemoveAt( 0 );

                return currentWave;
            } else {
                return null;
            }
        }           

        private void RandomizeGameBoardIfNoMonsterCombosAvailable() {
            GameBoard.RandomizeGameBoardIfNoMonsterCombosAvailable();
        }
    }
}
