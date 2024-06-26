using System.Collections;
using Enemies;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField]
    private float _speed = 50;
    [SerializeField]
    private float _rotationSpeed = 100;
    [SerializeField]
    private float _lifeTime = 5;
    [SerializeField]
    private ParticleSystem _explosion;
    [SerializeField]
    private AudioClip _moveClip;
    [SerializeField]
    private AudioClip _explosionClip;

    private Rigidbody _rigidbody;
    private AudioSource _audioSource;
    private Transform _target;
    private Vector3 _direction;
    private bool _isFollowingTarget;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        StartCoroutine(DestroyAfterLifetime());
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
        
        PlayAudio(_moveClip);
    }

    private IEnumerator DestroyAfterLifetime()
    {
        yield return new WaitForSeconds(_lifeTime);
        Explode();
    }
    
    private void OnCollisionEnter (Collision other)
    {
        var target = other.gameObject.GetComponent<ITarget>();

        target?.Hit();
        Explode();
    }

    private void Explode()
    {
        Instantiate(_explosion, transform.position, transform.rotation);
        PlayAudio(_explosionClip);
        Destroy(gameObject);
    }

    private void PlayAudio (AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }
}
