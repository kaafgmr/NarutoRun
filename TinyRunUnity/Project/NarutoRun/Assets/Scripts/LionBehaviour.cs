using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LionBehaviour : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<FollowerBehaviour>(out FollowerBehaviour FB))
        {
            HitFollower(FB.gameObject);
        }
        else if (other.TryGetComponent<PlayerController>(out PlayerController PC))
        {
            HitPlayer();
        }
    }

    public void HitPlayer()
    {
        if (AudioManager.instance != null)
            AudioManager.instance.PlaySound("PlayerHit");
    }

    public void HitFollower(GameObject Follower)
    {
        FollowerCounter.Instance.subtractFollowers(1, Follower);
        if (AudioManager.instance != null)
            AudioManager.instance.PlaySound("KunaiHit");
    }
}
