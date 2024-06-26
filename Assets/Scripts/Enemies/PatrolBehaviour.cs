using UnityEngine;

namespace Enemies
{
  public class PatrolBehaviour : MonoBehaviour, IPatrolling
  {
    [SerializeField]
    private Transform [] _waypoints;
    [SerializeField]
    private float _speed = 2f;
    [SerializeField]
    private float _waypointTolerance = .1f;

    private int _currentWaypointIndex;
    private bool _isPatrolling = true;
    
    public Transform [] Waypoints
    {
      get => _waypoints;
      set => _waypoints = value;
    }

    private void Update()
    {
      if (_isPatrolling)
      {
        Patrol(); 
      }
    }

    public void Patrol()
    {
      if (_waypoints.Length == 0)
      {
        Debug.LogError("Waypoints not set");
        return;
      }

      Transform targetWaypoint = _waypoints[_currentWaypointIndex];
      Vector3 direction = targetWaypoint.position - transform.position;
      float distance = direction.magnitude;

      if (distance < _waypointTolerance)
      {
        _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;
      } else
      {
        MoveTowards(targetWaypoint);
      }
    }

    public void StopPatrolling()
    {
      _isPatrolling = false;
    }

    private void MoveTowards (Transform waypoint)
    {
      Vector3 direction = (waypoint.position - transform.position).normalized;
      transform.position += direction * (_speed * Time.deltaTime);
      transform.LookAt(waypoint.position);
    }
  }
}
