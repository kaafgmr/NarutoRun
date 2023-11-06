using System.Collections;
using UnityEngine;

public class SingleTimedSpawner : MonoBehaviour
{
    public Transform WhereToSpawn;
    public float TimeToSpawnInSecs;
    public string[] ObjectsToSpawn;
    public static SingleTimedSpawner Instance;
    private float TimeToSpawn;

    private void Awake()
    {
        Instance = this;
    }

    public void StartSpawning()
    {
        TimeToSpawn = TimeToSpawnInSecs;
        StartCoroutine(Spawn());
    }

    public void StopSpawning()
    {
        StopAllCoroutines();
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(Time.deltaTime);
        TimeToSpawn -= Time.deltaTime;
        if(TimeToSpawn <= 0)
        {
            TimeToSpawn = TimeToSpawnInSecs;
            GameObject SpawnedObject = PoolingManager.Instance.GetPooledObject(ObjectsToSpawn[Random.Range(0, ObjectsToSpawn.Length)]);
            SpawnedObject.transform.position = WhereToSpawn.position;
            SpawnedObject.SetActive(true);
        }
        StartCoroutine(Spawn());
    }
}
