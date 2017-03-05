using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CrossMonsters {
    public class Startup : MonoBehaviour {

        void Start() {
            List<TreasureData> all = new List<TreasureData>();
            all.Add( new TreasureData() { Id = "hi", LootType = LootTypes.Treasure } );
            all.Add( new TreasureData() { Id = "bye", LootType = LootTypes.Treasure } );

            string json = JsonConvert.SerializeObject( all );
            UnityEngine.Debug.LogError( json );

#if UNITY_STANDALONE
            Screen.SetResolution( 768, 1024, false );
            Application.runInBackground = true;
#endif
            SceneManager.LoadScene( "Login" );
        }
    }
}