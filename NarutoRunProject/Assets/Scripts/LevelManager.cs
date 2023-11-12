using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public string LevelSong;
    public int minimunFollowers;
    public float WhenToSpawnFinalFloor;
    public PlayerController Player;
    public Transform PlayerFinalPos;
    public WinUIAnim winUIAnim;
    public WinUIAnim loseUIAnim;
    
    
    public static LevelManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void StartSpawningFloors()
    {
        if (SingleTimedSpawner.Instance != null)
        {
            SingleTimedSpawner.Instance.StartSpawning();
        }

        if (PointsSpawner.Instace != null)
        {
            PointsSpawner.Instace.StartSpawning();
        }

        RangeSpawner.instance.StartSpawning();
    }

    public void StopSpawningFloors()
    {
        if (SingleTimedSpawner.Instance != null)
        {
            SingleTimedSpawner.Instance.StopSpawning();
        }
        if (PointsSpawner.Instace != null)
        {
            PointsSpawner.Instace.StopSpawning();
        }
        RangeSpawner.instance.StopSpawning();
    }

    public void StartSpawningNextFloors()
    {
        DeSpawnerBehaviour.Instance.SetCanSpawnFloor(true);
    }

    public void StopSpawningNextFloors()
    {
        DeSpawnerBehaviour.Instance.SetCanSpawnFloor(false);
    }

    public void InitializeLevel()
    {
        SpawnerBehaviour.instance.ResetFinalFloor();

        if(AudioManager.instance != null)
        {
            AudioManager.instance.PlayMusic(LevelSong);
        }

        StartSpawningFloors();
        SpawnerBehaviour.instance.Init();

        CanvasTextChanger.instance.Showtext();
        CameraPositions.instance.ChangePositionTo("Playing");

        if(winUIAnim != null)
        {
            winUIAnim.Hide();
        }
        if(loseUIAnim != null)
        {
            loseUIAnim.Hide();
        }
    }

    public void Freeze()
    {
        StopSpawningFloors();
        StopSpawningNextFloors();
        SpawnerBehaviour.instance.FreezeAllFloors();
        Player.Idle();
    }

    public void Unfreeze()
    {
        StartSpawningFloors();
        StartSpawningNextFloors();
        SpawnerBehaviour.instance.UnFreezeAllFloors();
        Player.StartRunning();
    }

    public void Finished()
    {
        StopSpawningFloors();

        SpawnerBehaviour.instance.FreezeAllFloors();

        RemoveAllFollowers();
        CameraPositions.instance.ChangePositionTo("Finish");

        Player.transform.SetPositionAndRotation(PlayerFinalPos.position, Quaternion.Euler(0, 180, 0));

        if (FollowerCounter.instance.GetCurrentFollowers() >= minimunFollowers)
        {
            Win();
        }
        else
        {
            Lose();
        }
    }

    private void RemoveAllFollowers()
    {
        FollowerCounter.instance.DisableAllFollowers();
        RangeSpawner.instance.RemoveNotPickedFollowers();
    }

    public void Win()
    {
        Player.Win();
        winUIAnim.StartAnimation();
    }

    public void Lose()
    {
        Player.Lose();
        loseUIAnim.StartAnimation();
    }
}