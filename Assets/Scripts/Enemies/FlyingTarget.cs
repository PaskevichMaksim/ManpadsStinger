using UnityEngine;
namespace Enemies
{
    public class FlyingTarget : MonoBehaviour, ITarget
    {
        [SerializeField]
        private ParticleSystem _explosionEffect;
        [SerializeField]
        private AudioClip _explosionClip;

        private Rigidbody _rigidbody;
        private IPatrolling _patrollingBehaviour;
        private AudioSource _audioSource;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _patrollingBehaviour = GetComponent<IPatrolling>();
            _audioSource = GetComponent<AudioSource>();
        }

        public void Hit()
        {
            if (_explosionEffect)
            {
               _explosionEffect.Play();
               _audioSource.PlayOneShot(_explosionClip);
            }

            _patrollingBehaviour?.StopPatrolling();
            _rigidbody.useGravity = true;
        }
    }
}
