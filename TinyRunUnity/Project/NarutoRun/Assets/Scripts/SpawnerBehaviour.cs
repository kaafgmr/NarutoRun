using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBehaviour : MonoBehaviour
{
    public int AmountToPreload;
    public string FinalFloorName;
    public Transform WhereToSpawnFirst;
    public string[] ObjectsToSpawn;
    private Transform EndPoint;
    private float TimeToSpawnFinalfloor;
    private GameObject FinalFloor;

    public static List<GameObject> SpawnedFloors;
    public static SpawnerBehaviour instance;

    private void Start()
    {
        instance = this;
        SpawnedFloors = new List<GameObject>();
    }

    public void Init()
    {
        EndPoint = WhereToSpawnFirst;
        TimeToSpawnFinalfloor = LevelManager.instance.WhenToSpawnFinalFloor;
        StopAllCoroutines();
        for (int i = 0; i < AmountToPreload; i++)
        {
            SpawnRandomFloor();
        }
        StartCoroutine(FinalFloorTimer());
    }

    public void SpawnRandomFloor()
    {
        int RandomFloor = Random.Range(0, ObjectsToSpawn.Length);
        GameObject Floor = PoolingManager.Instance.GetPooledObject(ObjectsToSpawn[RandomFloor]);
        if (Floor != null)
        {
            Transform FloorStart = Floor.transform.GetChild(0).transform;
            Transform FloorEnd = Floor.transform.GetChild(1).transform;

            float ObjectWidth = Vector3.Distance(Floor.transform.position, FloorStart.position);
            FloorStart.position = EndPoint.position;
            Floor.transform.position = new Vector3(Floor.transform.position.x, Floor.transform.position.y, FloorStart.position.z + ObjectWidth);
            FloorStart.position = EndPoint.position;
            EndPoint = FloorEnd;

            LargeTrashBehaviour LTB = Floor.GetComponentInChildren<LargeTrashBehaviour>();
            SmallTrashBehaviour STB = Floor.GetComponentInChildren<SmallTrashBehaviour>();

            if(LTB != null)
            {
                LTB.StopMoving();
                LTB.ResetPos();
            }
            else if (STB != null)
            {
                STB.StopMoving();
                STB.ResetPos();
            }

            Floor.SetActive(true);
            SpawnedFloors.Add(Floor);
        }
    }

    public void SpawnFinalFloor()
    {
        GameObject Floor = PoolingManager.Instance.GetPooledObject(FinalFloorName);
        if (Floor != null)
        {
            Transform FloorStart = Floor.transform.GetChild(0).transform;
            Transform FloorEnd = Floor.transform.GetChild(1).transform;

            float ObjectWidth = Vector3.Distance(Floor.transform.position, FloorStart.position);
            FloorStart.position = EndPoint.position;
            Floor.transform.position = new Vector3(Floor.transform.position.x, Floor.transform.position.y, FloorStart.position.z + ObjectWidth);
            FloorStart.position = EndPoint.position;
            EndPoint = FloorEnd;

            Floor.SetActive(true);
            FinalFloor = Floor;
            SpawnedFloors.Add(Floor);
        }
    }

    IEnumerator FinalFloorTimer()
    {
        yield return new WaitForSeconds(Time.deltaTime);
        if(TimeToSpawnFinalfloor > 0)
        {
            TimeToSpawnFinalfloor -= Time.deltaTime;
            StartCoroutine(FinalFloorTimer());
        }
        else
        {
            SpawnFinalFloor();
            LevelManager.instance.StopSpawningFloors();
            StopAllCoroutines();
        }
    }

    public GameObject GetFinalFloor()
    {
        return FinalFloor;
    }
}