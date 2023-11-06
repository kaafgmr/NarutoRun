using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevel : MonoBehaviour
{
    void Start()
    {
        LevelManager.instance.InitializeLevel();
    }
}
