using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed;
    public Transform Center;
    public Rigidbody AttackLimit;
    private float Distance;
    private Vector3 Direction;
    private MovementBehaviour MB;
    private bool CanMove;

    private void Start()
    {
        MB = GetComponent<MovementBehaviour>();
        MB.Init(Speed);
        CanMove = true;
    }

    private void Update()
    {
        if (CanMove)
        {
            Direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);
        }
        
        Center.position = new Vector3(transform.position.x, transform.position.y, Center.position.z);
        Distance = Mathf.Abs(transform.position.z - Center.position.z);

        if (Distance > 0.2f)
        {
            transform.position = Vector3.Lerp(transform.position, Center.position, Time.deltaTime * 2);
        }

        if(Input.GetKeyDown(KeyCode.Space) && FollowerCounter.followerList.Count > 0)
        {
            FollowerBehaviour AttackerFollower = FollowerCounter.followerList[Random.Range(0, FollowerCounter.followerList.Count)].GetComponent<FollowerBehaviour>();
            AttackerFollower.AttackMode(AttackLimit);
            FollowerCounter.followerList.Remove(AttackerFollower.gameObject);
        }
    }

    private void FixedUpdate()
    {
        MB.MoveVelocity(Direction * Speed);
    }

    public void SetCanMove(bool value)
    {
        CanMove = value;
    }
}