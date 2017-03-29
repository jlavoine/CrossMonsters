using System.Collections.Generic;
using System.Collections;
using System;

namespace MyLibrary {
    public interface IBasicBackend  {
        string PlayerId { get; }
        string SessionTicket { get; }

        void Authenticate( string i_id );
        //void SetUpCloudServices( bool i_testing );

        void MakeCloudCall( string i_methodName, Dictionary<string,string> i_params, Callback<Dictionary<string, string>> requestSuccessCallback );
        IEnumerator WaitForCloudCall( string i_methodName, Dictionary<string, string> i_params, Callback<Dictionary<string, string>> requestSuccessCallback );

        void GetTitleData( string i_key, Callback<string> requestSuccessCallback );
        void GetNews( Callback<List<IBasicNewsData>> successCallback );
        //void GetAllTitleDataForClass( string i_className, Callback<string> requestSuccessCallback );
        void GetPublicPlayerData( string i_key, Callback<string> requestSuccessCallback );
        void GetReadOnlyPlayerData( string i_key, Callback<string> requestSuccessCallback );
        void UpdatePlayerData( string i_key, string i_data );
        void GetPlayerDataDeserialized<T>( string i_key, Callback<T> requestSuccessCallback );
        void GetVirtualCurrency( string i_key, Callback<int> requetSuccessCallback );

        void IsAccountLinkedWithGameCenter( string i_id, Callback<bool> requestCallback );
        void LinkAccountToGameCenter( string i_id, Callback<bool> requestCallback );

        bool IsClientOutOfSync();
        void ResetSyncState();

        DateTime GetDateTime();

        bool IsBusy();
        IEnumerator WaitUntilNotBusy();
    }
}