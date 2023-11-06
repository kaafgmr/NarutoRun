using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowLevels : MonoBehaviour
{
    public GameObject MenuToShow;
    public GameObject[] objectsToDesapear;

    private void Start()
    {
        Hide();
    }
    public void Show()
    {
        for(int i = 0; i < objectsToDesapear.Length; i++)
        {
            objectsToDesapear[i].SetActive(false);
        }
        MenuToShow.SetActive(true);
    }

    public void Hide()
    {
        for (int i = 0; i < objectsToDesapear.Length; i++)
        {
            objectsToDesapear[i].SetActive(true);
        }
        MenuToShow.SetActive(false);
    }
}
