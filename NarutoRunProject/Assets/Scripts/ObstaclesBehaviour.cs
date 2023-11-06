using UnityEngine;

public class ObstaclesBehaviour : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<FollowerBehaviour>(out FollowerBehaviour FB))
        {
            if(FB.Picked)
            HitFollower(FB.gameObject);
        }
        
        if (collision.gameObject.TryGetComponent<PlayerController>(out PlayerController PC))
        {
            HitPlayer();
        }
    }

    public void HitPlayer()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlaySound("PlayerHit");
        }
            LevelManager.instance.Finished();
    }

    public void HitFollower(GameObject Follower)
    {
        FollowerCounter.Instance.subtractFollowers(1, Follower);
        DisappearEffect.Instance.Disappear(Follower.transform);
        if (AudioManager.instance != null)
            AudioManager.instance.PlaySound("KunaiHit");
    }
}
