using System.Collections;
using UnityEngine;

public class InstantiateOverTime : MonoBehaviour
{
    public GameObject prefab;
    public float interval = 1f;

    [Header("Optional")]
    public Transform spawnPoint; // if null, uses this transform

    private void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            Spawn();
            yield return new WaitForSeconds(interval);
        }
    }

    void Spawn()
    {
        Transform point = spawnPoint != null ? spawnPoint : transform;

        Instantiate(prefab, point.position, point.rotation);
    }
}