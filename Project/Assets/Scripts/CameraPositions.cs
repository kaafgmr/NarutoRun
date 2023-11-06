using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static UnityEngine.GraphicsBuffer;

[Serializable]
public class CameraPosition
{
    public string name;
    public Transform position;
}

public class CameraPositions : MonoBehaviour
{
    public float Speed;
    public Camera cameraToMove;
    [SerializeField]
    public List<CameraPosition> positionList;
    [SerializeField]
    private Dictionary<string,Transform> positionDictionary;
    public static CameraPositions instance;

    private void Start()
    {
        positionDictionary = new Dictionary<string, Transform>();
        instance = this;
        for(int i = 0; i < positionList.Count; i++)
        {
            positionDictionary[positionList[i].name] = positionList[i].position;
        }
    }

    public void ChangePositionTo(string name)
    {
        StartCoroutine(SmoothTransition(positionDictionary[name]));
    }

    IEnumerator SmoothTransition(Transform Target)
    {
        yield return new WaitForSeconds(Time.deltaTime);
        if(cameraToMove.transform.position != Target.position && cameraToMove.transform.rotation != Target.rotation)
        {
            cameraToMove.transform.position = Vector3.Lerp(cameraToMove.transform.position, Target.position, Time.deltaTime * Speed);
            cameraToMove.transform.rotation = Quaternion.Lerp(cameraToMove.transform.rotation, Target.rotation, Time.deltaTime * Speed);
            StartCoroutine(SmoothTransition(Target));
        }
        else
        {
            StopAllCoroutines();
        }
    }
}
