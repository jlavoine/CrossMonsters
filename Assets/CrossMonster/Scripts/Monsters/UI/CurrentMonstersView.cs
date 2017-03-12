using UnityEngine;
using MyLibrary;
using System.Collections.Generic;
using Zenject;

namespace CrossMonsters {
    public class CurrentMonstersView : GroupView {
        public GameObject MonsterViewPrefab;

        [Inject]
        IMonsterManager MonsterManager;

        [Inject]
        IGameManager GameManager;

        [Inject]
        ICurrentDungeonGameManager CurrentDungeonData;

        void Start() {
            List<IGameMonster> monsters = GetMonsters();
            MonsterManager.SetMonsters( monsters );

            CreateMonsterViews();
        }

        void Update() {
            if ( GameManager.IsGamePlaying() ) {
                TickCurrentMonsters( (long) ( Time.deltaTime * 1000 ) );
            }           
        }

        private void TickCurrentMonsters( long i_tick ) {
            MonsterManager.Tick( i_tick );
        }

        private void CreateMonsterViews() {
            foreach ( IGameMonster monster in MonsterManager.CurrentMonsters ) {
                CreateMonsterView( monster );
            }

            foreach ( IGameMonster monster in MonsterManager.RemainingMonsters ) {
                CreateMonsterView( monster );
            }
        }

        private void CreateMonsterView( IGameMonster i_monster ) {
            GameObject monsterObject = gameObject.InstantiateUI( MonsterViewPrefab, gameObject );
            MonsterView monsterView = monsterObject.GetComponent<MonsterView>();
            monsterView.Init( new MonsterPM( i_monster ) );
        }
        
        private List<IGameMonster> GetMonsters() {
            return CurrentDungeonData.Monsters;
        }      
    }
}
