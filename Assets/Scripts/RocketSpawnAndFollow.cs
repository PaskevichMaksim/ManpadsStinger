using UnityEngine;
using System.Collections;

public class RocketSpawnAndFollow : MonoBehaviour
{
  public GameObject rocketPrefab;  // Перетягніть сюди префаб ракети з інспектора Unity
  public Transform targetObject;   // Ціль, за якою буде летіти ракета
  public float spawnInterval = 2f; // Інтервал між спавнами ракет

  void Start()
  {
    // Запускаємо корутину для спавну ракет кожні spawnInterval секунд
    StartCoroutine(SpawnRockets()); 
  }

  IEnumerator SpawnRockets()
  {
    while (true)
    {
      // Створення ракети в певній позиції з певним обертанням
      Vector3 spawnPosition = new Vector3(0f, 1f, 0f); // Позиція спавну
      Quaternion spawnRotation = Quaternion.identity;  // Обертання спавну (нульове обертання)

      GameObject rocketObject = Instantiate(rocketPrefab, spawnPosition, spawnRotation);
      Rocket rocketScript = rocketObject.GetComponent<Rocket>();

      // Ініціалізуємо ракету наслідувати за ціллю
      if (rocketScript != null && targetObject != null)
      {
        rocketScript.Initialize(targetObject);
      }
      else
      {
        Debug.LogError("Rocket or targetObject is not assigned!");
      }

      yield return new WaitForSeconds(spawnInterval);
    }
  }
}