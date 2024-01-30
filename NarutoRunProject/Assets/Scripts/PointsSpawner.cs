using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;
using UnityEngine;

public class PointsSpawner : MonoBehaviour
{
    [SerializeField] private float spawnEachSecs;
    [SerializeField] private bool SpawnInEveryPosition;
    [SerializeField] private string[] ObjectsToSpawn;
    [SerializeField] private Transform[] PointsList;

    public UnityEvent Spawned;

    public static PointsSpawner Instace;

    private List<GameObject> spawnedObjects;

    private void Start()
    {
        spawnedObjects = new List<GameObject>();
        Instace = this;
    }

    public void SpawnAtRandomPos()
    {
        int randomObject = Random.Range(0, ObjectsToSpawn.Length);
        GameObject Object = PoolingManager.Instance.GetPooledObject(ObjectsToSpawn[randomObject].ToString());

        int randomPosition = Random.Range(0, PointsList.Length);
        Object.transform.position = PointsList[randomPosition].position;
        AddToList(Object);
        Spawned.Invoke();
    }

    public void SpawnInEveryPoint()
    {
        for(int i = 0; i < PointsList.Length; i++)
        {
            int randomObject = Random.Range(0, ObjectsToSpawn.Length);
            GameObject Object = PoolingManager.Instance.GetPooledObject(ObjectsToSpawn[randomObject].ToString());

            Object.transform.position = PointsList[i].position;
            AddToList(Object);
            Spawned.Invoke();
        }
    }

    IEnumerator SpawnTimer()
    {
        yield return new WaitForSeconds(spawnEachSecs);

        if(SpawnInEveryPosition)
        {
            SpawnInEveryPoint();
        }
        else
        {
            SpawnAtRandomPos();
        }

        StartCoroutine(SpawnTimer());
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnTimer());
    }

    public void StopSpawning()
    {
        StopAllCoroutines();
    }

    public void DisableSpawnedObjs()
    {
        for (int i = 0; i < spawnedObjects.Count; i++)
        {
            if (spawnedObjects[i].activeInHierarchy)
            {
                spawnedObjects[i].SetActive(false);
            }
        }
    }

    public void AddToList(GameObject obj)
    {
        if (spawnedObjects.Contains(obj)) return;

        spawnedObjects.Add(obj);
    }
}