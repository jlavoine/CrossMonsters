using System.Collections.Generic;
using UnityEngine;
using System;

namespace MyLibrary {
    public class AudioList : MonoBehaviour {
        public List<MyAudioClip> Clips;

        [Serializable]
        public class MyAudioClip {
            public string Key;
            public AudioClip Clip;
        }
    }
}