using UnityEngine;
using System.Collections;

namespace MyLibrary {
    public class StringTableManager : IStringTableManager {
        private static IStringTableManager mInstance;
        public static IStringTableManager Instance {
            get {
                if ( mInstance == null ) {
                    mInstance = new StringTableManager();
                }

                return mInstance;
            } 
            set {
                // testing only!
                mInstance = value;
            }
        }

        private IStringTable mTable;

        public void Init( string i_langauge, IBasicBackend i_backend ) {
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