using UnityEngine;

namespace GameGuruCaseTwo.Systems.AudioSystem
{
    public class CustomAudioSource
    {
        public AudioSource AudioSource { get; private set; }

        public CustomAudioSource(AudioClip clip, string name = "audioSource", float pitch = 0.5f)
        {
            GameObject source = new GameObject(name);
            source.AddComponent<AudioSource>();
            AudioSource = source.GetComponent<AudioSource>();
            AudioSource.clip = clip;
            AudioSource.pitch = pitch;
            AudioSource.playOnAwake = false;
        }
    }
}