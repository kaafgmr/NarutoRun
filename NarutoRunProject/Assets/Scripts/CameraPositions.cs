using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class CameraPosition
{
    public string name;
    public Transform position;
}

public class CameraPositions : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float minPosDistance;
    [SerializeField] private float minRotDistance;
    [SerializeField] private Camera cameraToMove;
    [SerializeField] private List<CameraPosition> positionList;

    private Dictionary<string,Transform> positionDictionary;

    public static CameraPositions instance;

    private void Start()
    {
        instance = this;
        
        positionDictionary = new Dictionary<string, Transform>();

        foreach (CameraPosition cam in positionList)
        {
            positionDictionary[cam.name] = cam.position;
        }
    }

    public void ChangePositionTo(string name)
    {
        StartCoroutine(SmoothTransition(positionDictionary[name]));
    }

    IEnumerator SmoothTransition(Transform Target)
    {
        yield return new WaitForSeconds(Time.deltaTime);

        float posDis = Vector3.Distance(cameraToMove.transform.position, Target.transform.position);
        float rotDis = Vector3.Distance(cameraToMove.transform.rotation.eulerAngles, Target.transform.rotation.eulerAngles);

        if(posDis > minPosDistance && rotDis > minRotDistance)
        {
            Vector3 newPos = Vector3.Lerp(cameraToMove.transform.position, Target.position, Time.deltaTime * speed);
            Quaternion newRot = Quaternion.Lerp(cameraToMove.transform.rotation, Target.rotation, Time.deltaTime * speed);
            cameraToMove.transform.SetPositionAndRotation(newPos, newRot);
            
            StartCoroutine(SmoothTransition(Target));
        }
        else
        {
            cameraToMove.transform.SetPositionAndRotation(Target.position, Target.rotation);
            StopAllCoroutines();
        }
    }
}
