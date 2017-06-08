using UnityEngine;
using Zenject;

namespace MyLibrary {
    public class PlaySound : MonoBehaviour {
        [Inject]
        IAudioManager Audio;

        public string Key;

        public void Play() {
            Audio.PlayOneShot( Key );
        }
    }
}
