using UnityEngine;

public class DisappearEffect : MonoBehaviour
{
    public static DisappearEffect Instance;

    private void Start()
    {
        Instance = this;
    }

    public void Disappear(Transform Pos)
    {
        GameObject DisappearFX = PoolingManager.Instance.GetPooledObject("DisappearFX");
        if (DisappearFX != null && Pos != null)
        {
            DisappearFX.transform.position = Pos.position;
            DisappearFX.SetActive(true);
            DisappearFX.GetComponent<ParticleSystem>().Stop();
            DisappearFX.GetComponent<ParticleSystem>().Play();
        }
    }
}
