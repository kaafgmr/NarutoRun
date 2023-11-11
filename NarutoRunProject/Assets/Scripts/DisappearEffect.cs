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

        DisappearFX.transform.position = Pos.position;
        DisappearFX.GetComponent<ParticleSystem>().Stop();
        DisappearFX.GetComponent<ParticleSystem>().Play();
    }
}
