using UnityEngine;
using UnityEngine.SceneManagement;

namespace CrossMonsters {
    public class Startup : MonoBehaviour {

        void Start() {
#if UNITY_STANDALONE
            Screen.SetResolution( 768, 1024, false );
            Application.runInBackground = true;
#endif
            SceneManager.LoadScene( "Login" );
        }
    }
}