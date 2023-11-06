using UnityEngine;

public class ObstaclesBehaviour : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out FollowerBehaviour FB))
        {
            if(FB.picked)
            HitFollower(FB.gameObject);
        }
        
        if (collision.gameObject.TryGetComponent(out PlayerController _))
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
        FollowerCounter.instance.SubtractFollower(Follower);
        DisappearEffect.Instance.Disappear(Follower.transform);
        if (AudioManager.instance != null)
            AudioManager.instance.PlaySound("KunaiHit");
    }
}
