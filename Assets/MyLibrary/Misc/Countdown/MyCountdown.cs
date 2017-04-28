using System;

namespace MyLibrary {
    public class MyCountdown : IMyCountdown {
        readonly IBackendManager mBackend;

        private long mTargetTimeMs;
        public long TargetTimeMs { get { return mTargetTimeMs; } set { mTargetTimeMs = value; } }

        private long mRemainingTimeMs;
        public long RemainingTimeMs { get { return mRemainingTimeMs; } set { mRemainingTimeMs = value; } }

        private ICountdownCallback mCallback;
        public ICountdownCallback Callback { get { return mCallback; } set { mCallback = value; } }

        public MyCountdown( IBackendManager i_backend, long i_targetTimeMs, ICountdownCallback i_callback = null ) {
            mBackend = i_backend;
            Callback = i_callback;
            TargetTimeMs = i_targetTimeMs;

            CalculateRemainingTime();
        }

        public void Tick( long i_tickTimeMs ) {
            RemainingTimeMs -= i_tickTimeMs;
            if ( RemainingTimeMs <= 0 ) {
                RemainingTimeMs = 0;
                SendAndRemoveCallback();
            }
        }

        private void CalculateRemainingTime() {
            DateTime targetTimeDate = new DateTime( 1970, 1, 1, 0, 0, 0, DateTimeKind.Utc ).AddMilliseconds( TargetTimeMs );
            DateTime currentTime = mBackend.GetBackend<IBasicBackend>().GetDateTime();
            TimeSpan difference = targetTimeDate - currentTime;
            RemainingTimeMs = (long)difference.TotalMilliseconds;

            if (RemainingTimeMs < 0 ) {
                RemainingTimeMs = 0;
            }
        }

        private void SendAndRemoveCallback() {
            if ( Callback != null ) {
                Callback.SendCallback();
                Callback = null;
            }
        }
    }

    public interface ICountdownCallback {
        void SendCallback();
    }

    public class CountdownCallback : ICountdownCallback {
        private Callback mCallback;

        public CountdownCallback( Callback i_callback ) {
            mCallback = i_callback;
        }

        public void SendCallback() {
            if ( mCallback != null ) {
                mCallback();
            }
        }
    }
}