using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBehaviour : MonoBehaviour
{
    [SerializeField] private int amountToPreload;
    [SerializeField] private string finalFloorName;
    [SerializeField] private Transform whereToSpawnFirst;
    [SerializeField] private string[] objectsToSpawn;

    private Transform endPoint;
    private float timeToSpawnFinalfloor;
    private GameObject finalFloor;

    public static List<GameObject> spawnedFloors;
    public static SpawnerBehaviour instance;

    private void Start()
    {
        instance = this;
        spawnedFloors = new List<GameObject>();
    }

    public void Init()
    {
        endPoint = whereToSpawnFirst;
        timeToSpawnFinalfloor = LevelManager.instance.WhenToSpawnFinalFloor;
        StopAllCoroutines();
        RemoveAllFloors();
        for (int i = 0; i < amountToPreload; i++)
        {
            SpawnRandomFloor();
        }
        StartCoroutine(FinalFloorTimer());
    }

    public void SpawnRandomFloor()
    {
        int RandomFloor = Random.Range(0, objectsToSpawn.Length);
        GameObject Floor = PoolingManager.Instance.GetPooledObject(objectsToSpawn[RandomFloor]);
        Floor.GetComponent<ObjectsMoveBehaviour>().SetCanMove(true);

        Transform FloorStart = Floor.GetComponent<FloorBehaviour>().floorStart;
        Transform FloorEnd = Floor.GetComponent<FloorBehaviour>().floorEnd;
        float ObjectWidth = Vector3.Distance(Floor.transform.position, FloorStart.position);

        Floor.transform.position = endPoint.position;
        Floor.transform.position += Vector3.forward * ObjectWidth;
        endPoint = FloorEnd;

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

        spawnedFloors.Add(Floor);
    }

    public void SpawnNextFloor(GameObject oldFloor)
    {
        SpawnRandomFloor();
        spawnedFloors.Remove(oldFloor);
    }

    public void RemoveAllFloors()
    {
        if (spawnedFloors.Count <= 0) return;
        
        foreach (GameObject floor in spawnedFloors)
        {
            floor.SetActive(false);
        }
    }

    public void ResetFinalFloor()
    {
        GameObject finalFloor = GetFinalFloor();
        if (finalFloor != null)
        {
            finalFloor.GetComponent<ObjectsMoveBehaviour>().SetCanMove(true);
            finalFloor.transform.position = Vector3.zero;
            finalFloor.SetActive(false);
        }
    }

    public void SpawnFinalFloor()
    {
        GameObject Floor = PoolingManager.Instance.GetPooledObject(finalFloorName);

        Transform FloorStart = Floor.transform.GetChild(0).transform;
        Transform FloorEnd = Floor.transform.GetChild(1).transform;

        float ObjectWidth = Vector3.Distance(Floor.transform.position, FloorStart.position);
        FloorStart.position = endPoint.position;
        Floor.transform.position = new Vector3(Floor.transform.position.x, Floor.transform.position.y, FloorStart.position.z + ObjectWidth);
        FloorStart.position = endPoint.position;
        endPoint = FloorEnd;

        Floor.SetActive(true);
        finalFloor = Floor;
        spawnedFloors.Add(Floor);
    }

    IEnumerator FinalFloorTimer()
    {
        yield return new WaitForSeconds(Time.deltaTime);
        if(timeToSpawnFinalfloor > 0)
        {
            timeToSpawnFinalfloor -= Time.deltaTime;
            StartCoroutine(FinalFloorTimer());
        }
        else
        {
            SpawnFinalFloor();
            LevelManager.instance.StopSpawningNextFloors();
            StopAllCoroutines();
        }
    }

    public GameObject GetFinalFloor()
    {
        return finalFloor;
    }
}