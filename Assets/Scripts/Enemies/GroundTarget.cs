using UnityEngine;
namespace Enemies
{
  public class GroundTarget : MonoBehaviour,ITarget
  {
    [SerializeField]
    private GameObject _explosionEffect;
  
    private IPatrolling _patrollingBehaviour;

    private void Start()
    {
      _patrollingBehaviour = GetComponent<IPatrolling>();
    }

    public void Hit()
    {
      if (_explosionEffect)
      {
        Instantiate(_explosionEffect, transform.position, transform.rotation);
      }

      _patrollingBehaviour?.StopPatrolling();
    }
  }
}
