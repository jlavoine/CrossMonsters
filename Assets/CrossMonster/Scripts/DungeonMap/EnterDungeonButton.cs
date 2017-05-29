using UnityEngine;
using Zenject;

namespace MonsterMatch {
    public class EnterDungeonButton : MonoBehaviour {
        public string GameType;
        public int AreaId;
        public int DungeonId;

        [Inject]
        IEnterDungeonPM EnterDungeonPM;
        
        public void OnClick() {
            EnterDungeonPM.SetRequestedDungeon( GameType, AreaId, DungeonId );
            EnterDungeonPM.Show();
        }
    }
}
