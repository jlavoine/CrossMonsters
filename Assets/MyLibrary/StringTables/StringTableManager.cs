using UnityEngine;
using System.Collections;

namespace MyLibrary {
    public class StringTableManager : IStringTableManager {
        private IStringTable mTable;

        public void Init( string i_langauge, IBasicBackend i_backend ) {
            UnityEngine.Debug.LogError( "Initing string table for " + i_langauge );
            MyMessenger.Instance.Send<LogTypes, string, string>( MyLogger.LOG_EVENT, LogTypes.Info, "Initing string table for " + i_langauge, "" );

            string tableKey = "SimpleStringTable_" + i_langauge;
            i_backend.GetTitleData( tableKey, CreateTableFromJSON );
        }

        private void CreateTableFromJSON( string i_tableJSON ) {
            mTable = new StringTable( i_tableJSON );
        }

        public string Get( string i_key ) {
            if ( mTable != null ) {
                return mTable.Get( i_key );
            } 
            else {
                return "No string table";
            }
        }
    }
}