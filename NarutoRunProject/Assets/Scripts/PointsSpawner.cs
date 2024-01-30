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

    private void Start()
    {
        Instace = this;
    }

    public void SpawnAtRandomPos()
    {
        int randomObject = Random.Range(0, ObjectsToSpawn.Length);
        GameObject Object = PoolingManager.Instance.GetPooledObject(ObjectsToSpawn[randomObject].ToString());

        int randomPosition = Random.Range(0, PointsList.Length);
        Object.transform.position = PointsList[randomPosition].position;
        Spawned.Invoke();
    }


    public void SpawnInEveryPoint()
    {
        for(int i = 0; i < PointsList.Length; i++)
        {
            int randomObject = Random.Range(0, ObjectsToSpawn.Length);
            GameObject Object = PoolingManager.Instance.GetPooledObject(ObjectsToSpawn[randomObject].ToString());

            Object.transform.position = PointsList[i].position;
            Object.GetComponent<HealthBehaviour>().Reset();
            Spawned.Invoke();
        }
    }


    IEnumerator SpawnTimer()
    {
        if(FollowerCounter.instance.GetFollowerList().Count > 1)
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
        yield return new WaitForSeconds(spawnEachSecs);
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
