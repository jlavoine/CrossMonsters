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
            //UnityEngine.Debug.LogError( Social.localUser.authenticated );
            //UnityEngine.Debug.LogError( Social.localUser.id );
            /*PlayerData test = new PlayerData();
            test.HP = 100;
            test.Defenses = new Dictionary<int, int>() { { 0, 0 }, { 1, 10 } };
            test.Attacks = new Dictionary<int, int>() { { 0, 0 }, { 1, 10 } };

            string json = JsonConvert.SerializeObject( test );
            UnityEngine.Debug.LogError( json );*/

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