using UnityEngine;

public class ScareCrowBehaviour : MonoBehaviour
{
    [SerializeField] private DisappearEffect DE;

    private HealthBehaviour HB;

    private void Start()
    {
        HB = GetComponent<HealthBehaviour>();
        LevelManager.instance.OnWinLose.AddListener(DisappearCrow);
    }

    public void DisappearCrow()
    {
        DisappearEffect.Instance.Disappear(transform);
        gameObject.SetActive(false);
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
