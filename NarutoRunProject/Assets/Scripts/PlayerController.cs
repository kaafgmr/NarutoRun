using System.Collections.Generic;
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
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        MB = GetComponent<MovementBehaviour>();
        MB.Init(speed);
        canMove = true;
    }

    private void Update()
    {
        float horizontalAxis = Input.GetAxisRaw("Horizontal");

        direction = canMove ? Vector3.right * horizontalAxis : Vector3.zero;
      
        if (horizontalAxis == 0 && canMove)
        {
            float Distance = Vector3.Distance(transform.position, mapCenter.position);

            if (Distance > minDistToCenter)
            {
                transform.position = Vector3.Lerp(transform.position, mapCenter.position, Time.deltaTime * 2);
            }
            else
            {
                transform.position = mapCenter.position;
            }
        }

        List<GameObject> followerList = FollowerCounter.instance.GetFollowerList();

        if (Input.GetKeyDown(KeyCode.Space) && followerList.Count > 0)
        {
            FollowerBehaviour objToAttack = followerList[Random.Range(0, followerList.Count)].GetComponent<FollowerBehaviour>();
            objToAttack.AttackMode();
            followerList.Remove(objToAttack.gameObject);
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

    public void InitPlayerToPlay()
    {
        transform.SetPositionAndRotation(Vector3.zero, Quaternion.Euler(0, 0, 0));
        StartRunning();
    }

    public void StartRunning()
    {
        animator.SetInteger("Run", 1);
        animator.SetBool("Victory", false);
        animator.SetBool("Lose", false);
        SetCanMove(true);
    }
    public void Idle()
    {
        animator.SetInteger("Run", 0);
        animator.SetBool("Victory", false);
        animator.SetBool("Lose", false);
        SetCanMove(false);
    }

    public void Win()
    {
        animator.SetInteger("Run", 0);
        animator.SetBool("Victory", true);
        animator.SetBool("Lose", false);
        SetCanMove(false);
    }

    public void Lose()
    {
        animator.SetInteger("Run", 0);
        animator.SetBool("Victory", false);
        animator.SetBool("Lose", true);
        SetCanMove(false);
    }
}