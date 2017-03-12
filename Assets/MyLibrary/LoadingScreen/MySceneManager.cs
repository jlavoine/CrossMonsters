using UnityEngine.SceneManagement;

namespace MyLibrary {
    public class MySceneManager : ISceneManager {
        public void LoadScene( string i_scene ) {
            SceneManager.LoadScene( i_scene );
        }
    }
}