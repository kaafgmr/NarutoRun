using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform joint;
    [SerializeField]private float speed;
    [SerializeField]private Transform mapCenter;
    [SerializeField]private Rigidbody attackLimit;
    [SerializeField]private float minDistToCenter = 0.2f;

    private Vector3 direction;
    private MovementBehaviour MB;
    private bool canMove;

    private void Start()
    {
        MB = GetComponent<MovementBehaviour>();
        MB.Init(speed);
        canMove = true;
    }

    private void Update()
    {
        direction = canMove ? Vector3.right * Input.GetAxisRaw("Horizontal") : Vector3.zero;
        
        float Distance = Vector3.Distance(transform.position, mapCenter.position);

        if (Distance > minDistToCenter && Input.GetAxisRaw("Horizontal") == 0)
        {
            transform.position = Vector3.Lerp(transform.position, mapCenter.position, Time.deltaTime * 2);
        }
        else if (Distance <= minDistToCenter && Input.GetAxis("Horizontal") == 0)
        {
            transform.position = mapCenter.position;
        }

        if(Input.GetKeyDown(KeyCode.Space) && FollowerCounter.followerList.Count > 0)
        {
            FollowerBehaviour objToAttack = FollowerCounter.followerList[Random.Range(0, FollowerCounter.followerList.Count)].GetComponent<FollowerBehaviour>();
            objToAttack.AttackMode();
            FollowerCounter.followerList.Remove(objToAttack.gameObject);
        }
    }

    private void FixedUpdate()
    {
        MB.MoveRB3D(direction);
    }

    public void SetCanMove(bool value)
    {
        canMove = value;
    }
}