using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using MyLibrary.PlayFab;
using Newtonsoft.Json;
using System.Collections;
using System;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

namespace MyLibrary {
    public abstract class PlayFabBackend : IBasicBackend {
        public abstract DateTime GetDateTime();

        public const Dictionary<string, string> NULL_CLOUD_PARAMS = null;
        public const Callback<Dictionary<string, string>> NULL_CLOUD_CALLBACK = null;

        private const string TITLE_ID = "86FD";
        public const string PLAYFAB = "PlayFab";
        public const string CLIENT_OUT_OF_SYNC_KEY = "outOfSync";

        // some game cloud calls need to be queued so they don't try and edit the same data at once
        private Queue<QueuedCloudCall> mCloudCallQueue = new Queue<QueuedCloudCall>();
        private bool mWaitingForQueuedCall = false;

        private int mCloudRequestCount = 0;
        public int CloudRequestCount {
            get { return mCloudRequestCount; }
            set { mCloudRequestCount = value; }
        }

        private bool mClientOutOfSync;
        public bool ClientOutOfSync {
            get { return mClientOutOfSync; }
            set {
                mClientOutOfSync = value;

                if ( mClientOutOfSync ) {
                    MyMessenger.Instance.Send<LogTypes, string, string>( MyLogger.LOG_EVENT, LogTypes.Warn, "Client is out of sync", PLAYFAB );
                    MyMessenger.Instance.Send<string>( BackendMessages.BACKEND_OUT_OF_SYNC, "Reason incoming" );
                }
            }
        }

        public string mPlayFabId;
        public string PlayerId { get { return mPlayFabId; } }

        public string mSessionTicket;
        public string SessionTicket { get { return mSessionTicket; } }

        public PlayFabBackend() {
        }

        public bool IsBusy() {
            return CloudRequestCount > 0;
        }

        public void AuthenticateWithGameCenter() {
            LoginWithGameCenterRequest request = new LoginWithGameCenterRequest() {
                TitleId = TITLE_ID,
                PlayerId = ""
            };

            PlayFabClientAPI.LoginWithGameCenter( request, ( result ) => {
                mPlayFabId = result.PlayFabId;
                mSessionTicket = result.SessionTicket;

                IAuthenticationSuccess successResult = null;
                MyMessenger.Instance.Send<IAuthenticationSuccess>( BackendMessages.AUTH_SUCCESS, successResult );
            },
            ( error ) => { HandleError( error, BackendMessages.AUTH_FAIL ); } );
            
        }

        public void AuthenticateWithDevice() {
            LoginWithIOSDeviceIDRequest request = new LoginWithIOSDeviceIDRequest() {
                TitleId = TITLE_ID,
                CreateAccount = true,
                DeviceId = SystemInfo.deviceUniqueIdentifier
            };

            PlayFabClientAPI.LoginWithIOSDeviceID( request, ( result ) => {
                mPlayFabId = result.PlayFabId;
                mSessionTicket = result.SessionTicket;

                IAuthenticationSuccess successResult = null;
                MyMessenger.Instance.Send<IAuthenticationSuccess>( BackendMessages.AUTH_SUCCESS, successResult );
            },
            ( error ) => { HandleError( error, BackendMessages.AUTH_FAIL ); } );
        }

        public void Authenticate( string i_id ) {
            MyMessenger.Instance.Send<LogTypes, string, string>( MyLogger.LOG_EVENT, LogTypes.Info, "Authentication attempt for title " + TITLE_ID, PLAYFAB );

            PlayFabSettings.TitleId = TITLE_ID;

            LoginWithCustomIDRequest request = new LoginWithCustomIDRequest() {
                TitleId = TITLE_ID,
                CreateAccount = true,
                CustomId = i_id
            };

            PlayFabClientAPI.LoginWithCustomID( request, ( result ) => {
                mPlayFabId = result.PlayFabId;
                mSessionTicket = result.SessionTicket;
                
                IAuthenticationSuccess successResult = null;
                MyMessenger.Instance.Send<IAuthenticationSuccess>( BackendMessages.AUTH_SUCCESS, successResult );
            },
            ( error ) => { HandleError( error, BackendMessages.AUTH_FAIL ); } );
        }

        /*public void SetUpCloudServices( bool i_testing ) {
            MyMessenger.Instance.Send<LogTypes, string, string>( MyLogger.LOG_EVENT, LogTypes.Info, "Starting cloud service setup call", PLAYFAB );

            GetCloudScriptUrlRequest request = new GetCloudScriptUrlRequest() {
                Testing = i_testing
            };

            PlayFabClientAPI.GetCloudScriptUrl( request, ( result ) => {
                mCloudServicesSetUp = true;
                MyMessenger.Instance.Send( BackendMessages.CLOUD_SETUP_SUCCESS );
            },
            ( error ) => { HandleError( error, BackendMessages.CLOUD_SETUP_FAIL ); } );
        }*/

        public void QueueCloudCall( string i_methodName, Dictionary<string, string> i_params, Callback<Dictionary<string, string>> i_requestSuccessCallback ) {
            mCloudCallQueue.Enqueue( new QueuedCloudCall( i_methodName, i_params, i_requestSuccessCallback ) );

            if ( !mWaitingForQueuedCall ) {
                SendNextQueuedCloudCall();
            }
        }

        private void SendNextQueuedCloudCall() {
            if ( mCloudCallQueue.Count > 0 ) {
                QueuedCloudCall call = mCloudCallQueue.Dequeue();
                mWaitingForQueuedCall = true;

                MakeCloudCall( call.MethodName, call.Params, ( result ) => {
                    if ( call.SuccessCallback != null ) {
                        call.SuccessCallback( result );
                    }

                    mWaitingForQueuedCall = false;
                    SendNextQueuedCloudCall();
                } );
            }
        }

        public void IsAccountLinkedWithGameCenter( string i_id, Callback<bool> i_requestCallback, Callback i_errorCallback ) {
            StartRequest( "Checking if account is linked with GameCenter" );

            List<string> ids = new List<string>() { i_id };
            GetPlayFabIDsFromGameCenterIDsRequest request = new GetPlayFabIDsFromGameCenterIDsRequest() {
                GameCenterIDs = ids
            };

            PlayFabClientAPI.GetPlayFabIDsFromGameCenterIDs( request, 
                ( result ) => {
                    RequestComplete( "IsAccountLinkedWithGameCenter() compete, success", LogTypes.Info );
                    i_requestCallback( result.Data.Count > 0 );
                }, 
                ( error ) => {
                    RequestComplete( "IsAccountLinkedWithGameCenter() compete, error", LogTypes.Info );
                    i_errorCallback();
                } );
        } 

        public void LinkAccountToGameCenter( string i_id, Callback<bool> i_requestCallback, bool i_forceLink = false ) {
            StartRequest( "Linking account with GameCenter" );

            LinkGameCenterAccountRequest request = new LinkGameCenterAccountRequest() {
                GameCenterId = i_id,
                ForceLink = i_forceLink
            };

            PlayFabClientAPI.LinkGameCenterAccount( request, 
                ( result ) => {
                    RequestComplete( "LinkAccountToGameCenter() compete, success", LogTypes.Info );
                    i_requestCallback( true );
                }, 
                ( error ) => {
                    RequestComplete( "LinkAccountToGameCenter() compete, error", LogTypes.Info );
                    i_requestCallback( false );
                } );
        }

        public void UnlinkGameCenterFromAccount() {
            StartRequest( "Unlinking account from GameCenter" );

            UnlinkGameCenterAccountRequest request = new UnlinkGameCenterAccountRequest();

            PlayFabClientAPI.UnlinkGameCenterAccount( request, 
                ( result ) => {
                    RequestComplete( "UnlinkGameCenterFromAccount() compete, success", LogTypes.Info );
                }, 
                ( error ) => {
                    RequestComplete( "UnlinkGameCenterFromAccount() compete, error", LogTypes.Info );
                } );
        }

        public void UnlinkDeviceFromAccount( Callback<bool> i_requestCallback ) {
            StartRequest( "Unlinking device from account" );

            UnlinkIOSDeviceIDRequest request = new UnlinkIOSDeviceIDRequest() {
                DeviceId = SystemInfo.deviceUniqueIdentifier
            };

            PlayFabClientAPI.UnlinkIOSDeviceID( request, 
                ( result ) => {
                    RequestComplete( "UnlinkDeviceFromAccount() compete, success", LogTypes.Info );
                }, 
                ( error ) => {
                    RequestComplete( "UnlinkDeviceFromAccount() compete, error", LogTypes.Info );
                } );
        }

        public void LinkDeviceToAccount( Callback<bool> i_requestCallback ) {
            StartRequest( "Linking device to account" );

            LinkIOSDeviceIDRequest request = new LinkIOSDeviceIDRequest() {
                DeviceId = SystemInfo.deviceUniqueIdentifier
            };

            PlayFabClientAPI.LinkIOSDeviceID( request, 
                ( result ) => {
                    RequestComplete( "LinkDeviceToAccount() compete, success", LogTypes.Info );
                    i_requestCallback( true );
                }, 
                ( error ) => {
                    RequestComplete( "LinkDeviceToAccount() compete, error", LogTypes.Info );
                    i_requestCallback( false );
                } );
        }

        public void MakeCloudCall( string i_methodName, Dictionary<string, string> i_params, Callback<Dictionary<string, string>> i_requestSuccessCallback ) {
            StartRequest( "Request for cloud call " + i_methodName );
            LogCloudCallParams( i_params );

            ExecuteCloudScriptRequest request = new ExecuteCloudScriptRequest() {
                FunctionName = i_methodName,
                FunctionParameter = new { data = i_params }
            };

            PlayFabClientAPI.ExecuteCloudScript( request, ( result ) => {
                RequestComplete( "Cloud logs for " + i_methodName + "(" + result.ExecutionTimeSeconds + ")", LogTypes.Info );
                OutputResultLogs( result.Logs );                

                Dictionary<string, string> resultsDeserialized = new Dictionary<string, string>();
                if ( result.FunctionResult != null ) {
                    string resultAsString = result.FunctionResult.ToString();
                    resultsDeserialized = JsonConvert.DeserializeObject<Dictionary<string, string>>( resultAsString );

                    CheckForAndUpdateSyncState( resultsDeserialized );
                }

                if ( i_requestSuccessCallback != null ) {
                    i_requestSuccessCallback( resultsDeserialized );
                }
            }, ( error ) => { HandleError( error, i_methodName ); } );
        }

        private void OutputResultLogs( List<LogStatement> i_logs ) {
            string output = string.Empty;
            foreach ( LogStatement log in i_logs ) {
                output += log.Level + ": " + log.Message + "\n";
            }

            MyMessenger.Instance.Send<LogTypes, string, string>( MyLogger.LOG_EVENT, LogTypes.Info, output, PLAYFAB );
        }

        public IEnumerator WaitForCloudCall( string i_methodName, Dictionary<string, string> i_params, Callback<Dictionary<string, string>> i_requestSuccessCallback ) {
            MakeCloudCall( i_methodName, i_params, i_requestSuccessCallback );

            yield return WaitUntilNotBusy();
        }

        private void LogCloudCallParams( Dictionary<string, string> i_params ) {
            if ( i_params != null ) {
                string paramsAsString = "Params: ";
                foreach ( KeyValuePair<string, string> pair in i_params ) {
                    paramsAsString += "\n" + pair.Key + " : " + pair.Value;
                }
                MyMessenger.Instance.Send<LogTypes, string, string>( MyLogger.LOG_EVENT, LogTypes.Info, paramsAsString, PLAYFAB );
            }
        }

        public void UpdatePlayerData( string i_key, string i_data ) {
            Dictionary<string, string> dataToSend = new Dictionary<string, string>() { { i_key, i_data } };

            UpdateUserDataRequest request = new UpdateUserDataRequest() {
                Data = dataToSend
            };

            PlayFabClientAPI.UpdateUserData( request, ( result ) => {
                UnityEngine.Debug.LogError( "Updated " + i_key + " to " + i_data );
            }, 
            ( error ) => { HandleError( error, BackendMessages.UPDATE_USER_DATA_FAIL ); } );
        }

        public void GetTitleData( string i_key, Callback<string> requestSuccessCallback ) {
            StartRequest( "Requesting title data for " + i_key );

            GetTitleDataRequest request = new GetTitleDataRequest() {
                Keys = new List<string>() { i_key }
            };

            PlayFabClientAPI.GetTitleData( request, ( result ) => {
                RequestComplete( "Request title data success for " + i_key, LogTypes.Info );

                // should only call the callback ONCE because there is only one key
                foreach ( var entry in result.Data ) {
                    requestSuccessCallback( entry.Value );
                }
            },
            ( error ) => { HandleError( error, BackendMessages.TITLE_DATA_FAIL ); } );
        }

        public void GetNews( Callback<List<IBasicNewsData>> successCallback ) {
            StartRequest( "Getting news" );

            GetTitleNewsRequest request = new GetTitleNewsRequest();

            PlayFabClientAPI.GetTitleNews( request, ( result ) => {
                RequestComplete( "GetNews complete", LogTypes.Info );

                List<IBasicNewsData> newsList = new List<IBasicNewsData>();
                foreach ( TitleNewsItem newsItem in result.News ) {
                    BasicNewsData news = JsonConvert.DeserializeObject<BasicNewsData>( newsItem.Body );
                    news.Timestamp = newsItem.Timestamp;

                    newsList.Add( news );
                }
                successCallback( newsList );
            },
            ( error ) => { HandleError( error, BackendMessages.GET_NEWS_FAIL ); } );
        }

        public void GetPublicPlayerData( string i_key, Callback<string> requestSuccessCallback ) {
            StartRequest( "Request public player data " + i_key );

            GetUserDataRequest request = new GetUserDataRequest() {
                PlayFabId = PlayerId,
                Keys = new List<string>() { i_key }
            };

            PlayFabClientAPI.GetUserData( request, ( result ) => {
                RequestComplete( "Public player data request complete: " + i_key, LogTypes.Info );

                if ( ( result.Data == null ) || ( result.Data.Count == 0 ) ) {
                    MyMessenger.Instance.Send<LogTypes, string, string>( MyLogger.LOG_EVENT, LogTypes.Error, "No public user data for " + i_key, PLAYFAB );
                }
                else {
                    // should only call the callback ONCE because there is only one key
                    foreach ( var item in result.Data ) {
                        requestSuccessCallback( item.Value.Value );
                    }
                }
            }, ( error ) => { HandleError( error, BackendMessages.PLAYER_DATA_REQUEST_FAIL ); } );
        }

        public void GetReadOnlyPlayerData( string i_key, Callback<string> requestSuccessCallback ) {
            StartRequest( "Request read only player data " + i_key );

            GetUserDataRequest request = new GetUserDataRequest() {
                PlayFabId = PlayerId,
                Keys = new List<string>() { i_key }
            };

            PlayFabClientAPI.GetUserReadOnlyData( request, ( result ) => {
                RequestComplete( "Read only player data request complete: " + i_key, LogTypes.Info );

                if ( ( result.Data == null ) || ( result.Data.Count == 0 ) ) {
                    MyMessenger.Instance.Send<LogTypes, string, string>( MyLogger.LOG_EVENT, LogTypes.Error, "No read only user data for " + i_key, PLAYFAB );
                }
                else {
                    // should only call the callback ONCE because there is only one key
                    foreach ( var item in result.Data ) {
                        requestSuccessCallback( item.Value.Value );
                    }
                }
            }, ( error ) => { HandleError( error, BackendMessages.PLAYER_DATA_REQUEST_FAIL ); } );
        }

        public void GetPlayerDataDeserialized<T>( string i_key, Callback<T> requestSuccessCallback ) {
            StartRequest( "Request player data " + i_key );

            GetUserDataRequest request = new GetUserDataRequest() {
                PlayFabId = PlayerId,
                Keys = new List<string>() { i_key }
            };

            PlayFabClientAPI.GetUserReadOnlyData( request, ( result ) => {
                RequestComplete( "Player data request complete: " + i_key, LogTypes.Info );

                if ( ( result.Data == null ) || ( result.Data.Count == 0 ) ) {
                    MyMessenger.Instance.Send<LogTypes, string, string>( MyLogger.LOG_EVENT, LogTypes.Error, "No user data for " + i_key, PLAYFAB );
                }
                else {
                    // should only call the callback ONCE because there is only one key
                    foreach ( var item in result.Data ) {
                        requestSuccessCallback( JsonConvert.DeserializeObject<T>( item.Value.Value ) );
                    }
                }
            }, ( error ) => { HandleError( error, BackendMessages.PLAYER_DATA_REQUEST_FAIL ); } );
        }

        public void GetVirtualCurrency( string i_key, Callback<int> requestSuccessCallback ) {
            StartRequest( "Requesting virtual currency: " + i_key );

            GetUserInventoryRequest request = new GetUserInventoryRequest();

            PlayFabClientAPI.GetUserInventory( request, ( result ) => {
                RequestComplete( "Request for virtual currency complete: " + i_key, LogTypes.Info );

                int currency = 0;               
                if ( result.VirtualCurrency.ContainsKey( i_key ) ) {
                    currency = result.VirtualCurrency[i_key];
                } else {
                    MyMessenger.Instance.Send<LogTypes, string, string>( MyLogger.LOG_EVENT, LogTypes.Error, "No virtual currency for: " + i_key, PLAYFAB );
                }

                requestSuccessCallback( currency );
            },
            ( error ) => { HandleError( error, BackendMessages.CURRENCY_REQUEST_FAIL ); } );
        }

        /*public void GetAllTitleDataForClass( string i_className, Callback<string> requestSuccessCallback ) {
            StartRequest( "Request all data for class " + i_className );

            Dictionary<string, string> upgradeParams = new Dictionary<string, string>();
            upgradeParams.Add( "Class", i_className );

            RunCloudScriptRequest request = new RunCloudScriptRequest() {
                ActionId = "getAllDataForClass",
                Params = new { data = upgradeParams }
            };

            PlayFabClientAPI.RunCloudScript( request, ( result ) => {
                RequestComplete( "Cloud logs for all data request for " + i_className + "(" + result.ExecutionTime + "):" + result.ActionLog, LogTypes.Info );

                if ( result.Results != null ) {
                    string res = result.Results.ToString();
                    res = res.CleanStringForJsonDeserialization();

                    requestSuccessCallback( res );
                }
            }, ( error ) => { HandleError( error, BackendMessages.TITLE_DATA_FAIL ); } );
        }*/

        protected void HandleError( PlayFabError i_error, string i_messageType ) {
            ClientOutOfSync = true;

            RequestComplete( "Backend failure(" + i_messageType + "): " + i_error.ErrorMessage, LogTypes.Error );

            IBackendFailure failure = new BackendFailure( i_error.ErrorMessage );
            MyMessenger.Instance.Send<IBackendFailure>( BackendMessages.BACKEND_REQUEST_FAIL, failure );
            MyMessenger.Instance.Send<IBackendFailure>( i_messageType, failure );
        }

        protected void StartRequest( string i_message ) {
            MyMessenger.Instance.Send<LogTypes, string, string>( MyLogger.LOG_EVENT, LogTypes.Info, "START REQUEST: " + i_message, PLAYFAB );
            CloudRequestCount++;
        }

        protected void RequestComplete( string i_message, LogTypes i_messageType ) {
            MyMessenger.Instance.Send<LogTypes, string, string>( MyLogger.LOG_EVENT, i_messageType, "REQUEST COMPLETE: " + i_message, PLAYFAB );
            CloudRequestCount--;
        }

        public bool IsClientOutOfSync() {
            return ClientOutOfSync;
        }

        public void ResetSyncState() {
            ClientOutOfSync = false;
        }

        protected void CheckForAndUpdateSyncState( Dictionary<string, string> results ) {
            if ( results.ContainsKey( CLIENT_OUT_OF_SYNC_KEY ) ) {
                bool outOfSync = bool.Parse( results[CLIENT_OUT_OF_SYNC_KEY] );
                ClientOutOfSync = outOfSync;
            }
        }

        public IEnumerator WaitUntilNotBusy() {
            while ( IsBusy() ) {
                yield return 0;
            }
        }
    }
}