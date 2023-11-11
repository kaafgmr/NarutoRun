using UnityEngine;

public class DeSpawnerBehaviour : MonoBehaviour
{
    private bool CanSpawnFloor;

    public static DeSpawnerBehaviour Instance;

    private void Start()
    {
        Instance = this;
        CanSpawnFloor = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out FollowerBehaviour FB))
        {
             FB.TryDeSpawn();
        }
        else if(CanSpawnFloor && other.TryGetComponent(out ObjectsMoveBehaviour _))
        {
            SpawnerBehaviour.instance.SpawnNextFloor(other.gameObject);
        }

        other.gameObject.SetActive(false);
    }

    public void SetCanSpawnFloor(bool value)
    {
        CanSpawnFloor = value;
    }

    public void StartSpawningFloors()
    {
        SetCanSpawnFloor(true);
    }

    public void StopSpawningFloors()
    {
        SetCanSpawnFloor(false);
    }
}
