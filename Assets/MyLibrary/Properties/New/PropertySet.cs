
using System.Collections.Generic;

namespace MyLibrary {
    public class PropertySet {
        public Dictionary<string, Property> Properties = new Dictionary<string, Property>();

        public void CreateProperty( string i_key ) {
            SetProperty( i_key, null );
        }

        public Property GetProperty( string i_key ) {
            if ( HasProperty( i_key ) ) {
                return Properties[i_key];
            }
            else {
                Debug.LogError( "Trying to get a property that doesn't exist: " + i_key );
                return null;
            }
        }


        // can't use this because of AOT issues on iOS...!?
        public T GetPropertyValue<T>( string i_key ) {
            T value = default( T );

            if ( Properties.ContainsKey( i_key ) ) {
                Property property = Properties[i_key];
                value = property.GetValue<T>();
            }

            return value;
        }

        public int GetPropertyValue_Int( string i_key ) {
            if ( Properties.ContainsKey( i_key ) ) {
                Property property = Properties[i_key];
                return property.GetValue_Int();
            } else {
                return 0;
            }
        }

        public float GetPropertyValue_Float( string i_key ) {
            if ( Properties.ContainsKey( i_key ) ) {
                Property property = Properties[i_key];
                return property.GetValue_Float();
            }
            else {
                return 0f;
            }
        }

        public string GetPropertyValue_String( string i_key ) {
            if ( Properties.ContainsKey( i_key ) ) {
                Property property = Properties[i_key];
                return property.GetValue_String();
            }
            else {
                return string.Empty;
            }
        }

        public bool GetPropertyValue_Bool( string i_key ) {
            if ( Properties.ContainsKey( i_key ) ) {
                Property property = Properties[i_key];
                return property.GetValue_Bool();
            }
            else {
                return false;
            }
        }

        public bool HasProperty( string i_key ) {
            if ( Properties == null ) { 
                Properties = new Dictionary<string, Property>();
            }

            return Properties.ContainsKey( i_key );
        }

        public void SetProperty( string i_key, object i_value ) {
            if ( !HasProperty( i_key ) ) {
                Properties.Add( i_key, new Property( i_key ) );
            }

            Property property = Properties[i_key];
            property.SetValue( i_value );
        }
    }
}