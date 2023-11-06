using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinUIAnim : MonoBehaviour
{
    public static WinUIAnim instance;
    public RectTransform[] objectsToMove;
    public RectTransform[] InitialPoints;
    public RectTransform[] FinalPoints;
    public Image[] ObjectsToAppear;
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
            if (objectsToMove[i].anchoredPosition != InitialPoints[i].anchoredPosition)
            {
                StartCoroutine(MoveCounter(InitialPoints[i].anchoredPosition));
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
            StartCoroutine(MoveCounter(FinalPoints[i].anchoredPosition));
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
            if (Vector2.Distance(objectsToMove[i].anchoredPosition, Target) > 0.5f)
            {
                objectsToMove[i].anchoredPosition = Vector2.Lerp(objectsToMove[i].anchoredPosition, Target, Time.deltaTime * 5);
                StartCoroutine(MoveCounter(Target));
            }
            else
            {
                objectsToMove[i].anchoredPosition = Target;
                StopCoroutine(MoveCounter(Target));
            }
        }
    }
}
