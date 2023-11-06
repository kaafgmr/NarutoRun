using UnityEngine;

public class LoadLevel : MonoBehaviour
{
    void Start()
    {
        LevelManager.instance.InitializeLevel();
    }
}
