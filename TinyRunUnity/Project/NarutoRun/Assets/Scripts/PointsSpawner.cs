using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PointsSpawner : MonoBehaviour
{
    public float WhenToSpawn;
    public bool SpawnInEveryPosition;
    public string[] ObjectsToSpawn;
    public Transform[] PointsList;
    public UnityEvent Spawned;

    public static PointsSpawner Instace;

    private void Start()
    {
        Instace = this;
    }

    public void SpawnAtRandomPos()
    {
        int randomObject = Random.Range(0, ObjectsToSpawn.Length);
        GameObject Object = PoolingManager.Instance.GetPooledObject(ObjectsToSpawn[randomObject].ToString());
        if (Object != null)
        {
            int randomPosition = Random.Range(0, PointsList.Length);
            Object.transform.position = PointsList[randomPosition].position;
            Object.SetActive(true);
            Spawned.Invoke();
        }
    }


    public void SpawnInEveryPoint()
    {
        for(int i = 0; i < PointsList.Length; i++)
        {
            int randomObject = Random.Range(0, ObjectsToSpawn.Length);
            GameObject Object = PoolingManager.Instance.GetPooledObject(ObjectsToSpawn[randomObject].ToString());
            if (Object != null)
            {
                Object.transform.position = PointsList[i].position;
                Object.SetActive(true);
                Object.GetComponent<HealthBehaviour>().Reset();
                Spawned.Invoke();
            }
        }
    }


    IEnumerator SpawnTimer()
    {
        if(FollowerCounter.followerList.Count > 1)
        {
            if(SpawnInEveryPosition)
            {
                SpawnInEveryPoint();
            }
            else
            {
                SpawnAtRandomPos();
            }
        }
        yield return new WaitForSeconds(WhenToSpawn);
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
}
