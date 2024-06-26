using UnityEngine;
namespace Enemies
{
    public class FlyingTarget : MonoBehaviour, ITarget
    {
        [SerializeField]
        private ParticleSystem _explosionEffect;
    
        private Rigidbody _rigidbody;
        private IPatrolling _patrollingBehaviour;
        private bool _isFalling;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _patrollingBehaviour = GetComponent<IPatrolling>();
        }

        public void Hit()
        {
            if (_explosionEffect)
            {
               _explosionEffect.Play();
            }

            _patrollingBehaviour?.StopPatrolling();

            _isFalling = true;
            _rigidbody.useGravity = true;
        }

        private void Update()
        {
            if (_isFalling)
            {
            
            }
        }
    }
}
