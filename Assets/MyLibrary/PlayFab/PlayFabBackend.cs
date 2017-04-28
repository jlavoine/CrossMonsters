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

        public void AuthenticateWithGoogle( string i_authCode ) {

            LoginWithGoogleAccountRequest request = new LoginWithGoogleAccountRequest() {
                TitleId = TITLE_ID,
                ServerAuthCode = i_authCode
            };

            PlayFabClientAPI.LoginWithGoogleAccount( request, ( result ) => {
                UnityEngine.Debug.LogError( "Logged in with playfab id " + result.PlayFabId );
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

#if UNITY_EDITOR
             LoginWithCustomIDRequest request = new LoginWithCustomIDRequest() {
                 TitleId = TITLE_ID,
                 CreateAccount = true,
                 CustomId = i_id
             };

             PlayFabClientAPI.LoginWithCustomID( request, ( result ) => {
                 UnityEngine.Debug.LogError( "Logged in with playfab id " + result.PlayFabId );
                 mPlayFabId = result.PlayFabId;
                 mSessionTicket = result.SessionTicket;

                 IAuthenticationSuccess successResult = null;
                 MyMessenger.Instance.Send<IAuthenticationSuccess>( BackendMessages.AUTH_SUCCESS, successResult );
             },
             ( error ) => { HandleError( error, BackendMessages.AUTH_FAIL ); } );           
#elif UNITY_ANDROID
            LoginWithAndroidDeviceIDRequest request = new LoginWithAndroidDeviceIDRequest() {
                TitleId = TITLE_ID,
                CreateAccount = true,
                AndroidDeviceId = SystemInfo.deviceUniqueIdentifier,
            };

            PlayFabClientAPI.LoginWithAndroidDeviceID( request, ( result ) => {
                UnityEngine.Debug.LogError( "Logged in with playfab id " + result.PlayFabId );
                mPlayFabId = result.PlayFabId;
                mSessionTicket = result.SessionTicket;

                IAuthenticationSuccess successResult = null;
                MyMessenger.Instance.Send<IAuthenticationSuccess>( BackendMessages.AUTH_SUCCESS, successResult );
            },
            ( error ) => { HandleError( error, BackendMessages.AUTH_FAIL ); } );
#elif UNITY_IOS
            LoginWithIOSDeviceIDRequest request = new LoginWithIOSDeviceIDRequest() {
                TitleId = TITLE_ID,
                CreateAccount = true,
                DeviceId = SystemInfo.deviceUniqueIdentifier
            };

            PlayFabClientAPI.LoginWithIOSDeviceID( request, ( result ) => {
                UnityEngine.Debug.LogError( "Logged in with playfab id " + result.PlayFabId );
                mPlayFabId = result.PlayFabId;
                mSessionTicket = result.SessionTicket;

                IAuthenticationSuccess successResult = null;
                MyMessenger.Instance.Send<IAuthenticationSuccess>( BackendMessages.AUTH_SUCCESS, successResult );
            },
            ( error ) => { HandleError( error, BackendMessages.AUTH_FAIL ); } );
#endif
        }

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

        public void GetAccountInfo() {
            StartRequest( "Getting account info" );

            GetAccountInfoRequest request = new GetAccountInfoRequest() {
                PlayFabId = mPlayFabId
            };

            PlayFabClientAPI.GetAccountInfo( request,
                ( result ) => {
                    RequestComplete( "GetAccountInfo() compete, success", LogTypes.Info );
                    UserAccountInfo info = result.AccountInfo;
                    UnityEngine.Debug.LogError( "++++ " + info.PlayFabId);

                    if ( info.GoogleInfo != null ) {
                        UnityEngine.Debug.LogError( "++++ " + info.GoogleInfo.GoogleId );
                    }
                },
                ( error ) => {
                    HandleError( error, "GetAccountInfo() compete, error" );
                } );
        }

        public void UnlinkDeviceFromAccount( Callback<bool> i_requestCallback ) {
            StartRequest( "Unlinking device from account" );

#if UNITY_EDITOR
            UnlinkCustomIDRequest request = new UnlinkCustomIDRequest() {
                  CustomId = SystemInfo.deviceUniqueIdentifier
            };
            UnityEngine.Debug.LogError( "Unlinking " + SystemInfo.deviceUniqueIdentifier );
            PlayFabClientAPI.UnlinkCustomID( request,
                ( result ) => {
                    RequestComplete( "UnlinkDeviceFromAccount() compete, success", LogTypes.Info );
                },
                ( error ) => {
                    HandleError( error, "UnlinkDeviceFromAccount() complete" );
                } );
#elif UNITY_ANDROID
            UnlinkAndroidDeviceIDRequest request = new UnlinkAndroidDeviceIDRequest() {
                AndroidDeviceId = SystemInfo.deviceUniqueIdentifier
            };
            UnityEngine.Debug.LogError( "Unlinking " + SystemInfo.deviceUniqueIdentifier );
            PlayFabClientAPI.UnlinkAndroidDeviceID( request,
                ( result ) => {
                    RequestComplete( "UnlinkDeviceFromAccount() compete, success", LogTypes.Info );
                },
                ( error ) => {
                    HandleError( error, "UnlinkDeviceFromAccount() complete" );
                } );
#elif UNITY_IOS
            UnlinkIOSDeviceIDRequest request = new UnlinkIOSDeviceIDRequest() {
                DeviceId = SystemInfo.deviceUniqueIdentifier
            };
            UnityEngine.Debug.LogError( "Unlinking " + SystemInfo.deviceUniqueIdentifier );
            PlayFabClientAPI.UnlinkIOSDeviceID( request,
                ( result ) => {
                    RequestComplete( "UnlinkDeviceFromAccount() compete, success", LogTypes.Info );
                },
                ( error ) => {
                    HandleError( error, "UnlinkDeviceFromAccount() complete" );
                } );
#endif
        }

        public void LinkDeviceToAccount( Callback<bool> i_requestCallback ) {
            StartRequest( "Linking device to account" );

#if UNITY_EDITOR
            LinkCustomIDRequest request = new LinkCustomIDRequest() {
                CustomId = SystemInfo.deviceUniqueIdentifier,
                ForceLink = true
            };

            PlayFabClientAPI.LinkCustomID( request, 
                ( result ) => {
                    RequestComplete( "LinkDeviceToAccount() compete, success", LogTypes.Info );
                    i_requestCallback( true );
                }, 
                ( error ) => {
                    HandleError( error, "LinkDeviceToAccount() complete, error" );
                    i_requestCallback( false );
                } );                           
#elif UNITY_ANDROID
            LinkAndroidDeviceIDRequest request = new LinkAndroidDeviceIDRequest() {
                AndroidDeviceId = SystemInfo.deviceUniqueIdentifier,
                ForceLink = true
            };

            PlayFabClientAPI.LinkAndroidDeviceID( request,
                ( result ) => {
                    RequestComplete( "LinkDeviceToAccount() compete, success", LogTypes.Info );
                    i_requestCallback( true );
                },
                ( error ) => {
                    HandleError( error, "LinkDeviceToAccount() complete, error" );
                    i_requestCallback( false );
                } );
#elif UNITY_IOS
            LinkIOSDeviceIDRequest request = new LinkIOSDeviceIDRequest() {
                DeviceId = SystemInfo.deviceUniqueIdentifier,
                ForceLink = true
            };

            PlayFabClientAPI.LinkIOSDeviceID( request,
                ( result ) => {
                    RequestComplete( "LinkDeviceToAccount() compete, success", LogTypes.Info );
                    i_requestCallback( true );
                },
                ( error ) => {
                    HandleError( error, "LinkDeviceToAccount() complete, error" );
                    i_requestCallback( false );
                } );
#endif
        }

        public void MakeCloudCall( string i_methodName, Dictionary<string, string> i_params, Callback<Dictionary<string, string>> i_requestSuccessCallback ) {
            StartRequest( "Request for cloud call " + i_methodName );
            LogCloudCallParams( i_params );

            ExecuteCloudScriptRequest request = new ExecuteCloudScriptRequest() {
                FunctionName = i_methodName,
                FunctionParameter = new { data = i_params },
                //RevisionSelection = CloudScriptRevisionOption.Specific,
                //SpecificRevision = YOUR REV # HERE
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

        public void GetItemCatalog( Callback<Dictionary<string, IMyCatalogItem>> successCallback ) {
            StartRequest( "Getting item catalog" );
            GetCatalogItemsRequest request = new GetCatalogItemsRequest();

            PlayFabClientAPI.GetCatalogItems( request, ( result ) => {
                Dictionary<string, IMyCatalogItem> catalogItems = new Dictionary<string, IMyCatalogItem>();
                foreach ( CatalogItem item in result.Catalog ) {
                    RequestComplete( "GetItemCatalog() complete", LogTypes.Info );
                    catalogItems.Add( item.ItemId, new MyCatalogItem() { Id = item.ItemId, Tags = item.Tags } );
                }

                successCallback( catalogItems );
            },
            ( error ) => { HandleError( error, BackendMessages.GET_CATALOG_FAIL ); } );
        }

        public void GetInventory( Callback<List<IMyItemInstance>> successCallback ) {
            StartRequest( "Getting inventory" );
            GetUserInventoryRequest request = new GetUserInventoryRequest();

            PlayFabClientAPI.GetUserInventory( request, ( result ) => {
                RequestComplete( "GetInventory() complete", LogTypes.Info );
                List<IMyItemInstance> items = new List<IMyItemInstance>();
                foreach( ItemInstance item in result.Inventory ) {
                    items.Add( new MyItemInstance() { Id = item.ItemId, Count = (int)item.RemainingUses } );
                }
                successCallback( items );
            },
            (error) => { HandleError( error, BackendMessages.GET_INVENTORY_FAIL ); } );
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

        public void HandleError( PlayFabError i_error, string i_messageType ) {
            //ClientOutOfSync = true;   // should redo another method that does or doesn't do this...not using it for MonsterMatch

            RequestComplete( "Backend failure(" + i_messageType + "): " + i_error.ErrorMessage, LogTypes.Error );

            IBackendFailure failure = new BackendFailure( i_error.ErrorMessage );
            MyMessenger.Instance.Send<IBackendFailure>( BackendMessages.BACKEND_REQUEST_FAIL, failure );
            MyMessenger.Instance.Send<IBackendFailure>( i_messageType, failure );
        }

        public void StartRequest( string i_message ) {
            MyMessenger.Instance.Send<LogTypes, string, string>( MyLogger.LOG_EVENT, LogTypes.Info, "START REQUEST: " + i_message, PLAYFAB );
            CloudRequestCount++;
        }

        public void RequestComplete( string i_message, LogTypes i_messageType ) {
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