using UnityEngine;
using System.Linq;

namespace Puzzle.Farm
{
    public class ParticleByTap : MonoBehaviour
    {
        [SerializeField] private bool _spawnFromObjectCentre = false;
        [SerializeField] private ParticleSystem[] _particleSystem;
        [SerializeField] private Collider _collider;
        [SerializeField] private float _heightSpawn = 0.5f;
        [SerializeField] private AudioClip _audio;
        protected virtual void OnMouseDown()
        {
            ParticleSystem bufParticleSystem = _particleSystem.FirstOrDefault(x => !x.isPlaying);
            if (bufParticleSystem!=null)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (_collider.Raycast(ray, out hit, 100.0f))
                {
                    if(_spawnFromObjectCentre)
                    {
                       // bufParticleSystem.transform.position = transform.position;
                    }
                    else
                    {
                        bufParticleSystem.transform.position = new Vector3(hit.point.x, _heightSpawn, hit.point.z);
                    }
                   
                    bufParticleSystem.Play();
                    if (_audio != null)
                    {
                        AudioSource.PlayClipAtPoint(_audio, transform.position);
                    }
                }
            }          
        }
    }
}
