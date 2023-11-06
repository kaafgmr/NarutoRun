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
        if(collision.gameObject.TryGetComponent<FollowerBehaviour>(out FollowerBehaviour FB))
        {
            if(FB.getIsAttacking())
            {
                HB.GetHurt(1);
                DisappearEffect.Instance.Disappear(transform);
                FollowerCounter.Instance.subtractFollowers(1, collision.gameObject);
            }
        }
    }
}
