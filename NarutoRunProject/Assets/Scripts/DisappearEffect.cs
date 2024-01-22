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
        GameObject disappearFX = PoolingManager.Instance.GetPooledObject("DisappearFX");
        disappearFX.GetComponent<DisappearFXBehaviour>().Disappear(Pos);
    }
}
