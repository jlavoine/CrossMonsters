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
            MonsterData blobData = new MonsterData();
            blobData.Id = "Blob";
            blobData.MaxHP = 25;
            blobData.AttackCombo = new List<int>() { 0, 0 };

            monsters.Add( new GameMonster( blobData ) );

            return monsters;
        }
    }
}
