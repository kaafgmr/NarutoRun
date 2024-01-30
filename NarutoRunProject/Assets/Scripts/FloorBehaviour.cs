using UnityEngine;
using UnityEngine.Events;

public class FloorBehaviour : MonoBehaviour
{
    public Transform floorStart;
    public Transform floorEnd;

    public UnityEvent OnSpawn;

    public void Spawned()
    { 
        OnSpawn.Invoke();
    }
}