using System.Collections.Generic;
using Zenject;

namespace CrossMonsters {
    public class MonsterManager : IMonsterManager {
        [Inject]
        IGameRules GameRules;

        private List<IGameMonster> mCurrentMonsters;
        public List<IGameMonster> CurrentMonsters { get { return mCurrentMonsters; } set { mCurrentMonsters = value; } }

        private List<IGameMonster> mRemainingMonsters;
        public List<IGameMonster> RemainingMonsters { get { return mRemainingMonsters; } set { mRemainingMonsters = value; } }

        private List<int> mUsedPieceTypes;

        public void SetMonsters( List<IGameMonster> i_allMonsters ) {
            //SetUsedPieceTypes( i_allMonsters );

            CurrentMonsters = new List<IGameMonster>();
            RemainingMonsters = i_allMonsters;
            RemainingMonsters.Shuffle();

            FillCurrentMonstersWithRemaining();
        }       
        
        /*public void SetUsedPieceTypes( List<IGameMonster> i_monsters ) {
            mUsedPieceTypes = new List<int>();
            foreach ( IGameMonster monster in )
        }*/

        public void Tick( long i_time ) {
            foreach ( IGameMonster monster in CurrentMonsters ) {
                monster.Tick( i_time );
            }
        }

        public void ProcessPlayerMove( IGamePlayer i_player, List<int> i_move ) {
            ProcessPlayerMoveOnCurrentMonsters( i_player, i_move );
            RemoveDeadMonstersFromCurrentList();
            FillCurrentMonstersWithRemaining();
        }

        public void ProcessPlayerMoveOnCurrentMonsters( IGamePlayer i_player, List<int> i_move ) {
            foreach ( IGameMonster monster in CurrentMonsters ) {
                if ( monster.DoesMatchCombo( i_move ) ) {
                    monster.AttackedByPlayer( i_player );
                }
            }
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

        public void FillCurrentMonstersWithRemaining() {
            int monstersToAdd = GetNumberOfMissingCurrentMonsters();

            for ( int i = 0; i < monstersToAdd; ++i ) {
                if ( RemainingMonsters.Count > 0 ) {
                    IGameMonster monsterToAdd = RemainingMonsters[0];
                    CurrentMonsters.Add( monsterToAdd );
                    RemainingMonsters.RemoveAt( 0 );
                }
            }
        }

        private int GetNumberOfMissingCurrentMonsters() {
            int numCurrentMonsters = CurrentMonsters.Count;
            int requiredActiveMonsters = GameRules.GetActiveMonsterCount();

            return requiredActiveMonsters - numCurrentMonsters;
        }
    }
}
