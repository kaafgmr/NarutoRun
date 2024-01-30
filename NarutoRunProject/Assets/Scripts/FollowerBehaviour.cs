using UnityEngine;

public class FollowerBehaviour : MonoBehaviour
{
    public int damage = 1;

    [HideInInspector]public bool picked;
    private ObjectsMoveBehaviour OB;
    private AddForce AF;
    private Animator anim;
    private bool attacking;
    private Rigidbody attackLimitRB;

    private void Awake()
    {
        attackLimitRB = LevelManager.instance.AttackLimitRigidBody;
        OB = GetComponent<ObjectsMoveBehaviour>();
        AF = GetComponent<AddForce>();
        anim = GetComponent<Animator>();
        picked = false;
        attacking = false;
        ObjectMode();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerController PC) && !picked)
        {
            picked = true;
            transform.position = PC.joint.position;
            Rigidbody Joint = PC.joint.GetComponent<Rigidbody>();
            FollowerMode(Joint);
            FollowerCounter.instance.AddFollower(gameObject);
            RangeSpawner.instance.RemoveFollower(gameObject);
        }
    }

    private void AddSpringJoint(Rigidbody connectedBody)
    {
        SpringJoint defaultSpringJoint = gameObject.AddComponent<SpringJoint>();
        defaultSpringJoint.connectedBody = connectedBody;
        defaultSpringJoint.autoConfigureConnectedAnchor = false;
        defaultSpringJoint.connectedAnchor = new Vector3(0, 3, -3.5f);
        defaultSpringJoint.spring = 1000;
        defaultSpringJoint.damper = 100;
        defaultSpringJoint.minDistance = 1;
        defaultSpringJoint.maxDistance = 4;
    }

    public void ObjectMode()
    {
        OB.enabled = true;
        AF.enabled = false;
        attacking = false;
        picked = false;
        anim.SetBool("ObjectMode", true);
        anim.SetBool("FollowMode", false);
        if(gameObject.TryGetComponent(out SpringJoint SJ))
        {
            Destroy(SJ);
        }
    }

    public void FollowerMode(Rigidbody ObjectToFollow)
    {
        OB.enabled = false;
        AF.enabled = true;
        anim.SetBool("ObjectMode", false);
        anim.SetBool("FollowMode", true);
        if (!gameObject.TryGetComponent(out SpringJoint _))
        {
            attacking = false;
            AddSpringJoint(ObjectToFollow);
        }
    }

    public void AttackMode()
    {
        attacking = true;
        OB.enabled = false;
        AF.enabled = false;
        anim.SetBool("ObjectMode", false);
        anim.SetBool("FollowMode", true);

        
        gameObject.TryGetComponent(out SpringJoint SJ);
        if (SJ == null)
        {
            AddSpringJoint(attackLimitRB);
        }

        SJ.connectedBody = attackLimitRB;
    }

    public bool GetIsAttacking()
    {
        return attacking;
    }

    public void DeSpawn()
    {
        gameObject.SetActive(false);
        picked = false;
        attacking = false;
    }
}