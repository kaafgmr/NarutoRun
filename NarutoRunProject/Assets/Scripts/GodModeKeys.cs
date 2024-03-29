using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GodModeKeys : MonoBehaviour
{
    private bool Invulnerable;
    private bool Freezed;
    private void Start()
    {
        Invulnerable = false;
        Freezed = false;
    }
    void Update()
    {
        //Load levels
        if(Input.GetKeyDown(KeyCode.Alpha1) && SceneManager.GetActiveScene().name != "Level1")
        {
            MenuControl.instance.LoadScene("Level1");
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2) && SceneManager.GetActiveScene().name != "Level2")
        {
            MenuControl.instance.LoadScene("Level2");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && SceneManager.GetActiveScene().name != "Level3")
        {
            MenuControl.instance.LoadScene("Level3");
        }

        //Add/Remove followers
        if (Input.GetKeyDown(KeyCode.M))
        {
            RangeSpawner.instance.Spawn();
        }
        else if (Input.GetKeyDown(KeyCode.N))
        {
            List<GameObject> spawnedObjects = RangeSpawner.instance.SpawnedObjects;
            RangeSpawner.instance.RemoveFollower(spawnedObjects[Random.Range(0, spawnedObjects.Count)]);
            
            List<GameObject> followerList = FollowerCounter.instance.GetFollowerList();
            FollowerCounter.instance.SubtractFollower(followerList[Random.Range(0, followerList.Count)]);
        }

        //Toggle between been invulnerable or not
        if(Input.GetKeyDown(KeyCode.B))
        {
            if(Invulnerable)
            {
                gameObject.layer = 3;
                Invulnerable = false;
            }
            else
            {
                gameObject.layer = 12;
                Invulnerable = true;
            }
        }

        //freeze game

        if(Input.GetKeyDown(KeyCode.P))
        {
            if (Freezed)//unfreeze
            {
                LevelManager.instance.Unfreeze();
                Freezed = false;
            }
            else //freeze
            {
                LevelManager.instance.Freeze();
                Freezed = true;
            }
        }
    }
}