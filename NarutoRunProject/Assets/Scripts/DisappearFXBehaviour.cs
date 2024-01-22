using System.Collections;
using UnityEngine;

public class DisappearFXBehaviour : MonoBehaviour
{
    private ParticleSystem PS;

    private void Awake()
    {
        PS = GetComponent<ParticleSystem>();
    }

    public void Disappear(Transform pos)
    {
        transform.position = pos.position;
        PS.Stop();
        PS.Play();

        StartCoroutine(DisappearAffetSecs(PS.main.duration));
    }

    IEnumerator DisappearAffetSecs(float time)
    {
        yield return new WaitForSeconds(time);
        PS.Stop();
        gameObject.SetActive(false);
    }
}