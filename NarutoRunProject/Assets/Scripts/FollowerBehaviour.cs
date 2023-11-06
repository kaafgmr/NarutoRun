using UnityEngine;

public class FollowerBehaviour : MonoBehaviour
{
    private ObjectsMoveBehaviour OB;
    private AddForce AF;
    private Animator Anim;
    public Transform ChildRotation;
    public bool Picked;
    private bool Attacking;

    private void Awake()
    {
        OB = GetComponent<ObjectsMoveBehaviour>();
        AF = GetComponent<AddForce>();
        Anim = GetComponent<Animator>();
        Picked = false;
        Attacking = false;
        ObjectMode();
    }

    public void ObjectMode()
    {
        OB.enabled = true;
        AF.enabled = false;
        Anim.SetBool("ObjectMode", true);
        Anim.SetBool("FollowMode", false);
        if(gameObject.TryGetComponent<SpringJoint>(out SpringJoint SJ))
        {
            Destroy(SJ);
        }
    }

    public void followerMode(Rigidbody ObjectToFollow)
    {
        OB.enabled = false;
        AF.enabled = true;
        Anim.SetBool("ObjectMode", false);
        Anim.SetBool("FollowMode", true);
        if (!gameObject.TryGetComponent<SpringJoint>(out SpringJoint SJ))
        {
            Attacking = false;
            SpringJoint spring = gameObject.AddComponent<SpringJoint>();
            spring.connectedBody = ObjectToFollow;
            spring.autoConfigureConnectedAnchor = false;
            spring.connectedAnchor = new Vector3(0, 3, -3.5f);
            spring.spring = 1000;
            spring.damper = 100;
            spring.minDistance = 1;
            spring.maxDistance = 4;
        }
        else if(Attacking) //if was attacking
        {
            SpringJoint spring = gameObject.GetComponent<SpringJoint>();
            spring.connectedBody = ObjectToFollow;
            spring.autoConfigureConnectedAnchor = false;
            spring.connectedAnchor = new Vector3(0, 3, -3.5f);
            spring.minDistance = 1;
        }
    }

    public void AttackMode(Rigidbody AttackLimit)
    {
        Attacking = true;
        OB.enabled = false;
        AF.enabled = false;
        Anim.SetBool("ObjectMode", false);
        Anim.SetBool("FollowMode", true);
        if (gameObject.TryGetComponent<SpringJoint>(out SpringJoint SJ))
        {
            SJ.connectedBody = AttackLimit;
            SJ.autoConfigureConnectedAnchor = false;
            SJ.minDistance = 0f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerController>(out PlayerController PC) && !Picked)
        {
            Picked = true;
            transform.position = PC.transform.GetChild(0).position;
            Rigidbody Joint = PC.transform.GetChild(0).GetComponent<Rigidbody>();
            followerMode(Joint);
            FollowerCounter.Instance.addFollowers(1, this.gameObject);
            RangeSpawner.instance.RemoveFollower(this.gameObject);
        }
    }

    public bool getIsAttacking()
    {
        return Attacking;
    }
}
