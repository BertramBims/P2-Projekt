using UnityEngine;
using System.Collections;

public class CarSpawner : MonoBehaviour
{
    public GameObject[] carPrefabs;
    public Transform spawnPoint;
    public int direction = 1; // 1 = right, -1 = left

    public float minSpawnTime = 1f;
    public float maxSpawnTime = 3f;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnCar();
            float waitTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(waitTime);
        }
    }

   void SpawnCar()
{
    GameObject randomCar = carPrefabs[Random.Range(0, carPrefabs.Length)];

    GameObject car = Instantiate(randomCar, spawnPoint.position, Quaternion.identity);

    car.GetComponent<Car>().direction = direction;
}
}