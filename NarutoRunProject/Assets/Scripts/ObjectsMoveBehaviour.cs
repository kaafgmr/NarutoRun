using UnityEngine;

public class ObjectsMoveBehaviour : MonoBehaviour
{
    private float Speed = 40;
    private MovementBehaviour MB;
    private bool CanMove;

    void Start()
    {
        MB = GetComponent<MovementBehaviour>();
        CanMove = true;
    }

    void Update()
    {
        if(CanMove)
        {
            MB.MoveVelocity(Vector3.back * Speed);
        }
        else
        {
            MB.MoveVelocity(Vector3.back * 0);
        }
    }

    public void SetCanMove(bool value)
    {
        CanMove = value;
    }
}
