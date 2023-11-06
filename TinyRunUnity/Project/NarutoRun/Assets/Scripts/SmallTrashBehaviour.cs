using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallTrashBehaviour : MonoBehaviour
{
    public Transform FinalRotation;
    private Transform FirstPos;

    private void Awake()
    {
        FirstPos = transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<PlayerController>(out PlayerController PC))
        {
            StartCoroutine(Fall());
        }
        else if(other.TryGetComponent<DeSpawnerBehaviour>(out DeSpawnerBehaviour DSP))
        {
            StopMoving();
            ResetPos();
        }
    }

    IEnumerator Fall()
    {
        yield return new WaitForSeconds(Time.deltaTime);
        if(transform.rotation != FinalRotation.rotation)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, FinalRotation.rotation, Time.deltaTime * 10);
            StartCoroutine(Fall());
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
