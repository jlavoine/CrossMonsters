
namespace MyLibrary {
    public class IntegrationTestLogger {

        public IntegrationTestLogger() {
            MyMessenger.Instance.AddListener<LogTypes, string, string>( MyLogger.LOG_EVENT, LogWithCategory );
        }

        public void Dispose() {
            MyMessenger.Instance.RemoveListener<LogTypes, string, string>( MyLogger.LOG_EVENT, LogWithCategory );
        }

        public void LogWithCategory( LogTypes i_type, string i_message, string i_category ) {
            string prefix = "(" + i_type.ToString() + ")";
            i_message = string.IsNullOrEmpty( i_category ) ? prefix + i_message : prefix + i_category + ": " + i_message;

            UnityEngine.Debug.Log( i_message );
        }
    }
}