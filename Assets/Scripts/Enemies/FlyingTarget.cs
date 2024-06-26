using UnityEngine;
namespace Enemies
{
    public class FlyingTarget : MonoBehaviour, ITarget
    {
        [SerializeField]
        private GameObject _explosionEffect;
    
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
                Instantiate(_explosionEffect, transform.position, transform.rotation);
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
