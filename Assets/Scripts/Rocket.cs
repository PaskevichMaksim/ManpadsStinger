using System;
using Enemies;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField]
    private float _speed = 50;
    [SerializeField]
    private float _rotationSpeed = 100;
    [SerializeField]
    private ParticleSystem _explosion;

    private Rigidbody _rigidbody;
    private Transform _target;
    private Vector3 _direction;
    private bool _isFollowingTarget;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Initialize (Transform target)
    {
        _target = target;
        _isFollowingTarget = true;
    }

    public void InitializeStraight (Vector3 direction)
    {
        _direction = direction;
        _isFollowingTarget = false;
    }
    
    private void FixedUpdate()
    {
        Move();
    }
    
    private void Move()
    {
        if (_isFollowingTarget && _target != null)
        {
            Vector3 direction = (_target.position - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            
            _rigidbody.MoveRotation(Quaternion.Slerp(transform.localRotation, targetRotation, _rotationSpeed * Time.fixedDeltaTime));
            _rigidbody.velocity = transform.forward * _speed;
            
        } else
        {
            _rigidbody.velocity = _direction * _speed;
        }
    }

    private void OnCollisionEnter (Collision other)
    {
        var target = other.gameObject.GetComponent<ITarget>();

        target?.Hit();
        Explode();
    }

    private void Explode()
    {
        _explosion.Play();
        Destroy(gameObject);
    }
}
