using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CreditsController : MonoBehaviour
{
    public RectTransform RTrans;
    public RectTransform FinalPos;
    public float Speed;
    public Image image;

    private void Start()
    {
        image.color = Color.clear;
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        yield return new WaitForSeconds(0.01f);
        float Distance = RTrans.position.y - FinalPos.position.y;
        if(Distance < 0.2f)
        {
            RTrans.position -= Vector3.down * Speed;
            StartCoroutine(Move());
        }
        else
        {
            RTrans.position = FinalPos.position;
            StopCoroutine(Move());
            StartCoroutine(ShowImage());
        }
    }

    IEnumerator ShowImage()
    {
        yield return new WaitForSeconds(0.01f);
        if(image.color != Color.white)
        {
            image.color = Color.Lerp(image.color, Color.white, Time.deltaTime);
            StartCoroutine(ShowImage());
        }
        else
        {
            StopCoroutine(ShowImage());
        }
    }
}
