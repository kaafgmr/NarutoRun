using UnityEngine;

public class AttackLimitController : MonoBehaviour
{
    public Rigidbody player;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.TryGetComponent<SpringJoint>(out SpringJoint SP))
        {
            DisappearEffect.Instance.Disappear(SP.transform);
            FollowerCounter.Instance.subtractFollowers(1, collision.gameObject);
        }
    }
}
