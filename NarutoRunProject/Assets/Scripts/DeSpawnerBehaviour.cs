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

    public void SetCanSpawnFloor(bool value)
    {
        CanSpawnFloor = value;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<FollowerBehaviour>(out FollowerBehaviour FB) && FB.picked)
        {
            if(FB.picked)
            {
                FB.picked = false;
                FollowerCounter.instance.SubtractFollower(FB.gameObject);
            }
        }

        
        if(CanSpawnFloor && other.TryGetComponent<ObjectsMoveBehaviour>(out ObjectsMoveBehaviour OMB))
        {
            SpawnerBehaviour.instance.SpawnRandomFloor();
            SpawnerBehaviour.SpawnedFloors.Remove(other.gameObject);
        }

        other.gameObject.SetActive(false);
    }
}
