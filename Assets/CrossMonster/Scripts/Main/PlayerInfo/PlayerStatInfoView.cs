using MyLibrary;
using Zenject;
using UnityEngine;

namespace MonsterMatch {
    public class PlayerStatInfoView : GroupView {
        [Inject]
        IPlayerStatInfoPM PM;

        void Start() {
            SetModel( PM.ViewModel );
        }

        public void ShowView() {
            PM.Show();
        }

        public void HideView() {
            PM.Hide();
        }
    }
}