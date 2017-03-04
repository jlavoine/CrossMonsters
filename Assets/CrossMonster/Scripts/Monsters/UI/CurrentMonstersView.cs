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

        // TODO just for testing
        private List<IGameMonster> GetMonsters() {
            List<IGameMonster> monsters = new List<IGameMonster>();
            
            monsters.Add( GetMonsterDataTEMP( "Blob", 25, new List<int>() { 0, 0 }, 10, 0, 5000 ) );
            monsters.Add( GetMonsterDataTEMP( "Dragon", 50, new List<int>() { 1, 1 }, 15, 1, 10000 ) );
            monsters.Add( GetMonsterDataTEMP( "Blob", 25, new List<int>() { 3, 2 }, 10, 0, 5000 ) );
            monsters.Add( GetMonsterDataTEMP( "Blob", 25, new List<int>() { 2, 0 }, 10, 0, 5000 ) );
            monsters.Add( GetMonsterDataTEMP( "Goblin", 25, new List<int>() { 3, 3 }, 10, 0, 5000 ) );

            return monsters;
        }

        private GameMonster GetMonsterDataTEMP( string i_id, int i_hp, List<int> i_combo, int damage, int damageType, long attackRate ) {
            MonsterData data = new MonsterData();
            data.Id = i_id;
            data.MaxHP = i_hp;
            data.AttackCombo = i_combo;
            data.Damage = damage;
            data.DamageType = damageType;
            data.AttackRate = attackRate;

            return new GameMonster( data );
        }
    }
}
