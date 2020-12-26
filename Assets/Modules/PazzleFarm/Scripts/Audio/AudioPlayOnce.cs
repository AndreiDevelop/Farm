using UnityEngine;

namespace Puzzle.Farm
{
    public class AudioPlayOnce : MonoBehaviour
    {
        [SerializeField] private AudioClip _audio;
        public void Play()
        {
            if(_audio!=null)
            {
                AudioSource.PlayClipAtPoint(_audio, transform.position);
            }            
        }
    }
}
