using MyLibrary;
using UnityEngine;
using System.Collections.Generic;

namespace MonsterMatch {
    public class Login {

        private string mLoginID;
        private IBasicBackend mBackend;  
        private IAnalyticsTimer mLoginTimer;
        private ILoginMethodManager mLoginManager;

        public Login( IBasicBackend i_backend, IAnalyticsTimer i_loginTimer, string i_loginID, ILoginMethodManager i_loginManager ) {
            mBackend = i_backend;
            mLoginID = i_loginID;   // might use this later
            mLoginManager = i_loginManager;

            mLoginTimer = i_loginTimer;
            mLoginTimer.Start();
        }

        public void Start() {
            MyMessenger.Instance.AddListener<IAuthenticationSuccess>( BackendMessages.AUTH_SUCCESS, OnAuthenticationSucess );            
            MyMessenger.Instance.AddListener<IBackendFailure>( BackendMessages.BACKEND_REQUEST_FAIL, OnBackendFailure );

            mLoginTimer.Start();

            mLoginManager.Authenticate();   // still under development

            //mBackend.Authenticate( "2e4618f28257f978e1ef40d2e6603ee4a89622ed" ); // temporary, use just one user ; keeping for reference         
        }

        public void OnDestroy() {
            MyMessenger.Instance.RemoveListener<IAuthenticationSuccess>( BackendMessages.AUTH_SUCCESS, OnAuthenticationSucess );
            MyMessenger.Instance.RemoveListener<IBackendFailure>( BackendMessages.BACKEND_REQUEST_FAIL, OnBackendFailure );
        }

        private void OnAuthenticationSucess( IAuthenticationSuccess i_success ) {
            mLoginTimer.StepComplete( LibraryAnalyticEvents.AUTH_TIME );

            MyMessenger.Instance.Send<LogTypes, string, string>( MyLogger.LOG_EVENT, LogTypes.Info, "Authenticate success", "" );

            mBackend.MakeCloudCall( "onLogin", null, OnLogin );
        }

        private void OnLogin( Dictionary<string, string> i_result ) {
            mLoginTimer.StepComplete( LibraryAnalyticEvents.ON_LOGIN_TIME );
            mLoginManager.OnLogin();
            CrossBackend backend = (CrossBackend) mBackend;
            backend.SetLoggedInTime( double.Parse( i_result["data"] ) );            

            MyMessenger.Instance.Send<LogTypes, string, string>( MyLogger.LOG_EVENT, LogTypes.Info, "Login success", "" );

            MyMessenger.Instance.Send( BackendMessages.LOGIN_SUCCESS );
        }

        private void OnBackendFailure( IBackendFailure i_failure ) {
            MyMessenger.Instance.Send<LogTypes, string, string>( MyLogger.LOG_EVENT, LogTypes.Info, i_failure.GetMessage(), "" );
            mLoginTimer.StopTimer();
        }
    }
}