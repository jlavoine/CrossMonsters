using System.Collections.Generic;
using Zenject;

namespace MonsterMatch {
    public class MonsterWave : IMonsterWave {
        readonly IGameRules mRules; // gah, sorry, it's late and I don't want to create a spawner...

        private List<IGameMonster> mCurrentMonsters = new List<IGameMonster>();
        public List<IGameMonster> CurrentMonsters { get { return mCurrentMonsters; } set { mCurrentMonsters = value; } }

        private List<IGameMonster> mRemainingMonsters = new List<IGameMonster>();
        public List<IGameMonster> RemainingMonsters { get { return mRemainingMonsters; } set { mRemainingMonsters = value; } }

        public MonsterWave( IGameRules i_rules, IMonsterWaveData i_data ) {
            mRules = i_rules;
            RemainingMonsters = i_data.Monsters;
        }

        public void Prepare() {
            FillCurrentMonstersFromRemainingMonsters();
        }

        public void Tick( long i_time ) {
            foreach ( IGameMonster monster in CurrentMonsters ) {
                monster.Tick( i_time );
            }
        }

        public void ProcessPlayerMove( IGamePlayer i_player, List<IGamePiece> i_move ) {
            foreach ( IGameMonster monster in CurrentMonsters ) {
                if ( monster.DoesMatchCombo( i_move ) ) {
                    monster.AttackedByPlayer( i_player );
                }
            }

            RemoveDeadMonstersFromCurrentList();
            FillCurrentMonstersFromRemainingMonsters();
        }

        public bool IsCleared() {
            return CurrentMonsters.Count == 0;
        }

        public void RemoveDeadMonstersFromCurrentList() {
            List<IGameMonster> newCurrentMonsters = new List<IGameMonster>();

            foreach ( IGameMonster monster in CurrentMonsters ) {
                if ( !monster.IsDead() ) {
                    newCurrentMonsters.Add( monster );
                }
            }

            CurrentMonsters = newCurrentMonsters;
        }

        public void FillCurrentMonstersFromRemainingMonsters() {
            int monstersToAdd = GetNumberOfMissingCurrentMonsters();

            for ( int i = 0; i < monstersToAdd; ++i ) {
                if ( RemainingMonsters.Count > 0 ) {
                    IGameMonster monsterToAdd = RemainingMonsters[0];
                    CurrentMonsters.Add( monsterToAdd );
                    RemainingMonsters.RemoveAt( 0 );
                }
            }
        }

        public List<List<int>> GetCurrentMonsterCombos() {
            List<List<int>> currentCombos = new List<List<int>>();

            foreach ( IGameMonster monster in CurrentMonsters ) {
                currentCombos.Add( monster.AttackCombo );
            }

            return currentCombos;
        }

        public bool DoesMoveMatchAnyCurrentMonsters( List<IGamePiece> i_move ) {
            foreach ( IGameMonster monster in CurrentMonsters ) {
                if ( monster.DoesMatchCombo( i_move ) ) {
                    return true;
                }
            }

            return false;
        }

        private int GetNumberOfMissingCurrentMonsters() {
            int numCurrentMonsters = CurrentMonsters.Count;
            int requiredActiveMonsters = mRules.GetActiveMonsterCount();

            return requiredActiveMonsters - numCurrentMonsters;
        }
    }
}
