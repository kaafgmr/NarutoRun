using UnityEngine;

public class AttackLimitController : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (!collision.gameObject.TryGetComponent(out FollowerBehaviour FB)) return;
        if (!FB.GetIsAttacking()) return;
        
        DisappearEffect.Instance.Disappear(FB.transform);
        FollowerCounter.instance.SubtractFollower(FB.gameObject);
    }
}
