using UnityEngine;

public class ScareCrowBehaviour : MonoBehaviour
{
    private HealthBehaviour HB;

    private void Awake()
    {
        HB = GetComponent<HealthBehaviour>();
    }

    public void Reset()
    {
        HB.Reset(); 
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.TryGetComponent(out FollowerBehaviour FB)) return;
        if (!FB.GetIsAttacking()) return;
        HB.GetHurt(FB.damage);
        FollowerCounter.instance.SubtractFollower(collision.gameObject);
    }
}
