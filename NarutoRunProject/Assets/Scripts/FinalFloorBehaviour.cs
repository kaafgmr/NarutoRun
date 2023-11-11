using UnityEngine;

public class FinalFloorBehaviour : MonoBehaviour
{
    public Transform MovePlayer;
    private ObjectsMoveBehaviour OMB;
    private void Start()
    {
        OMB = GetComponent<ObjectsMoveBehaviour>();
    }

    private void OnTriggerEnter(Collider other)
    {
        OMB.SetCanMove(false);
        LevelManager.instance.PlayerFinalPos = MovePlayer;
        LevelManager.instance.Finished();
    }
}
