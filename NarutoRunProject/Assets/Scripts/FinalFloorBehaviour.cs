using UnityEngine;

public class FinalFloorBehaviour : MonoBehaviour
{
    public Transform MovePlayer;
    private ObjectsMoveBehaviour OMB;
    private void Start()
    {
        OMB = GetComponent<ObjectsMoveBehaviour>();
        if (LevelManager.instance.finishedLevel)
        {
            OMB.SetCanMove(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        OMB.SetCanMove(false);

        if (FollowerCounter.instance.GetCurrentFollowers() >= LevelManager.instance.minimunFollowers)
        {
            LevelManager.instance.Win();
        }
        else
        {
            LevelManager.instance.Lose();
        }
    }
}
