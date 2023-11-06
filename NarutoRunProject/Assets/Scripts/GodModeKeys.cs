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
            MenuControl.instance.LoadLevel(1);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2) && SceneManager.GetActiveScene().name != "Level2")
        {
            MenuControl.instance.LoadLevel(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && SceneManager.GetActiveScene().name != "Level3")
        {
            MenuControl.instance.LoadLevel(3);
        }

        //Add/Remove followers
        if (Input.GetKeyDown(KeyCode.M))
        {
            RangeSpawner.instance.Spawn();
        }
        else if (Input.GetKeyDown(KeyCode.N))
        {
           RangeSpawner.instance.RemoveFollower(RangeSpawner.SpawnedObjects[Random.Range(0, RangeSpawner.SpawnedObjects.Count)]);
           FollowerCounter.instance.SubtractFollower(FollowerCounter.followerList[Random.Range(0, FollowerCounter.followerList.Count)]);
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