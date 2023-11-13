using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FollowerCounter : MonoBehaviour
{
    public static FollowerCounter instance;

    public UnityEvent<string> UpdateFollowers;
    private List<GameObject> followerList;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if(instance != this)
            {
                Destroy(gameObject);
            }
        }
        
        followerList = new List<GameObject>();
        Init();
    }

    private void UpdateFollowersText()
    {
        UpdateFollowers.Invoke(followerList.Count + "/" + LevelManager.instance.minimunFollowers);
    }

    public void Init()
    {
        DisableAllFollowers();
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
        UpdateFollowersText();
    }

    public void DisableAllFollowers()
    {
        for (int i = 0; i < followerList.Count; i++)
        {
            followerList[i].GetComponent<FollowerBehaviour>().DeSpawn();
        }
    }

    public int GetCurrentFollowers()
    {
        return followerList.Count;
    }

    public List<GameObject> GetFollowerList()
    {
        return followerList;
    }
}