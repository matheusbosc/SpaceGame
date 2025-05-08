using UnityEngine;

namespace _Game._Scripts
{
    public class PlayClick : MonoBehaviour
    {
        public AudioSource a;

        public void PlayAudio()
        {
            a.Play();
        }
    }
}