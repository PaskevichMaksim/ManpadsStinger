using UnityEngine;

namespace Enemies
{
  public interface IPatrolling 
  {
    public Transform[] Waypoints { get; set; }

    public void Patrol();
  }
}
