﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Newtonsoft.Json;
using MyLibrary;
using UnityEngine.SocialPlatforms.GameCenter;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

namespace CrossMonsters {
    public class Startup : MonoBehaviour {

        void Start() {
            /*UnityEngine.Debug.LogError( Social.localUser.authenticated );
            UnityEngine.Debug.LogError( Social.localUser.id );
            DungeonGameSessionData test = new DungeonGameSessionData();
                                  

            string json = JsonConvert.SerializeObject( test );
            UnityEngine.Debug.LogError( json );*/

#if UNITY_STANDALONE
            Screen.SetResolution( 768, 1024, false );
            Application.runInBackground = true;
#elif UNITY_ANDROID
            UnityEngine.Debug.LogError( "about to init gpgs" );
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();

            PlayGamesPlatform.InitializeInstance( config );
            // recommended for debugging:
            PlayGamesPlatform.DebugLogEnabled = true;
            // Activate the Google Play Games platform
            PlayGamesPlatform.Activate();
            UnityEngine.Debug.LogError( "----done" );

#endif
            SceneManager.LoadScene( "Login" );
        }
    }
}