using UnityEngine;

namespace TowerOfLondon.Audio
{
    public class SoundManager : MonoBehaviour
    {
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            StopSound();
        }

        public void PlaySound()
        {
            if (_audioSource != null && !_audioSource.isPlaying)
            {
                _audioSource.Play();
            }
        }

        public void StopSound()
        {
            if (_audioSource != null)
            {
                _audioSource.Stop();
            }
        }
    }
}
