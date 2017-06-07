using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyLibrary {
    public class AudioManager : MonoBehaviour, IAudioManager {
        public List<GameObject> AudioLists;

        private Dictionary<string, AudioClip> mAudioClips = new Dictionary<string, AudioClip>();                

        void Start() {
            AddClipsFromLists();            
        }

        private void AddClipsFromLists() {
            foreach ( GameObject objList in AudioLists ) {
                AudioList list = objList.GetComponent<AudioList>();
                foreach ( AudioList.MyAudioClip clip in list.Clips ) {
                    AddClip( clip.Key, clip.Clip );
                }
            }
        }

        private void AddClip( string i_key, AudioClip i_clip ) {
            mAudioClips.Add( i_key, i_clip );
        }

        public void PlayOneShot( string i_key ) {
            if ( mAudioClips.ContainsKey( i_key ) ) {
                GameObject sourceObject = new GameObject( "Sound " + i_key );
                MyAudioSource sourceScript = sourceObject.AddComponent<MyAudioSource>();
                sourceScript.PlayOneShot( mAudioClips[i_key] );
            }
        }
    }
}