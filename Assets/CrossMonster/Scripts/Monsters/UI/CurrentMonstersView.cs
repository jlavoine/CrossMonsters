using UnityEngine;
using MyLibrary;
using System.Collections.Generic;

namespace CrossMonsters {
    public class CurrentMonstersView : GroupView {
        public GameObject MonsterViewPrefab;

        private IMonsterManager mManager;

        void Start() {
            List<IGameMonster> monsters = GetMonsters();
            mManager = new MonsterManager( monsters );

            CreateMonsterViews();
        }

        private void CreateMonsterViews() {
            foreach ( IGameMonster monster in mManager.CurrentMonsters ) {
                GameObject monsterObject = gameObject.InstantiateUI( MonsterViewPrefab, gameObject );
                MonsterView monsterView = monsterObject.GetComponent<MonsterView>();
                monsterView.Init( new MonsterPM( monster ) );
            }
        }

        // TODO just for testing
        private List<IGameMonster> GetMonsters() {
            List<IGameMonster> monsters = new List<IGameMonster>();
            

            monsters.Add( GetMonsterDataTEMP( "Blob", 25, new List<int>() { 0, 0 }, 10, 0 ) );
            monsters.Add( GetMonsterDataTEMP( "Dragon", 50, new List<int>() { 1, 1 }, 15, 1 ) );

            return monsters;
        }

        private GameMonster GetMonsterDataTEMP( string i_id, int i_hp, List<int> i_combo, int damage, int damageType ) {
            MonsterData data = new MonsterData();
            data.Id = i_id;
            data.MaxHP = i_hp;
            data.AttackCombo = i_combo;
            data.Damage = damage;
            data.DamageType = damageType;

            return new GameMonster( data );
        }
    }
}
