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

    private void Start()
    {
        BurstAmount = 1;
        instance = this;
        SpawnedObjects = new List<GameObject>();
    }

    public void Spawn()
    {
        for(int i = 0; i < BurstAmount; i++)
        {
            int randomObject = Random.Range(0, ObjectsToSpawn.Length);
            GameObject Object = PoolingManager.Instance.GetPooledObject(ObjectsToSpawn[randomObject]);
            if (Object != null)
            {
                Vector3 distanceVector = secondPoint.position - firstPoint.position;
                Object.transform.position = firstPoint.position + Random.value * distanceVector;
                Object.SetActive(true);
                if(Object.TryGetComponent<FollowerBehaviour>(out FollowerBehaviour FB))
                {
                    FB.ObjectMode();
                    FB.picked = false;
                }
                SpawnedObjects.Add(Object);
            }
        }
    }

    IEnumerator SpawnTimer()
    {
        Spawn();
        yield return new WaitForSeconds(Random.Range(minimumTimeRange, maximumTimeRange));
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

    public void RemoveNotPickedFollowers()
    {
        for(int i = 0; i < SpawnedObjects.Count; i++)
        {
            if (SpawnedObjects[i].GetComponent<FollowerBehaviour>().picked == false)
            {
                SpawnedObjects[i].SetActive(false);
                SpawnedObjects.Remove(SpawnedObjects[i]);
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
            SpawnedObjects[i].SetActive(false);
        }
        SpawnedObjects.Clear();
        SpawnedObjects.TrimExcess();
    }
}
