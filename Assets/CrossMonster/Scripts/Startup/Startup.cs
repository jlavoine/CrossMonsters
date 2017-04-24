using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Newtonsoft.Json;
using MyLibrary;
using UnityEngine.SocialPlatforms.GameCenter;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

namespace MonsterMatch {
    public class Startup : MonoBehaviour {

        void Start() {
            //UnityEngine.Debug.LogError( Social.localUser.authenticated );
            //UnityEngine.Debug.LogError( Social.localUser.id );
            StatInfoData test = new StatInfoData();
            test.Stats = new Dictionary<string, StatInfoEntry>();
            test.Stats.Add( "hp", new StatInfoEntry() { Key = "hp", ValuePerLevel = 1 } );
            test.Stats.Add( "PAtk", new StatInfoEntry() { Key = "Patk", ValuePerLevel = 1 } );

            string json = JsonConvert.SerializeObject( test );

#if UNITY_STANDALONE
            Screen.SetResolution( 768, 1024, false );
            Application.runInBackground = true;
#elif UNITY_ANDROID
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
                .AddOauthScope("email")
                .AddOauthScope("profile")
                .RequireGooglePlus()
                .Build();

            PlayGamesPlatform.InitializeInstance( config );
            
            // recommended for debugging:
            PlayGamesPlatform.DebugLogEnabled = true;
            // Activate the Google Play Games platform
            PlayGamesPlatform.Activate();
#endif
            SceneManager.LoadScene( "Login" );
        }
    }
}