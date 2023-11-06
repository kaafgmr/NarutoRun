using UnityEngine;

public class DestroyDisableObjects : MonoBehaviour
{
    public void DisableObject()
    {
        gameObject.SetActive(false);        
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }

    public void DisableAndDestroy()
    {
        DisableObject();
        DestroyObject();
    }
}
