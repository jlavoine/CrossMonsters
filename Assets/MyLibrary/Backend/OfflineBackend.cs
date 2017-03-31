using System;
using UnityEngine;
using MyLibrary.PlayFab;    // this is here just for string clean up/deserialization since I just copy the json on the playfab servers for offline usage (unit tests)
using System.Collections.Generic;
using System.Collections;
using Newtonsoft.Json;

namespace MyLibrary {
    public class OfflineBackend : IBasicBackend {
        public string PlayerId {
            get {
                return "Me";
            }
        }

        public string SessionTicket {
            get {
                throw new NotImplementedException();
            }
        }

        public void Authenticate( string i_id ) {
            throw new NotImplementedException();
        }

        public void GetAllTitleDataForClass( string i_className, Callback<string> requestSuccessCallback ) {
            string filePath = Application.streamingAssetsPath + "/OfflineData/" + i_className + ".json";
            string data = GetCleanTextAtPath( filePath );
            requestSuccessCallback( data );
        }

        public void GetReadOnlyPlayerData( string i_key, Callback<string> requestSuccessCallback ) {
            string filePath = Application.streamingAssetsPath + "/OfflineData/PlayerData/" + i_key + ".json";
            string data = GetCleanTextAtPath( filePath );
            requestSuccessCallback( data );
        }

        public void GetTitleData( string i_key, Callback<string> requestSuccessCallback ) {
            if ( i_key == Constants.TITLE_DATA_KEY ) {
                string filePath = Application.streamingAssetsPath + "/OfflineData/Constants.json";
                string data = GetCleanTextAtPath( filePath );
                requestSuccessCallback( data );
            }
        }

        public void GetPlayerDataDeserialized<T>( string i_key, Callback<T> requestSuccessCallback ) {
            requestSuccessCallback( GetPlayerData_Offline<T>( i_key ) );
        }

        public T GetPlayerData_Offline<T>( string i_key ) {
            string filePath = Application.streamingAssetsPath + "/OfflineData/Player/" + i_key + ".json";
            string data = GetCleanTextAtPath( filePath );
            return JsonConvert.DeserializeObject<T>( data );
        }

        public void GetVirtualCurrency( string i_key, Callback<int> requetSuccessCallback ) {
            throw new NotImplementedException();
        }

        public bool IsBusy() {
            throw new NotImplementedException();
        }

        public bool IsClientOutOfSync() {
            throw new NotImplementedException();
        }

        public void MakeCloudCall( string i_methodName, Dictionary<string, string> i_params, Callback<Dictionary<string, string>> requestSuccessCallback ) {
            throw new NotImplementedException();
        }

        public void ResetSyncState() {
            throw new NotImplementedException();
        }

        public void SetUpCloudServices( bool i_testing ) {
            throw new NotImplementedException();
        }

        public IEnumerator WaitForCloudCall( string i_methodName, Dictionary<string, string> i_params, Callback<Dictionary<string, string>> requestSuccessCallback ) {
            throw new NotImplementedException();
        }

        public IEnumerator WaitUntilNotBusy() {
            throw new NotImplementedException();
        }

        private string GetCleanTextAtPath( string i_path ) {
            string data = DataUtils.LoadFileWithPath( i_path );
            data = data.CleanStringForJsonDeserialization();

            return data;
        }

        public void RestartClient() {
            throw new NotImplementedException();
        }

        public DateTime GetDateTime() {
            throw new NotImplementedException();
        }

        public void GetNews() {
            throw new NotImplementedException();
        }

        public void GetNews( Callback<List<IBasicNewsData>> successCallback ) {
            throw new NotImplementedException();
        }

        public void UpdatePlayerData( string i_key, string i_data ) {
            throw new NotImplementedException();
        }

        public void GetPublicPlayerData( string i_key, Callback<string> requestSuccessCallback ) {
            throw new NotImplementedException();
        }

        public void IsAccountLinkedWithGameCenter( string i_id, Callback<bool> requestCallback ) {
            throw new NotImplementedException();
        }

        public void LinkAccountToGameCenter( string i_id, Callback<bool> requestCallback ) {
            throw new NotImplementedException();
        }

        public void IsAccountLinkedWithGameCenter( string i_id, Callback<bool> requestCallback, Callback i_errorCallback ) {
            throw new NotImplementedException();
        }

        public void LinkAccountToGameCenter( string i_id, Callback<bool> requestCallback, bool i_forceLink = false ) {
            throw new NotImplementedException();
        }

        public void LinkDeviceToAccount( Callback<bool> i_requestCallback ) {
            throw new NotImplementedException();
        }

        public void UnlinkGameCenterFromAccount() {
            throw new NotImplementedException();
        }
    }
}