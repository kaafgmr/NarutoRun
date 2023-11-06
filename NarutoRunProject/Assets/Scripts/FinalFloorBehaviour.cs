using UnityEngine;

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
