using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FollowerCounter : MonoBehaviour
{
    public static FollowerCounter instance;
    public static List<GameObject> followerList;
    public UnityEvent<string> UpdateFollowers;

    private void Awake()
    {
        instance = this;
        followerList = new List<GameObject>();
    }

    private void UpdateFollowersText()
    {
        UpdateFollowers.Invoke(followerList.Count + "/" + LevelManager.instance.minimunFollowers);
    }

    public void Init()
    {
        UpdateFollowersText();
    }

    public void AddFollower(GameObject Follower)
    {
        if(Follower == null) return;
        
        followerList.Add(Follower);
        UpdateFollowersText();
    }

    public void SubtractFollower(GameObject Follower)
    {
        if (Follower == null) return;
        
        followerList.Remove(Follower);
        Follower.SetActive(false);
        UpdateFollowersText();
    }

    public void DisableAllFollowers()
    {
        for(int i = 0; i < followerList.Count; i++)
        {
            followerList[i].SetActive(false);
        }
        followerList.Clear();
        followerList.TrimExcess();
    }

    public int GetCurrentFollowers()
    {
        return followerList.Count;
    }
}
