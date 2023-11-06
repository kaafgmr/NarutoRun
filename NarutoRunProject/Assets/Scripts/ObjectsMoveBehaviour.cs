using UnityEngine;

public class ObjectsMoveBehaviour : MonoBehaviour
{
    [SerializeField] private float speed = 40;

    private MovementBehaviour MB;
    private bool canMove;

    void Start()
    {
        MB = GetComponent<MovementBehaviour>();
        MB.Init(speed);
        canMove = true;
    }

    void Update()
    {
        if(canMove)
        {
            MB.MoveRB3D(Vector3.back);
        }
        else
        {
            MB.MoveVelocity(Vector3.zero);
        }
    }

    public void SetCanMove(bool value)
    {
        canMove = value;
    }
}
