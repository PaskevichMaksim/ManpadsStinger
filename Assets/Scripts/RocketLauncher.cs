using System.Collections;
using Enemies;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RocketLauncher : MonoBehaviour
{
    [SerializeField]
    private Rocket _rocketPrefab;
    [SerializeField]
    private Transform _launchPoint;
    [SerializeField]
    private Transform _sightPoint;
    [SerializeField]
    private Renderer _aimIndicator;
    [SerializeField]
    private float _lockOnTime = 2f;
    [SerializeField]
    private float _reloadTime = 3f;

    private XRGrabInteractable _interactable;
    private AudioSource _lockOnSound;
    private Transform _target;
    private Coroutine _lockOnCoroutine;
    private bool _isLockingOn;
    private bool _isTargetDetected;
    private bool _isCanFire = true;


    private static readonly Color notLockedColor = Color.red;
    private static readonly Color lockingColor = Color.yellow;
    private static readonly Color lockedColor = Color.green;

    private void Awake()
    {
        _lockOnSound = GetComponent<AudioSource>();
        _interactable = GetComponent<XRGrabInteractable>();
        _interactable.activated.AddListener(LaunchRocket);
    }

    private void Update()
    {
        PerformAiming();
    }

    private void PerformAiming()
    {
        const float SPHERE_RADIUS = 2f;
        const float MAX_DISTANCE = 300f;

        Vector3 direction = _sightPoint.forward;
        Vector3 sphereStart = _sightPoint.position;

        if (Physics.SphereCast(sphereStart, SPHERE_RADIUS, direction, out RaycastHit hit, MAX_DISTANCE))
        {
            ITarget targetInterface = hit.transform.GetComponent<ITarget>();

            if (targetInterface != null && (hit.transform == _target || hit.distance <= MAX_DISTANCE))
            {
                if (hit.transform == _target)
                {
                    return;
                }

                StartLockOnCoroutine(hit.transform);
            } else
            {
                CancelLockOn();
            }
        } else
        {
            CancelLockOn();
        }
    }

    private void StartLockOnCoroutine(Transform hitTransform)
    {
        if (_lockOnCoroutine != null)
        {
            StopCoroutine(_lockOnCoroutine);
        }

        _lockOnCoroutine = StartCoroutine(LockOnTarget(hitTransform));
    }

    private IEnumerator LockOnTarget(Transform hitTransform)
    {
        _target = hitTransform;
        _isLockingOn = true;
        _aimIndicator.material.color = lockingColor;
        yield return new WaitForSeconds(_lockOnTime);

        if (_isLockingOn && _target != null)
        {
            _isTargetDetected = true;
            _lockOnSound.Play();
            _aimIndicator.material.color = lockedColor;
        } else
        {
            _aimIndicator.material.color = notLockedColor;
        }

        _lockOnCoroutine = null;
    }

    public void LaunchRocket(ActivateEventArgs arg0)
    {
        if (!_isCanFire)
        {
            return;
        }

        _isCanFire = false;
        Rocket rocket = Instantiate(_rocketPrefab, _launchPoint.position, _launchPoint.rotation);

        if (_target != null && _isTargetDetected)
        {
            rocket.Initialize(_target);
        } else
        {
            rocket.InitializeStraight(_launchPoint.forward);
        }

        CancelLockOn();
        StartCoroutine(ReloadRocketLauncher());
    }
    
    private IEnumerator ReloadRocketLauncher()
    {
        yield return new WaitForSeconds(_reloadTime);
        _isCanFire = true;
    }

    private void CancelLockOn()
    {
        _isLockingOn = false;
        _isTargetDetected = false;
        _target = null;

        if (_lockOnCoroutine != null)
        {
            StopCoroutine(_lockOnCoroutine);
            _lockOnCoroutine = null;
        }

        _aimIndicator.material.color = notLockedColor;
    }
}
