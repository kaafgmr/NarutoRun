using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeTrashBehaviour : MonoBehaviour
{
    public Transform FinalPos;
    private Transform FirstPos;

    private void Awake()
    {
        FirstPos = transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<PlayerController>(out PlayerController PC))
        {
            StartCoroutine(Move());
        }
        else
        {
            StopMoving();
            ResetPos();
        }

    }

    IEnumerator Move()
    {
        yield return new WaitForSeconds(Time.deltaTime);

        if(transform.position != FinalPos.position)
        {
            transform.position = Vector3.Lerp(transform.position, FinalPos.position, Time.deltaTime * Random.Range(1,2));
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
        transform.position = FirstPos.position;
    }
}
