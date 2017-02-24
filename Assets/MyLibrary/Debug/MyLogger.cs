
namespace MyLibrary {
    public class MyLogger {
        public const string LOG_EVENT = "Log";

        public MyLogger() {
            MyMessenger.Instance.AddListener<LogTypes, string, string>( LOG_EVENT, LogWithCategory );
        }

        public void Dispose() {
            MyMessenger.Instance.RemoveListener<LogTypes, string, string>( LOG_EVENT, LogWithCategory );
        }

        public void LogWithCategory( LogTypes i_type, string i_message, string i_category ) {
            i_message = string.IsNullOrEmpty( i_category ) ? i_message : i_category + ": " + i_message;

            // TODO: make this better
            switch ( i_type ) {
                case LogTypes.Info:
                    UnityEngine.Debug.Log( i_message );
                    break;
                case LogTypes.Error:
                case LogTypes.Fatal:
                case LogTypes.Warn:
                    UnityEngine.Debug.LogError( i_message );
                    break;                                    
                default:
                    UnityEngine.Debug.LogError( "Debug type " + i_type + " unsupported!" );
                    break;
            }
        }
    }
}