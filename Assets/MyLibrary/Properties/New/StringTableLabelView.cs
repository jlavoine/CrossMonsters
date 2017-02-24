using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace MyLibrary {
    public class StringTableLabelView : MonoBehaviour {
        public string StringKey;

        private TextMeshProUGUI mTextFieldPro;
        public TextMeshProUGUI TextFieldPro {
            get {
                if ( mTextFieldPro == null ) {
                    mTextFieldPro = GetComponent<TextMeshProUGUI>();
                }

                return mTextFieldPro;
            }
        }

        private Text mTextField;
        public Text TextField {
            get {
                if ( mTextField == null ) {
                    mTextField = GetComponent<Text>();
                }

                return mTextField;
            }
        }

        protected void SetText( string i_text ) {
            if ( TextFieldPro != null ) {
                TextFieldPro.text = i_text;
            }
            else if ( TextField != null ) {
                TextField.text = i_text;
            }
            else {
                MyMessenger.Instance.Send<LogTypes, string, string>( MyLogger.LOG_EVENT, LogTypes.Error, "No text element for LabelView: " + gameObject, "UI" );
            }
        }

        void Start() {
            string label = StringTableManager.Instance.Get( StringKey );

            SetText( label );
        }
    }
}
