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

    void FixedUpdate()
    {
        int canMoveint = canMove ? 1 : 0;

        MB.MoveVelocity(Vector3.back * canMoveint);
    }

    public void SetCanMove(bool value)
    {
        canMove = value;
    }
}