using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeSpawner : MonoBehaviour
{
    public Transform firstPoint;
    public Transform secondPoint;
    public float BurstAmount;
    public float minimumTimeRange;
    public float maximumTimeRange;
    public string[] ObjectsToSpawn;
    public static RangeSpawner instance;
    public static List<GameObject> SpawnedObjects;

    private bool canSpawn;

    private void Start()
    {
        BurstAmount = 1;
        instance = this;
        canSpawn = true;
        SpawnedObjects = new List<GameObject>();
    }

    public void Spawn()
    {
        for(int i = 0; i < BurstAmount; i++)
        {
            Vector3 distanceVector = secondPoint.position - firstPoint.position;
            
            int randomObject = Random.Range(0, ObjectsToSpawn.Length);
            GameObject Object = PoolingManager.Instance.GetPooledObject(ObjectsToSpawn[randomObject]);

            Object.transform.position = firstPoint.position + Random.value * distanceVector;

            if(Object.TryGetComponent(out FollowerBehaviour FB))
            {
                FB.ObjectMode();
            }

            SpawnedObjects.Add(Object);
        }
    }

    IEnumerator SpawnTimer()
    {
        yield return new WaitForSeconds(Random.Range(minimumTimeRange, maximumTimeRange));

        if(canSpawn)
        {
            Spawn();
            StartCoroutine(SpawnTimer());
        }
    }

    public void StartSpawning()
    {
        canSpawn = true;
        StartCoroutine(SpawnTimer());
    }

    public void StopSpawning()
    {
        canSpawn = false;
        StopAllCoroutines();
    }

    public void RemoveNotPickedFollowers()
    {
        for (int i = 0; i < SpawnedObjects.Count; i++)
        {
            FollowerBehaviour followerBehaviour = SpawnedObjects[i].GetComponent<FollowerBehaviour>();
            if (!followerBehaviour.picked)
            {
                followerBehaviour.DeSpawn();
                RemoveFollower(SpawnedObjects[i]);
            }
        }
    }

    public void RemoveFollower(GameObject Follower)
    {
        if(Follower != null)
        {
            SpawnedObjects.Remove(Follower);
        }
    }

    public void CleanObjects()
    {
        for(int i = 0; i < SpawnedObjects.Count; i++)
        {
            SpawnedObjects[i].GetComponent<FollowerBehaviour>().DeSpawn();
        }
        SpawnedObjects.Clear();
        SpawnedObjects.TrimExcess();
    }
}
