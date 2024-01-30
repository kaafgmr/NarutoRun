using System.Collections;
using UnityEngine;

public class LargeTrashBehaviour : MonoBehaviour
{
    [SerializeField] private Transform startPos;
    [SerializeField] private Transform finalPos;
    [SerializeField] private FloorBehaviour thisFloor;
    [SerializeField] private float minimalDist;
    

    private void Start()
    {
        thisFloor.OnSpawn.AddListener(ResetTrash);
    }

    private void ResetTrash()
    {
        StopMoving();
        ResetPos();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerController PC))
        {
            StartCoroutine(Move());
        }
    }

    IEnumerator Move()
    {
        yield return new WaitForSeconds(Time.deltaTime);

        float dist = Vector3.Distance(finalPos.position, transform.position);

        if(dist > minimalDist)
        {
            transform.position = Vector3.Lerp(transform.position, finalPos.position, Time.deltaTime * Random.Range(1,2));
            StartCoroutine(Move());
        }
        else
        {
            StopAllCoroutines();
        }
    }

    public void StopMoving()
    {
        StopAllCoroutines();
    }

    public void ResetPos()
    {
        transform.position = startPos.position;
    }
}