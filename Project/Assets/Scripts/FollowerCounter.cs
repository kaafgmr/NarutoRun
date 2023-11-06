using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FollowerCounter : MonoBehaviour
{
    public static FollowerCounter Instance;
    public static List<GameObject> followerList;
    public UnityEvent<string> Followers;
    private int CurrentFollowers;

    private void Awake()
    {
        Instance = this;
        CurrentFollowers = 0;
        followerList = new List<GameObject>();
    }

    public void init(int amount)
    {
        CurrentFollowers = amount;
        Followers.Invoke(CurrentFollowers + "/" + LevelManager.instance.minimunFollowers);
    }

    public void addFollowers(int amount,GameObject Follower)
    {
        if(Follower != null)
        {
            CurrentFollowers += amount;
            Followers.Invoke(CurrentFollowers + "/" + LevelManager.instance.minimunFollowers);
            followerList.Add(Follower);
        }
    }

    public void subtractFollowers(int amount, GameObject Follower)
    {
        if(Follower != null)
        {
            CurrentFollowers -= amount;

            if(CurrentFollowers < 0)
            {
                CurrentFollowers = 0;
            }

            Followers.Invoke(CurrentFollowers + "/" + LevelManager.instance.minimunFollowers);
            Follower.SetActive(false);
            followerList.Remove(Follower);
        }
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
        return CurrentFollowers;
    }
}
