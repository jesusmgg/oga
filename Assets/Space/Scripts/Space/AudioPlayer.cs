using System.Collections.Generic;
using UnityEngine;

namespace Space
{
    public class AudioPlayer : MonoBehaviour
    {
        public AudioSource audioPlayer;
        public AudioSource musicPlayer;

        public List<AudioClip> musicList;

        int currentTrack = 0;

        void Start()
        {
            musicPlayer.clip = musicList[0];
            musicPlayer.Play();
        }

        public void PlaySound(AudioClip clip)
        {
            audioPlayer.PlayOneShot(clip);
        }

        void Update()
        {
            if (!musicPlayer.isPlaying)
            {
                currentTrack++;

                if (currentTrack >= musicList.Count)
                {
                    currentTrack = 0;
                }

                musicPlayer.clip = musicList[currentTrack];
                musicPlayer.Play();
            }
        }
    }
}