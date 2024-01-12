using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public string levelSong;
    public int minimunFollowers;
    public float whenToSpawnFinalFloor;
    public PlayerController player;
    public Transform playerFinalPos;
    public Quaternion playerFinalRotation;
    public WinUIAnim winUIAnim;
    public WinUIAnim loseUIAnim;
    public bool finishedLevel;

    private Vector3 defaultPlayerFinalPos;
    private Quaternion defaultPlayerFinalRotation;

    public static LevelManager instance;

    private void Awake()
    {
        instance = this;
        finishedLevel = false;
        defaultPlayerFinalPos = playerFinalPos.position;
        defaultPlayerFinalRotation = Quaternion.Euler(0, 180, 0);
    }

    public void InitializeLevel()
    {
        finishedLevel = false;

        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayMusic(levelSong);
        }

        player.InitPlayerToPlay();
        playerFinalPos.position = defaultPlayerFinalPos;
        playerFinalRotation = defaultPlayerFinalRotation;

        CanvasTextChanger.instance.Showtext();
        FollowerCounter.instance.Init();
        CameraPositions.instance.ChangePositionTo("Playing");

        if (winUIAnim != null)
        {
            winUIAnim.Hide();
        }
        if (loseUIAnim != null)
        {
            loseUIAnim.Hide();
        }

        SpawnerBehaviour.instance.RemoveAllFloors();
        StartSpawningFloors();
        StartSpawningFollowers();
        SpawnerBehaviour.instance.Init();
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
    }

    public void StartSpawningNextFloors()
    {
        DeSpawnerBehaviour.Instance.SetCanSpawnFloor(true);
    }

    public void StopSpawningNextFloors()
    {
        DeSpawnerBehaviour.Instance.SetCanSpawnFloor(false);
    }
    public void StartSpawningFollowers()
    {
        RangeSpawner.instance.StartSpawning();
    }

    public void StopSpawningFollowers()
    {
        RangeSpawner.instance.StopSpawning();
    }

    public void Freeze()
    {
        StopSpawningFloors();
        StopSpawningNextFloors();
        StopSpawningFollowers();
        SpawnerBehaviour.instance.FreezeAllFloors();
        player.Idle();
    }

    public void Unfreeze()
    {
        StartSpawningFloors();
        StartSpawningNextFloors();
        StartSpawningFollowers();
        SpawnerBehaviour.instance.UnFreezeAllFloors();
        player.StartRunning();
    }

    private void RemoveAllFollowers()
    {
        FollowerCounter.instance.DisableAllFollowers();
        RangeSpawner.instance.CleanObjects();
    }

    public void Finished()
    {
        StopSpawningFollowers();
        RemoveAllFollowers();
        StopSpawningFloors();
        SpawnerBehaviour.instance.FreezeAllFloors();
        CameraPositions.instance.ChangePositionTo("Finish");
        player.MoveToFinalPosition(playerFinalPos.position, playerFinalRotation);
        finishedLevel = true;
    }

    public void Win()
    {
        Finished();
        player.Win();
        winUIAnim.StartAnimation();
    }

    public void Lose()
    {
        Finished();
        player.Lose();
        loseUIAnim.StartAnimation();
    }

    public void Pause()
    {
        player.Idle();
    }
}