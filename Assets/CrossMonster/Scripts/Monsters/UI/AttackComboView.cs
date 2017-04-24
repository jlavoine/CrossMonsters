using MyLibrary;
using UnityEngine;
using System.Collections.Generic;

namespace MonsterMatch {
    public class AttackComboView : GroupView {
        public GameObject AttackComboPieceViewPrefab;

        public void Init( List<int> i_combo ) {
            CreateComboPieceViews( i_combo );
        }

        private void CreateComboPieceViews( List<int> i_combo ) {
            foreach ( int comboPiece in i_combo ) {
                GameObject obj = gameObject.InstantiateUI( AttackComboPieceViewPrefab, gameObject );
                AttackComboPieceView view = obj.GetComponent<AttackComboPieceView>();
                view.Init( comboPiece );
            }
        }
    }
}