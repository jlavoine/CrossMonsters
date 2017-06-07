using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyLibrary {
    public class MyAudioSource : MonoBehaviour {
        private AudioSource audioSource;

        private float m_fLifetime;
        public float GetLifetime() {
            return m_fLifetime;
        }

        public void PlayOneShot( AudioClip i_clip ) {
            if ( i_clip == null ) {
                Destroy( gameObject );
                return;
            }

            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = i_clip;
            audioSource.loop = false;

            //float fDefaultVolume = Constants_OLD.GetConstant<float>( "DefaultVolume" );
            //audioSource.volume = fDefaultVolume;

            /*ID_Audio dataAudio = IDL_Audio.GetData( strResource );
            if ( dataAudio != null ) {
                audioSource.volume = dataAudio.GetVolume();
                audioSource.loop = dataAudio.ShouldLoop();
            }*/

            // change pitch if necessary
            /*if ( i_hashOptional.ContainsKey( "pitch" ) ) {
                float fPitch = (float) i_hashOptional["pitch"];
                audioSource.pitch = fPitch;
            }*/

            //gameObject.transform.parent = tf;
            //gameObject.transform.position = tf.position;
            audioSource.Play();

            // add destroy script -- if the audio doesn't loop
            if ( audioSource.loop == false ) {
                m_fLifetime = i_clip.length + 0.1f;
                //if ( i_hashOptional.ContainsKey( "Time" ) )
                   // m_fLifetime = (float) i_hashOptional["Time"];

                DestroyThis scriptDestroy = gameObject.AddComponent<DestroyThis>();
                scriptDestroy.SetLife( m_fLifetime );
            }
        }
    }
}
