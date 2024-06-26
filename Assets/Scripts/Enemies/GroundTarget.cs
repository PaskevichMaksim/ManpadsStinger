using UnityEngine;
namespace Enemies
{
  public class GroundTarget : MonoBehaviour,ITarget
  {
    [SerializeField]
    private ParticleSystem _explosionEffect;
  
    private IPatrolling _patrollingBehaviour;

    private void Start()
    {
      _patrollingBehaviour = GetComponent<IPatrolling>();
    }

    public void Hit()
    {
      if (_explosionEffect)
      {
        _explosionEffect.Play();
      }

      _patrollingBehaviour?.StopPatrolling();
    }
  }
}
