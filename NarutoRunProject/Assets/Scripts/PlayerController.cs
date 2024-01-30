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
    private Rigidbody RB;

    private void Start()
    {
        RB = GetComponent<Rigidbody>();
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

            if (Distance <= minDistToCenter) return;
            
            Vector3 lerpPos = Vector3.Lerp(transform.position, mapCenter.position, Time.deltaTime * 2);
            direction = (lerpPos - transform.position).normalized;   
        }

        if (Input.GetKeyDown(KeyCode.Space) && canMove)
        {
            List<GameObject> followerList = FollowerCounter.instance.GetFollowerList();

            if (followerList.Count <= 0) return;

            FollowerBehaviour objToAttack = followerList[Random.Range(0, followerList.Count)].GetComponent<FollowerBehaviour>();
            objToAttack.AttackMode();
            FollowerCounter.instance.SubtractFollower(objToAttack.gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            MB.MoveRB3D(direction);
        }
        else
        {
            MB.MoveRB3D(Vector3.zero);
        }
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
        RB.velocity = Vector3.zero;
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

    public void MoveToFinalPosition(Vector3 position, Quaternion rotation)
    {
        transform.SetPositionAndRotation(position, rotation);
    }
}