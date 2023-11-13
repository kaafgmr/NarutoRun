using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WinUIAnim : MonoBehaviour
{
    [SerializeField] private float minTargetDist = 0.2f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float appearSpeed = 20f;
    [SerializeField] private RectTransform[] objectsToMove;
    [SerializeField] private RectTransform[] InitialPoints;
    [SerializeField] private RectTransform[] FinalPoints;
    [SerializeField] private Image[] ObjectsToAppear;
    
    public static WinUIAnim instance;
    
    private int iterations;

    private void Awake()
    {
        instance = this;
    }

    public void Hide()
    {
        foreach (Image img in ObjectsToAppear)
        {
            img.color = Color.clear;
            img.gameObject.SetActive(false);
        }

        for(int i = 0; i < objectsToMove.Length; i++)
        {
            if (objectsToMove[i].localPosition == InitialPoints[i].localPosition) continue;
            
            StartCoroutine(MoveCounter(InitialPoints[i].localPosition));
        }
    }

    public void StartAnimation()
    {
        foreach (Image img in ObjectsToAppear)
        {
            img.gameObject.SetActive(true);
        }

        iterations = 0;
        StartCoroutine(SmoothAppear());

        foreach (RectTransform rectTransform in FinalPoints)
        {
            StartCoroutine(MoveCounter(rectTransform.localPosition));
        }
    }

    IEnumerator SmoothAppear()
    {
        yield return new WaitForSeconds(Time.deltaTime);
        if(iterations < ObjectsToAppear.Length)
        {
            if (ObjectsToAppear[iterations].color != Color.white)
            {
                ObjectsToAppear[iterations].color = Color.Lerp(ObjectsToAppear[iterations].color, Color.white, Time.deltaTime * appearSpeed);
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

        foreach (RectTransform rectTransform in objectsToMove)
        {
            if (Vector2.Distance(rectTransform.localPosition, Target) > minTargetDist)
            {
                rectTransform.localPosition = Vector2.Lerp(rectTransform.localPosition, Target, Time.deltaTime * moveSpeed);
                StartCoroutine(MoveCounter(Target));
            }
            else
            {
                rectTransform.localPosition = Target;
                StopCoroutine(MoveCounter(Target));
            }
        }
    }
}
