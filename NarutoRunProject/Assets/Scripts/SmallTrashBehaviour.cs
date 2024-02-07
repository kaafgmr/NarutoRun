using System.Collections;
using UnityEngine;

public class SmallTrashBehaviour : MonoBehaviour
{
    [SerializeField] private Transform finalRotation;
    [SerializeField] private FloorBehaviour thisFloor;
    [SerializeField] private float minAngle;
    private Quaternion firstRot;

    private void Start()
    {
        firstRot = transform.rotation;
        thisFloor.OnSpawn.AddListener(ResetTrash);
    }

    private void ResetTrash()
    {
        StopMoving();
        ResetRotation();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerController _))
        {
            StartCoroutine(Fall());
        }
    }

    IEnumerator Fall()
    {
        yield return new WaitForSeconds(Time.deltaTime);

        float angle = Quaternion.Angle(transform.rotation, finalRotation.rotation);

        if(angle > minAngle)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, finalRotation.rotation, Time.deltaTime * 10);
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
    public void ResetRotation()
    {
        transform.rotation = firstRot;
    }
}