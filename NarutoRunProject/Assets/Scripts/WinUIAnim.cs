using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WinUIAnim : MonoBehaviour
{
    [SerializeField] private float minTargetDist = 0.2f;
    public RectTransform[] objectsToMove;
    public RectTransform[] InitialPoints;
    public RectTransform[] FinalPoints;
    public Image[] ObjectsToAppear;
    
    public static WinUIAnim instance;
    
    private int iterations;

    private void Awake()
    {
        instance = this;
    }

    public void Hide()
    {
        for (int i = 0; i < ObjectsToAppear.Length; i++)
        {
            ObjectsToAppear[i].color = Color.clear;
            ObjectsToAppear[i].gameObject.SetActive(false);
        }

        for(int i = 0; i < objectsToMove.Length; i++)
        {
            if (objectsToMove[i].localPosition != InitialPoints[i].localPosition)
            {
                StartCoroutine(MoveCounter(InitialPoints[i].localPosition));
            }
        }
    }

    public void StartAnimation()
    {
        for(int i = 0; i < ObjectsToAppear.Length; i++)
        {
            ObjectsToAppear[i].gameObject.SetActive(true);
        }
        iterations = 0;
        StartCoroutine(SmoothAppear());
        for(int i = 0; i < FinalPoints.Length; i++)
        {
            StartCoroutine(MoveCounter(FinalPoints[i].localPosition));
        }
    }

    IEnumerator SmoothAppear()
    {
        yield return new WaitForSeconds(Time.deltaTime);
        if(iterations < ObjectsToAppear.Length)
        {
            if (ObjectsToAppear[iterations].color != Color.white)
            {
                ObjectsToAppear[iterations].color = Color.Lerp(ObjectsToAppear[iterations].color, Color.white, Time.deltaTime * 20);
                StartCoroutine(SmoothAppear());
            }
            else
            {
                iterations++;
                StartCoroutine(SmoothAppear());
            }
        }
        else
        {
            StopCoroutine(SmoothAppear());
        }
    }

    IEnumerator MoveCounter(Vector2 Target)
    {
        yield return new WaitForSeconds(Time.deltaTime);
        for(int i = 0; i < objectsToMove.Length; i++)
        {
            if (Vector2.Distance(objectsToMove[i].localPosition, Target) > minTargetDist)
            {
                objectsToMove[i].localPosition = Vector2.Lerp(objectsToMove[i].localPosition, Target, Time.deltaTime * 5);
                StartCoroutine(MoveCounter(Target));
            }
            else
            {
                objectsToMove[i].localPosition = Target;
                StopCoroutine(MoveCounter(Target));
            }
        }
    }
}
