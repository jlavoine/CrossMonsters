using UnityEngine;
using MyLibrary;
using System.Collections.Generic;
using Zenject;

namespace MonsterMatch {
    public class CurrentMonstersView : GroupView {
        public GameObject MonsterViewPrefab;

        [Inject]
        IMessageService MessageService;

        [Inject]
        IMonsterManager MonsterManager;

        [Inject]
        IGameManager GameManager;

        [Inject]
        ICurrentDungeonGameManager CurrentDungeonData;

        void Start() {
            ListenForMessages( true );

            List<IMonsterWaveData> monsters = GetMonsters();
            MonsterManager.SetMonsters( monsters );

            CreateMonsterViews();
        }

        void OnDetroy() {
            ListenForMessages( false );
        }

        void Update() {
            if ( GameManager.IsGamePlaying() ) {
                TickCurrentMonsters( (long) ( Time.deltaTime * 1000 ) );
            }           
        }

        private void ListenForMessages( bool i_shouldListen ) {
            if ( i_shouldListen ) {
                MessageService.AddListener( GameMessages.NEW_MONSTER_WAVE_EVENT, CreateMonsterViews );
            } else {
                MessageService.RemoveListener( GameMessages.NEW_MONSTER_WAVE_EVENT, CreateMonsterViews );
            }
        }

        private void TickCurrentMonsters( long i_tick ) {
            MonsterManager.Tick( i_tick );
        }

        private void CreateMonsterViews() {
            foreach ( IGameMonster monster in MonsterManager.CurrentWave.CurrentMonsters ) {
                CreateMonsterView( monster );
            }

            foreach ( IGameMonster monster in MonsterManager.CurrentWave.RemainingMonsters ) {
                CreateMonsterView( monster );
            }
        }

        private void CreateMonsterView( IGameMonster i_monster ) {
            GameObject monsterObject = gameObject.InstantiateUI( MonsterViewPrefab, gameObject );
            MonsterView monsterView = monsterObject.GetComponent<MonsterView>();
            monsterView.Init( new MonsterPM( i_monster ) );
        }
        
        private List<IMonsterWaveData> GetMonsters() {
            return CurrentDungeonData.Monsters;
        }      
    }
}
