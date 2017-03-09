using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CrossMonsters {
    public class Startup : MonoBehaviour {

        void Start() {
            List<string> test = new List<string>();
            test.Add( "a" );
            test.Add( "b" );

            string json = JsonConvert.SerializeObject( test );
            UnityEngine.Debug.LogError( json );

#if UNITY_STANDALONE
            Screen.SetResolution( 768, 1024, false );
            Application.runInBackground = true;
#endif
            SceneManager.LoadScene( "Login" );
        }
    }
}