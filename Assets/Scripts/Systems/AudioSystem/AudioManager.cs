using GameGuruCaseTwo.Datas.AssetData;

namespace GameGuruCaseTwo.Systems.AudioSystem
{
    public class AudioManager
    {
        private readonly CustomAudioSource _pianoAudioSource;

        public AudioManager(GameAssets assets)
        {
            _pianoAudioSource = new CustomAudioSource(assets.PianoSoundClip, "PianoSource", 0.5f);
        }

        public void PlayPianoSource(int combo = 0)
        {
            _pianoAudioSource.AudioSource.pitch = 0.5f + combo * 0.2f;

            _pianoAudioSource.AudioSource.Play();
        }
    }
}