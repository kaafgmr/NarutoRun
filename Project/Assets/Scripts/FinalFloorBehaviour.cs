using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FinalFloorBehaviour : MonoBehaviour
{
    public Transform MovePlayer;
    private void OnTriggerEnter(Collider other)
    {
        gameObject.GetComponent<ObjectsMoveBehaviour>().SetCanMove(false);
        LevelManager.instance.PlayerFinalPos = MovePlayer;
        LevelManager.instance.Finished();
    }
}
