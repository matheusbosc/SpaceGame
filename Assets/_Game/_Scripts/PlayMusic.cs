using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using VHierarchy.Libs;

namespace _Game._Scripts
{
    public class PlayMusic : MonoBehaviour
    {
        public List<AudioClip> songs; // Add your songs in the Inspector
        public AudioSource audioSource; // Assign an AudioSource component

        private bool isWaiting = false;

        void Start()
        {

            var musics = GameObject.FindGameObjectsWithTag("MusicPlayer");

            foreach (var gO in musics)
            {
                if (gO == this.gameObject)
                {
                    continue;
                }
                else
                {
                    Destroy(gameObject);
                }
            }
            
            if (songs.Count > 0 && audioSource != null)
            {
                PlayRandomSong();
            }
            
            DontDestroyOnLoad(this.gameObject);
        }

        void Update()
        {
            if (!audioSource.isPlaying && !isWaiting)
            {
                StartCoroutine(WaitAndPlayNextSong());
            }
        }

        IEnumerator WaitAndPlayNextSong()
        {
            isWaiting = true;
            yield return new WaitForSeconds(5f);
            PlayRandomSong();
            isWaiting = false;
        }

        void PlayRandomSong()
        {
            if (songs.Count == 0) return;

            AudioClip nextSong = songs[Random.Range(0, songs.Count)];
            audioSource.clip = nextSong;
            audioSource.Play();
        }
    }
}